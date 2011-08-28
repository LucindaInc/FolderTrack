using System;
using System.Collections.Generic;
using System.Text;

namespace FolderTrackGuiTest1.CopyVersionDialog
{
    class PanelFromMonitorGroupLocation : PanelList<String>.PanelData
    {
        public Dictionary<String, String> CopyLocFromExtLoc;

        

        #region PanelData Members

        public System.Windows.Forms.Panel getPanel(string data)
        {
            return new MonitorLocationCopyRow(data, CopyLocFromExtLoc);
        }

        #endregion
    }
}
