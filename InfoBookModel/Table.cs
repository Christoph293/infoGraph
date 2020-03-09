using System;
using System.Collections.Generic;
using System.Text;

namespace InfoGraphModel
{
    public class Table
    {
        public Table(string name, List<TableCell> tableValues, int width)
        {
            Name = name;
            TableValues = tableValues;
            Width = width;
        }

        public string Name { get; set; }
        public List<TableCell> TableValues { get; set; }
        public int Width { get; set; }
    }
}
