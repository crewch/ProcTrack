using DB_Service.Data;
using DB_Service.Dtos;
using DB_Service.Exceptions;
using DB_Service.Tools;
using Microsoft.EntityFrameworkCore;

namespace DB_Service.Services.Comment.CRUD
{
    public class CommentService : ICommentService
    {
        private readonly DataContext _context;

        public CommentService(DataContext context)
        {
            _context = context;
        }

        public async Task<int> Create(int taskId, int userId, string text, string? fileref)
        {
            var newComment = new Models.Comment
            {
                TaskId = taskId,
                UserId = userId,
                Text = text,
                FileRef = fileref,
                CreatedAt = DateTime.Now.AddHours(3),
            };

            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return newComment.Id;
        }

        public async Task<int> Update(int commentId, string? text, string? fileref)
        {
            try
            {
                var comment = await Exist(commentId);

                comment.Text = text ?? comment.Text;
                comment.FileRef = fileref ?? comment.FileRef;

                await _context.SaveChangesAsync();

                return comment.Id;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }            
        }

        public async Task<int> Delete(int commentId) //TODO:добавить удаление из s3
        {
            try
            {
                var comment = await Exist(commentId);

                int taskId = comment.TaskId ?? 
                    throw new NotFoundException($"Task not found");;

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

                return taskId;
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }

        public async Task<Models.Comment> Exist(int commentId)
        {
            var comment = await _context.Comments
                .Include(c => c.Task)
                .Where(c => c.Id == commentId)
                .FirstOrDefaultAsync() ?? 
                throw new NotFoundException($"Comment with id = {commentId} not found");
            
            return comment;
        }

        public async Task<CommentDto> Get(int commentId)
        {
            try
            {
                var comment = await Exist(commentId);

                return new CommentDto
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    FileRef = comment.FileRef,
                    CreatedAt = DateParser.Parse(comment.CreatedAt)
                };
            }
            catch (NotFoundException ex) 
            {
                throw ex;
            }
        }
    }
}
