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
    public interface ICollectionService
    {
        IList<CollectionDto> GetAll();
        IList<CollectionDto> GetChildren(int currentPageId);
        CollectionDto GetFirstDecendant(int currentPageId);
    }

    public class CollectionService : UmbracoBaseService<CollectionDto>, ICollectionService
    {
        private readonly IContentService _contentService;
        private readonly IMediaService _mediaService;

        public CollectionService(IContentService contentService,
            IContentTypeService contentTypeService,
            IMediaService mediaService,
            HttpContextBase httpContextBase)
            : base(contentService, contentTypeService, mediaService, httpContextBase)
        {
            _contentService = contentService;
            _mediaService = mediaService;
        }

        public IList<CollectionDto> GetAll()
        {
            var contentItems = base.GetAll(ContentTypes.Collection, null);
            var collections = base.Map(contentItems);

            MapFields(contentItems, collections);

            return collections.OrderBy(x => x.SortOrder).ToList();
        }

        public IList<CollectionDto> GetChildren(int currentId)
        {
            var contentItems = base.GetChildren(currentId, null);
            var collections = base.Map(contentItems);

            MapFields(contentItems, collections);

            return collections.OrderBy(x => x.SortOrder).ToList();
        }

        public IList<CollectionDto> GetCollectionDescendants(int currentId)
        {
            var contentItems = base.GetAllDescendants(currentId, ContentTypes.Collection, null);
            var collections = base.Map(contentItems);

            MapFields(contentItems, collections);

            return collections.OrderBy(x => x.SortOrder).ToList();
        }

        public CollectionDto GetFirstDecendant(int currentId)
        {
            var content = base.GetAllDescendants(currentId, ContentTypes.Collection, null).FirstOrDefault();
            var collection = base.Map(content);

            MapFields(content, collection);

            return collection;
        }

        private void MapFields(IEnumerable<IContent> contentItems, IEnumerable<CollectionDto> collections)
        {
            if (collections != null)
            {
                foreach (var contentItem in contentItems)
                {
                    var collection = collections.FirstOrDefault(c => c.Id == contentItem.Id);

                    MapFields(contentItem, collection);
                }
            }
        }

        private void MapFields(IContent contentItem, CollectionDto collection)
        {
            if (collection != null)
            {
                collection.Title = base.GetStringValue(contentItem, Collection.Title);
                collection.Summary = base.GetStringValue(contentItem, Collection.Summary);
                collection.HeroImage = GetFileUrl(contentItem, Collection.HeroImage);
                collection.Url = umbraco.library.NiceUrl(contentItem.Id);
            }
        }
    }
}
