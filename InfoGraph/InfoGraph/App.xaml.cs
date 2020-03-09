using InfoGraph.Modules;
using InfoGraph.Services;
using InfoGraph.ViewModels;
using InfoGraph.Views;
using Ninject;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InfoGraph
{
    public partial class App : Application
    {
        public IKernel Kernel { get; set; }
        public App()
        {
            InitializeComponent();

            //Register core services
            Kernel = new StandardKernel(new InfoGraphCoreModule(), new InfoGraphNavModule());

            SetMainPage();
        }

        private void SetMainPage()
        {
            var bindingContext = Kernel.Get<MainViewModel>();
            var mainPage = new NavigationPage(new MainPage())
            {
                BindingContext = bindingContext,
                BarBackgroundColor = Color.FromHex("#8B002B"),
                BarTextColor = Color.White
            };
            var navService = Kernel.Get<INavService>() as XamarinFormsNavService;
            navService.XamarinFormsNav = mainPage.Navigation;

            MainPage = mainPage;
        }
    }
}
