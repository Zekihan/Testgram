using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IServices
{
    public interface ILikeService
    {
        Task<IEnumerable<Likes>> GetAllLikes();
        Task<IEnumerable<Likes>> GetLikesByUserId(int userId);
        Task<IEnumerable<Likes>> GetLikesByPostId(int postId);
        Task<Likes> CreateLike(Likes like);
        Task DeleteLike(Likes like);
    }
}
