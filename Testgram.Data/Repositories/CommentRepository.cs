using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Testgram.Core.IRepositories;
using Testgram.Core.Models;
using Testgram.Data.Configurations;

namespace Testgram.Data.Repositories
{
    class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(SocialContext context)
            : base(context)
        { }

        private SocialContext SocialContext
        {
            get { return Context as SocialContext; }
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await SocialContext.Comment
                    .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByParentIdAsync(long id)
        {
            return await SocialContext.Comment
                    .Where(a => a.ParentComment == id)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(long id)
        {
            return await SocialContext.Comment
                    .Where(a => a.PostId == id)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(long id)
        {
            return await SocialContext.Comment
                    .Where(a => a.UserId == id)
                    .ToListAsync();
        }
    }
}
