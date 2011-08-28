using System;
using System.Collections.Generic;
using System.Text;
using FolderTrack.Types;
using FolderTrackGuiTest1.Delete;

namespace FolderTrackGuiTest1.AllVersionsTab
{
    class PanelFromVersionInfo : PanelList<VersionInfo>.PanelData
    {

        private FTObjects m_FTObjects;
        private UnRemove m_Unremove;

        public PanelFromVersionInfo(FTObjects ftobjects)
        {
            this.m_FTObjects = ftobjects;
            this.m_Unremove = null;
        }

        public PanelFromVersionInfo(FTObjects ftobjects, UnRemove unremove)
        {
            this.m_FTObjects = ftobjects;
            this.m_Unremove = unremove;
        }

        #region PanelData Members

        public System.Windows.Forms.Panel getPanel(VersionInfo data)
        {
            if (m_Unremove == null)
            {

                return new VersionRow(data, m_FTObjects);
            }
            else
            {
                return new VersionRow(data, m_FTObjects, m_Unremove);
            }
        }

        #endregion
    }
}
