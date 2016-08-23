using Umbraco.Web.Mvc;
using GretaKate.Services;
using System.Web.Mvc;
using GretaKate.Web.Models;
using Umbraco.Web.Models;

namespace GretaKate.Web.Controllers
{
    public class CollectionController : RenderMvcController
    {
        private readonly IDressService _dressService;

        public CollectionController(IDressService dressService)
        {
            _dressService = dressService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var dressModel = new DressesModel(model);

            dressModel.Heading = string.Empty;
            dressModel.Content = string.Empty;

            var dresses = _dressService.GetAllDescendants(CurrentPage.Id);
            if (dresses != null)
            {
                dressModel.Dresses = dresses;
            }

            return CurrentTemplate(dressModel);
        }
    }
}