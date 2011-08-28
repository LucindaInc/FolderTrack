using System;
using System.Collections.Generic;
using System.Text;
using FolderTrack.Types;

namespace FolderTrackGuiTest1
{
    public class VersionInfoSearchCriteria
    {

        public List<DayOfWeek> DaysOfWeek;
        public List<DayOfWeek> DaysOfWeekR;

        public List<int> Months;
        public List<int> MonthsR;

        public List<int> DaysOfMonth;
        public List<int> DaysOfMonthR;

        public List<int> Years;
        public List<int> YearsR;

        public List<int> Hours;
        public List<int> HoursR;

        public List<int> Minutes;
        public List<int> MinutesR;

        public List<string> UserVersions;
        public List<string> UserVersionsR;

        public List<ChangeType> ChangeTypes;
        public List<ChangeType> ChangeTypesR;

        public List<string> VersionInfo;
        public List<string> VersionInfoR;

        public List<string> VersionName;
        public List<string> VersionNameR;

        public List<string> ChangeFile;
        public List<string> ChangeFileR;

        public List<string> Notes;
        public List<string> NotesR;

        public bool includeRemove;


        public class ExcludeTime
        {
            public ExcludeTime(int hour, int minute, int year, int month, int day)
            {
                this.hour = hour;
                this.minute = minute;
                this.year = year;
                this.month = month;
                this.day = day;
            }

            public int hour;
            public int minute;
            public int year;
            public int month;
            public int day;
        }

        public List<ExcludeTime> ExcludeTimes;

        //Include all versions that contain a user version
        public bool IncludeAllUserVersions;

        //Include all versions that do not contain a user version
        public bool IncludeNoUserVersions;

        //Include all that have discription
        public bool IncludeDiscription;

        //Include all versions that do not have discription
        public bool IncludeNoDiscription;

        public VersionInfoSearchCriteria()
        {
            DaysOfWeek = new List<DayOfWeek>();
            DaysOfWeekR = new List<DayOfWeek>();

            Months = new List<int>();
            MonthsR = new List<int>();

            DaysOfMonth = new List<int>();
            DaysOfMonthR = new List<int>();

            Years = new List<int>();
            YearsR = new List<int>();

            Hours = new List<int>();
            HoursR = new List<int>();

            Minutes = new List<int>();
            MinutesR = new List<int>();

            UserVersions = new List<string>();
            UserVersionsR = new List<string>();

            ChangeTypes = new List<ChangeType>();
            ChangeTypesR = new List<ChangeType>();

            ChangeFile = new List<string>();
            ChangeFileR = new List<string>();

            VersionInfo = new List<string>();
            VersionInfoR = new List<string>();

            VersionName = new List<string>();
            VersionNameR = new List<string>();

            Notes = new List<string>();
            NotesR = new List<string>();

            ExcludeTimes = new List<ExcludeTime>();
        }



        public void AddNotes(string note, bool reject)
        {
            lock (Notes)
            {
                if (reject == true)
                {
                    Notes.Add(note);
                }
                else
                {
                    NotesR.Add(note);
                }
            }
        }

        public void RemoveNotes(string changefile)
        {
            lock (Notes)
            {
                Notes.Remove(changefile);
                NotesR.Remove(changefile);
            }
        }

        public void AddVersionName(string versionName, bool reject)
        {
            lock (VersionName)
            {
                if (reject == true)
                {
                    VersionName.Add(versionName);
                }
                else
                {
                    VersionNameR.Add(versionName);

                }
            }
        }

        public void RemoveVersionName(string versionName)
        {
            lock (VersionName)
            {
                VersionName.Remove(versionName);
                VersionNameR.Remove(versionName);
            }
        }


        public void AddChangeFile(string changefile, bool reject)
        {
            lock (ChangeFile)
            {
                if (reject == true)
                {
                    ChangeFile.Add(changefile);
                }
                else
                {
                    ChangeFileR.Add(changefile);
                }
            }
        }

        public void RemoveChangeFile(string changefile)
        {
            lock (ChangeFile)
            {
                ChangeFile.Remove(changefile);
                ChangeFileR.Remove(changefile);
            }
        }
         


        public void AddChangeType(ChangeType changetype, bool reject)
        {
            lock (ChangeTypes)
            {
                if (reject == true)
                {
                    ChangeTypesR.Add(changetype);
                }
                else
                {
                    ChangeTypes.Add(changetype);
                }
            }
        }

        public void RemoveChangeType(ChangeType changetype)
        {
            lock (ChangeTypes)
            {
                ChangeTypes.Remove(changetype);
                ChangeTypesR.Remove(changetype);
            }
        }



        public void AddUserVersion(string userversion, bool reject)
        {
            lock (UserVersions)
            {
                if (reject == true)
                {
                    UserVersionsR.Add(userversion);
                }
                else
                {
                    UserVersions.Add(userversion);
                }
            }
        }

        public void RemoveUserVersion(string userversion)
        {
            lock (UserVersions)
            {
                UserVersions.Remove(userversion);
                UserVersionsR.Remove(userversion);
            }
        }

        public void AddDayOfWeek(DayOfWeek dayofweek, bool reject)
        {
            lock (DaysOfWeek)
            {
                if (reject == true)
                {
                    DaysOfWeek.Remove(dayofweek);
                    if (DaysOfWeekR.Contains(dayofweek) == false)
                    {
                        DaysOfWeekR.Add(dayofweek);
                    }
                }
                else
                {
                    DaysOfWeekR.Remove(dayofweek);
                    if (DaysOfWeek.Contains(dayofweek) == false)
                    {
                        DaysOfWeek.Add(dayofweek);
                    }
                }
            }
        }

        public void RemoveDayOfWeek(DayOfWeek dayofweek)
        {
            lock (DaysOfWeek)
            {
                DaysOfWeek.Remove(dayofweek);
                DaysOfWeekR.Remove(dayofweek);
            }
        }

        public void AddMonth(int month, bool reject)
        {
            lock (Months)
            {
                if (reject == true)
                {
                    Months.Remove(month);
                    if (MonthsR.Contains(month) == false)
                    {
                        MonthsR.Add(month);
                    }
                }
                else
                {
                    MonthsR.Remove(month);
                    if (Months.Contains(month) == false)
                    {
                        Months.Add(month);
                    }
                }
            }
        }

        public void RemoveMonth(int month)
        {
            lock (Months)
            {
                Months.Remove(month);
                MonthsR.Remove(month);
            }
        }

        public void AddDaysOfMonth(int daysofmonth, bool reject)
        {
            lock (DaysOfMonth)
            {
                if (reject == true)
                {
                    DaysOfMonth.Remove(daysofmonth);
                    if (DaysOfMonthR.Contains(daysofmonth) == false)
                    {
                        DaysOfMonthR.Add(daysofmonth);
                    }
                }
                else
                {
                    DaysOfMonthR.Remove(daysofmonth);
                    if (DaysOfMonth.Contains(daysofmonth) == false)
                    {
                        DaysOfMonth.Add(daysofmonth);
                    }
                }
            }
        }

        public void RemoveDaysOfMonth(int daysofmonth)
        {
            lock (DaysOfMonth)
            {
                DaysOfMonth.Remove(daysofmonth);
                DaysOfMonthR.Remove(daysofmonth);
            }
        }

        public void AddYears(int years, bool reject)
        {
            lock (Years)
            {
                if (reject == true)
                {
                    Years.Remove(years);
                    if (YearsR.Contains(years) == false)
                    {
                        YearsR.Add(years);
                    }
                }
                else
                {
                    YearsR.Remove(years);
                    if (Years.Contains(years) == false)
                    {
                        Years.Add(years);
                    }
                }
            }
        }

        public void RemoveYears(int years)
        {
            lock (Years)
            {
                Years.Remove(years);
                YearsR.Remove(years);
            }
        }

        public void AddHours(int hours, bool reject)
        {
            lock (Hours)
            {
                if (reject == true)
                {
                    Hours.Remove(hours);
                    if (HoursR.Contains(hours) == false)
                    {
                        HoursR.Add(hours);
                    }
                }
                else
                {
                    HoursR.Remove(hours);
                    if (Hours.Contains(hours) == false)
                    {
                        Hours.Add(hours);
                    }
                }
            }
        }

        public void RemoveHours(int hours)
        {
            lock (Hours)
            {
                Hours.Remove(hours);
                HoursR.Remove(hours);
            }
        }

        public void AddMinutes(int minutes, bool reject)
        {
            lock (Minutes)
            {
                if (reject == true)
                {
                    Minutes.Remove(minutes);
                    if (MinutesR.Contains(minutes) == false)
                    {
                        MinutesR.Add(minutes);
                    }
                }
                else
                {
                    MinutesR.Remove(minutes);
                    if (Minutes.Contains(minutes) == false)
                    {
                        Minutes.Add(minutes);
                    }
                }
            }
        }

        public void RemoveMinutes(int minutes)
        {
            lock (Minutes)
            {
                Minutes.Remove(minutes);
                MinutesR.Remove(minutes);
            }
        }

    }
}
