namespace FolderTrackGuiTest1.AllVersionsTab
{
    partial class MiniVersionDesigner
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.EditUserVerButton = new System.Windows.Forms.Button();
            this.UserVersionListBox = new System.Windows.Forms.ListBox();
            this.DateLabel = new System.Windows.Forms.Label();
            this.ChanListBox = new System.Windows.Forms.ListBox();
            this.DiscriptiionTextBox = new System.Windows.Forms.TextBox();
            this.SaveDiscriptioButton = new System.Windows.Forms.Button();
            this.UseButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.ExplorButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(29, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(575, 114);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Cornsilk;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.99999F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Controls.Add(this.VersionLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.EditUserVerButton, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.UserVersionListBox, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.DateLabel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.ChanListBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.SaveDiscriptioButton, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.DiscriptiionTextBox, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(542, 90);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.VersionLabel, 3);
            this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionLabel.Location = new System.Drawing.Point(3, 0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(357, 18);
            this.VersionLabel.TabIndex = 0;
            this.VersionLabel.Text = "label1";
            // 
            // EditUserVerButton
            // 
            this.EditUserVerButton.BackColor = System.Drawing.Color.BurlyWood;
            this.EditUserVerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditUserVerButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EditUserVerButton.Location = new System.Drawing.Point(522, 21);
            this.EditUserVerButton.Name = "EditUserVerButton";
            this.tableLayoutPanel1.SetRowSpan(this.EditUserVerButton, 2);
            this.EditUserVerButton.Size = new System.Drawing.Size(17, 30);
            this.EditUserVerButton.TabIndex = 3;
            this.EditUserVerButton.Text = "e";
            this.EditUserVerButton.UseVisualStyleBackColor = false;
            this.EditUserVerButton.Click += new System.EventHandler(this.EditUserVerButton_Click);
            // 
            // UserVersionListBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.UserVersionListBox, 2);
            this.UserVersionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserVersionListBox.FormattingEnabled = true;
            this.UserVersionListBox.Location = new System.Drawing.Point(366, 21);
            this.UserVersionListBox.Name = "UserVersionListBox";
            this.tableLayoutPanel1.SetRowSpan(this.UserVersionListBox, 2);
            this.UserVersionListBox.Size = new System.Drawing.Size(150, 30);
            this.UserVersionListBox.TabIndex = 10;
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.DateLabel, 4);
            this.DateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateLabel.Location = new System.Drawing.Point(366, 0);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(173, 18);
            this.DateLabel.TabIndex = 1;
            this.DateLabel.Text = "label1";
            // 
            // ChanListBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ChanListBox, 3);
            this.ChanListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChanListBox.FormattingEnabled = true;
            this.ChanListBox.Location = new System.Drawing.Point(3, 21);
            this.ChanListBox.Name = "ChanListBox";
            this.tableLayoutPanel1.SetRowSpan(this.ChanListBox, 2);
            this.ChanListBox.Size = new System.Drawing.Size(357, 30);
            this.ChanListBox.TabIndex = 4;
            this.ChanListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ChanListBox_MouseDoubleClick);
            // 
            // DiscriptiionTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.DiscriptiionTextBox, 4);
            this.DiscriptiionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiscriptiionTextBox.Location = new System.Drawing.Point(210, 57);
            this.DiscriptiionTextBox.Multiline = true;
            this.DiscriptiionTextBox.Name = "DiscriptiionTextBox";
            this.tableLayoutPanel1.SetRowSpan(this.DiscriptiionTextBox, 2);
            this.DiscriptiionTextBox.Size = new System.Drawing.Size(306, 30);
            this.DiscriptiionTextBox.TabIndex = 5;
            // 
            // SaveDiscriptioButton
            // 
            this.SaveDiscriptioButton.BackColor = System.Drawing.Color.BurlyWood;
            this.SaveDiscriptioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaveDiscriptioButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveDiscriptioButton.Location = new System.Drawing.Point(522, 57);
            this.SaveDiscriptioButton.Name = "SaveDiscriptioButton";
            this.tableLayoutPanel1.SetRowSpan(this.SaveDiscriptioButton, 2);
            this.SaveDiscriptioButton.Size = new System.Drawing.Size(17, 30);
            this.SaveDiscriptioButton.TabIndex = 6;
            this.SaveDiscriptioButton.Text = "s";
            this.SaveDiscriptioButton.UseVisualStyleBackColor = false;
            this.SaveDiscriptioButton.Click += new System.EventHandler(this.SaveDiscriptioButton_Click);
            // 
            // UseButton
            // 
            this.UseButton.BackColor = System.Drawing.Color.BurlyWood;
            this.UseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UseButton.Location = new System.Drawing.Point(8, 4);
            this.UseButton.Name = "UseButton";
            this.UseButton.Size = new System.Drawing.Size(52, 23);
            this.UseButton.TabIndex = 7;
            this.UseButton.Text = "Use";
            this.UseButton.UseVisualStyleBackColor = false;
            this.UseButton.Click += new System.EventHandler(this.UseButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.BackColor = System.Drawing.Color.BurlyWood;
            this.CopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CopyButton.Location = new System.Drawing.Point(74, 4);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(52, 23);
            this.CopyButton.TabIndex = 9;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = false;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // ExplorButton
            // 
            this.ExplorButton.AutoSize = true;
            this.ExplorButton.BackColor = System.Drawing.Color.BurlyWood;
            this.ExplorButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExplorButton.Location = new System.Drawing.Point(140, 4);
            this.ExplorButton.Name = "ExplorButton";
            this.ExplorButton.Size = new System.Drawing.Size(52, 23);
            this.ExplorButton.TabIndex = 8;
            this.ExplorButton.Text = "Explore";
            this.ExplorButton.UseVisualStyleBackColor = false;
            this.ExplorButton.Click += new System.EventHandler(this.ExplorButton_Click);
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.UseButton);
            this.panel2.Controls.Add(this.ExplorButton);
            this.panel2.Controls.Add(this.CopyButton);
            this.panel2.Location = new System.Drawing.Point(3, 57);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 2);
            this.panel2.Size = new System.Drawing.Size(200, 30);
            this.panel2.TabIndex = 11;
            // 
            // MiniVersionDesigner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(772, 408);
            this.Controls.Add(this.panel1);
            this.Name = "MiniVersionDesigner";
            this.Text = "MiniVersionDesigner";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Button EditUserVerButton;
        private System.Windows.Forms.ListBox ChanListBox;
        private System.Windows.Forms.TextBox DiscriptiionTextBox;
        private System.Windows.Forms.Button SaveDiscriptioButton;
        private System.Windows.Forms.Button UseButton;
        private System.Windows.Forms.Button ExplorButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.ListBox UserVersionListBox;
        private System.Windows.Forms.Panel panel2;
    }
}