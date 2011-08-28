using System;
using System.Collections.Generic;
using System.Text;
using FolderTrack.Types;

namespace FolderTrackGuiTest1.ExplorerTab
{
    class PanelFromChangeInstruction : PanelList<ChangeInstruction>.PanelData
    {
        #region PanelData Members

        public FTObjects ftobject;
        public ExplorerTab tab;

        public System.Windows.Forms.Panel getPanel(ChangeInstruction data)
        {
            return new ChangeRow(data, ftobject, tab);
        }

        #endregion
    }
}
