using InfoGraph.Services;
using InfoGraph.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfoGraph.Modules
{
    public class InfoGraphCoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainViewModel>().ToSelf();
            Bind<FileIOTransferVM>().ToSelf();
            Bind<WebTransferVM>().ToSelf();

            //Core services
            var fileIOService = new InfoGraphFileService();

            Bind<IInfoGraphFileService>()
                .ToMethod(x => fileIOService)
                .InSingletonScope();
        }
    }
}
