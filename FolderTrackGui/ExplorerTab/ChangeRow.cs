using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Drawing;
using System.IO;
using System.ComponentModel;
using System.Threading;
using FolderTrackGuiTest1.CopyVersionDialog;
using ZetaLongPaths;

namespace FolderTrackGuiTest1.ExplorerTab
{
    class ChangeRow : Panel, PanelList<ChangeInstruction>.CallAllDa
    {
        private ChangeInstruction m_change;
        private FTObjects m_FTObjects;
        private ExplorerTab tab;
        private delegate void VoidNoArgDelegate();
        private delegate void VoidBoolDelegate(bool monitor);
        public ChangeRow()
        {
            InitializeComponent();
        }

        public ChangeRow(ChangeInstruction change, FTObjects ftobjects, ExplorerTab tab)
        {
            InitializeComponent();
            if (change.folderUnit.type == Delimiter.FOLDER)
            {
                this.ViewButton.Visible = false;
                this.UseButton.Visible = false;
            }
            if (change.change == ChangeType.Delete)
            {
                this.ViewButton.Visible = false;
                this.CopyButton.Visible = false;
                this.UseButton.Visible = false;
            }
            m_change = change;
            FillVariables();
            this.m_FTObjects = ftobjects;
            SetChangeColor(change.change);
            this.tab = tab;
        }

        public void CallAll(object data)
        {
            if (data is Boolean)
            {
                bool da = (bool) data;
                DontMonitor(da);
            }
        }

        public void DontMonitor(bool dontMonitor)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidBoolDelegate(DontMonitor), new object[] { dontMonitor });
                return;
            }
            if (dontMonitor)
            {
                this.UseButton.Visible = false;
            }
            else
            {
                if (m_change.change != ChangeType.Delete && m_change.folderUnit.type != Delimiter.FOLDER)
                {
                    this.UseButton.Visible = true;
                }
            }
        }

        private void FillVariables()
        {
            this.FolderUnitLabel.Text = m_change.folderUnit.version;
            this.FirstVersionLinkLabel.Text = m_change.folderUnit.firstMGVersion;
            string datest = m_change.change.ToString() +
                                    " " + m_change.folderUnit.time.ToLongDateString() +
                                    " " + m_change.folderUnit.time.ToLongTimeString();
            this.DateAndTypeLabel.Text = datest;
            this.ExterlanLocationlabel.Text = m_change.folderUnit.externalLocation;

        }

        private void FirstVersionLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tab.SetExploredVersion(this.m_change.folderUnit.firstMGVersion);
        }

        public void CurrentR()
        {
            this.Invalidate();
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            Pen pe = new Pen(Color.BurlyWood, 3);
            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            if (tab.Current)
            {
                e.Graphics.Clear(Color.LightBlue);
            }
            else
            {
                e.Graphics.Clear(System.Drawing.SystemColors.Control);
            }
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(9, 12);
            pt[1] = new Point(this.Width - 11, 12);
            pt[2] = new Point(this.Width - 11, this.Height - 14);
            pt[3] = new Point(9, this.Height - 14);
            pt[4] = new Point(9, 12);

            e.Graphics.DrawLines(pe, pt);
        }

        private void SetChangeColor(ChangeType change)
        {
            ChangeType chan;
            if (change == ChangeType.First)
            {
                chan = ChangeType.Add;
            }
            else
            {
                chan = change;
            }
            switch (chan)
            {
                case ChangeType.Add:
                    this.DateAndTypeLabel.BackColor = System.Drawing.Color.DarkGreen;
                  //  this.FileVersionLabel.BackColor = System.Drawing.Color.DarkGreen;
                 //   this.GroupVersionLabel.BackColor = System.Drawing.Color.DarkGreen;
                 //   this.LocationLabel.BackColor = System.Drawing.Color.DarkGreen;

                    this.DateAndTypeLabel.ForeColor = System.Drawing.Color.White;
              //      this.FileVersionLabel.ForeColor = System.Drawing.Color.White;
              //      this.GroupVersionLabel.ForeColor = System.Drawing.Color.White;
             //       this.LocationLabel.ForeColor = System.Drawing.Color.White;
                break;

                case ChangeType.Change:
                    this.DateAndTypeLabel.BackColor = System.Drawing.Color.Goldenrod;
              //      this.FileVersionLabel.BackColor = System.Drawing.Color.Goldenrod;
             //       this.GroupVersionLabel.BackColor = System.Drawing.Color.Goldenrod;
              //      this.LocationLabel.BackColor = System.Drawing.Color.Goldenrod;
                break;

                case ChangeType.Rename:
                    this.DateAndTypeLabel.BackColor = System.Drawing.Color.Aqua;
              //      this.FileVersionLabel.BackColor = System.Drawing.Color.Aqua;
              //      this.GroupVersionLabel.BackColor = System.Drawing.Color.Aqua;
              //      this.LocationLabel.BackColor = System.Drawing.Color.Aqua;
                break;

                case ChangeType.Delete:
                    this.DateAndTypeLabel.BackColor = System.Drawing.Color.Crimson;
             //       this.FileVersionLabel.BackColor = System.Drawing.Color.Crimson;
            //        this.GroupVersionLabel.BackColor = System.Drawing.Color.Crimson;
             //       this.LocationLabel.BackColor = System.Drawing.Color.Crimson;
                break;

            }


        }

        private void UseButton_Click(object sender, EventArgs e)
        {
            VersionInfo versioninfo = m_FTObjects.VersionInfoFromVersionName(m_change.folderUnit.firstMGVersion);
            string VersionFile = m_change.folderUnit.externalLocation;
            m_FTObjects.UseFolderUnit(versioninfo, VersionFile);
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (m_change.folderUnit.type == Delimiter.FILE)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.FileName = ZlpPathHelper.GetFileNameFromFilePath(m_change.folderUnit.externalLocation);
                DialogResult dr = dialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    VersionInfo versioninfo = m_FTObjects.VersionInfoFromVersionName(m_change.folderUnit.firstMGVersion);
                    string VersionFile = m_change.folderUnit.externalLocation;
                    string CopyTo = dialog.FileName;
                    m_FTObjects.CopyFolderUnit(versioninfo, VersionFile, CopyTo);

                }
                dialog.Dispose();
            }
            else
            {
                showCopyDlgForDBlClk();
            }
        }

        private void showCopyDlgForDBlClk()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidNoArgDelegate(showCopyDlgForDBlClk));
                return;
            }
            string selectedFile = (string)m_change.folderUnit.externalLocation;
            List<string> moLoca = new List<string>();
            moLoca.Add(selectedFile);
            CopyVersionForm copyForm = new CopyVersionForm(moLoca);
            copyForm.ShowDialog(this);
            if (copyForm.DialogResult == DialogResult.OK)
            {
                //   Dictionary<string, string> ToLocations = copyForm.CopyLocFromExtLoc;
                //   m_FTObjects.CopyVersion(this.m_VersionInfo.versionName, ToLocations);

                new Thread(new ParameterizedThreadStart(HandleCopy)).Start(copyForm);
            }
        }

        public void HandleCopy(object copyFormO)
        {
            CopyVersionForm copyForm = (CopyVersionForm)copyFormO;
            Dictionary<string, string> ToLocations = copyForm.CopyLocFromExtLoc;
            m_FTObjects.CopyVersion(m_change.folderUnit.firstMGVersion, ToLocations);
        }


        private void ViewButton_Click(object sender, EventArgs e)
        {
            new Thread(HandleButtCli).Start();
        }

        private void HandleButtCli()
        {
            VersionInfo versioninfo = m_FTObjects.VersionInfoFromVersionName(m_change.folderUnit.firstMGVersion);
            if (versioninfo != null)
            {

                string VersionFile = m_change.folderUnit.externalLocation;


                string PreDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                             + Path.DirectorySeparatorChar +Util.Company + Path.DirectorySeparatorChar 
                             + Util.Product 
                             + Settings.Default.tempDir 
                             + Path.DirectorySeparatorChar;

                string CopyTo = PreDir +
                                ZlpPathHelper.GetFileNameFromFilePath(VersionFile);

                int uniqueFileInt = 0;
                while (ZlpIOHelper.FileExists(CopyTo) || ZlpIOHelper.DirectoryExists(CopyTo))
                {

                    CopyTo = PreDir +
                             Convert.ToString(uniqueFileInt) +
                             Path.DirectorySeparatorChar +
                             ZlpPathHelper.GetFileNameFromFilePath(VersionFile);
                    uniqueFileInt++;
                }

                ZlpIOHelper.CreateDirectory(ZlpPathHelper.GetDirectoryNameFromFilePath(CopyTo));

                m_FTObjects.CopyFolderUnit(versioninfo, VersionFile, CopyTo);


                try
                {
                    System.Diagnostics.Process.Start(CopyTo);
                }
                catch (Win32Exception ex)
                {
                    if (ex.NativeErrorCode == 1155)
                    {
                        ExplorerTab.OpenAs(CopyTo);
                    }
                    else
                    {
                        int a = 3 + 2;
                        a++;
                    }
                }
            }
        }




        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.FolderUnitLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.FirstVersionLinkLabel = new System.Windows.Forms.LinkLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.ExterlanLocationlabel = new System.Windows.Forms.Label();
            this.CopyButton = new System.Windows.Forms.Button();
            this.ViewButton = new System.Windows.Forms.Button();
            this.GroupVersionLabel = new System.Windows.Forms.Label();
            this.FileVersionLabel = new System.Windows.Forms.Label();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.DateAndTypeLabel = new System.Windows.Forms.Label();
            this.UseButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(12, 24);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(334, 177);
            this.TabIndex = 0;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Cornsilk;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.CopyButton, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.ViewButton, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.GroupVersionLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.FileVersionLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.LocationLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.DateAndTypeLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.UseButton, 2, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 14);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(311, 148);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.FolderUnitLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(84, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(224, 19);
            this.panel2.TabIndex = 1;
            // 
            // FolderUnitLabel
            // 
            this.FolderUnitLabel.AutoSize = true;
            this.FolderUnitLabel.Location = new System.Drawing.Point(0, 0);
            this.FolderUnitLabel.Name = "FolderUnitLabel";
            this.FolderUnitLabel.Size = new System.Drawing.Size(35, 13);
            this.FolderUnitLabel.TabIndex = 0;
            this.FolderUnitLabel.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.tableLayoutPanel1.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.FirstVersionLinkLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(84, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(224, 19);
            this.panel3.TabIndex = 4;
            // 
            // FirstVersionLinkLabel
            // 
            this.FirstVersionLinkLabel.AutoSize = true;
            this.FirstVersionLinkLabel.Location = new System.Drawing.Point(4, 5);
            this.FirstVersionLinkLabel.Name = "FirstVersionLinkLabel";
            this.FirstVersionLinkLabel.Size = new System.Drawing.Size(55, 13);
            this.FirstVersionLinkLabel.TabIndex = 0;
            this.FirstVersionLinkLabel.TabStop = true;
            this.FirstVersionLinkLabel.Text = "linkLabel1";
            this.FirstVersionLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.FirstVersionLinkLabel_LinkClicked);
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.tableLayoutPanel1.SetColumnSpan(this.panel4, 2);
            this.panel4.Controls.Add(this.ExterlanLocationlabel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(84, 78);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(224, 38);
            this.panel4.TabIndex = 5;
            // 
            // ExterlanLocationlabel
            // 
            this.ExterlanLocationlabel.AutoSize = true;
            this.ExterlanLocationlabel.Location = new System.Drawing.Point(0, 0);
            this.ExterlanLocationlabel.Name = "ExterlanLocationlabel";
            this.ExterlanLocationlabel.Size = new System.Drawing.Size(35, 13);
            this.ExterlanLocationlabel.TabIndex = 0;
            this.ExterlanLocationlabel.Text = "label1";
            // 
            // CopyButton
            // 
            this.CopyButton.BackColor = System.Drawing.Color.BurlyWood;
            this.CopyButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CopyButton.Location = new System.Drawing.Point(3, 122);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(75, 21);
            this.CopyButton.TabIndex = 6;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = false;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // ViewButton
            // 
            this.ViewButton.BackColor = System.Drawing.Color.BurlyWood;
            this.ViewButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ViewButton.Location = new System.Drawing.Point(84, 122);
            this.ViewButton.Name = "ViewButton";
            this.ViewButton.Size = new System.Drawing.Size(75, 21);
            this.ViewButton.TabIndex = 7;
            this.ViewButton.Text = "View";
            this.ViewButton.UseVisualStyleBackColor = false;
            this.ViewButton.Click += new System.EventHandler(this.ViewButton_Click);
            // 
            // GroupVersionLabel
            // 
            this.GroupVersionLabel.AutoSize = true;
            this.GroupVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupVersionLabel.Location = new System.Drawing.Point(0, 50);
            this.GroupVersionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.GroupVersionLabel.Name = "GroupVersionLabel";
            this.GroupVersionLabel.Size = new System.Drawing.Size(81, 25);
            this.GroupVersionLabel.TabIndex = 8;
            this.GroupVersionLabel.Text = "Group Version";
            // 
            // FileVersionLabel
            // 
            this.FileVersionLabel.AutoSize = true;
            this.FileVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileVersionLabel.Location = new System.Drawing.Point(0, 25);
            this.FileVersionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.FileVersionLabel.Name = "FileVersionLabel";
            this.FileVersionLabel.Size = new System.Drawing.Size(81, 25);
            this.FileVersionLabel.TabIndex = 9;
            this.FileVersionLabel.Text = "File Version";
            // 
            // LocationLabel
            // 
            this.LocationLabel.AutoSize = true;
            this.LocationLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocationLabel.Location = new System.Drawing.Point(0, 75);
            this.LocationLabel.Margin = new System.Windows.Forms.Padding(0);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(81, 44);
            this.LocationLabel.TabIndex = 10;
            this.LocationLabel.Text = "Location";
            // 
            // DateAndTypeLabel
            // 
            this.DateAndTypeLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.DateAndTypeLabel, 3);
            this.DateAndTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateAndTypeLabel.Location = new System.Drawing.Point(0, 0);
            this.DateAndTypeLabel.Margin = new System.Windows.Forms.Padding(0);
            this.DateAndTypeLabel.Name = "DateAndTypeLabel";
            this.DateAndTypeLabel.Size = new System.Drawing.Size(311, 25);
            this.DateAndTypeLabel.TabIndex = 11;
            this.DateAndTypeLabel.Text = "label1";
            // 
            // UseButton
            // 
            this.UseButton.BackColor = System.Drawing.Color.BurlyWood;
            this.UseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UseButton.Location = new System.Drawing.Point(199, 122);
            this.UseButton.Name = "UseButton";
            this.UseButton.Size = new System.Drawing.Size(75, 23);
            this.UseButton.TabIndex = 12;
            this.UseButton.Text = "Use";
            this.UseButton.UseVisualStyleBackColor = false;
            this.UseButton.Click += new System.EventHandler(this.UseButton_Click);
            // 
            // ChangeDesignerForm
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label FolderUnitLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label ExterlanLocationlabel;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button ViewButton;
        private System.Windows.Forms.LinkLabel FirstVersionLinkLabel;
        private System.Windows.Forms.Label GroupVersionLabel;
        private System.Windows.Forms.Label FileVersionLabel;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.Label DateAndTypeLabel;
        private System.Windows.Forms.Button UseButton;
    }
}
