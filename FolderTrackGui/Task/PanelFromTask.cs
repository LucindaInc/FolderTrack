using System;
using System.Collections.Generic;
using System.Text;

namespace FolderTrackGuiTest1.Task
{
    public class PanelFromTask : PanelList<FolderTrack.Types.Task>.PanelData
    {
        #region PanelData Members

        public System.Windows.Forms.Panel getPanel(FolderTrack.Types.Task data)
        {
            return new TaskRow(data);
        }

        #endregion
    }
}
