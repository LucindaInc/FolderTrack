using System;
using System.Collections.Generic;
using System.Text;
using FolderTrack.Types;

namespace FolderTrackGuiTest1.MonitorGroupChoser
{
    class EPanelFromMonitorGInfo : PanelList<MonitorGroupInfo>.PanelData
    {
        #region PanelData Members

        public FTObjects ftobjects;
        public MainForm mainForm;

        public System.Windows.Forms.Panel getPanel(MonitorGroupInfo data)
        {
            return new EMonitorGroupRow(data, ftobjects, mainForm);
        }

        #endregion
    }
}
