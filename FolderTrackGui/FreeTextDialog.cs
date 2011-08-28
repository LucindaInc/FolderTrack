using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FolderTrackGuiTest1
{
    public partial class FreeTextDialog : Form
    {
        public string FreeText;

        public FreeTextDialog(string FreeText)
        {
            InitializeComponent();
            this.FreeTextBox.Text = FreeText;
            DialogResult = DialogResult.Abort;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            FreeText = this.FreeTextBox.Text;
            this.Close();
        }

        private void AbortButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            FreeText = this.FreeTextBox.Text;
            this.Close();
        }
        
    }
}