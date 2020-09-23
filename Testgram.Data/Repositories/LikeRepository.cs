using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testgram.Core.IRepositories;
using Testgram.Core.Models;
using Testgram.Data.Configurations;

namespace Testgram.Data.Repositories
{
    class LikeRepository : BaseRepository<Likes>, ILikeRepository
    {
        public LikeRepository(SocialContext context)
            : base(context)
        { }

        private SocialContext SocialContext
        {
            get { return Context as SocialContext; }
        }

        public async Task<IEnumerable<Likes>> GetAllLikesAsync()
        {
            return await SocialContext.Likes
                    .Include(a => a.UserId)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Likes>> GetLikesByPostIdAsync(int id)
        {
            return await SocialContext.Likes
                    .Where(a => a.PostId == id)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Likes>> GetLikesByUserIdAsync(int id)
        {
            return await SocialContext.Likes
                    .Where(a => a.UserId == id)
                    .ToListAsync();
        }
    }
}
