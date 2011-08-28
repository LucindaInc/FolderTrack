using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using FolderTrackGuiTest1.CopyVersionDialog;
using FolderTrackGuiTest1.UserVersionDialog;
using System.Drawing;
using FolderTrackGuiTest1.CommonGuiFunctions;
using ZetaLongPaths;
using FolderTrackGuiTest1.Delete;

namespace FolderTrackGuiTest1
{
    public class VersionRow : Panel, PanelList<VersionInfo>.PanelFunction, PanelList<VersionInfo>.CallAllDa
    {
        

        public const int DONT_USE = 1;
        public const int USE = 2;

        private VersionInfo m_VersionInfo;
        private FTObjects m_FTObjects;
        private UnRemove m_unremove;
        private FolderUnit selec = null;
        List<string> FilterStr;
        
        private delegate void SetCurrentVersionDelegate();
        private delegate void UnSetCurrentVersionDelegate();
        private delegate void VoidNoArgDelegate();
        private delegate void VoidBoolDelegate(bool bbool);
        private delegate void VoidObjDelegate(object ob);
        bool current=false;

        public Dictionary<int, string> IndexToErrorString;
        
        public VersionRow()
        {
            InitializeComponent();
        }

        public enum VersionOption
        {
            REMOVED,
            NOT_REMOVED
        }

        public VersionRow(VersionInfo versionInfo, FTObjects ftobjects)
        {

            InitializeInfo(versionInfo, ftobjects);

        }

        public VersionRow(VersionInfo versionInfo, FTObjects ftobjects, UnRemove unremove)
        {

            InitializeInfo(versionInfo, ftobjects);
            m_unremove = unremove;

        }

        private void InitializeInfo(VersionInfo versionInfo, FTObjects ftobjects)
        {
            selec = versionInfo.ChangesInVersion[0].folderUnit;
            FilterStr = versionInfo.ChangesInVersion[0].FilteredList;
            InitializeComponent();
            this.m_FTObjects = ftobjects;
            IndexToErrorString = new Dictionary<int, string>();
            SetVersionInfo(versionInfo );
        }

        private void VersionTableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }


        private void FilterButton_Click(object sender, EventArgs e)
        {
            GuiFunctions.HandleFilters(selec, FilterStr);
        }
      
        private void FilterButton_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.TranslateTransform(28 , 8);
            e.Graphics.RotateTransform(90);
            string text = "";
            if (selec != null)
            {
                text = ZlpPathHelper.GetFileNameFromFilePath(selec.externalLocation);
                int len = text.Length;
                if (len > 5)
                {
                    len = 5;
                    text = text.Substring(0, len);
                    text += "...";
                }
                
            }
            Brush bruCol;
            if (m_VersionInfo.Removed)
            {
                bruCol = Brushes.White;
            }
            else
            {
                bruCol = Brushes.Black;
            }
            e.Graphics.DrawString("Filter " + text, new Font("Microsoft Sans Serif", 8.25F), bruCol, 0, 10);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.SaveDiscriptionButton.Text = "Save";
        }

        private void Exploreutton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Explore button clicked in All Version List on " + this.m_VersionInfo.versionName);
            this.m_FTObjects.ExploreVersion(m_VersionInfo.versionName);
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
            this.VersionTableLayoutPanel.Size = new System.Drawing.Size(327, 200);
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
            this.DateLabel.Size = new System.Drawing.Size(167, 35);
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
            this.AddUserVersionButton.Location = new System.Drawing.Point(118, 173);
            this.AddUserVersionButton.Name = "AddUserVersionButton";
            this.AddUserVersionButton.Size = new System.Drawing.Size(103, 23);
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
            this.DiscriptionTextBox.Size = new System.Drawing.Size(61, 51);
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
            this.ChangeFileListBox.Size = new System.Drawing.Size(292, 72);
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
            this.SaveDiscriptionButton.Size = new System.Drawing.Size(61, 24);
            this.SaveDiscriptionButton.TabIndex = 12;
            this.SaveDiscriptionButton.Text = "Save Description";
            this.SaveDiscriptionButton.UseVisualStyleBackColor = false;
            this.SaveDiscriptionButton.Click += new System.EventHandler(this.SaveDiscriptionButton_Click);
            // 
            // FilterButton
            // 
            this.FilterButton.BackColor = System.Drawing.Color.Gainsboro;
            this.FilterButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FilterButton.Location = new System.Drawing.Point(301, 38);
            this.FilterButton.Name = "FilterButton";
            this.FilterButton.Size = new System.Drawing.Size(23, 72);
            this.FilterButton.TabIndex = 18;
            this.FilterButton.UseVisualStyleBackColor = false;
            this.FilterButton.Paint += new System.Windows.Forms.PaintEventHandler(this.FilterButton_Paint);
            this.FilterButton.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // panel1
            // 
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.VersionTableLayoutPanel);
            this.Location = new System.Drawing.Point(12, 68);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(359, 214);
            this.TabIndex = 1;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this_Paint);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.AutoSize = true;
            this.deleteButton.BackColor = System.Drawing.Color.DarkRed;
            this.VersionTableLayoutPanel.SetColumnSpan(this.deleteButton, 3);
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deleteButton.ForeColor = System.Drawing.Color.White;
            this.deleteButton.Location = new System.Drawing.Point(182, 173);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 19;
            this.deleteButton.Text = "Remove";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // Form2
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
        private System.Windows.Forms.Label ChangesLabel;
        private System.Windows.Forms.Label UserVersionLabel;
        private System.Windows.Forms.Label DiscriptionLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button FilterButton;
        private System.Windows.Forms.Button deleteButton;
		
        private void this_Paint(object sender, PaintEventArgs e)
        {
            Pen pe;
            Color clear;
            if (current)
            {
                pe = new Pen(Color.RoyalBlue, 7);
                clear = System.Drawing.Color.White;
            }
            else if (m_VersionInfo.Removed)
            {
                pe = new Pen(Color.Gray, 7);
                clear = System.Drawing.Color.White;
            }
            else
            {
                pe = new Pen(Color.Black, 7);
                clear = System.Drawing.Color.White;
            }

            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(clear);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(13, 8);
            pt[1] = new Point(this.Width - 13, 8);
            pt[2] = new Point(this.Width - 13, this.Height - 8);
            pt[3] = new Point(13, this.Height - 8);
            pt[4] = new Point(13, 8);

            e.Graphics.DrawLines(pe, pt);

           
            

        }

        void AddUserVersionButton_Click(object sender, EventArgs e)
        {
            GuiFunctions.AddUserVersionButton_Click(this.m_VersionInfo, e, this);
        }

        void CopyButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Copy Button Clicked in All Version List on version " + this.m_VersionInfo.versionName);
            IList<string> moLoca = m_FTObjects.GetLocationsInMonitorGroup();

            CopyVersionForm copyForm = new CopyVersionForm(moLoca);
            copyForm.ShowDialog();
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

        void UseButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Use Button Clicked in All Version List on version " + this.m_VersionInfo.versionName);
            new Thread(new ParameterizedThreadStart(SetVersion)).Start(this.m_VersionInfo.versionName);
        }

        private void SetVersion(object vesionNameO)
        {
            m_FTObjects.SetVersion((string) vesionNameO);
        }

        public void UnIndicateCurrentVersion()
        {
            new Thread(UnInvokeCurrentVersion).Start();
        }

        private void UnInvokeCurrentVersion()
        {
            this.Invoke(new UnSetCurrentVersionDelegate(UnSetCurrentVersion));
        }

        private void UnSetCurrentVersion()
        {
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.VersionLabel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.DateLabel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.DiscriptionTextBox.BackColor = System.Drawing.Color.SteelBlue;
            this.ChangeFileListBox.BackColor = System.Drawing.Color.SteelBlue;
            this.UserVersionListBox.BackColor = System.Drawing.Color.SteelBlue;
        }



        void DiscriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            this.SaveDiscriptionButton.Text = "Save";
            this.SaveDiscriptionButton.Enabled = true;
        }

        void SaveDiscriptionButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Save Discription Button clicked in All Version Panel List on version " + this.m_VersionInfo.versionName);
            m_FTObjects.SetDiscription(this.m_VersionInfo.versionName, this.DiscriptionTextBox.Text);
            Util.UserDebug("Save Discription text is " + this.DiscriptionTextBox.Text);
            this.SaveDiscriptionButton.Enabled = false;
            this.SaveDiscriptionButton.Text = "Edit Description To Save";
        }

        private void ChangeFileListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(HandleChangeFileListBoxValueChanged)).Start((object) this.ChangeFileListBox.SelectedIndex);
        }

        private void HandleChangeFileListBoxValueChanged(object newValO)
        {
            if (this.FilterButton.InvokeRequired)
            {
                this.FilterButton.Invoke(new VoidObjDelegate(HandleChangeFileListBoxValueChanged), new object[] { newValO });
                return;
            }
            selec = null;
            if (newValO != null)
            {
                try
                {
                    selec = m_VersionInfo.ChangesInVersion[(int)newValO].folderUnit;
                    FilterStr = m_VersionInfo.ChangesInVersion[(int)newValO].FilteredList;
                }
                catch (Exception)
                {
                    selec = m_VersionInfo.ChangesInVersion[0].folderUnit;
                    FilterStr = m_VersionInfo.ChangesInVersion[0].FilteredList;
                }
            }
            this.FilterButton.Refresh();
        }

        void ChangeFileListBox_DoubleClick(object sender, EventArgs e)
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

        private void deleteButton_Click(object sender, EventArgs e)
        {
            new Thread(HandelDeleteButton).Start();
        }

        private void HandelDeleteButton()
        {
            if (m_VersionInfo.Removed == false)
            {
                m_FTObjects.DeleteVersion(this.m_VersionInfo);
                
            }
            else
            {
                
                m_FTObjects.UndeleteVersion(this.m_VersionInfo);
                if (m_unremove != null)
                {
                    m_unremove.RemoveVersion(m_VersionInfo);
                }
            }
            
        }

        private void tSetDeleteText()
        {
            deleteButton.Invoke(new VoidNoArgDelegate(iSetDeleteText));
        }

        private void iSetDeleteText()
        {
            if (this.m_VersionInfo.Removed == true)
            {
                deleteButton.Text = "Un-Remove";
            }
            else if (this.m_VersionInfo.Removed == false)
            {
                deleteButton.Text = "Remove";
            }
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
                this.Invoke(new VoidObjDelegate(showCopyDlgForDBlClk), new object[] { locat});
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

        public VersionInfo PublicVersionInfo
        {
            get
            {
                return m_VersionInfo;
            }
        }

        private delegate void VoidVersionInfoDelegate(VersionInfo vers);

        private void SetVersionInfo(VersionInfo versionInfo)
        {

           
            m_VersionInfo = versionInfo;
            this.VersionLabel.Text = versionInfo.versionName;
            this.DateLabel.Text = versionInfo.date.ToLongDateString() + " " + versionInfo.date.ToLongTimeString();
            if (versionInfo.UserVersName != null)
            {
                String [] userVerArr = new string[versionInfo.UserVersName.Count];
                versionInfo.UserVersName.CopyTo(userVerArr);
                this.UserVersionListBox.Items.AddRange(userVerArr);
            }

            string changeTypS;
            bool filterd;
            bool first = true;
            ErrorInfo utilErIn;
            int max = 0;
            Size sizeMeasure;
            if (versionInfo.changesInVersion != null)
            {
                String [] changes = new string[versionInfo.changesInVersion.Count];
                for (int i = 0; i < versionInfo.ChangesInVersion.Count ; i++)
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
                    if (versionInfo.ChangesInVersion[i].FilteredList != null && versionInfo.ChangesInVersion[i].FilteredList.Count > 0)
                    {
                        changeTypS = "Filtered";
                        filterd = true;
                    }
                    else
                    {
                        changeTypS = versionInfo.ChangesInVersion[i].change.ToString();
                    }
                    
                    if (versionInfo.ChangesInVersion[i].change == ChangeType.Rename)
                    {
                        changes[i] = changeTypS + ": " + versionInfo.ChangesInVersion[i].folderUnit.oldLocation + " to " + versionInfo.ChangesInVersion[i].externalLocation;
                    }
                    else
                    {
                        changes[i] = changeTypS + ": " + versionInfo.ChangesInVersion[i].externalLocation;
                    }
                    if (versionInfo.ErrorList != null)
                    {
                        foreach (ErrorInfo erInter in versionInfo.ErrorList.Values)
                        {
                            if (erInter.ChangeInstruction.externalLocation.Equals(versionInfo.ChangesInVersion[i].externalLocation))
                            {
                                changes[i] = "Could not read " + changes[i];
                                IndexToErrorString[i] = erInter.Error;
                                break;
                            }
                        }
                        
                    }

                    if (filterd)
                    {
                        changes[i] +=" (because of filter";
                        if (versionInfo.ChangesInVersion[i].FilteredList.Count > 1)
                        {
                            changes[i] += "s";
                        }
                        foreach (string fr in versionInfo.ChangesInVersion[i].FilteredList)
                        {
                            changes[i] +=  " "+fr ;
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

            if (versionInfo.FreeText != null)
            {
                this.DiscriptionTextBox.Text = versionInfo.FreeText;
                this.SaveDiscriptionButton.Text = "Edit to Save";
            }
            if (versionInfo.ErrorList.Count > 0)
            {
                if (versionInfo.Removed)
                {
                    this.deleteButton.Text = "Un-Remove";
                    SetRe();
                }
                else
                {
                    SetErr();
                }
            }
            else if (versionInfo.Removed == true)
            {
                this.deleteButton.Text = "Un-Remove";
                SetRemoved();
            }
            this.DiscriptionTextBox.TextChanged += new EventHandler(DiscriptionTextBox_TextChanged);


            

        }

        private void SetColor(Color roun, Color button, Color f4)
        {
            VersionTableLayoutPanel.BackColor = roun;
            VersionLabel.BackColor = roun;
            VersionLabel.ForeColor = f4;
            DateLabel.BackColor = roun;
            DateLabel.ForeColor = f4;
            UserVersionListBox.BackColor = roun;
            UserVersionListBox.ForeColor = f4;
            ChangeFileListBox.BackColor = roun;
            DiscriptionTextBox.BackColor = roun;
            DiscriptionTextBox.ForeColor = f4;
            UseButton.BackColor = button;
            UseButton.ForeColor = f4;
            CopyButton.BackColor = button;
            CopyButton.ForeColor = f4;
            AddUserVersionButton.BackColor = button;
            AddUserVersionButton.ForeColor = f4;
            Exploreutton.BackColor = button;
            Exploreutton.ForeColor = f4;
            SaveDiscriptionButton.BackColor = button;
            SaveDiscriptionButton.ForeColor = f4;
            panel2.BackColor = roun;
            FilterButton.BackColor = button;
            this.BackColor = roun;
            
        }

        private void SetRemoved()
        {
            SetColor(Color.Black, Color.Gray, Color.White);
            deleteButton.BackColor = Color.Blue;
        }

        private void SetErr()
        {
            SetColor(Color.RosyBrown, Color.White, Color.Black);
            deleteButton.BackColor = Color.DarkRed;
        }

        private void SetRe()
        {
            SetColor(Color.Black, Color.Red, Color.White);
            deleteButton.BackColor = Color.Blue;
        }




        private void SetUnRemove()
        {
            if (m_VersionInfo.ErrorList.Count > 0)
            {
                SetErr();
            }
            else
            {
                VersionTableLayoutPanel.BackColor = Color.White;
                VersionLabel.BackColor = Color.White;
                VersionLabel.ForeColor = Color.Black;
                DateLabel.BackColor = Color.White;
                DateLabel.ForeColor = Color.Black;
                UserVersionListBox.BackColor = Color.White;
                ChangeFileListBox.BackColor = Color.White;
                DiscriptionTextBox.BackColor = Color.White;
                UseButton.BackColor = Color.Gainsboro;
                UseButton.ForeColor = Color.Black;
                CopyButton.BackColor = Color.Gainsboro;
                CopyButton.ForeColor = Color.Black;
                AddUserVersionButton.BackColor = Color.Gainsboro;
                AddUserVersionButton.ForeColor = Color.Black;
                Exploreutton.BackColor = Color.Gainsboro;
                Exploreutton.ForeColor = Color.Black;
                SaveDiscriptionButton.BackColor = Color.Gainsboro;
                SaveDiscriptionButton.ForeColor = Color.Black;

                panel2.BackColor = Color.White;
                FilterButton.BackColor = Color.Gainsboro;
                this.BackColor = Color.White;
                deleteButton.BackColor = Color.DarkRed;
            }
        }

        private void SetCurrent(bool current)
        {
            this.current = current;
            if (current)
            {
               
                    VersionTableLayoutPanel.BackColor = Color.LightBlue;
                    VersionLabel.BackColor = Color.LightBlue;
                    VersionLabel.ForeColor = Color.Black;
                    DateLabel.BackColor = Color.LightBlue;
                    DateLabel.ForeColor = Color.Black;
                    UserVersionListBox.BackColor = Color.LightBlue;

                    ChangeFileListBox.BackColor = Color.LightBlue;
                    DiscriptionTextBox.BackColor = Color.LightBlue;
                    UseButton.BackColor = Color.LightSteelBlue;
                    UseButton.ForeColor = Color.Black;
                    CopyButton.BackColor = Color.LightSteelBlue;
                    CopyButton.ForeColor = Color.Black;
                    Exploreutton.BackColor = Color.LightSteelBlue;
                    Exploreutton.ForeColor = Color.Black;
                    SaveDiscriptionButton.BackColor = Color.LightSteelBlue;
                    SaveDiscriptionButton.ForeColor = Color.Black;
                    AddUserVersionButton.BackColor = Color.LightSteelBlue;
                    AddUserVersionButton.ForeColor = Color.Black;
                    panel2.BackColor = Color.LightBlue;
                    FilterButton.BackColor = Color.LightSteelBlue;
                    this.BackColor = Color.LightBlue;
                    deleteButton.Text = "Remove";
                    deleteButton.BackColor = Color.DarkRed;
                    deleteButton.ForeColor = Color.White;
                    deleteButton.Visible = false;
                
            }
            else
            {
                if (m_VersionInfo.ErrorList.Count > 0)
                {
                    SetErr();
                }
                else
                {
                    VersionTableLayoutPanel.BackColor = Color.White;
                    VersionLabel.BackColor = Color.White;
                    DateLabel.BackColor = Color.White;
                    UserVersionListBox.BackColor = Color.White;
                    ChangeFileListBox.BackColor = Color.White;
                    DiscriptionTextBox.BackColor = Color.White;
                    UseButton.BackColor = Color.Gainsboro;
                    CopyButton.BackColor = Color.Gainsboro;
                    Exploreutton.BackColor = Color.Gainsboro;
                    SaveDiscriptionButton.BackColor = Color.Gainsboro;
                    AddUserVersionButton.BackColor = Color.Gainsboro;
                    panel2.BackColor = Color.White;
                    FilterButton.BackColor = Color.Gainsboro;
                    this.BackColor = Color.White;
                    deleteButton.Visible = true;
                }
            }
        }
       


        #region PanelFunction Members

        public void DoFunction(object data)
        {
            if (data is Boolean)
            {
                this.Invoke(new VoidBoolDelegate(SetCurrent), new object[] { (Boolean)data });
            }
            else if (data is VersionOption)
            {
                if (((VersionOption)data) == VersionOption.REMOVED)
                {
                    this.m_VersionInfo.Removed = true;
                    new Thread(tSetDeleteText).Start();
                    this.Invoke(new VoidNoArgDelegate(SetRemoved));
                }
                else if (((VersionOption)data) == VersionOption.NOT_REMOVED)
                {
                    this.m_VersionInfo.Removed = false;
                    new Thread(tSetDeleteText).Start();
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
