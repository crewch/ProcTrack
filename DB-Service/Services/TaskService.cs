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
        private readonly IFileDataClient _fileClient;

        public TaskService(DataContext context, IAuthDataClient authClient, IFileDataClient fileClient)
        {
            _context = context;
            _authClient = authClient;
            _fileClient = fileClient;
        }

        public async Task<TaskDto> AssignTask(int UserId, int Id)
        {
            var taskDto = await GetTaskById(Id);
            
            if (taskDto == null)
            {
                return null;
            }

            var taskModel = _context.Tasks
                .Include(t => t.Stage)
                .ThenInclude(s => s.Status)
                .Where(t => t.Id == Id)
                .FirstOrDefault();

            var user = _authClient.GetUserById(UserId).Result;

            taskModel.ApprovedAt = DateTime.Now;
            taskModel.Signed = user.LongName;

            if (taskModel.StageId != null)
            {
                var stage = _context.Stages
                .Where(s => s.Id == taskModel.StageId)
                .FirstOrDefault();
            
                var status = _context.Statuses
                    .Where(s => s.Id == stage.StatusId)
                    .FirstOrDefault();

                if (status.Title.ToLower() == "отправлен на проверку")
                {
                    var newStatus = _context.Statuses
                        .Where(s => s.Title.ToLower() == "принят на проверку")
                        .FirstOrDefault();

                    stage.Status = newStatus;
                }
            }

            _context.SaveChanges();
            
            taskDto = await GetTaskById(Id);
            
            return taskDto;
        }

        public Task<CommentDto> CreateCommment(int Id, CommentDto data)
        {
            var taskDto = GetTaskById(Id).Result;

            if (taskDto == null)
            {
                return null;
            }

            var taskModel = _context.Tasks
                .Where(t => t.Id == taskDto.Id)
                .FirstOrDefault();

            var commentModel = new Comment
            {
                UserId = data.User.Id,
                Text = data.Text,
                CreatedAt = DateTime.Now,
            };

            _context.Comments.Add(commentModel);
            taskModel.Comments.Add(commentModel);
            _context.SaveChanges();

            var commentDto = new CommentDto
            {
                Id = commentModel.Id,
                Text = commentModel.Text,
                CreatedAt = DateTime.Now,
                User = data.User,
            };
            return System.Threading.Tasks.Task.FromResult(commentDto);
        }

        public async Task<List<CommentDto>> GetComentsByTaskId(int Id)
        {
            var comments = _context.Comments
                .Where(c => c.TaskId == Id)
                .ToList();

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
                    CreatedAt = comment.CreatedAt,
                    Text = comment.Text,
                    User = userDto,
                });
            }
            return dtos;
        }

        public async Task<TaskDto> GetTaskById(int Id)
        {
            var task = _context.Tasks
                .Include(t => t.Comments)
                .Where(t => t.Id == Id)
                .FirstOrDefault();
            
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
                    CreatedAt = iComment.CreatedAt,
                    User = iComment.UserId == null ? null : await _authClient.GetUserById((int)iComment.UserId),
                });
            }

            var res = new TaskDto
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
            return res;
        }

        public async Task<TaskDto> StartTask(int UserId, int Id)
        {
            var taskDto = await GetTaskById(Id);
            
            if (taskDto == null)
            {
                return null;
            }

            var taskModel = _context.Tasks
                .Where(t => t.Id == Id)
                .FirstOrDefault();

            taskModel.SignId = UserId;
            taskModel.StartedAt = DateTime.Now;

            _context.SaveChanges();

            taskDto = await GetTaskById(Id);

            return taskDto;
        }

        public async Task<TaskDto> StopTask(int UserId, int Id)
        {
            var taskDto = await GetTaskById(Id);
            
            if (taskDto == null)
            {
                return null;
            }

            var taskModel = _context.Tasks
                .Where(t => t.Id == Id)
                .FirstOrDefault();

            taskModel.SignId = null;
            taskModel.Signed = null;
            taskModel.ApprovedAt = null;
            taskModel.EndVerificationDate = null;
            taskModel.StartedAt = null;

            _context.SaveChanges();

            taskDto = await GetTaskById(Id);
            return taskDto;
        }

        public async Task<TaskDto> UpdateEndVerification(int UserId, int Id)
        {
            var taskDto = await GetTaskById(Id);
            
            if (taskDto == null)
            {
                return null;
            }

            var taskModel = _context.Tasks
                .Where(t => t.Id == Id)
                .FirstOrDefault();

            taskModel.EndVerificationDate = DateTime.Now;

            _context.SaveChanges();

            taskDto = await GetTaskById(Id);
            return taskDto;
        }
    }
}
