using System;
using System.Collections.Generic;
using System.Text;

namespace InfoGraphModel
{
    public class GraphCell
    {
        public GraphCell(int positionX, TableCell graphTableCell)
        {
            PositionX = positionX;
            GraphTableCell = graphTableCell;
        }
        public int PositionX { get; set; }
        public TableCell GraphTableCell { get; set; }
    }
}
