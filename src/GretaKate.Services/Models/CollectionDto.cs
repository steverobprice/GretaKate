using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GretaKate.Services.Models
{
    public class CollectionDto : BaseDto
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string HeroImage { get; set; }
    }
}
