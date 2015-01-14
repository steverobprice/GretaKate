using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GretaKate.Services.Models
{
    public interface IBaseDto
    {
        int Id { get; set; }
        int SortOrder { get; set; }
        string Url { get; set; }
    }

    public class BaseDto : IBaseDto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int SortOrder { get; set; }
        public string Url { get; set; }
    }
}
