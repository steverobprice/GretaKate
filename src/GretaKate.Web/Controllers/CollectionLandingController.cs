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
    public class CollectionLandingController : RenderMvcController
    {
        private readonly ICollectionService _collectionService;

        public CollectionLandingController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        public override ActionResult Index(RenderModel model)
        {
            var collectionModel = new CollectionLandingModel(model);

            var collections = _collectionService.GetChildren(CurrentPage.Id);
            if (collections != null)
            {
                collectionModel.Collections = collections;
            }

            return CurrentTemplate(collectionModel);
        }
    }
}