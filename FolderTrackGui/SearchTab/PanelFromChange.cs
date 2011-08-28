using System;
using System.Collections.Generic;
using System.Text;

namespace FolderTrackGuiTest1.SearchTab
{
    class PanelFromChange : PanelList<string>.PanelData
    {
        public SearchPanel pan;

        public PanelFromChange(SearchPanel pan)
        {
            this.pan = pan;
        }

        #region PanelData Members

        public System.Windows.Forms.Panel getPanel(string data)
        {
            return new ChangeRow(pan,data);
        }

        #endregion
    }
}
