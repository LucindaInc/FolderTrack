using System;
using System.Collections.Generic;
using System.Text;
using FolderTrack.ExclusionRules;

namespace FolderTrackGuiTest1.MGProperties
{
    class MGProRowFromMGObject : PanelList<GuiInfoMGProperties>.PanelData
    {

        #region PanelData Members

        public System.Windows.Forms.Panel getPanel(GuiInfoMGProperties data)
        {
            return new MGProRow(data);
        }


        #endregion
    }
}
