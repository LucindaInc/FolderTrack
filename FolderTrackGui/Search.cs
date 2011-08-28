using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Text.RegularExpressions;

namespace FolderTrackGuiTest1
{
    public partial class Search : Form
    {
        DataManager m_DataManager;

        SearchDataTable shearch;

        public List<VersionInfo> SeachData;

        public Search()
        {
            InitializeComponent();
        }

        public class VersionRow : DataGridViewRow
        {
            public VersionRow(VersionInfo versioninfo)
            {
                object[] ObjectCl = new object[7];
                ObjectCl[0] = "a";
                ObjectCl[1] = "b";
                ObjectCl[2] = "c";
                ObjectCl[3] = "d";
                ObjectCl[4] = "e";
                ObjectCl[5] = "f";
                ObjectCl[6] = "g";

                SetValues(ObjectCl);
            }
        }

        public Search(DataManager datamanager)
        {
            InitializeComponent();
            m_DataManager = datamanager;

            SeachData = new List<VersionInfo>();
            VersionInfo versioninfo = new VersionInfo();
            versioninfo.versionName = "Test Version Name";
            versioninfo.date = DateTime.Now;
            versioninfo.FreeText = "Test Free Text";
            versioninfo.UserVersName = new List<string>();
            versioninfo.UserVersName.Add("VersionName1");
            versioninfo.UserVersName.Add("VersionName2");
            SeachData.Add(versioninfo);
          //  this.dataGridView1.Rows.Add();
         //   d this.dataGridView1.Rows[0];
           shearch = new SearchDataTable(datamanager, this.dataGridView1);

            this.dataGridView1.DataSource = shearch;
            
            Setu();
        }



        public void Setu()
        {
            foreach (string user in m_DataManager.getUserVersions())
            {
                this.UserVersionsComboBox.Items.Add(user);
            }
            this.UserVersionsComboBox.AutoCompleteSource= AutoCompleteSource.ListItems;
            this.UserVersionsComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;

        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
        }

        private void UserVersSearchButton_Click(object sender, EventArgs e)
        {
            foreach (VersionInfo v in m_DataManager.VersionList)
            {
                if (Matches(this.VersionTextBox.Text,v.versionName))
                {
                    shearch.addVersion(v);
                }
            }
        }

        public static bool Matches(string pat, string test)
        {
            string[] toks = pat.Split('*');

            int start = 0;

            foreach (string tern in toks)
            {
                start = test.IndexOf(tern, start);
                if (start == -1)
                {
                    return false;
                }
            }

            return true;
        }

        private void versionInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void ShowDatePickerButton_Click(object sender, EventArgs e)
        {
            VersionInfoSearchCriteria vers = new VersionInfoSearchCriteria();
            vers.AddMonth(7,false);
            List<VersionInfo> lis = m_DataManager.VersionInfoFromCriteria(vers);
            foreach (VersionInfo v in lis)
            {
                shearch.addVersion(v);
            }
        }

        private void UserVersionsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            VersionInfoSearchCriteria vers = new VersionInfoSearchCriteria();
            vers.AddUserVersion((string) this.UserVersionsComboBox.SelectedItem,false);
            List<VersionInfo> lis = m_DataManager.VersionInfoFromCriteria(vers);
            foreach (VersionInfo v in lis)
            {
                shearch.addVersion(v);
            }
        }

        private void ChangeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            VersionInfoSearchCriteria vers = new VersionInfoSearchCriteria();
            vers.ChangeTypes.Add(ChangeType.Change);
            List<VersionInfo> lis = m_DataManager.VersionInfoFromCriteria(vers);
            foreach (VersionInfo v in lis)
            {
                shearch.addVersion(v);
            }
        }

        private void ChangeFileButton_Click(object sender, EventArgs e)
        {
            VersionInfoSearchCriteria vers = new VersionInfoSearchCriteria();
            SearchFileList list = new SearchFileList(m_DataManager);
            list.ShowDialog();
            vers.ChangeFile = list.Selected;
            List<VersionInfo> lis = m_DataManager.VersionInfoFromCriteria(vers);
            foreach (VersionInfo v in lis)
            {
                shearch.addVersion(v);
            }

        }

        private void NoNotesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.NoNotesRadioButton.Checked == true)
            {
                List<VersionInfo> lis = m_DataManager.VersionList;
                foreach (VersionInfo v in lis)
                {
                    if (v.FreeText == null)
                    {
                        shearch.addVersion(v);
                    }
                }
            }
        }

        private void HasNoteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.HasNoteRadioButton.Checked == true)
            {
                foreach (KeyValuePair<string,List< VersionInfo>> v in m_DataManager.VersionInfoFromNotes)
                {
                        foreach (VersionInfo vi in v.Value)
                        {
                            shearch.addVersion(vi);
                        }
                }
            }
        }

        private void FilterNoteRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.FilterNoteRadioButton.Checked == true)
            {
                foreach (KeyValuePair<string, List<VersionInfo>> v in m_DataManager.VersionInfoFromNotes)
                {
                    if (Matches(this.FreetextTextBox3.Text, v.Key))
                    {
                        foreach (VersionInfo vi in v.Value)
                        {
                            shearch.addVersion(vi);
                        }
                    }
                }
            }
        }


    }
}