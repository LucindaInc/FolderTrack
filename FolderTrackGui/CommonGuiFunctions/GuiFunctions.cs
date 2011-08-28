using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using FolderTrackGuiTest1.UserVersionDialog;
using FolderTrack.Types;

namespace FolderTrackGuiTest1.CommonGuiFunctions
{
    public class GuiFunctions
    {
        public static FTObjects m_FTObjects;

        class FiltArg
        {
            public FolderUnit path;
            public List<string> FilterSt;
        }

        public static void HandleFilters(FolderUnit path, List<string> FilterSt)
        {
            FiltArg filarg = new FiltArg();
            filarg.path = path;
            filarg.FilterSt = FilterSt;
            new Thread(ThreadHandleFilter).Start((object) filarg);
        }

        private static void ThreadHandleFilter(object filargO)
        {
            List<string> CurFil = m_FTObjects.GetFilters();
            FiltArg filarg = (FiltArg)filargO;
            List<string> mon = new List<string>();
            mon.AddRange(m_FTObjects.GetLocationsInMonitorGroup());

            Filters.FilterFileForm fil = new FolderTrackGuiTest1.Filters.FilterFileForm(filarg.path, CurFil, filarg.FilterSt,mon);
            DialogResult res = fil.ShowDialog();
            
            if (res == DialogResult.OK)
            {
                m_FTObjects.SetFilter(fil.GetLists());
            }
        }

        class UserVersVer
        {
            public UserVersionForm uservers;
            public VersionInfo versionInfo;
        }

        public static string ErrorMonitorGroupText()
        {
            return                     "A problem with deleting or setting up a monitor group " +
                                       " can cause this problem. If this is still in use" +
                                       " contact support: nick@foldertrack.com. If not" +
                                       " click Delete.";
        }

        public static void AddUserVersionButton_Click( VersionInfo VersionInfo, EventArgs e, Panel Caller)
        {
            if (VersionInfo != null)
            {
                Util.UserDebug("Add User Version Button Clicked " + VersionInfo.versionName);
                UserVersionForm uservers = new UserVersionForm(VersionInfo, m_FTObjects.AllUserVersions);
                DialogResult res = uservers.ShowDialog(Caller);
                if (res == DialogResult.OK)
                {
                    UserVersVer ver = new UserVersVer();
                    ver.uservers = uservers;
                    ver.versionInfo = VersionInfo;
                    new Thread(new ParameterizedThreadStart(HandleAddUser)).Start(ver);
                }
            }


        }

        private static void HandleAddUser(object verO)
        {
            UserVersVer ver = (UserVersVer)verO;
            UserVersionForm uservers = ver.uservers;
            
            UserVersionSet set = new UserVersionSet();
            foreach (string newuserver in uservers.NewUserVersions)
            {
                //       m_FTObjects.addUserVersion(m_VersionInfo.versionName, newuserver, false);
                //clean new version out of continue
                uservers.ContinueUserVersions.Remove(newuserver);
            }

            //   foreach (string reuser in uservers.ReUseUserVersions)
            //  {
            //      m_FTObjects.addUserVersion(m_VersionInfo.versionName, reuser, false);
            //  }

            set.AddUserVersion.AddRange(uservers.NewUserVersions);
            set.AddUserVersion.AddRange(uservers.ReUseUserVersions);


            foreach (UserVersionStatus lastuser in ver.versionInfo.userVersionsThatContain)
            {
                if (lastuser.end == true)
                {
                    uservers.LastUserVersions.Remove(lastuser.UserVersion);
                }
                else
                {
                    //clean allready continue versions 
                    uservers.ContinueUserVersions.Remove(lastuser.UserVersion);
                }
            }

            //         m_FTObjects.setStopVersion(uservers.LastUserVersions, m_VersionInfo.versionName, false, false);
            set.LastUserVersions.AddRange(uservers.LastUserVersions);

            //       foreach (string contin in uservers.ContinueUserVersions)
            //       {
            //          m_FTObjects.RemoveStopUserVersion(contin, m_VersionInfo.versionName);
            //      }
            set.RemoveStop.AddRange(uservers.ContinueUserVersions);

            //        m_FTObjects.setStopVersion(uservers.RemoveUserVersions, m_VersionInfo.versionName, true, true);
            set.RemoveUserVersions.AddRange(uservers.RemoveUserVersions);

            m_FTObjects.SetUserVersion(ver.versionInfo.versionName, set);
            uservers.Dispose();

        }
    }
}
