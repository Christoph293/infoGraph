using InfoGraphModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace InfoGraph.ViewModels
{
    public class GraphTableCellViewModel : BaseViewModel
    {
        private TableCell _tableCell;

        public GraphTableCellViewModel(TableCell tableCell)
        {
            _tableCell = tableCell;
        }

        public Command SelectCellCommand
        {
            get
            {
                return _selectCellCommand ?? (_selectCellCommand = new Command(() => SelectCell()));
            }
        }
        private Command _selectCellCommand;

        public Command ChangeToEditModeCommand
        {
            get
            {
                return _changeToEditModeCommand ?? (_changeToEditModeCommand = new Command(() => ChangeToEditMode()));
            }
        }
        private Command _changeToEditModeCommand;

        public int Value
        {
            get 
            {
                return _tableCell != null ? _tableCell.Value: 0; 
            }
            set
            {
                _tableCell.Value = value;
            }
        }

        public bool IsSelected
        {
            get
            {
                return _tableCell.IsSelected;
            }
            set
            {
                _tableCell.IsSelected = value;
                OnPropertyChanged();
            }
        }

        public bool IsTextEditable
        {
            get
            {
                return _isTextEditable;
            }
            set
            {
                _isTextEditable = value;
                OnPropertyChanged();
            }
        }
        private bool _isTextEditable;

        private void SelectCell()
        {
            if (IsSelected)
            {
                IsSelected = false;
            }
            else
            {
                IsSelected = true;
            }
        }

        private void ChangeToEditMode()
        {
            throw new NotImplementedException();
        }

    }
}
