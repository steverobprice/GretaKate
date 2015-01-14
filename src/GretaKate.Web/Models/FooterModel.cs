using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GretaKate.Services.Models;

namespace GretaKate.Web.Models
{
    public class FooterModel
    {
        public string LocationHeading { get; set; }
        public string Location { get; set; }
        public string OpeningHoursHeading { get; set; }
        public string OpeningHours { get; set; }
        public string SocialHeading { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string PinterestLink { get; set; }
        public string YouTubeLink { get; set; }
    }
}