namespace FolderTrackGuiTest1.UserVersionDialog
{
    partial class StopUserVersionRowDesignerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.UserVersionPanel = new System.Windows.Forms.Panel();
            this.UserVersionLabel = new System.Windows.Forms.Label();
            this.RemoveNewButton = new System.Windows.Forms.Button();
            this.LastButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.UserVersionPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UserVersionPanel
            // 
            this.UserVersionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.UserVersionPanel.BackColor = System.Drawing.Color.LightBlue;
            this.UserVersionPanel.Controls.Add(this.UserVersionLabel);
            this.UserVersionPanel.Controls.Add(this.RemoveNewButton);
            this.UserVersionPanel.Controls.Add(this.LastButton);
            this.UserVersionPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UserVersionPanel.Location = new System.Drawing.Point(10, 7);
            this.UserVersionPanel.Name = "UserVersionPanel";
            this.UserVersionPanel.Size = new System.Drawing.Size(300, 36);
            this.UserVersionPanel.TabIndex = 0;
            // 
            // UserVersionLabel
            // 
            this.UserVersionLabel.AutoSize = true;
            this.UserVersionLabel.Location = new System.Drawing.Point(15, 12);
            this.UserVersionLabel.Name = "UserVersionLabel";
            this.UserVersionLabel.Size = new System.Drawing.Size(35, 13);
            this.UserVersionLabel.TabIndex = 2;
            this.UserVersionLabel.Text = "label1";
            // 
            // RemoveNewButton
            // 
            this.RemoveNewButton.Location = new System.Drawing.Point(202, 6);
            this.RemoveNewButton.Name = "RemoveNewButton";
            this.RemoveNewButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveNewButton.TabIndex = 1;
            this.RemoveNewButton.Text = "Remove Now";
            this.RemoveNewButton.UseVisualStyleBackColor = true;
            // 
            // LastButton
            // 
            this.LastButton.Location = new System.Drawing.Point(114, 6);
            this.LastButton.Name = "LastButton";
            this.LastButton.Size = new System.Drawing.Size(82, 23);
            this.LastButton.TabIndex = 0;
            this.LastButton.Text = "Last Use";
            this.LastButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.UserVersionPanel);
            this.panel1.Location = new System.Drawing.Point(22, 156);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 50);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // StopUserVersionRowDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 266);
            this.Controls.Add(this.panel1);
            this.Name = "StopUserVersionRowDesignerForm";
            this.Text = "StopUserVersionRowDesignerForm";
            this.UserVersionPanel.ResumeLayout(false);
            this.UserVersionPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel UserVersionPanel;
        private System.Windows.Forms.Label UserVersionLabel;
        private System.Windows.Forms.Button RemoveNewButton;
        private System.Windows.Forms.Button LastButton;
        private System.Windows.Forms.Panel panel1;
    }
}