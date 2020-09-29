using System;
using System.Threading.Tasks;
using Testgram.Core;
using Testgram.Core.Exceptions;
using Testgram.Core.IRepositories;
using Testgram.Data.Configurations;
using Testgram.Data.Repositories;

namespace Testgram.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialContext _context;
        private ProfileRepository _profileRepository;
        private PostRepository _postRepository;
        private LikeRepository _likeRepository;
        private FollowRepository _followRepository;
        private CommentRepository _commentRepository;

        public UnitOfWork(SocialContext context)
        {
            this._context = context;
        }

        public IProfileRepository Profile => _profileRepository = _profileRepository ?? new ProfileRepository(_context);

        public IPostRepository Post => _postRepository = _postRepository ?? new PostRepository(_context);

        public ILikeRepository Like => _likeRepository = _likeRepository ?? new LikeRepository(_context);

        public IFollowRepository Follow => _followRepository = _followRepository ?? new FollowRepository(_context);

        public ICommentRepository Comment => _commentRepository = _commentRepository ?? new CommentRepository(_context);

        public async Task<int> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (e.InnerException.Message.Contains("Post_fk0"))
                {
                    throw new UserNotExistsException();
                }
                else if (e.InnerException.Message.Contains("Comment_fk0"))
                {
                    throw new CommentNotExistsException();
                }
                else if (e.InnerException.Message.Contains("Comment_fk1"))
                {
                    throw new UserNotExistsException();
                }
                else if (e.InnerException.Message.Contains("Comment_fk2"))
                {
                    throw new PostNotExistsException();
                }
                else if (e.InnerException.Message.Contains("Follow_fk0"))
                {
                    throw new UserNotExistsException();
                }
                else if (e.InnerException.Message.Contains("Follow_fk1"))
                {
                    throw new UserNotExistsException();
                }
                else if (e.InnerException.Message.Contains("Likes_fk0"))
                {
                    throw new UserNotExistsException();
                }
                else if (e.InnerException.Message.Contains("Likes_fk1"))
                {
                    throw new PostNotExistsException();
                }
                else if (e.InnerException.Message.Contains("UQ__Profile__AB6E61642272DBD6"))
                {
                    throw new DBException("This email is already in use.");
                }
                else if (e.InnerException.Message.Contains("UQ__Profile__F3DBC57207E43CE6"))
                {
                    throw new DBException("This username is already in use.");
                }
            }
            throw new ArgumentException("Internal error.");
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}