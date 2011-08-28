namespace FolderTrackGuiTest1.CopyVersionDialog
{
    partial class MonitorGroupCopyDesignerForm
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.MonitorGroupCopyPanel = new System.Windows.Forms.Panel();
            this.ExternalLocationLabel = new System.Windows.Forms.Label();
            this.ExternalLocationPathLabel = new System.Windows.Forms.Label();
            this.CopyLocationbutton = new System.Windows.Forms.Button();
            this.CopyLocationTextBox = new System.Windows.Forms.TextBox();
            this.CopyButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MonitorGroupCopyPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MonitorGroupCopyPanel
            // 
            this.MonitorGroupCopyPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MonitorGroupCopyPanel.AutoScroll = true;
            this.MonitorGroupCopyPanel.BackColor = System.Drawing.Color.Cornsilk;
            this.MonitorGroupCopyPanel.Controls.Add(this.CopyButton);
            this.MonitorGroupCopyPanel.Controls.Add(this.CopyLocationTextBox);
            this.MonitorGroupCopyPanel.Controls.Add(this.CopyLocationbutton);
            this.MonitorGroupCopyPanel.Controls.Add(this.ExternalLocationPathLabel);
            this.MonitorGroupCopyPanel.Controls.Add(this.ExternalLocationLabel);
            this.MonitorGroupCopyPanel.Location = new System.Drawing.Point(17, 8);
            this.MonitorGroupCopyPanel.Name = "MonitorGroupCopyPanel";
            this.MonitorGroupCopyPanel.Size = new System.Drawing.Size(622, 135);
            this.MonitorGroupCopyPanel.TabIndex = 1;
            // 
            // ExternalLocationLabel
            // 
            this.ExternalLocationLabel.AutoSize = true;
            this.ExternalLocationLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExternalLocationLabel.Location = new System.Drawing.Point(4, 36);
            this.ExternalLocationLabel.Name = "ExternalLocationLabel";
            this.ExternalLocationLabel.Size = new System.Drawing.Size(109, 16);
            this.ExternalLocationLabel.TabIndex = 0;
            this.ExternalLocationLabel.Text = "External Location";
            // 
            // ExternalLocationPathLabel
            // 
            this.ExternalLocationPathLabel.AutoSize = true;
            this.ExternalLocationPathLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExternalLocationPathLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExternalLocationPathLabel.Location = new System.Drawing.Point(4, 53);
            this.ExternalLocationPathLabel.Name = "ExternalLocationPathLabel";
            this.ExternalLocationPathLabel.Size = new System.Drawing.Size(555, 18);
            this.ExternalLocationPathLabel.TabIndex = 1;
            this.ExternalLocationPathLabel.Text = "C:\\Documents and Settings\\Nicole Willis\\ My Documents\\Projects\\NewYorkNewYork\\Sta" +
                "tue.c";
            // 
            // CopyLocationbutton
            // 
            this.CopyLocationbutton.AutoSize = true;
            this.CopyLocationbutton.BackColor = System.Drawing.Color.BurlyWood;
            this.CopyLocationbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CopyLocationbutton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyLocationbutton.Location = new System.Drawing.Point(4, 74);
            this.CopyLocationbutton.Name = "CopyLocationbutton";
            this.CopyLocationbutton.Size = new System.Drawing.Size(149, 26);
            this.CopyLocationbutton.TabIndex = 2;
            this.CopyLocationbutton.Text = "Choose Copy Location";
            this.CopyLocationbutton.UseVisualStyleBackColor = false;
            this.CopyLocationbutton.Click += new System.EventHandler(this.CopyLocationbutton_Click);
            // 
            // CopyLocationTextBox
            // 
            this.CopyLocationTextBox.BackColor = System.Drawing.Color.Azure;
            this.CopyLocationTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyLocationTextBox.Location = new System.Drawing.Point(4, 105);
            this.CopyLocationTextBox.Name = "CopyLocationTextBox";
            this.CopyLocationTextBox.Size = new System.Drawing.Size(555, 22);
            this.CopyLocationTextBox.TabIndex = 3;
            this.CopyLocationTextBox.TextChanged += new System.EventHandler(this.CopyLocationTextBox_TextChanged);
            // 
            // CopyButton
            // 
            this.CopyButton.AutoSize = true;
            this.CopyButton.BackColor = System.Drawing.Color.BurlyWood;
            this.CopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CopyButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyButton.Location = new System.Drawing.Point(7, 4);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(86, 25);
            this.CopyButton.TabIndex = 4;
            this.CopyButton.Text = "Do Not Copy";
            this.CopyButton.UseVisualStyleBackColor = false;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.MonitorGroupCopyPanel);
            this.panel1.Location = new System.Drawing.Point(12, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(657, 151);
            this.panel1.TabIndex = 2;
            // 
            // MonitorGroupCopyDesignerForm
            // 
            this.ClientSize = new System.Drawing.Size(681, 457);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(57, 112);
            this.Name = "MonitorGroupCopyDesignerForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.MonitorGroupCopyPanel.ResumeLayout(false);
            this.MonitorGroupCopyPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel MonitorGroupCopyPanel;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.TextBox CopyLocationTextBox;
        private System.Windows.Forms.Button CopyLocationbutton;
        private System.Windows.Forms.Label ExternalLocationPathLabel;
        private System.Windows.Forms.Label ExternalLocationLabel;
        private System.Windows.Forms.Panel panel1;
    }
}