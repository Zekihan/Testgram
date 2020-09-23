using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IRepositories
{
    public interface ILikeRepository : IRepository<Likes>
    {
        Task<IEnumerable<Likes>> GetAllLikesAsync();
        Task<IEnumerable<Likes>> GetLikesByPostIdAsync(int id);
        Task<IEnumerable<Likes>> GetLikesByUserIdAsync(int id);
    }
}
