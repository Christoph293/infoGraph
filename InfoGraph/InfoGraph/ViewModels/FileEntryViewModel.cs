using InfoGraph.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InfoGraph.ViewModels
{
    public class FileEntryViewModel : BaseViewModel
    {
        public delegate void EntrySelectedDelegate(string path);
        public event EntrySelectedDelegate EntrySelected;

        public FileEntryViewModel(string path)
        {
            Path = path;
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        private string path;

        public Command SelectPathCommand
        {
            get
            {
                return _selectPathCommand ?? (_selectPathCommand = new Command(SelectPath));
            }
        }
        private Command _selectPathCommand;

        private void SelectPath()
        {
            EntrySelected?.Invoke(path);
        }
    }
}
