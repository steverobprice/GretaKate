using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using System.Web;
using AutoMapper;
using GretaKate.Core.Consts.FieldNames;

namespace GretaKate.Services
{
    public interface IUmbracoBaseGetService<T> where T : class
    {
    }

    public class UmbracoBaseService<T> : BaseService, IUmbracoBaseGetService<T> where T : class
    {
        private readonly IContentService _contentService;
        private readonly IContentTypeService _contentTypeService;
        private readonly IMediaService _mediaService;

        public UmbracoBaseService(
            IContentService contentService,
            IContentTypeService contentTypeService,
            IMediaService mediaService,
            HttpContextBase httpContextBase)
            : base(httpContextBase)
        {
            _contentService = contentService;
            _contentTypeService = contentTypeService;
            _mediaService = mediaService;
        }

        protected IList<IContent> GetAll(string type, int? numberOfItems)
        {
            IContentType contentType = GetContentType(type);

            return GetAll(contentType.Id, numberOfItems);
        }

        protected IList<IContent> GetAllDescendants(int currentId, string type, int? numberOfItems)
        {
            IContentType contentType = GetContentType(type);
            return GetAllDescendants(currentId, contentType.Id, numberOfItems);
        }

        protected IContentType GetContentType(string alias)
        {
            return _contentTypeService.GetContentType(alias);
        }

        protected IList<IContent> GetAll(int typeId, int? numberOfItems)
        {
            var contentItems = _contentService.GetContentOfContentType(typeId).Where(x => x.Published);
            
            if (numberOfItems.HasValue)
            {
                contentItems = contentItems.Take(numberOfItems.Value);
            }

            return contentItems.ToList();
        }

        protected IList<IContent> GetChildren(int currentId, int? numberOfItems)
        {
            var contentItems = _contentService.GetChildren(currentId).Where(x => x.Published);

            if (numberOfItems.HasValue)
            {
                contentItems = contentItems.Take(numberOfItems.Value);
            }

            return contentItems.ToList();
        }

        protected IList<IContent> GetAllDescendants(int currentId, int typeId, int? numberOfItems)
        {
            var contentItems =
                _contentService.GetDescendants(currentId).Where(x => x.ContentTypeId == typeId && x.Published);
            if (numberOfItems.HasValue)
            {
                contentItems = contentItems.Take(numberOfItems.Value);
            }

            return contentItems.ToList();
        }

        protected IContent GetById(int currentId)
        {
            return _contentService.GetById(currentId);
        }

        protected IList<T> Map(IList<IContent> content)
        {
            Mapper.CreateMap<Content, T>();

            return Mapper.Map<IList<IContent>, IList<T>>(content);
        }

        protected IList<T> Map(IList<IMember> member)
        {
            Mapper.CreateMap<Member, T>();

            return Mapper.Map<IList<IMember>, IList<T>>(member);
        }

        protected T Map(IContent content)
        {
            Mapper.CreateMap<IContent, T>();

            return Mapper.Map<IContent, T>(content);
        }

        protected string GetStringValue(IContent content, string propertyAlias)
        {
            string returnValue = string.Empty;

            if (content.HasProperty(propertyAlias))
            {
                var value = content.GetValue(propertyAlias);

                if (value != null && value.ToString() != string.Empty)
                {
                    returnValue = value.ToString();
                }
            }

            return returnValue;
        }

        protected int? GetIntValue(IContent content, string propertyAlias)
        {
            int? returnValue = null;
            int tempValue;

            string value = GetStringValue(content, propertyAlias);

            //bug in Umbraco, that sometimes saves ints with quotes
            value = value.Replace("\"", "");

            if (Int32.TryParse(value, out tempValue))
            {
                returnValue = tempValue;
            }

            return returnValue;
        }

        protected DateTime? GetDateValue(IContent content, string propertyAlias)
        {
            DateTime? returnValue = null;
            DateTime tempValue;

            string value = GetStringValue(content, propertyAlias);

            if (DateTime.TryParse(value, out tempValue))
            {
                returnValue = tempValue;
            }

            return returnValue;
        }

        protected bool GetBoolValue(IContent content, string propertyAlias)
        {
            bool returnValue = false;

            string value = GetStringValue(content, propertyAlias);

            if (value == "1")
            {
                returnValue = true;
            }

            return returnValue;
        }

        protected string GetFileUrl(IContent content, string propertyAlias)
        {
            string url = null;

            if (content.HasProperty(propertyAlias))
            {
                var value = content.GetValue(propertyAlias);

                url = GetFileUrl(value.ToString());
            }

            return url;
        }

        protected string GetFileUrl(string mediaId)
        {
            string url = null;
            int id;

            if (int.TryParse(mediaId, out id))
            {
                var media = _mediaService.GetById(id);

                if (media != null && media.Properties.Contains(Generic.UmbracoFile) && media.Properties[Generic.UmbracoFile] != null)
                {
                    url = media.Properties[Generic.UmbracoFile].Value.ToString();
                }
            }

            return url;
        }
    }
}
