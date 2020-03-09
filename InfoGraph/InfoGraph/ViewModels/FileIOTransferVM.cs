using InfoGraph.Services;
using InfoGraphModel;
using System;
using System.Collections.Generic;
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
        }

        public Table CurrentTable { get; set; }

        public Command SelectFileCommand
        {
            get
            {
                return _selectFileCommand ?? (_selectFileCommand = new Command(SelectFile));
            }
        }
        private Command _selectFileCommand;

        public Command SaveTableCommand
        {
            get
            {
                return _saveTableCommand ?? (_saveTableCommand = new Command(SaveTable));
            }
        }
        private Command _saveTableCommand;

        public override void Init(Table table)
        {
            CurrentTable = table;
        }

        private void SaveTable()
        {
            _fileService.PersistTable(CurrentTable);
        }
    
        private async void SelectFile()
        {
            var table = await _fileService.PickTableByDialogue();
        }
    }
}
