using SportStore.Domain.Entities;
using SportStore.WebUI.Binders;
using SportStore.WebUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //добавлено для фабрики контроллеров 
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            //
            ModelBinders.Binders.Add(typeof(Cart),new CartModelBinder());
        }
    }
}
