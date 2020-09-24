using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IRepositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(long id);
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(long userId);
        Task<IEnumerable<Post>> GetPostsAfterDateAsync(DateTime dateTime);
    }
}
