using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IServices
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPostById(long id);
        Task<IEnumerable<Post>> GetAllPostsAfterDate(DateTime dateTime);
        Task<IEnumerable<Post>> GetPostsByUserId(long userId);
        Task<Post> CreatePost(Post post);
        Task UpdatePost(Post postToBeUpdated, Post post);
        Task DeletePost(Post post);
    }
}
