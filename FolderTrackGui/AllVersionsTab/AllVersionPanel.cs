using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.IO;
using System.Threading;

namespace FolderTrackGuiTest1.AllVersionsTab
{
    class AllVersionPanel : Panel, FolderTrack.WCFContracts.FolderTrackCallBack, FTObjects.GuiMessage
    {


        private delegate void listdeleg(object sender, EventArgs e);
        private delegate void VoidBoolDelegate(bool b);
        private delegate void VoidIntDelegate(int i);

        private PanelList<VersionInfo> ListAllVersionPanelList;

        public RelationshipPanel relationpan;
        VersionInfo lastCurrentVersion;
        Label LoatStatusLabel;
        int versCnt = 0;
        bool showingWait= false;
        public bool HoldAllDisplay;
        public bool sendv;
        public bool newcur;
        public bool usemoncall;
        public enum DisplayOptions
        {
            List,
            Relationship
        }

        private DisplayOptions m_DisplayOptions;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private FTObjects m_FTObjects;

        public AllVersionPanel()
        {
            InitializeComponent();

            relationpan = new RelationshipPanel();
            this.ListAllVersionPanelList = new PanelList<VersionInfo>();
            LoatStatusLabel = new Label();
            LoatStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            LoatStatusLabel.Text = "Please Wait";
            LoatStatusLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            LoatStatusLabel.Visible = false;
            LoatStatusLabel.AutoSize = true;
            
            LoatStatusLabel.BorderStyle = BorderStyle.FixedSingle;
            showingWait = false;
            this.ListAllVersionPanelList.ExtraRowSpace = 0;
            this.VersionPanel.Controls.Add(LoatStatusLabel);
            this.VersionPanel.Controls.Add(this.ListAllVersionPanelList);

            this.ListAllVersionPanelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListAllVersionPanelList.Visible = true;
            this.RelationShipPanel.Controls.Add(relationpan);
            relationpan.Dock = DockStyle.Fill;
            DisplayType = DisplayOptions.List;
        }

        

        private void ShowWait(object bO)
        {
            
            
                bool b = (bool)bO;
                if (showingWait == b)
                {
                    return;
                }


                showingWait = b;
                if (LoatStatusLabel.InvokeRequired)
                {
                    LoatStatusLabel.Invoke(new VoidBoolDelegate(setL), new object[] { showingWait });
                }
                else
                {
                    setL(showingWait);
                }
                if (ListAllVersionPanelList.InvokeRequired)
                {
                    ListAllVersionPanelList.Invoke(new VoidBoolDelegate(setListAllVersion), new object[] { !showingWait });
                }
                else
                {
                    setListAllVersion(!showingWait);
                }
            

        }

        private void setL(bool set)
        {
            LoatStatusLabel.Visible = set;
        }

        private void setListAllVersion(bool set)
        {
            ListAllVersionPanelList.Visible = set;
        }

        public FTObjects P_FTObjects
        {
            set
            {
                
                Util.DBug2("AllVersionPanel", "Entering FT");
                this.ListAllVersionPanelList.ClearAllData();
                Util.DBug2("AllVersionPanel", "Aighning FT");
                this.m_FTObjects = value;
                Util.DBug2("AllVersionPanel", "Asking for inform");
                
                Util.DBug2("AllVersionPanel", "Passing info ro relat");
                relationpan.P_FTObjects = m_FTObjects;
                Util.DBug2("AllVersionPanel", "Setting up pan lis");
                this.ListAllVersionPanelList.PanelFromData = new PanelFromVersionInfo(m_FTObjects);
                Util.DBug2("AllVersionPanel", "Adding data to pan lis");
             
                Util.DBug2("AllVersionPanel", "No last cur");
                lastCurrentVersion = null;
                Util.DBug2("AllVersionPanel", "Requesting current vers");
                VersionInfo vers = m_FTObjects.GetCurrentVersionInfo();
                                       ShowWait(true);
               
                
            }
        }

        private void FunctionsThatNeedCompleteVersionInfo()
        {
            
            
            VersionInfo vers = m_FTObjects.GetCurrentVersionInfo();
            Util.DBug2("AllVersionPanel", "Setting new curent vers");
            if (vers != null)
            {
                NewCurrentVersion(null, vers);
                Util.DBug2("AllVersionPanel", "informing relat of new current");
                relationpan.NewCurrentVersion(null, vers);
            }
            if (m_FTObjects.GetDontMonitor)
            {
                DontMonitor(m_FTObjects.CurrentMonitorGroup);
                relationpan.SetDontMonitor(true);
            }
            else
            {
                UseMonitor();
                relationpan.SetDontMonitor(false);
            }

            
                ShowWait(false);
            
        }


        public DisplayOptions DisplayType
        {
            get
            {
                return m_DisplayOptions;
            }
            set
            {
                m_DisplayOptions = value;
                if (m_DisplayOptions == DisplayOptions.List)
                {
                    if (this.ListRadioButton.Checked != true)
                    {
                        this.ListRadioButton.Checked = true;
                    }
                    this.RelationShipPanel.Visible = false;
                }
                else
                {
                    if (this.RelationshipRadioButton.Checked != true)
                    {
                        this.RelationshipRadioButton.Checked = true;
                    }
                    this.RelationShipPanel.Visible = true;
                }
            }
        }

        private void setAomountLoaded(int num)
        {
            LoatStatusLabel.Text = "Loaded "+num+" versions";
        }

        void ListRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Util.UserDebug("List selected in All Versions");
            if (this.ListRadioButton.InvokeRequired == true)
            {
                this.ListRadioButton.Invoke(new listdeleg(ListRadioButton_CheckedChanged), new object[] { sender, e });
                return;
            }
            if (this.ListRadioButton.Checked == true)
            {
                if (DisplayType != DisplayOptions.List)
                {
                    DisplayType = DisplayOptions.List;
                }
            }
        }

        private void RelationshipRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            Util.UserDebug("Relationship selected in All Versions");
            if (this.RelationshipRadioButton.InvokeRequired == true)
            {
                this.RelationshipRadioButton.Invoke(new listdeleg(RelationshipRadioButton_CheckedChanged), new object[] { sender, e });
                return;
            }

            if (this.RelationshipRadioButton.Checked == true)
            {
                if (DisplayType != DisplayOptions.Relationship)
                {
                    DisplayType = DisplayOptions.Relationship;
                }
            }
        }


        #region FolderTrackCallBack Members

        public void NewCurrentVersion(string MonitorGroup, VersionInfo vers)
        {
            Util.DBug2("AllVersionPanel", "new current version received");
            Util.DBug2("AllVersionPanel", "asking FT for version info");
            VersionInfo versI =m_FTObjects.VersionInfoFromVersionName(vers.versionName);

            if (lastCurrentVersion != null)
            {
                
                this.ListAllVersionPanelList.AddFunctionCallToDataH(lastCurrentVersion, false);
            }

            if (versI != null)
            {
                this.ListAllVersionPanelList.AddFunctionCallToDataH(versI, true);

                lastCurrentVersion = versI;
            }
            if (HoldAllDisplay == true)
            {
                if (usemoncall == true && sendv == true)
                {
                    this.ListAllVersionPanelList.CallInvoke();
                    usemoncall = false;
                    sendv = false;
                    newcur = false;
                    HoldAllDisplay = false;
                }
                else
                {
                    newcur = true;
                }
            }
            else
            {
                this.ListAllVersionPanelList.CallInvoke();
            }

            
        }
        public void SendNewUserVersion(string MonitorGroup, List<string> UserVers)
        {
        }

        public void PleaseRegister()
        {

        }

        public void NewVersion(string MonitorGroup, List<VersionInfo> vers)
        {
            this.ListAllVersionPanelList.AddDataTop(vers);
        }

        public void DeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            if (m_FTObjects.DeleteRetur == FolderTrack.Delete.DeleteRules.DeleteReturn.NOT_REMOVED)
            {
                ListAllVersionPanelList.RemoveData(version);
            }
            else
            {
                this.ListAllVersionPanelList.AddFunctionCallToData(version, VersionRow.VersionOption.REMOVED);
            }
        }


        public void DeleteVersionCList(string MonitorGroup, List<VersionInfo> versionList)
        {
            if (m_FTObjects.DeleteRetur == FolderTrack.Delete.DeleteRules.DeleteReturn.NOT_REMOVED)
            {
                foreach (VersionInfo version in versionList)
                {
                    ListAllVersionPanelList.RemoveDataH(version);
                }
            }
            else
            {
                foreach (VersionInfo version in versionList)
                {
                    this.ListAllVersionPanelList.AddFunctionCallToDataH(version, VersionRow.VersionOption.REMOVED);
                }
            }

            ListAllVersionPanelList.CallInvoke();

        }


        public void UndeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            if (m_FTObjects.DeleteRetur == FolderTrack.Delete.DeleteRules.DeleteReturn.ALL)
            {
                if (lastCurrentVersion != null && version.versionName.Equals(lastCurrentVersion.versionName) == false)
                {
                    this.ListAllVersionPanelList.AddFunctionCallToData(version, VersionRow.VersionOption.NOT_REMOVED);
                }
                 
            }
            else
            {
                this.ListAllVersionPanelList.ClearAllData();
                this.ListAllVersionPanelList.AddData(m_FTObjects.CurrentVersionList);
            }
        }

        private void iAddToUndelete(VersionInfo version)
        {

        }


        public void UndeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
        }

        public void NewVersionInformation(string MonitorGroup, List<VersionInfo> vers)
        {
            this.ListAllVersionPanelList.ClearFunctionData();

            this.ListAllVersionPanelList.ClearAndAddData(m_FTObjects.CurrentVersionList);
            NewCurrentVersion(null, m_FTObjects.VersionInfoFromVersionName(lastCurrentVersion.versionName));
        }

        public void TaskUpdate(TaskGroup[] task)
        {

        }

        public void SetDeleteReturnVal(FolderTrack.Delete.DeleteRules.DeleteReturn delret )
        {
            this.ListAllVersionPanelList.ClearAllDataH();
            HoldAllDisplay = true;

        }

        public void SendData(List<ChangeInstruction> data, int idnum, bool done)
        {
        }

        public void SendDataV(List<VersionInfo> data, string name, int idnum, bool done)
        {
            if (showingWait == done && done == false)
            {
                ShowWait(!done);
            }
            
            this.ListAllVersionPanelList.ClearFunctionData();
            data.Reverse();

            if (HoldAllDisplay == true)
            {
                this.ListAllVersionPanelList.AddDataTopH(data);
            }
            else
            {
                this.ListAllVersionPanelList.AddDataTop(data);
            }

            if (done == false)
            {
                versCnt += data.Count;
                LoatStatusLabel.Invoke(new VoidIntDelegate(setAomountLoaded), new object[] { versCnt });
            }
            else
            {
                versCnt = 0;
                FunctionsThatNeedCompleteVersionInfo();
            }

            if (newcur == true && usemoncall == true)
            {
                this.ListAllVersionPanelList.CallInvoke();
                newcur = false;
                sendv = false;
                HoldAllDisplay = false;
            }
            else
            {
                sendv = true;
            }
        }

        public void DontMonitor(string MonitorGroup)
        {
            if (HoldAllDisplay == true)
            {
                this.ListAllVersionPanelList.CallAllH(VersionRow.DONT_USE);
            }
            else
            {
                this.ListAllVersionPanelList.CallAll(VersionRow.DONT_USE);
            }

            if (newcur == true && sendv == true)
            {
                this.ListAllVersionPanelList.CallInvoke();
                newcur = false;
                sendv = false;
                usemoncall = false;
                HoldAllDisplay = false;
            }
            else
            {
                usemoncall = true;
            }
        }

        public void RestartMonitor(string MonitorGroup)
        {
            this.ListAllVersionPanelList.CallAll(VersionRow.USE);
        }

        public void UseMonitor()
        {
            if (HoldAllDisplay == true)
            {
                this.ListAllVersionPanelList.CallAllH(VersionRow.USE);
            }
            else
            {
                this.ListAllVersionPanelList.CallAll(VersionRow.USE);
            }

            if (newcur == true && sendv == true)
            {
                this.ListAllVersionPanelList.CallInvoke();
                newcur = false;
                sendv = false;
                usemoncall = false;
                HoldAllDisplay = false;
            }
            else
            {
                usemoncall = true;
            }
        }

        public void NewDiscription(string MonitorGroup, VersionInfo version)
        {
            this.ListAllVersionPanelList.ClearFunctionData();

            this.ListAllVersionPanelList.ClearAndAddData(m_FTObjects.CurrentVersionList);
            NewCurrentVersion(null, m_FTObjects.VersionInfoFromVersionName(lastCurrentVersion.versionName));
        }

        #endregion



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
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ListRadioButton = new System.Windows.Forms.RadioButton();
            this.RelationshipRadioButton = new System.Windows.Forms.RadioButton();
            this.VersionPanel = new System.Windows.Forms.Panel();
            this.RelationShipPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.VersionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.ListRadioButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.RelationshipRadioButton, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.VersionPanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(138, 22);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ListRadioButton
            // 
            this.ListRadioButton.AutoSize = true;
            this.ListRadioButton.Location = new System.Drawing.Point(3, 3);
            this.ListRadioButton.Name = "ListRadioButton";
            this.ListRadioButton.Size = new System.Drawing.Size(41, 17);
            this.ListRadioButton.TabIndex = 1;
            this.ListRadioButton.TabStop = true;
            this.ListRadioButton.Text = "List";
            this.ListRadioButton.UseVisualStyleBackColor = true;
            this.ListRadioButton.CheckedChanged += new System.EventHandler(this.ListRadioButton_CheckedChanged);
            // 
            // RelationshipRadioButton
            // 
            this.RelationshipRadioButton.AutoSize = true;
            this.RelationshipRadioButton.Location = new System.Drawing.Point(50, 3);
            this.RelationshipRadioButton.Name = "RelationshipRadioButton";
            this.RelationshipRadioButton.Size = new System.Drawing.Size(83, 17);
            this.RelationshipRadioButton.TabIndex = 2;
            this.RelationshipRadioButton.TabStop = true;
            this.RelationshipRadioButton.Text = "Relationship";
            this.RelationshipRadioButton.UseVisualStyleBackColor = true;
            this.RelationshipRadioButton.CheckedChanged += new System.EventHandler(this.RelationshipRadioButton_CheckedChanged);
            // 
            // VersionPanel
            // 
            this.VersionPanel.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.SetColumnSpan(this.VersionPanel, 3);
            this.VersionPanel.Controls.Add(this.RelationShipPanel);
            this.VersionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionPanel.Location = new System.Drawing.Point(3, 26);
            this.VersionPanel.Name = "VersionPanel";
            this.VersionPanel.Size = new System.Drawing.Size(132, 1);
            this.VersionPanel.TabIndex = 3;
            // 
            // RelationShipPanel
            // 
            this.RelationShipPanel.BackColor = System.Drawing.Color.Transparent;
            this.RelationShipPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RelationShipPanel.Location = new System.Drawing.Point(0, 0);
            this.RelationShipPanel.Name = "RelationShipPanel";
            this.RelationShipPanel.Size = new System.Drawing.Size(132, 1);
            this.RelationShipPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(138, 22);
            this.TabIndex = 1;
            // 
            // ListPanelDesignerForm
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.VersionPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton ListRadioButton;
        private System.Windows.Forms.RadioButton RelationshipRadioButton;
        private System.Windows.Forms.Panel VersionPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel RelationShipPanel;



       
    }
}
