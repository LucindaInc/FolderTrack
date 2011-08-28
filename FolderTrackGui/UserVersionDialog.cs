using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FolderTrackGuiTest1
{
    public partial class UserVersionDialog : Form
    {
        public string UserVersion;
        public List<string> StopVersions;

        public UserVersionDialog()
        {
            InitializeComponent();
            DialogResult = DialogResult.Abort;
            StopVersions = new List<string>();
        }

        public void setStopVersions(List<string> StopVers)
        {
            foreach (string vers in StopVers)
            {
                this.StopUserVersionsListBox.Items.Add(vers);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (this.UserVersionTextBox.Text.Length > 0)
            {
                UserVersion = this.UserVersionTextBox.Text;
            }
            else
            {
                UserVersion = null;
            }
            this.Close();
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            if (this.UserVersionTextBox.Text.Length > 0)
            {
                UserVersion = this.UserVersionTextBox.Text;
            }
            else
            {
                UserVersion = null;
            }
            this.Close();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selected = this.StopUserVersionsListBox.SelectedItems;
            foreach (object sel in selected)
            {
                StopVersions.Add(sel as string);
            }
            
        }


    }
}