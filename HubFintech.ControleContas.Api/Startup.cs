using System;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using Fabrik.Common.WebAPI;
using HubFintech.ControleContas.Api;
using HubFintech.ControleContas.Api.Configuration;
using HubFintech.ControleContas.Api.Configuration.Factories;
using HubFintech.ControleContas.Api.Configuration.Filters;
using HubFintech.ControleContas.Api.Configuration.Handlers;
using HubFintech.ControleContas.Api.Configuration.Middlewares;
using HubFintech.ControleContas.Api.Domain.Repositories;
using HubFintech.ControleContas.Api.Domain.Repositories.Interfaces;
using HubFintech.ControleContas.Api.Domain.ViewModels.Response.Hypermedia;
using log4net.Config;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Owin;
using SimpleInjector.Integration.WebApi;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(HubFintech.ControleContas.Api.Startup))]

namespace HubFintech.ControleContas.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            XmlConfigurator.Configure();

            var httpConfiguration = new HttpConfiguration();
            ConfigureHttpConfiguration(httpConfiguration);
            ConfigureSwagger(httpConfiguration);

            var container = ConfigureContainer();

            container.RegisterWebApiControllers(httpConfiguration);
            container.EnableHttpRequestMessageTracking(httpConfiguration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
            httpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            app.UseOwinRequestLifestyle();
            app.UseCors(CorsOptions.AllowAll);
            app.Use<HttpHeaderMiddleware>();
            app.UseWebApi(httpConfiguration);
        }

        private Container ConfigureContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new OwinRequestLifestyle();

            container.Register<DbContext, BaseContext>(Lifestyle.Scoped);
            container.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>), Lifestyle.Scoped);

            container.Register<ILogFactory, LogFactory>(Lifestyle.Scoped);

            return container;
        }

        private void ConfigureHttpConfiguration(HttpConfiguration httpConfiguration)
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);

            ConfigureHttpRoutes(httpConfiguration);
            ConfigureHttpFormatters(httpConfiguration);
            ConfigureHttpHandlers(httpConfiguration);
            ConfigureHttpFilters(httpConfiguration);
            ConfigureEnricher(httpConfiguration);
            httpConfiguration.EnsureInitialized();
        }

        private void ConfigureHttpRoutes(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.MapHttpAttributeRoutes();

            httpConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );
        }

        private void ConfigureHttpHandlers(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.MessageHandlers.Add(new PayloadLoggingHandler());
        }

        private void ConfigureHttpFilters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Filters.Add(new ValidateModelStateFilter());
        }

        private void ConfigureHttpFormatters(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling =
                DateTimeZoneHandling.Utc;
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.DateParseHandling =
                DateParseHandling.DateTimeOffset;
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.DateFormatHandling =
                DateFormatHandling.IsoDateFormat;
            httpConfiguration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling =
                ReferenceLoopHandling.Ignore;
            httpConfiguration.Formatters.Remove(httpConfiguration.Formatters.XmlFormatter);
        }

        private void ConfigureEnricher(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.AddResponseEnrichers(
                new PessoaResponseEnricher()
            );
        }

        private void ConfigureSwagger(HttpConfiguration httpConfiguration)
        {
            httpConfiguration
                .EnableSwagger(x =>
                {
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
                    var commentsFile = Path.Combine(baseDirectory, commentsFileName);

                    x.SingleApiVersion("v1", "HubFintech.ControleContas.Api");
                    x.IncludeXmlComments(commentsFile);
                })
                .EnableSwaggerUi(x =>
                {
                    x.DocExpansion(DocExpansion.List);
                    x.SupportedSubmitMethods("POST", "PUT", "GET", "DELETE");
                });
        }
    }
}