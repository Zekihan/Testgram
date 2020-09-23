using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testgram.Core.IRepositories;
using Testgram.Core.Models;
using Testgram.Data.Configurations;

namespace Testgram.Data.Repositories
{
    class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(SocialContext context)
            : base(context)
        { }

        private SocialContext SocialContext
        {
            get { return Context as SocialContext; }
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await SocialContext.Post
                    .Include(a => a.PostId)
                    .ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await SocialContext.Post
                 .SingleOrDefaultAsync(a => a.PostId == id);
        }

        public async Task<IEnumerable<Post>> GetPostsAfterDateAsync(DateTime dateTime)
        {
            return await SocialContext.Post
                    .Where(a => a.PostDate > dateTime)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId)
        {
            return await SocialContext.Post
                    .Where(a => a.UserId == userId)
                    .ToListAsync();
        }
    }
}
