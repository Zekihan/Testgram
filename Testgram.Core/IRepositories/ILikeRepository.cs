﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Testgram.Core.Models;

namespace Testgram.Core.IRepositories
{
    public interface ILikeRepository : IRepository<Likes>
    {
        Task<IEnumerable<Likes>> GetAllLikesAsync();
        Task<IEnumerable<Likes>> GetLikesByPostIdAsync(long id);
        Task<IEnumerable<Likes>> GetLikesByUserIdAsync(long id);
    }
}
