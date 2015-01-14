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
using GretaKate.Services.Models;
using AutoMapper;

namespace GretaKate.Web.Controllers
{
    public class FooterController : SurfaceController
    {
        private readonly IFooterService _footerService;

        public FooterController(IFooterService footerService)
        {
            _footerService = footerService;
        }

        [ChildActionOnly]
        public ActionResult Index()
        {
            var footer = _footerService.GetFooter();

            Mapper.CreateMap<FooterDto, FooterModel>();
            
            return PartialView("_Footer", Mapper.Map<FooterModel>(footer));
        }
    }
}