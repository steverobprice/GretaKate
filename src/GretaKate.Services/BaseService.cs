using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GretaKate.Services
{
    public interface IBaseService
    {
    }

    public class BaseService : IBaseService
    {
        private readonly HttpContextBase _httpContextBase;
        
        public BaseService(HttpContextBase httpContextBase)
        {
            _httpContextBase = httpContextBase;
        }

        public HttpContextBase BaseHttpContext
        {
            get { return _httpContextBase; }
        }
    }
}
