namespace Testgram.Api.ApiModels
{
    public class ProfileModel
    {
        public ProfileModel()
        {

        }

        public long UserId { get; set; }
        public string Username { get; set; }
        public string Biografy { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
