using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using FolderTrack.Types;
using System.Threading;
using FolderTrackGuiTest1.VersionPanels;
using System.Drawing;

namespace FolderTrackGuiTest1.SearchTab
{
    class SearchPanel : SplitContainer, FolderTrack.WCFContracts.FolderTrackCallBack, DataManager.Status,FTObjects.GuiMessage
    {
        object newVerOb = new object();
        private FTObjects m_FTObjects;
        VersionInfoSearchCriteria m_Criteria;
        public PanelList<VersionInfo> VersionInfoPanelList;
        public PanelList<string> ChangeFPanelList;
        private delegate void HandleMonthChangeDelegate();
        private delegate void VoidNoArgDelegate();
        private delegate void VoidListStringDelegate(List<string> de);
        HandleMonthChangeDelegate InvokeMonthChan;
        VersionInfo lastCurrentVersion;
        Thread searchThread;
        System.Threading.Timer time;
        private bool calld = false;
        public bool HoldAllDisplay;
        public bool funcver;
        public bool newcur;
        public bool usem;
        public bool includeRemove;
        public bool CallPerformS;

        private Dictionary<string, int> MonthNumFromName;

        /// <summary>
        /// Converts a 12am to 0, 1 am to 1, 1pm to 13
        /// </summary>
        private Dictionary<string, int> HourNFromName;
        Label l;
        bool showingWait = false;
        public delegate void VoidBoolDelegate(bool b);
        public delegate void VoidStringDelegate(string s);
        public SearchPanel()
        {
            InitializeComponent();
            InitializeVariables();
            this.VersionInfoPanelList = new PanelList<VersionInfo>();
            l = new Label();
            l.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l.Text = "Please Wait";
            l.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            l.Visible = false;
            l.AutoSize = true;
            this.VersionInfoPanelList.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(l);
            this.panel1.Controls.Add(this.VersionInfoPanelList);
            this.ChangeFPanelList = new PanelList<string>();
            this.panel2.Controls.Add(this.ChangeFPanelList);
            this.ChangeFPanelList.Dock = DockStyle.Fill;
            ChangeFPanelList.PanelFromData = new PanelFromChange(this);
            this.DoubleBuffered = true;
            InvokeMonthChan = new HandleMonthChangeDelegate(HandleMonthChange);
        }

        private void InitializeVariables()
        {
            m_Criteria = new VersionInfoSearchCriteria();
            MonthNumFromName = new Dictionary<string, int>();
            HourNFromName = new Dictionary<string, int>();
        }

        private void ShowWait(object bO)
        {
            bool b = (bool)bO;
            if (showingWait == b)
            {
                return;
            }


            showingWait = b;
            if (b == true)
            {
                l.Invoke(new VoidBoolDelegate(setL), new object[] { true });
                VersionInfoPanelList.Invoke(new VoidBoolDelegate(setListAllVersion), new object[] { false });
            }
            else
            {
                l.Invoke(new VoidBoolDelegate(setL), new object[] { false }); ;
                VersionInfoPanelList.Invoke(new VoidBoolDelegate(setListAllVersion), new object[] { true });
            }


        }

        private void setL(bool set)
        {
            l.Visible = set;
        }

        private void setListAllVersion(bool set)
        {
            VersionInfoPanelList.Visible = set;
        }

        #region Status Members

        public void ReceiveStatus(string stat, bool done)
        {
            if (done == showingWait)
            {
                ShowWait(!done);
            }
            if (done == false)
            {
                l.Invoke(new VoidStringDelegate(SetlText), new object[] { stat });
            }
        }

        #endregion

        public void SetlText(string tex)
        {
            l.Text = tex;
        }


        public FTObjects P_FTObjects
        {
            set
            {
                m_FTObjects = value;
                
                VersionInfo vers = m_FTObjects.GetCurrentVersionInfo();
            }
        }
        
        public void FunctionsThatNeedCompleteVersionInfo()
        {
            
            
            VersionInfo vers = m_FTObjects.GetCurrentVersionInfo();
            if (vers != null)
            {
                NewCurrentVersion(null, vers);
            }
            if (m_FTObjects.GetDontMonitor)
            {
                DontMonitor(m_FTObjects.CurrentMonitorGroup);
            }
            else
            {
                UseMonitor();
            }
            
           
            FillSearchBoxes();
           
            VersionInfoPanelList.PanelFromData = new PanelFromVersionInfo(m_FTObjects);
            ChangeFPanelList.AddData(m_FTObjects.AllFiles);

            if (CallPerformS)
            {
                CallPerformS = false;
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }

            if (newcur == true && usem == true)
            {
                VersionInfoPanelList.CallInvoke();
                newcur = false;
                usem = true;
                funcver = false;

            }
            else
            {
                funcver = true;
            }

            
        }
        #region GuiMessage Functions

        public void SetDeleteReturnVal(FolderTrack.Delete.DeleteRules.DeleteReturn del)
        {
            if (del == FolderTrack.Delete.DeleteRules.DeleteReturn.ALL)
            {
                includeRemove = true;

            }
            else if (del == FolderTrack.Delete.DeleteRules.DeleteReturn.NOT_REMOVED)
            {
                includeRemove = false;

            }

            //when all data is received call perform search

            
            CallPerformS = true;

            HoldAllDisplay = true;
        }

        #endregion

        private void FillSearchBoxes()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidNoArgDelegate(FillSearchBoxes));
                return;
            }
            int LatestYear = AddYears();
            AddMonths(LatestYear);
            AddDays();
            PopulateCalendar();
            PopulateTime();
            PopulateUserVersions();
        }

        void MonthCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(InvokeHandleMonthChange);
            thread.Start();
        }

        private void InvokeHandleMonthChange()
        {
            this.MonthCheckedListBox.Invoke(new HandleMonthChangeDelegate(HandleMonthChange));
        }

        private void HandleMonthChange()
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            bool Changed = false;
            List<int> RemoveMonths = new List<int>();
            foreach (int mon in m_Criteria.Months)
            {
                if (this.MonthCheckedListBox.CheckedItems.Contains(info.GetMonthName(mon)) == false)
                {
                    Changed = true;
                    RemoveMonths.Add(mon);
                }
            }
            foreach (int monR in RemoveMonths)
            {
                m_Criteria.Months.Remove(monR);
            }

            string [] ChkdMonthArr = new string[MonthCheckedListBox.CheckedItems.Count];
            MonthCheckedListBox.CheckedItems.CopyTo(ChkdMonthArr,0);
            foreach (string mon in ChkdMonthArr)
            {
                if (m_Criteria.Months.Contains(MonthNumFromName[mon]) == false)
                {
                    Changed = true;
                    m_Criteria.Months.Add(MonthNumFromName[mon]);
                }
            }
            if (Changed)
            {
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private void DayOfMonthCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(InvokeHandleDayOfMonthChange);
            thread.Start();
        }

        private void InvokeHandleDayOfMonthChange()
        {
            this.DayOfMonthCheckedListBox.Invoke(new HandleMonthChangeDelegate(HandleDayOfMonthChange));
        }

        private void HandleDayOfMonthChange()
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            bool Changed = false;
            List<int> RemoveDayOfMonths = new List<int>();
            foreach (int DayOfMonth in m_Criteria.DaysOfMonth)
            {
                if (this.DayOfMonthCheckedListBox.CheckedItems.Contains(DayOfMonth) == false)
                {
                    Changed = true;
                    RemoveDayOfMonths.Add(DayOfMonth);
                }
            }

            lock (m_Criteria.DaysOfMonth)
            {
                foreach (int monR in RemoveDayOfMonths)
                {
                    m_Criteria.DaysOfMonth.Remove(monR);
                }
            }

            int[] ChkdMonthArr = new int[DayOfMonthCheckedListBox.CheckedItems.Count];
            DayOfMonthCheckedListBox.CheckedItems.CopyTo(ChkdMonthArr, 0);

            lock (m_Criteria.DaysOfMonth)
            {
                foreach (int mon in ChkdMonthArr)
                {
                    if (m_Criteria.DaysOfMonth.Contains(mon) == false)
                    {
                        Changed = true;
                        m_Criteria.DaysOfMonth.Add(mon);
                    }
                }
            }
            if (Changed)
            {
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private void DayOfWeekCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(InvokeHandleDayOfWeekChange);
            thread.Start();
        }

        private void InvokeHandleDayOfWeekChange()
        {
            this.DayOfWeekCheckedListBox.Invoke(new HandleMonthChangeDelegate(HandleDayOfDayOfWeekChange));
        }

        private void HandleDayOfDayOfWeekChange()
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            bool Changed = false;
            List<DayOfWeek> RemoveDayOfWeeks = new List<DayOfWeek>();
            foreach (DayOfWeek  day in m_Criteria.DaysOfWeek)
            {
                if (this.DayOfWeekCheckedListBox.CheckedItems.Contains(day) == false)
                {
                    Changed = true;
                    RemoveDayOfWeeks.Add(day);
                }
            }
            foreach (DayOfWeek day in RemoveDayOfWeeks)
            {
                m_Criteria.DaysOfWeek.Remove(day);
            }

            DayOfWeek[] ChkdMonthArr = new DayOfWeek[DayOfWeekCheckedListBox.CheckedItems.Count];
            DayOfWeekCheckedListBox.CheckedItems.CopyTo(ChkdMonthArr, 0);
            foreach (DayOfWeek mon in ChkdMonthArr)
            {
                if (m_Criteria.DaysOfWeek.Contains(mon) == false)
                {
                    Changed = true;
                    m_Criteria.DaysOfWeek.Add(mon);
                }
            }
            if (Changed)
            {
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private void YearCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(InvokeHandleYearChange);
            thread.Start();
        }

        private void InvokeHandleYearChange()
        {
            this.YearCheckedListBox.Invoke(new HandleMonthChangeDelegate(HandleYearChange));
        }

        private void HandleYearChange()
        {
            bool Changed = false;
            List<int> RemoveYears = new List<int>();
            foreach (int mon in m_Criteria.Years)
            {
                if (this.YearCheckedListBox.CheckedItems.Contains(mon) == false)
                {
                    Changed = true;
                    RemoveYears.Add(mon);
                }
            }
            foreach (int monR in RemoveYears)
            {
                m_Criteria.Years.Remove(monR);
            }

            int[] ChkdMonthArr = new int[YearCheckedListBox.CheckedItems.Count];
            YearCheckedListBox.CheckedItems.CopyTo(ChkdMonthArr, 0);
            foreach (int mon in ChkdMonthArr)
            {
                if (m_Criteria.Years.Contains(mon) == false)
                {
                    Changed = true;
                    m_Criteria.Years.Add(mon);
                }
            }
            if (Changed)
            {
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private void HourCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(InvokeHandleHourChange);
            thread.Start();
        }

        private void AddVersionNameButton_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(InvokeAddVersionName);
            thread.Start();
            new Thread(PopulateVersionName).Start();
        }

        private void InvokeAddVersionName()
        {
            this.VersionNameListBox.Invoke(new HandleMonthChangeDelegate(HandleVersionAdd));

        }

        private void PopulateVersionName()
        {
            m_Criteria.VersionName.Add(this.VersionNameTextBox.Text);
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start();
        }
        private void HandleVersionAdd()
        {
            this.VersionNameListBox.Items.Add(this.VersionNameTextBox.Text);

        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(InvokeRemoveVersionName);
            thread.Start();
            Thread pre = new Thread(new ParameterizedThreadStart(RPopulateVersionName));
            pre.Start(this.VersionNameListBox.SelectedItem);
        }

        private void InvokeRemoveVersionName()
        {
            this.VersionNameListBox.Invoke(new HandleMonthChangeDelegate(HandleVersionRemove));

        }

        private void HandleVersionRemove()
        {
            this.VersionNameListBox.Items.Remove(this.VersionNameListBox.SelectedItem);

        }

        private void RPopulateVersionName(object item)
        {
            m_Criteria.VersionName.Remove((string) item);
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start();
        }

        private void InvokeHandleHourChange()
        {
            this.HourCheckedListBox.Invoke(new HandleMonthChangeDelegate(HandleHourChange));
        }

        private void HandleHourChange()
        {
            bool Changed = false;
            List<int> RemoveHour = new List<int>();
            foreach (int mon in m_Criteria.Hours)
            {
                if (this.HourCheckedListBox.CheckedItems.Contains(mon) == false)
                {
                    Changed = true;
                    RemoveHour.Add(mon);
                }
            }
            foreach (int monR in RemoveHour)
            {
                m_Criteria.Hours.Remove(monR);
            }

            string[] ChkdMonthArr = new string[HourCheckedListBox.CheckedItems.Count];
            HourCheckedListBox.CheckedItems.CopyTo(ChkdMonthArr, 0);
            foreach (string mon in ChkdMonthArr)
            {
                if (m_Criteria.Hours.Contains(HourNFromName[mon]) == false)
                {
                    Changed = true;
                    m_Criteria.Hours.Add(HourNFromName[mon]);
                }
            }
            if (Changed)
            {
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private void MinutesCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(InvokeHandleMinuteChange);
            thread.Start();
        }

        private void InvokeHandleMinuteChange()
        {
            this.MinutesCheckedListBox.Invoke(new HandleMonthChangeDelegate(HandleMinuteChange));
        }

        private void HandleMinuteChange()
        {
            bool Changed = false;
            List<int> RemoveMinutes = new List<int>();
            foreach (int mon in m_Criteria.Minutes)
            {
                if (this.MinutesCheckedListBox.CheckedItems.Contains(mon) == false)
                {
                    Changed = true;
                    RemoveMinutes.Add(mon);
                }
            }
            foreach (int monR in RemoveMinutes)
            {
                m_Criteria.Minutes.Remove(monR);
            }

            int[] ChkdMonthArr = new int[MinutesCheckedListBox.CheckedItems.Count];
            MinutesCheckedListBox.CheckedItems.CopyTo(ChkdMonthArr, 0);
            foreach (int mon in ChkdMonthArr)
            {
                if (m_Criteria.Minutes.Contains(mon) == false)
                {
                    Changed = true;
                    m_Criteria.Minutes.Add(mon);
                }
            }
            if (Changed)
            {
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private int AddYears()
        {
            
            int[] years = m_FTObjects.GetYears;
            int LatestYear = 0;
            this.YearCheckedListBox.Items.Clear();
            foreach (int year in years)
            {
                this.YearCheckedListBox.Items.Add(year);
                LatestYear = (LatestYear < year) ? year : LatestYear;
            }
            return LatestYear;
        }

        private void AddMonths(int year)
        {
            if (year == 0)
            {
                return;
            }

            if (this.MonthCheckedListBox.Items.Count > 0)
            {
                return;
            }
            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            int AmountOfMonths = cal.GetMonthsInYear(year);
            
            for (int i = 1; i <= AmountOfMonths; i++)
            {
                this.MonthCheckedListBox.Items.Add(info.GetMonthName(i), false);
                MonthNumFromName[info.GetMonthName(i)] = i;
            }
        }

        private void AddDays()
        {
            if (this.DayOfMonthCheckedListBox.Items.Count > 0)
            {
                return;
            }
            DayOfWeek[] week = new DayOfWeek[Enum.GetValues(typeof(DayOfWeek)).Length];
            Enum.GetValues(typeof(DayOfWeek)).CopyTo(week, 0);

            
            for (int i = 1; i <= week.Length; i++)
            {
                //This is for Sunday 
                if (i == week.Length)
                {
                    this.DayOfWeekCheckedListBox.Items.Add(week[0], false);
                }
                else
                {
                    this.DayOfWeekCheckedListBox.Items.Add(week[i], false);
                }
            }
        }

        private void PopulateTime()
        {
             DateTimeFormatInfo datetimfor = new DateTimeFormatInfo();
            
            string hname;
            if (this.HourCheckedListBox.Items.Count == 0)
            {
                for (int ampm = 0; ampm <= 1; ampm++)
                {
                    for (int i = 0; i <= 11; i++)
                    {
                        //If AM
                        if (ampm == 0)
                        {
                            if (i == 0)
                            {
                                hname = 12 + datetimfor.AMDesignator;
                            }
                            else
                            {
                                hname = i + datetimfor.AMDesignator;
                            }
                            this.HourCheckedListBox.Items.Add(hname, false);
                            HourNFromName[hname] = i;
                        }
                        else //If PM
                        {
                            if (i == 0)
                            {
                                hname = 12 + datetimfor.PMDesignator;
                            }
                            else
                            {
                                hname = i + datetimfor.PMDesignator;
                            }
                            this.HourCheckedListBox.Items.Add(hname, false);
                            HourNFromName[hname] = i + 12;
                        }
                    }
                }
            }
            if (this.MinutesCheckedListBox.Items.Count == 0)
            {
                for (int min = 0; min < 60; min++)
                {
                    this.MinutesCheckedListBox.Items.Add(min);
                }
            }
        }

        private void PopulateCalendar()
        {
            if (this.DayOfMonthCheckedListBox.Items.Count == 0)
            {
                for (int date = 1; date <= 31; date++)
                {
                    this.DayOfMonthCheckedListBox.Items.Add(date, false);
                }
            }
        }

        private void PopulateUserVersions()
        {
            
            List<string> AllUserVersions = m_FTObjects.AllUserVersions;
            if (AllUserVersions != null)
            {
                foreach (string Userver in m_FTObjects.AllUserVersions)
                {
                    this.UserVersionCheckedListBox.Items.Add(Userver);
                }
            }
        }

        private void UserVersionCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            Thread thread = new Thread(InvokeHandleUserVersionChange);
            thread.Start();
        }

        private void InvokeHandleUserVersionChange()
        {
            this.UserVersionCheckedListBox.Invoke(new HandleMonthChangeDelegate(HandleUserVersionChange));
        }

        private void HandleUserVersionChange()
        {
            bool Changed = false;
            List<string> RemoveUserVersion = new List<string>();
            foreach (string mon in m_Criteria.UserVersions)
            {
                if (this.UserVersionCheckedListBox.CheckedItems.Contains(mon) == false)
                {
                    Changed = true;
                    RemoveUserVersion.Add(mon);
                }
            }
            foreach (string monR in RemoveUserVersion)
            {
                m_Criteria.UserVersions.Remove(monR);
            }

            string[] ChkdMonthArr = new string[UserVersionCheckedListBox.CheckedItems.Count];
            UserVersionCheckedListBox.CheckedItems.CopyTo(ChkdMonthArr, 0);
            foreach (string mon in ChkdMonthArr)
            {
                if (m_Criteria.UserVersions.Contains(mon) == false)
                {
                    Changed = true;
                    m_Criteria.UserVersions.Add(mon);
                }
            }
            if (Changed)
            {
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private void HasUserVersionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(HandleHasUserChange);
            thread.Start();
        }

        private void HandleHasUserChange()
        {
            if (m_Criteria.IncludeAllUserVersions != this.HasUserVersionCheckBox.Checked)
            {
                m_Criteria.IncludeAllUserVersions = this.HasUserVersionCheckBox.Checked;
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private void NoUserVersionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(HandleNoUserVersionChange);
            thread.Start();
        }

        private void HandleNoUserVersionChange()
        {
            if (m_Criteria.IncludeNoUserVersions != this.NoUserVersionCheckBox.Checked)
            {
                m_Criteria.IncludeNoUserVersions = this.NoUserVersionCheckBox.Checked;
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private void ChangeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleChangeChec);
            searchThread.Start();
        }

        private void AddCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleChangeChec);
            searchThread.Start();
        }

        private void DeleteCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleChangeChec);
            searchThread.Start();
        }

        private void RenameCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleChangeChec);
            searchThread.Start();
        }

        private void HandleChangeChec()
        {
            bool changed = false;

            if (m_Criteria.ChangeTypes.Contains(ChangeType.Change) != this.ChangeCheckBox.Checked)
            {
                changed = true;
                if (this.ChangeCheckBox.Checked == false)
                {
                    m_Criteria.ChangeTypes.Remove(ChangeType.Change);
                }
                else
                {
                    m_Criteria.ChangeTypes.Add(ChangeType.Change);
                }
            }

            if (m_Criteria.ChangeTypes.Contains(ChangeType.Delete) != this.DeleteCheckBox.Checked)
            {
                changed = true;
                if (this.DeleteCheckBox.Checked == false)
                {
                    m_Criteria.ChangeTypes.Remove(ChangeType.Delete);
                }
                else
                {
                    m_Criteria.ChangeTypes.Add(ChangeType.Delete);
                }
            }

            if (m_Criteria.ChangeTypes.Contains(ChangeType.Rename) != this.RenameCheckBox.Checked)
            {
                changed = true;
                if (this.RenameCheckBox.Checked == false)
                {
                    m_Criteria.ChangeTypes.Remove(ChangeType.Rename);
                }
                else
                {
                    m_Criteria.ChangeTypes.Add(ChangeType.Rename);
                }
            }

            if (m_Criteria.ChangeTypes.Contains(ChangeType.Add) != this.AddCheckBox.Checked)
            {
                changed = true;
                if (this.AddCheckBox.Checked == false)
                {
                    m_Criteria.ChangeTypes.Remove(ChangeType.Add);
                }
                else
                {
                    m_Criteria.ChangeTypes.Add(ChangeType.Add);
                }
            }
            if (changed == true)
            {

                m_Criteria.includeRemove = includeRemove;
                List<VersionInfo> ver = m_FTObjects.VersionInfoFromCriteria(m_Criteria, this);
                VersionInfoPanelList.ClearAndAddData(ver);
                if (ver.Count == 0)
                {
                    this.l.Invoke(new VoidStringDelegate(SetlText), new object[] { "No results" });
                }
            }
        }
        class Chan
        {
            public string ChangeFile;
            public bool status;
        }
        public void HandleChangeFileChan(string ChangeFile, bool status)
        {
            Chan ch = new Chan();
            ch.ChangeFile = ChangeFile;
            ch.status = status;
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(new ParameterizedThreadStart(HandleChangeFileChan));
            searchThread.Start(ch);
        }

        private void HandleChangeFileChan(object handleCh)
        {
            Chan ch = (Chan)handleCh;
            string ChangeFile = ch.ChangeFile;
            bool status = ch.status;
            
            if (m_Criteria.ChangeFile.Contains(ChangeFile) != status)
            {
                if (status == false)
                {
                    m_Criteria.RemoveChangeFile(ChangeFile);
                }
                else
                {
                    m_Criteria.AddChangeFile(ChangeFile, true);
                }


                m_Criteria.includeRemove = includeRemove;
                List<VersionInfo> ver = m_FTObjects.VersionInfoFromCriteria(m_Criteria, this);
                VersionInfoPanelList.ClearAndAddData(ver);
                if (ver.Count == 0)
                {
                    this.l.Invoke(new VoidStringDelegate(SetlText), new object[] { "No results" });
                }
            }
        }

        private void DoesNotDiscripcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(HandleNoDiscrip);
            thread.Start();
        }

        private void HasDiscriptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Thread thread = new Thread(HandleHasDiscrip);
            thread.Start();
        }

        private void DiscripcheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string testj = (string) this.DiscriCheckedListBox.SelectedItem;
            if (this.DiscriCheckedListBox.CheckedItems.Contains(testj))
            {
                m_Criteria.AddNotes(testj, true);
            }
            else
            {
                m_Criteria.RemoveNotes(testj);
            }
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start();
        }

        private void AddDiscriButton_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(InvokeAddDiscri);
            thread.Start();
            Thread thread2 = new Thread(InvokePopDisc);
            thread2.Start();
        }

        private void InvokeAddDiscri()
        {
            this.DiscriCheckedListBox.Invoke(new HandleMonthChangeDelegate(HandleDiscriAdd));

        }

        private void InvokePopDisc()
        {
            this.DiscriCheckedListBox.Invoke(new HandleMonthChangeDelegate(PopulateDiscri));
        }

        private void PopulateDiscri()
        {
            m_Criteria.Notes.Add(this.DiscriptionTextBox.Text);
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start();
        }
        private void HandleDiscriAdd()
        {
            this.DiscriCheckedListBox.Items.Add(this.DiscriptionTextBox.Text,true);

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(InvokeRemoveDiscri);
            thread.Start();
            Thread pre = new Thread(new ParameterizedThreadStart(RPopulateDiscri));
            pre.Start(this.DiscriCheckedListBox.SelectedItem);
        }

        private void InvokeRemoveDiscri()
        {
            this.DiscriCheckedListBox.Invoke(new HandleMonthChangeDelegate(HandleDiscriRemove));

        }

        private void HandleDiscriRemove()
        {
            this.DiscriCheckedListBox.Items.Remove(this.DiscriCheckedListBox.SelectedItem);

        }

        private void RPopulateDiscri(object item)
        {
            m_Criteria.Notes.Remove((string)item);
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start();
        }

        private void HandleNoDiscrip()
        {
            if (m_Criteria.IncludeNoDiscription != this.DoesNotDiscripcheckBox.Checked)
            {
                m_Criteria.IncludeNoDiscription = this.DoesNotDiscripcheckBox.Checked;
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private void HandleHasDiscrip()
        {
            if (m_Criteria.IncludeDiscription != this.HasDiscriptCheckBox.Checked)
            {
                m_Criteria.IncludeDiscription = this.HasDiscriptCheckBox.Checked;
                if (searchThread != null)
                {
                    searchThread.Abort();
                }
                searchThread = new Thread(HandleUpdate);
                searchThread.Start();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            m_FTObjects.Search(textBox5.Text);
        }








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

        private void HandleUpdate()
        {
            m_Criteria.includeRemove = includeRemove;
            List<VersionInfo> ver = m_FTObjects.VersionInfoFromCriteria(m_Criteria, this);
            VersionInfoPanelList.ClearAndAddDataH(ver);
            if(ver.Count == 0)
            {
                this.l.Invoke(new VoidStringDelegate(SetlText),new object[] {"No results"});
                ShowWait(true);
            }
            if (lastCurrentVersion != null)
            {
                NewCurrentVersion(null, lastCurrentVersion);
            }
            else
            {
                VersionInfoPanelList.CallInvoke();
            }
        }

        #region FolderTrackCallBack Members

        public void NewCurrentVersion(string MonitorGroup, VersionInfo vers)
        {
            lock (newVerOb)
            {
                VersionInfo versI = m_FTObjects.VersionInfoFromVersionName(vers.versionName);

                if (lastCurrentVersion != null)
                {
                    this.VersionInfoPanelList.AddFunctionCallToDataH(lastCurrentVersion, false);
                }
                if (versI != null)
                {
                    this.VersionInfoPanelList.AddFunctionCallToDataH(versI, true);
                    lastCurrentVersion = versI;
                }

                if (HoldAllDisplay == false)
                {
                    this.VersionInfoPanelList.CallInvoke();
                }
                else if (funcver == true && usem == true)
                {
                    usem = false;
                    HoldAllDisplay = false;
                    funcver = false;
                    newcur = false;
                    this.VersionInfoPanelList.CallInvoke();
                }
                else
                {
                    newcur = true;
                }
            }
        }

        public void SendData(List<ChangeInstruction> data, int idnum, bool done)
        {

        }

        public void SendDataV(List<VersionInfo> data, string name, int idnum, bool done)
        {
            if (done == true)
            {
                FunctionsThatNeedCompleteVersionInfo();
            }
        }

        public void TaskUpdate(TaskGroup[] task)
        {

        }

        public void NewVersion(string MonitorGroup, List<VersionInfo> vers)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start();
        }

        public void DeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start();
        }


        public void DeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start();
        }


        public void UndeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start();
        }


        public void UndeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
        }

        private void this_Resize(object sender, EventArgs e)
        {
            this.SplitterDistance = this.VersionGroupBox.Width + this.SplitterWidth + 20;
        }

        public void SendNewUserVersion(string MonitorGroup, List<string> UserVersion)
        {
            AddUserVersionTo(UserVersion);
        }

        private void AddUserVersionTo(List<string> UserVersion)
        {
            if (this.UserVersionCheckedListBox.InvokeRequired)
            {
                this.UserVersionCheckedListBox.Invoke(new VoidListStringDelegate(AddUserVersionTo), new object[] { UserVersion });
                return;
            }
            string[] UserArr = new string[UserVersion.Count];
            UserVersion.CopyTo(UserArr);
            this.UserVersionCheckedListBox.Items.AddRange(UserArr);
        }

        public void DontMonitor(string MonitorGroup)
        {
            if (HoldAllDisplay)
            {
                VersionInfoPanelList.CallAllH(VersionMini.DONT_USE);
            }
            else
            {
                VersionInfoPanelList.CallAll(VersionMini.DONT_USE);
            }
            if (funcver == true && newcur == true)
            {
                funcver = false;
                newcur = false;
                HoldAllDisplay = false;
                usem = false;
                VersionInfoPanelList.CallInvoke();
            }
            else
            {
                usem = true;
            }
        }

        public void RestartMonitor(string MonitorGroup)
        {
            VersionInfoPanelList.CallAll(VersionMini.USE);
        }

        public void UseMonitor()
        {
            if (HoldAllDisplay)
            {
                VersionInfoPanelList.CallAllH(VersionMini.USE);
            }
            else
            {
                VersionInfoPanelList.CallAll(VersionMini.USE);
            }
            if (newcur == true && funcver == true)
            {
                newcur = false;
                funcver = false;
                HoldAllDisplay = false;
                usem = false;
                VersionInfoPanelList.CallInvoke();
            }
            else
            {
                usem = true;
            }
            
        }

        public void PleaseRegister()
        {

        }

        public void NewVersionInformation(string MonitorGroup, List<VersionInfo> vers)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start(); 
        }

        public void NewDiscription(string MonitorGroup, VersionInfo version)
        {
            if (searchThread != null)
            {
                searchThread.Abort();
            }
            searchThread = new Thread(HandleUpdate);
            searchThread.Start();
        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ControlTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.AddDiscriButton = new System.Windows.Forms.Button();
            this.DiscriptionTextBox = new System.Windows.Forms.TextBox();
            this.DiscriCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.DoesNotDiscripcheckBox = new System.Windows.Forms.CheckBox();
            this.HasDiscriptCheckBox = new System.Windows.Forms.CheckBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.CalendarGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.CalendarTextBox = new System.Windows.Forms.TextBox();
            this.MonthLabel = new System.Windows.Forms.Label();
            this.DayOfMonthLabel = new System.Windows.Forms.Label();
            this.DayOfWeekLabel = new System.Windows.Forms.Label();
            this.MonthCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.DayOfMonthCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.DayOfWeekCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.YearLabel = new System.Windows.Forms.Label();
            this.HourLabel = new System.Windows.Forms.Label();
            this.Minuteslabel = new System.Windows.Forms.Label();
            this.YearCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.MinutesCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.HourCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.UserVersionCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.HasUserVersionCheckBox = new System.Windows.Forms.CheckBox();
            this.NoUserVersionCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.ChangeCheckBox = new System.Windows.Forms.CheckBox();
            this.AddCheckBox = new System.Windows.Forms.CheckBox();
            this.DeleteCheckBox = new System.Windows.Forms.CheckBox();
            this.RenameCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.VersionGroupBox = new System.Windows.Forms.GroupBox();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.VersionNameListBox = new System.Windows.Forms.ListBox();
            this.AddVersionNameButton = new System.Windows.Forms.Button();
            this.VersionNameTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.ControlTableLayoutPanel.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.CalendarGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.VersionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.Panel1.AutoScroll = true;
            this.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.Panel1.Controls.Add(this.ControlTableLayoutPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.Panel2.Controls.Add(this.panel1);
            this.Size = new System.Drawing.Size(845, 773);
            this.SplitterDistance = 488;
            this.SplitterWidth = 7;
            this.TabIndex = 0;
            this.Resize += new System.EventHandler(this_Resize);
            // 
            // ControlTableLayoutPanel
            // 
            this.ControlTableLayoutPanel.AutoSize = true;
            this.ControlTableLayoutPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ControlTableLayoutPanel.ColumnCount = 1;
            this.ControlTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ControlTableLayoutPanel.Controls.Add(this.groupBox4, 0, 6);
            this.ControlTableLayoutPanel.Controls.Add(this.CalendarGroupBox, 0, 0);
            this.ControlTableLayoutPanel.Controls.Add(this.groupBox1, 0, 2);
            this.ControlTableLayoutPanel.Controls.Add(this.groupBox2, 0, 3);
            this.ControlTableLayoutPanel.Controls.Add(this.groupBox3, 0, 4);
            this.ControlTableLayoutPanel.Controls.Add(this.VersionGroupBox, 0, 7);
            this.ControlTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ControlTableLayoutPanel.Name = "ControlTableLayoutPanel";
            this.ControlTableLayoutPanel.RowCount = 9;
            this.ControlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ControlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ControlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ControlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ControlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ControlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ControlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ControlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ControlTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ControlTableLayoutPanel.Size = new System.Drawing.Size(410, 1146);
            this.ControlTableLayoutPanel.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.tableLayoutPanel5);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(3, 613);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(404, 169);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Description ";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.AddDiscriButton, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.DiscriptionTextBox, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.DiscriCheckedListBox, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.DoesNotDiscripcheckBox, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.HasDiscriptCheckBox, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.DeleteButton, 1, 3);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 5;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(398, 150);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // AddDiscriButton
            // 
            this.AddDiscriButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.AddDiscriButton.BackColor = System.Drawing.Color.BurlyWood;
            this.AddDiscriButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddDiscriButton.Location = new System.Drawing.Point(305, 26);
            this.AddDiscriButton.Name = "AddDiscriButton";
            this.AddDiscriButton.Size = new System.Drawing.Size(75, 23);
            this.AddDiscriButton.TabIndex = 27;
            this.AddDiscriButton.Text = "Add";
            this.AddDiscriButton.UseVisualStyleBackColor = false;
            this.AddDiscriButton.Click += new System.EventHandler(this.AddDiscriButton_Click);
            // 
            // DiscriptionTextBox
            // 
            this.DiscriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiscriptionTextBox.Location = new System.Drawing.Point(3, 26);
            this.DiscriptionTextBox.Name = "DiscriptionTextBox";
            this.DiscriptionTextBox.Size = new System.Drawing.Size(282, 20);
            this.DiscriptionTextBox.TabIndex = 25;
            // 
            // DiscriCheckedListBox
            // 
            this.DiscriCheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.DiscriCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DiscriCheckedListBox.CheckOnClick = true;
            this.DiscriCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiscriCheckedListBox.FormattingEnabled = true;
            this.DiscriCheckedListBox.Location = new System.Drawing.Point(3, 55);
            this.DiscriCheckedListBox.Name = "DiscriCheckedListBox";
            this.DiscriCheckedListBox.Size = new System.Drawing.Size(282, 92);
            this.DiscriCheckedListBox.TabIndex = 28;
            this.DiscriCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.DiscripcheckedListBox_SelectedIndexChanged);
            // 
            // DoesNotDiscripcheckBox
            // 
            this.DoesNotDiscripcheckBox.AutoSize = true;
            this.DoesNotDiscripcheckBox.Location = new System.Drawing.Point(3, 3);
            this.DoesNotDiscripcheckBox.Name = "DoesNotDiscripcheckBox";
            this.DoesNotDiscripcheckBox.Size = new System.Drawing.Size(159, 17);
            this.DoesNotDiscripcheckBox.TabIndex = 23;
            this.DoesNotDiscripcheckBox.Text = "Does Not Have Description ";
            this.DoesNotDiscripcheckBox.UseVisualStyleBackColor = true;
            this.DoesNotDiscripcheckBox.CheckedChanged += new System.EventHandler(this.DoesNotDiscripcheckBox_CheckedChanged);
            // 
            // HasDiscriptCheckBox
            // 
            this.HasDiscriptCheckBox.AutoSize = true;
            this.HasDiscriptCheckBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.HasDiscriptCheckBox.Location = new System.Drawing.Point(291, 3);
            this.HasDiscriptCheckBox.Name = "HasDiscriptCheckBox";
            this.HasDiscriptCheckBox.Size = new System.Drawing.Size(104, 17);
            this.HasDiscriptCheckBox.TabIndex = 24;
            this.HasDiscriptCheckBox.Text = "Has Description ";
            this.HasDiscriptCheckBox.UseVisualStyleBackColor = true;
            this.HasDiscriptCheckBox.CheckedChanged += new System.EventHandler(this.HasDiscriptCheckBox_CheckedChanged);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DeleteButton.BackColor = System.Drawing.Color.BurlyWood;
            this.DeleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.DeleteButton.Location = new System.Drawing.Point(305, 124);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 29;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // CalendarGroupBox
            // 
            this.CalendarGroupBox.AutoSize = true;
            this.CalendarGroupBox.BackColor = System.Drawing.SystemColors.Control;
            this.CalendarGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.CalendarGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalendarGroupBox.ForeColor = System.Drawing.Color.Black;
            this.CalendarGroupBox.Location = new System.Drawing.Point(3, 3);
            this.CalendarGroupBox.Name = "CalendarGroupBox";
            this.CalendarGroupBox.Size = new System.Drawing.Size(404, 190);
            this.CalendarGroupBox.TabIndex = 1;
            this.CalendarGroupBox.TabStop = false;
            this.CalendarGroupBox.Tag = "";
            this.CalendarGroupBox.Text = "Calendar";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CalendarTextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.MonthLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.DayOfMonthLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.DayOfWeekLabel, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.MonthCheckedListBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.DayOfMonthCheckedListBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.DayOfWeekCheckedListBox, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.YearLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.HourLabel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.Minuteslabel, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.YearCheckedListBox, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.MinutesCheckedListBox, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.HourCheckedListBox, 1, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(398, 171);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.label1, 3);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Calendar Text";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CalendarTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.CalendarTextBox, 3);
            this.CalendarTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalendarTextBox.Location = new System.Drawing.Point(3, 16);
            this.CalendarTextBox.Name = "CalendarTextBox";
            this.CalendarTextBox.Size = new System.Drawing.Size(392, 20);
            this.CalendarTextBox.TabIndex = 2;
            // 
            // MonthLabel
            // 
            this.MonthLabel.AutoSize = true;
            this.MonthLabel.Location = new System.Drawing.Point(3, 39);
            this.MonthLabel.Name = "MonthLabel";
            this.MonthLabel.Size = new System.Drawing.Size(37, 13);
            this.MonthLabel.TabIndex = 2;
            this.MonthLabel.Text = "Month";
            // 
            // DayOfMonthLabel
            // 
            this.DayOfMonthLabel.AutoSize = true;
            this.DayOfMonthLabel.Location = new System.Drawing.Point(129, 39);
            this.DayOfMonthLabel.Name = "DayOfMonthLabel";
            this.DayOfMonthLabel.Size = new System.Drawing.Size(73, 13);
            this.DayOfMonthLabel.TabIndex = 3;
            this.DayOfMonthLabel.Text = "Day Of Month";
            // 
            // DayOfWeekLabel
            // 
            this.DayOfWeekLabel.AutoSize = true;
            this.DayOfWeekLabel.Location = new System.Drawing.Point(255, 39);
            this.DayOfWeekLabel.Name = "DayOfWeekLabel";
            this.DayOfWeekLabel.Size = new System.Drawing.Size(72, 13);
            this.DayOfWeekLabel.TabIndex = 4;
            this.DayOfWeekLabel.Text = "Day Of Week";
            // 
            // MonthCheckedListBox
            // 
            this.MonthCheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.MonthCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MonthCheckedListBox.CheckOnClick = true;
            this.MonthCheckedListBox.FormattingEnabled = true;
            this.MonthCheckedListBox.Location = new System.Drawing.Point(3, 55);
            this.MonthCheckedListBox.Name = "MonthCheckedListBox";
            this.MonthCheckedListBox.Size = new System.Drawing.Size(120, 47);
            this.MonthCheckedListBox.TabIndex = 3;
            this.MonthCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.MonthCheckedListBox_SelectedIndexChanged);
            // 
            // DayOfMonthCheckedListBox
            // 
            this.DayOfMonthCheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.DayOfMonthCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DayOfMonthCheckedListBox.CheckOnClick = true;
            this.DayOfMonthCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DayOfMonthCheckedListBox.FormattingEnabled = true;
            this.DayOfMonthCheckedListBox.Location = new System.Drawing.Point(129, 55);
            this.DayOfMonthCheckedListBox.Name = "DayOfMonthCheckedListBox";
            this.DayOfMonthCheckedListBox.Size = new System.Drawing.Size(120, 47);
            this.DayOfMonthCheckedListBox.TabIndex = 4;
            this.DayOfMonthCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.DayOfMonthCheckedListBox_SelectedIndexChanged);
            // 
            // DayOfWeekCheckedListBox
            // 
            this.DayOfWeekCheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.DayOfWeekCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DayOfWeekCheckedListBox.CheckOnClick = true;
            this.DayOfWeekCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DayOfWeekCheckedListBox.FormattingEnabled = true;
            this.DayOfWeekCheckedListBox.Location = new System.Drawing.Point(255, 55);
            this.DayOfWeekCheckedListBox.Name = "DayOfWeekCheckedListBox";
            this.DayOfWeekCheckedListBox.Size = new System.Drawing.Size(140, 47);
            this.DayOfWeekCheckedListBox.TabIndex = 5;
            this.DayOfWeekCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.DayOfWeekCheckedListBox_SelectedIndexChanged);
            // 
            // YearLabel
            // 
            this.YearLabel.AutoSize = true;
            this.YearLabel.Location = new System.Drawing.Point(3, 105);
            this.YearLabel.Name = "YearLabel";
            this.YearLabel.Size = new System.Drawing.Size(29, 13);
            this.YearLabel.TabIndex = 8;
            this.YearLabel.Text = "Year";
            // 
            // HourLabel
            // 
            this.HourLabel.AutoSize = true;
            this.HourLabel.Location = new System.Drawing.Point(129, 105);
            this.HourLabel.Name = "HourLabel";
            this.HourLabel.Size = new System.Drawing.Size(30, 13);
            this.HourLabel.TabIndex = 9;
            this.HourLabel.Text = "Hour";
            // 
            // Minuteslabel
            // 
            this.Minuteslabel.AutoSize = true;
            this.Minuteslabel.Location = new System.Drawing.Point(255, 105);
            this.Minuteslabel.Name = "Minuteslabel";
            this.Minuteslabel.Size = new System.Drawing.Size(44, 13);
            this.Minuteslabel.TabIndex = 10;
            this.Minuteslabel.Text = "Minutes";
            // 
            // YearCheckedListBox
            // 
            this.YearCheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.YearCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.YearCheckedListBox.CheckOnClick = true;
            this.YearCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YearCheckedListBox.FormattingEnabled = true;
            this.YearCheckedListBox.Location = new System.Drawing.Point(3, 121);
            this.YearCheckedListBox.Name = "YearCheckedListBox";
            this.YearCheckedListBox.Size = new System.Drawing.Size(120, 47);
            this.YearCheckedListBox.TabIndex = 7;
            this.YearCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.YearCheckedListBox_SelectedIndexChanged);
            // 
            // MinutesCheckedListBox
            // 
            this.MinutesCheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.MinutesCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MinutesCheckedListBox.CheckOnClick = true;
            this.MinutesCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinutesCheckedListBox.FormattingEnabled = true;
            this.MinutesCheckedListBox.Location = new System.Drawing.Point(255, 121);
            this.MinutesCheckedListBox.Name = "MinutesCheckedListBox";
            this.MinutesCheckedListBox.Size = new System.Drawing.Size(140, 47);
            this.MinutesCheckedListBox.TabIndex = 9;
            this.MinutesCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.MinutesCheckedListBox_SelectedIndexChanged);
            // 
            // HourCheckedListBox
            // 
            this.HourCheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.HourCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HourCheckedListBox.CheckOnClick = true;
            this.HourCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HourCheckedListBox.FormattingEnabled = true;
            this.HourCheckedListBox.Location = new System.Drawing.Point(129, 121);
            this.HourCheckedListBox.Name = "HourCheckedListBox";
            this.HourCheckedListBox.Size = new System.Drawing.Size(120, 47);
            this.HourCheckedListBox.TabIndex = 8;
            this.HourCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.HourCheckedListBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.tableLayoutPanel6);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(3, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 132);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Version";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.UserVersionCheckedListBox, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.HasUserVersionCheckBox, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.NoUserVersionCheckBox, 1, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel6.Size = new System.Drawing.Size(398, 113);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // UserVersionCheckedListBox
            // 
            this.UserVersionCheckedListBox.BackColor = System.Drawing.SystemColors.Control;
            this.UserVersionCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserVersionCheckedListBox.CheckOnClick = true;
            this.tableLayoutPanel6.SetColumnSpan(this.UserVersionCheckedListBox, 2);
            this.UserVersionCheckedListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserVersionCheckedListBox.FormattingEnabled = true;
            this.UserVersionCheckedListBox.Location = new System.Drawing.Point(3, 3);
            this.UserVersionCheckedListBox.Name = "UserVersionCheckedListBox";
            this.UserVersionCheckedListBox.Size = new System.Drawing.Size(392, 77);
            this.UserVersionCheckedListBox.TabIndex = 11;
            this.UserVersionCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.UserVersionCheckedListBox_SelectedIndexChanged);
            // 
            // HasUserVersionCheckBox
            // 
            this.HasUserVersionCheckBox.AutoSize = true;
            this.HasUserVersionCheckBox.Location = new System.Drawing.Point(3, 93);
            this.HasUserVersionCheckBox.Name = "HasUserVersionCheckBox";
            this.HasUserVersionCheckBox.Size = new System.Drawing.Size(108, 17);
            this.HasUserVersionCheckBox.TabIndex = 12;
            this.HasUserVersionCheckBox.Text = "Has User Version";
            this.HasUserVersionCheckBox.UseVisualStyleBackColor = true;
            this.HasUserVersionCheckBox.CheckedChanged += new System.EventHandler(this.HasUserVersionCheckBox_CheckedChanged);
            // 
            // NoUserVersionCheckBox
            // 
            this.NoUserVersionCheckBox.AutoSize = true;
            this.NoUserVersionCheckBox.Location = new System.Drawing.Point(202, 93);
            this.NoUserVersionCheckBox.Name = "NoUserVersionCheckBox";
            this.NoUserVersionCheckBox.Size = new System.Drawing.Size(163, 17);
            this.NoUserVersionCheckBox.TabIndex = 13;
            this.NoUserVersionCheckBox.Text = "Does Not Have User Version";
            this.NoUserVersionCheckBox.UseVisualStyleBackColor = true;
            this.NoUserVersionCheckBox.CheckedChanged += new System.EventHandler(this.NoUserVersionCheckBox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(3, 337);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 42);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Change Type";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.ChangeCheckBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.AddCheckBox, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.DeleteCheckBox, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.RenameCheckBox, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(398, 23);
            this.tableLayoutPanel3.TabIndex = 17;
            // 
            // ChangeCheckBox
            // 
            this.ChangeCheckBox.AutoSize = true;
            this.ChangeCheckBox.Location = new System.Drawing.Point(3, 3);
            this.ChangeCheckBox.Name = "ChangeCheckBox";
            this.ChangeCheckBox.Size = new System.Drawing.Size(63, 17);
            this.ChangeCheckBox.TabIndex = 15;
            this.ChangeCheckBox.Text = "Change";
            this.ChangeCheckBox.UseVisualStyleBackColor = true;
            this.ChangeCheckBox.CheckedChanged += new System.EventHandler(this.ChangeCheckBox_CheckedChanged);
            // 
            // AddCheckBox
            // 
            this.AddCheckBox.AutoSize = true;
            this.AddCheckBox.Location = new System.Drawing.Point(102, 3);
            this.AddCheckBox.Name = "AddCheckBox";
            this.AddCheckBox.Size = new System.Drawing.Size(45, 17);
            this.AddCheckBox.TabIndex = 17;
            this.AddCheckBox.Text = "Add";
            this.AddCheckBox.UseVisualStyleBackColor = true;
            this.AddCheckBox.CheckedChanged += new System.EventHandler(this.AddCheckBox_CheckedChanged);
            // 
            // DeleteCheckBox
            // 
            this.DeleteCheckBox.AutoSize = true;
            this.DeleteCheckBox.Location = new System.Drawing.Point(201, 3);
            this.DeleteCheckBox.Name = "DeleteCheckBox";
            this.DeleteCheckBox.Size = new System.Drawing.Size(57, 17);
            this.DeleteCheckBox.TabIndex = 18;
            this.DeleteCheckBox.Text = "Delete";
            this.DeleteCheckBox.UseVisualStyleBackColor = true;
            this.DeleteCheckBox.CheckedChanged += new System.EventHandler(this.DeleteCheckBox_CheckedChanged);
            // 
            // RenameCheckBox
            // 
            this.RenameCheckBox.AutoSize = true;
            this.RenameCheckBox.Location = new System.Drawing.Point(300, 3);
            this.RenameCheckBox.Name = "RenameCheckBox";
            this.RenameCheckBox.Size = new System.Drawing.Size(66, 17);
            this.RenameCheckBox.TabIndex = 19;
            this.RenameCheckBox.Text = "Rename";
            this.RenameCheckBox.UseVisualStyleBackColor = true;
            this.RenameCheckBox.CheckedChanged += new System.EventHandler(this.RenameCheckBox_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(3, 385);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(404, 222);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Change File";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(398, 203);
            this.panel2.TabIndex = 21;
            // 
            // VersionGroupBox
            // 
            this.VersionGroupBox.AutoSize = true;
            this.VersionGroupBox.Controls.Add(this.RemoveButton);
            this.VersionGroupBox.Controls.Add(this.VersionNameListBox);
            this.VersionGroupBox.Controls.Add(this.AddVersionNameButton);
            this.VersionGroupBox.Controls.Add(this.VersionNameTextBox);
            this.VersionGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.VersionGroupBox.ForeColor = System.Drawing.Color.Black;
            this.VersionGroupBox.Location = new System.Drawing.Point(3, 788);
            this.VersionGroupBox.Name = "VersionGroupBox";
            this.VersionGroupBox.Size = new System.Drawing.Size(404, 223);
            this.VersionGroupBox.TabIndex = 30;
            this.VersionGroupBox.TabStop = false;
            this.VersionGroupBox.Text = "Version";
            // 
            // RemoveButton
            // 
            this.RemoveButton.BackColor = System.Drawing.Color.BurlyWood;
            this.RemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RemoveButton.Location = new System.Drawing.Point(158, 181);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 34;
            this.RemoveButton.Text = "Delete";
            this.RemoveButton.UseVisualStyleBackColor = false;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // VersionNameListBox
            // 
            this.VersionNameListBox.FormattingEnabled = true;
            this.VersionNameListBox.Location = new System.Drawing.Point(3, 79);
            this.VersionNameListBox.Name = "VersionNameListBox";
            this.VersionNameListBox.Size = new System.Drawing.Size(385, 95);
            this.VersionNameListBox.TabIndex = 33;
            // 
            // AddVersionNameButton
            // 
            this.AddVersionNameButton.BackColor = System.Drawing.Color.BurlyWood;
            this.AddVersionNameButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddVersionNameButton.Location = new System.Drawing.Point(158, 43);
            this.AddVersionNameButton.Name = "AddVersionNameButton";
            this.AddVersionNameButton.Size = new System.Drawing.Size(75, 23);
            this.AddVersionNameButton.TabIndex = 32;
            this.AddVersionNameButton.Text = "Add";
            this.AddVersionNameButton.UseVisualStyleBackColor = false;
            this.AddVersionNameButton.Click += new System.EventHandler(this.AddVersionNameButton_Click);
            // 
            // VersionNameTextBox
            // 
            this.VersionNameTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.VersionNameTextBox.Location = new System.Drawing.Point(3, 16);
            this.VersionNameTextBox.Name = "VersionNameTextBox";
            this.VersionNameTextBox.Size = new System.Drawing.Size(398, 20);
            this.VersionNameTextBox.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 771);
            this.panel1.TabIndex = 0;
            // 
            // SearchDesignerForm
            // 
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.ControlTableLayoutPanel.ResumeLayout(false);
            this.ControlTableLayoutPanel.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.CalendarGroupBox.ResumeLayout(false);
            this.CalendarGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.VersionGroupBox.ResumeLayout(false);
            this.VersionGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel ControlTableLayoutPanel;
        private System.Windows.Forms.GroupBox CalendarGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CalendarTextBox;
        private System.Windows.Forms.Label MonthLabel;
        private System.Windows.Forms.Label DayOfMonthLabel;
        private System.Windows.Forms.Label DayOfWeekLabel;
        private System.Windows.Forms.CheckedListBox MonthCheckedListBox;
        private System.Windows.Forms.CheckedListBox DayOfMonthCheckedListBox;
        private System.Windows.Forms.CheckedListBox DayOfWeekCheckedListBox;
        private System.Windows.Forms.Label YearLabel;
        private System.Windows.Forms.Label HourLabel;
        private System.Windows.Forms.Label Minuteslabel;
        private System.Windows.Forms.CheckedListBox YearCheckedListBox;
        private System.Windows.Forms.CheckedListBox MinutesCheckedListBox;
        private System.Windows.Forms.GroupBox VersionGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox VersionNameTextBox;
        private System.Windows.Forms.Button AddVersionNameButton;
        private System.Windows.Forms.ListBox VersionNameListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox UserVersionCheckedListBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox ChangeCheckBox;
        private System.Windows.Forms.CheckBox AddCheckBox;
        private System.Windows.Forms.CheckBox DeleteCheckBox;
        private System.Windows.Forms.CheckBox RenameCheckBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.CheckedListBox HourCheckedListBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.CheckBox HasUserVersionCheckBox;
        private System.Windows.Forms.CheckBox NoUserVersionCheckBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button AddDiscriButton;
        private System.Windows.Forms.TextBox DiscriptionTextBox;
        private System.Windows.Forms.CheckedListBox DiscriCheckedListBox;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.CheckBox DoesNotDiscripcheckBox;
        private System.Windows.Forms.Button DeleteVerionButtton;
        private System.Windows.Forms.CheckBox HasDiscriptCheckBox;


        
    }
}
