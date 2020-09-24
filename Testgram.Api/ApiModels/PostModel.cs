using System;

namespace Testgram.Api.ApiModels
{
    public partial class PostModel
    {
        public PostModel()
        {
        }

        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
    }
}