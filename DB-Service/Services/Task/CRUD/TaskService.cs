using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Exceptions;
using DB_Service.Services.Comment.CRUD;
using DB_Service.Tools;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services.Task.CRUD
{
    public class TaskService : ITaskService
    {
        private readonly DataContext _context;
        private readonly ICommentService _commentService;

        public TaskService(
            DataContext context,
            ICommentService commentService
            )
        {
            _context = context;
            _commentService = commentService;
        }

        public async Task<int> Create(
            int? stageId, 
            string title, 
            TimeSpan expectedTime
            )
        {
            var newTask = new Models.Task
            {
                StageId = stageId.GetValueOrDefault(),
                Title = title,
                ExpectedTime = expectedTime
            };

            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();

            return newTask.Id;
        }

        public async Task<int> Update(
            int taskId, 
            string? title, 
            DateTime? endVerificationDate, 
            DateTime? startedAt, 
            DateTime? approvedAt, 
            TimeSpan? expectedTime, 
            string? signed, 
            int? signId)
        {
            try
            {
                var task = await Exist(taskId);
                
                task.Title = title ?? task.Title;
                task.EndVerificationDate = endVerificationDate ?? task.EndVerificationDate;
                task.StartedAt = startedAt ?? task.StartedAt;
                task.ApprovedAt = approvedAt ?? task.ApprovedAt;
                task.ExpectedTime = expectedTime ?? task.ExpectedTime;
                task.Signed = signed ?? task.Signed;
                task.SignId = signId ?? task.SignId;

                await _context.SaveChangesAsync();

                return task.Id;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(int taskId)
        {
            try
            {
                var task = await Exist(taskId);

                var stageId = task.StageId
                    .GetValueOrDefault();

                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();

                return stageId;
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<Models.Task> Exist(int taskId)
        {
            var task = await _context.Tasks
                .Include(t => t.Stage)
                .Include(t => t.Comments)
                .Where(t => t.Id == taskId)
                .FirstOrDefaultAsync() ??
                throw new NotFoundException($"Task with id = {taskId} not found");

            return task;
        }

        public async Task<TaskDto> Get(int taskId)
        {
            try
            {
                var task = await Exist(taskId);

                var commentIds = await Comments(taskId);
                var commentsNotAwaited = commentIds
                    .Select(async id => await _commentService.Get(id))
                    .ToList();
                var comments = await System.Threading.Tasks.Task.WhenAll(commentsNotAwaited);

                return new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    EndVerificationDate = DateParser.TryParse(task.EndVerificationDate),
                    ApprovedAt = DateParser.TryParse(task.ApprovedAt),
                    StartedAt = DateParser.TryParse(task.StartedAt),
                    ExpectedTime = task.ExpectedTime,
                    Signed = task.Signed,
                    SignId = task.SignId,
                    StageId = task.StageId.GetValueOrDefault(),
                    Comments = comments.ToList(),
                };
            }
            catch (NotFoundException ex)
            {
                throw ex;
            }
        }

        public async Task<List<int>> Comments(int taskId)
        {
            return await _context.Comments
                .Where(c => c.TaskId.GetValueOrDefault() == taskId)
                .Select(c => c.Id)
                .ToListAsync();
        }
    }
}
