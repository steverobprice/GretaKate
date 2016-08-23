using System.Threading.Tasks;
using System.Web.Mvc;
using GretaKate.Services;
using Umbraco.Web.Mvc;

namespace GretaKate.Web.Controllers
{
    public class InstagramAuthController : SurfaceController
    {
        private readonly IInstagramService _instagramService;

        public InstagramAuthController(IInstagramService instagramService)
        {
            _instagramService = instagramService;
        }

        public ActionResult Login()
        {
            var loginLink = _instagramService.GetAuthLink();

            return Redirect(loginLink);
        }

        public ActionResult Code(string code)
        {
            var oauthResponse = _instagramService.RequestToken(code).Result;

            Session["InstagramAccessToken"] = oauthResponse.AccessToken;

            return Redirect("/");
        }
    }
}