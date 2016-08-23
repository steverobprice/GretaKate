using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;
using GretaKate.Core.Consts;
using GretaKate.Services.Models;
using Newtonsoft.Json;

namespace GretaKate.Services
{
    public interface IInstagramService
    {
        string GetAuthLink();
        OAuthResponse RequestToken(string code);
    }

    public class InstagramService : IInstagramService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;

        public InstagramService()
        {
            _clientId = ConfigurationManager.AppSettings[Settings.InstagramClientId];
            _clientSecret = ConfigurationManager.AppSettings[Settings.InstagramClientSecret];
            _redirectUri = ConfigurationManager.AppSettings[Settings.InstagramRedirectUri];
        }

        public string GetAuthLink()
        {
            var queryParams = $"client_id={_clientId}&redirect_uri={_redirectUri}&response_type=code";

            return $"https://api.instagram.com/oauth/authorize/?{queryParams}";
        }

        public OAuthResponse RequestToken(string code)
        {
            using (var client = new WebClient())
            {
                var myParameters = new NameValueCollection
                {
                    {"client_id", _clientId},
                    {"client_secret", _clientSecret},
                    {"grant_type", "authorization_code"},
                    {"redirect_uri", _redirectUri},
                    {"code", code}
                };
                
                var response = client.UploadValues("https://api.instagram.com/oauth/access_token", myParameters);
                
                var utf8Encoding = new UTF8Encoding();
                var responseBody = utf8Encoding.GetString(response);
                return JsonConvert.DeserializeObject<OAuthResponse>(responseBody);
            }
        }
    }
}
