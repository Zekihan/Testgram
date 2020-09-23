using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IRepositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<IEnumerable<Profile>> GetAllProfilesAsync();
        Task<Profile> GetProfileByIdAsync(int id);
        Task<Profile> GetProfileByUsernameAsync(string username);
        Task<Profile> GetProfileByEmailAsync(string email);
    }
}
