using System;

namespace Testgram.Core.Models
{
    public partial class Follow
    {
        public long UserId { get; set; }
        public long FollowerId { get; set; }
        public DateTime FollowDate { get; set; }

        public virtual Profile Follower { get; set; }
        public virtual Profile User { get; set; }
    }
}
