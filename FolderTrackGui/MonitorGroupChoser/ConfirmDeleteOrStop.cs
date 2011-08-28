using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FolderTrackGuiTest1.MonitorGroupChoser
{
    public partial class ConfirmDeleteOrStop : Form
    {
        public enum CONFTY
        {
            STOP,
            DELETE
        };

        public ConfirmDeleteOrStop()
        {
            InitializeComponent();
        }

        public ConfirmDeleteOrStop(CONFTY cont)
        {
            this.DialogResult = DialogResult.Cancel;
            InitializeComponent();

            if (cont == CONFTY.DELETE)
            {
                this.Discriplabel.Text = "If you delete all of the monitor group" +
                                    " history will be lost. To just stop FolderTrack" +
                                    " from monitoring click Cancel on this dialog and" +
                                    " select Stop. If you are sure you want to delete" +
                                    " click Confirm Delete";
                this.ConfirButton.Text = "Confirm Delete";
            }
            else
            {
                this.Discriplabel.Text = "If you stop monitoring FolderTrack will no longer" +
                                    " record new changes. Also FolderTrack will not revert" +
                                    " your monitorgroup to previous versions." +
                                    " If you are sure you want to stop click Confirm Stop otherwise" +
                                    " click Cancel";
                this.ConfirButton.Text = "Confirm Stop";
            }


        }

        private void ConfirButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}