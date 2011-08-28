using System;
using System.Collections.Generic;
using System.Text;

namespace FolderTrackGuiTest1.Task
{
    class PanelFromTaskGroup : PanelList<FolderTrack.Types.TaskGroup>.PanelData
    {

        public PanelFromTaskGroup(FTObjects m_ftopb)
        {
            this.m_ftopb = m_ftopb;
        }
        private FTObjects m_ftopb;

        #region PanelData Members

        public System.Windows.Forms.Panel getPanel(FolderTrack.Types.TaskGroup data)
        {
            return new TaskGroupRow(data, m_ftopb);
        }

        #endregion
    }
}
