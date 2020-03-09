using InfoGraphModel;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfoGraph.Services
{
    public interface IInfoGraphFileService
    {
        Task<Table> PickTableByDialogue();

        void PersistTable(Table table);
    }
}
