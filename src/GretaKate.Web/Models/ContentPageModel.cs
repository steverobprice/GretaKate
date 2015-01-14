using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Models;
using GretaKate.Services.Models;

namespace GretaKate.Web.Models
{
    public class ContentPageModel : RenderModel
    {
        public ContentPageModel(RenderModel model)
            : base(model.Content, model.CurrentCulture)
        { }

        public IList<DressDto> Dresses { get; set; }
    }
}