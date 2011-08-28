using System;
using System.Collections.Generic;
using System.Text;
using FolderTrack.Types;
using FolderTrack.WCFContracts;
using FolderTrackGuiTest1.License;
using FolderTrack.Info;
using FolderTrack.Delete;

namespace FolderTrackGuiTest1
{
    public class FTObjects : FolderTrack.WCFContracts.FolderTrackCallBack
    {
        public interface GuiMessage
        {
            void SetDeleteReturnVal(FolderTrack.Delete.DeleteRules.DeleteReturn delete);
        }
        public const int NO_VERSION_LIST = -1;

        public DataManager dataManager;

        public DataReceiver dataReceiver;

        private string m_currentMonitorGroup;

        private string m_CurrentVersion;

        private MainForm mainform;

        private FolderTrack.Delete.DeleteRules.DeleteReturn pDeleteReturn;

        public const int UNKN = -1;

        private int dontMonitor = UNKN;

        public const int DONT_MON = 1;

        public const int MON = 2;

        public FolderTrack.Delete.DeleteRules.DeleteReturn DeleteRetur
        {
            get
            {
                return pDeleteReturn;
            }
        }


        private List<FolderTrack.WCFContracts.FolderTrackCallBack> CallList;
        private List<GuiMessage> GuiCallList;

        private Dictionary<int, Util.MonGroRet> MonGroCallFromPass = new Dictionary<int, Util.MonGroRet>();

        public FTObjects(DataReceiver dataReceiver)
        {
            this.dataReceiver = dataReceiver;
            pDeleteReturn = FolderTrack.Delete.DeleteRules.DeleteReturn.NOT_REMOVED;
            this.dataReceiver.AddToCallList(this);
            CallList = new List<FolderTrackCallBack>();
            GuiCallList = new List<GuiMessage>();
        }

        public void AddToCallList(FolderTrackCallBack addCall)
        {
            CallList.Remove(addCall);
            CallList.Add(addCall);
        }

        public void AddToCallGuiCallList(GuiMessage add)
        {
            GuiCallList.Remove(add);
            GuiCallList.Add(add);
        }

        public string CurrentVersion
        {
            get
            {
                return m_CurrentVersion;
            }
        }

        public void SetMainForm(MainForm mainform)
        {
            this.mainform = mainform;
        }

        public VersionInfo GetCurrentVersionInfo()
        {
            if (m_CurrentVersion == null)
            {
                return null;
            }
            return VersionInfoFromVersionName(m_CurrentVersion);
        }

        public DeleteRules getDeleteRules()
        {
            if (m_CurrentVersion == null)
            {
                return null;
            }
            return dataReceiver.getDeleteRules(m_currentMonitorGroup);
        }


        public void ExploreVersion(string version)
        {
            mainform.ExploreVersion(version);
        }

        public bool VersionExist(string version)
        {
            return dataManager.VersionExist(version);
        }

        public void TaskAnswer(int TyeNumber)
        {
            dataReceiver.TaskAnswer(TyeNumber);
        }

        public bool NameReady()
        {
            if (CurrentMonitorGroup != null)
            {
                return dataReceiver.NameReady(CurrentMonitorGroup);
            }

            return false;
        }

        public bool NameReady(string name)
        {
            return dataReceiver.NameReady(name);
        }

        public void SetDeleteReturnVal(FolderTrack.Delete.DeleteRules.DeleteReturn deleterule)
        {
            pDeleteReturn = deleterule;
            foreach (GuiMessage guim in GuiCallList)
            {
                guim.SetDeleteReturnVal(pDeleteReturn);
            }
            SetNewDataAndCurrent();

            

        }

        public FolderTrack.Delete.DeleteRules.DeleteReturn GetDeleteReturnVal()
        {
            return pDeleteReturn;
        }


        public string CurrentMonitorGroup
        {
            get
            {
                return m_currentMonitorGroup;
            }

            set
            {
                Util.DBug2("MainForm", "Enter CurrentMonitorGroup");
                Util.DBug2("MainForm", "Enter assign value");
                m_currentMonitorGroup = value;
                SetNewDataAndCurrent();
            }
        }

        private void SetNewDataAndCurrent()
        {
            
            dontMonitor = UNKN;
            Util.DBug2("MainForm", "Enter try to get VersionName");
            m_CurrentVersion = dataReceiver.GetCurrentVersionName(m_currentMonitorGroup);
            Util.DBug2("MainForm", "Enter try to get Manager");
            this.dataManager = dataReceiver.GetDataManagerFromName(m_currentMonitorGroup, pDeleteReturn);
            Util.DBug2("MainForm", "Enter try to get GetDataManagerFromName");
        }

        public void CopyVersion(String version, Dictionary<String, String> ToLocations)
        {
            dataReceiver.CopyVersion(CurrentMonitorGroup, version, ToLocations);
        }


        public void StopMonitoringGroup(string MonitorGroup)
        {
            dataReceiver.StopMonitoringGroup(MonitorGroup);
        }

        public void RestartMonitoringGroup(string MonitorGroup)
        {
            dataReceiver.RestartMonitoringGroup(MonitorGroup);
        }

        public void DeletMonitoringGroup(string MonitorGroup)
        {
            dataReceiver.DeletMonitoringGroup(MonitorGroup);
            if (m_currentMonitorGroup != null && m_currentMonitorGroup.Equals(MonitorGroup))
            {
                m_currentMonitorGroup = null;
            }
        }

        public IList<VersionInfo> CurrentVersionList
        {
            get
            {
                if (dataManager != null)
                {
                    List<VersionInfo> vers = new List<VersionInfo>( dataManager.VersionList.Values);
                    vers.Reverse();
                    return vers;
                }
                return null;
            }
        }

        public FolderUnit[] GetFolderUnitFromVersion(string version)
        {
            VersionInfo outversioninf;
            if(dataManager.TryGetValue(version,out outversioninf))
            {
                return dataReceiver.GetFolderUnit(m_currentMonitorGroup, outversioninf);
            }
            return null;
        }


        public void addUserVersion(string version, List<string> UserVersion, bool lastUserVersC)
        {
            dataReceiver.addUserVersion(this.m_currentMonitorGroup, version, UserVersion, lastUserVersC);
        }

        public void setStopVersion(List<string> UserVerNames, string version, bool ExcludeNow, bool lastUserVersC)
        {
            dataReceiver.setStopVersion(this.m_currentMonitorGroup, UserVerNames, version, ExcludeNow, lastUserVersC);
        }

        public void SetUserVersion(string version, UserVersionSet set)
        {
            dataReceiver.SetUserVersion(this.m_currentMonitorGroup, version, set);
        }

        public int CurrentAmountOfVersions
        {
            get
            {
                if (dataManager != null && dataManager.VersionList != null)
                {
                    return dataManager.VersionList.Count;
                }
                return NO_VERSION_LIST;
            }
        }

        public void SetDiscription(String version, String Discription)
        {
            //TODO this should figure out if the opperation was successful
            dataReceiver.setFreeText(m_currentMonitorGroup, version, Discription);
        }

        public List<string> AllUserVersions
        {
            get
            {
                return dataManager.getUserVersions();
            }
        }

        public List<string> AllFiles
        {
            get
            {
                List<string> AllFile = new List<string>();
                AllFile.AddRange(dataManager.getFiles());
                return AllFile;
            }
        }

        public void SetVersion(String version)
        {
            dataReceiver.useVersion(m_currentMonitorGroup, version);
        }

        public IList<string> GetLocationsInMonitorGroup()
        {
            return dataReceiver.LocationsInMonitorGroup(m_currentMonitorGroup);
        }

        public VersionInfo VersionInfoFromVersionName(string versionName)
        {
            Util.DBug2("FTOn", "wants versioninfo for " + versionName);
            VersionInfo versionin;
            Util.DBug2("FTOn", "Asking dm");
            if (dataManager.TryGetValue(versionName, out versionin))
            {
                Util.DBug2("FTOn", "dm has it returning");
                return versionin;
            }
            Util.DBug2("FTOn", "dm must not have it returning null");
            return null;
        }
           
        public bool CheckLicense()
        {
            return dataReceiver.CheckLicense();
        }

        public void UseFolderUnit(VersionInfo versionin, string VersionLocation)
        {

            dataReceiver.UseFolderUnit(m_currentMonitorGroup, versionin, VersionLocation);
        }

        public object CopyFolderUnit(VersionInfo versionin, string VersionLocation,string CopyLocation)
        {
           
            return dataReceiver.CopyFolderUnit(m_currentMonitorGroup,versionin, VersionLocation,CopyLocation);
        }

        public void SetFilter(FilterChangeOb filt)
        {
             dataReceiver.SetFilter(m_currentMonitorGroup, filt);
        }

        public List<string> GetFilters()
        {
            return dataReceiver.GetFilters(m_currentMonitorGroup);
        }

        public void DeleteVersion(VersionInfo version)
        {
            dataReceiver.DeleteVersion(m_currentMonitorGroup, version);
            //DeleteVersionC(m_currentMonitorGroup, version);
            

        }

        public void UndeleteVersion(VersionInfo version)
        {
            dataReceiver.UndeleteVersion(m_currentMonitorGroup, version);
        }

        public void UndeleteVersion(List<VersionInfo> versionL)
        {
            dataReceiver.UndeleteVersion(m_currentMonitorGroup, versionL);
        }

        public void DeleteVersion(List<VersionInfo> versionL)
        {
            dataReceiver.DeleteVersion(m_currentMonitorGroup, versionL);
        }

        public Options GetOptions()
        {
            return dataReceiver.GetOptions(m_currentMonitorGroup);
        }

        public void OptionChanes(Options o)
        {
            dataReceiver.OptionChanges(m_currentMonitorGroup, o);
        }

        public IList<ChangeInstruction> GetFolderUnitInfo(VersionInfo version, string FuLocation)
        {
            return dataReceiver.GetFolderUnitInfo(m_currentMonitorGroup, version, FuLocation);
        }

        public int FolderUnitsVersionU(VersionInfo version, string FuLocation)
        {
            return dataReceiver.FolderUnitsVersionU(m_currentMonitorGroup, version, FuLocation);
        }

        public List<SearchResults> Search(string search)
        {
            return dataReceiver.Search(m_currentMonitorGroup, search);
        }

        public int[] GetYears
        {
            get
            {
                return dataManager.GetYears();
            }
        }

        public void RemoveStopUserVersion(string UserVerName, string vers)
        {
            dataReceiver.RemoveStopUse(m_currentMonitorGroup, UserVerName, vers);
        }

        public Information GetInformation()
        {
            return dataReceiver.GetInformation();
        }

        public bool GetDontMonitor
        {
            get
            {
                if (dontMonitor == UNKN)
                {
                    if (dataReceiver.GetDontMonitor(m_currentMonitorGroup))
                    {
                        dontMonitor = DONT_MON;
                        return true;
                    }
                    else
                    {
                        dontMonitor = MON;
                        return false;
                    }
                }
                else if (dontMonitor == DONT_MON)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        

        public List<VersionInfo> VersionInfoFromCriteria(VersionInfoSearchCriteria criteria, DataManager.Status stat)
        {
            return dataManager.VersionInfoFromCriteria(criteria,stat);
        }

        public List<MonitorGroupInfo> GetAllMonitorGroupInfor()
        {
            return dataReceiver.GetAllMonitorGroupInfo();
        }

        public string GetProgramNumber()
        {
            return dataReceiver.GetProgramNumber();
        }

        public bool SetLicense(string lc)
        {
            return dataReceiver.SetLicense(lc);
        }

        #region FolderTrackCallBack Members

        public void NewCurrentVersion(string MonitorGroup, VersionInfo vers)
        {
            if (MonitorGroup.Equals(m_currentMonitorGroup) == true)
            {
                m_CurrentVersion = vers.versionName;

                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.NewCurrentVersion(MonitorGroup, vers);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }

            }
        }

        public void SendNewUserVersion(string MonitorGroup, List<string> UserVers)
        {
            if (MonitorGroup.Equals(m_currentMonitorGroup) == true)
            {
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.SendNewUserVersion(MonitorGroup, UserVers);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }

            }

        }

        public void DeleteRules(DeleteRules rules)
        {
            dataReceiver.DeleteRules(m_currentMonitorGroup, rules);
        }

        public void DontMonitor(string MonitorGroup)
        {
            if (MonitorGroup.Equals(m_currentMonitorGroup) == true)
            {
                dontMonitor = DONT_MON;
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.DontMonitor(MonitorGroup);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }

            }

            

        }

        public void RestartMonitor(string MonitorGroup)
        {
            if (MonitorGroup.Equals(m_currentMonitorGroup) == true)
            {
                dontMonitor = MON;
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.RestartMonitor(MonitorGroup);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }

            }



        }

        public void SendData(List<ChangeInstruction> data, int idnum, bool done)
        {
            
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.SendData(data, idnum, done);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }

            
        }

        public void GetVersionInfoFromMonitor(FolderTrack.Delete.DeleteRules.DeleteReturn delret, Util.MonGroRet retCall)
        {
            int passed;
            passed = dataReceiver.getMonitorGroupVersionsOf(m_currentMonitorGroup, delret);
            MonGroCallFromPass[passed] = retCall;

        }

        public void SendDataV(List<VersionInfo> data, string name, int idnum, bool done)
        {
            if (idnum == dataReceiver.SendToAll)
            {
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    flCall.SendDataV(data, name, idnum, done);
                }
            }
            else
            {
                Util.MonGroRet callMon;

                if (MonGroCallFromPass.TryGetValue(idnum, out callMon))
                {
                    callMon.VerInfoCal(data, done);
                    if (done == true)
                    {
                        MonGroCallFromPass.Remove(idnum);
                    }
                }
            }
        }


        public void NewVersion(string MonitorGroup, List<VersionInfo> vers)
        {
            vers.Reverse();
            if (MonitorGroup.Equals(m_currentMonitorGroup) == true)
            {
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.NewVersion(MonitorGroup, vers);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }

            }
            if (vers.Count > 0)
            {
                NewCurrentVersion(MonitorGroup, vers[0]);
            }
        }

        public void DeleteVersionC(string MonitorGroup,VersionInfo version)
        {
            if (MonitorGroup.Equals(m_currentMonitorGroup) == true)
            {
                dontMonitor = DONT_MON;
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.DeleteVersionC(MonitorGroup,version);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }

            }
        }


        public void DeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
            if (MonitorGroup.Equals(m_currentMonitorGroup) == true)
            {
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.DeleteVersionCList(MonitorGroup, version);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }
            }
        }


        public void UndeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            if (MonitorGroup.Equals(m_currentMonitorGroup) == true)
            {
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.UndeleteVersionC(MonitorGroup, version);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }
            }
        }


        public void UndeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
            if (MonitorGroup.Equals(m_currentMonitorGroup) == true)
            {
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.UndeleteVersionCList(MonitorGroup, version);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }
            }
        }

        public void NewVersionInformation(string MonitorGroup, List<VersionInfo> vers)
        {
            if (MonitorGroup.Equals(m_currentMonitorGroup) == true)
            {
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.NewVersionInformation(MonitorGroup, vers);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }

            }
        }

        public void TaskUpdate(TaskGroup[] task)
        {
            foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.TaskUpdate(task);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }
        }

        public bool NewMonitorGroup(string monitorGroup, IList<string> MonitiorLocations, Options filter)
        {
            return dataReceiver.NewMonitorGroup(monitorGroup, MonitiorLocations, filter);
        }

        public void PleaseRegister()
        {
            PleaseRegister((object) true);
        }

        public void PleaseRegister(object reminde)
        {
            bool reminder = (bool)reminde;
            PleaseRegisterForm plrefo = new PleaseRegisterForm(GetProgramNumber(), this, reminder);
            plrefo.ShowDialog();
            if (plrefo.DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                mainform.RemoveLicenseOption();
            }
            
        }



        public void NewDiscription(string MonitorGroup, VersionInfo version)
        {
            if (MonitorGroup.Equals(m_currentMonitorGroup))
            {
                foreach (FolderTrackCallBack flCall in CallList)
                {
                    try
                    {
                        flCall.NewDiscription(MonitorGroup, version);
                    }
                    catch (Exception)
                    {
                        //Don't care
                    }
                }
            }
        }

        #endregion
    }
}
