using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Threading;

namespace FolderTrackGuiTest1.UserVersionDialog
{
    public partial class UserVersionForm : Form
    {
        PanelList<String> StopUserVersionPanelList;

        VersionInfo m_VersionInfo;
        List<string> m_AllUserVersions;

        //Versions to add to this list that were not previously there
        public List<string> NewUserVersions;

        //All versions that will be used rater new or not
        List<string> UsedUserVersions;

        public List<string> RemoveUserVersions;

        //Versions that allready existed but was not allready attached to this version
        public List<string> ReUseUserVersions;


        public List<string> LastUserVersions;

        public List<string> ContinueUserVersions;

        private delegate void VoidNoArgDelegate();

        public UserVersionForm(VersionInfo versioninfo, List<string>  AllUserVersions)
        {
            InitializeComponent();
            m_VersionInfo = versioninfo;
            m_AllUserVersions = AllUserVersions;
            StopUserVersionPanelList = new PanelList<string>();
            PanelFromUserVersion panelFromDat = new PanelFromUserVersion();
            panelFromDat.m_UserVersionForm = this;
            StopUserVersionPanelList.PanelFromData = panelFromDat;
            this.VersionPanel.Controls.Add(StopUserVersionPanelList);
            NewUserVersions = new List<string>();
            RemoveUserVersions = new List<string>();
            ReUseUserVersions = new List<string>();
            LastUserVersions = new List<string>();
            UsedUserVersions = new List<string>();
            ContinueUserVersions = new List<string>();
            SetUserVersions(versioninfo.UserVersionsThatContain);
            SetReUseUserVersion(versioninfo.UserVersionsThatContain, AllUserVersions);
            this.DialogResult = DialogResult.Cancel;
            StopUserVersionPanelList.ExtraRowSpace = 5;
        }

        private void SetUserVersions(IList<UserVersionStatus> UserVersions)
        {
            foreach (UserVersionStatus usverst in UserVersions)
            {
                StopUserVersionPanelList.AddDataBottom(usverst.UserVersion);
                UsedUserVersions.Add(usverst.UserVersion);
            }

            foreach (UserVersionStatus usverst in UserVersions)
            {
                if (usverst.end == true)
                {
                    SetLastUserVersion(usverst.UserVersion);
                }
            }
        }

        private void SetReUseUserVersion(IList<UserVersionStatus> UserVersions, List<string> AllUserVersions)
        {
            if (AllUserVersions != null)
            {
                foreach (string userversion in AllUserVersions)
                {
                    if (ContainsVersion(UserVersions,userversion) == false)
                    {
                        this.ReUseListBox.Items.Add(userversion);
                    }
                }
            }
        }

        public static bool ContainsVersion(IList<UserVersionStatus> UserVerStaLi, string version)
        {
            foreach (UserVersionStatus usstat in UserVerStaLi)
            {
                if (usstat.UserVersion.Equals(version))
                {
                    return true;
                }
            }
            return false;
        }

        private void AddNewUserVersion(string UserVersion)
        {
            if (UserVersionAllreadyExist(UserVersion) == false)
            {
                NewUserVersions.Add(UserVersion);
            }
            else
            {
                if ( ContainsVersion( m_VersionInfo.userVersionsThatContain,UserVersion) == false)
                {
                    ReUseUserVersions.Add(UserVersion);
                }
            }

            if (UsedUserVersions.Contains(UserVersion) == false)
            {
                StopUserVersionPanelList.AddDataBottom(UserVersion);
                UsedUserVersions.Add(UserVersion);
            }
            RemoveUserVersions.Remove(UserVersion);
            
        }

        private void AddNewUserVersion(List<string>  UserVersion)
        {
            List<string> NewVersionsToAdd = new List<string>();
            List<string> VersionsToAddToPanelList = new List<string>();
            foreach (string userv in UserVersion)
            {
                if (UserVersionAllreadyExist(userv) == false)
                {
                    NewVersionsToAdd.Add(userv);
                }
                else
                {
                    if (ContainsVersion(m_VersionInfo.userVersionsThatContain,userv) == false)
                    {
                        ReUseUserVersions.Add(userv);
                    }
                }

                if (UsedUserVersions.Contains(userv) == false)
                {
                    VersionsToAddToPanelList.Add(userv);
                }
                RemoveUserVersions.Remove(userv);
            }
            NewUserVersions.AddRange(NewVersionsToAdd);

            StopUserVersionPanelList.AddData(VersionsToAddToPanelList);
            UsedUserVersions.AddRange(VersionsToAddToPanelList);
        }

        private void InvokeUserVersionNow(string UserVersion)
        {
            if (UserVersionAllreadyExist(UserVersion))
            {
                if (this.ReUseListBox.Items.Contains(UserVersion) == false)
                {
                    this.ReUseListBox.Items.Add(UserVersion);
                }
            }
        }

        private bool UserVersionAllreadyExist(string userversion)
        {
            if (m_AllUserVersions != null)
            {
                foreach (string userver in m_AllUserVersions)
                {
                    if (userver.Equals(userversion))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void RemoveVersionNow(string userversion)
        {
            if ( ContainsVersion( m_VersionInfo.userVersionsThatContain,userversion))
            {
                RemoveUserVersions.Add(userversion);
            }
            if(UserVersionAllreadyExist(userversion))
            {
                if (this.ReUseListBox.Items.Contains(userversion) == false)
                {
                    this.ReUseListBox.Items.Add(userversion);
                }
            }
            NewUserVersions.Remove(userversion);
            StopUserVersionPanelList.RemoveData(userversion);
            UsedUserVersions.Remove(userversion);
            LastUserVersions.Remove(userversion);
        }

        public void  SetLastUserVersion(string userversion)
        {
            if (LastUserVersions.Contains(userversion) == false)
            {
                LastUserVersions.Add(userversion);
                ContinueUserVersions.Remove(userversion);
                StopUserVersionPanelList.AddFunctionCallToData(userversion, true);
            }
            else
            {
                LastUserVersions.Remove(userversion);
                ContinueUserVersions.Add(userversion);
                StopUserVersionPanelList.AddFunctionCallToData(userversion, false);

            }

        }

        private void UseAgainButton_Click(object sender, EventArgs e)
        {
            string [] selec = new string[this.ReUseListBox.SelectedItems.Count];
            
            this.ReUseListBox.SelectedItems.CopyTo(selec, 0);

            List<string> UseList = new List<string>();
            UseList.AddRange(selec);
            AddNewUserVersion(UseList);

            foreach(string removeUsrV in selec)
            {
                this.ReUseListBox.Items.Remove(removeUsrV);
            }


        }

        private void CreateAndUseButton_Click(object sender, EventArgs e)
        {
            string newuservers = this.CreateUserVersionTextBox.Text;
            if (newuservers.Trim().Length > 0)
            {
                AddNewUserVersion(newuservers);
                this.CreateUserVersionTextBox.Clear();
            }
        }

        private void CreateUserVersionTextBox_TextChanged(object sender, EventArgs e)
        {
            new Thread(EnAbCreateAndUseButoTh).Start();
        }

        private void EnAbCreateAndUseButoTh()
        {
            this.CreateUserVersionTextBox.Invoke(new VoidNoArgDelegate(EnAbCreateAndUseButo));
        }

        private void EnAbCreateAndUseButo()
        {
            if (this.CreateUserVersionTextBox.Text.Trim().Length > 0)
            {
                CreateAndUseButton.Text = "Create and Use";
                CreateAndUseButton.Enabled = true;
            }
            else
            {
                CreateAndUseButton.Text = "Enter Text";
                CreateAndUseButton.Enabled = false;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        

        


    }
}