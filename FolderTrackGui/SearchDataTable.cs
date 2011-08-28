using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using FolderTrack.Types;

namespace FolderTrackGuiTest1
{
    class SearchDataTable : DataTable
    {
        DataGridView m_DataGridView;
        DataManager m_DataManager;

        Dictionary<VersionInfo, DataRow> VersionInfoFromDataRow;

        public SearchDataTable()
        {
            Columns.Add("Date");
            Columns.Add("Version");
            Columns.Add("Notes");
        }

        public SearchDataTable(DataManager datamanager, DataGridView datagridview)
        {
            Columns.Add("Date");
            Columns.Add("Version");
            Columns.Add("Notes");
            m_DataGridView = datagridview;
            m_DataGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(m_DataGridView_RowsAdded);
            m_DataManager = datamanager;
            VersionInfoFromDataRow = new Dictionary<VersionInfo, DataRow>();
        }

        public void addVersion(VersionInfo version)
        {
            DataRow daterow = this.Rows.Add(version.date, version.versionName, version.freeText);
            VersionInfoFromDataRow[version] = daterow;
            bool sd = daterow.HasErrors;
        }

        public void removeVersion(VersionInfo version)
        {
            DataRow dat;
            VersionInfoFromDataRow.TryGetValue(version, out dat);
            if (dat != null)
            {
                this.Rows.Remove(dat);
                VersionInfoFromDataRow.Remove(version);
            }
        }

        void m_DataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewRow datrow =  m_DataGridView.Rows[e.RowIndex];
            //get the version name and match it and populate coresponding user version and changes
            string versionName = datrow.Cells["Version"].Value as string;
            VersionInfo versioninfo;
            m_DataManager.VersionInfoFromVersionName.TryGetValue(versionName, out versioninfo);
            if (versioninfo.UserVersName != null)
            {
                DataGridViewComboBoxCell co = datrow.Cells["UserVersions"] as DataGridViewComboBoxCell;
                foreach (string userverionname in versioninfo.UserVersName)
                {
                    co.Items.Add(userverionname);
                }
            }
            if (versioninfo.changesInVersion != null)
            {
                DataGridViewComboBoxCell co = datrow.Cells["Changes"] as DataGridViewComboBoxCell;
                foreach (ChangeInstruction changeinstruction in versioninfo.changesInVersion)
                {
                    if (changeinstruction.change != ChangeType.Rename)
                    {
                        co.Items.Add(changeinstruction.change + " " + changeinstruction.externalLocation);
                    }
                    else
                    {
                        co.Items.Add(changeinstruction.change + " " + changeinstruction.folderUnit.oldLocation + " to " + changeinstruction.externalLocation);
                    }
                }
            }


        }

    }
}
