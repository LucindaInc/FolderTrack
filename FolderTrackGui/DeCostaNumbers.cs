using System;
using System.Collections.Generic;
using System.Text;

namespace FolderTrackGuiTest1
{
    public class DeCostaNumbers
    {
        public const string NONE = "NONE";

        public const string AUNT = "AUNT";

        public static string GetBigSister(string version)
        {
            string[] versionSpl = version.Split('.');
            string letters = "";
            int number;
            if (versionSpl.Length == 1)
            {
                return NONE;
            }

            if (versionSpl[versionSpl.Length - 1].Equals("1") == false)
            {
                return AUNT;
            }
            else
            {
                if (ContainsLetter(versionSpl[versionSpl.Length - 2]) == true)
                {
                    letters = GetLetters(versionSpl[versionSpl.Length - 2]);
                    number = getVersionLetter(letters);
                    number--;
                    letters = "";
                    if (number >= 0)
                    {
                        letters = getLetterVersion(number);
                    }
                    versionSpl[versionSpl.Length - 2] = RemoveLetters(versionSpl[versionSpl.Length - 2]) + letters;
                    StringBuilder versi = new StringBuilder();
                    for (int i = 0; i < versionSpl.Length; i++)
                    {
                        versi.Append(versionSpl[i]);
                        versi.Append(".");
                    }
                    versi.Length = versi.Length - 1;
                    return versi.ToString();
                }
                else
                {
                    versionSpl[versionSpl.Length - 2] = Convert.ToString(Convert.ToInt32(versionSpl[versionSpl.Length - 2]) + 1);
                    StringBuilder versi = new StringBuilder();
                    for (int i = 0; i < versionSpl.Length - 1; i++)
                    {
                        versi.Append(versionSpl[i]);
                        versi.Append(".");
                    }
                    versi.Length = versi.Length - 1;
                    return versi.ToString();
                }
            }
        }


        /// <summary>
        /// Determines of 2 versions are first decendents of each other
        /// 
        /// 4      0a.4
        /// 3      0a.3
        /// 2      0a.2
        /// 1  0.1 0a.1
        /// 0
        /// 
        /// In the above number set: 
        /// 0,1        true 
        /// 4,2        true
        /// 0a.2, 0a.4 true
        /// 2, 0a.3    false
        /// 0.1, 0a.4  false
        /// </summary>
        public static bool FirstDaughterDecendent(string version1, string version2)
        {
            int v1 = version1.LastIndexOf(".");
            int v2 = version2.LastIndexOf(".");

            if (v1 < 0 && v2 > 0)
            {
                return false;
            }
            else if (v1 > 0 && v2 < 0)
            {
                return false;
            }
            else if (v1 < 0 && v2 < 0)
            {
                return true;
            }

            string subSt1 = version1.Substring(0,v1);
            string subSt2 = version2.Substring(0,v2);

            return subSt1.Equals(subSt2);
        }




        public static string GetMother(string version)
        {
            string[] versionSpl = version.Split('.');
            if (versionSpl.Length == 1)
            {
                int vers = Convert.ToInt32(version);
                vers--;
                return Convert.ToString(vers);
            }

            if (versionSpl[versionSpl.Length - 1].Equals("1") == false)
            {
                int lastN = (Convert.ToInt32(versionSpl[versionSpl.Length - 1]) - 1);
                versionSpl[versionSpl.Length - 1] = Convert.ToString(lastN);
                StringBuilder versi = new StringBuilder();
                for (int i = 0; i < versionSpl.Length; i++)
                {
                    versi.Append(versionSpl[i]);
                    versi.Append(".");
                }
                versi.Length = versi.Length - 1;
                return versi.ToString();

            }
            else
            {
                versionSpl[versionSpl.Length - 2] = RemoveLetters(versionSpl[versionSpl.Length - 2]);
                StringBuilder versi = new StringBuilder();
                for (int i = 0; i < versionSpl.Length - 1; i++)
                {
                    versi.Append(versionSpl[i]);
                    versi.Append(".");
                }
                versi.Length = versi.Length - 1;
                return versi.ToString();
            }
        }

        public static string GetAunt(string version)
        {
            string Mo = GetMother(version);
            string ReturnString;
            do
            {
                ReturnString = GetBigSister(Mo);
                Mo = GetMother(Mo);
            } while (ReturnString.Equals(AUNT));
            return ReturnString;
        }

        /// <summary>
        /// The opposite of get LetterVersoion. This takes a letters and returns the numbers
        /// </summary>
        /// <example>
        /// a -> 0
        /// b -> 1
        /// c -> 2
        /// ...
        /// aa -> 26
        /// ab -> 27
        /// ...
        /// 702 -> aaa
        /// </example>
        /// 
        /// <param name="let"> the letters</param>
        /// <returns>the number representing the numbers</returns>
        public static int getVersionLetter(String let)
        {
            double verD = 0;
            int num;
            int ver = 0;
            double pow;

            for (int i = 0; i < let.Length; i++)
            {
                num = (int)let[(let.Length - 1) - i];
                num = num - 97;
                pow = Math.Pow(posDig, i);
                verD = verD + (num + 1) * pow;
            }
            ver = (int)verD - 1;
            return ver;
        }

        //Amount of letters in the alphabet
        private static double posDig = 26;

        /// <summary>
        /// Retruns letters to represent a number. Read the example for a better
        /// discription
        /// </summary>
        /// <example>
        /// 0 -> a
        /// 1 -> b
        /// 2 -> c
        /// ...
        /// 26 -> aa
        /// 27 -> ab
        /// ...
        /// 702 -> aaa
        /// </example>
        /// <param name="ver"> the number</param>
        /// <returns>letters to represent the number </returns>
        public static String getLetterVersion(int ver)
        {
            double z = ver;
            int num;
            double amntOfDig;
            StringBuilder letVer = new StringBuilder();
            String let;



            amntOfDig = getAmOfDig(z);
            while (z >= 0)
            {
                num = (int)((Math.Floor((z - getBeginOfSet(posDig, amntOfDig)) / Math.Pow(posDig, amntOfDig - 1))) % posDig);
                letVer.Append((char)(num + 97));
                z = getSetBefore(z);
                amntOfDig = getAmOfDig(z);
            }
            let = letVer.ToString();
            return let;
        }



        /// <summary>
        /// The amount of digits in a number for example 100 has 3 and 23 has 2
        /// </summary>
        /// <param name="z">the number that needs to be evaluated</param>
        /// <returns>the amount of digits in a number</returns>
        private static int getAmOfDig(double z)
        {
            double a, c;
            a = 0;
            c = 0;
            while (z >= a)
            {

                c = c + 1;
                a = a + Math.Pow(posDig, c);
            }
            return (int)c;
        }

        /// <summary>
        /// Not totaly sure what this does but it is used to convert numbers to letters
        /// </summary>
        /// <param name="z">unknown what this is for</param>
        /// <param name="a">unknown what this is for</param>
        /// <returns></returns>
        private static double getBeginOfSet(double z, double a)
        {
            double tot = 0;
            for (int i = 1; i < a; i++)
            {
                tot = tot + Math.Pow(z, i);
            }
            return tot;
        }

        /// <summary>
        /// Not sure what this is
        /// </summary>
        /// <param name="z">not sure what it is for</param>
        /// <returns>not sure what this returns</returns>
        private static double getSetBefore(double z)
        {
            double AmOfDig = getAmOfDig(z);
            double beginOfSet = getBeginOfSet(posDig, AmOfDig);
            double top = z - beginOfSet;
            double set = Math.Floor(top / Math.Pow(posDig, AmOfDig - 1)) + 1;
            return z - Math.Pow(posDig, AmOfDig - 1) * set;
        }

        /// <summary>
        /// Returns true if the passed in version contains letters
        /// </summary>
        /// <param name="version">the version string</param>
        /// <returns>true of the string version contains letters false otherwise</returns>
        public static bool ContainsLetter(string version)
        {
            foreach (char ch in version)
            {
                if (Char.IsLetter(ch))
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// Returns the letters in a given string
        /// </summary>
        /// <param name="version">the version string</param>
        /// <returns>the letters present in the version string</returns>
        public static string GetLetters(string version)
        {
            StringBuilder returnString = new StringBuilder();

            foreach (char ch in version)
            {
                if (Char.IsLetter(ch))
                {
                    returnString.Append(ch);
                }
            }

            return returnString.ToString();
        }

        /// <summary>
        /// Given a string this removed the letters 
        /// </summary>
        /// <param name="version">a string to remove letters from</param>
        /// <returns>the passed in string version without the letters</returns>
        public static string RemoveLetters(string version)
        {
            StringBuilder returnString = new StringBuilder();

            foreach (char ch in version)
            {
                if (Char.IsDigit(ch))
                {
                    returnString.Append(ch);
                }
            }

            return returnString.ToString();
        }

    }
}
