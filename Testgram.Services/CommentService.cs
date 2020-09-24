using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core;
using Testgram.Core.IServices;
using Testgram.Core.Models;

namespace Testgram.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Comment> CreateComment(Comment comment)
        {
            await _unitOfWork.Comment.AddAsync(comment);
            await _unitOfWork.CommitAsync();
            return comment;
        }

        public async Task DeleteComment(Comment comment)
        {
            _unitOfWork.Comment.Remove(comment);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _unitOfWork.Comment.GetAllCommentsAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsByParentComment(long commentId)
        {
            return await _unitOfWork.Comment.GetCommentsByParentIdAsync(commentId);
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsByPostId(long postId)
        {
            return await _unitOfWork.Comment.GetCommentsByPostIdAsync(postId);
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsByUserId(long userId)
        {
            return await _unitOfWork.Comment.GetCommentsByUserIdAsync(userId);
        }

        public async Task<Comment> GetCommentById(long commentId)
        {
            return await _unitOfWork.Comment.GetByIdAsync(commentId);
        }

        public async Task UpdateComment(Comment commentToBeUpdated, Comment comment)
        {
            commentToBeUpdated.Content = comment.Content;
            await _unitOfWork.CommitAsync();
        }
    }
}