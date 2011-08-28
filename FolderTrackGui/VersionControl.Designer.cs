namespace FolderTrackGuiTest1
{
    partial class VersionControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VersionPanel = new System.Windows.Forms.Panel();
            this.CopyLabel = new System.Windows.Forms.Label();
            this.DetailsLabel = new System.Windows.Forms.Label();
            this.FreeTextBox = new System.Windows.Forms.TextBox();
            this.DateLabel = new System.Windows.Forms.Label();
            this.UseButton = new System.Windows.Forms.Button();
            this.ExploreButton = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.VersionLabelBig = new System.Windows.Forms.Label();
            this.VersionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // VersionPanel
            // 
            this.VersionPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.VersionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VersionPanel.Controls.Add(this.CopyLabel);
            this.VersionPanel.Controls.Add(this.DetailsLabel);
            this.VersionPanel.Controls.Add(this.FreeTextBox);
            this.VersionPanel.Controls.Add(this.DateLabel);
            this.VersionPanel.Controls.Add(this.UseButton);
            this.VersionPanel.Controls.Add(this.ExploreButton);
            this.VersionPanel.Controls.Add(this.VersionLabel);
            this.VersionPanel.Controls.Add(this.VersionLabelBig);
            this.VersionPanel.Location = new System.Drawing.Point(79, 82);
            this.VersionPanel.Name = "VersionPanel";
            this.VersionPanel.Size = new System.Drawing.Size(150, 150);
            this.VersionPanel.TabIndex = 0;
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
            // FreeTextBox
            // 
            this.FreeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FreeTextBox.Enabled = false;
            this.FreeTextBox.Location = new System.Drawing.Point(0, 96);
            this.FreeTextBox.Multiline = true;
            this.FreeTextBox.Name = "FreeTextBox";
            this.FreeTextBox.Size = new System.Drawing.Size(148, 32);
            this.FreeTextBox.TabIndex = 5;
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
            // UseButton
            // 
            this.UseButton.Location = new System.Drawing.Point(90, 53);
            this.UseButton.Name = "UseButton";
            this.UseButton.Size = new System.Drawing.Size(52, 23);
            this.UseButton.TabIndex = 3;
            this.UseButton.Text = "Use";
            this.UseButton.UseVisualStyleBackColor = true;
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
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(61, 30);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(35, 13);
            this.VersionLabel.TabIndex = 1;
            this.VersionLabel.Text = "label1";
            // 
            // VersionLabelBig
            // 
            this.VersionLabelBig.AutoSize = true;
            this.VersionLabelBig.Location = new System.Drawing.Point(58, 12);
            this.VersionLabelBig.Name = "VersionLabelBig";
            this.VersionLabelBig.Size = new System.Drawing.Size(35, 13);
            this.VersionLabelBig.TabIndex = 0;
            this.VersionLabelBig.Text = "label1";
            // 
            // VersionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VersionPanel);
            this.Name = "VersionControl";
            this.Size = new System.Drawing.Size(308, 314);
            this.VersionPanel.ResumeLayout(false);
            this.VersionPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel VersionPanel;
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
