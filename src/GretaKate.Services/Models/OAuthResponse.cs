using Newtonsoft.Json;

namespace GretaKate.Services.Models
{
    public class OAuthResponse
    {
        public UserInfo User { get; set; }

        [JsonProperty("Access_Token")]
        public string AccessToken { get; set; }
    }
}
