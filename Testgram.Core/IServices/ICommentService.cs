using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IServices
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllComments();
        Task<IEnumerable<Comment>> GetAllCommentsByPostId(int postId);
        Task<IEnumerable<Comment>> GetAllCommentsByUserId(int userId);
        Task<IEnumerable<Comment>> GetAllCommentsByParentComment(int commentId);
        Task<Comment> CreateComment(Comment comment);
        Task UpdateComment(Comment commentToBeUpdated, Comment comment);
        Task DeleteComment(Comment comment);
    }
}
