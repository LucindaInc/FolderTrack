using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using ZetaLongPaths;
using FolderTrack.Types;

namespace FolderTrackGuiTest1
{
    public class Util
    {


        public interface MonGroRet
        {
            void VerInfoCal(List<VersionInfo> versList, bool done);
        }

        static object lockObject = new object();
        static string fotDebNam = null;
        static string useDebNam = null;
        static string company=null;
        static string product=null;
        /// <summary>
        /// Used as an object to prevent invoke from locking each
        /// </summary>
       

        public static void DBug2(string location, string text)
        {
            
            if (fotDebNam == null)
            {
                string PreDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                             + Path.DirectorySeparatorChar + Util.Company + Path.DirectorySeparatorChar
                             + Util.Product
                             + Settings.Default.debugDir
                             + Path.DirectorySeparatorChar;

                fotDebNam = PreDir + "GuiDebug" + Path.DirectorySeparatorChar + DateTime.Now.ToString("d-MMM-yyyy.HH.mm.ss") + ".txt";
            }
            lock (lockObject)
            {
                try
                {
                    if (ZlpIOHelper.DirectoryExists(ZlpPathHelper.GetDirectoryNameFromFilePath(fotDebNam)) == false)
                    {
                        ZlpIOHelper.CreateDirectory(ZlpPathHelper.GetDirectoryNameFromFilePath(fotDebNam));
                    }
             //       StreamWriter sr = new StreamWriter(File.Open(fotDebNam, FileMode.Append));
          //          sr.WriteLine(DateTime.Now + " : " + text);
         //           sr.Flush();
          //          sr.Close();
                }
                catch (Exception e)
                {
                }
            }

        }

        public static List<string> GetDirectoriesAndFile(string location)
        {
            List<string> direcandfilLis = new List<string>();
            GetDirectoriesAndFilesAr(location, direcandfilLis);
            return direcandfilLis;
        }


        private static void GetDirectoriesAndFilesAr(string location, List<string> direcandfilLis)
        {
            try
            {
                ZlpIOHelper.GetFiles(location);
                // Directory.GetFiles(location);
                direcandfilLis.Add(location);
            }
            catch
            {
                return;
            }

            //   ZlpIOHelper.GetDirectories(
            foreach (ZlpDirectoryInfo directory in ZlpIOHelper.GetDirectories(location, "*"))
            {
                GetDirectoriesAndFilesAr(directory.FullName, direcandfilLis);
            }
            foreach (ZlpFileInfo filein in ZlpIOHelper.GetFiles(location, "*", SearchOption.TopDirectoryOnly))
            {
                direcandfilLis.Add(filein.FullName);
            }
        }


        public static String Company
        {
            get
            {
                if (company == null)
                {
                    AssemblyCompanyAttribute aca = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(typeof(FolderTrackGuiTest1.MainForm).Assembly, typeof(AssemblyCompanyAttribute));
                    company = aca.Company;
                }
                return company;
            }
        }

        public static String Product
        {
            get
            {
                if (product == null)
                {
                    AssemblyProductAttribute apa = (AssemblyProductAttribute)Attribute.GetCustomAttribute(typeof(FolderTrackGuiTest1.MainForm).Assembly, typeof(AssemblyProductAttribute));
                    product = apa.Product;
                }
                return product;
            }
        }



        public static void UserDebug(string text)
        {
            if (useDebNam == null)
            {
                string PreDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                             + Path.DirectorySeparatorChar + Util.Company + Path.DirectorySeparatorChar
                             + Util.Product
                             + Settings.Default.debugDir
                             + Path.DirectorySeparatorChar;

                useDebNam = PreDir + "GuiDebug" + Path.DirectorySeparatorChar + DateTime.Now.ToString("d-MMM-yyyy.HH.mm.ss") + ".txt";
            }
            lock (lockObject)
            {
                try
                {
                    if (ZlpIOHelper.DirectoryExists(ZlpPathHelper.GetDirectoryNameFromFilePath(useDebNam)) == false)
                    {
                        ZlpIOHelper.CreateDirectory(ZlpPathHelper.GetDirectoryNameFromFilePath(useDebNam));
                    }
             //       Microsoft.Win32.SafeHandles.SafeFileHandle han = ZlpIOHelper.CreateFileHandle("GuiDebug.txt", Utilities.Native.CreationDisposition.,
             //       Utilities.Native.FileAccess.GenericWrite,
            //        Utilities.Native.FileShare.Write);
         //       FileStream filStr = new FileStream(han, FileAccess.Write);
               

           //         StreamWriter sr = new StreamWriter(File.Open(useDebNam, FileMode.Append));
         //           sr.WriteLine(DateTime.Now + " : " + text);
          //          sr.Flush();
           //         sr.Close();
                }
                catch (Exception e)
                {
                }
            }

        }

        public static string intToTimeStr(int tim)
        {
            if (tim >= 0 && tim <= 11)
            {
                return "" + (tim + 1) + "am";
            }
            else if(tim >= 13 && tim <= 23)
            {
                return "" + (tim - 12) + "pm";
            }
            else if (tim == 12)
            {

                return "12pm";
            }
            else
            {
                throw new Exception("Invalid time entered :" + tim + " the time must be between 0 and 23");
            }
       }
    }
}
