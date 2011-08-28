using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using FolderTrack.WCFContracts;
using FolderTrack.Types;
using System.ServiceModel.Description;
using System.Threading;
using FolderTrackGuiTest1.License;
using FolderTrack.Info;
using FolderTrack.Delete;


namespace FolderTrackGuiTest1
{
    
    public class DataReceiver : FolderTrack.WCFContracts.FolderTrackCallBack
    {
        private static string END_POINT_ADDRESS = "net.pipe://localhost/FolderTrack";
        private static FolderTackContract m_FolderTrack;
        private static DuplexChannelFactory<FolderTackContract> FolderTrackFac;
        private static EndpointAddress address;
        private string[] monitorGroupList;
        private Dictionary<String,IList<String>> LocationListFromMonitorGroup;
        private Dictionary<string,DataManager> DataManagerFromName;
        private Dictionary<string, FolderTrack.Delete.DeleteRules.DeleteReturn> DelereReturnFromName;
        private string ProgramNumber= null;
        private List<FolderTrack.WCFContracts.FolderTrackCallBack> CallList;
        //  private static 
        Timer t;
        private MainForm m_FolderTrackGui;
        private Dictionary<string, bool> NameIsReady = new Dictionary<string, bool>();
        private const int DONT_KEEP = -1;
        private int pSendToAll;

        public int SendToAll
        {
            get
            {
                return pSendToAll;
            }
        }
        

        public DataReceiver()
        {
      //      m_FolderTrackGui = FolderTrackGui;
      //      m_FolderTrackGui.m_Datareceiver = this;
            LocationListFromMonitorGroup = new Dictionary<string,IList<string>>();
            DataManagerFromName = new Dictionary<string, DataManager>();
            DelereReturnFromName = new Dictionary<string, DeleteRules.DeleteReturn>();
            Util.DBug2("DataReceiver", "Calling connect");
            connect();
            Util.DBug2("DataReceiver", "connect returned");
            Util.DBug2("DataReceiver", "Calling GetMonitorGroupVersions");
            GetMonitorGroupVersions();
            Util.DBug2("DataReceiver", "GetMonitorGroupVersions returned");
            CallList = new List<FolderTrackCallBack>();
            t = new Timer(new TimerCallback(CallFolderTrack), null, TimeSpan.FromMinutes(9), TimeSpan.FromMinutes(9));
            

        }

        ~DataReceiver()
        {
            t.Dispose();
        }

        public bool NewMonitorGroup(string monitorGroup, IList<string> MonitorLocations, Options filter)
        {
            string returnS = pFolderTrack.NewMonitorGroup(monitorGroup, MonitorLocations, filter);
            ReadyStates redSt;
            if (returnS != null)
            {
                do
                {
                    Thread.Sleep(10);
                    redSt = pFolderTrack.ready(monitorGroup);
                } while (redSt == ReadyStates.WORKING);
                if (redSt == ReadyStates.READY)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

        }


        public void AddToCallList(FolderTrackCallBack addCall)
        {
            CallList.Add(addCall);
        }

        private string[] GetMonitorGroupVersions()
        {
            Util.DBug2("DataReceiver", "Calling FGetListOfMonitorGroup");
            monitorGroupList = pFolderTrack.GetListOfMonitorGroup();
            Util.DBug2("DataReceiver", "FGetListOfMonitorGroup Returned");
            return monitorGroupList;
        }

        public IList<string> LocationsInMonitorGroup(string MonitorGroupName)
        {
            IList<string> returnList;

            if (LocationListFromMonitorGroup.TryGetValue(MonitorGroupName, out returnList) == false)
            {
                returnList = m_LocationsInMonitorGroup(MonitorGroupName);
            }

            return returnList;
        }

        public void OptionChanges(string MonitorGroupName, Options o)
        {
            pFolderTrack.OptionChanges(MonitorGroupName, o);
        }

        private IList<String> m_LocationsInMonitorGroup(string MonitorGroupName)
        {
            LocationListFromMonitorGroup[MonitorGroupName] = pFolderTrack.GetLocationsInMonitorGroup(MonitorGroupName);
            return LocationListFromMonitorGroup[MonitorGroupName];
        }

        public string GetCurrentVersionName(string MonitorGroupName)
        {
            string returnCurVers;
            Util.DBug2("MainForm", "Call GetCurrentVersion");
            returnCurVers = pFolderTrack.GetCurrentVersion(MonitorGroupName);
            Util.DBug2("MainForm", "GetCurrentVersion Returned");
          //  Functions.SerializeToFile<string>("returnCurVers",returnCurVers);
         //   returnCurVers = Functions.DeserializeFromFile<string>("returnCurVers");

            return returnCurVers;
        }

        public void RemoveStopUse(string MonitorGroupName, string UserVerName, string vers)
        {
            pFolderTrack.RemoveStopUserVersion(MonitorGroupName, UserVerName, vers);
        }


        public DataManager GetDataManagerFromName(string MonitorGroupName, FolderTrack.Delete.DeleteRules.DeleteReturn delret)
        {
            DataManager returnDataManager;
            Util.DBug2("DataReceiver", "Enter GetDataManagerFromName");
            bool correctDeleRetu = false;
            FolderTrack.Delete.DeleteRules.DeleteReturn outDelR;
            if (DelereReturnFromName.TryGetValue(MonitorGroupName, out outDelR))
            {
                if (outDelR == delret)
                {
                    correctDeleRetu = true;
                }
            }
            if (DataManagerFromName.TryGetValue(MonitorGroupName, out returnDataManager) &&
                correctDeleRetu)
            {
                Util.DBug2("DataReceiver", "Enter Manager found");
                pSendToAll = DONT_KEEP;
                SendDataV(new List<VersionInfo>(returnDataManager.VersionList.Values), MonitorGroupName, pSendToAll, true);
                return returnDataManager;
            }
            else
            {
                DataManagerFromName[MonitorGroupName] = new DataManager();
                DelereReturnFromName[MonitorGroupName] = delret;

                Util.DBug2("DataReceiver", "Call getMonitorGroupVersionsOf");
                pSendToAll = getMonitorGroupVersionsOf(MonitorGroupName, delret);
                Util.DBug2("DataReceiver", "getMonitorGroupVersionsOf returned");
                return DataManagerFromName[MonitorGroupName];
            }
        }

        private void connect()
        {
            Util.DBug2("DataReceiver", "address");
            address = new EndpointAddress(END_POINT_ADDRESS);
            Util.DBug2("DataReceiver", "binding");
            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            Util.DBug2("DataReceiver", "SendTimeout");
            binding.SendTimeout = TimeSpan.FromMinutes(10);
            Util.DBug2("DataReceiver", "ReceiveTimeout");
            binding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            Util.DBug2("DataReceiver", "MaxReceivedMessageSize");
            binding.MaxReceivedMessageSize = 2147483647;
            Util.DBug2("DataReceiver", "DuplexChannelFactory");
            FolderTrackFac = new DuplexChannelFactory<FolderTackContract>(new InstanceContext(this), binding);
            Util.DBug2("DataReceiver", "operattions");
            foreach (OperationDescription op in FolderTrackFac.Endpoint.Contract.Operations)
            {
                Util.DBug2("DataReceiver", "Behavior");
                DataContractSerializerOperationBehavior dataContractBehavior =
                op.Behaviors.Find<DataContractSerializerOperationBehavior>()
                as DataContractSerializerOperationBehavior;
                if (dataContractBehavior != null)
                {
                    Util.DBug2("DataReceiver", "dataContract");
                    dataContractBehavior.MaxItemsInObjectGraph = 2147483647;
                }

            }
            Util.DBug2("DataReceiver", "CreateChannel");
            m_FolderTrack = FolderTrackFac.CreateChannel(address);
        }

        public FolderUnit[] GetFolderUnit(string MonitorGroupName, VersionInfo version)
        {
            return pFolderTrack.GetFolderUnit(MonitorGroupName, version);
        }

        public void UseFolderUnit(string MonitorGroupName, VersionInfo version, string VersionLocation)
        {
            pFolderTrack.UseFolderUnit(MonitorGroupName, version, VersionLocation);
        }

        public Object CopyFolderUnit(string MonitorGroupName, VersionInfo version, string VersionLocation, String CopyLocation)
        {
            return pFolderTrack.CopyFolderUnit(MonitorGroupName, version, VersionLocation, CopyLocation);
        }

        /// <summary>
        /// Returns the list 
        /// </summary>
        /// <param name="nameOfMonitorGroup"></param>
        /// <param name="version"></param>
        /// <param name="VersionLocation"></param>
        /// <returns></returns>
        public IList<ChangeInstruction> GetFolderUnitInfo(String nameOfMonitorGroup, VersionInfo version, string VersionLocation)
        {
            return pFolderTrack.GetFolderUnitInfo(nameOfMonitorGroup, version, VersionLocation);
        }

        /// <summary>
        /// Returns an int and the data is sent be SendData
        /// </summary>
        /// <param name="MonitorGroup"></param>
        /// <param name="version"></param>
        /// <param name="VersionLocation"></param>
        /// <returns></returns>
        public int FolderUnitsVersionU(string MonitorGroup, VersionInfo version, string VersionLocation)
        {
            return pFolderTrack.GetVersionsOfFolderU(MonitorGroup, version, VersionLocation);
        }

        public void DeleteRules(string MonitorGroup, DeleteRules rules)
        {
            pFolderTrack.DeleteRules(MonitorGroup, rules);
        }

        public DeleteRules getDeleteRules(string MonitorGroup)
        {
            return pFolderTrack.getDeleteRules(MonitorGroup);
        }

        public void CopyVersion(String MonitorGroupName, String version, Dictionary<String, String> ToLocations)
        {
            pFolderTrack.CopyVersion(MonitorGroupName, version, ToLocations);
        }

        public void SetFilter(String MonirotGroupName, FilterChangeOb filt)
        {
            pFolderTrack.SetFilter(MonirotGroupName, filt);
        }


        
        public int getMonitorGroupVersionsOf(string MonitorGroup, FolderTrack.Delete.DeleteRules.DeleteReturn delret)
        {
            return pFolderTrack.GetFullMonitorGroupVerUD(MonitorGroup, delret);
        }

        private void getMonitorGroupVersionsOfOld(string MonitorGroup)
        {
            Util.DBug2("DataReceiver", "Call FolderTrack.GetFullMonitorGroupVer " + MonitorGroup);
            List<VersionInfo> monitorGroupVersions;
            do
            {
                monitorGroupVersions = pFolderTrack.GetFullMonitorGroupVer(MonitorGroup);
                Util.DBug2("DataReceiver", "Returned FolderTrack.GetFullMonitorGroupVer " + MonitorGroup);
                //     List<VersionInfo> monitorGroupVersions = Functions.DeserializeFromFile<List<VersionInfo>>("monitorGroupVers.dat");
                //    Functions.SerializeToFile<List<VersionInfo>>("monitorGroupVers.dat",monitorGroupVersions);
                if (monitorGroupVersions == null)
                {
                    Thread.Sleep(2000);
                }
            } while (monitorGroupVersions == null);
            
           DataManagerFromName[MonitorGroup] = new DataManager();
            Util.DBug2("DataReceiver", "Call Create new manager ");
            DataManagerFromName[MonitorGroup].AddVersionInfo(monitorGroupVersions);
            Util.DBug2("DataReceiver", "Call add version");
            Util.DBug2("DataReceiver", "Call Userversions");
            List<string> UserVersions = pFolderTrack.UserVersionList(MonitorGroup);
            Util.DBug2("DataReceiver", "Userversions returned");
            if (UserVersions == null)
            {
                Util.DBug2("DataReceiver", "Userversions is null");
            }
            DataManagerFromName[MonitorGroup].setUserVersions(UserVersions);
            Util.DBug2("DataReceiver", "Set user returned");
    //        m_FolderTrackGui.setVersionsOfMonitorGroup(MonitorGroup,monitorGroupVersions);

        }

        public void setFreeText(string MonitorGroup,string version, string FreeText)
        {
            pFolderTrack.SetFreeTextForVersion(MonitorGroup, version, FreeText);
        }

        public void addUserVersion(string MonitorGroup, string version, List<string> UserVersion, bool lastUserVersC)
        {
            pFolderTrack.AddUserVersionForVersion(MonitorGroup, version, UserVersion, lastUserVersC);
        }

        public void setStopVersion(string MonitorGroup, List<string> UserVerNames, string version, bool ExcludeNow, bool lastUserVersC)
        {

            pFolderTrack.StopUserVersion(MonitorGroup, UserVerNames, version, ExcludeNow, lastUserVersC);
        }


        public bool GetDontMonitor(string MonitorGroup)
        {
            return pFolderTrack.DontMonitor(MonitorGroup);
        }

        public void TaskAnswer(int TyeNumber)
        {
            pFolderTrack.TaskAnswer(TyeNumber);
        }



        public void useVersion(string MonitorGroup,string Version)
        {
            pFolderTrack.setVersion(MonitorGroup, Version);
        }

        

        public List<SearchResults> Search(string MonitorGroup, string search)
        {
            List<SearchResults> SearchResults = pFolderTrack.DoSearch(MonitorGroup, search);
            return SearchResults;
        }
        public List<string> GetFilters(string MonitorGroup)
        {
            return pFolderTrack.GetFilters(MonitorGroup);
        }

        public void DeleteVersion(string MonitorGroup, VersionInfo version)
        {
            pFolderTrack.DeleteVersion(MonitorGroup, version);
        }

        public void UndeleteVersion(string MonitorGroup, VersionInfo version)
        {
            pFolderTrack.UndeleteVersion(MonitorGroup, version);
        }

        public void DeleteVersion(string MonitorGroup, List<VersionInfo> versionL)
        {
            pFolderTrack.DeleteVersionList(MonitorGroup, versionL);
        }

        public void UndeleteVersion(string MonitorGroup, List<VersionInfo> versionL)
        {
            pFolderTrack.UndeleteVersionList(MonitorGroup, versionL);
        }

        public string GetProgramNumber()
        {
            if (ProgramNumber == null)
            {
                ProgramNumber = pFolderTrack.GetProgramNumber();
            }
            return ProgramNumber;
        }

        public bool SetLicense(string lc)
        {
            return pFolderTrack.SetLicense(lc);
        }

        public Information GetInformation()
        {
            return pFolderTrack.GetInfo();
        }

        public bool CheckLicense()
        {
            return pFolderTrack.CheckLicense();
        }

        public void SetUserVersion(string MonitorGroup, string version, UserVersionSet set)
        {
            pFolderTrack.SetUserVersion(MonitorGroup, version, set);
        }

        private void CallFolderTrack(object test)
        {
            FolderTackContract f = pFolderTrack;
        }

        public Options GetOptions(string MonitorGroup)
        {
            return pFolderTrack.GetOptions(MonitorGroup);
        }

        


        private FolderTackContract pFolderTrack
        {
            get
            {
                try
                {
                    m_FolderTrack.Test();
                }
                catch (EndpointNotFoundException)
                {
                    //inform user that the service is not running. Maybe try to start it
                }
                catch (CommunicationObjectFaultedException)
                {
                    m_FolderTrack = FolderTrackFac.CreateChannel(address);
                }
                catch (CommunicationException)
                {
                    m_FolderTrack = FolderTrackFac.CreateChannel(address);
                    m_FolderTrack.Test();
                }

                return m_FolderTrack;

            }
        }





        #region IFolderTrackCallBack Members

        public void NewVersion(string MonitorGroup, List<VersionInfo> vers)
        {
            DataManager temp;

            if (DataManagerFromName.TryGetValue(MonitorGroup, out temp))
            {
                temp.AddVersionInfo(vers);
            }
            if (CallList == null)
            {
                return;
            }
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.NewVersion(MonitorGroup, vers);
                }
                catch(Exception)
                {
                    //Don't care
                }
            }

        }

        public void DeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            DataManager datma;
            if (DataManagerFromName.TryGetValue(MonitorGroup, out datma))
            {
                datma.UpdateRemove(version);
            }
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.DeleteVersionC(MonitorGroup, version);
                }
                catch (Exception)
                {
                    //Don't care
                }
            }
        }


        public void DeleteVersionCList(string MonitorGroup, List<VersionInfo> versions)
        {
            DataManager datma;
            if (DataManagerFromName.TryGetValue(MonitorGroup, out datma))
            {
                foreach (VersionInfo version in versions)
                {
                    datma.UpdateRemove(version);
                }
            }
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.DeleteVersionCList(MonitorGroup, versions);
                }
                catch (Exception)
                {
                    //Don't care
                }
            }
        }


        public void UndeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            DataManager datma;
            if (DataManagerFromName.TryGetValue(MonitorGroup, out datma))
            {
                datma.UpdateRemove(version);
            }
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


        public void UndeleteVersionCList(string MonitorGroup, List<VersionInfo> versions)
        {
            DataManager datma;
            if (DataManagerFromName.TryGetValue(MonitorGroup, out datma))
            {
                foreach (VersionInfo version in versions)
                {
                    datma.UpdateRemove(version);
                }
            }
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.UndeleteVersionCList(MonitorGroup, versions);
                }
                catch (Exception)
                {
                    //Don't care
                }
            }
        }

        public void PleaseRegister()
        {
            new Thread(SendPleaseRegister).Start();
        }

        private void SendPleaseRegister()
        {
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.PleaseRegister();
                }
                catch (Exception)
                {
                    //Don't care
                }
            }
        }

        public void SendNewUserVersion(string MonitorGroup, List<string> UserVers)
        {
            DataManager temp;

            if (DataManagerFromName.TryGetValue(MonitorGroup, out temp))
            {
                temp.addUserVersion(UserVers);
            }
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

        public bool NameReady(string name)
        {
            if (NameIsReady.ContainsKey(name))
            {
                if (NameIsReady[name])
                {
                    return true;
                }
            }
            return false;
        }

        class SendDat
        {
            public List<ChangeInstruction> data;
            public int idnum;
            public bool done;
        }  

        public void SendData(List<ChangeInstruction> data, int idnum, bool done)
        {
            SendDat send = new SendDat();
            send.data = data;
            send.idnum = idnum;
            send.done = done;

            new Thread(new ParameterizedThreadStart(tSendData)).Start(send);
        }

        public void tSendData(object sendO)
        {
            SendDat send = (SendDat) sendO;
            List<ChangeInstruction> data = send.data;
            int idnum = send.idnum;
            bool done = send.done;

            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.SendData(data, idnum, done);
                }
                catch (Exception)
                {
                    //Dont care
                }
            }
        }

        class SendDatV
        {
            public List<VersionInfo> data;
            public string name;
            public bool done;
            public int idnum;
        }
        public void SendDataV(List<VersionInfo> data, string name, int idnum, bool done)
        {
            
                SendDatV send = new SendDatV();
                send.data = data;
                send.name = name;
                send.done = done;
                send.idnum = idnum;

                new Thread(new ParameterizedThreadStart(tSendDataV)).Start(send);
            
        }

        private void tSendDataV(object sendO)
        {
            
            SendDatV send = (SendDatV)sendO;

            List<VersionInfo> data = send.data;
            int idnum = send.idnum;
            string name = send.name;
            bool done = send.done;


            if (idnum == SendToAll && idnum != DONT_KEEP)
            {
                DataManager datam;
                NameIsReady[name] = false;
                if (DataManagerFromName.TryGetValue(name, out datam))
                {
                    datam.AddVersionInfo(data);
                }
                else
                {
                    DataManagerFromName[name] = new DataManager();
                    DataManagerFromName[name].AddVersionInfo(data);
                }


                NameIsReady[name] = done;
            }


            foreach (FolderTrackCallBack flCall in CallList)
            {
                flCall.SendDataV(data, name, idnum, done);
            }
            
            
        }

        

        public void NewCurrentVersion(string MonitorGroup, VersionInfo vers)
        {
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.NewCurrentVersion(MonitorGroup, vers);
                }
                catch (Exception)
                {
                    //Dont care
                }
            }
        }


        /// <summary>
        /// Expects ever versioninfo
        /// </summary>
        /// <param name="MonitorGroup"></param>
        /// <param name="vers"></param>
        public void NewVersionInformation(string MonitorGroup, List<VersionInfo> vers)
        {
            DataManager newData;
            if (DataManagerFromName.TryGetValue(MonitorGroup, out newData))
            {
                newData.ClearAll();
                newData.AddVersionInfo(vers);
            }
            else
            {
                DataManagerFromName[MonitorGroup] = new DataManager();
                DataManagerFromName[MonitorGroup].AddVersionInfo(vers);
            }
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.NewVersionInformation(MonitorGroup, vers);
                }
                catch (Exception)
                {
                    //Dont care
                }
            }
        }

        public void DontMonitor(string MonitorGroup)
        {
            if (CallList == null)
            {
                return;
            }
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.DontMonitor(MonitorGroup);
                }
                catch (Exception)
                {
                    //Dont care
                }
            }
        }

        public void RestartMonitor(string MonitorGroup)
        {
            if (CallList == null)
            {
                return;
            }
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.RestartMonitor(MonitorGroup);
                }
                catch (Exception)
                {
                    //Dont care
                }
            }
        }

        public void StopMonitoringGroup(string MonitorGroup)
        {
            pFolderTrack.StopMonitoringGroup(MonitorGroup);
        }

        public void RestartMonitoringGroup(string MonitorGroup)
        {
            pFolderTrack.RestartMonitoringGroup(MonitorGroup);
        }

        public void DeletMonitoringGroup(string MonitorGroup)
        {
            DataManagerFromName.Remove(MonitorGroup);   
            pFolderTrack.DeleteMonitoringGroup(MonitorGroup);
        }

        public List<MonitorGroupInfo> GetAllMonitorGroupInfo()
        {
            return pFolderTrack.GetAllMonitorGroupInfo();
        }

        public void Close()
        {
            FolderTrackFac.Close();
        }



        



        public void TaskUpdate(TaskGroup[] task)
        {
            if(CallList == null)
            {
                return;
            }
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.TaskUpdate(task);
                }
                catch (Exception)
                {
                    //Dont care
                }
            }
        }
        


        public void NewDiscription(string MonitorGroup, VersionInfo version)
        {
            DataManager temp;
            if (DataManagerFromName.TryGetValue(MonitorGroup, out temp))
            {
                temp.UpDateDiscription(version);
            }
            foreach (FolderTrackCallBack flCall in CallList)
            {
                try
                {
                    flCall.NewDiscription(MonitorGroup, version);
                }
                catch (Exception)
                {
                    //Dont care
                }
            }

        }

        

        #endregion


    }
}
