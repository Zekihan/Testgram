using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IRepositories
{
    public interface IFollowRepository : IRepository<Follow>
    {
        Task<IEnumerable<Follow>> GetAllFollowsAsync();

        Task<IEnumerable<Follow>> GetFollowsByFollowerIdAsync(long id);

        Task<IEnumerable<Follow>> GetFollowsByUserIdAsync(long id);

        Task<Follow> GetFollowsByIdAsync(long userId, long followerId);
    }
}