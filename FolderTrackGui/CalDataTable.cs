using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace FolderTrackGuiTest1
{
    class CalDataTable : DataTable
    {
        public CalDataTable()
        {
            Columns.Add("Date");
            Columns.Add("Version");
            Columns.Add("Notes");
        }
    }
}
