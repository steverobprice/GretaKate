using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;
using GretaKate.Services;
using System.Web.Mvc;
using GretaKate.Web.Models;
using Umbraco.Web.Models;

namespace GretaKate.Web.Controllers
{
    public class DressController : RenderMvcController
    {
        private readonly IDressService _dressService;

        public DressController(IDressService dressService)
        {
            _dressService = dressService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var dressModel = new DressModel(model);

            dressModel.Dress = _dressService.GetById(CurrentPage.Id);

            return CurrentTemplate(dressModel);
        }
    }
}