using InfoGraph.Services;
using InfoGraph.ViewModels;
using InfoGraph.Views;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoGraph.Modules
{
    public class InfoGraphNavModule : NinjectModule
    {
        public override void Load()
        {
            var navService = new XamarinFormsNavService();

            //Register view mappings
            navService.RegisterViewMapping(typeof(MainViewModel), typeof(MainPage));
            navService.RegisterViewMapping(typeof(FileIOTransferVM), typeof(FileIOTransferPage));
            navService.RegisterViewMapping(typeof(WebTransferVM), typeof(WebTransferPage));

            Bind<INavService>()
                .ToMethod(x => navService)
                .InSingletonScope();
        }
    }
}
