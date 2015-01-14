using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using GretaKate.Services.Models;
using System.Web;
using GretaKate.Core.Consts;
using GretaKate.Core.Consts.FieldNames;

namespace GretaKate.Services
{
    public interface IDressService
    {
        IList<DressDto> GetAll();
        IList<DressDto> GetAllDescendants(int currentPageId);
        DressDto GetById(int currentPageId);
    }

    public class DressService : UmbracoBaseService<DressDto>, IDressService
    {
        private readonly IContentService _contentService;
        private readonly IMediaService _mediaService;

        public DressService(IContentService contentService,
            IContentTypeService contentTypeService,
            IMediaService mediaService,
            HttpContextBase httpContextBase)
            : base(contentService, contentTypeService, mediaService, httpContextBase)
        {
            _contentService = contentService;
            _mediaService = mediaService;
        }

        public IList<DressDto> GetAll()
        {
            var contentItems = base.GetAll(ContentTypes.Dress, null);
            var dresses = base.Map(contentItems);

            MapFields(contentItems, dresses);

            return dresses.OrderBy(x => x.SortOrder).ToList();
        }

        public IList<DressDto> GetAllDescendants(int currentId)
        {
            var contentItems = base.GetAllDescendants(currentId, ContentTypes.Dress, null);
            var dresses = base.Map(contentItems);

            MapFields(contentItems, dresses);

            return dresses.OrderBy(x => x.SortOrder).ToList();
        }

        public DressDto GetById(int currentId)
        {
            var content = base.GetById(currentId);
            var dress = base.Map(content);

            MapFields(content, dress);

            return dress;
        }

        private void MapFields(IEnumerable<IContent> contentItems, IEnumerable<DressDto> dresses)
        {
            if (dresses != null)
            {
                foreach (var contentItem in contentItems)
                {
                    var dress = dresses.FirstOrDefault(d => d.Id == contentItem.Id);

                    MapFields(contentItem, dress);
                }
            }
        }

        private void MapFields(IContent contentItem, DressDto dress)
        {
            if (dress != null)
            {
                dress.Title = base.GetStringValue(contentItem, Dress.Title);
                dress.Description = base.GetStringValue(contentItem, Dress.Description);
                dress.HeroImage = GetFileUrl(contentItem, Dress.HeroImage);
                dress.Price = base.GetStringValue(contentItem, Dress.Price);
                dress.Url = umbraco.library.NiceUrl(contentItem.Id);
                dress.Images.Add(new ImageDto { Id = dress.Id.ToString(), Url = dress.HeroImage });

                var images = base.GetStringValue(contentItem, Dress.OtherImages);
                foreach (var image in images.Split(',').Where(i => i != string.Empty))
                {
                    dress.Images.Add(new ImageDto { Id = image, Url = GetFileUrl(image) });
                }
            }
        }
    }
}
