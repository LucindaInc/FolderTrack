namespace FolderTrackGuiTest1
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.allVersionPanel1 = new FolderTrackGuiTest1.AllVersionsTab.AllVersionPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.calendarPanel1 = new FolderTrackGuiTest1.CalendarTab.CalendarPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.searchPanel1 = new FolderTrackGuiTest1.SearchTab.SearchPanel();
            this.Exploretab = new System.Windows.Forms.TabPage();
            this.explorerTab1 = new FolderTrackGuiTest1.ExplorerTab.ExplorerTab();
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopDeleteMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDeletedVersionsInResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ErrorButton = new System.Windows.Forms.Button();
            this.Monitpanel = new System.Windows.Forms.Panel();
            this.NoMonitorGroupPanel = new System.Windows.Forms.Panel();
            this.NoMonitoringGroupButton = new System.Windows.Forms.Button();
            this.NoMonitotGroupLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.calendarPanel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.searchPanel1.SuspendLayout();
            this.Exploretab.SuspendLayout();
            this.MainMenuStrip.SuspendLayout();
            this.Monitpanel.SuspendLayout();
            this.NoMonitorGroupPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.Exploretab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(821, 609);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.allVersionPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(813, 583);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "All Versions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // allVersionPanel1
            // 
            this.allVersionPanel1.DisplayType = FolderTrackGuiTest1.AllVersionsTab.AllVersionPanel.DisplayOptions.List;
            this.allVersionPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allVersionPanel1.Location = new System.Drawing.Point(3, 3);
            this.allVersionPanel1.Name = "allVersionPanel1";
            this.allVersionPanel1.Size = new System.Drawing.Size(807, 577);
            this.allVersionPanel1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.calendarPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(813, 583);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Calendar Search";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // calendarPanel1
            // 
            this.calendarPanel1.BackColor = System.Drawing.Color.BurlyWood;
            this.calendarPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calendarPanel1.Location = new System.Drawing.Point(3, 3);
            this.calendarPanel1.Name = "calendarPanel1";
            // 
            // calendarPanel1.Panel1
            // 
            this.calendarPanel1.Panel1.AutoScroll = true;
            this.calendarPanel1.Size = new System.Drawing.Size(805, 575);
            this.calendarPanel1.SplitterDistance = 154;
            this.calendarPanel1.SplitterWidth = 8;
            this.calendarPanel1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.searchPanel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(813, 583);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Category Search";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // searchPanel1
            // 
            this.searchPanel1.BackColor = System.Drawing.Color.BurlyWood;
            this.searchPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPanel1.Location = new System.Drawing.Point(3, 3);
            this.searchPanel1.Name = "searchPanel1";
            // 
            // searchPanel1.Panel1
            // 
            this.searchPanel1.Panel1.AutoScroll = true;
            this.searchPanel1.Panel1.BackColor = System.Drawing.Color.Moccasin;
            // 
            // searchPanel1.Panel2
            // 
            this.searchPanel1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.searchPanel1.Size = new System.Drawing.Size(807, 577);
            this.searchPanel1.SplitterDistance = 431;
            this.searchPanel1.SplitterWidth = 7;
            this.searchPanel1.TabIndex = 0;
            // 
            // Exploretab
            // 
            this.Exploretab.Controls.Add(this.explorerTab1);
            this.Exploretab.Location = new System.Drawing.Point(4, 22);
            this.Exploretab.Name = "Exploretab";
            this.Exploretab.Padding = new System.Windows.Forms.Padding(3);
            this.Exploretab.Size = new System.Drawing.Size(813, 583);
            this.Exploretab.TabIndex = 3;
            this.Exploretab.Text = "Explore";
            this.Exploretab.UseVisualStyleBackColor = true;
            // 
            // explorerTab1
            // 
            this.explorerTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.explorerTab1.Location = new System.Drawing.Point(3, 3);
            this.explorerTab1.Name = "explorerTab1";
            this.explorerTab1.Size = new System.Drawing.Size(807, 577);
            this.explorerTab1.TabIndex = 1;
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.deleteToolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(821, 24);
            this.MainMenuStrip.TabIndex = 1;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.stopDeleteMonitorToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // stopDeleteMonitorToolStripMenuItem
            // 
            this.stopDeleteMonitorToolStripMenuItem.Name = "stopDeleteMonitorToolStripMenuItem";
            this.stopDeleteMonitorToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.stopDeleteMonitorToolStripMenuItem.Text = "Stop/Delete Monitor Group";
            this.stopDeleteMonitorToolStripMenuItem.Click += new System.EventHandler(this.stopDeleteMonitorToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.optionsToolStripMenuItem.Text = "Filter";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteOptionsToolStripMenuItem,
            this.undeleteToolStripMenuItem,
            this.showDeletedVersionsInResultsToolStripMenuItem});
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            this.deleteToolStripMenuItem1.Text = "Delete";
            // 
            // deleteOptionsToolStripMenuItem
            // 
            this.deleteOptionsToolStripMenuItem.Name = "deleteOptionsToolStripMenuItem";
            this.deleteOptionsToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.deleteOptionsToolStripMenuItem.Text = "Delete Options";
            this.deleteOptionsToolStripMenuItem.Click += new System.EventHandler(this.deleteOptionsToolStripMenuItem_Click);
            // 
            // undeleteToolStripMenuItem
            // 
            this.undeleteToolStripMenuItem.Name = "undeleteToolStripMenuItem";
            this.undeleteToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.undeleteToolStripMenuItem.Text = "Un-remove";
            this.undeleteToolStripMenuItem.Click += new System.EventHandler(this.undeleteToolStripMenuItem_Click);
            // 
            // showDeletedVersionsInResultsToolStripMenuItem
            // 
            this.showDeletedVersionsInResultsToolStripMenuItem.Name = "showDeletedVersionsInResultsToolStripMenuItem";
            this.showDeletedVersionsInResultsToolStripMenuItem.Size = new System.Drawing.Size(256, 22);
            this.showDeletedVersionsInResultsToolStripMenuItem.Text = "Show Removed Versions in Results";
            this.showDeletedVersionsInResultsToolStripMenuItem.Click += new System.EventHandler(this.showDeletedVersionsInResultsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem,
            this.licenseToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(113, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.licenseToolStripMenuItem.Text = "License";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // 
            // ErrorButton
            // 
            this.ErrorButton.BackColor = System.Drawing.Color.Green;
            this.ErrorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ErrorButton.Location = new System.Drawing.Point(373, 1);
            this.ErrorButton.Name = "ErrorButton";
            this.ErrorButton.Size = new System.Drawing.Size(75, 23);
            this.ErrorButton.TabIndex = 2;
            this.ErrorButton.Text = "Working";
            this.ErrorButton.UseVisualStyleBackColor = false;
            this.ErrorButton.Visible = false;
            this.ErrorButton.Click += new System.EventHandler(this.ErrorButton_Click);
            // 
            // Monitpanel
            // 
            this.Monitpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Monitpanel.BackColor = System.Drawing.SystemColors.Control;
            this.Monitpanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Monitpanel.BackgroundImage")));
            this.Monitpanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Monitpanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Monitpanel.Controls.Add(this.NoMonitorGroupPanel);
            this.Monitpanel.Location = new System.Drawing.Point(0, 46);
            this.Monitpanel.Name = "Monitpanel";
            this.Monitpanel.Size = new System.Drawing.Size(821, 587);
            this.Monitpanel.TabIndex = 1;
            // 
            // NoMonitorGroupPanel
            // 
            this.NoMonitorGroupPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NoMonitorGroupPanel.BackColor = System.Drawing.Color.Linen;
            this.NoMonitorGroupPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NoMonitorGroupPanel.Controls.Add(this.NoMonitoringGroupButton);
            this.NoMonitorGroupPanel.Controls.Add(this.NoMonitotGroupLabel);
            this.NoMonitorGroupPanel.Location = new System.Drawing.Point(6, 240);
            this.NoMonitorGroupPanel.Name = "NoMonitorGroupPanel";
            this.NoMonitorGroupPanel.Size = new System.Drawing.Size(807, 100);
            this.NoMonitorGroupPanel.TabIndex = 0;
            // 
            // NoMonitoringGroupButton
            // 
            this.NoMonitoringGroupButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.NoMonitoringGroupButton.AutoSize = true;
            this.NoMonitoringGroupButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoMonitoringGroupButton.Location = new System.Drawing.Point(302, 56);
            this.NoMonitoringGroupButton.Name = "NoMonitoringGroupButton";
            this.NoMonitoringGroupButton.Size = new System.Drawing.Size(201, 34);
            this.NoMonitoringGroupButton.TabIndex = 1;
            this.NoMonitoringGroupButton.Text = "Monitor Folder or File";
            this.NoMonitoringGroupButton.UseVisualStyleBackColor = true;
            this.NoMonitoringGroupButton.Click += new System.EventHandler(this.NoMonitoringGroupButton_Click);
            // 
            // NoMonitotGroupLabel
            // 
            this.NoMonitotGroupLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.NoMonitotGroupLabel.AutoSize = true;
            this.NoMonitotGroupLabel.BackColor = System.Drawing.Color.Linen;
            this.NoMonitotGroupLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoMonitotGroupLabel.Location = new System.Drawing.Point(135, 0);
            this.NoMonitotGroupLabel.Name = "NoMonitotGroupLabel";
            this.NoMonitotGroupLabel.Size = new System.Drawing.Size(535, 29);
            this.NoMonitotGroupLabel.TabIndex = 0;
            this.NoMonitotGroupLabel.Text = "FolderTrack is not monitoring any folders or files ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 633);
            this.Controls.Add(this.ErrorButton);
            this.Controls.Add(this.Monitpanel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.MainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "FolderTrack";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.calendarPanel1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.searchPanel1.ResumeLayout(false);
            this.Exploretab.ResumeLayout(false);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.Monitpanel.ResumeLayout(false);
            this.NoMonitorGroupPanel.ResumeLayout(false);
            this.NoMonitorGroupPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
        private FolderTrackGuiTest1.CalendarTab.CalendarPanel calendarPanel1;
        private System.Windows.Forms.TabPage tabPage3;
        private FolderTrackGuiTest1.SearchTab.SearchPanel searchPanel1;
        private System.Windows.Forms.TabPage Exploretab;
        private FolderTrackGuiTest1.AllVersionsTab.AllVersionPanel allVersionPanel1;
        private System.Windows.Forms.Panel Monitpanel;
        private FolderTrackGuiTest1.ExplorerTab.ExplorerTab explorerTab1;
        private System.Windows.Forms.Button ErrorButton;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.Panel NoMonitorGroupPanel;
        private System.Windows.Forms.Label NoMonitotGroupLabel;
        private System.Windows.Forms.Button NoMonitoringGroupButton;
        private System.Windows.Forms.ToolStripMenuItem stopDeleteMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDeletedVersionsInResultsToolStripMenuItem;

    }
}

