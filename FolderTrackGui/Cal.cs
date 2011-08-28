using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using FolderTrack.Types;
using System.Threading;

namespace FolderTrackGuiTest1
{
    public partial class Cal : Form
    {
        List<VersionInfo> VersionToShow;
        public class CritGui: DataManager.Status
        {
            VersionInfoSearchCriteria m_Criteria = new VersionInfoSearchCriteria();
            public Dictionary<int, MonthButton> m_MonthButton;
            public Dictionary<DayOfWeek, DayOfWeekButton> m_DayNames;
            public Dictionary<int, DayOfMonthButton> m_DayOfMonth;
            public Dictionary<int, YearButton> m_YearCheckBox;
            public Dictionary<int, HourButton> m_HourButton;
            public Dictionary<int, MinuteButton> m_MinuteButton;
            Cal m_Cal;

            private delegate void UpdateVersionDelegateWrArg(VersionInfo vers);
            private delegate void UpdateVersionDelegate();

            public CritGui(Cal cal)
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

            public void PerformSearch()
            {
                Thread t = new Thread(UpdateThread);
                t.Start();

            }
            int row;
            private void UpdateThread(object datamanager)
            {
                m_Cal.VersionToShow = m_Cal.m_DataManager.VersionInfoFromCriteria(m_Criteria,null);
              //  if (m_list.Count != 0)
             //   {
             //       m_Cal.CalDataGridView.RowCount = m_list.Count;
            //    }

                m_Cal.Invoke(new UpdateVersionDelegate(UpdateSizeOfVersion));
            //    row = 0;
            //    foreach (VersionInfo vers in list)
             //   {
             //       m_Cal.Invoke(new UpdateVersionDelegateWrArg(AddVersion), new object[] { vers });
             //       row++;
              //  }
             //   m_Cal.Invoke(new UpdateVersionDelegate(DoneUpdateSizeOfVersion));
            }
            private void UpdateSizeOfVersion()
            {
                if (m_Cal.VersionToShow.Count != 0)
                {
                    m_Cal.CalDataGridView.RowCount = m_Cal.VersionToShow.Count;
                }
            //    m_Cal.SuspendLayout();
             //   m_Cal.VersionTableLayoutPanel.SuspendLayout();
           //     m_Cal.VersionPanel.SuspendLayout();
            //    m_Cal.MainTableLayoutPanel.SuspendLayout();
          //      m_Cal.VersionTableLayoutPanel.Controls.Clear();
         //       m_Cal.VersionTableLayoutPanel.RowCount = list.Count;
       //         for(int i = 0; i < m_Cal.VersionTableLayoutPanel.RowCount ; i++)
         //       {
            //        m_Cal.VersionTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
        //        }
            }

            private void DoneUpdateSizeOfVersion()
            {
                m_Cal.ResumeLayout(false);
                m_Cal.PerformLayout();
                m_Cal.Refresh();
                m_Cal.MainTableLayoutPanel.ResumeLayout(false);
                m_Cal.MainTableLayoutPanel.PerformLayout();
                m_Cal.MainTableLayoutPanel.Refresh();
                m_Cal.VersionPanel.ResumeLayout(false);
                m_Cal.VersionPanel.PerformLayout();
                m_Cal.VersionPanel.Refresh();
            //    m_Cal.VersionTableLayoutPanel.ResumeLayout(false);
             //   m_Cal.VersionTableLayoutPanel.PerformLayout();
            //    m_Cal.VersionTableLayoutPanel.Refresh();
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

            public List<HourButton> ExcludeOtherHours(int hour)
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
                        hourcheckbox.State = StateEnum.Exclude;
                        ReturnList.Add(hourcheckbox);
                    }
                }

                return ReturnList;
            }

            public void UnExcludeOtherHours(List<HourButton> hours)
            {
                foreach (HourButton hourcheckbox in hours)
                {
                    if (hourcheckbox.State == StateEnum.Exclude)
                    {
                        hourcheckbox.State = StateEnum.NoPreference;
                    }
                }
            }

            public void SetMinuteState(int minute, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
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

            public List<MinuteButton> ExcludeOtherMinutes(int minute)
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
                        minutecheckbox.State = StateEnum.Exclude;
                        ReturnList.Add(minutecheckbox);
                    }
                }

                return ReturnList;
            }

            public void UnExcludeOtherMinutes(List<MinuteButton> minutes)
            {
                foreach (MinuteButton minutecheckbox in minutes)
                {
                    if (minutecheckbox.State == StateEnum.Exclude)
                    {
                        minutecheckbox.State = StateEnum.NoPreference;
                    }
                }
            }

            public void SetMonthState(int month, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
                        m_Criteria.AddMonth(month, false);
                        break;

                    case StateEnum.Exclude:
                        m_Criteria.AddMonth(month, true);
                        break;

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
            public List<MonthButton> ExcludeOtherMonths(int month)
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
                        monthcheckbox.State = StateEnum.Exclude;
                        ReturnList.Add(monthcheckbox);
                    }
                }

                return ReturnList;
            }

            
            public void UnExcludeOtherMonths(List<MonthButton> months)
            {
                foreach (MonthButton monthcheckbox in months)
                {
                    if (monthcheckbox.State == StateEnum.Exclude)
                    {
                        monthcheckbox.State = StateEnum.NoPreference;
                    }
                }
            }


            public void SetDayOfWeekState(DayOfWeek dayofweek, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
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
            public List<DayOfWeekButton> ExcludeOtherDayOfWeek(DayOfWeek dayofweek)
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
                        daynames.State = StateEnum.Exclude;
                        ReturnList.Add(daynames);
                    }
                }

                return ReturnList;
            }


            public void UnExcludeOtherDayOfWeek(List<DayOfWeekButton> dayofweeks)
            {
                foreach (DayOfWeekButton dayofweek in dayofweeks)
                {
                    if (dayofweek.State == StateEnum.Exclude)
                    {
                        dayofweek.State = StateEnum.NoPreference;
                    }
                }
            }

            public void SetDayOfMonthState(int dayofmonth, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
                        m_Criteria.AddDaysOfMonth(dayofmonth, false);
                        break;

                    case StateEnum.Exclude:
                        m_Criteria.AddDaysOfMonth(dayofmonth, true);
                        break;

                    case StateEnum.NoPreference:
                        m_Criteria.RemoveDaysOfMonth(dayofmonth);
                        break;
                }

                if (m_DayOfMonth[dayofmonth].State != state)
                {
                    m_DayOfMonth[dayofmonth].State = state;
                }
            }

            //Returns a list of months excluded by this call
            public List<DayOfMonthButton> ExcludeOtherDayOfMonth(int dayofmonth)
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
                        dayofmonthbutton.State = StateEnum.Exclude;
                        ReturnList.Add(dayofmonthbutton);
                    }
                }

                return ReturnList;
            }


            public void UnExcludeOtherDayOfMonth(List<DayOfMonthButton> dayofmonths)
            {
                foreach (DayOfMonthButton dayofmonthbutton in dayofmonths)
                {
                    if (dayofmonthbutton.State == StateEnum.Exclude)
                    {
                        dayofmonthbutton.State = StateEnum.NoPreference;
                    }
                }
            }

            //Year
            public void SetYearState(int year, StateEnum state)
            {
                switch (state)
                {
                    case StateEnum.Include:
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
            public List<YearButton> ExcludeOtherYear(int year)
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
                        yearcheckbox.State = StateEnum.Exclude;
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







            #region Status Members

            public void ReceiveStatus(string stat)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        public enum StateEnum
        {
            NoPreference,
            Include,
            IncludeThisExcludeOthers,
            Exclude
        }


        private int m_Year;
        private int m_Day;
        private int m_Month;
        private int[] m_Years;

        private Dictionary<DayOfWeek, int> ColFromDayOfWeek;

        public class HourButton : Button
        {
            static DateTimeFormatInfo info = new DateTimeFormatInfo();
            private int m_Hour;
            private int m_AmPm;
            private StateEnum m_State;
            List<HourButton> ExcludeList;
            private CritGui m_CritGui;

            public HourButton(int Hour,int ampm, CritGui critgui)
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
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.NoPreference;
                        break;

                }
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
                            this.BackColor = Color.White;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetHourState(this.Hour, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherHours(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Blue;
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetHourState(this.Hour, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherHours(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Red;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            ExcludeList = m_CritGui.ExcludeOtherHours(this.Hour);
                            this.BackColor = Color.Blue;
                            break;
                    }

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
                m_CritGui.m_MinuteButton.Add(m_Minute, this);
                this.Text = Convert.ToString(Minute);
                m_State = StateEnum.NoPreference;
            }

            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
                switch (State)
                {
                    case StateEnum.NoPreference:
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.NoPreference;
                        break;

                }
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
                            this.BackColor = Color.White;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetMinuteState(this.Minute, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherMinutes(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Blue;
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetMinuteState(this.Minute, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherMinutes(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Red;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            ExcludeList = m_CritGui.ExcludeOtherMinutes(this.Minute);
                            this.BackColor = Color.Blue;
                            break;
                    }

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
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.NoPreference;
                        break;

                }
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
                            this.BackColor = Color.White;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetMonthState(this.Month, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherMonths(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Blue;
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetMonthState(this.Month, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherMonths(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Red;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            ExcludeList = m_CritGui.ExcludeOtherMonths(this.Month);
                            this.BackColor = Color.Blue;
                            break;
                    }

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
                m_DayOfWeek = day;
                this.Text = day.ToString().Substring(0,3);
                this.AutoSize = true;
                m_CritGui = critgui;
                m_CritGui.m_DayNames.Add(m_DayOfWeek,this);
                m_State = StateEnum.NoPreference;
            }

            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);
                switch (State)
                {
                    case StateEnum.NoPreference:
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.NoPreference;
                        break;

                }
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
                            this.BackColor = Color.White;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetDayOfWeekState(this.DayName, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherDayOfWeek(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Blue;
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetDayOfWeekState(this.DayName, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherDayOfWeek(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Red;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            ExcludeList = m_CritGui.ExcludeOtherDayOfWeek(this.DayName);
                            this.BackColor = Color.Blue;
                            break;
                    }

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
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.NoPreference;
                        break;

                }
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
                            this.BackColor = Color.White;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetDayOfMonthState(this.DayOfMonthNum, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherDayOfMonth(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Blue;
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetDayOfMonthState(this.DayOfMonthNum, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeOtherDayOfMonth(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Red;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            ExcludeList = m_CritGui.ExcludeOtherDayOfMonth(this.DayOfMonthNum);
                            this.BackColor = Color.Blue;
                            break;
                    }

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

            public YearButton(int year, CritGui critgui)
            {
                this.m_Year = year;
                this.Text = Convert.ToString(year);
                m_CritGui = critgui;
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
                        State = StateEnum.Include;
                        break;
                    case StateEnum.Include:
                        State = StateEnum.IncludeThisExcludeOthers;
                        break;
                    case StateEnum.IncludeThisExcludeOthers:
                        State = StateEnum.Exclude;
                        break;
                    case StateEnum.Exclude:
                        State = StateEnum.NoPreference;
                        break;

                }
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
                            this.BackColor = Color.White;
                            break;
                        case StateEnum.Include:
                            m_CritGui.SetYearState(this.Year, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeYear(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Blue;
                            break;
                        case StateEnum.Exclude:
                            m_CritGui.SetYearState(this.Year, State);
                            if (ExcludeList != null)
                            {
                                m_CritGui.UnExcludeYear(ExcludeList);
                                ExcludeList = null;
                            }
                            this.BackColor = Color.Red;
                            break;
                        case StateEnum.IncludeThisExcludeOthers:
                            ExcludeList = m_CritGui.ExcludeOtherYear(this.Year);
                            this.BackColor = Color.Blue;
                            break;
                    }

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
        private DataManager m_DataManager;

        public void AddYear(int year)
        {
            YearButton yearB;
            Array.Resize<int>(ref m_Years,m_Years.Length + 1);
            m_Years[m_Years.Length - 1] = year;
            yearB = new YearButton(year, m_CritGui);
            yearB.Location = new Point(m_Years.Length * 100, 0);
            this.YearPanel.Controls.Add(yearB);

        }

        Label l;

        public Cal()
        {
            m_Years = new int [0];
            InitializeComponent();
            l = new Label();
            VersionToShow = new List<VersionInfo>();
            m_CritGui = new CritGui(this);
            ColFromDayOfWeek = new Dictionary<DayOfWeek, int>();
            AddMonths(m_Year);
            AddDays();
            PopulateDays(2009,12);
            this.CalDataGridView.VirtualMode = true;
            this.CalDataGridView.RowCount = 200;
        }

        public Cal(DataManager datamanager)
        {
            InitializeComponent();
            m_DataManager = datamanager;
            VersionToShow = new List<VersionInfo>();
            this.CalDataGridView.VirtualMode = true;
            m_CritGui = new CritGui(this);
            ColFromDayOfWeek = new Dictionary<DayOfWeek, int>();
            m_Years = new int [0];
            int [] years = m_DataManager.GetYears();
            int LatestYear = 0;
            foreach(int year in years)
            {
                AddYear(year);
                LatestYear = (LatestYear < year) ? year : LatestYear;
            }

            AddMonths(LatestYear);
            AddDays();
            PopulateCalendar();
            PopulateTime();
        //    m_CritGui.PerformSearch();
         //   this.VersionTableLayoutPanel.Controls.Add(new CalendarPanel(), 0, 0);
        }



        private void AddMonths(int year)
        {
            Calendar cal = CultureInfo.InvariantCulture.Calendar;
            
            int AmountOfMonths = cal.GetMonthsInYear(year);
            for (int i = 1; i <= AmountOfMonths; i++)
            {
                this.MonthTableLayoutPanel.Controls.Add(new MonthButton(i, m_CritGui));
            }
        }

        private void AddDays()
        {
            int i = 0;
            foreach (DayOfWeek da in Enum.GetValues(typeof(DayOfWeek)))
            {
                this.CalendarTableLayoutPanel.Controls.Add(new DayOfWeekButton(da,m_CritGui),i,0);
                ColFromDayOfWeek[da] = i;
                i++;

            }
        }

        private void PopulateTime()
        {
            DateTimeFormatInfo datetimfor = new DateTimeFormatInfo();
            this.AMLabel.Text = datetimfor.AMDesignator;
            this.PMLabel.Text = datetimfor.PMDesignator;

            for (int ampm = 0; ampm <= 1; ampm++)
            {
                for (int i = 1; i <= 12; i++)
                {
                    this.hourLayoutPanel.Controls.Add(new HourButton(i,ampm, m_CritGui), ampm, i + 1);
                }
            }

            for (int min = 0; min < 60; min++)
            {
                this.hourLayoutPanel.Controls.Add(new MinuteButton(min, m_CritGui),  (min % 5) + 2, (int)(min / 5) + 2);
            }
        }

        private void PopulateDays(int year, int month)
        {
            Calendar Calenda = CultureInfo.InvariantCulture.Calendar;
            int row = 1;
            Array DaysOfWeek = Enum.GetValues(typeof(DayOfWeek));
            DayOfWeek LastDayOfWeek = (DayOfWeek)DaysOfWeek.GetValue(DaysOfWeek.Length - 1);
            for (int date = 1; date < Calenda.GetDaysInMonth(year, month); date++)
            {
                DayOfWeek day = Calenda.GetDayOfWeek(new DateTime(year, month, date));
                this.CalendarTableLayoutPanel.Controls.Add(new DayOfMonthButton(date,m_CritGui), ColFromDayOfWeek[day], row);
                if (day == LastDayOfWeek)
                {
                    row++;
                }
            }

        }

        private void PopulateCalendar()
        {
            int row = 1;
            int col = 0;
            int daysinweek = Enum.GetValues(typeof(DayOfWeek)).Length;
            for (int date = 1; date < 31; date++)
            {
                this.CalendarTableLayoutPanel.Controls.Add(new DayOfMonthButton(date, m_CritGui), col, row);
                col++;
                if (date % daysinweek == 0)
                {
                    col = 0;
                    row++;
                }
            }

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.VScroll = true;
        }

        private void CalDataGridView_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.RowIndex >= VersionToShow.Count)
            {
                return;
            }
            switch (e.ColumnIndex)
            {
                case 0:
                    e.Value = VersionToShow[e.RowIndex].date.ToLongTimeString() + " " + VersionToShow[e.RowIndex].date.ToLongDateString();
                    break;

                case 1:
                    e.Value = "test version";
                    break;

                case 2:
                      e.Value = null;
                    break;

                case 3:
                    e.Value = "test notes";
                    break;

                case 4:
                  //  DataGridViewRow datrow2 = this.CalDataGridView.Rows[e.RowIndex];
                //    DataGridViewComboBoxCell data4 = datrow2.Cells["Changes"] as DataGridViewComboBoxCell;
                //    data4.Items.Add("test change1");
               //     data4.Items.Add("test change2");
               //     e.Value = data4;

                      
                    e.Value = "test version";
                    break;
                    
            }
        }

        private void CalDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}