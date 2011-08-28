using System;
using System.Collections.Generic;
using System.Text;
using FolderTrack.Types;
using FolderTrackGuiTest1.VersionPanels;

namespace FolderTrackGuiTest1.SearchTab
{
    class PanelFromVersionInfo : PanelList<VersionInfo>.PanelData
    {
        private FTObjects m_FTObjects;

        public PanelFromVersionInfo(FTObjects ftobjects)
        {
            this.m_FTObjects = ftobjects;
        }

        #region PanelData Members

        public System.Windows.Forms.Panel getPanel(VersionInfo data)
        {
            return new VersionMini(data, m_FTObjects);
        }

        #endregion
    }
}
