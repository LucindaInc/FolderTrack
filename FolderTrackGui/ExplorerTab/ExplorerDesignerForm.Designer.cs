namespace FolderTrackGuiTest1.ExplorerTab
{
    partial class ExplorerDesignerForm
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
            this.CurrentTextBox = new System.Windows.Forms.TextBox();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.ExplorertButton = new System.Windows.Forms.Button();
            this.SavedVersionsListBox = new System.Windows.Forms.ListBox();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.InfoTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.UserVerionLabel = new System.Windows.Forms.Label();
            this.DiscriptionLabel = new System.Windows.Forms.Label();
            this.disctextBox = new System.Windows.Forms.TextBox();
            this.SaveDiscriptionButton = new System.Windows.Forms.Button();
            this.UserVersionListBox = new System.Windows.Forms.ListBox();
            this.EditUserVersionButton = new System.Windows.Forms.Button();
            this.ChangesLabel = new System.Windows.Forms.Label();
            this.ChangeListBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.DeleteLabel = new System.Windows.Forms.Label();
            this.RenameLabel = new System.Windows.Forms.Label();
            this.ChangeLabel = new System.Windows.Forms.Label();
            this.AddStatsLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AddLabel = new System.Windows.Forms.Label();
            this.ChangeStatsLabel = new System.Windows.Forms.Label();
            this.RenameStatsLabel = new System.Windows.Forms.Label();
            this.DeleteStatsLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FolderUnitTreeView = new System.Windows.Forms.TreeView();
            this.UseButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.InfoTableLayoutPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // CurrentTextBox
            // 
            this.tableLayoutPanel5.SetColumnSpan(this.CurrentTextBox, 2);
            this.CurrentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentTextBox.Location = new System.Drawing.Point(3, 3);
            this.CurrentTextBox.Name = "CurrentTextBox";
            this.CurrentTextBox.Size = new System.Drawing.Size(428, 29);
            this.CurrentTextBox.TabIndex = 2;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(3, 35);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(29, 13);
            this.ErrorLabel.TabIndex = 2;
            this.ErrorLabel.Text = "Error";
            this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ErrorLabel.Visible = false;
            // 
            // ExplorertButton
            // 
            this.ExplorertButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExplorertButton.BackColor = System.Drawing.Color.BurlyWood;
            this.ExplorertButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExplorertButton.Location = new System.Drawing.Point(437, 6);
            this.ExplorertButton.Name = "ExplorertButton";
            this.ExplorertButton.Size = new System.Drawing.Size(75, 23);
            this.ExplorertButton.TabIndex = 3;
            this.ExplorertButton.Text = "Explore";
            this.ExplorertButton.UseVisualStyleBackColor = false;
            this.ExplorertButton.Click += new System.EventHandler(this.ExplorertButton_Click);
            // 
            // SavedVersionsListBox
            // 
            this.SavedVersionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SavedVersionsListBox.FormattingEnabled = true;
            this.SavedVersionsListBox.Location = new System.Drawing.Point(3, 3);
            this.SavedVersionsListBox.Name = "SavedVersionsListBox";
            this.SavedVersionsListBox.Size = new System.Drawing.Size(185, 82);
            this.SavedVersionsListBox.TabIndex = 8;
            this.SavedVersionsListBox.DoubleClick += new System.EventHandler(this.SavedVersionsListBox_DoubleClick);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RemoveButton.BackColor = System.Drawing.Color.BurlyWood;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveButton.Location = new System.Drawing.Point(58, 103);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 19);
            this.RemoveButton.TabIndex = 9;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = false;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // InfoTableLayoutPanel
            // 
            this.InfoTableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.InfoTableLayoutPanel.ColumnCount = 1;
            this.InfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InfoTableLayoutPanel.Controls.Add(this.UserVerionLabel, 0, 2);
            this.InfoTableLayoutPanel.Controls.Add(this.DiscriptionLabel, 0, 5);
            this.InfoTableLayoutPanel.Controls.Add(this.disctextBox, 0, 6);
            this.InfoTableLayoutPanel.Controls.Add(this.SaveDiscriptionButton, 0, 7);
            this.InfoTableLayoutPanel.Controls.Add(this.RemoveButton, 0, 1);
            this.InfoTableLayoutPanel.Controls.Add(this.SavedVersionsListBox, 0, 0);
            this.InfoTableLayoutPanel.Controls.Add(this.UserVersionListBox, 0, 3);
            this.InfoTableLayoutPanel.Controls.Add(this.EditUserVersionButton, 0, 4);
            this.InfoTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.InfoTableLayoutPanel.Name = "InfoTableLayoutPanel";
            this.InfoTableLayoutPanel.RowCount = 8;
            this.InfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.InfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.InfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.InfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.InfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.InfoTableLayoutPanel.Size = new System.Drawing.Size(191, 523);
            this.InfoTableLayoutPanel.TabIndex = 6;
            // 
            // UserVerionLabel
            // 
            this.UserVerionLabel.AutoSize = true;
            this.UserVerionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserVerionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserVerionLabel.Location = new System.Drawing.Point(3, 125);
            this.UserVerionLabel.Name = "UserVerionLabel";
            this.UserVerionLabel.Size = new System.Drawing.Size(185, 20);
            this.UserVerionLabel.TabIndex = 2;
            this.UserVerionLabel.Text = "User Versions";
            this.UserVerionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DiscriptionLabel
            // 
            this.DiscriptionLabel.AutoSize = true;
            this.DiscriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DiscriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiscriptionLabel.Location = new System.Drawing.Point(3, 324);
            this.DiscriptionLabel.Name = "DiscriptionLabel";
            this.DiscriptionLabel.Size = new System.Drawing.Size(185, 20);
            this.DiscriptionLabel.TabIndex = 3;
            this.DiscriptionLabel.Text = "Description";
            this.DiscriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // disctextBox
            // 
            this.disctextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disctextBox.Location = new System.Drawing.Point(3, 347);
            this.disctextBox.Multiline = true;
            this.disctextBox.Name = "disctextBox";
            this.disctextBox.Size = new System.Drawing.Size(185, 144);
            this.disctextBox.TabIndex = 12;
            this.disctextBox.TextChanged += new System.EventHandler(this.disctextBox_TextChanged);
            // 
            // SaveDiscriptionButton
            // 
            this.SaveDiscriptionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveDiscriptionButton.BackColor = System.Drawing.Color.BurlyWood;
            this.SaveDiscriptionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveDiscriptionButton.Location = new System.Drawing.Point(58, 497);
            this.SaveDiscriptionButton.Name = "SaveDiscriptionButton";
            this.SaveDiscriptionButton.Size = new System.Drawing.Size(75, 23);
            this.SaveDiscriptionButton.TabIndex = 13;
            this.SaveDiscriptionButton.Text = "Save";
            this.SaveDiscriptionButton.UseVisualStyleBackColor = false;
            this.SaveDiscriptionButton.Click += new System.EventHandler(this.SaveDiscriptionButton_Click);
            // 
            // UserVersionListBox
            // 
            this.UserVersionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserVersionListBox.FormattingEnabled = true;
            this.UserVersionListBox.Location = new System.Drawing.Point(3, 148);
            this.UserVersionListBox.Name = "UserVersionListBox";
            this.UserVersionListBox.Size = new System.Drawing.Size(185, 134);
            this.UserVersionListBox.TabIndex = 10;
            // 
            // EditUserVersionButton
            // 
            this.EditUserVersionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.EditUserVersionButton.BackColor = System.Drawing.Color.BurlyWood;
            this.EditUserVersionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.EditUserVersionButton.Location = new System.Drawing.Point(58, 298);
            this.EditUserVersionButton.Name = "EditUserVersionButton";
            this.EditUserVersionButton.Size = new System.Drawing.Size(75, 23);
            this.EditUserVersionButton.TabIndex = 11;
            this.EditUserVersionButton.Text = "Edit";
            this.EditUserVersionButton.UseVisualStyleBackColor = false;
            this.EditUserVersionButton.Click += new System.EventHandler(this.EditUserVersionButton_Click);
            // 
            // ChangesLabel
            // 
            this.ChangesLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChangesLabel.Location = new System.Drawing.Point(3, 612);
            this.ChangesLabel.Name = "ChangesLabel";
            this.ChangesLabel.Size = new System.Drawing.Size(208, 15);
            this.ChangesLabel.TabIndex = 0;
            this.ChangesLabel.Text = "Changes";
            this.ChangesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChangeListBox
            // 
            this.ChangeListBox.BackColor = System.Drawing.SystemColors.Control;
            this.ChangeListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeListBox.FormattingEnabled = true;
            this.ChangeListBox.Location = new System.Drawing.Point(3, 635);
            this.ChangeListBox.Name = "ChangeListBox";
            this.ChangeListBox.Size = new System.Drawing.Size(515, 43);
            this.ChangeListBox.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.DeleteLabel, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.RenameLabel, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.ChangeLabel, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.AddStatsLabel, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.AddLabel, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.ChangeStatsLabel, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.RenameStatsLabel, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.DeleteStatsLabel, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(524, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel4.SetRowSpan(this.tableLayoutPanel2, 4);
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(344, 676);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // DeleteLabel
            // 
            this.DeleteLabel.AutoSize = true;
            this.DeleteLabel.BackColor = System.Drawing.Color.Crimson;
            this.DeleteLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeleteLabel.Location = new System.Drawing.Point(0, 59);
            this.DeleteLabel.Margin = new System.Windows.Forms.Padding(0);
            this.DeleteLabel.Name = "DeleteLabel";
            this.DeleteLabel.Size = new System.Drawing.Size(47, 13);
            this.DeleteLabel.TabIndex = 7;
            this.DeleteLabel.Text = "Delete";
            // 
            // RenameLabel
            // 
            this.RenameLabel.AutoSize = true;
            this.RenameLabel.BackColor = System.Drawing.Color.Turquoise;
            this.RenameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RenameLabel.Location = new System.Drawing.Point(0, 46);
            this.RenameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.RenameLabel.Name = "RenameLabel";
            this.RenameLabel.Size = new System.Drawing.Size(47, 13);
            this.RenameLabel.TabIndex = 6;
            this.RenameLabel.Text = "Rename";
            // 
            // ChangeLabel
            // 
            this.ChangeLabel.AutoSize = true;
            this.ChangeLabel.BackColor = System.Drawing.Color.Goldenrod;
            this.ChangeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeLabel.Location = new System.Drawing.Point(0, 33);
            this.ChangeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ChangeLabel.Name = "ChangeLabel";
            this.ChangeLabel.Size = new System.Drawing.Size(47, 13);
            this.ChangeLabel.TabIndex = 5;
            this.ChangeLabel.Text = "Change";
            // 
            // AddStatsLabel
            // 
            this.AddStatsLabel.AutoSize = true;
            this.AddStatsLabel.BackColor = System.Drawing.Color.DarkGreen;
            this.AddStatsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddStatsLabel.ForeColor = System.Drawing.Color.White;
            this.AddStatsLabel.Location = new System.Drawing.Point(47, 20);
            this.AddStatsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.AddStatsLabel.Name = "AddStatsLabel";
            this.AddStatsLabel.Size = new System.Drawing.Size(297, 13);
            this.AddStatsLabel.TabIndex = 0;
            this.AddStatsLabel.Text = "Add Stats";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel2.SetColumnSpan(this.panel2, 2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(338, 598);
            this.panel2.TabIndex = 8;
            // 
            // AddLabel
            // 
            this.AddLabel.AutoSize = true;
            this.AddLabel.BackColor = System.Drawing.Color.DarkGreen;
            this.AddLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddLabel.ForeColor = System.Drawing.Color.White;
            this.AddLabel.Location = new System.Drawing.Point(0, 20);
            this.AddLabel.Margin = new System.Windows.Forms.Padding(0);
            this.AddLabel.Name = "AddLabel";
            this.AddLabel.Size = new System.Drawing.Size(47, 13);
            this.AddLabel.TabIndex = 4;
            this.AddLabel.Text = "Add";
            // 
            // ChangeStatsLabel
            // 
            this.ChangeStatsLabel.AutoSize = true;
            this.ChangeStatsLabel.BackColor = System.Drawing.Color.Goldenrod;
            this.ChangeStatsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeStatsLabel.Location = new System.Drawing.Point(47, 33);
            this.ChangeStatsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.ChangeStatsLabel.Name = "ChangeStatsLabel";
            this.ChangeStatsLabel.Size = new System.Drawing.Size(297, 13);
            this.ChangeStatsLabel.TabIndex = 1;
            this.ChangeStatsLabel.Text = "Change Stats";
            // 
            // RenameStatsLabel
            // 
            this.RenameStatsLabel.AutoSize = true;
            this.RenameStatsLabel.BackColor = System.Drawing.Color.Turquoise;
            this.RenameStatsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RenameStatsLabel.Location = new System.Drawing.Point(47, 46);
            this.RenameStatsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.RenameStatsLabel.Name = "RenameStatsLabel";
            this.RenameStatsLabel.Size = new System.Drawing.Size(297, 13);
            this.RenameStatsLabel.TabIndex = 2;
            this.RenameStatsLabel.Text = "Rename Stats";
            // 
            // DeleteStatsLabel
            // 
            this.DeleteStatsLabel.AutoSize = true;
            this.DeleteStatsLabel.BackColor = System.Drawing.Color.Crimson;
            this.DeleteStatsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeleteStatsLabel.Location = new System.Drawing.Point(47, 59);
            this.DeleteStatsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.DeleteStatsLabel.Name = "DeleteStatsLabel";
            this.DeleteStatsLabel.Size = new System.Drawing.Size(297, 13);
            this.DeleteStatsLabel.TabIndex = 3;
            this.DeleteStatsLabel.Text = "Delete Stats";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "File History";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FolderUnitTreeView
            // 
            this.FolderUnitTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FolderUnitTreeView.Location = new System.Drawing.Point(0, 0);
            this.FolderUnitTreeView.Name = "FolderUnitTreeView";
            this.FolderUnitTreeView.Size = new System.Drawing.Size(316, 523);
            this.FolderUnitTreeView.TabIndex = 15;
            this.FolderUnitTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.FolderUnitTreeView_NodeMouseDoubleClick);
            this.FolderUnitTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FolderUnitTreeView_AfterSelect);
            // 
            // UseButton
            // 
            this.UseButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UseButton.BackColor = System.Drawing.Color.BurlyWood;
            this.UseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UseButton.Location = new System.Drawing.Point(356, 51);
            this.UseButton.Name = "UseButton";
            this.UseButton.Size = new System.Drawing.Size(75, 23);
            this.UseButton.TabIndex = 4;
            this.UseButton.Text = "Use";
            this.UseButton.UseVisualStyleBackColor = false;
            this.UseButton.Click += new System.EventHandler(this.UseButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CopyButton.BackColor = System.Drawing.Color.BurlyWood;
            this.CopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CopyButton.Location = new System.Drawing.Point(437, 51);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(75, 23);
            this.CopyButton.TabIndex = 5;
            this.CopyButton.Tag = "";
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = false;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 682);
            this.panel1.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel4.Controls.Add(this.ChangesLabel, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.ChangeListBox, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 4;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(871, 682);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.BurlyWood;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 86);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.InfoTableLayoutPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.FolderUnitTreeView);
            this.splitContainer1.Size = new System.Drawing.Size(515, 523);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 7;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.CurrentTextBox, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.ErrorLabel, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.UseButton, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.CopyButton, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.ExplorertButton, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 3;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(515, 77);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // ExplorerDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 682);
            this.Controls.Add(this.panel1);
            this.Name = "ExplorerDesignerForm";
            this.Text = "ExplorerDesignerForm";
            this.InfoTableLayoutPanel.ResumeLayout(false);
            this.InfoTableLayoutPanel.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox CurrentTextBox;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Button ExplorertButton;
        private System.Windows.Forms.ListBox SavedVersionsListBox;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.Label AddStatsLabel;
        private System.Windows.Forms.Label ChangeStatsLabel;
        private System.Windows.Forms.Label RenameStatsLabel;
        private System.Windows.Forms.Label DeleteStatsLabel;
        private System.Windows.Forms.Label AddLabel;
        private System.Windows.Forms.Label ChangeLabel;
        private System.Windows.Forms.Label RenameLabel;
        private System.Windows.Forms.Label DeleteLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView FolderUnitTreeView;
        private System.Windows.Forms.Button UseButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel InfoTableLayoutPanel;
        private System.Windows.Forms.Label ChangesLabel;
        private System.Windows.Forms.Label UserVerionLabel;
        private System.Windows.Forms.Label DiscriptionLabel;
        private System.Windows.Forms.TextBox disctextBox;
        private System.Windows.Forms.Button SaveDiscriptionButton;
        private System.Windows.Forms.ListBox ChangeListBox;
        private System.Windows.Forms.ListBox UserVersionListBox;
        private System.Windows.Forms.Button EditUserVersionButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    }
}