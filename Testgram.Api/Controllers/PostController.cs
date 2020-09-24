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
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IProfileService _profileService;
        private readonly AutoMapper.IMapper _mapper;
        public PostController(IPostService postService, AutoMapper.IMapper mapper, IProfileService profileService)
        {
            this._mapper = mapper;
            this._postService = postService;
            this._profileService = profileService;
        }
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts();
            var postsModel = _mapper.Map<IEnumerable<Post>, IEnumerable<PostModel>>(posts);
            foreach (PostModel post in postsModel)
            {
                var profile = await _profileService.GetProfileById(post.UserId);
                post.Username = profile.Username;
            }
            return Ok(postsModel);
        }
        [HttpGet("username")]
        public async Task<ActionResult<PostModel>> GetPostsByUsername(string username)
        {
            var profile = await _profileService.GetProfileByUsername(username);
            if (profile == null)
            {
                return NotFound("Username doesn't exists.");
            }
            var posts = await _postService.GetPostsByUserId(profile.UserId);
            var postsModel = _mapper.Map<IEnumerable<Post>, IEnumerable<PostModel>>(posts);
            foreach (PostModel post in postsModel)
            {
                profile = await _profileService.GetProfileById(post.UserId);
                post.Username = profile.Username;
            }
            return Ok(postsModel);
        }

        [HttpGet("dateTime")]
        public async Task<ActionResult<PostModel>> GetPostsAfterDate(DateTime dateTime)
        {
            var posts = await _postService.GetAllPostsAfterDate(dateTime);
            var postsModel = _mapper.Map<IEnumerable<Post>, IEnumerable<PostModel>>(posts);
            foreach (PostModel post in postsModel)
            {
                var profile = await _profileService.GetProfileById(post.UserId);
                post.Username = profile.Username;
            }
            return Ok(postsModel);
        }

        [HttpPost]
        public async Task<ActionResult<PostModel>> CreatePost(PostModel newPost)
        {
            try
            {
                var post = _mapper.Map<PostModel, Post>(newPost);
                var postModel = await _postService.CreatePost(post);

                newPost = _mapper.Map<Post, PostModel>(postModel);
                return Ok(newPost);
            }
            catch (DbUpdateException e)
            {
                //This either returns a error string, or null if it can’t handle that error
                if (e != null)
                {
                    Console.WriteLine(e.StackTrace);
                    return BadRequest("Error: Unhandled Error\nMessage: " + e.Message + "\nInner message: " + e.InnerException.Message);
                }
                return BadRequest("Unknown Error"); //couldn’t handle that error
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult<PostModel>> UpdatePost(int id, PostModel newPost)
        {
            try
            {
                var postToUpdate = await _postService.GetPostById(id);

                if (postToUpdate == null)
                    return NotFound();

                var post = _mapper.Map<PostModel, Post>(newPost);

                await _postService.UpdatePost(postToUpdate, post);

                post = await _postService.GetPostById(id);
                newPost = _mapper.Map<Post, PostModel>(post);

                return Ok(newPost);
            }
            catch (DbUpdateException e)
            {
                //This either returns a error string, or null if it can’t handle that error
                if (e != null)
                {
                    Console.WriteLine(e.StackTrace);
                    return BadRequest("Error: Unhandled Error\nMessage: " + e.Message + "\nInner message: " + e.InnerException.Message);
                }
                return BadRequest("Unknown Error"); //couldn’t handle that error
            }
        }
        [HttpDelete("id")]
        public async Task<ActionResult<ProfileModel>> DeletePost(int id)
        {
            try
            {
                var postToBeDeleted = await _postService.GetPostById(id);

                if (postToBeDeleted == null)
                    return NotFound();

                var postModel = _mapper.Map<Post, PostModel>(postToBeDeleted);

                await _postService.DeletePost(postToBeDeleted);
                return Ok(postModel);
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
