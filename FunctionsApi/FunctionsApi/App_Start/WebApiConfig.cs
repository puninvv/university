﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FunctionsApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы Web API

            // Маршруты Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{function}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );
        }
    }
}
