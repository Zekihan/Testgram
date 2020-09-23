using System;

namespace Testgram.Core.Models
{
    public partial class Likes
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
        public DateTime LikeDate { get; set; }

        public virtual Post Post { get; set; }
        public virtual Profile User { get; set; }
    }
}
