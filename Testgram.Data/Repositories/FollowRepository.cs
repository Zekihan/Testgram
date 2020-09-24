using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testgram.Core.IRepositories;
using Testgram.Core.Models;
using Testgram.Data.Configurations;

namespace Testgram.Data.Repositories
{
    class FollowRepository : BaseRepository<Follow>, IFollowRepository
    {
        public FollowRepository(SocialContext context)
            : base(context)
        { }

        private SocialContext SocialContext
        {
            get { return Context as SocialContext; }
        }

        public async Task<IEnumerable<Follow>> GetAllFollowsAsync()
        {
            return await SocialContext.Follow
                    .ToListAsync();
        }

        public async Task<IEnumerable<Follow>> GetFollowsByFollowerIdAsync(long id)
        {
            return await SocialContext.Follow
                    .Where(a => a.FollowerId == id)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Follow>> GetFollowsByUserIdAsync(long id)
        {
            return await SocialContext.Follow
                    .Where(a => a.UserId == id)
                    .ToListAsync();
        }
    }
}
