using System.Collections.Generic;

namespace Testgram.Core.Models
{
    public partial class Profile
    {
        public Profile()
        {
            Comment = new HashSet<Comment>();
            FollowFollower = new HashSet<Follow>();
            FollowUser = new HashSet<Follow>();
            Likes = new HashSet<Likes>();
            Post = new HashSet<Post>();
        }

        public long UserId { get; set; }
        public string Username { get; set; }
        public string Biografy { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Follow> FollowFollower { get; set; }
        public virtual ICollection<Follow> FollowUser { get; set; }
        public virtual ICollection<Likes> Likes { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
