using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ZetaLongPaths;

namespace FolderTrackGuiTest1
{
    public partial class SearchFileList : Form
    {
        DataManager m_DataManager;
        string [] Files;
        public List<string> Selected;

        public SearchFileList()
        {
            InitializeComponent();
        }

        public SearchFileList(DataManager datamanager)
        {
            InitializeComponent();
            this.m_DataManager = datamanager;
            Selected = new List<string>();
            this.fileListView.VirtualListSize = m_DataManager.VersionInfoFromChangeFile.Keys.Count;
            Files = new string[m_DataManager.VersionInfoFromChangeFile.Keys.Count];
            m_DataManager.VersionInfoFromChangeFile.Keys.CopyTo(Files, 0);
        }

        private void SearchFileList_Load(object sender, EventArgs e)
        {

        }

        private void fileListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(ZlpPathHelper.GetFileNameFromFilePath(Files[e.ItemIndex]));
            e.Item.SubItems.Add(Files[e.ItemIndex]);
        }

        private void fileListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected == true)
            {
                Selected.Add(e.Item.SubItems[1].Text);
            }
            else
            {
                Selected.Remove(e.Item.SubItems[1].Text);
            }
        }
    }
}