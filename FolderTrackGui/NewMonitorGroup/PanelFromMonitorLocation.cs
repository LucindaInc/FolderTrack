using System;
using System.Collections.Generic;
using System.Text;

namespace FolderTrackGuiTest1.NewMonitorGroup
{
    class PanelFromMonitorLocation: PanelList<string>.PanelData
    {

        public NewMonitorGroupForm.NewLocationManger NewLocationManger;
        #region PanelData Members


        public System.Windows.Forms.Panel getPanel(string data)
        {
            return new NewMonitorLocationRow(data, NewLocationManger);
        }

        #endregion
    }
}
