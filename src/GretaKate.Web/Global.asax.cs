using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using System.Reflection;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using GretaKate.Services;
using Umbraco.Web;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace GretaKate.Web
{
    public class MvcApplication : UmbracoApplication
    {
        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            base.OnApplicationStarted(sender, e);

            //var builder = new ContainerBuilder();
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterModule(new AutofacWebTypesModule());

            //builder.Register(c => ApplicationContext.Current.Services.MediaService).As<IMediaService>().InstancePerHttpRequest();
            //builder.Register(c => ApplicationContext.Current.Services.ContentService).As<IContentService>().InstancePerHttpRequest();
            //builder.Register(c => ApplicationContext.Current.Services.ContentTypeService).As<IContentTypeService>().InstancePerHttpRequest();


            var builder = new ContainerBuilder();

            //register all controllers found in this assembly
            builder.RegisterControllers(typeof(UmbracoApplication).Assembly);
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterApiControllers(typeof(UmbracoApplication).Assembly);

            builder.RegisterModule(new AutofacWebTypesModule());

            builder.Register(c => ApplicationContext.Current.Services.MediaService).As<IMediaService>().InstancePerHttpRequest();
            builder.Register(c => ApplicationContext.Current.Services.ContentService).As<IContentService>().InstancePerHttpRequest();
            builder.Register(c => ApplicationContext.Current.Services.ContentTypeService).As<IContentTypeService>().InstancePerHttpRequest();

            // Scan an assembly for components
            builder.RegisterAssemblyTypes(typeof(BaseService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerHttpRequest();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}