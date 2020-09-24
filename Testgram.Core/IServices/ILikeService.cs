using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IServices
{
    public interface ILikeService
    {
        Task<IEnumerable<Likes>> GetAllLikes();

        Task<Likes> GetLikeById(long postId, long userId);

        Task<IEnumerable<Likes>> GetLikesByUserId(long userId);

        Task<IEnumerable<Likes>> GetLikesByPostId(long postId);

        Task<Likes> CreateLike(Likes like);

        Task DeleteLike(Likes like);
    }
}