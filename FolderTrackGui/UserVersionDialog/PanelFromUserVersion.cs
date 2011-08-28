using System;
using System.Collections.Generic;
using System.Text;

namespace FolderTrackGuiTest1.UserVersionDialog
{
    public class PanelFromUserVersion : PanelList<string>.PanelData
    {
        public UserVersionForm m_UserVersionForm;

        public System.Windows.Forms.Panel getPanel(string data)
        {
            return new StopUserVersionRow(data, m_UserVersionForm);
        }

    }
}
