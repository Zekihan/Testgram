using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Api.ApiModels;
using Testgram.Api.InputModels;
using Testgram.Core.Exceptions;
using Testgram.Core.IServices;
using Testgram.Core.Models;

namespace Testgram.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;
        private readonly IProfileService _profileService;
        private readonly AutoMapper.IMapper _mapper;

        public FollowController(IFollowService followService, AutoMapper.IMapper mapper, IProfileService profileService)
        {
            this._mapper = mapper;
            this._followService = followService;
            this._profileService = profileService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<FollowModel>>> GetAllFollows()
        {
            var follows = await _followService.GetAllFollows();
            var followsModel = _mapper.Map<IEnumerable<Follow>, IEnumerable<FollowModel>>(follows);
            foreach (FollowModel follow in followsModel)
            {
                var profile1 = await _profileService.GetProfileById(follow.UserId);
                var profile2 = await _profileService.GetProfileById(follow.FollowerId);
                follow.Username = profile1.Username;
                follow.FollowerUsername = profile2.Username;
            }
            return Ok(followsModel);
        }

        [HttpGet("follower/{username}")]
        public async Task<ActionResult<IEnumerable<FollowModel>>> GetAllFollowerByUsername(string username)
        {
            var profile = await _profileService.GetProfileByUsername(username);
            if (profile == null)
            {
                return NotFound();
            }
            var follows = await _followService.GetFollowByUserId(profile.UserId);
            var followsModel = _mapper.Map<IEnumerable<Follow>, IEnumerable<FollowModel>>(follows);
            foreach (FollowModel follow in followsModel)
            {
                var profile1 = await _profileService.GetProfileById(follow.UserId);
                var profile2 = await _profileService.GetProfileById(follow.FollowerId);
                follow.Username = profile1.Username;
                follow.FollowerUsername = profile2.Username;
            }
            return Ok(followsModel);
        }

        [HttpGet("followed/{username}")]
        public async Task<ActionResult<IEnumerable<FollowModel>>> GetAllFollowedByUsername(string username)
        {
            var profile = await _profileService.GetProfileByUsername(username);
            if (profile == null)
            {
                return NotFound();
            }
            var follows = await _followService.GetFollowByFollowerId(profile.UserId);
            var followsModel = _mapper.Map<IEnumerable<Follow>, IEnumerable<FollowModel>>(follows);
            foreach (FollowModel follow in followsModel)
            {
                var profile1 = await _profileService.GetProfileById(follow.UserId);
                var profile2 = await _profileService.GetProfileById(follow.FollowerId);
                follow.Username = profile1.Username;
                follow.FollowerUsername = profile2.Username;
            }
            return Ok(followsModel);
        }

        [HttpPost]
        public async Task<ActionResult<FollowModel>> CreateFollow(FollowInputModel newFollow)
        {
            try
            {
                var follow = _mapper.Map<FollowInputModel, Follow>(newFollow);
                follow.FollowDate = DateTime.UtcNow;
                var followModel = await _followService.CreateFollow(follow);

                var followOutput = _mapper.Map<Follow, FollowModel>(followModel);

                var profile = await _profileService.GetProfileById(newFollow.UserId);
                followOutput.Username = profile.Username;

                profile = await _profileService.GetProfileById(newFollow.FollowerId);
                followOutput.FollowerUsername = profile.Username;

                return Ok(followOutput);
            }
            catch (DBException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Internal error.");
            }
        }

        [HttpDelete("{userId}/{followerId}")]
        public async Task<ActionResult<FollowModel>> DeleteFollow(long userId, long followerId)
        {
            try
            {
                var followToBeDeleted = await _followService.GetFollowById(userId, followerId);

                if (followToBeDeleted == null)
                    return NotFound();

                var postModel = _mapper.Map<Follow, FollowModel>(followToBeDeleted);

                var profile = await _profileService.GetProfileById(postModel.UserId);
                postModel.Username = profile.Username;

                profile = await _profileService.GetProfileById(postModel.FollowerId);
                postModel.FollowerUsername = profile.Username;

                await _followService.DeleteFollow(followToBeDeleted);
                return Ok(postModel);
            }
            catch (DBException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Internal error.");
            }
        }
    }
}