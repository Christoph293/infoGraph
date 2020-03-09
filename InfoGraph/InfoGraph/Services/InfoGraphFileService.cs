using InfoGraphModel;
using Newtonsoft.Json;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace InfoGraph.Services
{
    public class InfoGraphFileService : IInfoGraphFileService
    {
        public async Task<Table> PickTableByDialogue()
        {
            var fileData = await CrossFilePicker.Current.PickFile();
            if (fileData == null)
                return null; //user canceled file picking

            Table table = (Table) JsonConvert.DeserializeObject(fileData.GetStream().ToString());

            return table;
        }

        public void PersistTable(Table table)
        {
            var tableJsoned = JsonConvert.SerializeObject(table);
            File.WriteAllText(table.Name, tableJsoned);
        }
    }
}
