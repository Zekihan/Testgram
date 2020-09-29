using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core;
using Testgram.Core.Exceptions;
using Testgram.Core.IServices;
using Testgram.Core.Models;

namespace Testgram.Services
{
    public class FollowService : IFollowService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Follow> CreateFollow(Follow follow)
        {
            try
            {
                await _unitOfWork.Follow.AddAsync(follow);
                await _unitOfWork.CommitAsync();
                return follow;
            }
            catch (DBException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Internal error.");
            }
        }

        public async Task DeleteFollow(Follow follow)
        {
            try
            {
                _unitOfWork.Follow.Remove(follow);
                await _unitOfWork.CommitAsync();
            }
            catch (DBException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Internal error.");
            }
        }

        public async Task<IEnumerable<Follow>> GetAllFollows()
        {
            return await _unitOfWork.Follow.GetAllFollowsAsync();
        }

        public async Task<IEnumerable<Follow>> GetFollowByFollowerId(long followerId)
        {
            return await _unitOfWork.Follow.GetFollowsByFollowerIdAsync(followerId);
        }

        public async Task<Follow> GetFollowById(long userId, long followerId)
        {
            return await _unitOfWork.Follow.GetFollowsByIdAsync(userId, followerId);
        }

        public async Task<IEnumerable<Follow>> GetFollowByUserId(long userId)
        {
            return await _unitOfWork.Follow.GetFollowsByUserIdAsync(userId);
        }
    }
}