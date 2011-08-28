using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;


namespace FolderTrackGuiTest1
{
    class CalendarText
    {
        SetCriteris CritSet;

        public CalendarText(SetCriteris critset, int [] yearslist)
        {
            SetDictionarys();
            CritSet = critset;
            this.YearsList = yearslist;
        }

        public int [] YearsList;

        static Dictionary<string, int> DayOfMonthDic = new Dictionary<string, int>();
        static Dictionary<string, int> MonthOfYearDic = new Dictionary<string, int>();
        static Dictionary<string, DayOfWeek> DayOfWeekDic = new Dictionary<string, DayOfWeek>();
        static Dictionary<string, int> NumbersFromNames = new Dictionary<string, int>();
        static Dictionary<string, catTypes> CatFromNames = new Dictionary<string, catTypes>();
        Timer timer;
        List<catTypes> CaledTypes = new List<catTypes>();
        VersionInfoSearchCriteria crit;

        public static string[] timeIndicator = {"am","pm"};

        public static string[] date = { "st", "nd", "th", "rd" };

        public static string[] oclock = { "o'clock","oclock","clock","o","'","o'"};

        string[] rollin = { "hour","hours","minute",
            "minutes","day","days","year","years"};



        string[] rolling = {"today","last","this","yesterday"
        };

        string[] daysofweeks = {"monday","tuesday","wedensday","thursday","friday",
            "saturday","sunday"
        };

        string[] month = {"janurary","february","march","april","may","june","july",
            "august","september","october","november","december"
        };

        string [] includers = {"exclude", "include","every" , "all"};

        public enum catTypes
        {
            Month,
            Year,
            DayOM,
            Minute,
            Hour,
            DayOW,
            Week,
            Day

        };

        public enum RollonTypes
        {
            Yesterday,
            today,
            last
        }

        public class RollingInfo
        {
            public RollonTypes RolType;
            public catTypes lastType;
            public int lastAmount;
            VersionInfoSearchCriteria crit;
        }


        public static void SetDictionarys()
        {
            DayOfMonthDic["first"] = 1;
            DayOfMonthDic["second"] = 2;
            DayOfMonthDic["third"] = 3;
            DayOfMonthDic["forth"] = 4;
            DayOfMonthDic["fifth"] = 5;
            DayOfMonthDic["sixth"] = 6;
            DayOfMonthDic["seventh"] = 7;
            DayOfMonthDic["eighth"] = 8;
            DayOfMonthDic["ninth"] = 9;
            DayOfMonthDic["tenth"] =  10;
            DayOfMonthDic["eleventh"] = 11;
            DayOfMonthDic["twelfth"] = 12;
            DayOfMonthDic["thirteenth"] = 13;
            DayOfMonthDic["fourteenth"] = 14;
            DayOfMonthDic["fifteenth"] = 15;
            DayOfMonthDic["sixteenth"] = 16;
            DayOfMonthDic["seventeenth"] = 17;
            DayOfMonthDic["eighteenth"] = 18;
            DayOfMonthDic["nineteenth"] = 19;
            DayOfMonthDic["twentieth"] = 20;
            DayOfMonthDic["twentyfirst"] = 21;
            DayOfMonthDic["twentysecond"] = 22;
            DayOfMonthDic[ "twentythird"] = 23;
            DayOfMonthDic["twentyfourth"] = 24;
            DayOfMonthDic["twentyfifth"] = 25;
            DayOfMonthDic["twentysixth"] = 26;
            DayOfMonthDic["twentyseventh"] = 27;
            DayOfMonthDic["twentyeighth"] = 28;
            DayOfMonthDic["twentyninth"] = 29;
            DayOfMonthDic["thirtieth"] = 30;
            DayOfMonthDic["thirtyfirst"] = 31;



            MonthOfYearDic["janurary"] = 1;
            MonthOfYearDic["february"] = 2;
            MonthOfYearDic["march"] = 3;
            MonthOfYearDic["april"] = 4;
            MonthOfYearDic["may"] = 5;
            MonthOfYearDic["june"] = 6;
            MonthOfYearDic["july"] = 7;
            MonthOfYearDic["august"] = 8;
            MonthOfYearDic["september"] = 9;
            MonthOfYearDic["october"] = 10;
            MonthOfYearDic["november"] = 11;
            MonthOfYearDic["december"] = 12;


            DayOfWeekDic["monday"] = DayOfWeek.Monday;
            DayOfWeekDic["tuesday"] = DayOfWeek.Tuesday;
            DayOfWeekDic["wedensday"] = DayOfWeek.Wednesday;
            DayOfWeekDic["thursday"] = DayOfWeek.Thursday;
            DayOfWeekDic["friday"] = DayOfWeek.Friday;
            DayOfWeekDic["saturday"] = DayOfWeek.Saturday;
            DayOfWeekDic["sunday"] = DayOfWeek.Sunday;

            NumbersFromNames["one"] = 1;
            NumbersFromNames["two"] = 2;
            NumbersFromNames["three"] = 3;
            NumbersFromNames["four"] =4;
            NumbersFromNames["five"] =5;
            NumbersFromNames["six"]=6;
            NumbersFromNames["seven"]=7;
            NumbersFromNames["eight"]=8;
            NumbersFromNames["nine"]=9;
            NumbersFromNames["ten"]= 10;
            NumbersFromNames["elevan"]= 11;
            NumbersFromNames["twelve"]= 12;

            CatFromNames["week"] = catTypes.Week;
            CatFromNames["weeks"] = catTypes.Week;
            CatFromNames["year"] = catTypes.Year;
            CatFromNames["years"] = catTypes.Year;
            CatFromNames["day"] = catTypes.Day;
            CatFromNames["days"] = catTypes.Day;
            CatFromNames["month"] = catTypes.Month;
            CatFromNames["months"] = catTypes.Month;
            CatFromNames["hour"] = catTypes.Hour;
            CatFromNames["hours"] = catTypes.Hour;
            CatFromNames["minute"] = catTypes.Minute;
            CatFromNames["minutes"] = catTypes.Minute;

        }

       

        public VersionInfoSearchCriteria ProcessString(string text)
        {
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer = null;
            }
            crit = new VersionInfoSearchCriteria();

            bool changed = false;

              string fixe = addIndicaToText(text);

            string[] tokens = fixe.Split(' ');
            int number;
            DayOfWeek dayOfweek;
            int []hour;
            int minute;
            bool last;
            string te;
            string tok;
            RollingInfo rollIn;
            for(int i = 0 ; i < tokens.Length; i++)
            {
                tok = tokens[i];
            
                if (isDayOfMonth(tok, out number))
                {
                //    if (CaledTypes.Contains(catTypes.DayOM) == false)
               //     {
               //         CaledTypes.Add(catTypes.DayOM);
                        Exclude(crit, catTypes.DayOM);
               //     }
                    crit.RemoveDaysOfMonth(number);
                    crit.AddDaysOfMonth(number,false);
                    changed = true;
                    continue;
                }

                if (isDayOfWeek(tok, out dayOfweek))
                {
                //    if (CaledTypes.Contains(catTypes.DayOW) == false)
                //    {
                //        CaledTypes.Add(catTypes.DayOW);
                        Exclude(crit, catTypes.DayOW);
                //    }
                    crit.RemoveDayOfWeek(dayOfweek);
                    crit.AddDayOfWeek(dayOfweek,false);
                    changed = true;
                    continue;
                }

                if (isMonth(tok, out number))
                {
          //          if (CaledTypes.Contains(catTypes.Month) == false)
            //        {
           //             CaledTypes.Add(catTypes.Month);
                        Exclude(crit, catTypes.Month);
           //         }
                    crit.RemoveMonth(number);
                    crit.AddMonth(number,false);
                    changed = true;
                    continue;
                }

                if (isTime(tok, out hour, out minute))
                {
                    if (tokens.Length > i + 1 && isTimeAMPM(tokens[i + 1]))
                    {
                        if (tokens[i + 1].Equals("am"))
                        {
                            foreach (int h in hour)
                            {
                                if (h < 12)
                                {
                               //     if (CaledTypes.Contains(catTypes.Hour) == false)
                                //    {
                                //        CaledTypes.Add(catTypes.Hour);
                                        Exclude(crit, catTypes.Hour);
                               //     }
                                    crit.RemoveHours(h);
                                    crit.AddHours(h, false);
                                    changed = true;
                                }
                            }
                        }
                        else
                        {
                            foreach (int h in hour)
                            {
                                if (h >= 12)
                                {
                              //      if (CaledTypes.Contains(catTypes.Hour) == false)
                              //      {
                               //         CaledTypes.Add(catTypes.Hour);
                                        Exclude(crit, catTypes.Hour);
                             //       }
                                    crit.RemoveHours(h);
                                    crit.AddHours(h, false);
                                    changed = true;
                                }
                            }
                        }


                    }
                    else
                    {
                        foreach (int h in hour)
                        {
                            if (h > -1 && h < 24)
                            {
                        //        if (CaledTypes.Contains(catTypes.Hour) == false)
                        //        {
                        //            CaledTypes.Add(catTypes.Hour);
                                    Exclude(crit, catTypes.Hour);
                         //       }
                                crit.RemoveHours(h);
                                crit.AddHours(h, false);
                                changed = true;
                            }
                        }
                    }

                    if (minute > 0)
                    {
                   //     if (CaledTypes.Contains(catTypes.Minute) == false)
                  //      {
                    //        CaledTypes.Add(catTypes.Minute);
                            Exclude(crit, catTypes.Minute);
                //        }
                        crit.RemoveMinutes(minute);
                        crit.AddMinutes(minute, false);
                        changed = true;
                    }
                    continue;
                }

                if (isRolling(tok, out rollIn))
                {
                    
                        int dueTime = 60 - DateTime.Now.Second;
                        if (dueTime == 60)
                        {
                            dueTime = 0;
                        }
                        else
                        {
                            handleRolling(rollIn);
                        }
                        timer = new Timer(handleRolling, rollIn, dueTime * 1000, 60000);
                    
                }
            }
            if (changed)
            {
                return crit;
            }
            else
            {
                return null;
            }

        }



        public void handleRolling(object RollinIn)
        {
            crit = new VersionInfoSearchCriteria();
            bool change = false;
            RollingInfo roi = (RollingInfo)RollinIn;
            DateTime date = DateTime.Now;
            CaledTypes.Clear();

            int ohour;
            int oday;
            int omonth;
            int oyear;

            if (roi.RolType == RollonTypes.today || roi.RolType == RollonTypes.Yesterday)
            {
                if (roi.RolType == RollonTypes.Yesterday)
                {
                    date = date.Subtract(TimeSpan.FromDays(1));
                }

                if (crit.DaysOfMonth.Contains(date.Day) == false ||
                    crit.DaysOfMonthR.Contains(date.Day) == true)
                {
                    if (CaledTypes.Contains(catTypes.DayOM) == false)
                    {
                        CaledTypes.Add(catTypes.DayOM);
                        Exclude(crit, catTypes.DayOM);
                    }

                    crit.RemoveDaysOfMonth(date.Day);
                    crit.AddDaysOfMonth(date.Day, false);
                    change = true;
                }

                if (crit.Months.Contains(date.Month) == false ||
                   crit.MonthsR.Contains(date.Month) == true)
                {
                    if (CaledTypes.Contains(catTypes.Month) == false)
                    {
                        CaledTypes.Add(catTypes.Month);
                        Exclude(crit, catTypes.Month);
                    }
                    crit.RemoveMonth(date.Month);
                    crit.AddMonth(date.Month, false);
                    change = true;
                }

                if (crit.Years.Contains(date.Year) == false ||
                    crit.YearsR.Contains(date.Year) == true)
                {
                    if (CaledTypes.Contains(catTypes.Year) == false)
                    {
                        CaledTypes.Add(catTypes.Year);
                        Exclude(crit, catTypes.Year);
                    }
                    crit.RemoveYears(date.Year);
                    crit.AddYears(date.Year, false);
                    change = true;
                }

                if (change == true)
                {

                }

            }
            else
            {

                TimeSpan span = new TimeSpan();
                int m = 0;
                int y = 0;

                int mnt;
                int yer;

                int lastamount = roi.lastAmount;
                catTypes rcatTyp = roi.lastType;
                switch (roi.lastType)
                {
                    case catTypes.Minute:
                        span = new TimeSpan(0, 1, 0);
                        break;

                    case catTypes.Hour:
                        span = new TimeSpan(1, 0, 0);
                        break;

                    case catTypes.Day:
                        span = new TimeSpan(1, 0, 0, 0);
                        break;

                    case catTypes.Week:
                        lastamount *= 7;
                        rcatTyp = catTypes.Day;
                        span = new TimeSpan(1, 0, 0, 0);
                        break;

                    case catTypes.Month:
                        m = 1;
                        break;

                    case catTypes.Year:
                        y = 1;
                        break;

                }

                date = DateTime.Now;
                mnt = date.Month;
                yer = date.Year;

                for (int i = 0; i <= lastamount; i++)
                {


                    if (rcatTyp == catTypes.Minute)
                    {
                        if (crit.Minutes.Contains(date.Minute) == false ||
                            crit.MinutesR.Contains(date.Minute) == true)
                        {
                            if (CaledTypes.Contains(catTypes.Minute) == false)
                            {
                                CaledTypes.Add(catTypes.Minute);
                                Exclude(crit, catTypes.Minute);
                            }
                            crit.RemoveMinutes(date.Minute);
                            crit.AddMinutes(date.Minute, false);
                            change = true;
                        }
                    }

                    if (rcatTyp == catTypes.Minute ||
                        rcatTyp == catTypes.Hour)
                    {
                        if (crit.Hours.Contains(date.Hour) == false ||
                            crit.HoursR.Contains(date.Hour) == true)
                        {
                            if (CaledTypes.Contains(catTypes.Hour) == false)
                            {
                                CaledTypes.Add(catTypes.Hour);
                                Exclude(crit, catTypes.Hour);
                            }
                            crit.RemoveHours(date.Hour);
                            crit.AddHours(date.Hour, false);
                            change = true;
                        }
                    }

                    if (rcatTyp == catTypes.Minute ||
                        rcatTyp == catTypes.Hour ||
                        rcatTyp == catTypes.Day)
                    {
                        if (CaledTypes.Contains(catTypes.DayOM) == false)
                        {
                            CaledTypes.Add(catTypes.DayOM);
                            Exclude(crit, catTypes.DayOM);
                        }


                        if (crit.DaysOfMonth.Contains(date.Day) == false ||
                            crit.DaysOfMonthR.Contains(date.Day) == true)
                        {
                            crit.RemoveDaysOfMonth(date.Day);
                            crit.AddDaysOfMonth(date.Day, false);
                            change = true;
                        }
                    }

                    if (rcatTyp != catTypes.Year)
                    {
                        yer = date.Year;
                    }

                    if (crit.Years.Contains(yer) == false ||
                        crit.YearsR.Contains(yer) == true)
                    {
                        if (CaledTypes.Contains(catTypes.Year) == false)
                        {
                            CaledTypes.Add(catTypes.Year);
                            Exclude(crit, catTypes.Year);
                        }
                        crit.RemoveYears(yer);
                        crit.AddYears(yer, false);
                        change = true;
                    }

                    if (rcatTyp == catTypes.Year)
                    {
                        yer--;
                    }

                    if (rcatTyp == catTypes.Minute ||
                        rcatTyp == catTypes.Hour ||
                        rcatTyp == catTypes.Day ||
                        rcatTyp == catTypes.Month)
                    {
                        if (rcatTyp != catTypes.Month)
                        {
                            mnt = date.Month;
                        }

                        if (crit.Months.Contains(mnt) == false ||
                           crit.MonthsR.Contains(mnt) == true)
                        {
                            if (CaledTypes.Contains(catTypes.Month) == false)
                            {
                                CaledTypes.Add(catTypes.Month);
                                Exclude(crit, catTypes.Month);
                            }
                            crit.RemoveMonth(mnt);
                            crit.AddMonth(mnt, false);
                            change = true;
                        }

                        if (rcatTyp == catTypes.Month)
                        {
                            mnt--;
                            if (mnt == 0)
                            {
                                mnt = 12;
                                yer--;
                            }
                        }
                    }

                    ohour = date.Hour;
                    oday = date.Day;
                    omonth = date.Month;
                    oyear = date.Year;
                    UnExcludeOver(date);
                    date = date.Subtract(span);

                    if (date.Day != oday && rcatTyp != catTypes.Day)
                    {
                        ExcludeOver(catTypes.Day, 0, date.Day, date.Month, date.Year, -1);
                    }

                    if (date.Hour != ohour )
                    {
                        ExcludeOver(catTypes.Hour, date.Hour, date.Day, date.Month, date.Year, date.Minute);
                    }

                    if (date.Month != omonth && rcatTyp != catTypes.Month)
                    {
                        ExcludeOver(catTypes.Month, 0, 0, date.Month, date.Year, -1);
                    }

                    if (date.Year != oyear && rcatTyp != catTypes.Year)
                    {
                        ExcludeOver(catTypes.Year, 0, 0, 0, date.Year, -1);
                    }


                }//for (int i = 0; i < lastamount; i++)

                
            }
            CritSet.SetCriteria(crit);
        }

        public void UnExcludeOver(DateTime date)
        {
             VersionInfoSearchCriteria.ExcludeTime ex = DataManager.RejectExclude(date,crit);
             while (ex != null)
             {
                 crit.ExcludeTimes.Remove(ex);
                 ex = DataManager.RejectExclude(date, crit);
             }
        }

        public void ExcludeOver(catTypes cat, int hour, int day, int month, int year, int minMinute)
        {
            switch (cat)
            {
                case catTypes.Day:
                    for (int i = 0; i < 24; i++)
                    {
                        crit.ExcludeTimes.Add(new VersionInfoSearchCriteria.ExcludeTime(i,-1,year,month,day));
                    }
                break;

                case catTypes.Hour:
                    for (int i = 0; i < minMinute; i++)
                    {
                        crit.ExcludeTimes.Add(new VersionInfoSearchCriteria.ExcludeTime(hour, i, year, month, day));
                    }
                break;

                case catTypes.Month:
                    for (int i = 0; i < 32; i++)
                    {
                        crit.ExcludeTimes.Add(new VersionInfoSearchCriteria.ExcludeTime(-1, -1, year, month, i));
                    }
                break;

                case catTypes.Year:
                    for (int i = 0; i < 13; i++)
                    {
                        crit.ExcludeTimes.Add(new VersionInfoSearchCriteria.ExcludeTime(-1, -1, year, i, -1));
                    }
                break;

            }
        }

        public string addIndicaToText(string te)
        {
            string[] tokens = te.Split(' ');
            StringBuilder newString = new StringBuilder();
            int incre;
            int subIndex;

            bool oclockPresent;
            for (int i = 0; i < tokens.Length; i = i + incre)
            {





                incre = 1;
                oclockPresent = false;

                if (tokens[i].Equals("last"))
                {
                    if (tokens.Length > (i + 1))
                    {
                        if (isNumber(tokens[i + 1]) || NumbersFromNames.ContainsKey(tokens[i + 1]))
                        {
                            if (tokens.Length > (i + 2) && CatFromNames.ContainsKey(tokens[i + 2]))
                            {
                                newString.Append( tokens[i] + "-" + tokens[i + 1] + "-" + tokens[i+2] );
                                incre = 3;
                                newString.Append(" ");
                                continue;
                            }

                            incre = 2;
                        }
                        else if (CatFromNames.ContainsKey(tokens[i + 1]))
                        {
                            newString.Append(tokens[i] + "-" + tokens[i + 1]);
                            incre = 2;
                            newString.Append(" ");
                            continue;
                        }
                    }
                }
                //naked number
                else if (isNumber(tokens[i]) || NumbersFromNames.ContainsKey(tokens[i]))
                {
                    subIndex =  1;
                    //skip past all oclock
                    while (tokens.Length > (i + subIndex) && isOclock(tokens[i + subIndex]))
                    {
                        subIndex++;
                        oclockPresent = true;
                    }

                    if (tokens.Length > (i + subIndex) && isTimeAMPM(tokens[i + subIndex]))
                    {
                        newString.Append(tokens[i] + tokens[i + subIndex]);
                        newString.Append(" ");
                        incre = subIndex + 1;
                        continue;
                    }

                    if (oclockPresent == true)
                    {
                        newString.Append(tokens[i]);
                        newString.Append(" ");
                        continue;
                    }

                    if (tokens.Length > (i + subIndex) && isDate(tokens[i + subIndex]))
                    {
                        newString.Append(tokens[i] + tokens[i + subIndex]);
                        newString.Append(" ");
                        incre = subIndex + 1;
                        continue;
                    }

                    if (tokens.Length > (i + subIndex ) && MonthOfYearDic.ContainsKey(tokens[i + subIndex]))
                    {
                        string afterMonth;
                        //check if this month has a day specified after it
                        if (tokens.Length > (i +subIndex + 1))
                        {
                            afterMonth = tokens[i + subIndex + 1];
                        }
                        else
                        {
                            afterMonth = "";
                        }
                        string removeIden;
                        int hour;
                        int day;
                        if (afterMonth.Length > 0 && isNumber(afterMonth))
                        {
                            try
                            {
                                day = Convert.ToInt32(afterMonth);
                                if (day > 0 && day < 32)
                                {
                                    if (isDate(tokens[i + subIndex + 2]))
                                    {
                                        //this month allready has a date assume this is 
                                        //a time 
                                        newString.Append(tokens[i]);
                                        newString.Append(" ");
                                        continue;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }

                         
                        }
                        //if first character of string after the month starts
                        //with a number
                        else if (afterMonth.Length > 0 && Char.IsNumber(afterMonth[0]))
                        {
                            removeIden = afterMonth.Substring(afterMonth.Length - 2);
                            if (isDate(removeIden))
                            {
                                removeIden = afterMonth.Substring(0, afterMonth.Length - 2);
                                try
                                {
                                   day =  Convert.ToInt32(removeIden);
                                   if (day > 0 && day < 32)
                                   {
                                       //this month allready has a date assume this is 
                                       //a time 
                                       newString.Append(tokens[i]);
                                       newString.Append(" ");
                                       continue;
                                   }

                                }
                                catch (Exception)
                                {
                                }

                            }
                        }

                        //make this a date
                        newString.Append(tokens[i] + "th");
                        newString.Append(" ");
                        continue;

                    }

                    if (i != 0 && MonthOfYearDic.ContainsKey(tokens[i - 1]))
                    {
                        //make this a date
                        newString.Append(tokens[i] + "th");
                        newString.Append(" ");
                        continue;
                    }

                    newString.Append(tokens[i]);
                    newString.Append(" ");
                    

                }
                else
                {
                    newString.Append(tokens[i]);
                    newString.Append(" ");
                }
            }
            return newString.ToString();
        }

        public static bool isDate(string text)
        {
            if (text.Length == 0)
                return false;
            foreach (string t in date)
            {
                if (text.Equals(t))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool isTimeAMPM(string text)
        {
            if (text.Length == 0)
                return false;
            foreach (string t in timeIndicator)
            {
                if (text.Equals(t))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool isOclock(string text)
        {
            if (text.Length == 0)
                return false;
            foreach (string ocl in oclock)
            {
                if (text.Equals(ocl))
                {
                    return true;
                }
            }
            return false;
        }


        public static bool isNumber(string text)
        {
            if (text.Length == 0)
                return false;

            foreach(char c in text)
            {
                if (Char.IsNumber(c) == false)
                {
                    return false;
                }
            }

            return true;
        }

        private void Exclude(VersionInfoSearchCriteria crit,catTypes cator)
        {
            switch(cator)
            {
                case catTypes.DayOW:
                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    {
                        if (crit.DaysOfWeek.Contains(day) == false)
                        {
                            crit.AddDayOfWeek(day, true);
                        }
                    }
                break;
                
                case catTypes.Month:
                    for (int i = 1; i <= 12; i++)
                    {
                        if (crit.Months.Contains(i) == false)
                        {
                            crit.AddMonth(i, true);
                        }
                    }
                break;

                case catTypes.DayOM:
                    for (int i = 1; i <= 31; i++)
                    {
                        if (crit.DaysOfMonth.Contains(i) == false)
                        {
                            crit.AddDaysOfMonth(i, true);
                        }
                    }
                break;

                case catTypes.Hour:
                    for (int i = 0; i < 24; i++)
                    {
                        if (crit.Hours.Contains(i) == false)
                        {
                            crit.AddHours(i, true);
                        }
                    }
                break;

                case catTypes.Minute:
                    for (int i = 0; i < 60; i++)
                    {
                        if (crit.Minutes.Contains(i) == false)
                        {
                            crit.AddMinutes(i, true);
                        }
                    }
                break;

                case catTypes.Year:
                    foreach (int y in YearsList)
                    {
                        if (crit.Years.Contains(y) == false)
                        {
                            crit.AddYears(y, true);
                        }
                    }
                    break;

            }
        }


        public bool isDayOfMonth(string tex, out int number)
        {
            string fixText = tex.ToLower();
            string testNumN;
            string testNumL;
            fixText = fixText.Trim();
            fixText = fixText.Replace('-', '\0');
            testNumN = DeCostaNumbers.RemoveLetters(fixText);
            if (testNumN.Length > 0)
            {
                testNumL = DeCostaNumbers.GetLetters(fixText);
                if (
                    isDate(testNumL))
                {
                    number = Convert.ToInt32(testNumN);
                    if (number > 0 && number < 32)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                number = 0;
                return false;

            }
            else
            {
                return DayOfMonthDic.TryGetValue(fixText, out number);
            }
        }

        public bool isMonth(string text, out int number)
        {
            string fixText = text.ToLower();
            fixText = fixText.Trim();
            return MonthOfYearDic.TryGetValue(fixText, out number);

        }

        public bool isDayOfWeek(string text, out DayOfWeek week)
        {
            string fixText = text.ToLower();
            fixText = fixText.Trim();
            return DayOfWeekDic.TryGetValue(text, out week);
        }

        public bool isTime(string text, out int []hour, out int min)
        {
            string fixText = text.ToLower();
            int h;
            int h2;
            fixText = fixText.Trim();
            if (fixText.Contains("clock"))
            {
                fixText = RemoveOclock(fixText);
                min = 0;
                if (NumbersFromNames.TryGetValue(fixText, out h))
                {
                    if (h > 0 && h < 13)
                    {
                        if(h != 12)
                        {
                            h2 = h + 12;
                        }else
                        {
                            h2 = 0;
                        }
                        hour = new int[]{h, h2};
                        return true;
                    }
                    else
                    {
                        hour = new int[] { -1 };
                        return false;
                    }
                }
            }
            else if(fixText.Contains(":"))
            {
                string[] timeArr = fixText.Split(':');
                if (timeArr.Length > 1)
                {
                    ParseHourAndMinute(timeArr, out hour, out min);

                    if (hour[0] != -1 && min >= 0 && min <= 59)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (fixText.Contains("am"))
            {
                int rem = fixText.IndexOf("am");
                fixText = fixText.Remove(rem, 2);
                min = -1;
                try
                {
                    h= Convert.ToInt32(fixText);
                    if (h > 0 && h < 13)
                    {
                        if (h == 12)
                        {
                            h = 0;
                        }
                        hour = new int[] { h };
                        return true;
                    }
                    else
                    {
                        hour = new int[] { -1 };
                        return false;
                    }
                }
                catch (Exception)
                {
                    hour = new int[] { -1 };
                    return false;
                }

            }
            else if (fixText.Contains("pm"))
            {
                int rem = fixText.IndexOf("pm");
                fixText = fixText.Remove(rem, 2);
                min = -1;
                try
                {
                    h= Convert.ToInt32(fixText);
                    if (h > 0 && h < 13)
                    {
                        if (h != 12)
                        {
                            h += 12;
                        }
                        hour = new int[] { h };
                        return true;
                    }
                    else
                    {
                        hour = new int[] { -1 };
                        return false;
                    }
                }
                catch (Exception)
                {
                    hour = new int[] { -1 };
                    return false;
                }

            }
            else if (NumbersFromNames.TryGetValue(fixText, out h))
            {
                min = -1;
                if (h > 0 && h < 13)
                {
                    if (h != 12)
                    {
                        h2 = h + 12;
                    }
                    else
                    {
                        h2 = 0;
                    }
                    hour = new int[] { h, h2 };

                    return true;
                }
                else
                {
                    hour = new int[] { -1 };
                    return false;
                }
            }

            try
            {
                h = Convert.ToInt32(fixText);
                min = -1;
                if (h > 0 && h < 13)
                {
                    if (h != 12)
                    {
                        h2 = h + 12;
                    }
                    else
                    {
                        h2 = 0;
                    }
                    hour = new int[] { h, h2 };
                    return true;
                }
                else
                {
                    hour = new int[] { -1 };
                    return false;
                }
            }
            catch (Exception)
            {
            }

            hour = new int[] { -1 };
            min = -1;

            return false;


        }

        public static string RemoveOclock(string name)
        {

            int index = name.IndexOf("o'clock");
            if (index > -1)
            {
                return name.Substring(0, index);
            }
            index = name.IndexOf("oclock");
            if (index > -1)
            {
                return name.Substring(0, index);
            }
            index = name.IndexOf("clock");
            if (index > -1)
            {
                return name.Substring(0, index);
            }

            return "";
        }

        public static void ParseHourAndMinute(string[] timeArr, out int [] hour, out int min)
        {
            string hourS = timeArr[0];
            string minuteS = timeArr[1];
            hourS = hourS.Trim();
            minuteS = minuteS.Trim();
            bool pm = false;
            bool am = false;
            int h;
            int h2;
            if (minuteS.Contains("am"))
            {
                int rem = minuteS.IndexOf("am");
                minuteS = minuteS.Remove(rem, 2);
                am = true;
            }
            else if (minuteS.Contains("pm"))
            {
                int rem = minuteS.IndexOf("pm");
                minuteS = minuteS.Remove(rem, 2);
                pm = true;
            }



            hour = new int[]{-1,-1};
            min = -1;
            try
            {
                h = Convert.ToInt32(hourS);
                if (h < 1 || h > 12)
                {
                    hour = new int[] { -1, -1 };
                    return;
                }

                if (pm == true)
                {
                    if (h != 12)
                    {
                        h += 12;
                    }
                }
                else if (am == true)
                {
                    if (h == 12)
                    {
                        h = 0;
                    }
                }
                if (am || pm)
                {
                    hour = new int[] { h };
                }
                else
                {
                    if(h != 12)
                    {
                        h2 = h + 12;
                    }
                    else
                    {
                        h2 = 0;
                    }
                    hour = new int[]{h, h2};
                }
            }
            catch (Exception)
            {
            }
            try
            {
                min = Convert.ToInt32(minuteS);
            }
            catch (Exception)
            {
            }
        }

        public bool isRolling(string text, out RollingInfo rollIn)
        {

            string[] rollAr = text.Split('-');
            rollIn = new RollingInfo();
            catTypes Cat;
            if (rollAr[0].Equals("today"))
            {
                rollIn.RolType = RollonTypes.today;
                return true;
            }
            else if (rollAr[0].Equals("yesterday"))
            {
                rollIn.RolType = RollonTypes.Yesterday;
                return true;
            }
            else if (rollAr[0].Equals("last"))
            {
              
                bool num = false;
                
                if (isNumber(rollAr[1]))
                {
                    num = true;
                    try
                    {
                        rollIn.lastAmount = Convert.ToInt32(rollAr[1]);
                    }
                    catch (Exception)
                    {
                    }
                }

                int index;
                if (num == true)
                {
                    index = 2;

                }
                else
                {
                    index = 1;
                }

                if (CatFromNames.TryGetValue(rollAr[index], out Cat))
                {
                    rollIn.lastType = Cat;
                    rollIn.RolType = RollonTypes.last;
                    return true;
                }
                


                

            }


            rollIn = null;
            return false;
        }
    }
}
