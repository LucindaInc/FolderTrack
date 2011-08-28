namespace FolderTrackGuiTest1.AllVersionsTab
{
    partial class VersionMiniDesigner
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
            this.VersionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.ChangeFileListBox = new System.Windows.Forms.ListBox();
            this.UserVersionListBox = new System.Windows.Forms.ListBox();
            this.DiscriptionTextBox = new System.Windows.Forms.TextBox();
            this.SaveDiscriptionButton = new System.Windows.Forms.Button();
            this.AddUserVersionButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.UseButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.Exploreutton = new System.Windows.Forms.Button();
            this.DateLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VersionTableLayoutPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // VersionTableLayoutPanel
            // 
            this.VersionTableLayoutPanel.AutoSize = true;
            this.VersionTableLayoutPanel.BackColor = System.Drawing.Color.White;
            this.VersionTableLayoutPanel.ColumnCount = 5;
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.VersionTableLayoutPanel.Controls.Add(this.VersionLabel, 0, 0);
            this.VersionTableLayoutPanel.Controls.Add(this.ChangeFileListBox, 0, 2);
            this.VersionTableLayoutPanel.Controls.Add(this.UserVersionListBox, 0, 3);
            this.VersionTableLayoutPanel.Controls.Add(this.DiscriptionTextBox, 3, 3);
            this.VersionTableLayoutPanel.Controls.Add(this.SaveDiscriptionButton, 3, 4);
            this.VersionTableLayoutPanel.Controls.Add(this.AddUserVersionButton, 0, 4);
            this.VersionTableLayoutPanel.Controls.Add(this.panel2, 4, 1);
            this.VersionTableLayoutPanel.Controls.Add(this.DateLabel, 3, 0);
            this.VersionTableLayoutPanel.Location = new System.Drawing.Point(5, 8);
            this.VersionTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.VersionTableLayoutPanel.Name = "VersionTableLayoutPanel";
            this.VersionTableLayoutPanel.RowCount = 5;
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VersionTableLayoutPanel.Size = new System.Drawing.Size(372, 192);
            this.VersionTableLayoutPanel.TabIndex = 0;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.BackColor = System.Drawing.Color.White;
            this.VersionTableLayoutPanel.SetColumnSpan(this.VersionLabel, 3);
            this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.Location = new System.Drawing.Point(0, 0);
            this.VersionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(175, 16);
            this.VersionLabel.TabIndex = 0;
            this.VersionLabel.Text = "1";
            // 
            // ChangeFileListBox
            // 
            this.ChangeFileListBox.BackColor = System.Drawing.Color.White;
            this.ChangeFileListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VersionTableLayoutPanel.SetColumnSpan(this.ChangeFileListBox, 5);
            this.ChangeFileListBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeFileListBox.FormattingEnabled = true;
            this.ChangeFileListBox.HorizontalScrollbar = true;
            this.ChangeFileListBox.ItemHeight = 14;
            this.ChangeFileListBox.Location = new System.Drawing.Point(3, 57);
            this.ChangeFileListBox.Name = "ChangeFileListBox";
            this.ChangeFileListBox.Size = new System.Drawing.Size(366, 44);
            this.ChangeFileListBox.TabIndex = 3;
            this.ChangeFileListBox.DoubleClick += new System.EventHandler(this.ChangeFileListBox_DoubleClick);
            // 
            // UserVersionListBox
            // 
            this.UserVersionListBox.BackColor = System.Drawing.Color.White;
            this.UserVersionListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VersionTableLayoutPanel.SetColumnSpan(this.UserVersionListBox, 3);
            this.UserVersionListBox.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserVersionListBox.FormattingEnabled = true;
            this.UserVersionListBox.HorizontalScrollbar = true;
            this.UserVersionListBox.ItemHeight = 17;
            this.UserVersionListBox.Location = new System.Drawing.Point(3, 107);
            this.UserVersionListBox.Name = "UserVersionListBox";
            this.UserVersionListBox.Size = new System.Drawing.Size(169, 53);
            this.UserVersionListBox.TabIndex = 2;
            // 
            // DiscriptionTextBox
            // 
            this.DiscriptionTextBox.BackColor = System.Drawing.Color.White;
            this.DiscriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VersionTableLayoutPanel.SetColumnSpan(this.DiscriptionTextBox, 2);
            this.DiscriptionTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiscriptionTextBox.Location = new System.Drawing.Point(178, 107);
            this.DiscriptionTextBox.Multiline = true;
            this.DiscriptionTextBox.Name = "DiscriptionTextBox";
            this.DiscriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DiscriptionTextBox.Size = new System.Drawing.Size(191, 53);
            this.DiscriptionTextBox.TabIndex = 7;
            this.DiscriptionTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // SaveDiscriptionButton
            // 
            this.SaveDiscriptionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveDiscriptionButton.AutoSize = true;
            this.SaveDiscriptionButton.BackColor = System.Drawing.Color.Gainsboro;
            this.VersionTableLayoutPanel.SetColumnSpan(this.SaveDiscriptionButton, 2);
            this.SaveDiscriptionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveDiscriptionButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveDiscriptionButton.Location = new System.Drawing.Point(206, 166);
            this.SaveDiscriptionButton.Name = "SaveDiscriptionButton";
            this.SaveDiscriptionButton.Size = new System.Drawing.Size(135, 23);
            this.SaveDiscriptionButton.TabIndex = 12;
            this.SaveDiscriptionButton.Text = "Edit Description To Save";
            this.SaveDiscriptionButton.UseVisualStyleBackColor = false;
            this.SaveDiscriptionButton.Click += new System.EventHandler(this.SaveDiscriptionButton_Click);
            // 
            // AddUserVersionButton
            // 
            this.AddUserVersionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AddUserVersionButton.AutoSize = true;
            this.AddUserVersionButton.BackColor = System.Drawing.Color.Gainsboro;
            this.VersionTableLayoutPanel.SetColumnSpan(this.AddUserVersionButton, 3);
            this.AddUserVersionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddUserVersionButton.Location = new System.Drawing.Point(36, 166);
            this.AddUserVersionButton.Name = "AddUserVersionButton";
            this.AddUserVersionButton.Size = new System.Drawing.Size(103, 23);
            this.AddUserVersionButton.TabIndex = 13;
            this.AddUserVersionButton.Text = "Edit User Versions";
            this.AddUserVersionButton.UseVisualStyleBackColor = false;
            this.AddUserVersionButton.Click += new System.EventHandler(this.AddUserVersionButton_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.VersionTableLayoutPanel.SetColumnSpan(this.panel2, 5);
            this.panel2.Controls.Add(this.UseButton);
            this.panel2.Controls.Add(this.CopyButton);
            this.panel2.Controls.Add(this.Exploreutton);
            this.panel2.Location = new System.Drawing.Point(79, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(213, 32);
            this.panel2.TabIndex = 17;
            // 
            // UseButton
            // 
            this.UseButton.BackColor = System.Drawing.Color.Gainsboro;
            this.UseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UseButton.Location = new System.Drawing.Point(3, 3);
            this.UseButton.Name = "UseButton";
            this.UseButton.Size = new System.Drawing.Size(69, 23);
            this.UseButton.TabIndex = 9;
            this.UseButton.Text = "Use";
            this.UseButton.UseVisualStyleBackColor = false;
            this.UseButton.Click += new System.EventHandler(this.UseButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.BackColor = System.Drawing.Color.Gainsboro;
            this.CopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CopyButton.Location = new System.Drawing.Point(69, 3);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(69, 23);
            this.CopyButton.TabIndex = 10;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = false;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // Exploreutton
            // 
            this.Exploreutton.BackColor = System.Drawing.Color.Gainsboro;
            this.Exploreutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Exploreutton.Location = new System.Drawing.Point(137, 3);
            this.Exploreutton.Name = "Exploreutton";
            this.Exploreutton.Size = new System.Drawing.Size(69, 23);
            this.Exploreutton.TabIndex = 11;
            this.Exploreutton.Text = "Explore";
            this.Exploreutton.UseVisualStyleBackColor = false;
            this.Exploreutton.Click += new System.EventHandler(this.Exploreutton_Click);
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.BackColor = System.Drawing.Color.White;
            this.VersionTableLayoutPanel.SetColumnSpan(this.DateLabel, 2);
            this.DateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLabel.Location = new System.Drawing.Point(175, 0);
            this.DateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(197, 16);
            this.DateLabel.TabIndex = 1;
            this.DateLabel.Text = "Wed, Sep 02 90 5:45:34PM\r\n";
            this.DateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.VersionTableLayoutPanel);
            this.panel1.Location = new System.Drawing.Point(12, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 208);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // VersionMiniDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 737);
            this.Controls.Add(this.panel1);
            this.Name = "VersionMiniDesigner";
            this.Text = "Form2";
            this.VersionTableLayoutPanel.ResumeLayout(false);
            this.VersionTableLayoutPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel VersionTableLayoutPanel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.ListBox UserVersionListBox;
        private System.Windows.Forms.ListBox ChangeFileListBox;
        private System.Windows.Forms.TextBox DiscriptionTextBox;
        private System.Windows.Forms.Button SaveDiscriptionButton;
        private System.Windows.Forms.Button AddUserVersionButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button UseButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button Exploreutton;

    }
}