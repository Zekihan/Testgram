using System;
using System.Collections.Generic;

namespace Testgram.Core.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParentCommentNavigation = new HashSet<Comment>();
        }

        public long CommentId { get; set; }
        public string Content { get; set; }
        public long? ParentComment { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }
        public DateTime CommentDate { get; set; }

        public virtual Comment ParentCommentNavigation { get; set; }
        public virtual Post Post { get; set; }
        public virtual Profile User { get; set; }
        public virtual ICollection<Comment> InverseParentCommentNavigation { get; set; }
    }
}