using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IServices
{
    public interface IFollowService
    {
        Task<IEnumerable<Follow>> GetAllFollows();
        Task<IEnumerable<Follow>> GetFollowByUserId(long userId);
        Task<IEnumerable<Follow>> GetFollowByFollowerId(long followerId);
        Task<Follow> CreateFollow(Follow follow);
        Task DeleteFollow(Follow follow);
    }
}
