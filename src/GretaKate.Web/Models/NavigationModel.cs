using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GretaKate.Services.Models;

namespace GretaKate.Web.Models
{
    public class NavigationModel
    {
        public List<NavigationItemDto> NavigationItems { get; set; }
        public bool IsHomePage { get; set; }
    }
}