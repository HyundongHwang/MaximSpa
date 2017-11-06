using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BlackBoxLib;
using MaximSpa.Assets.Strings;
using MaximSpa.Utils;

namespace MaximSpa
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.EnableCors();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Filters.Add(new MyLogFilterAttribute());
            config.Filters.Add(new MyExceptionFilterAttribute());

            BlackBox.Init(MaximSecureKeysStrings.azure_storage_connection_string);
        }
    }
}
