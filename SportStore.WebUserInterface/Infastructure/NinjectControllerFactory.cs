using Ninject;
using SportStore.Domain.Abstract;
using SportStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportStore.WebUserInterface.Infastructure
{
    //реализация пользовательской фабрики контроллеров
    //наследуясь от фабрики используемой по умолчанию
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            //создание контейнера
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext,Type controllerType)
        {
            //получение обьекта контроллера из контейнера
            //используя его тип
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            //конфигурирование контейнера
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}