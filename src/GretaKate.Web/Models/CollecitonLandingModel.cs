using GretaKate.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Umbraco.Web.Models;

namespace GretaKate.Web.Models
{
    public class CollectionLandingModel : RenderModel
    {
        public CollectionLandingModel(RenderModel model)
            : base(model.Content, model.CurrentCulture)
        {
            Collections = new List<CollectionDto>();
        }

        public IList<CollectionDto> Collections { get; set; }
    }
}
