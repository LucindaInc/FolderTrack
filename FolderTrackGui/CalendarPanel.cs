using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;

namespace FolderTrackGuiTest1
{
    public partial class CalendarPanel : TableLayoutPanel
    {
        public CalendarPanel(VersionInfo vers)
        {
            InitializeComponent();
            this.DateLabel.Text = vers.date.ToLongDateString();
            this.TimeLabel.Text = vers.date.ToLongTimeString();
            this.VersionsListBox.Items.Add(vers.versionName);
            if (vers.userVersName != null)
            {
                foreach (string v in vers.userVersName)
                {
                    this.VersionsListBox.Items.Add(v);
                }
            }
            
        }
    }
}
