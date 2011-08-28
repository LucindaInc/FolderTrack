namespace FolderTrackGuiTest1.UserVersionDialog
{
    partial class UserVersionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserVersionForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CreateAndUseButton = new System.Windows.Forms.Button();
            this.CreateUserVersionTextBox = new System.Windows.Forms.TextBox();
            this.CurrentUserVersionsGroupBox = new System.Windows.Forms.GroupBox();
            this.VersionPanel = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.UseAgainButton = new System.Windows.Forms.Button();
            this.ReUseListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.CurrentUserVersionsGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CreateAndUseButton);
            this.groupBox1.Controls.Add(this.CreateUserVersionTextBox);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 158);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create and Use User Version";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name";
            // 
            // CreateAndUseButton
            // 
            this.CreateAndUseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CreateAndUseButton.AutoSize = true;
            this.CreateAndUseButton.BackColor = System.Drawing.Color.BurlyWood;
            this.CreateAndUseButton.Enabled = false;
            this.CreateAndUseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CreateAndUseButton.Location = new System.Drawing.Point(39, 87);
            this.CreateAndUseButton.Name = "CreateAndUseButton";
            this.CreateAndUseButton.Size = new System.Drawing.Size(108, 26);
            this.CreateAndUseButton.TabIndex = 1;
            this.CreateAndUseButton.Text = "Enter Text";
            this.CreateAndUseButton.UseVisualStyleBackColor = false;
            this.CreateAndUseButton.Click += new System.EventHandler(this.CreateAndUseButton_Click);
            // 
            // CreateUserVersionTextBox
            // 
            this.CreateUserVersionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateUserVersionTextBox.Location = new System.Drawing.Point(6, 58);
            this.CreateUserVersionTextBox.Name = "CreateUserVersionTextBox";
            this.CreateUserVersionTextBox.Size = new System.Drawing.Size(174, 21);
            this.CreateUserVersionTextBox.TabIndex = 0;
            this.CreateUserVersionTextBox.TextChanged += new System.EventHandler(this.CreateUserVersionTextBox_TextChanged);
            // 
            // CurrentUserVersionsGroupBox
            // 
            this.CurrentUserVersionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentUserVersionsGroupBox.Controls.Add(this.VersionPanel);
            this.CurrentUserVersionsGroupBox.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentUserVersionsGroupBox.ForeColor = System.Drawing.Color.Black;
            this.CurrentUserVersionsGroupBox.Location = new System.Drawing.Point(195, 3);
            this.CurrentUserVersionsGroupBox.Name = "CurrentUserVersionsGroupBox";
            this.CurrentUserVersionsGroupBox.Size = new System.Drawing.Size(378, 158);
            this.CurrentUserVersionsGroupBox.TabIndex = 1;
            this.CurrentUserVersionsGroupBox.TabStop = false;
            this.CurrentUserVersionsGroupBox.Text = "Used User Versions";
            // 
            // VersionPanel
            // 
            this.VersionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionPanel.Location = new System.Drawing.Point(3, 17);
            this.VersionPanel.Name = "VersionPanel";
            this.VersionPanel.Size = new System.Drawing.Size(372, 138);
            this.VersionPanel.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.UseAgainButton);
            this.groupBox3.Controls.Add(this.ReUseListBox);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(579, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(187, 158);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Not Used User Versions";
            // 
            // UseAgainButton
            // 
            this.UseAgainButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.UseAgainButton.BackColor = System.Drawing.Color.BurlyWood;
            this.UseAgainButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UseAgainButton.Location = new System.Drawing.Point(56, 129);
            this.UseAgainButton.Name = "UseAgainButton";
            this.UseAgainButton.Size = new System.Drawing.Size(75, 23);
            this.UseAgainButton.TabIndex = 1;
            this.UseAgainButton.Text = "Use";
            this.UseAgainButton.UseVisualStyleBackColor = false;
            this.UseAgainButton.Click += new System.EventHandler(this.UseAgainButton_Click);
            // 
            // ReUseListBox
            // 
            this.ReUseListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ReUseListBox.FormattingEnabled = true;
            this.ReUseListBox.ItemHeight = 15;
            this.ReUseListBox.Location = new System.Drawing.Point(6, 16);
            this.ReUseListBox.Name = "ReUseListBox";
            this.ReUseListBox.Size = new System.Drawing.Size(171, 79);
            this.ReUseListBox.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.CurrentUserVersionsGroupBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(769, 194);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.Cancelbutton);
            this.panel1.Controls.Add(this.OKButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(195, 167);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(378, 24);
            this.panel1.TabIndex = 3;
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.BackColor = System.Drawing.Color.BurlyWood;
            this.Cancelbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cancelbutton.Location = new System.Drawing.Point(212, 1);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbutton.TabIndex = 1;
            this.Cancelbutton.Text = "Cancel";
            this.Cancelbutton.UseVisualStyleBackColor = false;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.Color.BurlyWood;
            this.OKButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.OKButton.Location = new System.Drawing.Point(92, 1);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "Ok";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // UserVersionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(769, 194);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserVersionForm";
            this.Text = "Create and Edit User Versions";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.CurrentUserVersionsGroupBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CreateAndUseButton;
        private System.Windows.Forms.TextBox CreateUserVersionTextBox;
        private System.Windows.Forms.GroupBox CurrentUserVersionsGroupBox;
        private System.Windows.Forms.Panel VersionPanel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button UseAgainButton;
        private System.Windows.Forms.ListBox ReUseListBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button Cancelbutton;
    }
}