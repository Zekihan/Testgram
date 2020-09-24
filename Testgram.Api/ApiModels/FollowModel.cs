using System;

namespace Testgram.Api.ApiModels
{
    public partial class FollowModel
    {
        public long UserId { get; set; }
        public long FollowerId { get; set; }
        public string Username { get; set; }
        public string FollowerUsername { get; set; }
        public DateTime FollowDate { get; set; }
    }
}