using System;
using System.Collections.Generic;
using System.Linq;

namespace InfoGraphModel
{
    public class GraphData
    {
        public delegate void GraphDataPropertyChangedDelegate(GraphPointCoordinatesUpdated updateobject);

        public event GraphDataPropertyChangedDelegate GraphDataPropertyChanged;
        public GraphData(Table table)
        {
            Table = table;
            TableValues.ForEach(graphTableCell => graphTableCell.GraphTablePropertyChanged += GraphTableCell_GraphTablePropertyChanged);
            GraphValues = new List<GraphCell>();
            Width = table.Width;
        }

        private void GraphTableCell_GraphTablePropertyChanged(GraphTableCellPropertyUpdated updateobject)
        {
            if(updateobject is GraphTableCellIsSelectedUpdate)
            {
                GraphTableCellIsSelectedUpdate isSelectedChangedEvent = (GraphTableCellIsSelectedUpdate) updateobject;
                SelectCell(isSelectedChangedEvent.TableCell);
            }
        }

        public List<Tuple<int, int>> PointCoordinates 
        { 
            get
            {
                if (GraphValues != null)
                {
                    return GraphValues.Select(x => new Tuple<int, int>(x.PositionX, x.GraphTableCell.Value)).ToList();
                }
                else
                {
                    return null;
                }
            }
        }
        public List<GraphCell> GraphValues { get; set; }
        public List<TableCell> TableValues => Table.TableValues;
        public int Width { get; set; }

        public Table Table { get; set; }

        public void SelectCell(TableCell cell)
        {
            if (cell.IsSelected)
            {
                var position = GraphValues.Count;
                GraphValues.Add(new GraphCell(position, cell));
                GraphDataPropertyChanged?.Invoke(new GraphPointCoordinatesUpdated());
            }
            else
            {
                GraphValues.Remove(GraphValues.Where(x => x.GraphTableCell.Equals(cell)).Single());
                for (int i = 0; i < GraphValues.Count(); i++)
                {
                    var currentGraphCell = GraphValues.ElementAt(i);
                    currentGraphCell.PositionX = i;
                }
                GraphDataPropertyChanged?.Invoke(new GraphPointCoordinatesUpdated());
            }
        }
    }

    public abstract class GraphDataUpdated
    {
    }

    public class GraphPointCoordinatesUpdated : GraphDataUpdated
    {
    }
}
