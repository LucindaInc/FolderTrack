using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Threading;
using System.Diagnostics;
using FolderTrackGuiTest1.NewMonitorGroup;
using FolderTrackGuiTest1.MonitorGroupChoser;

namespace FolderTrackGuiTest1
{
    public partial class MainForm : Form, FolderTrack.WCFContracts.FolderTrackCallBack
    {
        
        private FTObjects m_FTObjects;

        PanelList<MonitorGroupInfo> MonitorGrouPanLis = new PanelList<MonitorGroupInfo>();
        
        private VersionRow CurrentVerionRow;

        private Dictionary<int, VersionRow> m_VersionRowFromIndex;


        private object BlockAddingAndRemovingVersionRows;

        private bool ExitRemoveKey;
        private delegate void VoidStringDelegate(string str);

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(FTObjects ftobjects)
        {
            Util.DBug2("MainForm", "Starting");
            this.m_FTObjects = ftobjects;
            Util.DBug2("I", "Requesting Monitor Group Info");
            List<MonitorGroupInfo> monitorGrList = m_FTObjects.GetAllMonitorGroupInfor();
            Util.DBug2("I", "Received Monitor Group Info " + monitorGrList.Count+" items ");

            InitializeComponent();
            Util.DBug2("MainForm", "Done initilize components");
            ftobjects.SetMainForm(this);
            Util.DBug2("MainForm", "Done SetMainForm");
            m_FTObjects.AddToCallList(this);
            Util.DBug2("MainForm", "Done AddToCallList");
            PanelFromMonitorGInfo pan = new PanelFromMonitorGInfo();
            Util.DBug2("MainForm", "Done new pan");
            pan.mainfor = this;
            MonitorGrouPanLis.PanelFromData = pan;
            Util.DBug2("MainForm", "Done pan from data");
            this.Monitpanel.Controls.Add(this.MonitorGrouPanLis);
            Util.DBug2("MainForm", "Add panels");
            this.MonitorGrouPanLis.Dock = DockStyle.Fill;
            if (monitorGrList.Count == 1)
            {
                Util.DBug2("MainForm", "Open monitor group");
                OpenMonitorGroup(monitorGrList[0].name);
            }
            else
            {
                Util.DBug2("MainForm", "Show chooser");
                showMonitorChoser(monitorGrList);
            }

        //    this.allVersionPanel1.P_FTObjects = this.m_FTObjects;
      //      this.calendarPanel1.P_FTObjects = this.m_FTObjects;
     //       this.searchPanel1.P_FTObjects = this.m_FTObjects;
      //      this.explorerTab1.P_FTObjects = this.m_FTObjects;
            

        }

       

        public void OpenMonitorGroup(string monitorloc)
        {
            Util.DBug2("MainForm", "Enter Open monitor group");
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidStringDelegate(OpenMonitorGroup), new object[] { monitorloc });
                return;
            }
            Util.DBug2("MainForm", "name " + monitorloc);
            MonitorGroupName = monitorloc;
            Util.DBug2("MainForm", "Enter allVersionPanel1");
            this.allVersionPanel1.P_FTObjects = this.m_FTObjects;
            Util.DBug2("MainForm", "Enter calendarPanel1");
            this.calendarPanel1.P_FTObjects = this.m_FTObjects;
            Util.DBug2("MainForm", "Enter searchPanel1");
            this.searchPanel1.P_FTObjects = this.m_FTObjects;
            Util.DBug2("MainForm", "Enter explorerTab1");
            this.explorerTab1.P_FTObjects = this.m_FTObjects;
            Util.DBug2("MainForm", "Done explorerTab1");

            this.Monitpanel.Visible = false;
            Util.DBug2("MainForm", "Done Monitpanel");
            this.tabControl1.Visible = true;
            Util.DBug2("MainForm", "Done tabControl1");
        }

        private void showMonitorChoser()
        {
            List<MonitorGroupInfo> monitorGrList = m_FTObjects.GetAllMonitorGroupInfor();
            showMonitorChoser(monitorGrList);
        }

        private void showMonitorChoser(List<MonitorGroupInfo> monitorGrList)
        {
            this.MonitorGrouPanLis.ClearAndAddData(monitorGrList);
            this.Monitpanel.Visible = true;
            this.tabControl1.Visible = false;

        }

        public void ExploreVersion(string version)
        {
            this.explorerTab1.SetExploredVersion(version);
            SwitchToExploreTab();
        }

        public void SwitchToExploreTab()
        {
            this.tabControl1.SelectTab(this.Exploretab);
        }

        public string MonitorGroupName
        {
            get
            {
                
                return m_FTObjects.CurrentMonitorGroup;
            }

            set
            {
                Util.DBug2("MainForm", "Enter CurrentMonitorGroup");
                m_FTObjects.CurrentMonitorGroup = value;
            }
        }


        #region FolderTrackCallBack Members

        public void NewCurrentVersion(string MonitorGroup, VersionInfo vers)
        {
            if (CurrentVerionRow != null && CurrentVerionRow.IsDisposed == false)
            {
                CurrentVerionRow.UnIndicateCurrentVersion();
            }
            foreach (KeyValuePair<int,VersionRow> kVer in m_VersionRowFromIndex)
            {
                if(kVer.Value.PublicVersionInfo.versionName.Equals(vers.versionName))
                {
                    kVer.Value.IndicateCurrentVersion();
                    CurrentVerionRow = kVer.Value;
                    break;
                }
            }
        }

        public void SendNewUserVersion(string MonitorGroup, string UserVers)
        {
        }

        public void PleaseRegister()
        {
            
        }

        public void NewVersion(string MonitorGroup, VersionInfo vers)
        {
            //TODO
        }

        public void NewVersionInformation(string MonitorGroup, List<VersionInfo> vers)
        {

        }

       

        private void allVersionPanel1_Paint(object sender, PaintEventArgs e)
        {

        }



        public void NewDiscription(string MonitorGroup, VersionInfo version)
        {
            //
        }

        #endregion

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread OpenThre = new Thread(voidOpenNewMonitorGroup);
            OpenThre.SetApartmentState(ApartmentState.STA);
            OpenThre.Start();
            
        }

        private void voidOpenNewMonitorGroup()
        {
            OpenNewMonitorGroup();
        }


        private DialogResult OpenNewMonitorGroup()
        {
            NewMonitorGroupForm mgDia = new NewMonitorGroupForm();
            DialogResult di = mgDia.ShowDialog();
            if (di == DialogResult.OK)
            {
                m_FTObjects.NewMonitorGroup(mgDia.LocationManager.NameOfMonitor, mgDia.LocationManager.MonitorLocation);
                OpenMonitorGroup(mgDia.LocationManager.NameOfMonitor);
            }
            mgDia.Dispose();
            return di;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox(m_FTObjects.GetInformation());
            ab.ShowDialog();
        }



        
    }
}