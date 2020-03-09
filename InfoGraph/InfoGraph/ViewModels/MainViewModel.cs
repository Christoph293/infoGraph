using InfoGraphModel;
using InfoGraph.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;

namespace InfoGraph.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private GraphData _graphData;

        public MainViewModel(INavService navService)
            :base(navService)
        {
            var graphTableValues = new List<TableCell>(){ new TableCell(10,1), new TableCell(4, 2), new TableCell(10, 3) ,
                new TableCell(14,4),new TableCell(7,5),new TableCell(2,6),new TableCell(5,7),new TableCell(22,8),new TableCell(31,9),
                new TableCell(22,10),new TableCell(1,11),new TableCell(5,12),new TableCell(7,13),new TableCell(15,14),new TableCell(78,15),new TableCell(2,16),
                new TableCell(101,17),new TableCell(200,18),new TableCell(71,19),new TableCell(5,20),new TableCell(33,21),new TableCell(98,22),new TableCell(0,23),
                new TableCell(1005,24)};
            var table = new Table("DefaultTable", graphTableValues, 4);
            _graphData = new GraphData(table);
            _graphData.GraphDataPropertyChanged += GraphData_GraphDataPropertyChanged;
            TableValues = _graphData.TableValues.Select(x => new GraphTableCellViewModel(x)).ToList();
            OnPropertyChanged(nameof(GraphCoordinates));
            OnPropertyChanged(nameof(WidthOfTable));
            OnPropertyChanged(nameof(TableValues));
        }

        private void GraphData_GraphDataPropertyChanged(GraphPointCoordinatesUpdated updateobject)
        {
            OnPropertyChanged("GraphCoordinates");
        }

        public Command NavigateToWebCommand
        {
            get
            {
                return _navigateToWebCommand ?? (_navigateToWebCommand = new Command(async () => await NavService.NavigateTo<WebTransferVM>()));
            }
        }

        private Command _navigateToWebCommand;

        public Command NavigateToFileIOCommand
        {
            get
            {
                return _navigateToFileIOCommand ?? (_navigateToFileIOCommand = new Command(async () => await NavService.NavigateTo<FileIOTransferVM, Table>(_graphData.Table)));
            }
        }
        private Command _navigateToFileIOCommand;

        public List<Tuple<int,int>> GraphCoordinates
        {
            get
            {
                return _graphData?.PointCoordinates;
            }
        }

        public int WidthOfTable
        {
            get
            {
                return _graphData == null ? 0: _graphData.Width;
            }
        }

        public List<GraphTableCellViewModel> TableValues
        {
            get
            {
                return _tableValues;
            }
            set
            {
                _tableValues = value;
            }
        }
        private List<GraphTableCellViewModel> _tableValues;
    }
}
