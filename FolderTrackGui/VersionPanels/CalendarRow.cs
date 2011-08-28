using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using FolderTrackGuiTest1.UserVersionDialog;
using FolderTrackGuiTest1.CopyVersionDialog;
using System.Threading;
using System.IO;
using System.ComponentModel;
using ZetaLongPaths;

namespace FolderTrackGuiTest1.VersionPanels
{
    class CalendarRow : Panel
    {

        private VersionInfo m_VersionInfo;
        private FTObjects m_FTObjects;

        private delegate void SetCurrentVersionDelegate();
        private delegate void UnSetCurrentVersionDelegate();

        public CalendarRow()
        {
            InitializeComponent();
        }

        public CalendarRow(VersionInfo versionInfo, FTObjects ftobjects)
        {
            InitializeComponent();
            this.m_FTObjects = ftobjects;
            SetVersionInfo(versionInfo);
        }

        void AddUserVersionButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Add User Version Button Clicked in Calendar on version " + this.m_VersionInfo.versionName);
            UserVersionForm uservers = new UserVersionForm(this.m_VersionInfo, m_FTObjects.AllUserVersions);
            DialogResult res = uservers.ShowDialog();
            if (res == DialogResult.OK)
            {
                new Thread(new ParameterizedThreadStart(HandleAddUser)).Start(uservers);
            }


        }

        private void HandleAddUser(object userversO)
        {
            UserVersionForm uservers = (UserVersionForm)userversO;
            UserVersionSet set = new UserVersionSet();
            foreach (string newuserver in uservers.NewUserVersions)
            {
                //       m_FTObjects.addUserVersion(m_VersionInfo.versionName, newuserver, false);
                //clean new version out of continue
                uservers.ContinueUserVersions.Remove(newuserver);
            }

            //   foreach (string reuser in uservers.ReUseUserVersions)
            //  {
            //      m_FTObjects.addUserVersion(m_VersionInfo.versionName, reuser, false);
            //  }

            set.AddUserVersion.AddRange(uservers.NewUserVersions);
            set.AddUserVersion.AddRange(uservers.ReUseUserVersions);


            foreach (UserVersionStatus lastuser in this.m_VersionInfo.userVersionsThatContain)
            {
                if (lastuser.end == true)
                {
                    uservers.LastUserVersions.Remove(lastuser.UserVersion);
                }
                else
                {
                    //clean allready continue versions 
                    uservers.ContinueUserVersions.Remove(lastuser.UserVersion);
                }
            }

            //         m_FTObjects.setStopVersion(uservers.LastUserVersions, m_VersionInfo.versionName, false, false);
            set.LastUserVersions.AddRange(uservers.LastUserVersions);

            //       foreach (string contin in uservers.ContinueUserVersions)
            //       {
            //          m_FTObjects.RemoveStopUserVersion(contin, m_VersionInfo.versionName);
            //      }
            set.RemoveStop.AddRange(uservers.ContinueUserVersions);

            //        m_FTObjects.setStopVersion(uservers.RemoveUserVersions, m_VersionInfo.versionName, true, true);
            set.RemoveUserVersions.AddRange(uservers.RemoveUserVersions);

            m_FTObjects.SetUserVersion(m_VersionInfo.versionName, set);

            uservers.Dispose();
        }

        void CopyButton_Click(object sender, EventArgs e)
        {
            IList<string> moLoca = m_FTObjects.GetLocationsInMonitorGroup();

            CopyVersionForm copyForm = new CopyVersionForm(moLoca);
            copyForm.ShowDialog();
            Dictionary<string, string> ToLocations = copyForm.CopyLocFromExtLoc;
            m_FTObjects.CopyVersion(this.m_VersionInfo.versionName, ToLocations);

        }

        void UseButton_Click(object sender, EventArgs e)
        {
            m_FTObjects.SetVersion(this.m_VersionInfo.versionName);
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

        public void IndicateCurrentVersion()
        {
            new Thread(InvokeSetCurrentVersion).Start();
        }

        private void InvokeSetCurrentVersion()
        {
            this.Invoke(new SetCurrentVersionDelegate(SetCurrentVersion));
        }

        private void SetCurrentVersion()
        {
            this.BackColor = System.Drawing.Color.Plum;
            this.VersionLabel.BackColor = System.Drawing.Color.Thistle;
            this.DateLabel.BackColor = System.Drawing.Color.Thistle;
            this.DiscriptionTextBox.BackColor = System.Drawing.Color.Plum;
            this.ChangeFileListBox.BackColor = System.Drawing.Color.Plum;
            this.UserVersionListBox.BackColor = System.Drawing.Color.Plum;

        }

        void DiscriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            this.SaveDiscriptionButton.Text = "Save";
            this.SaveDiscriptionButton.Enabled = true;
        }

        void SaveDiscriptionButton_Click(object sender, EventArgs e)
        {
            m_FTObjects.SetDiscription(this.m_VersionInfo.versionName, this.DiscriptionTextBox.Text);
            this.SaveDiscriptionButton.Enabled = false;
            this.SaveDiscriptionButton.Text = "Edit to Save";
        }




        void ChangeFileListBox_DoubleClick(object sender, EventArgs e)
        {
            String selectedFile = null;
            if (this.ChangeFileListBox.SelectedItem is String)
            {
                selectedFile = this.ChangeFileListBox.SelectedItem.ToString();
                int firstspace = selectedFile.IndexOf(' ');
                selectedFile = selectedFile.Substring(firstspace + 1);
            }
            if (selectedFile != null)
            {
                string VersionFile = selectedFile;

                string PreDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                             + Path.DirectorySeparatorChar + Util.Company + Path.DirectorySeparatorChar
                             + Util.Product + Path.DirectorySeparatorChar
                             + Settings.Default.tempDir
                             + Path.DirectorySeparatorChar;

                string CopyTo = PreDir +
                                ZlpPathHelper.GetFileNameFromFilePath(selectedFile);

                int uniqueFileInt = 0;
                while (ZlpIOHelper.FileExists(CopyTo) || ZlpIOHelper.DirectoryExists(CopyTo))
                {

                    CopyTo = PreDir +
                             Path.DirectorySeparatorChar +
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

        }


        public VersionInfo PublicVersionInfo
        {
            get
            {
                return m_VersionInfo;
            }
        }


        private void SetVersionInfo(VersionInfo versionInfo)
        {
            m_VersionInfo = versionInfo;
            this.VersionLabel.Text = versionInfo.versionName;
            this.DateLabel.Text = versionInfo.date.ToLongTimeString() + " " + versionInfo.date.ToLongDateString();
            if (versionInfo.UserVersName != null)
            {
                String[] userVerArr = new string[versionInfo.UserVersName.Count];
                versionInfo.UserVersName.CopyTo(userVerArr);
                this.UserVersionListBox.Items.AddRange(userVerArr);
            }

            if (versionInfo.changesInVersion != null)
            {
                String[] changes = new string[versionInfo.changesInVersion.Count];
                for (int i = 0; i < versionInfo.ChangesInVersion.Count; i++)
                {
                    if (versionInfo.ChangesInVersion[i].change == ChangeType.Rename)
                    {
                        changes[i] = versionInfo.ChangesInVersion[i].change.ToString() + ": " + versionInfo.ChangesInVersion[i].folderUnit.oldLocation + " to " + versionInfo.ChangesInVersion[i].externalLocation;
                    }
                    else
                    {
                        changes[i] = versionInfo.ChangesInVersion[i].change.ToString() + ": " + versionInfo.ChangesInVersion[i].externalLocation;
                    }
                }
                this.ChangeFileListBox.Items.AddRange(changes);
            }

            if (versionInfo.FreeText != null)
            {
                this.DiscriptionTextBox.Text = versionInfo.FreeText;
            }
            this.DiscriptionTextBox.TextChanged += new EventHandler(DiscriptionTextBox_TextChanged);




        }





        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AddUserVersionButton = new System.Windows.Forms.Button();
            this.UseButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.ExploreButton = new System.Windows.Forms.Button();
            this.SaveDiscriptionButton = new System.Windows.Forms.Button();
            this.UserVersionListBox = new System.Windows.Forms.ListBox();
            this.ChangeFileListBox = new System.Windows.Forms.ListBox();
            this.DiscriptionTextBox = new System.Windows.Forms.TextBox();
            this.USER_VERSION_Label = new System.Windows.Forms.Label();
            this.CHANGES_Label = new System.Windows.Forms.Label();
            this.DISCRIPTION_Label = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.BackColor = System.Drawing.Color.Sienna;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(50, 65);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(583, 148);
            this.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.Controls.Add(this.AddUserVersionButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.UseButton, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.CopyButton, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.ExploreButton, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.SaveDiscriptionButton, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.UserVersionListBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ChangeFileListBox, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.DiscriptionTextBox, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.USER_VERSION_Label, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.CHANGES_Label, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.DISCRIPTION_Label, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.VersionLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DateLabel, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, -1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(557, 139);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // AddUserVersionButton
            // 
            this.AddUserVersionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.AddUserVersionButton, 2);
            this.AddUserVersionButton.Location = new System.Drawing.Point(41, 113);
            this.AddUserVersionButton.Name = "AddUserVersionButton";
            this.AddUserVersionButton.Size = new System.Drawing.Size(75, 23);
            this.AddUserVersionButton.TabIndex = 0;
            this.AddUserVersionButton.Text = "Add";
            this.AddUserVersionButton.UseVisualStyleBackColor = true;
            // 
            // UseButton
            // 
            this.UseButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.UseButton.Location = new System.Drawing.Point(161, 113);
            this.UseButton.Name = "UseButton";
            this.UseButton.Size = new System.Drawing.Size(73, 23);
            this.UseButton.TabIndex = 1;
            this.UseButton.Text = "Use";
            this.UseButton.UseVisualStyleBackColor = true;
            // 
            // CopyButton
            // 
            this.CopyButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CopyButton.Location = new System.Drawing.Point(240, 113);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(73, 23);
            this.CopyButton.TabIndex = 2;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            // 
            // ExploreButton
            // 
            this.ExploreButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExploreButton.Location = new System.Drawing.Point(319, 113);
            this.ExploreButton.Name = "ExploreButton";
            this.ExploreButton.Size = new System.Drawing.Size(73, 23);
            this.ExploreButton.TabIndex = 3;
            this.ExploreButton.Text = "Explore";
            this.ExploreButton.UseVisualStyleBackColor = true;
            // 
            // SaveDiscriptionButton
            // 
            this.SaveDiscriptionButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.SaveDiscriptionButton, 2);
            this.SaveDiscriptionButton.Location = new System.Drawing.Point(438, 113);
            this.SaveDiscriptionButton.Name = "SaveDiscriptionButton";
            this.SaveDiscriptionButton.Size = new System.Drawing.Size(75, 23);
            this.SaveDiscriptionButton.TabIndex = 4;
            this.SaveDiscriptionButton.Text = "Save";
            this.SaveDiscriptionButton.UseVisualStyleBackColor = true;
            // 
            // UserVersionListBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.UserVersionListBox, 2);
            this.UserVersionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserVersionListBox.FormattingEnabled = true;
            this.UserVersionListBox.Location = new System.Drawing.Point(3, 29);
            this.UserVersionListBox.Name = "UserVersionListBox";
            this.UserVersionListBox.Size = new System.Drawing.Size(152, 69);
            this.UserVersionListBox.TabIndex = 5;
            // 
            // ChangeFileListBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.ChangeFileListBox, 3);
            this.ChangeFileListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChangeFileListBox.FormattingEnabled = true;
            this.ChangeFileListBox.Location = new System.Drawing.Point(161, 29);
            this.ChangeFileListBox.Name = "ChangeFileListBox";
            this.ChangeFileListBox.Size = new System.Drawing.Size(231, 69);
            this.ChangeFileListBox.TabIndex = 6;
            // 
            // DiscriptionTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.DiscriptionTextBox, 2);
            this.DiscriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiscriptionTextBox.Location = new System.Drawing.Point(398, 29);
            this.DiscriptionTextBox.Multiline = true;
            this.DiscriptionTextBox.Name = "DiscriptionTextBox";
            this.DiscriptionTextBox.Size = new System.Drawing.Size(156, 78);
            this.DiscriptionTextBox.TabIndex = 7;
            // 
            // USER_VERSION_Label
            // 
            this.USER_VERSION_Label.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.USER_VERSION_Label, 2);
            this.USER_VERSION_Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.USER_VERSION_Label.Location = new System.Drawing.Point(3, 13);
            this.USER_VERSION_Label.Name = "USER_VERSION_Label";
            this.USER_VERSION_Label.Size = new System.Drawing.Size(152, 13);
            this.USER_VERSION_Label.TabIndex = 8;
            this.USER_VERSION_Label.Text = "User Versions";
            this.USER_VERSION_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CHANGES_Label
            // 
            this.CHANGES_Label.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.CHANGES_Label, 3);
            this.CHANGES_Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CHANGES_Label.Location = new System.Drawing.Point(161, 13);
            this.CHANGES_Label.Name = "CHANGES_Label";
            this.CHANGES_Label.Size = new System.Drawing.Size(231, 13);
            this.CHANGES_Label.TabIndex = 9;
            this.CHANGES_Label.Text = "Changes";
            this.CHANGES_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DISCRIPTION_Label
            // 
            this.DISCRIPTION_Label.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.DISCRIPTION_Label, 2);
            this.DISCRIPTION_Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DISCRIPTION_Label.Location = new System.Drawing.Point(398, 13);
            this.DISCRIPTION_Label.Name = "DISCRIPTION_Label";
            this.DISCRIPTION_Label.Size = new System.Drawing.Size(156, 13);
            this.DISCRIPTION_Label.TabIndex = 10;
            this.DISCRIPTION_Label.Text = "Discription";
            this.DISCRIPTION_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.BackColor = System.Drawing.Color.Sienna;
            this.tableLayoutPanel1.SetColumnSpan(this.VersionLabel, 4);
            this.VersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionLabel.Location = new System.Drawing.Point(0, 0);
            this.VersionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(316, 13);
            this.VersionLabel.TabIndex = 11;
            this.VersionLabel.Text = "label2";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.BackColor = System.Drawing.Color.Sienna;
            this.tableLayoutPanel1.SetColumnSpan(this.DateLabel, 3);
            this.DateLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DateLabel.Location = new System.Drawing.Point(316, 0);
            this.DateLabel.Margin = new System.Windows.Forms.Padding(0);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(241, 13);
            this.DateLabel.TabIndex = 12;
            this.DateLabel.Text = "label3";
            this.DateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CalendarRowDesignerPanel
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button AddUserVersionButton;
        private System.Windows.Forms.Button UseButton;
        private System.Windows.Forms.Button CopyButton;
        private System.Windows.Forms.Button ExploreButton;
        private System.Windows.Forms.Button SaveDiscriptionButton;
        private System.Windows.Forms.ListBox UserVersionListBox;
        private System.Windows.Forms.ListBox ChangeFileListBox;
        private System.Windows.Forms.TextBox DiscriptionTextBox;
        private System.Windows.Forms.Label USER_VERSION_Label;
        private System.Windows.Forms.Label CHANGES_Label;
        private System.Windows.Forms.Label DISCRIPTION_Label;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label DateLabel;
    }
}
