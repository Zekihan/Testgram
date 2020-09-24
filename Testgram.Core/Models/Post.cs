using System;
using System.Collections.Generic;

namespace Testgram.Core.Models
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            Likes = new HashSet<Likes>();
        }

        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }

        public virtual Profile User { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Likes> Likes { get; set; }
    }
}