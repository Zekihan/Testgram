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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IProfileService _profileService;
        private readonly AutoMapper.IMapper _mapper;

        public CommentController(ICommentService commentService, AutoMapper.IMapper mapper, IProfileService profileService)
        {
            this._mapper = mapper;
            this._commentService = commentService;
            this._profileService = profileService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetAllComments()
        {
            var comments = await _commentService.GetAllComments();
            var commentsModel = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentModel>>(comments);
            foreach (CommentModel comment in commentsModel)
            {
                var profile = await _profileService.GetProfileById(comment.UserId);
                comment.Username = profile.Username;
            }
            return Ok(commentsModel);
        }

        [HttpGet("commentId")]
        public async Task<ActionResult<CommentModel>> GetCommentById(long commentId)
        {
            var comment = await _commentService.GetCommentById(commentId);
            var commentModel = _mapper.Map<Comment, CommentModel>(comment);

            var profile = await _profileService.GetProfileById(commentModel.UserId);
            commentModel.Username = profile.Username;

            return Ok(commentModel);
        }

        [HttpGet("postId")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetAllCommentsByPostId(long postId)
        {
            var comments = await _commentService.GetAllCommentsByPostId(postId);
            var commentsModel = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentModel>>(comments);
            foreach (CommentModel comment in commentsModel)
            {
                var profile = await _profileService.GetProfileById(comment.UserId);
                comment.Username = profile.Username;
            }
            return Ok(commentsModel);
        }

        [HttpGet("username")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetAllCommentsByUsername(string username)
        {
            var profile = await _profileService.GetProfileByUsername(username);
            if (profile == null)
            {
                return NotFound();
            }
            var comments = await _commentService.GetAllCommentsByUserId(profile.UserId);
            var commentsModel = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentModel>>(comments);
            foreach (CommentModel comment in commentsModel)
            {
                profile = await _profileService.GetProfileById(comment.UserId);
                comment.Username = profile.Username;
            }
            return Ok(commentsModel);
        }

        [HttpGet("parentId")]
        public async Task<ActionResult<IEnumerable<CommentModel>>> GetAllCommentsByParentComment(long parentId)
        {
            var comments = await _commentService.GetAllCommentsByParentComment(parentId);
            var commentsModel = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentModel>>(comments);
            foreach (CommentModel comment in commentsModel)
            {
                var profile = await _profileService.GetProfileById(comment.UserId);
                comment.Username = profile.Username;
            }
            return Ok(commentsModel);
        }

        [HttpPost]
        public async Task<ActionResult<CommentModel>> CreateComment(CommentModel newComment)
        {
            try
            {
                var comment = _mapper.Map<CommentModel, Comment>(newComment);
                var commentModel = await _commentService.CreateComment(comment);

                newComment = _mapper.Map<Comment, CommentModel>(commentModel);
                return Ok(newComment);
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

        [HttpDelete("commentId")]
        public async Task<ActionResult<CommentModel>> DeleteLike(long commentId)
        {
            try
            {
                var commentToBeDeleted = await _commentService.GetCommentById(commentId);

                if (commentToBeDeleted == null)
                    return NotFound();

                var postModel = _mapper.Map<Comment, CommentModel>(commentToBeDeleted);

                await _commentService.DeleteComment(commentToBeDeleted);
                return Ok(postModel);
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
        public async Task<ActionResult<CommentModel>> UpdateProfile(int id, CommentModel newComment)
        {
            try
            {
                var commentToUpdate = await _commentService.GetCommentById(id);

                if (commentToUpdate == null)
                    return NotFound();

                var comment = _mapper.Map<CommentModel, Comment>(newComment);

                await _commentService.UpdateComment(commentToUpdate, comment);

                comment = await _commentService.GetCommentById(id);
                newComment = _mapper.Map<Comment, CommentModel>(comment);

                return Ok(newComment);
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
    }
}