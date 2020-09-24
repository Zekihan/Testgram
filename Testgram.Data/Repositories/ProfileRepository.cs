using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.IRepositories;
using Testgram.Core.Models;
using Testgram.Data.Configurations;

namespace Testgram.Data.Repositories
{
    internal class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(SocialContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
        {
            return await SocialContext.Profile
                .ToListAsync();
        }

        public async Task<Profile> GetProfileByEmailAsync(string email)
        {
            return await SocialContext.Profile
                 .SingleOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Profile> GetProfileByIdAsync(long id)
        {
            return await SocialContext.Profile
                 .SingleOrDefaultAsync(a => a.UserId == id);
        }

        public async Task<Profile> GetProfileByUsernameAsync(string username)
        {
            return await SocialContext.Profile
                .SingleOrDefaultAsync(a => a.Username == username);
        }

        private SocialContext SocialContext
        {
            get { return Context as SocialContext; }
        }
    }
}