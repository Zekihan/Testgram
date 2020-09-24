using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IServices
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAllComments();

        Task<Comment> GetCommentById(long commentId);

        Task<IEnumerable<Comment>> GetAllCommentsByPostId(long postId);

        Task<IEnumerable<Comment>> GetAllCommentsByUserId(long userId);

        Task<IEnumerable<Comment>> GetAllCommentsByParentComment(long commentId);

        Task<Comment> CreateComment(Comment comment);

        Task UpdateComment(Comment commentToBeUpdated, Comment comment);

        Task DeleteComment(Comment comment);
    }
}