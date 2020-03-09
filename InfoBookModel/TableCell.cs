using System;
using System.Collections.Generic;
using System.Text;

namespace InfoGraphModel
{
    public class TableCell
    {
        public delegate void GraphTablePropertyChangedDelegate(GraphTableCellPropertyUpdated updateobject);

        public event GraphTablePropertyChangedDelegate GraphTablePropertyChanged;
        public TableCell(int value, int position)
        {
            Value = value;
            Position = position;
        }

        public int Position { get; set; }
        public int Value { get; set; }
        public bool IsSelected
        {
            get => isSelected;

            set
            {
                isSelected = value;
                GraphTablePropertyChanged?.Invoke(new GraphTableCellIsSelectedUpdate(this));
            }
        }
        private bool isSelected;
    }

    public abstract class GraphTableCellPropertyUpdated
    {
    }

    public class GraphTableCellIsSelectedUpdate : GraphTableCellPropertyUpdated
    {
        public GraphTableCellIsSelectedUpdate(TableCell tableCell)
        {
            this.TableCell = tableCell;
        }
        public TableCell TableCell { get; set; }
    }
}
