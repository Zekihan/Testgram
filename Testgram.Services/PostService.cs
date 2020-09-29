using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core;
using Testgram.Core.Exceptions;
using Testgram.Core.IServices;
using Testgram.Core.Models;

namespace Testgram.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Post> CreatePost(Post post)
        {
            try
            {
                await _unitOfWork.Post.AddAsync(post);
                await _unitOfWork.CommitAsync();
                return post;
            }
            catch (DBException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Internal error.");
            }
        }

        public async Task DeletePost(Post post)
        {
            try
            {
                _unitOfWork.Post.Remove(post);
                await _unitOfWork.CommitAsync();
            }
            catch (DBException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Internal error.");
            }
        }

        public async Task UpdatePost(Post postToBeUpdated, Post post)
        {
            try
            {
                postToBeUpdated.Content = post.Content;
                await _unitOfWork.CommitAsync();
            }
            catch (DBException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Internal error.");
            }
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _unitOfWork.Post.GetAllPostsAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAfterDate(DateTime dateTime)
        {
            return await _unitOfWork.Post.GetPostsAfterDateAsync(dateTime);
        }

        public async Task<Post> GetPostById(long id)
        {
            return await _unitOfWork.Post.GetPostByIdAsync(id);
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(long userId)
        {
            return await _unitOfWork.Post.GetPostsByUserIdAsync(userId);
        }
    }
}