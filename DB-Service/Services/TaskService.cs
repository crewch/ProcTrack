using DB_Service.Clients.Http;
using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Models;
using DB_Service.Tools;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DB_Service.Services
{
	public class TaskService : ITaskService
	{
		private readonly DataContext _context;
		private readonly IAuthDataClient _authClient;
		private readonly ILogService _logService;
		private readonly INotificationService _notificationService;

		public TaskService(DataContext context, 
						   IAuthDataClient authClient,
						   ILogService logService,
						   INotificationService notificationService)
		{
			_context = context;
			_authClient = authClient;
			_logService = logService;
			_notificationService = notificationService;
		}

		public async Task<TaskDto> AssignTask(int UserId, int Id)
		{
			var taskDto = await GetTaskById(Id);

			if (taskDto == null)
			{
				return null;
			}

			var taskModel = await _context.Tasks
					.Include(t => t.Stage)
					.ThenInclude(s => s.Status)
					.Where(t => t.Id == Id)
					.FirstOrDefaultAsync();

			var oldApprovedAt = taskModel.ApprovedAt;
			var oldSigned = taskModel.Signed;
			var oldSignId = taskModel.SignId;

			var user = await _authClient.GetUserById(UserId);

			taskModel.ApprovedAt = DateTime.Now.AddHours(3);
			taskModel.Signed = user.LongName;
			taskModel.SignId = user.Id;

			if (taskModel.StageId != null)
			{
				var stage = await _context.Stages
					.Include(s => s.Status)
					.Where(s => s.Id == taskModel.StageId && 
                       s.Status.Title.ToLower() != "не начат" &&
                       s.Status.Title.ToLower() != "остановлен")
					.FirstOrDefaultAsync();

				var status = await _context.Statuses
					.Where(s => s.Id == stage.StatusId)
					.FirstOrDefaultAsync();
				
				if (status.Title.ToLower() == "отправлен на проверку")
				{
					var newStatus = await _context.Statuses
							.Where(s => s.Title.ToLower() == "принят на проверку")
							.FirstOrDefaultAsync();

					stage.Status = newStatus;
				}
				
				await _context.SaveChangesAsync();

				var processForNotification = await _context.Processes		//NOTE:notification
					.Include(p => p.Stages)
					.Where(p => p.Id == stage.ProcessId)
					.FirstOrDefaultAsync();

				if (processForNotification.Stages != null)
				{
					foreach (var iStage in processForNotification.Stages)
					{
						var groupHolds = await _authClient.FindHold(iStage.Id, "Stage");
                
						foreach (var hold in groupHolds)
						{
							if (hold.Groups == null) continue;
							foreach (var group in hold.Groups)
							{
								var users = await _authClient.GetUsersByGroupId(group.Id);
								foreach (var iUser in users)
								{
									_notificationService.SendNotification(processForNotification.Id, iStage.Id, iUser.Id, "AssignTask");
								}
							}
						}
					}
				}
			}

			if (user != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Task",
                    Field = "ApprovedAt, Signed, SignId",
                    Operation = "Update",
                    LogId = Id.ToString(),
                    Old = $"{oldApprovedAt}, {oldSigned}, {oldSignId}",
                    New = $"{taskModel.ApprovedAt}, {taskModel.Signed}, {taskModel.SignId}",
                    Author = user.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

			return await GetTaskById(Id);
		}

		public async Task<CommentDto> CreateCommment(int Id, CommentDto data)
		{
			var taskDto = await GetTaskById(Id);

			if (taskDto == null)
			{
				return null;
			}

			var taskModel = await _context.Tasks
				.Where(t => t.Id == taskDto.Id)
				.FirstOrDefaultAsync();

			var commentModel = new Comment
			{
				UserId = data.User.Id,
				Text = data.Text,
				FileRef = data.FileRef,
				CreatedAt = DateTime.Now.AddHours(3),
			};

			await _context.Comments.AddAsync(commentModel);
			taskModel.Comments.Add(commentModel);
			
			await _context.SaveChangesAsync();

			var stage = await _context.Stages			//NOTE:notification
				.Include(s => s.Status)
				.Where(s => s.Id == taskModel.StageId)
				.FirstOrDefaultAsync();

			var processForNotification = await _context.Processes
				.Include(p => p.Stages)
				.Where(p => p.Id == stage.ProcessId)
				.FirstOrDefaultAsync();

			if (processForNotification.Stages != null)
			{
				foreach (var iStage in processForNotification.Stages)
				{
					var groupHolds = await _authClient.FindHold(iStage.Id, "Stage");
               
					foreach (var hold in groupHolds)
					{
						if (hold.Groups == null) continue;
						foreach (var group in hold.Groups)
						{
							var users = await _authClient.GetUsersByGroupId(group.Id);
							foreach (var iUser in users)
							{
								_notificationService.SendNotification(processForNotification.Id, iStage.Id, iUser.Id, "CreateComment");
							}
						}
					}
				}
			}

			return new CommentDto
			{
				Id = commentModel.Id,
				Text = commentModel.Text,
				FileRef = commentModel.FileRef,
				CreatedAt = DateParser.Parse(DateTime.Now),
				User = data.User,
			};
		}

		public async Task<List<CommentDto>> GetComentsByTaskId(int Id)
		{
			var comments = await _context.Comments
					.Where(c => c.TaskId == Id)
					.ToListAsync();

			if (comments.Count == 0)
			{
				return null;
			}

			var dtos = new List<CommentDto>();

			foreach (var comment in comments)
			{
				var userDto = new UserDto();

				if (comment.UserId != null)
				{
					userDto = await _authClient.GetUserById((int)comment.UserId);
				}

				dtos.Add(new CommentDto
				{
					Id = comment.Id,
					CreatedAt = comment.CreatedAt == null ? null : DateParser.Parse((DateTime)comment.CreatedAt),
					Text = comment.Text,
					FileRef = comment.FileRef,
					User = userDto,
				});
			}

			return dtos;
		}

		public async Task<TaskDto> GetTaskById(int Id)
		{
			var task = await _context.Tasks
					.Include(t => t.Comments)
					.Where(t => t.Id == Id)
					.FirstOrDefaultAsync();

			if (task == null)
			{
				return null;
			}

			var userDto = new UserDto();

			if (task.SignId != null)
			{
				userDto = await _authClient.GetUserById((int)task.SignId);
			}

			var comments = new List<CommentDto>();

			foreach (var iComment in task.Comments)
			{
				comments.Add(new CommentDto
				{
					Id = iComment.Id,
					Text = iComment.Text,
					FileRef = iComment.FileRef,
					CreatedAt = iComment.CreatedAt == null ? null : DateParser.Parse((DateTime)iComment.CreatedAt),
					User = iComment.UserId == null ? null : await _authClient.GetUserById((int)iComment.UserId),
				});
			}

			return new TaskDto
			{
				Id = task.Id,
				Title = task.Title,
				ApprovedAt = task.ApprovedAt == null ? null : DateParser.Parse((DateTime)task.ApprovedAt),
				ExpectedTime = task.ExpectedTime,
				User = userDto,
				Comments = comments,
				SignId = task.SignId,
				StartedAt = task.StartedAt == null ? null : DateParser.Parse((DateTime)task.StartedAt),
				Signed = task.Signed,
				EndVerificationDate = task.EndVerificationDate == null ? null : DateParser.Parse((DateTime)task.EndVerificationDate),
			};
		}

		public async Task<TaskDto> StartTask(int UserId, int Id)
		{
			var logUser = await _authClient.GetUserById(UserId);

			var taskDto = await GetTaskById(Id);

			if (taskDto == null)
			{
				return null;
			}

			var taskModel = await _context.Tasks
					.Where(t => t.Id == Id)
					.FirstOrDefaultAsync();

			taskModel.SignId = UserId;
			taskModel.StartedAt = DateTime.Now.AddHours(3);

			await _context.SaveChangesAsync();

			var stage = await _context.Stages			//NOTE:notification
				.Include(s => s.Status)
				.Where(s => s.Id == taskModel.StageId && 
                       s.Status.Title.ToLower() != "не начат" &&
                       s.Status.Title.ToLower() != "остановлен")
				.FirstOrDefaultAsync();

			var processForNotification = await _context.Processes
				.Include(p => p.Stages)
				.Where(p => p.Id == stage.ProcessId)
				.FirstOrDefaultAsync();

			if (processForNotification.Stages != null)
			{
				foreach (var iStage in processForNotification.Stages)
				{
					var groupHolds = await _authClient.FindHold(iStage.Id, "Stage");
               
					foreach (var hold in groupHolds)
					{
						if (hold.Groups == null) continue;
						foreach (var group in hold.Groups)
						{
							var users = await _authClient.GetUsersByGroupId(group.Id);
							foreach (var iUser in users)
							{
								_notificationService.SendNotification(processForNotification.Id, iStage.Id, iUser.Id, "StartTask");
							}
						}
					}
				}
			}

			if (logUser != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Task",
                    Field = "SignId, StartedAt",
                    Operation = "Update",
                    LogId = Id.ToString(),
                    Old = "null, null",
                    New = $"{taskModel.SignId}, {taskModel.StartedAt}",
                    Author = logUser.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

			return await GetTaskById(Id);
		}

		public async Task<TaskDto> StopTask(int UserId, int Id)
		{
			var logUser = await _authClient.GetUserById(UserId);

			var taskDto = await GetTaskById(Id);

			if (taskDto == null)
			{
				return null;
			}

			var taskModel = await _context.Tasks
				.Where(t => t.Id == Id)
				.FirstOrDefaultAsync();

			if (logUser != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Task",
                    Field = "Signed, SignId, ApprovedAt, EndVerificationDate, StartedAt",
                    Operation = "Update",
                    LogId = Id.ToString(),
                    Old = $"{taskModel.SignId}, " +
						  $"{taskModel.SignId}, " +
						  $"{taskModel.ApprovedAt}, " +
						  $"{taskModel.EndVerificationDate}, " +
						  $"{taskModel.StartedAt}",
                    New = "null, null, null, null, null",
                    Author = logUser.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

			taskModel.SignId = null;
			taskModel.Signed = null;
			taskModel.ApprovedAt = null;
			taskModel.EndVerificationDate = null;
			taskModel.StartedAt = null;

			await _context.SaveChangesAsync();

			var stage = await _context.Stages		//NOTE:notification
				.Include(s => s.Status)
				.Where(s => s.Id == taskModel.StageId && 
                       s.Status.Title.ToLower() != "не начат" &&
                       s.Status.Title.ToLower() != "остановлен")
				.FirstOrDefaultAsync();

			var status = await _context.Statuses
				.Where(s => s.Id == stage.StatusId)
				.FirstOrDefaultAsync();
	
			if (status.Title.ToLower() == "принят на проверку")
			{
				var newStatus = await _context.Statuses
					.Where(s => s.Title.ToLower() == "отправлен на проверку")
					.FirstOrDefaultAsync();
				stage.Status = newStatus;
			}

			await _context.SaveChangesAsync();

			var processForNotification = await _context.Processes
				.Include(p => p.Stages)
				.Where(p => p.Id == stage.ProcessId)
				.FirstOrDefaultAsync();

			if (processForNotification.Stages != null)
			{
				foreach (var iStage in processForNotification.Stages)
				{
					var groupHolds = await _authClient.FindHold(iStage.Id, "Stage");
               
					foreach (var hold in groupHolds)
					{
						if (hold.Groups == null) continue;
						foreach (var group in hold.Groups)
						{
							var users = await _authClient.GetUsersByGroupId(group.Id);
							foreach (var iUser in users)
							{
								_notificationService.SendNotification(processForNotification.Id, iStage.Id, iUser.Id, "StopTask");
							}
						}
					}
				}
			}

			return await GetTaskById(Id);
		}

		public async Task<TaskDto> UpdateEndVerification(int UserId, int Id)
		{
			var logUser = await _authClient.GetUserById(UserId);

			var taskDto = await GetTaskById(Id);

			if (taskDto == null)
			{
				return null;
			}

			var taskModel = await _context.Tasks
				.Where(t => t.Id == Id)
				.FirstOrDefaultAsync();

			var oldEndVerificationDate = taskModel.EndVerificationDate;

			taskModel.EndVerificationDate = DateTime.Now.AddHours(3);

			await _context.SaveChangesAsync();

			if (logUser != null)
            {
                await _logService.AddLog(new Log
                {
                    Table = "Task",
                    Field = "EndVerificationDate",
                    Operation = "Update",
                    LogId = Id.ToString(),
                    Old = $"{oldEndVerificationDate}",
                    New = $"{taskModel.EndVerificationDate}",
                    Author = logUser.ShortName.ToString(),
                    CreatedAt = DateTime.Now.AddHours(3)
                });
                await _context.SaveChangesAsync();
            }

			var stage = await _context.Stages		//NOTE:notification
				.Include(s => s.Status)
				.Where(s => s.Id == taskModel.StageId && 
                       s.Status.Title.ToLower() != "не начат" &&
                       s.Status.Title.ToLower() != "остановлен")
				.FirstOrDefaultAsync();

			var processForNotification = await _context.Processes
				.Include(p => p.Stages)
				.Where(p => p.Id == stage.ProcessId)
				.FirstOrDefaultAsync();

			if (processForNotification.Stages != null)
			{
				foreach (var iStage in processForNotification.Stages)
				{
					var groupHolds = await _authClient.FindHold(iStage.Id, "Stage");
               
					foreach (var hold in groupHolds)
					{
						if (hold.Groups == null) continue;
						foreach (var group in hold.Groups)
						{
							var users = await _authClient.GetUsersByGroupId(group.Id);
							foreach (var iUser in users)
							{
								_notificationService.SendNotification(processForNotification.Id, iStage.Id, iUser.Id, "UpdateEndVerification");
							}
						}
					}
				}
			}

			return await GetTaskById(Id);
		}
	}
}
