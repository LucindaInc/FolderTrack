using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FolderTrack.Types;
using FolderTrackGuiTest1.NewMonitorGroup;
using System.Threading;

namespace FolderTrackGuiTest1
{
    static class Program
    {
        static FTObjects ftobjects;
        static MainForm mainform;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Util.UserDebug("FolderTrack Gui Started");
            Util.DBug2("Program", "Started Gui");
            Application.EnableVisualStyles();
            Util.DBug2("Program", "EnableVisualStyles");
            Application.SetCompatibleTextRenderingDefault(false);
            Util.DBug2("Program", "SetCompatibleTextRenderingDefault");
            ftobjects = new FTObjects(new DataReceiver());
            Util.DBug2("Program", "Run Check");
         //   MainForm FolderTrackGui = new MainForm(ftobjects);
            Util.DBug2("Program", "Get Monitor Group Information");
            List<MonitorGroupInfo> monitorGrList = ftobjects.GetAllMonitorGroupInfor();
            mainform = new MainForm();
            mainform.SetFTBeforeOpenMonGr(ftobjects);
            //add main form so it can receive alerts and 
            ftobjects.AddToCallList(mainform);
            if (monitorGrList.Count == 0)
            {
                Util.DBug2("Program", "No Monitor Groups Detected show window");
                NewMonitorGroupForm mgDia = new NewMonitorGroupForm();
                DialogResult di = mgDia.ShowDialog();
                if (di == DialogResult.OK)
                {
                    mainform.HideNoMonitorGroup();
                    Util.DBug2("Program", "Sending NameOfMonitor " + mgDia.LocationManager.NameOfMonitor);
                    foreach (string loca in mgDia.LocationManager.MonitorLocation)
                    {
                        Util.DBug2("Program", "Location " + loca);
                    }
                    new Thread(new ParameterizedThreadStart(SendNewMonitorGroup)).Start(mgDia);
                }
                else if (di == DialogResult.Cancel)
                {
                 //   return;
                }
            }
            else
            {
                new Thread(SetFtOb).Start();
            }
            Util.DBug2("Program", "R");
            Application.Run(mainform);
        }

        public static void SendNewMonitorGroup(object mgDiaO)
        {
            NewMonitorGroupForm mgDia = (NewMonitorGroupForm)mgDiaO;

            bool succ = ftobjects.NewMonitorGroup(mgDia.LocationManager.NameOfMonitor, mgDia.LocationManager.MonitorLocation, mgDia.LocationManager.filter);
            if (succ == true)
            {
                mainform.SetFtObjects(ftobjects);
            }
        }

        public static void SetFtOb()
        {
            mainform.SetFtObjects(ftobjects);
        }

    }
}