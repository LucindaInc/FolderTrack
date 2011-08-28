using System;
using System.Collections.Generic;
using System.Text;

namespace FolderTrackGuiTest1.Filters
{
    class FilterRowFromFilterObject : PanelList<FilterObject>.PanelData
    {

        #region PanelData Members

        public List<string> monitor;
        public List<string> FilterStrings;

        public System.Windows.Forms.Panel getPanel(FilterObject data)
        {
            data.monitor = monitor;
            data.filterStrings = FilterStrings;
            return new FolderRow(data);
        }

        #endregion
    }
}
