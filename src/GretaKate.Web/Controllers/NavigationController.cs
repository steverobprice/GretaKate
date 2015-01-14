using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using GretaKate.Web.Models;
using Umbraco.Core.Services;
using GretaKate.Core.Consts;
using GretaKate.Services;

namespace GretaKate.Web.Controllers
{
    public class NavigationController : SurfaceController
    {
        private readonly INavigationService _navigationService;

        public NavigationController(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        [ChildActionOnly]
        public ActionResult TopNavigation()
        {
            var model = new NavigationModel();

            model.NavigationItems = _navigationService.GetNavigationItems();
            model.IsHomePage = CurrentPage.Level == 1;

            return PartialView("_TopNavigation", model);
        }

        [ChildActionOnly]
        public ActionResult MobileNavigation()
        {
            var model = new NavigationModel();

            model.NavigationItems = _navigationService.GetNavigationItems();
            model.IsHomePage = CurrentPage.Level == 1;

            return PartialView("_MobileNavigation", model);
        }
    }
}