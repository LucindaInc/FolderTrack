using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FolderTrackGuiTest1.CopyVersionDialog
{
    public partial class CopyVersionForm : Form
    {
        PanelList<String> MonitorLocationsPanelList;

        public Dictionary<String, String> CopyLocFromExtLoc;

        public CopyVersionForm()
        {
            InitializeComponent();
        }

        public CopyVersionForm(IList<String> MonitorGroupLocations)
        {
            InitializeComponent();
            CopyLocFromExtLoc = new Dictionary<string,string>();
            MonitorLocationsPanelList = new PanelList<string>();
            this.MonitorLocationsPanel.Controls.Add(MonitorLocationsPanelList);
            PanelFromMonitorGroupLocation pan = new PanelFromMonitorGroupLocation();
            pan.CopyLocFromExtLoc = CopyLocFromExtLoc;
            MonitorLocationsPanelList.PanelFromData = pan;
            MonitorLocationsPanelList.AddData(MonitorGroupLocations);
            this.DialogResult = DialogResult.Cancel;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}