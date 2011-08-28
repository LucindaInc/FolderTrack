using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Globalization;
using System.Drawing;
using System.Threading;
using FolderTrackGuiTest1.VersionPanels;

namespace FolderTrackGuiTest1.CalendarTab
{
    class CalendarPanel : SplitContainer, SetCriteris, FolderTrack.WCFContracts.FolderTrackCallBack, DataManager.Status, FTObjects.GuiMessage
    {
        object newVers = new object();
        private delegate void VoidListIntArra(int[] intLis);
        System.Threading.Timer time;
        private bool callb = false;
        public bool includeRemove = false;
        public bool CallPerformS = false;
        public bool HoldAllDisplay = false;
        public bool usemoncall = false;
        public bool newcur = false;
        public bool callFunthcom = false;

        public class CritGui 
        {
            VersionInfoSearchCriteria m_Criteria = new VersionInfoSearchCriteria();
            
            public Dictionary<int, MonthButton> m_MonthButton;
            public Dictionary<DayOfWeek, DayOfWeekButton> m_DayNames;
            public Dictionary<int, DayOfMonthButton> m_DayOfMonth;
            public Dictionary<int, YearButton> m_YearCheckBox;
            public Dictionary<int, HourButton> m_HourButton;
            public Dictionary<int, MinuteButton> m_MinuteButton;
            CalendarPanel m_Cal;
            Thread SearchThread;
            public bool upd;


            public CritGui(CalendarPanel cal)
            {
                m_MonthButton = new Dictionary<int, MonthButton>();
                m_DayNames = new Dictionary<DayOfWeek, DayOfWeekButton>();
                m_DayNames = new Dictionary<DayOfWeek, DayOfWeekButton>();
                m_DayOfMonth = new Dictionary<int, DayOfMonthButton>();
                m_YearCheckBox = new Dictionary<int, YearButton>();
                m_HourButton = new Dictionary<int, HourButton>();
                m_MinuteButton = new Dictionary<int, MinuteButton>();
                m_Cal = cal;
            }

            public void SetCriteria(VersionInfoSearchCriteria crit)
            {
                m_Criteria = crit;
                PerformSearch();

                SetButtonsToCriteria(crit);
            }

            public void SetButtonsToCriteria(VersionInfoSearchCriteria crit)
            {
                
                for (int i = 0; i < 24; i++)
                {
                    m_HourButton[i].State = StateEnum.NoPreferenceNoCall;
                    
                }
                foreach (int h in crit.Hours)
                {
                    m_HourButton[h].State = StateEnum.IncludeThisExcludeOthersNoCall;
          
                }

                foreach (int h in crit.HoursR)
                {
                    m_HourButton[h].State = StateEnum.ExcludeNoCall;
            
                }



        

                foreach (DayOfWeek d in Enum.GetValues(typeof(DayOfWeek)))
                {
                    m_DayNames[d].State = StateEnum.NoPreferenceNoCall;
                }
                
                foreach (DayOfWeek d in crit.DaysOfWeek)
                {
                    m_DayNames[d].State = StateEnum.IncludeThisExcludeOthersNoCall;
                }

                foreach (DayOfWeek d in crit.DaysOfWeekR)
                {
                    m_DayNames[d].State = StateEnum.ExcludeNoCall;
                }


                for (int m = 1; m < 13; m++ )
                {
                    m_MonthButton[m].State = StateEnum.NoPreferenceNoCall;
                }

                foreach(int m in crit.Months)
                {
                    m_MonthButton[m].State = StateEnum.IncludeThisExcludeOthersNoCall;
                }

                foreach (int m in crit.MonthsR)
                {
                    m_MonthButton[m].State = StateEnum.ExcludeNoCall;
                }


                for (int d= 1; d < 32; d++)
                {
                    m_DayOfMonth[d].State = StateEnum.NoPreferenceNoCall;
                }

                foreach (int d in crit.DaysOfMonth)
                {
                    m_DayOfMonth[d].State = StateEnum.IncludeThisExcludeOthersNoCall;
                }

                foreach (int d in crit.DaysOfMonthR)
                {
                    m_DayOfMonth[d].State = StateEnum.ExcludeNoCall;
                }

                foreach (int y in m_Cal.m_FTObjects.GetYears)
                {
                    m_YearCheckBox[y].State = StateEnum.NoPreferenceNoCall;
                }

                foreach (int y in crit.Years)
                {
                    m_YearCheckBox[y].State = StateEnum.IncludeThisExcludeOthersNoCall;
                }

                foreach (int y in crit.YearsR)
                {
                    m_YearCheckBox[y].State = StateEnum.ExcludeNoCall;
                }
                

            }

            public void SetCalText(string text)
            {
                if(m_Cal.TextTextBox.InvokeRequired)
                {
                    m_Cal.TextTextBox.Invoke(new VoidStringDelegate(SetCalText), new object[] { text });
                    return;
                }
                m_Cal.rejectText = true;
                m_Cal.TextTextBox.Text = text;
            }

            public void PerformWithText()
            {
                StringBuilder builder = new StringBuilder();
                bool textAdded = false;
                bool textBeforeTime = false;
                int count = 0;

                count = m_Criteria.DaysOfWeekR.Count;
                textAdded = false;
                foreach (DayOfWeek d in m_Criteria.DaysOfWeek)
                {
                    if (textAdded == true)
                    {
                        builder.Append(',');
                    }
                    builder.Append(d.ToString());
                    textAdded = true;
                    textBeforeTime = true;
                    count++;
                }

                if (count != 7 && count != 0)
                {
                    SetCalText("");
                    return;
                }

                if (textAdded == true)
                {
                    builder.Append(' ');
                }
                textAdded = false;



                foreach (int m in m_Criteria.Months)
                {
                    if (textAdded)
                    {
                        builder.Append(',');
                        builder.Append(' ');
                    }
                    info.GetMonthName(m);
                    builder.Append(info.GetMonthName(m));
                    textAdded = true;
                    textBeforeTime = true;
                }

                if (textAdded == true)
                {
                    builder.Append(' ');
                }

                count = m_Criteria.Months.Count;
                count = m_Criteria.MonthsR.Count + count;

                if (count != 12 && count != 0)
                {
                    SetCalText("");
                    return;
                }
                

                count = m_Criteria.DaysOfMonth.Count;
                count = m_Criteria.DaysOfMonthR.Count + count;
                foreach (int d in m_Criteria.DaysOfMonth)
                {
                    builder.Append(d);
                    if (d == 1)
                    {
                        builder.Append("st");
                    }
                    else if (d == 2)
                    {
                        builder.Append("nd");
                    }
                    else if (d == 3)
                    {
                        builder.Append("rd");
                    }
                    else
                    {
                        builder.Append("th");
                    }
                    builder.Append(',');
                    builder.Append(' ');
                    textBeforeTime = true;
                }

                if (count != 31 && count != 0)
                {
                    SetCalText("");
                    return;
                }

                textAdded = false;

                count = m_Criteria.YearsR.Count;
                count = m_Criteria.Years.Count + count;


                foreach (int y in m_Criteria.Years)
                {
                    if (textAdded == true)
                    {
                        builder.Append(' ');
                    }
                    builder.Append(y);
                    textAdded = true;
                    textBeforeTime = true;
                }

                if(count != m_Cal.m_FTObjects.GetYears.Length && count != 0)
                {
                    SetCalText("");
                    return;
                }

                if (textBeforeTime == true)
                {
                    builder.Append(' ');
                }

                count = m_Criteria.Hours.Count;
                count = m_Criteria.HoursR.Count + count;

                textAdded = false;
                foreach (int h in m_Criteria.Hours)
                {
                    if (textAdded == true)
                    {
                        builder.Append(',');
                        builder.Append(' ');
                    }
                    builder.Append(h);
                    textAdded = true;
                }

                if (count != 24 && count != 0)
                {
                    SetCalText("");
                    return;
                }

                if (textAdded == true)
                {
                    builder.Append(' ');
                }

                count = m_Criteria.Minutes.Count;
                count = m_Criteria.MinutesR.Count + count;

                foreach (int m in m_Criteria.Minutes)
                {
                    builder.Append(':');
                    if (m < 10)
                    {
                        builder.Append('0');
                    }
                    builder.Append(m);
                    builder.Append(' ');
                }

                if (count != 60 && count != 0)
                {
                    SetCalText("");
                    return;
                }

                m_Cal.rejectText = true;
                m_Cal.TextTextBox.Text = builder.ToString();
            }


            public void PerformSearch()
            {
                if (SearchThread != null)
                {
                    SearchThread.Abort();
                }

                SearchThread = new Thread(UpdateThread);

                SearchThread.Start();

            }

            

            int row;
            private void UpdateThread(object datamanager)
            {
                m_Criteria.includeRemove = m_Cal.includeRemove;
                List<VersionInfo> AddLi = m_Cal.m_FTObjects.VersionInfoFromCriteria(m_Criteria, m_Cal);
                if (AddLi != null && AddLi.Count > 0)
                {
                    if (m_Cal.HoldAllDisplay)
                    {
                        m_Cal.VersionInfoPanelList.ClearAndAddDataH(AddLi);
                    }
                    else
                    {
                        m_Cal.VersionInfoPanelList.ClearAndAddData(AddLi);
                    }
                    if (this.m_Cal.lastCurrentVersion != null)
                    {
                        this.m_Cal.NewCurrentVersion(null, this.m_Cal.lastCurrentVersion);
                    }
                }
                else
                {
                    m_Cal.VersionInfoPanelList.ClearAllData();
                }
                if (m_Cal.usemoncall == true && m_Cal.newcur == true)
                {
                    m_Cal.VersionInfoPanelList.CallInvoke();
                    m_Cal.usemoncall = false;
                    m_Cal.newcur = false;
                    upd = false;
                    m_Cal.HoldAllDisplay = false;
                }
                else
                {
                    upd = true;
                }
                
                
            }





            private void AddVersion(VersionInfo vers)
            {
                //      m_Cal.VersionTableLayoutPanel.Controls.Add(new CalendarPanel(vers), 0, row);
            }

            public void SetHourState(int hour, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
                    case StateEnum.IncludeThisExcludeOthers:
                        m_Criteria.AddHours(hour, false);
                        break;

                    case StateEnum.Exclude:
                        m_Criteria.AddHours(hour, true);
                        break;

                    case StateEnum.NoPreference:
                        m_Criteria.RemoveHours(hour);
                        break;
                }

                if (m_HourButton[hour].State != state)
                {
                    m_HourButton[hour].State = state;
                }
            }

            public List<HourButton> ExcludeOtherHours(int hour, bool SendCall)
            {
                List<HourButton> ReturnList = new List<HourButton>();
                foreach (HourButton hourcheckbox in m_HourButton.Values)
                {
                    if (hourcheckbox.Hour == hour)
                    {
                        continue;
                    }
                    if (hourcheckbox.State == StateEnum.NoPreference)
                    {
                        if (SendCall)
                        {
                            hourcheckbox.State = StateEnum.Exclude;
                        }
                        ReturnList.Add(hourcheckbox);
                    }
                }

                return ReturnList;
            }

            public void UnExcludeOtherHours(List<HourButton> hours)
            {
                foreach (HourButton hourcheckbox in hours)
                {
                    if (hourcheckbox.State == StateEnum.Exclude ||
                        hourcheckbox.State == StateEnum.NoPreferenceExcludeNext)
                    {
                        hourcheckbox.State = StateEnum.NoPreference;
                    }
                    if (hourcheckbox.State == StateEnum.Include)
                    {
                        hourcheckbox.State = StateEnum.IncludeNoPreferenceNext;
                    }
                }
            }

            public void SetMinuteState(int minute, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
                    case StateEnum.IncludeThisExcludeOthers:
                        m_Criteria.AddMinutes(minute, false);
                        break;

                    case StateEnum.Exclude:
                        m_Criteria.AddMinutes(minute, true);
                        break;

                    case StateEnum.NoPreference:
                        m_Criteria.RemoveMinutes(minute);
                        break;
                }

                if (m_MinuteButton[minute].State != state)
                {
                    m_MinuteButton[minute].State = state;
                }
            }

            public List<MinuteButton> ExcludeOtherMinutes(int minute, bool SendCall)
            {
                List<MinuteButton> ReturnList = new List<MinuteButton>();
                foreach (MinuteButton minutecheckbox in m_MinuteButton.Values)
                {
                    if (minutecheckbox.Minute == minute)
                    {
                        continue;
                    }
                    if (minutecheckbox.State == StateEnum.NoPreference)
                    {
                        if (SendCall)
                        {
                            minutecheckbox.State = StateEnum.Exclude;
                        }
                        ReturnList.Add(minutecheckbox);
                    }
                }

                return ReturnList;
            }

            public void UnExcludeOtherMinutes(List<MinuteButton> minutes)
            {
                foreach (MinuteButton minutecheckbox in minutes)
                {
                    if (minutecheckbox.State == StateEnum.Exclude ||
                        minutecheckbox.State == StateEnum.NoPreferenceExcludeNext)
                    {
                        minutecheckbox.State = StateEnum.NoPreference;
                    }
                    if (minutecheckbox.State == StateEnum.Include)
                    {
                        minutecheckbox.State = StateEnum.IncludeNoPreferenceNext;
                    }
                }
            }

            public void SetMonthState(int month, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
                    case StateEnum.IncludeThisExcludeOthers:
                        m_Criteria.AddMonth(month, false);
                        break;

                    case StateEnum.Exclude:
                        m_Criteria.AddMonth(month, true);
                        break;

                    case StateEnum.NoPreferenceExcludeNext:
                    case StateEnum.NoPreference:
                        m_Criteria.RemoveMonth(month);
                        break;
                }

                if (m_MonthButton[month].State != state)
                {
                    m_MonthButton[month].State = state;
                }
            }

            //Returns a list of months excluded by this call
            public List<MonthButton> ExcludeOtherMonths(int month, bool SendCall)
            {
                List<MonthButton> ReturnList = new List<MonthButton>();
                foreach (MonthButton monthcheckbox in m_MonthButton.Values)
                {
                    if (monthcheckbox.Month == month)
                    {
                        continue;
                    }
                    if (monthcheckbox.State == StateEnum.NoPreference)
                    {
                        if (SendCall)
                        {
                            monthcheckbox.State = StateEnum.Exclude;
                        }
                        ReturnList.Add(monthcheckbox);
                        
                    }
                }

                return ReturnList;
            }

            public void UnExcludeOtherMonths(List<MonthButton> months)
            {
                foreach (MonthButton monthcheckbox in months)
                {
                    if (monthcheckbox.State == StateEnum.Exclude ||
                        monthcheckbox.State == StateEnum.NoPreferenceExcludeNext)
                    {
                        monthcheckbox.State = StateEnum.NoPreference;
                    }
                    if (monthcheckbox.State == StateEnum.Include)
                    {
                        monthcheckbox.State = StateEnum.IncludeNoPreferenceNext;
                    }
                }
            }

            public void SetDayOfWeekState(DayOfWeek dayofweek, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
                    case StateEnum.IncludeThisExcludeOthers:
                        m_Criteria.AddDayOfWeek(dayofweek, false);
                        break;

                    case StateEnum.Exclude:
                        m_Criteria.AddDayOfWeek(dayofweek, true);
                        break;

                    case StateEnum.NoPreference:
                        m_Criteria.RemoveDayOfWeek(dayofweek);
                        break;
                }

                if (m_DayNames[dayofweek].State != state)
                {
                    m_DayNames[dayofweek].State = state;
                }
            }

            //Returns a list of months excluded by this call
            public List<DayOfWeekButton> ExcludeOtherDayOfWeek(DayOfWeek dayofweek, bool SendCall)
            {
                List<DayOfWeekButton> ReturnList = new List<DayOfWeekButton>();
                foreach (DayOfWeekButton daynames in m_DayNames.Values)
                {
                    if (daynames.DayName == dayofweek)
                    {
                        continue;
                    }

                    if (daynames.State == StateEnum.NoPreference)
                    {
                        if (SendCall)
                        {
                            daynames.State = StateEnum.Exclude;
                        }
                        ReturnList.Add(daynames);
                    }
                }

                return ReturnList;
            }

            public void UnExcludeOtherDayOfWeek(List<DayOfWeekButton> dayofweeks)
            {
                foreach (DayOfWeekButton dayofweek in dayofweeks)
                {
                    if (dayofweek.State == StateEnum.Exclude ||
                        dayofweek.State == StateEnum.NoPreferenceExcludeNext)
                    {
                        dayofweek.State = StateEnum.NoPreference;
                    }
                    if (dayofweek.State == StateEnum.Include)
                    {
                        dayofweek.State = StateEnum.IncludeNoPreferenceNext;
                    }
                }
            }

            public void SetDayOfMonthState(int dayofmonth, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
                    case StateEnum.IncludeThisExcludeOthers:
                        m_Criteria.AddDaysOfMonth(dayofmonth, false);
                        break;

                    case StateEnum.Exclude:
                        m_Criteria.AddDaysOfMonth(dayofmonth, true);
                        break;

                    case StateEnum.NoPreference:
                    case StateEnum.NoPreferenceExcludeNext:
                        m_Criteria.RemoveDaysOfMonth(dayofmonth);
                        break;
                }

                if (m_DayOfMonth[dayofmonth].State != state)
                {
                    m_DayOfMonth[dayofmonth].State = state;
                }
            }

            //Returns a list of months excluded by this call
            //SendCall actiall exclude or just return list
            public List<DayOfMonthButton> ExcludeOtherDayOfMonth(int dayofmonth, bool SendCall)
            {
                List<DayOfMonthButton> ReturnList = new List<DayOfMonthButton>();
                foreach (DayOfMonthButton dayofmonthbutton in m_DayOfMonth.Values)
                {
                    if (dayofmonthbutton.DayOfMonthNum == dayofmonth)
                    {
                        continue;
                    }

                    if (dayofmonthbutton.State == StateEnum.NoPreference)
                    {
                        if (SendCall)
                        {
                            dayofmonthbutton.State = StateEnum.Exclude;
                        }
                        ReturnList.Add(dayofmonthbutton);
                    }
                }

                return ReturnList;
            }

            public void UnExcludeOtherDayOfMonth(List<DayOfMonthButton> dayofmonths)
            {
                foreach (DayOfMonthButton dayofmonthbutton in dayofmonths)
                {
                    if (dayofmonthbutton.State == StateEnum.Exclude ||
                        dayofmonthbutton.State == StateEnum.NoPreferenceExcludeNext)
                    {
                        dayofmonthbutton.State = StateEnum.NoPreference;
                    }
                    if (dayofmonthbutton.State == StateEnum.Include)
                    {
                        dayofmonthbutton.State = StateEnum.IncludeNoPreferenceNext;
                    }
                }
            }

            //Year
            public void SetYearState(int year, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
                    case StateEnum.IncludeThisExcludeOthers:
                        m_Criteria.AddYears(year, false);
                        break;

                    case StateEnum.Exclude:
                        m_Criteria.AddYears(year, true);
                        break;

                    case StateEnum.NoPreference:
                        m_Criteria.RemoveYears(year);
                        break;
                }

                if (m_YearCheckBox[year].State != state)
                {
                    m_YearCheckBox[year].State = state;
                }
            }

            //Returns a list of months excluded by this call
            public List<YearButton> ExcludeOtherYear(int year, bool SendCall)
            {
                List<YearButton> ReturnList = new List<YearButton>();
                foreach (YearButton yearcheckbox in m_YearCheckBox.Values)
                {
                    if (yearcheckbox.Year == year)
                    {
                        continue;
                    }

                    if (yearcheckbox.State == StateEnum.NoPreference)
                    {
                        if (SendCall)
                        {
                            yearcheckbox.State = StateEnum.Exclude;
                        }
                        ReturnList.Add(yearcheckbox);
                    }
                }

                return ReturnList;
            }

            public void UnExcludeYear(List<YearButton> years)
            {
                foreach (YearButton year in years)
                {
                    if (year.State == StateEnum.Exclude)
                    {
                        year.State = StateEnum.NoPreference;
                    }
                }
            }


            
        }


        public enum StateEnum
        {
            NoPreference,
            NoPreferenceExcludeNext,
            NoPreferenceNoCall,
            Include,
            IncludeNoCall,
            IncludeThisExcludeOthers,
            IncludeThisExcludeOthersNoCall,
            IncludeNoPreferenceNext,
            Exclude,
            ExcludeNoCall
        }

        public bool rejectText = false;

        public static DateTimeFormatInfo info = new DateTimeFormatInfo();

        delegate void VoidNoArgDelegate();
        delegate void VoidStringDelegate(string text);
        private int m_Year;
        private int m_Day;
        private int m_Month;
        private int[] m_Years;

        public PanelList<VersionInfo> VersionInfoPanelList;

        private Dictionary<DayOfWeek, int> ColFromDayOfWeek;
        VersionInfo lastCurrentVersion;

        static void ButtonColor(StateEnum sta, Button but)
        {
            switch (sta)
            {
                case StateEnum.NoPreferenceExcludeNext:
                case StateEnum.NoPreference:
                case StateEnum.NoPreferenceNoCall:
                    but.BackColor = System.Drawing.Color.BurlyWood;
                    but.ForeColor = System.Drawing.Color.Black;
                    break;
                case StateEnum.IncludeThisExcludeOthers:
                case StateEnum.Include:
                case StateEnum.IncludeNoCall:
                    but.BackColor = System.Drawing.Color.DarkGreen;
                    but.ForeColor = System.Drawing.Color.White;
                    break;
                case StateEnum.ExcludeNoCall:
                case StateEnum.Exclude:
                    but.BackColor = System.Drawing.Color.Crimson;
                    but.ForeColor = System.Drawing.Color.Black;
                    break;
            }
        }


        public class HourButton : Button
        {
            static DateTimeFormatInfo info = new DateTimeFormatInfo();
            private int m_Hour;
            private int m_AmPm;
            private StateEnum m_State;
            List<HourButton> ExcludeList;
            private CritGui m_CritGui;

            public HourButton(int Hour, int ampm, CritGui critgui)
            {
                if (Hour == 12 && ampm == 0)
                {
                    m_Hour = 0;
                }
                else if (Hour == 12 && ampm == 1)
                {
                    m_Hour = 12;
                }
                else
                {
                    m_Hour = Hour + (ampm * 12);
                }
                m_AmPm = ampm;
                m_CritGui = critgui;
                this.AutoSize = false;
                this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.Size = new System.Drawing.Size(29, 23);
                this.BackColor = System.Drawing.Color.BurlyWood;
                this.UseVisualStyleBackColor = true;
                m_CritGui.m_HourButton.Add(m_Hour, this);
                this.Text = Convert.ToString(Hour);
                m_State = StateEnum.NoPreference;
            }

            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
                switch (State)
                {
                    case StateEnum.NoPreference:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.NoPreference;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.NoPreferenceExcludeNext;
                        break;
                    case StateEnum.NoPreferenceExcludeNext:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.IncludeNoPreferenceNext:
                        State = StateEnum.NoPreference;
                        break;

                }
                m_CritGui.PerformWithText();
                m_CritGui.PerformSearch();
            }

            public StateEnum State
            {
                get
                {
                    return m_State;
                }
                set
                {
                    if (value == m_State)
                    {
                        return;
                    }
                    m_State = value;
                    switch (m_State)
                    {
                        case StateEnum.NoPreference:
                            m_CritGui.SetHourState(this.Hour, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherHours(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.NoPreferenceExcludeNext:
                            m_CritGui.SetHourState(this.Hour, State);
                            break;
                        case StateEnum.NoPreferenceNoCall:
                            m_State = StateEnum.NoPreference;
                            ExcludeList = null;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetHourState(this.Hour, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherHours(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.IncludeNoCall:
                            m_State = StateEnum.Include;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeNoPreferenceNext:
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetHourState(this.Hour, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherHours(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.ExcludeNoCall:
                            m_State = StateEnum.Exclude;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            m_CritGui.SetHourState(this.Hour, State);
                            ExcludeList = m_CritGui.ExcludeOtherHours(this.Hour,true);
                            break;
                        case StateEnum.IncludeThisExcludeOthersNoCall:
                            m_State = StateEnum.IncludeThisExcludeOthers;
                            ExcludeList = m_CritGui.ExcludeOtherHours(this.Hour,false);
                            break;
                    }
                    CalendarPanel.ButtonColor(m_State, this);

                }
            }

            public int Hour
            {
                get
                {
                    return m_Hour;
                }
            }
        }

        public class MinuteButton : Button
        {
            static DateTimeFormatInfo info = new DateTimeFormatInfo();
            private int m_Minute;
            private StateEnum m_State;
            List<MinuteButton> ExcludeList;
            private CritGui m_CritGui;

            public MinuteButton(int Minute, CritGui critgui)
            {
                m_CritGui = critgui;
                m_Minute = Minute;
                this.AutoSize = false;
                this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.Size = new System.Drawing.Size(29, 23);
                this.BackColor = System.Drawing.Color.BurlyWood;
                this.UseVisualStyleBackColor = true;
                m_CritGui.m_MinuteButton.Add(m_Minute, this);
                if (Minute < 10)
                {
                    this.Text = "0" + Convert.ToString(Minute);
                }
                else
                {
                    this.Text =  Convert.ToString(Minute);
                }
                m_State = StateEnum.NoPreference;
            }

            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
                switch (State)
                {
                    case StateEnum.NoPreference:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.NoPreference;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.NoPreferenceExcludeNext;
                        break;
                    case StateEnum.NoPreferenceExcludeNext:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.IncludeNoPreferenceNext:
                        State = StateEnum.NoPreference;
                        break;

                }
                m_CritGui.PerformWithText();
                m_CritGui.PerformSearch();
            }

            public StateEnum State
            {
                get
                {
                    return m_State;
                }
                set
                {
                    if (value == m_State)
                    {
                        return;
                    }
                    m_State = value;
                    switch (m_State)
                    {
                        case StateEnum.NoPreference:
                            m_CritGui.SetMinuteState(this.Minute, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherMinutes(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.NoPreferenceExcludeNext:
                            m_CritGui.SetMinuteState(this.Minute, State);
                            break;
                        case StateEnum.NoPreferenceNoCall:
                            m_State = StateEnum.NoPreference;
                            ExcludeList = null;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetMinuteState(this.Minute, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherMinutes(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.IncludeNoCall:
                            m_State = StateEnum.Include;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeNoPreferenceNext:
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetMinuteState(this.Minute, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherMinutes(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.ExcludeNoCall:
                            m_State = StateEnum.Exclude;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            m_CritGui.SetMinuteState(this.Minute, State);
                            ExcludeList = m_CritGui.ExcludeOtherMinutes(this.Minute,true);
                            break;
                        case StateEnum.IncludeThisExcludeOthersNoCall:
                            m_State = StateEnum.IncludeThisExcludeOthers;
                            ExcludeList = m_CritGui.ExcludeOtherMinutes(this.Minute,false);
                            break;
                    }
                    CalendarPanel.ButtonColor(m_State, this);

                }
            }

            public int Minute
            {
                get
                {
                    return m_Minute;
                }
            }
        }

        public class MonthButton : Button
        {
            static DateTimeFormatInfo info = new DateTimeFormatInfo();
            private int m_Month;
            private StateEnum m_State;
            List<MonthButton> ExcludeList;
            private CritGui m_CritGui;

            public MonthButton(int Month, CritGui critgui)
            {
                this.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.AutoSize = false;
                this.BackColor = System.Drawing.Color.BurlyWood;
                this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.Size = new System.Drawing.Size(79, 23);
                this.TabIndex = 12;
                this.UseVisualStyleBackColor = false;
                m_Month = Month;
                m_CritGui = critgui;
                m_CritGui.m_MonthButton.Add(m_Month, this);
                this.Text = info.GetMonthName(m_Month);
                m_State = StateEnum.NoPreference;
            }

            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
                switch (State)
                {
                    case StateEnum.NoPreference:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.NoPreference;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.NoPreferenceExcludeNext;
                        break;
                    case StateEnum.NoPreferenceExcludeNext:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.IncludeNoPreferenceNext:
                        State = StateEnum.NoPreference;
                        break;

                }
                m_CritGui.PerformWithText();
                m_CritGui.PerformSearch();
            }

            public StateEnum State
            {
                get
                {
                    return m_State;
                }
                set
                {
                    if (value == m_State)
                    {
                        return;
                    }
                    m_State = value;
                    switch (m_State)
                    {
                        case StateEnum.NoPreference:
                            m_CritGui.SetMonthState(this.Month, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherMonths(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.NoPreferenceExcludeNext:
                            m_CritGui.SetMonthState(this.Month, State);
                            break;
                        case StateEnum.NoPreferenceNoCall:
                            m_State = StateEnum.NoPreference;
                            ExcludeList = null;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetMonthState(this.Month, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherMonths(ExcludeList);
                                ExcludeList = null;
                            }
                            
                            break;
                        case StateEnum.IncludeNoCall:
                            m_State = StateEnum.Include;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeNoPreferenceNext:
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetMonthState(this.Month, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherMonths(ExcludeList);
                                ExcludeList = null;
                            }
                            
                            break;
                        case StateEnum.ExcludeNoCall:
                            m_State = StateEnum.Exclude;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            m_CritGui.SetMonthState(this.Month, State);
                            ExcludeList = m_CritGui.ExcludeOtherMonths(this.Month, true);
                            break;
                        case StateEnum.IncludeThisExcludeOthersNoCall:
                            m_State = StateEnum.IncludeThisExcludeOthers;
                            ExcludeList = m_CritGui.ExcludeOtherMonths(this.Month,false);
                            
                            break;
                    }
                    CalendarPanel.ButtonColor(m_State, this);

                }
            }

            public int Month
            {
                get
                {
                    return m_Month;
                }
            }
        }

        public class DayOfWeekButton : Button
        {
            private CritGui m_CritGui;
            private DayOfWeek m_DayOfWeek;
            private StateEnum m_State;
            private List<DayOfWeekButton> ExcludeList;

            public DayOfWeekButton(DayOfWeek day, CritGui critgui)
            {
                this.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.AutoSize = false;
                this.BackColor = System.Drawing.Color.BurlyWood;
                this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.Size = new System.Drawing.Size(79, 23);
                this.TabIndex = 12;
                this.UseVisualStyleBackColor = false;
                m_DayOfWeek = day;
                this.Text = day.ToString();
                m_CritGui = critgui;
                m_CritGui.m_DayNames.Add(m_DayOfWeek, this);
                m_State = StateEnum.NoPreference;
            }

            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
                switch (State)
                {
                    case StateEnum.NoPreference:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.NoPreference;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.NoPreferenceExcludeNext;
                        break;
                    case StateEnum.NoPreferenceExcludeNext:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.IncludeNoPreferenceNext:
                        State = StateEnum.NoPreference;
                        break;

                }
                m_CritGui.PerformWithText();
                m_CritGui.PerformSearch();
            }


            public StateEnum State
            {
                get
                {
                    return m_State;
                }
                set
                {
                    if (value == m_State)
                    {
                        return;
                    }
                    m_State = value;
                    switch (m_State)
                    {
                        case StateEnum.NoPreference:
                            m_CritGui.SetDayOfWeekState(this.DayName, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherDayOfWeek(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.NoPreferenceExcludeNext:
                            m_CritGui.SetDayOfWeekState(this.DayName, State);
                            break;
                        case StateEnum.NoPreferenceNoCall:
                            m_State = StateEnum.NoPreference;
                            ExcludeList = null;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetDayOfWeekState(this.DayName, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherDayOfWeek(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.IncludeNoCall:
                            m_State = StateEnum.Include;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeNoPreferenceNext:
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetDayOfWeekState(this.DayName, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherDayOfWeek(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.ExcludeNoCall:
                            m_State = StateEnum.Exclude;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            m_CritGui.SetDayOfWeekState(this.DayName, State);
                            ExcludeList = m_CritGui.ExcludeOtherDayOfWeek(this.DayName,true);
                            break;
                        case StateEnum.IncludeThisExcludeOthersNoCall:
                            m_State = StateEnum.IncludeThisExcludeOthers;
                            ExcludeList = m_CritGui.ExcludeOtherDayOfWeek(this.DayName,false);
                            break;
                    }
                    CalendarPanel.ButtonColor(m_State, this);

                }
            }

            public DayOfWeek DayName
            {
                get
                {
                    return m_DayOfWeek;
                }

            }


        }

        public class DayOfMonthButton : Button
        {
            private CritGui m_CritGui;
            private int m_DayOfMonthNum;
            private StateEnum m_State;
            private List<DayOfMonthButton> ExcludeList;

            public DayOfMonthButton(int date, CritGui critgui)
            {
                m_DayOfMonthNum = date;
                this.AutoSize = false;
                this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.Size = new System.Drawing.Size(29, 23);
                this.BackColor = System.Drawing.Color.BurlyWood;
                this.UseVisualStyleBackColor = true;
                this.Text = Convert.ToString(date);
                m_CritGui = critgui;
                m_CritGui.m_DayOfMonth.Add(m_DayOfMonthNum, this);
                this.AutoSize = true;
                State = StateEnum.NoPreference;
            }

            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
                switch (State)
                {
                    case StateEnum.NoPreference:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.NoPreference;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.NoPreferenceExcludeNext;
                        break;
                    case StateEnum.NoPreferenceExcludeNext:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.IncludeNoPreferenceNext:
                        State = StateEnum.NoPreference;
                        break;

                }
                m_CritGui.PerformWithText();
                m_CritGui.PerformSearch();
            }

            public StateEnum State
            {
                get
                {
                    return m_State;
                }
                set
                {
                    if (value == m_State)
                    {
                        return;
                    }
                    m_State = value;
                    switch (m_State)
                    {
                        case StateEnum.NoPreference:
                            m_CritGui.SetDayOfMonthState(this.DayOfMonthNum, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherDayOfMonth(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.NoPreferenceExcludeNext:
                            m_CritGui.SetDayOfMonthState(this.DayOfMonthNum, State);
                            break;
                        case StateEnum.NoPreferenceNoCall:
                            m_State = StateEnum.NoPreference;
                            ExcludeList = null;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetDayOfMonthState(this.DayOfMonthNum, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherDayOfMonth(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.IncludeNoCall:
                            m_State = StateEnum.Include;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeNoPreferenceNext:
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetDayOfMonthState(this.DayOfMonthNum, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherDayOfMonth(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.ExcludeNoCall:
                            m_State = StateEnum.Exclude;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            m_CritGui.SetDayOfMonthState(this.DayOfMonthNum, State);
                            ExcludeList = m_CritGui.ExcludeOtherDayOfMonth(this.DayOfMonthNum,true);
                            break;
                        case StateEnum.IncludeThisExcludeOthersNoCall:
                            m_State = StateEnum.IncludeThisExcludeOthers;
                            ExcludeList = m_CritGui.ExcludeOtherDayOfMonth(this.DayOfMonthNum,false);
                            break;
                    }
                    CalendarPanel.ButtonColor(m_State, this);

                }
            }

            public int DayOfMonthNum
            {
                get
                {
                    return m_DayOfMonthNum;
                }

            }
        }

        public class YearButton : Button
        {
            private CritGui m_CritGui;
            private int m_Year;
            private StateEnum m_State;
            private List<YearButton> ExcludeList;

            public YearButton()
            {
 
                this.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.AutoSize = true;
                this.BackColor = System.Drawing.Color.BurlyWood;
                this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.Size = new System.Drawing.Size(79, 23);
                this.TabIndex = 12;
                this.UseVisualStyleBackColor = false;
            }


            public YearButton(int year, CritGui critgui)
            {
                this.m_Year = year;
                this.Anchor = System.Windows.Forms.AnchorStyles.None;
                this.AutoSize = true;
                this.BackColor = System.Drawing.Color.BurlyWood;
                this.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.Size = new System.Drawing.Size(79, 23);
                this.TabIndex = 12;
                this.UseVisualStyleBackColor = false;
                this.Text = Convert.ToString(year);
                m_CritGui = critgui;
                m_CritGui.m_YearCheckBox.Remove(m_Year);
                m_CritGui.m_YearCheckBox.Add(m_Year, this);
                this.AutoSize = true;
                State = StateEnum.NoPreference;
            }

            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
                switch (State)
                {
                    case StateEnum.NoPreference:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.NoPreference;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.NoPreferenceExcludeNext;
                        break;
                    case StateEnum.NoPreferenceExcludeNext:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.IncludeNoPreferenceNext:
                        State = StateEnum.NoPreference;
                        break;

                }
                m_CritGui.PerformWithText();
                m_CritGui.PerformSearch();
            }

            public StateEnum State
            {
                get
                {
                    return m_State;
                }
                set
                {
                    if (value == m_State)
                    {
                        return;
                    }
                    m_State = value;
                    switch (m_State)
                    {
                        case StateEnum.NoPreference:
                            m_CritGui.SetYearState(this.Year, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeYear(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.NoPreferenceExcludeNext:
                            m_CritGui.SetYearState(this.Year, State);
                            break;
                        case StateEnum.NoPreferenceNoCall:
                            m_State = StateEnum.NoPreference;
                            ExcludeList = null;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetYearState(this.Year, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeYear(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.IncludeNoCall:
                            m_State = StateEnum.Include;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeNoPreferenceNext:
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetYearState(this.Year, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeYear(ExcludeList);
                                ExcludeList = null;
                            }
                            break;
                        case StateEnum.ExcludeNoCall:
                            m_State = StateEnum.Exclude;
                            ExcludeList = null;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            m_CritGui.SetYearState(this.Year, State);
                            ExcludeList = m_CritGui.ExcludeOtherYear(this.Year,true);
                            break;
                        case StateEnum.IncludeThisExcludeOthersNoCall:
                            m_State = StateEnum.IncludeThisExcludeOthers;
                            ExcludeList = m_CritGui.ExcludeOtherYear(this.Year,false);
                            break;
                    }
                    CalendarPanel.ButtonColor(m_State, this);

                }
            }

            public int Year
            {
                get
                {
                    return m_Year;
                }
            }
        }

        private CritGui m_CritGui;

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

        private FTObjects m_FTObjects;
        public List<VersionInfo> VersionToShow;
        CalendarText cal;
        bool buttonsAdded;

        public delegate void  VoidBoolDelegate(bool b);
        private List<YearButton> YearButtonList;

        Label l;
        bool showingWait = false;
        public CalendarPanel()
        {
            InitializeComponent();
            VersionToShow = new List<VersionInfo>();
            this.VersionInfoPanelList = new PanelList<VersionInfo>();
            l = new Label();
            l.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            l.Text = "Please Wait";
            l.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            l.Visible = false;
            l.AutoSize = true;

            YearButtonList = new List<YearButton>();
            this.VersionInfoPanelList.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(l);
            this.panel1.Resize += new EventHandler(panel1_Resize);
            this.panel1.Controls.Add(this.VersionInfoPanelList);
            this.DoubleBuffered = true;
        }

        void panel1_Resize(object sender, EventArgs e)
        {
            l.Refresh();
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

        #region GuiMessage Functions

        public void SetDeleteReturnVal(FolderTrack.Delete.DeleteRules.DeleteReturn del)
        {
            if (del == FolderTrack.Delete.DeleteRules.DeleteReturn.ALL)
            {
                includeRemove = true;
                
            }
            else if(del == FolderTrack.Delete.DeleteRules.DeleteReturn.NOT_REMOVED)
            {
                includeRemove = false;
               
            }

            //when all data is received call perform search
           
            CallPerformS = true;

            HoldAllDisplay = true;
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
                
                VersionInfoPanelList.PanelFromData = new PanelFromVersionInfo(m_FTObjects);
                callb = false;
                if (callFunthcom)
                {
                    FunctionsThatNeedCompleteVersionInfo();
                    callFunthcom = false;
                }
                
            }
        }

        private void FunctionsThatNeedCompleteVersionInfo()
        {

            if (m_FTObjects.NameReady() == false || callb == true)
            {
                return;
            }
            
            this.YearPanel.Invoke(new VoidNoArgDelegate(AddButtond));
            VersionInfo vers = m_FTObjects.GetCurrentVersionInfo();
            if (vers != null && CallPerformS == false)
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
            if (CallPerformS)
            {
                CallPerformS = false;
                new Thread(m_CritGui.PerformSearch).Start();
            }
        }

        private void AddButtond()
        {

                if (m_CritGui == null)
                {
                    m_CritGui = new CritGui(this);
                }
                m_Years = new int[0];
                int[] years = m_FTObjects.GetYears;
                int LatestYear = 0;

                foreach (YearButton yb in YearButtonList)
                {
                    this.YearPanel.Controls.Remove(yb);
                }

                AddAllYear(years);
                foreach (int year in years)
                {
                    LatestYear = (LatestYear < year) ? year : LatestYear;
                }
                cal = new CalendarText(this, m_FTObjects.GetYears);

                if (buttonsAdded == false)
                {

                    ColFromDayOfWeek = new Dictionary<DayOfWeek, int>();

                    AddMonths(LatestYear);
                    AddDays();
                    PopulateCalendar();
                    PopulateTime();
                    buttonsAdded = true;
                }
                new Thread(SetSplit).Start();
            
        }

        private void SetSplit()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidNoArgDelegate(SetSplit));
                return;
            }
            try
            {
                this.SplitterDistance = this.YearGroupBox.Width + this.SplitterWidth + 17; //make room for scroll
            }
            catch (Exception)
            {
                //throw away
            }
        }

        

        private void TextTextBox_TextChanged(object sender, EventArgs e)
        {
            if (rejectText == true)
            {
                rejectText = false;
                return;
            }
            VersionInfoSearchCriteria crit = cal.ProcessString(this.TextTextBox.Text);
            crit.includeRemove = includeRemove;

            if (crit != null)
            {
                m_CritGui.SetCriteria(crit);
            }
            else if (this.TextTextBox.Text.Length == 0)
            {
                //Clear ll
                m_CritGui.SetCriteria(new VersionInfoSearchCriteria());
            }
        }

        public void AddAllYear(int[] yearList)
        {
            if (yearList.Length == 0)
            {
                return;
            }
            if (this.YearPanel.InvokeRequired == true)
            {
                this.YearPanel.Invoke(new VoidListIntArra(AddAllYear), new object[] { yearList });
                return;
            }
            YearButton yearB;
            Point pt = new Point(0, 0);
            this.YearPanel.Size = new Size(yearList.Length * 100, this.YearPanel.Height);
            this.YearPanel.SuspendLayout();
            foreach (int year in yearList)
            {
                
                Array.Resize<int>(ref m_Years, m_Years.Length + 1);
                m_Years[m_Years.Length - 1] = year;
                yearB = new YearButton(year, m_CritGui);
                YearButtonList.Add(yearB);

                yearB.Location = pt;
                pt.X = yearB.Location.X + yearB.Width + 20;
                this.YearPanel.Controls.Add(yearB);
                //  this.YearPanelBack.Size = new Size(this.YearPanelBack.Width, yearB.Height + 20);

            }
            this.YearPanel.PerformLayout();
            this.YearPanel.ResumeLayout(false);

        }

        private void AddMonths(int year)
        {
            if (year == 0)
            {
                return;
            }
            Calendar cal = CultureInfo.InvariantCulture.Calendar;

            int AmountOfMonths = cal.GetMonthsInYear(year);
            int col =0;
            int row =0;
            for (int i = 1; i <= AmountOfMonths; i++)
            {
                this.MonthTableLayoutPanel.Controls.Add(new MonthButton(i, m_CritGui),col,row);
                col++;
                if (col == 4)
                {
                    col = 0;
                    row++;
                }
            }
        }

        
        private void AddDays()
        {
            DayOfWeek [] week = new DayOfWeek[Enum.GetValues(typeof(DayOfWeek)).Length];
            Enum.GetValues(typeof(DayOfWeek)).CopyTo(week, 0);
            int row = 0;
            int col = 0;

            for (int i = 1; i <= week.Length ; i++ )
            {
                //This is for Sunday 
                if (i == week.Length)
                {
                    this.DayOfWeekTableLayoutPanel.Controls.Add(new DayOfWeekButton(week[0], m_CritGui),col,row);
                    ColFromDayOfWeek[week[0]] = 0;
                }
                else
                {
                    this.DayOfWeekTableLayoutPanel.Controls.Add(new DayOfWeekButton(week[i], m_CritGui),col,row);
                    ColFromDayOfWeek[week[i]] = i;
                }
                col++;
                if (col == 4)
                {
                    col = 0;
                    row++;
                }
            }
        }

        private void PopulateTime()
        {
           // DateTimeFormatInfo datetimfor = new DateTimeFormatInfo();
           //// this.AMLabel.Text = datetimfor.AMDesignator;
           //// this.PMLabel.Text = datetimfor.PMDesignator;

            int column;
            int row;
            for (int ampm = 0; ampm <= 1; ampm++)
            {
                column = 0;
                row = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (column == (7-1))
                    {
                        column = 0;
                        row++;
                    }
                    //If AM
                    if (ampm == 0)
                    {
                        this.AMTableLayoutPanel.Controls.Add(new HourButton(i, ampm, m_CritGui),column,row);
                    }
                    else //If PM
                    {
                        this.PMtableLayoutPanel.Controls.Add(new HourButton(i, ampm, m_CritGui),column,row);
                    }
                    column++;
                }

            }

            for (int min = 0; min < 60; min++)
            {
                this.MinuteTableLayoutPanel.Controls.Add(new MinuteButton(min, m_CritGui));
            }
        }


        private void this_Resize(object sender, EventArgs e)
        {
            SetSplit();
        }


        private void PopulateCalendar()
        {
            int row = 0;
            int col = 0;
            int daysinweek = Enum.GetValues(typeof(DayOfWeek)).Length;
            for (int date = 1; date <= 31; date++)
            {
                this.DayOfMonthTableLayoutPanel.Controls.Add(new DayOfMonthButton(date, m_CritGui), col, row);
                col++;
                if (date % daysinweek == 0)
                {
                    col = 0;
                    row++;
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.MinuteTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.HourBTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.AMPanel = new System.Windows.Forms.Panel();
            this.AMTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.PMPanel = new System.Windows.Forms.Panel();
            this.PMtableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DayOfMonthTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DayOfWeekGroupBox = new System.Windows.Forms.GroupBox();
            this.DayOfWeekTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TextTextBox = new System.Windows.Forms.TextBox();
            this.YearGroupBox = new System.Windows.Forms.GroupBox();
            this.YearPanelBack = new System.Windows.Forms.Panel();
            this.YearPanel = new System.Windows.Forms.Panel();
            this.MonthGroupBox = new System.Windows.Forms.GroupBox();
            this.MonthTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.HourBTableLayoutPanel.SuspendLayout();
            this.AMPanel.SuspendLayout();
            this.PMPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.DayOfWeekGroupBox.SuspendLayout();
            this.YearGroupBox.SuspendLayout();
            this.YearPanelBack.SuspendLayout();
            this.MonthGroupBox.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.DayOfWeekGroupBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.TextTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.YearGroupBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.MonthGroupBox, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(263, 263);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.MinuteTableLayoutPanel);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(3, 241);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(129, 19);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Minute";
            this.groupBox3.Visible = false;
            // 
            // MinuteTableLayoutPanel
            // 
            this.MinuteTableLayoutPanel.AutoSize = true;
            this.MinuteTableLayoutPanel.ColumnCount = 10;
            this.MinuteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MinuteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MinuteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MinuteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MinuteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MinuteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MinuteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MinuteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MinuteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MinuteTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MinuteTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinuteTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.MinuteTableLayoutPanel.Name = "MinuteTableLayoutPanel";
            this.MinuteTableLayoutPanel.RowCount = 6;
            this.MinuteTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MinuteTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MinuteTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MinuteTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MinuteTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MinuteTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MinuteTableLayoutPanel.Size = new System.Drawing.Size(123, 0);
            this.MinuteTableLayoutPanel.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.HourBTableLayoutPanel);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(3, 190);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(129, 45);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Hour";
            // 
            // HourBTableLayoutPanel
            // 
            this.HourBTableLayoutPanel.AutoSize = true;
            this.HourBTableLayoutPanel.ColumnCount = 2;
            this.HourBTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.HourBTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.HourBTableLayoutPanel.Controls.Add(this.AMPanel, 1, 0);
            this.HourBTableLayoutPanel.Controls.Add(this.PMPanel, 1, 1);
            this.HourBTableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.HourBTableLayoutPanel.Controls.Add(this.label2, 0, 1);
            this.HourBTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HourBTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.HourBTableLayoutPanel.Name = "HourBTableLayoutPanel";
            this.HourBTableLayoutPanel.RowCount = 2;
            this.HourBTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.HourBTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.HourBTableLayoutPanel.Size = new System.Drawing.Size(123, 26);
            this.HourBTableLayoutPanel.TabIndex = 0;
            // 
            // AMPanel
            // 
            this.AMPanel.AutoSize = true;
            this.AMPanel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.AMPanel.Controls.Add(this.AMTableLayoutPanel);
            this.AMPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AMPanel.Location = new System.Drawing.Point(23, 0);
            this.AMPanel.Margin = new System.Windows.Forms.Padding(0);
            this.AMPanel.Name = "AMPanel";
            this.AMPanel.Size = new System.Drawing.Size(100, 13);
            this.AMPanel.TabIndex = 0;
            // 
            // AMTableLayoutPanel
            // 
            this.AMTableLayoutPanel.AutoSize = true;
            this.AMTableLayoutPanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.AMTableLayoutPanel.ColumnCount = 12;
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.AMTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AMTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.AMTableLayoutPanel.Name = "AMTableLayoutPanel";
            this.AMTableLayoutPanel.RowCount = 1;
            this.AMTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.AMTableLayoutPanel.Size = new System.Drawing.Size(100, 13);
            this.AMTableLayoutPanel.TabIndex = 1;
            // 
            // PMPanel
            // 
            this.PMPanel.AutoSize = true;
            this.PMPanel.BackColor = System.Drawing.Color.Black;
            this.PMPanel.Controls.Add(this.PMtableLayoutPanel);
            this.PMPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PMPanel.Location = new System.Drawing.Point(23, 13);
            this.PMPanel.Margin = new System.Windows.Forms.Padding(0);
            this.PMPanel.Name = "PMPanel";
            this.PMPanel.Size = new System.Drawing.Size(100, 13);
            this.PMPanel.TabIndex = 1;
            // 
            // PMtableLayoutPanel
            // 
            this.PMtableLayoutPanel.AutoSize = true;
            this.PMtableLayoutPanel.BackColor = System.Drawing.Color.Black;
            this.PMtableLayoutPanel.ColumnCount = 12;
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PMtableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PMtableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.PMtableLayoutPanel.Name = "PMtableLayoutPanel";
            this.PMtableLayoutPanel.RowCount = 1;
            this.PMtableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PMtableLayoutPanel.Size = new System.Drawing.Size(100, 13);
            this.PMtableLayoutPanel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "AM";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "PM";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.DayOfMonthTableLayoutPanel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(3, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(129, 19);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Day Of Month";
            // 
            // DayOfMonthTableLayoutPanel
            // 
            this.DayOfMonthTableLayoutPanel.AutoSize = true;
            this.DayOfMonthTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DayOfMonthTableLayoutPanel.ColumnCount = 7;
            this.DayOfMonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfMonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfMonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfMonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfMonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfMonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfMonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfMonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.DayOfMonthTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DayOfMonthTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.DayOfMonthTableLayoutPanel.Name = "DayOfMonthTableLayoutPanel";
            this.DayOfMonthTableLayoutPanel.RowCount = 5;
            this.DayOfMonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DayOfMonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DayOfMonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DayOfMonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DayOfMonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DayOfMonthTableLayoutPanel.Size = new System.Drawing.Size(123, 0);
            this.DayOfMonthTableLayoutPanel.TabIndex = 0;
            // 
            // DayOfWeekGroupBox
            // 
            this.DayOfWeekGroupBox.AutoSize = true;
            this.DayOfWeekGroupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DayOfWeekGroupBox.Controls.Add(this.DayOfWeekTableLayoutPanel);
            this.DayOfWeekGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DayOfWeekGroupBox.ForeColor = System.Drawing.Color.Black;
            this.DayOfWeekGroupBox.Location = new System.Drawing.Point(3, 140);
            this.DayOfWeekGroupBox.Name = "DayOfWeekGroupBox";
            this.DayOfWeekGroupBox.Size = new System.Drawing.Size(129, 19);
            this.DayOfWeekGroupBox.TabIndex = 4;
            this.DayOfWeekGroupBox.TabStop = false;
            this.DayOfWeekGroupBox.Text = "Day Of Week";
            // 
            // DayOfWeekTableLayoutPanel
            // 
            this.DayOfWeekTableLayoutPanel.AutoSize = true;
            this.DayOfWeekTableLayoutPanel.ColumnCount = 5;
            this.DayOfWeekTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfWeekTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfWeekTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfWeekTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DayOfWeekTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.DayOfWeekTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DayOfWeekTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.DayOfWeekTableLayoutPanel.Name = "DayOfWeekTableLayoutPanel";
            this.DayOfWeekTableLayoutPanel.RowCount = 2;
            this.DayOfWeekTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DayOfWeekTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DayOfWeekTableLayoutPanel.Size = new System.Drawing.Size(123, 0);
            this.DayOfWeekTableLayoutPanel.TabIndex = 0;
            // 
            // TextTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TextTextBox, 2);
            this.TextTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextTextBox.Location = new System.Drawing.Point(3, 3);
            this.TextTextBox.Name = "TextTextBox";
            this.TextTextBox.Size = new System.Drawing.Size(257, 20);
            this.TextTextBox.TabIndex = 1;
            this.TextTextBox.TextChanged += new System.EventHandler(this.TextTextBox_TextChanged);
            // 
            // YearGroupBox
            // 
            this.YearGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.YearGroupBox.Controls.Add(this.YearPanelBack);
            this.YearGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YearGroupBox.ForeColor = System.Drawing.Color.Black;
            this.YearGroupBox.Location = new System.Drawing.Point(3, 53);
            this.YearGroupBox.Name = "YearGroupBox";
            this.YearGroupBox.Size = new System.Drawing.Size(129, 56);
            this.YearGroupBox.TabIndex = 2;
            this.YearGroupBox.TabStop = false;
            this.YearGroupBox.Text = "Year";
            // 
            // YearPanelBack
            // 
            this.YearPanelBack.AutoScroll = true;
            this.YearPanelBack.Controls.Add(this.YearPanel);
            this.YearPanelBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.YearPanelBack.Location = new System.Drawing.Point(3, 16);
            this.YearPanelBack.Name = "YearPanelBack";
            this.YearPanelBack.Size = new System.Drawing.Size(123, 37);
            this.YearPanelBack.TabIndex = 0;
            // 
            // YearPanel
            // 
            this.YearPanel.AutoSize = true;
            this.YearPanel.Location = new System.Drawing.Point(3, 3);
            this.YearPanel.Name = "YearPanel";
            this.YearPanel.Size = new System.Drawing.Size(84, 22);
            this.YearPanel.TabIndex = 0;
            // 
            // MonthGroupBox
            // 
            this.MonthGroupBox.AutoSize = true;
            this.MonthGroupBox.Controls.Add(this.MonthTableLayoutPanel);
            this.MonthGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MonthGroupBox.ForeColor = System.Drawing.Color.Black;
            this.MonthGroupBox.Location = new System.Drawing.Point(3, 115);
            this.MonthGroupBox.Name = "MonthGroupBox";
            this.MonthGroupBox.Size = new System.Drawing.Size(129, 19);
            this.MonthGroupBox.TabIndex = 3;
            this.MonthGroupBox.TabStop = false;
            this.MonthGroupBox.Text = "Month";
            // 
            // MonthTableLayoutPanel
            // 
            this.MonthTableLayoutPanel.AutoSize = true;
            this.MonthTableLayoutPanel.ColumnCount = 5;
            this.MonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.MonthTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MonthTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MonthTableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.MonthTableLayoutPanel.Name = "MonthTableLayoutPanel";
            this.MonthTableLayoutPanel.RowCount = 3;
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.MonthTableLayoutPanel.Size = new System.Drawing.Size(123, 0);
            this.MonthTableLayoutPanel.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.Location = new System.Drawing.Point(12, 31);
            this.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.Panel1.AutoScroll = true;
            this.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.Panel2.Controls.Add(this.panel1);
            this.Size = new System.Drawing.Size(828, 687);
            this.SplitterDistance = 87;
            this.SplitterWidth = 8;
            this.TabIndex = 1;
            this.Resize += new System.EventHandler(this_Resize);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(87, 687);
            this.panel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 687);
            this.panel1.TabIndex = 0;
            // 
            // CalendarDesignerForm
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.HourBTableLayoutPanel.ResumeLayout(false);
            this.HourBTableLayoutPanel.PerformLayout();
            this.AMPanel.ResumeLayout(false);
            this.AMPanel.PerformLayout();
            this.PMPanel.ResumeLayout(false);
            this.PMPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.DayOfWeekGroupBox.ResumeLayout(false);
            this.DayOfWeekGroupBox.PerformLayout();
            this.YearGroupBox.ResumeLayout(false);
            this.YearPanelBack.ResumeLayout(false);
            this.YearPanelBack.PerformLayout();
            this.MonthGroupBox.ResumeLayout(false);
            this.MonthGroupBox.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel DayOfMonthTableLayoutPanel;
        private System.Windows.Forms.GroupBox DayOfWeekGroupBox;
        private System.Windows.Forms.TableLayoutPanel DayOfWeekTableLayoutPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel HourBTableLayoutPanel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel MinuteTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel MonthTableLayoutPanel;
        private System.Windows.Forms.GroupBox MonthGroupBox;
        private System.Windows.Forms.TableLayoutPanel TextTableLayoutPanel;
        private System.Windows.Forms.TextBox TextTextBox;
        private System.Windows.Forms.GroupBox YearGroupBox;
        private System.Windows.Forms.Panel YearPanelBack;
        private System.Windows.Forms.Panel YearPanel;
        private System.Windows.Forms.Panel AMPanel;
        private System.Windows.Forms.Panel PMPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel AMTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel PMtableLayoutPanel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;





        #region SetCriteris Members

        public void SetCriteria(VersionInfoSearchCriteria criteria)
        {
            m_CritGui.SetCriteria(criteria);
        }

        #endregion

        #region FolderTrackCallBack Members

        public void NewCurrentVersion(string MonitorGroup, VersionInfo vers)
        {
            lock (newVers)
            {
                VersionInfo versI = m_FTObjects.VersionInfoFromVersionName(vers.versionName);

                if (lastCurrentVersion != null)
                {
                   this.VersionInfoPanelList.AddFunctionCallToDataH(lastCurrentVersion, false);
                }
                //if versI is null it is probably because all data from FolderTrack is not 
                // once it is done this function will be called again
                if (versI != null)
                {
                    this.VersionInfoPanelList.AddFunctionCallToDataH(versI, true);
                    lastCurrentVersion = versI;
                }
                if (HoldAllDisplay == false)
                {
                    this.VersionInfoPanelList.CallInvoke();
                }

                if (usemoncall == true)
                {
                    this.VersionInfoPanelList.CallInvoke();
                    usemoncall = false;
                    newcur = false;
                    HoldAllDisplay = false;
                }
                else
                {
                    newcur = true;
                }
            }
        }

        

        public void NewVersion(string MonitorGroup, List<VersionInfo> vers)
        {
            m_CritGui.PerformSearch();
        }

        public void DeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            new Thread(m_CritGui.PerformSearch).Start();
        }


        public void DeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
            new Thread(m_CritGui.PerformSearch).Start();
        }


        public void UndeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            new Thread(m_CritGui.PerformSearch).Start();
        }


        public void UndeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
        }

        public void SendNewUserVersion(string MonitorGroup, List<string> UserVersion)
        {
            
        }

        public void DontMonitor(string MonitorGroup)
        {
            if (HoldAllDisplay)
            {
                this.VersionInfoPanelList.CallAllH(VersionMini.DONT_USE);
            }
            else
            {
                this.VersionInfoPanelList.CallAll(VersionMini.DONT_USE);
            }
            if(newcur == true && m_CritGui.upd == true)
            {
                this.VersionInfoPanelList.CallInvoke();
                usemoncall = false;
                newcur = false;
                HoldAllDisplay = false;
                m_CritGui.upd = false;
            }
            else
            {
                usemoncall = true;
            }
        }

        public void SendData(List<ChangeInstruction> data, int idnum, bool done)
        {
            
        }

        public void SendDataV(List<VersionInfo> data, string name, int idnum, bool done)
        {
            if (done == true)
            {
                if (m_FTObjects != null)
                {
                    FunctionsThatNeedCompleteVersionInfo();
                }
                else
                {
                    callFunthcom = true;
                }
            }
        }

        public void RestartMonitor(string MonitorGroup)
        {
            this.VersionInfoPanelList.CallAll(VersionMini.USE);
        }

        public void UseMonitor()
        {
            if (HoldAllDisplay)
            {
                this.VersionInfoPanelList.CallAllH(VersionMini.USE);
                
            }
            else
            {
                this.VersionInfoPanelList.CallAll(VersionMini.USE);
            }

            if (newcur == true && m_CritGui.upd == true)
            {
                this.VersionInfoPanelList.CallInvoke();
                usemoncall = false;
                newcur = false;
                HoldAllDisplay = false;
                m_CritGui.upd = false;
            }
            else
            {
                usemoncall = true;
            }
        }

        public void NewVersionInformation(string MonitorGroup, List<VersionInfo> vers)
        {
            m_CritGui.PerformSearch();
        }

        public void NewDiscription(string MonitorGroup, VersionInfo version)
        {
            m_CritGui.PerformSearch();
        }

        public void PleaseRegister()
        {
            
        }

        public void TaskUpdate(TaskGroup [] task)
        {

        }

        #endregion


    }
}
