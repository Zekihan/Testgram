using System;
using System.Threading.Tasks;
using Testgram.Core.IRepositories;

namespace Testgram.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IProfileRepository Profile { get; }
        IPostRepository Post { get; }
        ILikeRepository Like { get; }
        IFollowRepository Follow { get; }
        ICommentRepository Comment { get; }
        Task<int> CommitAsync();
    }
}
