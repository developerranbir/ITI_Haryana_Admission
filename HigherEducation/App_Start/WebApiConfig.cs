using HigherEducation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace HigherEducation.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.EnableCors();
            var route = RouteTable.Routes.MapHttpRoute(
               name: "Version1",
               routeTemplate: "api/v1/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
           );
            route.RouteHandler = new MyHttpControllerRouteHandler();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}