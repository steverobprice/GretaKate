using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GretaKate.Services.Models;
using Umbraco.Web.Models;

namespace GretaKate.Web.Models
{
    public class DressesModel : RenderModel
    {
        public DressesModel(RenderModel model)
            : base(model.Content, model.CurrentCulture)
        {
            Dresses = new List<DressDto>();
        }

        public string Heading { get; set; }
        public string Content { get; set; }
        public IList<DressDto> Dresses { get; set; }
    }
}