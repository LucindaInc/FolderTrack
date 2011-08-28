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
using FolderTrackGuiTest1.Task;
using FolderTrackGuiTest1.CommonGuiFunctions;
using System.IO;
using FolderTrackGuiTest1.MGProperties;
using FolderTrack.ExclusionRules;
using FolderTrackGuiTest1.Filters;
using ZetaLongPaths;
using FolderTrackGuiTest1.Delete;
using FolderTrack.Delete;

namespace FolderTrackGuiTest1
{
    public partial class MainForm : Form, FolderTrack.WCFContracts.FolderTrackCallBack
    {
        
        private FTObjects m_FTObjects;
        private FTObjects abFTObjects;

        PanelList<MonitorGroupInfo> MonitorGrouPanLis = new PanelList<MonitorGroupInfo>();
        PanelList<MonitorGroupInfo> EMonitorGrouPanLis = new PanelList<MonitorGroupInfo>();
        TaskGroupChoose taskChoose;
        UnRemove unremove;
        List<MonitorGroupInfo> monitorGrList;
        List<MonitorGroupInfo> monitorGrListOpen;
        private bool MonGroVis;
        private bool EMonGroVis;
        private bool NoMonGr;
        private VersionRow CurrentVerionRow;

        private Dictionary<int, VersionRow> m_VersionRowFromIndex;


        private object BlockAddingAndRemovingVersionRows;

        private bool ExitRemoveKey;


        bool hideTaskButton=true;
        TaskGroup[] CurrentTaskArr;

        public delegate void VoidTaskGroupArrDelegate(TaskGroup[] taskarr);

        private delegate void VoidStringDelegate(string str);

        private delegate void DeleteRulesNoArgDelegate(DeleteRules rules);

        private delegate void VoidListMonitGroI(List<MonitorGroupInfo> inf);

        private delegate void VoidNoArgDelegate();

        private delegate void VoidControlDelega(Control cont);
        int amountOfErroFlases = 9;
        int amountLe;

        public MainForm()
        {
            InitializeComponent();
            this.NoMonitorGroupPanel.Visible = NoMonGr;
            NoMonGr = true;
            taskChoose = new TaskGroupChoose(this);
            PanelFromMonitorGInfo pan = new PanelFromMonitorGInfo();
            unremove = new UnRemove(this);
            unremove.BackColor = Color.White;
            Util.DBug2("MainForm", "Done new pan");
            pan.mainfor = this;
            MonitorGrouPanLis.PanelFromData = pan;
            Util.DBug2("MainForm", "Done pan from data");
            this.Monitpanel.Controls.Add(this.MonitorGrouPanLis);
            this.Monitpanel.Controls.Add(this.EMonitorGrouPanLis);
            this.Monitpanel.Controls.Add(this.taskChoose);
            this.Monitpanel.Controls.Add(unremove);
            unremove.Dock = DockStyle.Fill;
            this.EMonitorGrouPanLis.Visible = false;
            this.MonitorGrouPanLis.Visible = false;
            EMonGroVis = this.EMonitorGrouPanLis.Visible;
            MonGroVis = this.MonitorGrouPanLis.Visible;
            this.taskChoose.Dock = DockStyle.Fill;
            this.MonitorGrouPanLis.Dock = DockStyle.Fill;
            this.EMonitorGrouPanLis.Dock = DockStyle.Fill;

            new Thread(deleteTemp).Start();

        }

        private void deleteTemp()
        {
            string PreDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                                 + Path.DirectorySeparatorChar + Util.Company + Path.DirectorySeparatorChar
                                 + Util.Product
                                 + Settings.Default.tempDir
                                 + Path.DirectorySeparatorChar;
            try
            {
                ZlpIOHelper.DeleteDirectory(PreDir, true);
                
            }
            catch
            {
                //throw away
            }
        }

        public void TaskClosed()
        {
            if (this.Monitpanel.InvokeRequired)
            {
                this.Monitpanel.Invoke(new VoidNoArgDelegate(TaskClosed));
                return;
            }

            this.MonitorGrouPanLis.Visible = MonGroVis;
            this.EMonitorGrouPanLis.Visible = EMonGroVis;
            if (NoMonGr == true || monitorGrList == null || monitorGrList.Count < 1)
            {
                if (m_FTObjects != null)
                {
                    monitorGrList = m_FTObjects.GetAllMonitorGroupInfor();
                }
                if (monitorGrList == null ||  monitorGrList.Count < 1)
                {
                    ShowNoMonitorGroup();
                }
            }

            if (this.MonitorGrouPanLis.Visible == false && this.EMonitorGrouPanLis.Visible == false && NoMonGr == false)
            {
                this.Monitpanel.Visible = false;
            }
        }

        /// <summary>
        /// The purpose of thing function is to set FTObject but only
        /// the task gets it. This is so that taskAnswers canbe sent back
        /// </summary>
        public void SetFTBeforeOpenMonGr(FTObjects ftob)
        {
            this.taskChoose.SetFTOb(ftob);
            EPanelFromMonitorGInfo epan = new EPanelFromMonitorGInfo();
            epan.ftobjects = ftob;
            epan.mainForm = this;
            this.EMonitorGrouPanLis.PanelFromData = epan;
            abFTObjects = ftob;
            abFTObjects.SetMainForm(this);
            if (ftob.CheckLicense())
            {
                RemoveLicenseOption();
            }
            
        }

       

      
       
        private void AddMonitControl(Control cont)
        {
            this.Monitpanel.Controls.Add(cont);
        }

        public void SetFtObjects(object ftobjectsO)
        {
            FTObjects ftobjects = (FTObjects)ftobjectsO;
            
            this.m_FTObjects = ftobjects;
            List<MonitorGroupInfo> monitorGrList = m_FTObjects.GetAllMonitorGroupInfor();
           
            
            
            m_FTObjects.AddToCallList(calendarPanel1);
            m_FTObjects.AddToCallGuiCallList(calendarPanel1);
            m_FTObjects.AddToCallList(searchPanel1);
            m_FTObjects.AddToCallGuiCallList(searchPanel1);
            m_FTObjects.AddToCallList(explorerTab1);
            m_FTObjects.AddToCallList(allVersionPanel1);
            m_FTObjects.AddToCallGuiCallList(allVersionPanel1);
            //this is not good programming practice should be changed
            m_FTObjects.AddToCallList(allVersionPanel1.relationpan);

            if (monitorGrList.Count == 1 && monitorGrList[0].error == false)
            {
                
                try
                {
                    NoMonitorGroupPanel.Invoke(new VoidNoArgDelegate(HideNoMonitorGroup));
                }
                catch (InvalidOperationException)
                {
                    NoMonGr = false;
                }
                OpenMonitorGroup(monitorGrList[0].name);
            }
            else if (monitorGrList.Count > 1 ||
                monitorGrList.Count == 1 && monitorGrList[0].error == true)
            {
                Util.DBug2("MainForm", "Show chooser");
              //  new Thread(HideNoMonitorGroup).Start();
                try
                {
                    this.NoMonitorGroupPanel.Invoke(new VoidNoArgDelegate(HideNoMonitorGroup));
                }
                catch (InvalidOperationException)
                {
                    NoMonGr = false;
                }
                showMonitorChoser(monitorGrList);
            }
            else
            {
                ShowNoMonitorGroup();
            }

           


        }

        public void HideNoMonitorGroup()
        {
         

            this.NoMonitorGroupPanel.Visible = false;
            NoMonGr = false;
        }


        public void ShowNoMonitorGroup()
        {
            if (this.NoMonitorGroupPanel.InvokeRequired)
            {
                this.NoMonitorGroupPanel.Invoke(new VoidNoArgDelegate(ShowNoMonitorGroup));
                return;
            }

            this.NoMonitorGroupPanel.Visible = true;
            
            NoMonGr = true;
            if (this.NoMonitorGroupPanel.BackColor == Color.IndianRed)
            {
                this.NoMonitorGroupPanel.BackColor = Color.Linen;
                this.NoMonitotGroupLabel.BackColor = Color.Linen;
            }
            else
            {
                this.NoMonitorGroupPanel.BackColor = Color.IndianRed;
                this.NoMonitotGroupLabel.BackColor = Color.IndianRed;
            }

        }


       

        public void OpenMonitorGroup(string monitorloc)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidNoArgDelegate(HideOpenMonitGr));
                this.Invoke(new VoidNoArgDelegate(ShowTaskChoseAndTabContr));
                this.Invoke(new VoidNoArgDelegate(OpenMonGrp));
            }
            else
            {
                HideOpenMonitGr();
                ShowTaskChoseAndTabContr();
                OpenMonGrp();
            }
            MonitorGroupName = monitorloc;
            

        }

        public void HideOpenMonitGr()
        {
            
            this.Monitpanel.Visible = true;
            this.MonitorGrouPanLis.Visible = false;
            this.EMonitorGrouPanLis.Visible = false;
            MonGroVis = false;
            EMonGroVis = false;

        }

        private void ShowTaskChoseAndTabContr()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidNoArgDelegate(ShowTaskChoseAndTabContr));
                return;
            }
            this.taskChoose.Visible = true;
            this.taskChoose.Dock = DockStyle.Fill;
            this.tabControl1.Visible = true;
        }

        private void OpenMonGrp()
        {
            
                if (this.InvokeRequired)
                {
                    
                        this.Invoke(new VoidNoArgDelegate(  OpenMonGrp));
                        return;
                    
                }
                GuiFunctions.m_FTObjects = this.m_FTObjects;
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
                this.unremove.P_FTObjects = this.m_FTObjects;
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Thread(showMonitorChoser).Start();
        }

        private void showMonitorChoser()
        {
            monitorGrListOpen = null;
            if (m_FTObjects != null)
            {
                monitorGrListOpen = m_FTObjects.GetAllMonitorGroupInfor();
            }
            if (monitorGrListOpen != null && monitorGrListOpen.Count > 0)
            {
                if (monitorGrListOpen.Count == 1 && monitorGrListOpen[0].error == false)
                {
                    
                    OpenMonitorGroup(monitorGrListOpen[0].name);
                }
                else
                {
                    showMonitorChoser(monitorGrListOpen);
                }
            }
            else
            {
                ShowNoMonitorGroup();
            }
            
        }

        

        private void showMonitorChoser(List<MonitorGroupInfo> monitorGrList)
        {
            monitorGrListOpen = monitorGrList;
            if (this.InvokeRequired == true)
            {
                this.Invoke(new VoidListMonitGroI(showMonitorChoser), new object[] { monitorGrListOpen });
                return;
            }
            this.MonitorGrouPanLis.ClearAndAddData(monitorGrListOpen);
            ShowMonitpanel();
            HideTabCotn();
            HideTaskChoose();
            ShowMonitGrouPan();

        }

        private void HideTabCotn()
        {
            if (this.tabControl1.InvokeRequired)
            {
                this.tabControl1.Invoke(new VoidNoArgDelegate(HideTabCotn));
                return;
            }
            this.tabControl1.Visible = false;
        }

        private void HideTaskChoose()
        {
            if(this.taskChoose.InvokeRequired)
            {
                this.taskChoose.Invoke(new VoidNoArgDelegate(HideTaskChoose));
                return;
            }
            this.taskChoose.Visible = false;
        }

        private void ShowMonitpanel()
        {
            if (this.Monitpanel.InvokeRequired)
            {
                this.Monitpanel.Invoke(new VoidNoArgDelegate(ShowMonitpanel));
                return;
            }
            this.Monitpanel.Visible = true;
        }

        private void ShowMonitGrouPan()
        {
            if (this.MonitorGrouPanLis.InvokeRequired)
            {
                this.MonitorGrouPanLis.Invoke(new VoidNoArgDelegate(ShowMonitGrouPan));
                return;
            }
            this.MonitorGrouPanLis.Visible = true;
            MonGroVis = this.MonitorGrouPanLis.Visible;
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
                if (m_FTObjects == null)
                {
                    return null;
                }
                else
                {
                    return m_FTObjects.CurrentMonitorGroup;
                }

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
  //          if (CurrentVerionRow != null && CurrentVerionRow.IsDisposed == false)
 //           {
  //              CurrentVerionRow.UnIndicateCurrentVersion();
  //          }
   //         foreach (KeyValuePair<int,VersionRow> kVer in m_VersionRowFromIndex)
   //         {
    //            if(kVer.Value.PublicVersionInfo.versionName.Equals(vers.versionName))
    //            {
     //               kVer.Value.IndicateCurrentVersion();
    //                CurrentVerionRow = kVer.Value;
    //                break;
    //            }
     //       }
        }

        public void SendData(List<ChangeInstruction> data, int idnum, bool done)
        {

        }

        public void SendDataV(List<VersionInfo> data, string name, int idnum, bool done)
        {
        }

        public void SendNewUserVersion(string MonitorGroup, List<string> UserVers)
        {
        }

        public void SetMonitorGrouPanLis()
        {
            if (this.MonitorGrouPanLis.InvokeRequired == true)
            {
                this.MonitorGrouPanLis.Invoke(new VoidNoArgDelegate(SetMonitorGrouPanLis));
                return;
            }
            this.MonitorGrouPanLis.Visible = MonGroVis;
        }

        public void SetEMonitorGrouPanLis()
        {
            if (this.EMonitorGrouPanLis.InvokeRequired == true)
            {
                this.EMonitorGrouPanLis.Invoke(new VoidNoArgDelegate(SetEMonitorGrouPanLis));
                return;
            }
            this.EMonitorGrouPanLis.Visible = EMonGroVis;
        }

        public void taskChooseFalse()
        {
            if (this.taskChoose.InvokeRequired)
            {
                this.taskChoose.Invoke(new VoidNoArgDelegate(taskChooseFalse));
                return;
            }
            this.taskChoose.Visible = false;
        }

        public void ErrorButtonFalse()
        {
            if (this.ErrorButton.InvokeRequired)
            {
                this.ErrorButton.Invoke(new VoidNoArgDelegate(ErrorButtonFalse));
                return;
            }
            this.ErrorButton.Visible = false;
        }

        public void HideTask()
        {



            if (this.Monitpanel.InvokeRequired == true)
            {
                this.Monitpanel.Invoke(new VoidNoArgDelegate(HideTask));
                return;
            }
            SetMonitorGrouPanLis();
            SetEMonitorGrouPanLis();

            taskChooseFalse();
            if (this.MonitorGroupName == null)
            {
                try
                {
                    this.Monitpanel.Visible = true;
                }
                catch (InvalidOperationException)
                {
                    this.Monitpanel.Invoke(new VoidNoArgDelegate(HideTask));
                    return;
                }
                showMonitorChoser();

            }
            else
            {
                if (MonGroVis == false && EMonGroVis == false)
                {
                    this.Monitpanel.Visible = false;
                }
            }
            hideTaskButton = true;
            ErrorButtonFalse();
        }

        public void TaskUpdate(TaskGroup[] task)
        {
            bool allowHide = true;
            if (CurrentTaskArr != null)
            {
                
                foreach (TaskGroup tas in CurrentTaskArr)
                {
                    if (tas.Status == TaskGroup.FAILED)
                    {
                        allowHide = false;
                        break;
                    }
                }
            }

            if (task == null && allowHide)
            {
                new Thread(HideTask).Start();
                return;
            }

            if (task != null)
            {
                CurrentTaskArr = task;
            }
            foreach (TaskGroup tas in task)
            {
                Util.DBug2("TaskManager", tas.TaskName);
                foreach (FolderTrack.Types.Task t in tas.TaskList.Values)
                {
                    Util.DBug2("MainForm", t.Action + " " + t.Detail + " " + t.percent);
                }
            }

            bool showAllTask = false;
            if (this.taskChoose.Visible == false)
            {
                foreach (TaskGroup t in task)
                {
                    if (t.Status == TaskGroup.PERSISTANT_FAIL)
                    {
                        showAllTask = true;
                        break;
                    }
                    foreach (FolderTrack.Types.Task ta in t.TaskList.Values)
                    {
                        if (ta.ErrorDiscrip != null)
                        {
                            showAllTask = true;
                            break;
                        }
                    }
                    if (showAllTask == true)
                    {
                        break;
                    }

                }
            }
            else
            {
                showAllTask = true;
            }


            if (showAllTask)
            {
                ShowAllTask(task);
            }
            else
            {
                hideTaskButton = false;
                new Thread(BlinkButton).Start();
            }
            
        }

        public void  BlinkButton()
        {

            do
            {
                if (hideTaskButton == true)
                {
                    return;
                }
                ShowTaskButtonThr();
                Thread.Sleep(400);
                if (this.taskChoose.Visible == true)
                {
                    return;
                }
            } while (amountLe > 0);
        }


        public void ShowTaskButtonThr()
        {
            if (amountLe <= 0)
            {
                amountLe = amountOfErroFlases;
            }
            if (this.ErrorButton.InvokeRequired)
            {
                this.ErrorButton.Invoke(new VoidNoArgDelegate(ShowTaskButtonThr));
                return;
            }
            if (hideTaskButton == true)
            {
                return;
            }

            this.ErrorButton.Visible = true;
            if (amountLe % 2 == 0)
            {
                this.ErrorButton.BackColor = Color.Green;
                this.ErrorButton.ForeColor = Color.Black;
            }
            else
            {
                this.ErrorButton.BackColor = Color.Black;
                this.ErrorButton.ForeColor = Color.Green;
            }
            amountLe--;

        }

        public void ShowAllTask(TaskGroup[] task)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidTaskGroupArrDelegate(ShowAllTask), new object[] { task });
                return;
            }

            this.NoMonitorGroupPanel.Visible = false;
            this.MonitorGrouPanLis.Visible = false;
            

            this.EMonitorGrouPanLis.Visible = false;
            

            this.taskChoose.Visible = true;
            this.Monitpanel.Visible = true;
            
                this.taskChoose.SetGroup(task);
           
        }

        public void PleaseRegister()
        {
            
        }

        public void NewVersion(string MonitorGroup, List<VersionInfo> vers)
        {
            //TODO
        }

        public void DeleteVersionC(string MonitorGroup, VersionInfo version)
        {
        }


        public void DeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
        }


        public void UndeleteVersionC(string MonitorGroup, VersionInfo version)
        {
        }


        public void UndeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
        }

        public void NewVersionInformation(string MonitorGroup, List<VersionInfo> vers)
        {

        }

        public void DontMonitor(string MonitorGroup)
        {
        }

        public void RestartMonitor(string MonitorGroup)
        {
        }



        public void NewDiscription(string MonitorGroup, VersionInfo version)
        {
            //
        }

        #endregion

        private void allVersionPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

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
            //was no monitor showing
            bool BegenNoState = NoMonGr;
          //  new Thread(HideNoMonitorGroup).Start();
            this.NoMonitorGroupPanel.Invoke(new VoidNoArgDelegate(HideNoMonitorGroup));
            DialogResult di = mgDia.ShowDialog();
            bool ret = false;
            if (di == DialogResult.OK)
            {
                FTObjects oFtP = null;
                if (m_FTObjects != null)
                {
                    oFtP = m_FTObjects;
                }
                else if (abFTObjects != null)
                {
                    oFtP = abFTObjects;
                }
                
                ret = oFtP.NewMonitorGroup(mgDia.LocationManager.NameOfMonitor, mgDia.LocationManager.MonitorLocation,mgDia.LocationManager.filter);
                if (m_FTObjects == null)
                {
                    SetFtObjects(oFtP);
                }
                if (ret == true)
                {
                    OpenMonitorGroup(mgDia.LocationManager.NameOfMonitor);
                }
                
            }
            mgDia.Dispose();
            if (ret == false && BegenNoState == true)
            {
                new Thread(ShowNoMonitorGroup).Start();
            }
            return di;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            AboutBox ab; 
            if (m_FTObjects != null)
            {
                ab = new AboutBox(m_FTObjects.GetInformation());
                ab.ShowDialog();
            }
            else if (abFTObjects != null)
            {
                ab = new AboutBox(abFTObjects.GetInformation());
                ab.ShowDialog();
            }
        }

        private void ErrorButton_Click(object sender, EventArgs e)
        {
            new Thread(CallShow).Start();
            this.ErrorButton.Visible = false;
        }

        public void CallShow()
        {
            ShowAllTask(CurrentTaskArr);
        }

        private void NoMonitoringGroupButton_Click(object sender, EventArgs e)
        {
            Thread OpenThre = new Thread(voidOpenNewMonitorGroup);
            OpenThre.SetApartmentState(ApartmentState.STA);
            OpenThre.Start();
        }

        private void stopDeleteMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Thread(showEMonitorChoser).Start();
        }

        private void showEMonitorChoser()
        {
            monitorGrList = null;
            if (m_FTObjects != null)
            {
                monitorGrList = m_FTObjects.GetAllMonitorGroupInfor();
            }
            if (monitorGrList != null && monitorGrList.Count > 0)
            {
                showEMonitorChoser(monitorGrList);
            }
            else
            {
                ShowNoMonitorGroup();
            }

        }



        private void showEMonitorChoser(List<MonitorGroupInfo> monitorGrList)
        {
            if (this.EMonitorGrouPanLis.InvokeRequired == true)
            {
                this.Invoke(new VoidListMonitGroI(showEMonitorChoser), new object[] { monitorGrList });
                return;
            }
            ShowEMonitGrouPan();
            this.EMonitorGrouPanLis.ClearAndAddData(monitorGrList);
            ShowMonitpanel();
            HideTabCotn();
            HideTaskChoose();

        }

        private void ShowEMonitGrouPan()
        {
            if (this.EMonitorGrouPanLis.InvokeRequired)
            {
                this.EMonitorGrouPanLis.Invoke(new VoidNoArgDelegate(ShowEMonitGrouPan));
                return;
            }
            this.MonitorGrouPanLis.Visible = false;
            this.EMonitorGrouPanLis.Visible = true;

            MonGroVis = false;
            EMonGroVis = true;
        }

        public void DeletMonitoringGroup(string name)
        {
            m_FTObjects.DeletMonitoringGroup(name);
        }


        public void HandleMonitorGroDelOpen(MonitorGroupInfo infi)
        {

            if (monitorGrListOpen != null)
            {
                monitorGrListOpen.Remove(infi);
                if (monitorGrListOpen.Count == 0)
                {
                    this.Invoke(new VoidNoArgDelegate(HideNoMonitorGroup));
                    new Thread(ShowNoMonitorGroup).Start();
                }
                else if (monitorGrListOpen.Count == 1 && monitorGrListOpen[0].error == false)
                {
                    OpenMonitorGroup(monitorGrListOpen[0].name);
                }
                this.MonitorGrouPanLis.RemoveData(infi);
            }
        }

        public void HandleMonitorGroDel(MonitorGroupInfo infi)
        {

            if(monitorGrList != null)
            {
                monitorGrList.Remove(infi);
                if (monitorGrList.Count == 0)
                {
                    this.Invoke(new VoidNoArgDelegate(HideOpenMonitGr));
                    new Thread(ShowNoMonitorGroup).Start();
                }
                this.EMonitorGrouPanLis.RemoveData(infi);
            }
        }

        public void HandleMonitorGroStp(MonitorGroupInfo infi)
        {
            if (monitorGrList != null)
            {
                this.EMonitorGrouPanLis.AddFunctionCallToData(infi, true);
            }
        }

        public void HandleMonitorGroRes(MonitorGroupInfo infi)
        {
            if (monitorGrList != null)
            {
                this.EMonitorGrouPanLis.AddFunctionCallToData(infi, false);
            }
        }

        private void licenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(abFTObjects.PleaseRegister)).Start((object)false);
        }

        

        public void RemoveLicenseOption()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidNoArgDelegate(RemoveLicenseOption));
                return;
            }
            this.helpToolStripMenuItem.DropDownItems.Remove(this.licenseToolStripMenuItem);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_FTObjects == null)
            {
                //add something telling user options are not available
                return;
            }
            if (m_FTObjects.CurrentMonitorGroup != null)
            {
                Options o = m_FTObjects.GetOptions();
                if (o == null)
                {
                    //Todo add something telling user options are not available 
                    return;
                }
                
                //MGPro pro = new MGPro();
                
                List<string> monitor = new List<string>();
                List<string> FilterStrings;
                monitor.AddRange(m_FTObjects.GetLocationsInMonitorGroup());
               // pro.monitor = monitor;
                FilterStrings= m_FTObjects.GetFilters();
              //  pro.SetOptions(o);
                SelectFilterFromMonitorGroup gr = new SelectFilterFromMonitorGroup(o, monitor);
                gr.ShowDialog();
                if (this.InvokeRequired)
                {
                    int a = 1 + 2;
                }
             //   pro.ShowDialog(this);

              //  if (pro.DialogResult == DialogResult.OK)
                if(gr.DialogResult == DialogResult.OK)
                {
                    o.filtCh = gr.GetFilterList();
                    
                    new Thread(new ParameterizedThreadStart(SendOptions)).Start(o);
                }
              //  pro.Dispose();
                gr.Dispose();
            }
        }

        private void SendOptions(Object Ooption)
        {
            Options o = (Options)Ooption;
            m_FTObjects.OptionChanes(o);

        }

        

        
        private void tdeleteTool()
        {
            DeleteRules rules = m_FTObjects.getDeleteRules();
            this.Invoke(new DeleteRulesNoArgDelegate(ideleteTool),new object[] {rules});
        }

        private void ideleteTool(DeleteRules rules)
        {
            DeleteForm dfor = new DeleteForm(rules);
            dfor.ShowDialog(this);
            if (dfor.DialogResult == DialogResult.OK)
            {
                new Thread(new ParameterizedThreadStart(tSendRules)).Start(dfor.m_DeleteRules);
            }
            dfor.Dispose();
        }

        private void tSendRules(object rulesO)
        {
            DeleteRules rules = (DeleteRules) rulesO;
            m_FTObjects.DeleteRules(rules);
        }

        private void deleteOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Thread(tdeleteTool).Start();
        }

        private void undeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Thread(tShowRemove).Start();
        }

        private void tShowRemove()
        {
            Monitpanel.Invoke(new VoidNoArgDelegate(iShowUnRemove));
        }

        private void iShowUnRemove()
        {
            unremove.CallDelete();
            this.taskChoose.Visible = false;
            unremove.Visible = true;
            Monitpanel.Visible = true;
        }

        public void HideRemove()
        {
            new Thread(tHideRemove).Start();
        }

        private void tHideRemove()
        {
            Monitpanel.Invoke(new VoidNoArgDelegate(iHideRemove));
        }

        private void iHideRemove()
        {
            this.taskChoose.Visible = true;
            unremove.Visible = false;
            Monitpanel.Visible = false;
        }

        private void showDeletedVersionsInResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showDeletedVersionsInResultsToolStripMenuItem.Checked = !this.showDeletedVersionsInResultsToolStripMenuItem.Checked;
            new Thread(new ParameterizedThreadStart(tSetReturnVers)).Start(this.showDeletedVersionsInResultsToolStripMenuItem.Checked);
        }

        private void tSetReturnVers(object showRemovedO)
        {
            bool showRemoved = (bool) showRemovedO;
            
            if (showRemoved)
            {
                m_FTObjects.SetDeleteReturnVal(DeleteRules.DeleteReturn.ALL);
            }
            else
            {
                m_FTObjects.SetDeleteReturnVal(DeleteRules.DeleteReturn.NOT_REMOVED);
            }
        }

        
    }
}