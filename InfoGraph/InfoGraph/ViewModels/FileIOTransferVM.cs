using InfoGraph.Services;
using InfoGraphModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace InfoGraph.ViewModels
{
    public class FileIOTransferVM : BaseViewModel<Table>
    {
        private IInfoGraphFileService _fileService;
        public FileIOTransferVM(INavService navService, IInfoGraphFileService fileService)
            :base(navService)
        {
            _fileService = fileService;
            InitializeExistingFiles();
        }

        public Table CurrentTable { get; set; }

        public Command SaveTableCommand
        {
            get
            {
                return _saveTableCommand ?? (_saveTableCommand = new Command(SaveTable));
            }
        }
        private Command _saveTableCommand;

        public IEnumerable<FileEntryViewModel> ExistingFiles
        {
            get { return _existingFiles; }
            set { _existingFiles = value; }
        }
        private IEnumerable<FileEntryViewModel> _existingFiles;

        private void InitializeExistingFiles()
        {
            var files = _fileService.GetExistingFilesInAppDir();
            ExistingFiles = files.Select(x => new FileEntryViewModel(x)).ToList();
            foreach (var file in ExistingFiles)
            {
                file.EntrySelected += File_EntrySelected;
            }
        }

        private void File_EntrySelected(string path)
        {
            SelectFile(path);
        }

        public override void Init(Table table)
        {
            CurrentTable = table;
        }

        private void SaveTable()
        {
            _fileService.PersistTable(CurrentTable);
        }
    
        private void SelectFile(string path)
        {
            var table = _fileService.LoadTableByPath(path);
            NavService.NavigateTo<MainViewModel, Table>(table);
        }
    }
}
