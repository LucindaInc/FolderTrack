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

namespace FolderTrackGuiTest1.VersionPanels
{
    class VersionMini : Panel, PanelList<VersionInfo>.PanelFunction, PanelList<VersionInfo>.CallAllDa
    {
        public VersionInfo m_VersionInfo;

        private FTObjects m_FTObjects;

        public const int DONT_USE = 1;
        public const int USE = 2;

        private delegate void VoidBoolDelegate(bool b);
        private delegate void VoidNoArgDelegate();
        private delegate void VoidStringDelegate(string s);
        private delegate void VoidObjDelegate(object ob);
        bool current;
        public Dictionary<int, string> IndexToErrorString;

        public VersionMini()
        {
            InitializeComponent();
        }

        public VersionMini(VersionInfo vers, FTObjects ftobjects)
        {
            InitializeComponent();
            m_VersionInfo = vers;
            m_FTObjects = ftobjects;
            IndexToErrorString = new Dictionary<int, string>();
            SetVersion();
        }

        public void SetVersion()
        {
            this.VersionLabel.Text = m_VersionInfo.versionName;
            this.DateLabel.Text = string.Format("{0:ddd, dd MMM yyyy}", m_VersionInfo.date) +" "+m_VersionInfo.date.ToLongTimeString();
            if (m_VersionInfo.UserVersName != null)
            {
                String[] userVerArr = new string[m_VersionInfo.UserVersName.Count];
                m_VersionInfo.UserVersName.CopyTo(userVerArr);
                this.UserVersionListBox.Items.AddRange(userVerArr);
            }
            Size sizeMeasure;
            int max = 0;
            bool filterd;
            string changeTypS;

            if (m_VersionInfo.changesInVersion != null)
            {
                String[] changes = new string[m_VersionInfo.changesInVersion.Count];
                for (int i = 0; i < m_VersionInfo.ChangesInVersion.Count; i++)
                {
                    if (i != 0)
                    {
                        sizeMeasure = TextRenderer.MeasureText(changes[i - 1], ChangeFileListBox.Font);
                        if (sizeMeasure.Width > max)
                        {
                            max = sizeMeasure.Width;
                        }
                    }
                    filterd = false;
                    if (m_VersionInfo.ChangesInVersion[i].FilteredList != null && m_VersionInfo.ChangesInVersion[i].FilteredList.Count > 0)
                    {
                        changeTypS = "Filtered";
                        filterd = true;
                    }
                    else
                    {
                        changeTypS = m_VersionInfo.ChangesInVersion[i].change.ToString();
                    }

                    if (m_VersionInfo.ChangesInVersion[i].change == ChangeType.Rename)
                    {
                        changes[i] = changeTypS + ": " + m_VersionInfo.ChangesInVersion[i].folderUnit.oldLocation + " to " + m_VersionInfo.ChangesInVersion[i].externalLocation;
                    }
                    else
                    {
                        changes[i] = changeTypS + ": " + m_VersionInfo.ChangesInVersion[i].externalLocation;
                    }
                    if (m_VersionInfo.ErrorList != null)
                    {
                        foreach (ErrorInfo erInter in m_VersionInfo.ErrorList.Values)
                        {
                            if (erInter.ChangeInstruction.externalLocation.Equals(m_VersionInfo.ChangesInVersion[i].externalLocation))
                            {
                                changes[i] = "Could not read " + changes[i];
                                IndexToErrorString[i] = erInter.Error;
                                break;
                            }
                        }

                    }

                    if (filterd)
                    {
                        changes[i] += " (because of filter";
                        if (m_VersionInfo.ChangesInVersion[i].FilteredList.Count > 1)
                        {
                            changes[i] += "s";
                        }
                        foreach (string fr in m_VersionInfo.ChangesInVersion[i].FilteredList)
                        {
                            changes[i] += " " + fr;
                        }
                        changes[i] += ")";
                    }
                }
                sizeMeasure = TextRenderer.MeasureText(changes[changes.Length - 1], ChangeFileListBox.Font);
                if (sizeMeasure.Width > max)
                {
                    max = sizeMeasure.Width;
                }
                this.ChangeFileListBox.Items.AddRange(changes);

                this.ChangeFileListBox.HorizontalExtent = max;


            }


            if (m_VersionInfo.FreeText != null)
            {
                this.DiscriptionTextBox.Text = m_VersionInfo.FreeText;
            }
            if(m_VersionInfo.ErrorList.Count > 0)
            {
                if (m_VersionInfo.Removed)
                {
                    
                    SetRe();
                }
                else
                {
                    SetErr();
                }
            }
            else if (m_VersionInfo.Removed == true)
            {
                SetRemove();
            }
        }

        private void UseButton_Click(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(SetVersion)).Start(this.m_VersionInfo.versionName);
        }

        private void SetVersion(object vesionNameO)
        {
            m_FTObjects.SetVersion((string)vesionNameO);
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
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
            this.m_FTObjects.ExploreVersion(m_VersionInfo.versionName);
        }

        private void AddUserVersionButton_Click(object sender, EventArgs e)
        {
            GuiFunctions.AddUserVersionButton_Click(this.m_VersionInfo, e, this);
        }

        private void SaveDiscriptionButton_Click(object sender, EventArgs e)
        {
            new Thread(SendDiscription).Start();
        }

        private void SendDiscription()
        {
            if (this.DiscriptionTextBox.InvokeRequired)
            {
                this.DiscriptionTextBox.Invoke(new VoidNoArgDelegate(SendDiscription));
                return;
            }
            m_FTObjects.SetDiscription(this.m_VersionInfo.versionName, this.DiscriptionTextBox.Text);
            this.SaveDiscriptionButton.Enabled = false;
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
            Color clear;
            if (current)
            {
                pe = new Pen(Color.RoyalBlue, 17);
                
            }
            else if (m_VersionInfo.Removed)
            {
                pe = new Pen(Color.Gray, 17);
            }
            else
            {
                pe = new Pen(Color.Black, 17);
                
            }


            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(System.Drawing.SystemColors.Control);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(12, 12);
            pt[1] = new Point(this.Width - 12, 12);
            pt[2] = new Point(this.Width - 12, this.Height - 12);
            pt[3] = new Point(12, this.Height - 12);
            pt[4] = new Point(12, 12);

            e.Graphics.DrawLines(pe, pt);
        }

        private void ChangeFileListBox_DoubleClick(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(HandleDoubleCick)).Start((object)this.ChangeFileListBox.SelectedItem);
        }

        private void ChangeFileListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            //from http://stackoverflow.com/questions/91747/background-color-of-a-listbox-item-winforms
            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            int index = e.Index;
            if (index >= 0 && index < ChangeFileListBox.Items.Count)
            {
                string text = ChangeFileListBox.Items[index].ToString();
                Graphics g = e.Graphics;
                //   Color color = (selected) ? Color.FromKnownColor(KnownColor.Highlight) : (((index % 2) == 0) ? Color.White : Color.Gray);                 
                if (IndexToErrorString.ContainsKey(index) == false)
                {
                    if (m_VersionInfo.Removed == false)
                    {
                        g.FillRectangle(new SolidBrush(Color.White), e.Bounds);                  // Print text                 
                        g.DrawString(text, e.Font, new SolidBrush(Color.Black), ChangeFileListBox.GetItemRectangle(index).Location);
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), e.Bounds);                  // Print text                 
                        g.DrawString(text, e.Font, new SolidBrush(Color.White), ChangeFileListBox.GetItemRectangle(index).Location);
                    }
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.Red), e.Bounds);                  // Print text                 
                    g.DrawString(text, e.Font, new SolidBrush(Color.White), ChangeFileListBox.GetItemRectangle(index).Location);
                }
            }
            e.DrawFocusRectangle();
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

        private void SetColor(Color back, Color button, Color f4)
        {
            this.VersionTableLayoutPanel.BackColor = back;
            this.VersionLabel.BackColor = back;
            this.DateLabel.BackColor = back;
            this.UserVersionListBox.BackColor = back;
            this.SaveDiscriptionButton.BackColor = button;
            this.panel2.BackColor = back;
            this.UseButton.BackColor = button;
            this.CopyButton.BackColor = button;
            this.Exploreutton.BackColor = button;
            this.AddUserVersionButton.BackColor = button;
            this.DiscriptionTextBox.BackColor = back;
            this.ChangeFileListBox.BackColor = back;

            this.VersionTableLayoutPanel.ForeColor = f4;
            this.VersionLabel.ForeColor = f4;
            this.DateLabel.ForeColor = f4;
            this.UserVersionListBox.ForeColor = f4;
            this.SaveDiscriptionButton.ForeColor = f4;
            this.panel2.ForeColor = f4;
            this.UseButton.ForeColor = f4;
            this.CopyButton.ForeColor = f4;
            this.Exploreutton.ForeColor = f4;
            this.AddUserVersionButton.ForeColor = f4;
            this.DiscriptionTextBox.ForeColor = f4;
            this.ChangeFileListBox.ForeColor = f4;
        }

        private void SetRemove()
        {
            SetColor(Color.Black, Color.Gray, Color.White);
        }

        private void SetRe()
        {
            SetColor(Color.Black, Color.Red, Color.White);
        }
        private void SetErr()
        {
            SetColor(Color.RosyBrown, Color.White, Color.Black);
            
        }
        private void SetUnRemove()
        {
            SetCurrent(false);
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
                if (m_VersionInfo.ErrorList.Count > 0)
                {
                    SetErr();
                }
                else
                {
                    SetColor(Color.White, Color.Gainsboro, Color.Black);
                }
            }

        }

        private System.ComponentModel.IContainer components = null;

        void textBox1_TextChanged(object sender, EventArgs e)
        {
            //Handle button
        }

        

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
            this.DateLabel = new System.Windows.Forms.Label();
            this.UserVersionListBox = new System.Windows.Forms.ListBox();
            this.SaveDiscriptionButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.UseButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.Exploreutton = new System.Windows.Forms.Button();
            this.AddUserVersionButton = new System.Windows.Forms.Button();
            this.DiscriptionTextBox = new System.Windows.Forms.TextBox();
            this.ChangeFileListBox = new System.Windows.Forms.ListBox();
            this.VersionTableLayoutPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // VersionTableLayoutPanel
            // 
            this.VersionTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.VersionTableLayoutPanel.AutoSize = true;
            this.VersionTableLayoutPanel.BackColor = System.Drawing.Color.White;
            this.VersionTableLayoutPanel.ColumnCount = 7;
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.VersionTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.VersionTableLayoutPanel.Controls.Add(this.VersionLabel, 0, 0);
            this.VersionTableLayoutPanel.Controls.Add(this.DateLabel, 2, 0);
            this.VersionTableLayoutPanel.Controls.Add(this.UserVersionListBox, 1, 2);
            this.VersionTableLayoutPanel.Controls.Add(this.SaveDiscriptionButton, 3, 3);
            this.VersionTableLayoutPanel.Controls.Add(this.panel2, 0, 2);
            this.VersionTableLayoutPanel.Controls.Add(this.AddUserVersionButton, 1, 3);
            this.VersionTableLayoutPanel.Controls.Add(this.DiscriptionTextBox, 3, 2);
            this.VersionTableLayoutPanel.Controls.Add(this.ChangeFileListBox, 0, 1);
            this.VersionTableLayoutPanel.Location = new System.Drawing.Point(9, 7);
            this.VersionTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.VersionTableLayoutPanel.Name = "VersionTableLayoutPanel";
            this.VersionTableLayoutPanel.RowCount = 4;
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.VersionTableLayoutPanel.Size = new System.Drawing.Size(619, 200);
            this.VersionTableLayoutPanel.TabIndex = 0;
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
            this.VersionLabel.Size = new System.Drawing.Size(241, 35);
            this.VersionLabel.TabIndex = 0;
            this.VersionLabel.Text = "1";
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.BackColor = System.Drawing.Color.White;
            this.VersionTableLayoutPanel.SetColumnSpan(this.DateLabel, 5);
            this.DateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateLabel.Location = new System.Drawing.Point(241, 0);
            this.DateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(378, 35);
            this.DateLabel.TabIndex = 0;
            this.DateLabel.Text = " December 12, 2009";
            this.DateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // UserVersionListBox
            // 
            this.UserVersionListBox.BackColor = System.Drawing.Color.White;
            this.UserVersionListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VersionTableLayoutPanel.SetColumnSpan(this.UserVersionListBox, 2);
            this.UserVersionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserVersionListBox.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserVersionListBox.FormattingEnabled = true;
            this.UserVersionListBox.HorizontalScrollbar = true;
            this.UserVersionListBox.ItemHeight = 24;
            this.UserVersionListBox.Location = new System.Drawing.Point(83, 116);
            this.UserVersionListBox.Name = "UserVersionListBox";
            this.UserVersionListBox.Size = new System.Drawing.Size(262, 50);
            this.UserVersionListBox.TabIndex = 4;
            // 
            // SaveDiscriptionButton
            // 
            this.SaveDiscriptionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SaveDiscriptionButton.AutoSize = true;
            this.SaveDiscriptionButton.BackColor = System.Drawing.Color.Gainsboro;
            this.VersionTableLayoutPanel.SetColumnSpan(this.SaveDiscriptionButton, 4);
            this.SaveDiscriptionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SaveDiscriptionButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveDiscriptionButton.Location = new System.Drawing.Point(416, 173);
            this.SaveDiscriptionButton.Name = "SaveDiscriptionButton";
            this.SaveDiscriptionButton.Size = new System.Drawing.Size(135, 23);
            this.SaveDiscriptionButton.TabIndex = 10;
            this.SaveDiscriptionButton.Text = "Edit Description To Save";
            this.SaveDiscriptionButton.UseVisualStyleBackColor = false;
            this.SaveDiscriptionButton.Click += new System.EventHandler(this.SaveDiscriptionButton_Click);
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
            this.panel2.TabIndex = 2;
            // 
            // UseButton
            // 
            this.UseButton.BackColor = System.Drawing.Color.Gainsboro;
            this.UseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UseButton.Location = new System.Drawing.Point(3, 3);
            this.UseButton.Name = "UseButton";
            this.UseButton.Size = new System.Drawing.Size(69, 23);
            this.UseButton.TabIndex = 3;
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
            this.CopyButton.TabIndex = 7;
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
            this.Exploreutton.TabIndex = 8;
            this.Exploreutton.Text = "Explore";
            this.Exploreutton.UseVisualStyleBackColor = false;
            this.Exploreutton.Click += new System.EventHandler(this.Exploreutton_Click);
            // 
            // AddUserVersionButton
            // 
            this.AddUserVersionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AddUserVersionButton.AutoSize = true;
            this.AddUserVersionButton.BackColor = System.Drawing.Color.Gainsboro;
            this.VersionTableLayoutPanel.SetColumnSpan(this.AddUserVersionButton, 2);
            this.AddUserVersionButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddUserVersionButton.Location = new System.Drawing.Point(162, 173);
            this.AddUserVersionButton.Name = "AddUserVersionButton";
            this.AddUserVersionButton.Size = new System.Drawing.Size(103, 23);
            this.AddUserVersionButton.TabIndex = 9;
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
            this.DiscriptionTextBox.Location = new System.Drawing.Point(351, 116);
            this.DiscriptionTextBox.Multiline = true;
            this.DiscriptionTextBox.Name = "DiscriptionTextBox";
            this.DiscriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DiscriptionTextBox.Size = new System.Drawing.Size(265, 51);
            this.DiscriptionTextBox.TabIndex = 5;
            this.DiscriptionTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ChangeFileListBox
            // 
            this.ChangeFileListBox.BackColor = System.Drawing.Color.White;
            this.ChangeFileListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VersionTableLayoutPanel.SetColumnSpan(this.ChangeFileListBox, 7);
            this.ChangeFileListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeFileListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ChangeFileListBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChangeFileListBox.FormattingEnabled = true;
            this.ChangeFileListBox.HorizontalScrollbar = true;
            this.ChangeFileListBox.ItemHeight = 14;
            this.ChangeFileListBox.Location = new System.Drawing.Point(3, 38);
            this.ChangeFileListBox.Name = "ChangeFileListBox";
            this.ChangeFileListBox.Size = new System.Drawing.Size(613, 72);
            this.ChangeFileListBox.TabIndex = 1;
            this.ChangeFileListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ChangeFileListBox_DrawItem);
            this.ChangeFileListBox.DoubleClick += new System.EventHandler(this.ChangeFileListBox_DoubleClick);
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.VersionTableLayoutPanel);
            this.Location = new System.Drawing.Point(12, 68);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(637, 214);
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

        #region PanelFunction Members

        public void DoFunction(object data)
        {
            if(data is Boolean)
            {
                SetCurrent((Boolean)data);
            }
            else if (data is FolderTrackGuiTest1.VersionRow.VersionOption)
            {
                if (((FolderTrackGuiTest1.VersionRow.VersionOption)data) == 
                    FolderTrackGuiTest1.VersionRow.VersionOption.REMOVED)
                {
                    this.m_VersionInfo.Removed = true;
                 //   new Thread(tSetDeleteText).Start();
                    this.Invoke(new VoidNoArgDelegate(SetRemove));
                }
                else if (((FolderTrackGuiTest1.VersionRow.VersionOption)data) == 
                    FolderTrackGuiTest1.VersionRow.VersionOption.NOT_REMOVED)
                {
                    this.m_VersionInfo.Removed = false;
                //    new Thread(tSetDeleteText).Start();
                    this.Invoke(new VoidNoArgDelegate(SetUnRemove));
                }
            }
        }

        #endregion

        #region CallAllDa Members

        public void CallAll(object data)
        {
            if (this.UseButton.InvokeRequired)
            {
                this.UseButton.Invoke(new VoidObjDelegate(CallAll), new object[] { data });
                return;
            }

            if (data is Int32)
            {
                int i = (Int32)data;
                if (i == DONT_USE)
                {
                    this.UseButton.Visible = false;
                }
                if (i == USE)
                {
                    this.UseButton.Visible = true;
                }
            }
        }



        #endregion
    }
}
