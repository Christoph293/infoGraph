using InfoGraphModel;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace InfoGraph.Services
{
    public class InfoGraphFileService : IInfoGraphFileService
    {
        readonly string filePrefix = "IGTable_";
 

        public void PersistTable(Table table)
        {
            var fileDir = Path.Combine(FileSystem.AppDataDirectory, String.Concat(filePrefix, table.Name));
            var tableJsoned = JsonConvert.SerializeObject(table);
            File.WriteAllText(fileDir, tableJsoned);
        }

        public IEnumerable<string> GetExistingFilesInAppDir()
        {
            var appDir = FileSystem.AppDataDirectory;
            var files = Directory.GetFiles(appDir, String.Concat(filePrefix,"*"));
            return files;
        }

        public Table LoadTableByPath(string path)
        {
            var fileTxt = File.ReadAllText(path);
            Table table = JsonConvert.DeserializeObject<Table>(fileTxt);

            return table;
        }
    }
}
