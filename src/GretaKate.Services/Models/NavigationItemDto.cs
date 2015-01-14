using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GretaKate.Services.Models
{
    public class NavigationItemDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
        public List<NavigationItemDto> Children { get; set; }
        public bool HasChildren { get { return Children.Any(); } }
    }
}
