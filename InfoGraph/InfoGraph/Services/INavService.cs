using InfoGraph.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace InfoGraph.Services
{
    public interface INavService
    {
        bool CanGoBack { get; }
        Task GoBack();
        Task NavigateTo<TVM>() where TVM : BaseViewModel;
        Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel;
        void RemoveLastView();
        void ClearBackStack();
        void NavigateToUri(Uri uri);

        event PropertyChangedEventHandler CanGoBackChanged;

    }
}
