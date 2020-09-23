using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IServices
{
    public interface IFollowService
    {
        Task<IEnumerable<Follow>> GetAllFollows();
        Task<IEnumerable<Follow>> GetFollowByUserId(int userId);
        Task<IEnumerable<Follow>> GetFollowByFollowerId(int followerId);
        Task<Follow> CreateFollow(Follow follow);
        Task DeleteFollow(Follow follow);
    }
}
