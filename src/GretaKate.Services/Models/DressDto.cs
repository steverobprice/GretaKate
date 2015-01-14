using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GretaKate.Services.Models
{
    public class DressDto : BaseDto
    {
        public DressDto()
        {
            Images = new List<ImageDto>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string HeroImage { get; set; }
        public string Price { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}
