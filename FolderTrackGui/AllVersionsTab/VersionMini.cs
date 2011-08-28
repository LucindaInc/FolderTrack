using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Threading;
using FolderTrackGuiTest1.CopyVersionDialog;
using FolderTrackGuiTest1.UserVersionDialog;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using FolderTrackGuiTest1.CommonGuiFunctions;
using ZetaLongPaths;

namespace FolderTrackGuiTest1.AllVersionsTab
{
    class VersionMini : Panel
    {
        public VersionInfo m_VersionInfo;

        private FTObjects m_FTObjects;

        private delegate void VoidBoolDelegate(bool b);
        private delegate void VoidNoArgDelegate();
        private delegate void VoidStringDelegate(string s);
        private delegate void VoidObjDelegate(object ob);

        bool current;
        Label DeleteLabel;

        public VersionMini()
        {
            InitializeComponent();

            
        }

        public VersionMini(VersionInfo vers, FTObjects ftobjects)
        {
            InitializeComponent();
            m_VersionInfo = vers;
            m_FTObjects = ftobjects;
            SetVersion();
        }

        public VersionMini(VersionInfo vers, FTObjects ftobjects, bool removed)
        {
            InitializeComponent();
            
                m_VersionInfo = vers;
                m_FTObjects = ftobjects;
                SetVersion();
                if (removed == true)
                {
                    iSetRemoved(true);
                }
            
           
          
        }

        public void SetVersion()
        {
            
            
                this.VersionLabel.Text = m_VersionInfo.versionName;
                this.DateLabel.Text = string.Format("{0:ddd, dd MMM yyyy}", m_VersionInfo.date) + " " + m_VersionInfo.date.ToLongTimeString();
                if (m_VersionInfo.UserVersName != null)
                {
                    String[] userVerArr = new string[m_VersionInfo.UserVersName.Count];
                    m_VersionInfo.UserVersName.CopyTo(userVerArr);
                    this.UserVersionListBox.Items.AddRange(userVerArr);
                }

                if (m_VersionInfo.changesInVersion != null)
                {
                    String[] changes = new string[m_VersionInfo.changesInVersion.Count];
                    for (int i = 0; i < m_VersionInfo.ChangesInVersion.Count; i++)
                    {
                        if (m_VersionInfo.ChangesInVersion[i].change == ChangeType.Rename)
                        {
                            changes[i] = m_VersionInfo.ChangesInVersion[i].change.ToString()[0] + ": " + m_VersionInfo.ChangesInVersion[i].folderUnit.oldLocation + " to " + m_VersionInfo.ChangesInVersion[i].externalLocation;
                        }
                        else
                        {
                            changes[i] = m_VersionInfo.ChangesInVersion[i].change.ToString()[0] + ": " + m_VersionInfo.ChangesInVersion[i].externalLocation;
                        }
                    }
                    this.ChangeFileListBox.Items.AddRange(changes);
                }

                if (m_VersionInfo.FreeText != null)
                {
                    this.DiscriptionTextBox.Text = m_VersionInfo.FreeText;
                }
            
        }

        private void UseButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Use Button Clicked on Relationship");
            new Thread(new ParameterizedThreadStart(SetVersion)).Start(this.m_VersionInfo.versionName);
        }

        private void SetVersion(object vesionNameO)
        {
            Util.UserDebug("User Button Clicked in Relationship had version " + (string)vesionNameO);
            m_FTObjects.SetVersion((string)vesionNameO);
        }

        

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Copy Button Clicked in Relationship");
            IList<string> moLoca = m_FTObjects.GetLocationsInMonitorGroup();

            CopyVersionForm copyForm = new CopyVersionForm(moLoca);
            copyForm.ShowDialog();
            if (copyForm.DialogResult == DialogResult.OK)
            {
                Dictionary<string, string> ToLocations = copyForm.CopyLocFromExtLoc;
                m_FTObjects.CopyVersion(this.m_VersionInfo.versionName, ToLocations);
            }
        }

        private void Exploreutton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Explore Button Clicked in Relationship");
            this.m_FTObjects.ExploreVersion(m_VersionInfo.versionName);
        }


        void textBox1_TextChanged(object sender, EventArgs e)
        {
            new Thread(EnableSaveDiscription).Start();
        }

        private void EnableSaveDiscription()
        {
            if (this.SaveDiscriptionButton.InvokeRequired)
            {
                this.SaveDiscriptionButton.Invoke(new VoidNoArgDelegate(EnableSaveDiscription));
                return;
            }
            this.SaveDiscriptionButton.Text = "Save Description";
            this.SaveDiscriptionButton.Enabled = true;
        }

        private void UnSaveDisciption()
        {
            if (this.SaveDiscriptionButton.InvokeRequired)
            {
                this.SaveDiscriptionButton.Invoke(new VoidNoArgDelegate(UnSaveDisciption));
                return;
            }
            this.SaveDiscriptionButton.Text = "Edit Description To Save";
            this.SaveDiscriptionButton.Enabled = false;
        }

        private void AddUserVersionButton_Click(object sender, EventArgs e)
        {
            GuiFunctions.AddUserVersionButton_Click(this.m_VersionInfo, e, this);
        }

        private void SaveDiscriptionButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Save Discription clicked on version " + this.m_VersionInfo.versionName + " in Relationship");
            new Thread(SendDiscription).Start();
        }

        private void SendDiscription()
        {
            if (this.DiscriptionTextBox.InvokeRequired)
            {
                this.DiscriptionTextBox.Invoke(new VoidNoArgDelegate(SendDiscription));
                return;
            }
            Util.UserDebug("Saving in version " + this.m_VersionInfo.versionName + " this text :" + this.DiscriptionTextBox.Text);
            m_FTObjects.SetDiscription(this.m_VersionInfo.versionName, this.DiscriptionTextBox.Text);
            UnSaveDisciption();
        }

        public void SetDiscription(string discrip)
        {
            if (this.DiscriptionTextBox.InvokeRequired)
            {
                this.DiscriptionTextBox.Invoke(new VoidStringDelegate(SetDiscription), new object[] { discrip });
                return;
            }
            this.DiscriptionTextBox.Text = discrip;
        }

        private void this_Paint(object sender, PaintEventArgs e)
        {
            Pen pe;
         
            if (current)
            {
                pe = new Pen(Color.RoyalBlue, 5);
        
            }
            else
            {
                pe = new Pen(Color.Black, 5);
  
            }
            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(System.Drawing.SystemColors.Control);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(3, 7);
            pt[1] = new Point(this.Width - 4, 7);
            pt[2] = new Point(this.Width - 4, this.Height -8);
            pt[3] = new Point(3, this.Height - 8);
            pt[4] = new Point(3, 7);

            e.Graphics.DrawLines(pe, pt);
        }

        private void ChangeFileListBox_DoubleClick(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(HandleDoubleCick)).Start((object)this.ChangeFileListBox.SelectedItem);
        }
        private void HandleDoubleCick(object selectedFileO)
        {
            Util.UserDebug("Change File double clicked on version " + this.m_VersionInfo.versionName);

            string selectedFile = (string)selectedFileO;

            bool justShow = false;

            if (selectedFile != null)
            {
                Util.UserDebug("File double clicked on version " + this.m_VersionInfo.versionName + " is " + selectedFile);
                int firstspace = selectedFile.IndexOf(' ');
                selectedFile = selectedFile.Substring(firstspace + 1);

                //determine if this is a folder
                foreach (ChangeInstruction chi in m_VersionInfo.ChangesInVersion)
                {
                    if (chi.externalLocation.Equals(selectedFile))
                    {
                        if (chi.folderUnit.type == Delimiter.FILE)
                        {
                            justShow = true;
                            break;
                        }
                    }
                }

                if (justShow)
                {

                    string VersionFile = selectedFile;

                    string PreDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                                 + Path.DirectorySeparatorChar + Util.Company + Path.DirectorySeparatorChar
                                 + Util.Product
                                 + Settings.Default.tempDir
                                 + Path.DirectorySeparatorChar;
                    
                    string CopyTo = PreDir +
                                    ZlpPathHelper.GetFileNameFromFilePath(selectedFile);

                    int uniqueFileInt = 0;
                    while (ZlpIOHelper.FileExists(CopyTo) || ZlpIOHelper.DirectoryExists(CopyTo))
                    {

                        CopyTo = PreDir +
                                 Convert.ToString(uniqueFileInt) +
                                 Path.DirectorySeparatorChar +
                                 ZlpPathHelper.GetFileNameFromFilePath(selectedFile);
                        uniqueFileInt++;
                    }
                    
                    ZlpIOHelper.CreateDirectory(ZlpPathHelper.GetDirectoryNameFromFilePath(CopyTo));


                    m_FTObjects.dataReceiver.CopyFolderUnit(m_FTObjects.CurrentMonitorGroup, m_VersionInfo, VersionFile, CopyTo);

                    try
                    {
                        System.Diagnostics.Process.Start(CopyTo);
                    }
                    catch (Win32Exception ex)
                    {
                        if (ex.NativeErrorCode == 1155)
                        {
                            FTMethods.OpenAs(CopyTo);
                        }
                        //TODO Figure out what to do with errors may need to checkout
                        //that frog tool
                        // ex.StackTrace;
                    }
                }
                else
                {

                    showCopyDlgForDBlClk(selectedFile);
                }
            }

        }

        public void DontMonitor(bool dontMonitor)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidBoolDelegate(DontMonitor), new object[] { dontMonitor });
                return;
            }

            this.UseButton.Visible = !dontMonitor;
        }

        private void showCopyDlgForDBlClk(object locat)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidObjDelegate(showCopyDlgForDBlClk), new object[] { locat });
                return;
            }
            string selectedFile = (string)locat;
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
            m_FTObjects.CopyVersion(this.m_VersionInfo.versionName, ToLocations);
        }

        public void SetRemoved(bool removed)
        {
            new Thread(new ParameterizedThreadStart(tSetRemoved)).Start(removed);
        }
        private void tSetRemoved(object removed)
        {
            this.Invoke(new VoidBoolDelegate(iSetRemoved), new object[] { (bool) removed });
        }

        private void iSetRemoved(bool removed)
        {
            if (removed == true)
            {
                this.Controls.Remove(this.VersionTableLayoutPanel);
                DeleteLabel = new Label();
                DeleteLabel.Text = m_VersionInfo.versionName;
                DeleteLabel.TextAlign = ContentAlignment.MiddleCenter;
                DeleteLabel.Dock = DockStyle.Fill;
                this.Controls.Add(DeleteLabel);
            }
            else
            {
                this.Controls.Clear();
                DeleteLabel = null;
                this.Controls.Add(this.VersionTableLayoutPanel);
                
            }
            
        }



        public void SetCurrent(bool current)
        {

            if (this.panel2.InvokeRequired == true)
            {
                this.panel2.Invoke(new VoidBoolDelegate(SetCurrent), new object[] { current });
                return;
            }

            this.current = current;
            if (current)
            {
                panel2.BackColor = Color.LightBlue;
                VersionTableLayoutPanel.BackColor = Color.LightBlue;
                VersionLabel.BackColor = Color.LightBlue;
                DateLabel.BackColor = Color.LightBlue;
                AddUserVersionButton.BackColor = Color.LightSteelBlue;
                ChangeFileListBox.BackColor = Color.LightBlue;
                DiscriptionTextBox.BackColor = Color.LightBlue;
                SaveDiscriptionButton.BackColor = Color.LightSteelBlue;
                UseButton.BackColor = Color.LightSteelBlue;
                Exploreutton.BackColor = Color.LightSteelBlue;
                CopyButton.BackColor = Color.LightSteelBlue;
                UserVersionListBox.BackColor = Color.LightBlue;
            }
            else
            {
                panel2.BackColor = Color.White;
                VersionTableLayoutPanel.BackColor = Color.White;
                VersionLabel.BackColor = Color.White;
                DateLabel.BackColor = Color.White;
                AddUserVersionButton.BackColor = Color.Gainsboro;
                ChangeFileListBox.BackColor = Color.White;
                DiscriptionTextBox.BackColor = Color.White;
                SaveDiscriptionButton.BackColor = Color.Gainsboro;
                UseButton.BackColor = Color.Gainsboro;
                Exploreutton.BackColor = Color.Gainsboro;
                CopyButton.BackColor = Color.Gainsboro;
                UserVersionListBox.BackColor = Color.White;
            }

        }
        


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
            this.VersionTableLayoutPanel.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.VersionTableLayoutPanel);
            this.Location = new System.Drawing.Point(12, 68);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(382, 208);
            this.TabIndex = 1;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this_Paint);
            // 
            // VersionMiniDesigner
            // 
            this.VersionTableLayoutPanel.ResumeLayout(false);
            this.VersionTableLayoutPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel VersionTableLayoutPanel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Button AddUserVersionButton;
        private System.Windows.Forms.ListBox ChangeFileListBox;
        private System.Windows.Forms.TextBox DiscriptionTextBox;
        private System.Windows.Forms.Button SaveDiscriptionButton;
        private System.Windows.Forms.Button UseButton;
        private System.Windows.Forms.Button Exploreutton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.ListBox UserVersionListBox;
    }
}
