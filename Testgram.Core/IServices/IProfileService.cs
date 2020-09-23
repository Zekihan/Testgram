using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IServices
{
    public interface IProfileService
    {
        Task<IEnumerable<Profile>> GetAllProfiles();
        Task<Profile> GetProfileById(int id);
        Task<Profile> GetProfileByUsername(string username);
        Task<Profile> GetProfileByEmail(string email);
        Task<Profile> CreateProfile(Profile profile);
        Task UpdateProfile(Profile profileToBeUpdated, Profile profile);
        Task DeleteProfile(Profile profile);
    }
}
