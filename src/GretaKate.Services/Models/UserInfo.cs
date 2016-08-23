using Newtonsoft.Json;

namespace GretaKate.Services.Models
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string Username { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("profile_picture")]
        public string ProfilePicture { get; set; }
    }
}
