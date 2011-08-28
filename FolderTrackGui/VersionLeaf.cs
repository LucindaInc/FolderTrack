using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;

namespace FolderTrackGuiTest1
{
    class VersionLeaf : Panel
    {

        public VersionInfo m_VersionInfo;

        public VersionLeaf(VersionInfo vers)
        {
            this.m_VersionInfo = vers;
            this.Visible = true;
            InitializeComponent();
            if (m_VersionInfo != null)
            {
                this.VersionLabelBig.Text = m_VersionInfo.versionName;
            }
            else
            {
                this.VersionLabelBig.Text = "Big Version";
            }
            if (m_VersionInfo != null && m_VersionInfo.userVersName != null)
            {
                StringBuilder bil = new StringBuilder();
                foreach (string name in m_VersionInfo.userVersName)
                {
                    bil.Append(name + "\n");
                }
                this.VersionLabel.Text = bil.ToString();
            }
            else
            {
                this.VersionLabel.Text = "Small Version";
            }
        }

        public void setVersionInfo(VersionInfo vers)
        {
            this.m_VersionInfo = vers;
            this.VersionLabelBig.Text = vers.versionName;
            StringBuilder bil = new StringBuilder();
            foreach (string name in m_VersionInfo.userVersName)
            {
                bil.Append(name + "\n");
            }
            this.VersionLabel.Text = bil.ToString();
            this.Visible = true;
        }

        #region Component Designer generated code

        public VersionLeaf()
        {
            InitializeComponent();
        }
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VersionLabelBig = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.ExploreButton = new System.Windows.Forms.Button();
            this.UseButton = new System.Windows.Forms.Button();
            this.DateLabel = new System.Windows.Forms.Label();
            this.FreeTextBox = new System.Windows.Forms.TextBox();
            this.DetailsLabel = new System.Windows.Forms.Label();
            this.CopyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // VersionLabelBig
            // 
            this.VersionLabelBig.AutoSize = true;
            this.VersionLabelBig.Location = new System.Drawing.Point(58, 12);
            this.VersionLabelBig.Name = "VersionLabelBig";
            this.VersionLabelBig.Size = new System.Drawing.Size(0, 13);
            this.VersionLabelBig.TabIndex = 0;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(61, 30);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(0, 13);
            this.VersionLabel.TabIndex = 1;
            // 
            // ExploreButton
            // 
            this.ExploreButton.Location = new System.Drawing.Point(9, 53);
            this.ExploreButton.Name = "ExploreButton";
            this.ExploreButton.Size = new System.Drawing.Size(52, 23);
            this.ExploreButton.TabIndex = 2;
            this.ExploreButton.Text = "Explore";
            this.ExploreButton.UseVisualStyleBackColor = true;
            // 
            // UseButton
            // 
            this.UseButton.Location = new System.Drawing.Point(90, 53);
            this.UseButton.Name = "UseButton";
            this.UseButton.Size = new System.Drawing.Size(52, 23);
            this.UseButton.TabIndex = 3;
            this.UseButton.Text = "Use";
            this.UseButton.UseVisualStyleBackColor = true;
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.Location = new System.Drawing.Point(47, 82);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(56, 13);
            this.DateLabel.TabIndex = 4;
            this.DateLabel.Text = "DateLabel";
            // 
            // FreeTextBox
            // 
            this.FreeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FreeTextBox.Enabled = false;
            this.FreeTextBox.Location = new System.Drawing.Point(0, 97);
            this.FreeTextBox.Multiline = true;
            this.FreeTextBox.Name = "FreeTextBox";
            this.FreeTextBox.Size = new System.Drawing.Size(150, 32);
            this.FreeTextBox.TabIndex = 5;
            // 
            // DetailsLabel
            // 
            this.DetailsLabel.AutoSize = true;
            this.DetailsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DetailsLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.DetailsLabel.Location = new System.Drawing.Point(4, 134);
            this.DetailsLabel.Name = "DetailsLabel";
            this.DetailsLabel.Size = new System.Drawing.Size(39, 13);
            this.DetailsLabel.TabIndex = 6;
            this.DetailsLabel.Text = "Details";
            // 
            // CopyLabel
            // 
            this.CopyLabel.AutoSize = true;
            this.CopyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyLabel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.CopyLabel.Location = new System.Drawing.Point(114, 134);
            this.CopyLabel.Name = "CopyLabel";
            this.CopyLabel.Size = new System.Drawing.Size(31, 13);
            this.CopyLabel.TabIndex = 7;
            this.CopyLabel.Text = "Copy";
            // 
            // VersionLeaf
            // 
            this.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.CopyLabel);
            this.Controls.Add(this.DetailsLabel);
            this.Controls.Add(this.FreeTextBox);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.UseButton);
            this.Controls.Add(this.ExploreButton);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.VersionLabelBig);
            this.Name = "VersionControl";
            this.Size = new System.Drawing.Size(150, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public void setVersion(string version)
        {
            this.VersionLabel.Text = version;
        }

        public void setText(string text)
        {
            this.FreeTextBox.Text = text;
        }

        private System.Windows.Forms.Label VersionLabelBig;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Button UseButton;
        private System.Windows.Forms.Button ExploreButton;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.TextBox FreeTextBox;
        private System.Windows.Forms.Label CopyLabel;
        private System.Windows.Forms.Label DetailsLabel;
    }
    
}
