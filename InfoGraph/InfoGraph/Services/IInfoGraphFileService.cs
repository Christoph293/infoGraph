using InfoGraphModel;
using System.Collections.Generic;

namespace InfoGraph.Services
{
    public interface IInfoGraphFileService
    {
        Table LoadTableByPath(string path);
        void PersistTable(Table table);
        IEnumerable<string> GetExistingFilesInAppDir();
    }
}
