using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core;
using Testgram.Core.IServices;
using Testgram.Core.Models;

namespace Testgram.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LikeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Likes> CreateLike(Likes like)
        {
            await _unitOfWork.Like.AddAsync(like);
            await _unitOfWork.CommitAsync();
            return like;
        }

        public async Task DeleteLike(Likes like)
        {
            _unitOfWork.Like.Remove(like);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Likes>> GetAllLikes()
        {
            return await _unitOfWork.Like.GetAllLikesAsync();
        }

        public async Task<Likes> GetLikeById(long postId, long userId)
        {
            return await _unitOfWork.Like.GetLikeByIdAsync(postId, userId);
        }

        public async Task<IEnumerable<Likes>> GetLikesByPostId(long postId)
        {
            return await _unitOfWork.Like.GetLikesByPostIdAsync(postId);
        }

        public async Task<IEnumerable<Likes>> GetLikesByUserId(long userId)
        {
            return await _unitOfWork.Like.GetLikesByUserIdAsync(userId);
        }
    }
}
