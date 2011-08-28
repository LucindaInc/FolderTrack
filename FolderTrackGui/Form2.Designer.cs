namespace FolderTrackGuiTest1
{
    partial class Form2
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
            this.UserVersionListBox = new System.Windows.Forms.ListBox();
            this.DateLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.UseButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.Exploreutton = new System.Windows.Forms.Button();
            this.AddUserVersionButton = new System.Windows.Forms.Button();
            this.DiscriptionTextBox = new System.Windows.Forms.TextBox();
            this.ChangeFileListBox = new System.Windows.Forms.ListBox();
            this.SaveDiscriptionButton = new System.Windows.Forms.Button();
            this.FilterButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VersionTableLayoutPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // VersionTableLayoutPanel
            // 
            this.VersionTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.VersionTableLayoutPanel.AutoSize = true;
            this.VersionTableLayoutPanel.BackColor = System.Drawing.Color.White;
            this.VersionTableLayoutPanel.ColumnCount = 7;
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.VersionTableLayoutPanel.Controls.Add(this.VersionLabel, 0, 0);
            this.VersionTableLayoutPanel.Controls.Add(this.UserVersionListBox, 1, 2);
            this.VersionTableLayoutPanel.Controls.Add(this.DateLabel, 2, 0);
            this.VersionTableLayoutPanel.Controls.Add(this.panel2, 0, 2);
            this.VersionTableLayoutPanel.Controls.Add(this.AddUserVersionButton, 1, 3);
            this.VersionTableLayoutPanel.Controls.Add(this.DiscriptionTextBox, 3, 2);
            this.VersionTableLayoutPanel.Controls.Add(this.ChangeFileListBox, 0, 1);
            this.VersionTableLayoutPanel.Controls.Add(this.SaveDiscriptionButton, 3, 3);
            this.VersionTableLayoutPanel.Controls.Add(this.FilterButton, 6, 1);
            this.VersionTableLayoutPanel.Controls.Add(this.deleteButton, 4, 3);
            this.VersionTableLayoutPanel.Location = new System.Drawing.Point(16, 7);
            this.VersionTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.VersionTableLayoutPanel.Name = "VersionTableLayoutPanel";
            this.VersionTableLayoutPanel.RowCount = 4;
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.VersionTableLayoutPanel.Size = new System.Drawing.Size(533, 200);
            this.VersionTableLayoutPanel.TabIndex = 0;
            this.VersionTableLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.VersionTableLayoutPanel_Paint);
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.BackColor = System.Drawing.Color.White;
            this.VersionTableLayoutPanel.SetColumnSpan(this.VersionLabel, 2);
            this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionLabel.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel.Location = new System.Drawing.Point(0, 0);
            this.VersionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(160, 35);
            this.VersionLabel.TabIndex = 0;
            this.VersionLabel.Text = "1";
            // 
            // UserVersionListBox
            // 
            this.UserVersionListBox.BackColor = System.Drawing.Color.White;
            this.UserVersionListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VersionTableLayoutPanel.SetColumnSpan(this.UserVersionListBox, 2);
            this.UserVersionListBox.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserVersionListBox.FormattingEnabled = true;
            this.UserVersionListBox.HorizontalScrollbar = true;
            this.UserVersionListBox.ItemHeight = 24;
            this.UserVersionListBox.Location = new System.Drawing.Point(83, 116);
            this.UserVersionListBox.Name = "UserVersionListBox";
            this.UserVersionListBox.Size = new System.Drawing.Size(174, 50);
            this.UserVersionListBox.TabIndex = 2;
            // 
            // DateLabel
            // 
            this.DateLabel.BackColor = System.Drawing.Color.White;
            this.VersionTableLayoutPanel.SetColumnSpan(this.DateLabel, 5);
            this.DateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateLabel.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLabel.Location = new System.Drawing.Point(160, 0);
            this.DateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(373, 35);
            this.DateLabel.TabIndex = 1;
            this.DateLabel.Text = "18:08PM Saturday,   December 12, 2009";
            this.DateLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.UseButton);
            this.panel2.Controls.Add(this.CopyButton);
            this.panel2.Controls.Add(this.Exploreutton);
            this.panel2.Location = new System.Drawing.Point(3, 116);
            this.panel2.Name = "panel2";
            this.VersionTableLayoutPanel.SetRowSpan(this.panel2, 2);
            this.panel2.Size = new System.Drawing.Size(74, 81);
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
            this.CopyButton.Location = new System.Drawing.Point(3, 30);
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
            this.Exploreutton.Location = new System.Drawing.Point(3, 57);
            this.Exploreutton.Name = "Exploreutton";
            this.Exploreutton.Size = new System.Drawing.Size(69, 23);
            this.Exploreutton.TabIndex = 11;
            this.Exploreutton.Text = "Explore";
            this.Exploreutton.UseVisualStyleBackColor = false;
            this.Exploreutton.Click += new System.EventHandler(this.Exploreutton_Click);
            // 
            // AddUserVersionButton
            // 
            this.AddUserVersionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AddUserVersionButton.AutoSize = true;
            this.AddUserVersionButton.BackColor = System.Drawing.Color.Gainsboro;
            this.AddUserVersionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddUserVersionButton.Location = new System.Drawing.Point(83, 173);
            this.AddUserVersionButton.Name = "AddUserVersionButton";
            this.AddUserVersionButton.Size = new System.Drawing.Size(74, 23);
            this.AddUserVersionButton.TabIndex = 13;
            this.AddUserVersionButton.Text = "Edit User Versions";
            this.AddUserVersionButton.UseVisualStyleBackColor = false;
            this.AddUserVersionButton.Click += new System.EventHandler(this.AddUserVersionButton_Click);
            // 
            // DiscriptionTextBox
            // 
            this.DiscriptionTextBox.BackColor = System.Drawing.Color.White;
            this.DiscriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VersionTableLayoutPanel.SetColumnSpan(this.DiscriptionTextBox, 4);
            this.DiscriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiscriptionTextBox.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiscriptionTextBox.Location = new System.Drawing.Point(263, 116);
            this.DiscriptionTextBox.Multiline = true;
            this.DiscriptionTextBox.Name = "DiscriptionTextBox";
            this.DiscriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DiscriptionTextBox.Size = new System.Drawing.Size(267, 51);
            this.DiscriptionTextBox.TabIndex = 7;
            this.DiscriptionTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ChangeFileListBox
            // 
            this.ChangeFileListBox.BackColor = System.Drawing.Color.White;
            this.ChangeFileListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VersionTableLayoutPanel.SetColumnSpan(this.ChangeFileListBox, 5);
            this.ChangeFileListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeFileListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ChangeFileListBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeFileListBox.FormattingEnabled = true;
            this.ChangeFileListBox.HorizontalScrollbar = true;
            this.ChangeFileListBox.ItemHeight = 14;
            this.ChangeFileListBox.Location = new System.Drawing.Point(3, 38);
            this.ChangeFileListBox.Name = "ChangeFileListBox";
            this.ChangeFileListBox.Size = new System.Drawing.Size(498, 72);
            this.ChangeFileListBox.TabIndex = 3;
            this.ChangeFileListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ChangeFileListBox_DrawItem);
            this.ChangeFileListBox.DoubleClick += new System.EventHandler(this.ChangeFileListBox_DoubleClick);
            this.ChangeFileListBox.SelectedValueChanged += new System.EventHandler(this.ChangeFileListBox_SelectedValueChanged);
            // 
            // SaveDiscriptionButton
            // 
            this.SaveDiscriptionButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SaveDiscriptionButton.AutoSize = true;
            this.SaveDiscriptionButton.BackColor = System.Drawing.Color.Gainsboro;
            this.SaveDiscriptionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveDiscriptionButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveDiscriptionButton.Location = new System.Drawing.Point(263, 173);
            this.SaveDiscriptionButton.Name = "SaveDiscriptionButton";
            this.SaveDiscriptionButton.Size = new System.Drawing.Size(98, 24);
            this.SaveDiscriptionButton.TabIndex = 12;
            this.SaveDiscriptionButton.Text = "Save Description";
            this.SaveDiscriptionButton.UseVisualStyleBackColor = false;
            this.SaveDiscriptionButton.Click += new System.EventHandler(this.SaveDiscriptionButton_Click);
            // 
            // FilterButton
            // 
            this.FilterButton.BackColor = System.Drawing.Color.Gainsboro;
            this.FilterButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FilterButton.Location = new System.Drawing.Point(507, 38);
            this.FilterButton.Name = "FilterButton";
            this.FilterButton.Size = new System.Drawing.Size(23, 72);
            this.FilterButton.TabIndex = 18;
            this.FilterButton.UseVisualStyleBackColor = false;
            this.FilterButton.Paint += new System.Windows.Forms.PaintEventHandler(this.FilterButton_Paint);
            this.FilterButton.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.AutoSize = true;
            this.deleteButton.BackColor = System.Drawing.Color.DarkRed;
            this.VersionTableLayoutPanel.SetColumnSpan(this.deleteButton, 3);
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(432, 173);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(69, 23);
            this.deleteButton.TabIndex = 19;
            this.deleteButton.Text = "Remove";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.VersionTableLayoutPanel);
            this.panel1.Location = new System.Drawing.Point(12, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(565, 214);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 737);
            this.Controls.Add(this.panel1);
            this.Name = "Form2";
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
        private System.Windows.Forms.Button UseButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button Exploreutton;
        private System.Windows.Forms.Button SaveDiscriptionButton;
        private System.Windows.Forms.Button AddUserVersionButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button FilterButton;
        private System.Windows.Forms.Button deleteButton;

    }
}