using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Services;
using GretaKate.Services.Models;
using GretaKate.Core.Consts;

namespace GretaKate.Services
{
    public interface INavigationService
    {
        List<NavigationItemDto> GetNavigationItems();
    }

    public class NavigationService: INavigationService
    {
        private readonly IContentService _contentService;
        private readonly IContentTypeService _contentTypeService;

        public NavigationService(
            IContentService contentService,
            IContentTypeService contentTypeService)
        {
            _contentService = contentService;
            _contentTypeService = contentTypeService;
        }

        public List<NavigationItemDto> GetNavigationItems()
        {
            var contentType = _contentTypeService.GetContentType(ContentTypes.Home);
            var homeItem = _contentService.GetContentOfContentType(contentType.Id).FirstOrDefault();
            return GetNavigationItems(homeItem.Id);
        }

        private List<NavigationItemDto> GetNavigationItems(int id)
        {
            var navItems = new List<NavigationItemDto>();

            foreach (var contentItem in _contentService.GetChildren(id).Where(x => x.Published && !x.GetValue<bool>("umbracoNaviHide")).ToList())
            {
                var navItem = new NavigationItemDto
                {
                    Url = umbraco.library.NiceUrl(contentItem.Id),
                    Name = contentItem.Name,
                    SortOrder = contentItem.SortOrder,
                    Children = GetNavigationItems(contentItem.Id)
                };

                navItems.Add(navItem);
            }

            return navItems.OrderBy(i => i.SortOrder).ToList();
        }
    }
}
