using System;

namespace Testgram.Api.ApiModels
{
    public partial class CommentModel
    {
        public CommentModel()
        {
        }

        public long CommentId { get; set; }
        public string Content { get; set; }
        public long? ParentComment { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public long PostId { get; set; }
        public DateTime CommentDate { get; set; }
    }
}