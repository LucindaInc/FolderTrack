using System;
using System.Collections.Generic;
using System.Text;
using FolderTrack.Types;
using System.Diagnostics;
using System.Globalization;

namespace FolderTrackGuiTest1
{
    public class DataManager
    {
        public enum FromTypes
        {
            DayOfWeek,
            Month,
            Year,
            DayOfMonth,
            Hour,
            Minute,
            VersionName,
            UserVersion,
            ChangeType,
            Notes
        }

        public interface Status
        {
            void ReceiveStatus(string stat, bool done);
        }

        public Dictionary<DayOfWeek,List<VersionInfo>> VersionInfoFromDayOfWeek;
        public Dictionary<int, List<VersionInfo>> VersionInfoFromMonth;
        public Dictionary<int, List<VersionInfo>> VersionInfoFromYear;
        public Dictionary<int, List<VersionInfo>> VersionInfoFromDayOfMonth;
        public Dictionary<int, List<VersionInfo>> VersionInfoFromHour;
        public Dictionary<int, List<VersionInfo>> VersionInfoFromMinute;

        public Dictionary<string, VersionInfo> VersionInfoFromVersionName;
        public Dictionary<string, List<VersionInfo>> VersionInfoFromUserVersion;
        public Dictionary<ChangeType, List<VersionInfo>> VersionInfoFromChangeType;
        public Dictionary<string, List<VersionInfo>> VersionInfoFromChangeFile;
        public Dictionary<string, List<VersionInfo>> VersionInfoFromNotes;
        public SortedList<DateTime,VersionInfo> VersionList;

        public List<string> UserVersions;

        public DataManager()
        {
            VersionInfoFromDayOfWeek = new Dictionary<DayOfWeek, List<VersionInfo>>();
            VersionInfoFromMonth = new Dictionary<int, List<VersionInfo>>();
            VersionInfoFromYear = new Dictionary<int, List<VersionInfo>>();
            VersionInfoFromDayOfMonth = new Dictionary<int, List<VersionInfo>>();
            VersionInfoFromHour = new Dictionary<int, List<VersionInfo>>();
            VersionInfoFromMinute = new Dictionary<int, List<VersionInfo>>();

            VersionInfoFromVersionName = new Dictionary<string, VersionInfo>();
            VersionInfoFromUserVersion = new Dictionary<string, List<VersionInfo>>();
            VersionInfoFromChangeType = new Dictionary<ChangeType, List<VersionInfo>>();
            VersionInfoFromChangeFile = new Dictionary<string, List<VersionInfo>>();
            VersionInfoFromNotes = new Dictionary<string, List<VersionInfo>>();
            VersionList = new SortedList<DateTime, VersionInfo>();
        }

        public void ClearAll()
        {
            VersionInfoFromDayOfWeek.Clear();
            VersionInfoFromMonth.Clear();
            VersionInfoFromYear.Clear();
            VersionInfoFromDayOfMonth.Clear();
            VersionInfoFromHour.Clear();
            VersionInfoFromMinute.Clear();

            VersionInfoFromVersionName.Clear();
            VersionInfoFromUserVersion.Clear();
            VersionInfoFromChangeType.Clear();
            VersionInfoFromChangeFile.Clear();
            VersionInfoFromNotes.Clear();
            VersionList.Clear();
        }


        public void AddVersionInfo(List<VersionInfo> version)
        {
            foreach (VersionInfo vers in version)
            {
                AddVersionInfo(vers, true);
            }
        }

        public bool TryGetValue(string version, out VersionInfo versioninfo)
        {
            return VersionInfoFromVersionName.TryGetValue(version, out versioninfo);
        }

      

        public void AddVersionInfo(VersionInfo version, bool multiple)
        {
            VersionList.Add(version.Date,version);

            //Day of Week
            if (VersionInfoFromDayOfWeek.ContainsKey(version.date.DayOfWeek) == false)
            {
                VersionInfoFromDayOfWeek[version.date.DayOfWeek] = new List<VersionInfo>();
            }
            VersionInfoFromDayOfWeek[version.date.DayOfWeek].Add(version);

            //Month
            if (VersionInfoFromMonth.ContainsKey(version.date.Month) == false)
            {
                VersionInfoFromMonth[version.date.Month] = new List<VersionInfo>();
            }
            VersionInfoFromMonth[version.date.Month].Add(version);

            ///Year
            if (VersionInfoFromYear.ContainsKey(version.date.Year) == false)
            {
                VersionInfoFromYear[version.date.Year] = new List<VersionInfo>();
            }
            VersionInfoFromYear[version.date.Year].Add(version);

            //Day of month
            if (VersionInfoFromDayOfMonth.ContainsKey(version.date.Day) == false)
            {
                VersionInfoFromDayOfMonth[version.date.Day] = new List<VersionInfo>();
            }
            VersionInfoFromDayOfMonth[version.date.Day].Add(version);

            //Hour
            if (VersionInfoFromHour.ContainsKey(version.date.Hour) == false)
            {
                VersionInfoFromHour[version.date.Hour] = new List<VersionInfo>();
            }
            VersionInfoFromHour[version.date.Hour].Add(version);

            //Minute
            if (VersionInfoFromMinute.ContainsKey(version.date.Minute) == false)
            {
                VersionInfoFromMinute[version.date.Minute] = new List<VersionInfo>();
            }
            VersionInfoFromMinute[version.date.Minute].Add(version);

            //VersionName
            VersionInfoFromVersionName[version.versionName] = version;

            if (version.ChangesInVersion != null)
            {
                foreach (ChangeInstruction change in version.ChangesInVersion)
                {
                    if (VersionInfoFromChangeFile.ContainsKey(change.externalLocation) == false)
                    {
                        VersionInfoFromChangeFile[change.externalLocation] = new List<VersionInfo>();
                    }
                    VersionInfoFromChangeFile[change.externalLocation].Add(version);

                    if (VersionInfoFromChangeType.ContainsKey(change.change) == false)
                    {
                        VersionInfoFromChangeType[change.change] = new List<VersionInfo>();
                    }
                    VersionInfoFromChangeType[change.change].Add(version);
                }
            }
            if (version.UserVersionsThatContain != null && version.UserVersionsThatContain.Count > 0)
            {
                foreach (UserVersionStatus UserVersion in version.UserVersionsThatContain)
                {
                    if (VersionInfoFromUserVersion.ContainsKey(UserVersion.UserVersion) == false)
                    {
                        VersionInfoFromUserVersion[UserVersion.UserVersion] = new List<VersionInfo>();
                    }
                    VersionInfoFromUserVersion[UserVersion.UserVersion].Add(version);
                }
            }
            else
            {
                if (VersionInfoFromUserVersion.ContainsKey(String.Empty) == false)
                {
                    VersionInfoFromUserVersion[String.Empty] = new List<VersionInfo>();
                }
                VersionInfoFromUserVersion[String.Empty].Add(version);
            }



            if (version.FreeText != null)
            {
                if (VersionInfoFromNotes.ContainsKey(version.FreeText) == false)
                {
                    VersionInfoFromNotes[version.FreeText] = new List<VersionInfo>();
                }
                VersionInfoFromNotes[version.FreeText].Add(version);
            }
            else
            {
                if (VersionInfoFromNotes.ContainsKey(String.Empty) == false)
                {
                    VersionInfoFromNotes[String.Empty] = new List<VersionInfo>();
                }
                VersionInfoFromNotes[String.Empty].Add(version);
            }




        }

        public void UpdateRemove(VersionInfo versio)
        {
            VersionInfo outVersionInf;
            if (VersionInfoFromVersionName.TryGetValue(versio.versionName, out outVersionInf))
            {
                outVersionInf.Removed = versio.Removed;
            }
            //do not add it here if it is removed
            //it should be added through the normal way for add
            // versione and just update
            else if(versio.Removed == false)
            {
                AddVersionInfo(versio, false);
            }
        }

        public void UpDateDiscription(VersionInfo versio)
        {
            VersionInfo vers = VersionInfoFromVersionName[versio.versionName];
            List<VersionInfo> versIn;
            if (vers.freeText != null && VersionInfoFromNotes.TryGetValue(vers.freeText, out versIn))
            {
                versIn.Remove(vers);
                if (versIn.Count == 0)
                {
                    VersionInfoFromVersionName.Remove(vers.freeText);
                }
            }

            vers.freeText = versio.freeText;

            if (vers.FreeText != null)
            {
                if (VersionInfoFromNotes.ContainsKey(vers.FreeText) == false)
                {
                    VersionInfoFromNotes[vers.FreeText] = new List<VersionInfo>();
                }
                VersionInfoFromNotes[vers.FreeText].Add(vers);
            }
            else
            {
                if (VersionInfoFromNotes.ContainsKey(String.Empty) == false)
                {
                    VersionInfoFromNotes[String.Empty] = new List<VersionInfo>();
                }
                VersionInfoFromNotes[String.Empty].Add(vers);
            }
            
        }



        public bool VersionExist(string version)
        {
            return VersionInfoFromVersionName.ContainsKey(version);
        }

        public string[] getFiles()
        {
            lock (VersionInfoFromChangeFile)
            {
                int count = VersionInfoFromChangeFile.Keys.Count;
                if (count > 0)
                {
                    string[] ChangeFile;
                    //If there is a no userversion in here it is not included in this
                    //list so adjust the size to reflect that
                    if (VersionInfoFromChangeFile.ContainsKey(String.Empty) == true)
                    {
                        //The only user version is the no user version so therefore
                        if (count == 1)
                        {
                            return null;
                        }
                        ChangeFile = new string[count - 1];
                    }
                    else
                    {
                        ChangeFile = new string[count];
                    }
                    int i = 0;
                    foreach (KeyValuePair<string, List<VersionInfo>> k in VersionInfoFromChangeFile)
                    {
                        if (k.Key.Equals(String.Empty) == false)
                        {
                            ChangeFile[i] = k.Key;
                            i++;
                        }
                    }

                    return ChangeFile;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<string> getUserVersions()
        {
            List<string> ReturnList = new List<string>();
            if (UserVersions == null)
            {
                return ReturnList;
            }
            ReturnList.AddRange(UserVersions);
            return ReturnList;
        }

        public void setUserVersions(List<string> UserVersions)
        {
            this.UserVersions = UserVersions;
        }

        public void addUserVersion(List<string> UserVers)
        {
            if (UserVersions != null)
            {
                foreach (string uv in UserVers)
                {
                    if (uv != null)
                    {
                        UserVersions.AddRange(UserVers);
                    }
                }
            }
        }

        public int[] GetYears()
        {
            int[] YearArray = new int[VersionInfoFromYear.Keys.Count];
            VersionInfoFromYear.Keys.CopyTo(YearArray, 0);
            return YearArray;
        }



        public List<VersionInfo> VersionInfoFromCriteria(VersionInfoSearchCriteria crit,Status stat)
        {
            string statusStr="";
            string CstatusStr = "";
            int cn=0;
            Stopwatch stopw = new Stopwatch();
            List<VersionInfo> ReturnListI = new List<VersionInfo>();
            List<VersionInfo> ReturnList = new List<VersionInfo>();
            List<VersionInfo> util;
            Dictionary<string, VersionInfo> RetuDic = new Dictionary<string, VersionInfo>();



            stopw.Reset();
            stopw.Start();
            foreach (DayOfWeek dayofweek in crit.DaysOfWeek)
            {
                cn = 0;
                
                if (VersionInfoFromDayOfWeek.TryGetValue(dayofweek, out util))
                {
                    foreach (VersionInfo vers in util)
                    {
                        
                            cn++;
                        
                        RetuDic[vers.versionName] = vers;
                        if (stopw.ElapsedMilliseconds > 250)
                        {
                            CstatusStr = "Added "+cn+" versions created on " + dayofweek+ " so far";
                            stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                            stopw.Reset();
                            stopw.Start();
                        }
                    }
                }
                statusStr += '\n'+"Added " + cn + " versions created on " + dayofweek;
            }

            stopw.Reset();
            stopw.Start();
            foreach (ChangeType changetype in crit.ChangeTypes)
            {
                cn = 0;

                if (VersionInfoFromChangeType.TryGetValue(changetype, out util))
                {
                    foreach (VersionInfo vers in util)
                    {
                        cn++;

                        RetuDic[vers.versionName] = vers;
                        if (stopw.ElapsedMilliseconds > 250)
                        {
                            CstatusStr = "Added " + cn + " versions created because of a " + changetype + " so far";
                            stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                            stopw.Reset();
                            stopw.Start();
                        }
                    }
                }
                statusStr += '\n'+"Added " + cn + " versions created because of a " + changetype;
            }

            stopw.Reset();
            stopw.Start();
            foreach (string changefile in crit.ChangeFile)
            {
                cn = 0;
                if (VersionInfoFromChangeFile[changefile] != null)
                {
                    foreach (VersionInfo vers in VersionInfoFromChangeFile[changefile])
                    {
                        cn++;

                        RetuDic[vers.versionName] = vers;
                        if (stopw.ElapsedMilliseconds > 250)
                        {
                            CstatusStr = "Added " + cn + " versions because of " + changefile + " so far";
                            stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                            stopw.Reset();
                            stopw.Start();
                        }
                    }
                }
                statusStr += '\n'+"Added " + cn + " versions because of " + changefile;
            }

            VersionInfo outVerIn;
            stopw.Reset();
            stopw.Start();
            foreach (string versionName in crit.VersionName)
            {
                cn = 0;
                if (versionName.Contains("*") == false)
                {
                    if (VersionInfoFromVersionName.TryGetValue(versionName,out outVerIn))
                    {
                        cn++;

                        RetuDic[outVerIn.versionName] = outVerIn;
                        if (stopw.ElapsedMilliseconds > 250)
                        {
                            CstatusStr = "Added " + cn + " versions because of version name" + versionName + " so far";
                            stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                            stopw.Reset();
                            stopw.Start();
                        }
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, VersionInfo> k in VersionInfoFromVersionName)
                    {
                        if (Matches(versionName, k.Key))
                        {
                            cn++;

                            RetuDic[k.Value.versionName] = k.Value;
                            if (stopw.ElapsedMilliseconds > 250)
                            {
                                CstatusStr = "Added " + cn + " versions because of version name" + versionName + " so far";
                                stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                                stopw.Reset();
                                stopw.Start();
                            }
                        }
                    }

                }
                statusStr += '\n'+ "Added " + cn + " versions because of version name" + versionName;
            }

            stopw.Reset();
            stopw.Start();
            foreach (string discrip in crit.Notes)
            {
                cn = 0;
                foreach (KeyValuePair<string, List<VersionInfo>> k in VersionInfoFromNotes)
                {
                    if (k.Key.Contains(discrip) == true)
                    {
                        foreach (VersionInfo ve in k.Value)
                        {
                            cn++;

                            RetuDic[ve.versionName] = ve;
                            if (stopw.ElapsedMilliseconds > 250)
                            {
                                CstatusStr = "Added " + cn + " versions because notes contain\"" + discrip + "\" so far";
                                stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                                stopw.Reset();
                                stopw.Start();
                            }
                        }
                    }
                }
                statusStr += '\n'+ "Added " + cn + " versions because notes contain\"" + discrip + "\"";
            }

            AddItems(RetuDic, crit.DaysOfMonth, VersionInfoFromDayOfMonth, FromTypes.DayOfMonth,stat,statusStr);
            AddItems(RetuDic, crit.Hours, VersionInfoFromHour, FromTypes.Hour, stat, statusStr);
            AddItems(RetuDic, crit.Minutes, VersionInfoFromMinute, FromTypes.Minute, stat, statusStr);
            AddItems(RetuDic, crit.Months, VersionInfoFromMonth, FromTypes.Month, stat, statusStr);
            AddItems(RetuDic, crit.Years, VersionInfoFromYear, FromTypes.Year, stat, statusStr);
            AddItems(RetuDic, crit.UserVersions, VersionInfoFromUserVersion, FromTypes.UserVersion, stat, statusStr);
            

            if (crit.IncludeAllUserVersions == true)
            {
                stopw.Reset();
                stopw.Start();
                cn = 0;
                foreach (KeyValuePair<string, List<VersionInfo>> k in VersionInfoFromUserVersion)
                {
                    
                    //If list has a key that has a user version
                    if (k.Key.Equals(String.Empty) == false)
                    {
                        foreach (VersionInfo ve in k.Value)
                        {
                            cn++;

                            RetuDic[ve.versionName] = ve;
                            if (stopw.ElapsedMilliseconds > 250)
                            {
                                CstatusStr = "Added " + cn + " versions so far because they have user versions";
                                stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                                stopw.Reset();
                                stopw.Start();
                            }
                        }

                    }
                    statusStr += '\n'+"Added " + cn + " versions because they have user versions";
                }

            }

            if (crit.IncludeNoUserVersions == true)
            {
                List<VersionInfo> NoUserVersion;
                stopw.Reset();
                stopw.Start();
                cn = 0;
                if (VersionInfoFromUserVersion.TryGetValue(String.Empty, out NoUserVersion))
                {
                    foreach (VersionInfo ve in NoUserVersion)
                    {
                        cn++;

                        RetuDic[ve.versionName] = ve;
                        if (stopw.ElapsedMilliseconds > 250)
                        {
                            CstatusStr = "Added " + cn + " versions so far because they do not have user versions";
                            stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                            stopw.Reset();
                            stopw.Start();
                        }
                    }
                    statusStr += '\n' + "Added " + cn + " versions because they do not have user versions";
                }
            }

            if (crit.IncludeDiscription == true)
            {
                stopw.Reset();
                stopw.Start();
                cn = 0;
                foreach (KeyValuePair<string, List<VersionInfo>> k in VersionInfoFromNotes)
                {
                    //If list has a key that has a user version
                    if (k.Key.Equals(String.Empty) == false)
                    {
                        foreach (VersionInfo ve in k.Value)
                        {
                            cn++;

                            RetuDic[ve.versionName] = ve;
                            if (stopw.ElapsedMilliseconds > 250)
                            {
                                CstatusStr = "Added " + cn + " versions so far because they have a description";
                                stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                                stopw.Reset();
                                stopw.Start();
                            }
                        }

                    }
                    statusStr += '\n'+"Added " + cn + " versions because they have a description";
                 }
                }
            

            if (crit.IncludeNoDiscription == true)
            {
                List<VersionInfo> NoDiscription;
                cn = 0;
                stopw.Reset();
                stopw.Start();
                if (VersionInfoFromUserVersion.TryGetValue(String.Empty, out NoDiscription))
                {
                    foreach (VersionInfo ve in NoDiscription)
                    {
                        cn++;

                        RetuDic[ve.versionName] = ve;
                        if (stopw.ElapsedMilliseconds > 250)
                        {
                            CstatusStr = "Added " + cn + " versions so far because they do not have a description";
                            stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                            stopw.Reset();
                            stopw.Start();
                        }
                    }
                    statusStr += '\n' + "Added " + cn + " versions because they do not have a description";
                }
            }



            ReturnListI.AddRange(RetuDic.Values);
            //used to get out of for each loops
            bool breakAndContinue;
            stopw.Reset();
            stopw.Start();
            cn = 0;
            foreach (VersionInfo vers in ReturnListI)
            {
                if (stopw.ElapsedMilliseconds > 250)
                {
                    CstatusStr = "Removed " + cn + " versions so far because they were exlcuded";
                    stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                    stopw.Reset();
                    stopw.Start();
                }
               
                if (crit.DaysOfWeekR != null && crit.DaysOfWeekR.Contains(vers.date.DayOfWeek) == true)
                {
                    cn++;
                    continue;
                }

                if (crit.DaysOfMonthR != null && crit.DaysOfMonthR.Contains(vers.date.Day) == true)
                {
                    cn++;
                    continue;
                }

                if (crit.HoursR != null && crit.HoursR.Contains(vers.date.Hour) == true)
                {
                    cn++;
                    continue;
                }

                if (crit.MonthsR != null && crit.MonthsR.Contains(vers.date.Month) == true)
                {
                    cn++;
                    continue;
                }

                if (crit.YearsR != null && crit.YearsR.Contains(vers.date.Year) == true)
                {
                    cn++;
                    continue;
                }

                if (crit.MinutesR != null && crit.MinutesR.Contains(vers.date.Minute) == true)
                {
                    cn++;
                    continue;
                }

                if (vers.Removed == true && crit.includeRemove == false)
                {
                    cn++;
                    continue;
                }

                breakAndContinue = false;
                if (crit.VersionNameR != null)
                {
                    foreach (string versionName in crit.VersionNameR)
                    {
                        if (versionName.Contains("*") == false)
                        {
                            if (crit.VersionNameR.Contains(vers.versionName) == true)
                            {
                                breakAndContinue = true;
                                break;
                            }
                        }
                        else
                        {

                            if (Matches(versionName, vers.versionName))
                            {
                                    breakAndContinue = true;
                                    break;
                            }
                        }
                    }
                    if (breakAndContinue == true)
                    {
                        cn++;
                        continue;
                    }
                }


                breakAndContinue = false;
                if (crit.UserVersionsR != null)
                {
                    foreach (string userVersions in crit.UserVersionsR)
                    {
                        if (ContainsVersion(vers.UserVersionsThatContain,userVersions))
                        {
                            breakAndContinue = true;
                            break;
                        }
                    }
                    if (breakAndContinue == true)
                    {
                        cn++;
                        continue;
                    }

                }

                breakAndContinue = false;
                if (crit.ChangeTypesR!= null)
                {
                    foreach (ChangeType changetype in crit.ChangeTypesR)
                    {
                        foreach (ChangeInstruction changeinstructions in vers.changesInVersion)
                        {
                            if (changeinstructions.change == changetype)
                            {
                                breakAndContinue = true;
                                break;
                            }
                        }
                        if (breakAndContinue == true)
                        {
                            break;
                        }
                    }
                    if (breakAndContinue == true)
                    {
                        cn++;
                        continue;
                    }

                }

                breakAndContinue = false;
                if (crit.ChangeFileR != null)
                {
                    foreach (string changefile in crit.ChangeFileR)
                    {
                        foreach (ChangeInstruction changeinstructions in vers.changesInVersion)
                        {
                            if (changeinstructions.externalLocation.Equals(changefile))
                            {
                                breakAndContinue = true;
                                break;
                            }
                        }
                        if (breakAndContinue == true)
                        {
                            break;
                        }
                    }
                    if (breakAndContinue == true)
                    {
                        cn++;
                        continue;
                    }

                }

                if (RejectExclude(vers.date, crit) != null)
                {
                    cn++;
                    continue;
                }

                ReturnList.Add(vers);

            }
            statusStr += '\n' + "Removed " + cn + " versions because they were exlcuded";
            stat.ReceiveStatus(statusStr, true);
            return ReturnList;

            
        }


        public static bool ContainsVersion(List<UserVersionStatus> UserVerStaLi, string version)
        {
            foreach (UserVersionStatus usstat in UserVerStaLi)
            {
                if(usstat.UserVersion.Equals(version))
                {
                    return true;
                }
            }
            return false;
        }


        public static VersionInfoSearchCriteria.ExcludeTime RejectExclude(DateTime date, VersionInfoSearchCriteria crit)
        {
            bool year = false;
            bool hour = false;
            bool day = false;
            bool month = false;
            bool minute = false;

            foreach (VersionInfoSearchCriteria.ExcludeTime ex in crit.ExcludeTimes)
            {
                year = false;
                hour = false;
                day = false;
                month = false;
                minute = false;

                if (ex.year != -1)
                {
                    if (date.Year == ex.year)
                    {
                        year = true;
                    }
                }
                else
                {
                    year = true;
                }

                if (ex.day != -1)
                {
                    if (date.Day == ex.day)
                    {
                        day = true;
                    }
                }
                else
                {
                    day = true;
                }

                if (ex.hour != -1)
                {
                    if (date.Hour == ex.hour)
                    {
                        hour = true;
                    }
                }
                else
                {
                    hour = true;
                }

                if (ex.month != -1)
                {
                    if (date.Month == ex.month)
                    {
                        month = true;
                    }
                }
                else
                {
                    month = true;
                }

                if (ex.minute != -1)
                {
                    if (date.Minute == ex.minute)
                    {
                        minute = true;
                    }
                }
                else
                {
                    minute = true;
                }

                if (year && month && day && hour && minute)
                {
                    return ex;
                }
            }
            return null;
        }

        private void AddItems(Dictionary<string,VersionInfo> ReturnList, List<int> Criteria, Dictionary<int, List<VersionInfo>> Diction, FromTypes fromtypr, Status stat, string statusStr)
        {
            if (Criteria == null)
            {
                return;
            }
            int cn;
            string CstatusStr = "";
            Stopwatch st = new Stopwatch();
            st.Start();

            foreach (int item in Criteria)
            {
                cn = 0;
                if (Diction.ContainsKey(item) == true && Diction[item] != null)
                {
                    foreach (VersionInfo vers in Diction[item])
                    {
                    
                            cn++;
                    
                        ReturnList[vers.versionName] = vers;
                        if (st.ElapsedMilliseconds > 250)
                        {
                            CstatusStr = CreateString(cn, fromtypr, item);
                            CstatusStr += " so far";
                            stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                            st.Reset();
                            st.Start();
                        }
                    }
                }
                statusStr += '\n' + CreateString(cn, fromtypr, item);
            }
        }

        private void AddItems(Dictionary<string, VersionInfo> RetuDic, List<string> Criteria, Dictionary<string, List<VersionInfo>> Diction, FromTypes fromtypr, Status stat, string statusStr)
        {
            if (Criteria == null)
            {
                return;
            }
            int cn;
            string CstatusStr = "";
            Stopwatch st = new Stopwatch();
            st.Start();
            foreach (string item in Criteria)
            {
                cn = 0;
                if (Diction.ContainsKey(item) == true && Diction[item] != null)
                {
                    foreach (VersionInfo vers in Diction[item])
                    {
                        cn++;

                        RetuDic[vers.versionName] = vers;
                        if (st.ElapsedMilliseconds > 250)
                        {
                            CstatusStr = CreateString(cn, fromtypr, item);
                            CstatusStr += " so far";
                            stat.ReceiveStatus(CstatusStr + '\n' + statusStr,false);
                            st.Reset();
                            st.Start();
                        }
                    }
                }
                statusStr += '\n'+CreateString(cn, fromtypr, item);
            }
        }
        public string CreateString(int cn, FromTypes fromt, int crit)
        {
            switch (fromt)
            {
                case FromTypes.DayOfMonth:
                    return "Added " + cn + " versions that were created on day " + crit + " of the month";
                case FromTypes.Hour:
                    return "Added " + cn + " versions that were created at "+Util.intToTimeStr(crit);
                case FromTypes.Month:
                    return "Added " + cn + " versions that were created in " + DateTimeFormatInfo.CurrentInfo.GetMonthName(crit) ;
                case FromTypes.Year:
                    return "Added " + cn + " versions that were created in " + crit;
                default:
                    throw new Exception("Function CreateString 'int crit)' has no string for " + fromt.ToString());
            }
        }



        public string CreateString(int cn, FromTypes fromt, string crit)
        {
            switch (fromt)
            {
                case FromTypes.UserVersion:
                    return "Added "+cn+" versions because the contain user version "+crit;
                default:
                    throw new Exception("Function CreateString 'string crit)' has no string for " + fromt.ToString());
            }
        }

        public static bool Matches(string pat, string test)
        {
            string[] toks = pat.Split('*');

            //make sure that first tok matches begining and the for loops handles rest
            if (toks.Length > 0 && test.StartsWith(toks[0]) == false)
            {
                return false;
            }

            int start = 0;

            foreach (string tern in toks)
            {
                start = test.IndexOf(tern, start);
                if (start == -1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
