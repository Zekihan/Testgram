using System.Threading.Tasks;
using Testgram.Core;
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
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
