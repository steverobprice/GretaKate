using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using GretaKate.Services.Models;
using GretaKate.Core.Consts;
using GretaKate.Core.Consts.FieldNames;
using System.Web;

namespace GretaKate.Services
{
    public interface IFooterService
    {
        FooterDto GetFooter();
    }

    public class FooterService : UmbracoBaseService<FooterDto>, IFooterService
    {
        private readonly IContentService _contentService;
        private readonly IContentTypeService _contentTypeService;

        public FooterService(
            IContentService contentService,
            IContentTypeService contentTypeService,
            IMediaService mediaService,
            HttpContextBase httpContextBase)
            : base(contentService, contentTypeService, mediaService, httpContextBase)
        {
            _contentService = contentService;
            _contentTypeService = contentTypeService;
        }

        public FooterDto GetFooter()
        {
            var home = base.GetAll(ContentTypes.Home, null).FirstOrDefault();
            var footer = base.Map(home);

            MapFields(home, footer);

            return footer;
        }

        private void MapFields(IContent contentItem, FooterDto footer)
        {
            if (footer != null)
            {
                footer.LocationHeading = base.GetStringValue(contentItem, Footer.LocationHeading);
                footer.Location = base.GetStringValue(contentItem, Footer.Location);
                footer.OpeningHoursHeading = base.GetStringValue(contentItem, Footer.OpeningHoursHeading);
                footer.OpeningHours = base.GetStringValue(contentItem, Footer.OpeningHours);
                footer.SocialHeading = base.GetStringValue(contentItem, Footer.SocialHeading);
                footer.FacebookLink = base.GetStringValue(contentItem, Footer.FacebookLink);
                footer.InstagramLink = base.GetStringValue(contentItem, Footer.InstagramLink);
                footer.PinterestLink = base.GetStringValue(contentItem, Footer.PinterestLink);
                footer.YouTubeLink = base.GetStringValue(contentItem, Footer.YouTubeLink);
            }
        }
    }
}
