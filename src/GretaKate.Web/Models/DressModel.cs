using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GretaKate.Services.Models;
using Umbraco.Web.Models;

namespace GretaKate.Web.Models
{
    public class DressModel : RenderModel
    {
        public DressModel(RenderModel model)
            : base(model.Content, model.CurrentCulture)
        {
        }
        
        public DressDto Dress { get; set; }
    }
}