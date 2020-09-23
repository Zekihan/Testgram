using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IRepositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int id);
        Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(int id);
        Task<IEnumerable<Comment>> GetCommentsByParentIdAsync(int id);
    }
}
