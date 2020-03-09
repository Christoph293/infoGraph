using InfoGraphModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfoGraph.Services
{
    public interface IInfoGraphWebService
    {
        Task<IList<Table>> GetTablesAsync();
        Task<Table> AddTableAsync(Table table);
        Task<Table> ChangeTableAsync(Table table);
    }
}
