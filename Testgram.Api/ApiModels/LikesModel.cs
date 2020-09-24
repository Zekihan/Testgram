using System;

namespace Testgram.Api.ApiModels
{
    public partial class LikesModel
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public long PostId { get; set; }
        public DateTime LikeDate { get; set; }
    }
}