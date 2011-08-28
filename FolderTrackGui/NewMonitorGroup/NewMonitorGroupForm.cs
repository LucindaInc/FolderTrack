using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FolderTrackGuiTest1.Filters;
using FolderTrack.Types;
using ZetaLongPaths;

namespace FolderTrackGuiTest1.NewMonitorGroup
{
    public partial class NewMonitorGroupForm : Form
    {
        public class ErrorAc
        {
            public string location;
            public NewMonitorLocationRow ro;
        }

        public class FixMoniGr
        {
            public List<string> stringsRemoved;
            public Dictionary<string, List<string>> DoubleMon;
            public IList<string> FinStr;
        }

        public class NewLocationManger
        {

            public PanelList<String> NewMonitorLocationsPanelList;

            public List<string> MonitorLocation;

            public string NameOfMonitor;

            public List<string> FilterString;

            public Options filter;

            public Button OkButton;
            string NoLocation = "Must Add Folder or File";
            string LocationsExi = "OK";
            Dictionary<string,List<NewMonitorLocationRow>> RowOfLoc = new Dictionary<string,List<NewMonitorLocationRow>>();
            public delegate void VoidNoArgDelegate();

            public NewLocationManger(PanelList<String> NewMonitorLocationsPanelList, List<string> MonitorLocation, Button okButton)
            {
                this.NewMonitorLocationsPanelList = NewMonitorLocationsPanelList;
                this.MonitorLocation = MonitorLocation;
                FilterString = new List<string>();
                filter = new Options();
                filter.filterList = new List<string>();
                FolderTrack.Types.MGProperties temMGP = new FolderTrack.Types.MGProperties();
                filter.proper = temMGP.GetGuiProp();
                filter.filtCh = new FilterChangeOb();
                this.OkButton = okButton;
                HandleOkButton();
            }

            public void AddRowFromString(object ErrorAcJ)
            {
                ErrorAc er = (ErrorAc)ErrorAcJ;
                string location = er.location;
                NewMonitorLocationRow ro = er.ro;

                lock (RowOfLoc)
                {
                    if (RowOfLoc.ContainsKey(location) == false)
                    {
                        RowOfLoc[location] = new List<NewMonitorLocationRow>();
                    }

                    RowOfLoc[location].Add(ro);
                    ShowErrors();
                }
            }

            public void RemoveRowFromString(object ErrorAcJ)
            {

                ErrorAc er = (ErrorAc)ErrorAcJ;
                string location = er.location;
                NewMonitorLocationRow ro = er.ro;

                lock (RowOfLoc)
                {
                    if (RowOfLoc.ContainsKey(location) == false)
                    {
                        return;
                    }
                    RowOfLoc[location].Remove(ro);
                    ShowErrors();
                }
            }

            private void ShowErrors()
            {
                    foreach (KeyValuePair<string, List<NewMonitorLocationRow>> k in RowOfLoc)
                    {
                        foreach (NewMonitorLocationRow ro in RowOfLoc[k.Key])
                        {
                            ro.removeError();
                        }
                    }
                List <NewMonitorLocationRow> rol;
                    FixMoniGr gr = IdentifyDoubleMon(MonitorLocation);
                    if (gr.stringsRemoved != null)
                    {
                        foreach (string location in gr.stringsRemoved)
                        {
                            if (RowOfLoc.TryGetValue(location, out rol))
                            {
                                for (int i = 1; i < rol.Count; i++)
                                {
                                    try
                                    {
                                        RowOfLoc[location][i].showError(location + " was allready included");
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }

                    if (gr.DoubleMon != null)
                    {
                        string list;
                        foreach (KeyValuePair<string, List<string>> k in gr.DoubleMon)
                        {
                            list = "";
                            if (k.Value.Count > 1)
                            {
                                list = " by the following locations: \r\n";
                                foreach (string dloc in k.Value)
                                {
                                    list += dloc + "\r\n";
                                }
                            }
                            else if (k.Value.Count == 1)
                            {
                                list = " by " + k.Value[0];
                            }
                            if (RowOfLoc.TryGetValue(k.Key, out rol))
                            {
                                foreach (NewMonitorLocationRow r in rol)
                                {
                                    try
                                    {
                                        r.showError("You do not have to include " + k.Key + " because it is allready monitored" + list);
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                
            }

            public void AddLocation(string location)
            {
                MonitorLocation.Add(location);
                NewMonitorLocationsPanelList.AddDataBottom(location);
                HandleOkButton();
            }

             
            public void RemoveLocation(string location)
            {     
                MonitorLocation.Remove(location);
                NewMonitorLocationsPanelList.RemoveData(location);    
                ShowErrors();
                HandleOkButton();
            }

            public FixMoniGr IdentifyDoubleMon(IList<string> omoni)
            {
                List<string> moni = new List<string>(omoni);
                FixMoniGr groFox = new FixMoniGr();
                Dictionary<string, int> AmountToDelete = new Dictionary<string, int>();
                List<string> stringsRemoved = new List<string>();



                foreach (string loca in moni)
                {
                    if (AmountToDelete.ContainsKey(loca) == false)
                    {
                        AmountToDelete[loca] = -1;
                    }
                    AmountToDelete[loca]++;
                }

                foreach (KeyValuePair<string, int> k in AmountToDelete)
                {
                    for (int i = 0; i < k.Value; i++)
                    {
                        moni.Remove(k.Key);
                        if (stringsRemoved.Contains(k.Key) == false)
                        {
                            stringsRemoved.Add(k.Key);
                        }
                    }
                }
                groFox.stringsRemoved = stringsRemoved;

                Dictionary<string, List<string>> DoubleMon = new Dictionary<string, List<string>>();
                foreach (string loca in moni)
                {
                    string slThere = loca;
                    if (slThere.EndsWith("" + Path.DirectorySeparatorChar) == false)
                    {
                        slThere += Path.DirectorySeparatorChar;
                    }
                    foreach (string subst in moni)
                    {
                        if (slThere.StartsWith(subst) && loca.Equals(subst) == false)
                        {
                            if (DoubleMon.ContainsKey(loca) == false)
                            {
                                DoubleMon[loca] = new List<string>();
                            }

                            DoubleMon[loca].Add(subst);

                        }
                    }
                }

                foreach (KeyValuePair<string, List<string>> k in DoubleMon)
                {
                    moni.Remove(k.Key);
                }

                groFox.DoubleMon = DoubleMon;
                groFox.FinStr = moni;

                return groFox;


            }

            public void HandleOkButton()
            {
                try
                {
                    if (OkButton != null)
                    {
                        try
                        {
                            if (OkButton.InvokeRequired == true)
                            {
                                OkButton.Invoke(new VoidNoArgDelegate(HandleOkButton));
                                return;
                            }
                        }
                        catch
                        {
                            //thow away
                        }

                        if (MonitorLocation.Count == 0)
                        {
                            OkButton.Text = NoLocation;
                            OkButton.Enabled = false;
                            
                        }
                        else
                        {
                            OkButton.Text = LocationsExi;
                            OkButton.Enabled = true;
                        }
                    }
                }
                catch(Exception e)
                {
                    Util.DBug2("NewMonitorGroupForm", "Exception on OKButton " + e.ToString());
                }
            }

        }


        public NewLocationManger LocationManager;

        string NoPathAddText = "Enter Path to Folder or File";
        string PathPresent = "Add";
        delegate void  VoidNoArgDelegate();

        public NewMonitorGroupForm()
        {
            InitializeComponent();
            this.Height = (int)((double) SystemInformation.PrimaryMonitorSize.Height * .7);
            this.Width = (int)((double) SystemInformation.PrimaryMonitorSize.Width * .7);
            LocationManager = new NewLocationManger(new PanelList<string>(), new List<string>(), this.OkNewButton);
            LocationManager.OkButton = this.OkNewButton;
            PanelFromMonitorLocation pan = new PanelFromMonitorLocation();
            this.AddButton.Text = NoPathAddText;
            this.AddButton.Enabled = false;
            pan.NewLocationManger = LocationManager;
            LocationManager.NewMonitorLocationsPanelList.PanelFromData = pan;
            this.panel1.Controls.Add(LocationManager.NewMonitorLocationsPanelList);
            LocationManager.NewMonitorLocationsPanelList.Dock = DockStyle.Fill;
            this.DialogResult = DialogResult.Cancel;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Add Button Clicked in New Monitor Group Form with " + this.LocationTextBox.Text +" as the location");
            AddLocation();
        }

        public void AddLocation()
        {
            if (this.LocationTextBox.InvokeRequired)
            {
                this.LocationTextBox.Invoke(new VoidNoArgDelegate(AddLocation));
                return;
            }
            LocationManager.AddLocation(this.LocationTextBox.Text);
            this.NameTextBox.Text = ZlpPathHelper.GetFileNameFromFilePath(this.LocationTextBox.Text);
            this.LocationTextBox.Text = "";
        }

        private void AddFolderButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Add Folder Button Clicked in New Monitor Group Form");
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult res = folder.ShowDialog();

            if (res == DialogResult.OK)
            {
                this.LocationTextBox.Text = folder.SelectedPath;
                AddLocation();
            }
            folder.Dispose();
        }

        private void AddFileButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Add File Button Clicked in New Monitor Group Form");
            OpenFileDialog opfidia = new OpenFileDialog();
            opfidia.RestoreDirectory = true;
            DialogResult res = opfidia.ShowDialog();

            if (res == DialogResult.OK)
            {
                this.LocationTextBox.Text = opfidia.FileName;
                AddLocation();
            }
            opfidia.Dispose();
        }

        private void CanButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("Cancel Button Clicked in New Monitor Group Form");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void OkNewButton_Click(object sender, EventArgs e)
        {
            Util.UserDebug("OK Button Clicked in New Monitor Group Form");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            LocationManager.NameOfMonitor = NameTextBox.Text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Util.UserDebug("Help link clicked New Monitor Group Form");
            Help h = new Help();
            h.ShowDialog();
        }

        private void NewMonitorGroupForm_Paint(object sender, PaintEventArgs e)
        {
            Pen pe = new Pen(Color.BurlyWood, 12);
            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(Color.Peru);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(this.tableLayoutPanel1.Location.X, this.tableLayoutPanel1.Location.Y);
            pt[1] = new Point(this.tableLayoutPanel1.Location.X + this.tableLayoutPanel1.Size.Width - 3, this.tableLayoutPanel1.Location.Y);
            pt[2] = new Point(this.tableLayoutPanel1.Location.X + this.tableLayoutPanel1.Size.Width - 3, this.tableLayoutPanel1.Location.Y + this.tableLayoutPanel1.Size.Height - 3);
            pt[3] = new Point(this.tableLayoutPanel1.Location.X, this.tableLayoutPanel1.Location.Y + this.tableLayoutPanel1.Size.Height - 3);
            pt[4] = new Point(this.tableLayoutPanel1.Location.X, this.tableLayoutPanel1.Location.Y);

            e.Graphics.DrawLines(pe, pt);
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NameLabel_Click(object sender, EventArgs e)
        {

        }

        private void NewMonitorGroupForm_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Pen pe = new Pen(Color.BurlyWood, 12);
            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(Color.Peru);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(this.tableLayoutPanel2.Location.X, this.tableLayoutPanel2.Location.Y);
            pt[1] = new Point(this.tableLayoutPanel2.Location.X + this.tableLayoutPanel2.Size.Width - 3, this.tableLayoutPanel2.Location.Y);
            pt[2] = new Point(this.tableLayoutPanel2.Location.X + this.tableLayoutPanel2.Size.Width - 3, this.tableLayoutPanel2.Location.Y + this.tableLayoutPanel2.Size.Height - 3);
            pt[3] = new Point(this.tableLayoutPanel2.Location.X, this.tableLayoutPanel2.Location.Y + this.tableLayoutPanel2.Size.Height - 3);
            pt[4] = new Point(this.tableLayoutPanel2.Location.X, this.tableLayoutPanel2.Location.Y);

            e.Graphics.DrawLines(pe, pt);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Pen pe = new Pen(Color.BurlyWood, 12);
            pe.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            pe.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            Point[] pt = new Point[5];
            e.Graphics.Clear(Color.Peru);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pt[0] = new Point(this.tableLayoutPanel3.Location.X, this.tableLayoutPanel3.Location.Y);
            pt[1] = new Point(this.tableLayoutPanel3.Location.X + this.tableLayoutPanel3.Size.Width - 3, this.tableLayoutPanel3.Location.Y);
            pt[2] = new Point(this.tableLayoutPanel3.Location.X + this.tableLayoutPanel3.Size.Width - 3, this.tableLayoutPanel3.Location.Y + this.tableLayoutPanel3.Size.Height - 3);
            pt[3] = new Point(this.tableLayoutPanel3.Location.X, this.tableLayoutPanel3.Location.Y + this.tableLayoutPanel3.Size.Height - 3);
            pt[4] = new Point(this.tableLayoutPanel3.Location.X, this.tableLayoutPanel3.Location.Y);

            e.Graphics.DrawLines(pe, pt);
        }

        private void LocationTextBox_TextChanged(object sender, EventArgs e)
        {
            if (LocationTextBox.Text.Length > 0)
            {
                this.AddButton.Text = PathPresent;
                this.AddButton.Enabled = true;
            }
            else
            {
                this.AddButton.Text = NoPathAddText;
                this.AddButton.Enabled = false;
            }
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            List<string> FilterSt = new List<string>();
            FilterSt.AddRange(LocationManager.FilterString);
            Options o = LocationManager.filter;
            SelectFilterFromMonitorGroup sel = new SelectFilterFromMonitorGroup(o,LocationManager.MonitorLocation);
            sel.ShowDialog();
            if (sel.DialogResult == DialogResult.OK)
            {
                o.filtCh = sel.GetFilterList();
                
                

                LocationManager.filter = o;

                foreach (string f in o.filtCh.addFilfer)
                {
                    if (LocationManager.FilterString.Contains(f) == false)
                    {
                        LocationManager.FilterString.Add(f);
                    }
                }

            }
        }
    }
}