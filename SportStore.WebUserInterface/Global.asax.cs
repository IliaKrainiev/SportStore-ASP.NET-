using SportStore.WebUserInterface.Infastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportStore.WebUserInterface
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
        }
    }
}
