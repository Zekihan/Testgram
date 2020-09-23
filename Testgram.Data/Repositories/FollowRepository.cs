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
                    .Include(a => a.UserId)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Follow>> GetFollowsByFollowerIdAsync(int id)
        {
            return await SocialContext.Follow
                    .Where(a => a.FollowerId == id)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Follow>> GetFollowsByUserIdAsync(int id)
        {
            return await SocialContext.Follow
                    .Where(a => a.UserId == id)
                    .ToListAsync();
        }
    }
}
