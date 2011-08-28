using System;
using System.Collections.Generic;
using System.Text;
using FolderTrack.Types;

namespace FolderTrackGuiTest1.MonitorGroupChoser
{
    class PanelFromMonitorGInfo: PanelList<MonitorGroupInfo>.PanelData
    {
        #region PanelData Members

        public MainForm mainfor;

        public System.Windows.Forms.Panel getPanel(MonitorGroupInfo data)
        {
            return new MonitorGroupRow(data, mainfor);
        }

        #endregion
    }
}
