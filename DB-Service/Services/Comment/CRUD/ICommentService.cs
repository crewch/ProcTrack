using DB_Service.Dtos;
using DB_Service.Models;

namespace DB_Service.Services.Comment.CRUD
{
    public interface ICommentService
    {
        Task<int> Create(int taskId, int userId, string text, string? fileref);

        Task<CommentDto> Get(int commentId);

        Task<Models.Comment> Exist(int commentId);

        Task<int> Update(int commentId, string? text, string? fileref);

        Task<int> Delete(int commentId);
    }
}
