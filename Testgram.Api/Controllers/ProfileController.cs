using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Api.ApiModels;
using Testgram.Core.IServices;
using Testgram.Core.Models;

namespace Testgram.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly AutoMapper.IMapper _mapper;
        public ProfileController(IProfileService profileService, AutoMapper.IMapper mapper)
        {
            this._mapper = mapper;
            this._profileService = profileService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ProfileModel>>> GetAllProfiles()
        {
            var profiles = await _profileService.GetAllProfiles();
            var profilesModel = _mapper.Map<IEnumerable<Profile>, IEnumerable<ProfileModel>>(profiles);
            return Ok(profilesModel);
        }

        [HttpGet("username")]
        public async Task<ActionResult<ProfileModel>> GetProfileByUsername(string username)
        {
            var profile = await _profileService.GetProfileByUsername(username);
            if (profile == null)
            {
                return NotFound();
            }
            var profileModel = _mapper.Map<Profile, ProfileModel>(profile);
            return Ok(profileModel);
        }

        [HttpPut("id")]
        public async Task<ActionResult<ProfileModel>> UpdateProfile(int id, ProfileModel newProfile)
        {
            var profileToUpdate = await _profileService.GetProfileById(id);

            if (profileToUpdate == null)
                return NotFound();

            var profile = _mapper.Map<ProfileModel, Profile>(newProfile);

            await _profileService.UpdateProfile(profileToUpdate, profile);

            profile = await _profileService.GetProfileById(id);
            newProfile = _mapper.Map<Profile, ProfileModel>(profile);

            return Ok(newProfile);
        }

        [HttpPost]
        public async Task<ActionResult<ProfileModel>> CreateProfile(ProfileModel newProfile)
        {
            try
            {
                var profile = _mapper.Map<ProfileModel, Profile>(newProfile);
                Console.WriteLine(profile);
                var profileModel = await _profileService.CreateProfile(profile);

                newProfile = _mapper.Map<Profile, ProfileModel>(profileModel);
                return Ok(newProfile);
            }
            catch (DbUpdateException e)
            {
                //This either returns a error string, or null if it can’t handle that error
                if (e != null)
                {
                    if (e.InnerException.Message.Contains("UQ__Profile__AB6E61642272DBD6"))
                    {
                        return BadRequest("Email is already used");
                    }
                    else if (e.InnerException.Message.Contains("UQ__Profile__F3DBC57207E43CE6"))
                    {
                        return BadRequest("Username is already used");
                    }
                    else return BadRequest("Error: Unhandled Error\nMessage: " + e.Message + "\nInner message: " + e.InnerException.Message);
                }
                return BadRequest("Unknown Error"); //couldn’t handle that error
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult<ProfileModel>> DeleteProfile(int id)
        {
            try
            {
                var profileToBeDeleted = await _profileService.GetProfileById(id);

                if (profileToBeDeleted == null)
                    return NotFound();

                var profileModel = _mapper.Map<Profile, ProfileModel>(profileToBeDeleted);

                await _profileService.DeleteProfile(profileToBeDeleted);
                return Ok(profileModel);
            }
            catch (DbUpdateException e)
            {
                //This either returns a error string, or null if it can’t handle that error
                if (e != null)
                {
                    return BadRequest("Error: Unhandled Error\nMessage: " + e.Message + "\nInner message: " + e.InnerException.Message);
                }
                return BadRequest("Unknown Error"); //couldn’t handle that error
            }
        }

    }
}
