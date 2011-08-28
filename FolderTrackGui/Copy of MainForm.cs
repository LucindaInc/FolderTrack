using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Threading;
using System.Diagnostics;

namespace FolderTrackGuiTest1
{
    public partial class MainForm : Form, FolderTrack.WCFContracts.FolderTrackCallBack
    {
        
        private FTObjects m_FTObjects;


        
        private VersionRow CurrentVerionRow;

        private Dictionary<int, VersionRow> m_VersionRowFromIndex;

        //The height of a version Row
        private int VersionRowSpaceH;

        //The width of a version Row
        private int VersionRowSpaceW;


        //Extra space between each row 
        private const int ExtraVersionRowSpace = 25;

        private object BlockAddingAndRemovingVersionRows;

        private bool ExitRemoveKey;

        private delegate void DisplayVersionsDelegate();

        private delegate void RemoveFarAwayVersionRowDelegate();

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(FTObjects ftobjects)
        {
            InitializeComponent();

            //Determine VersionRowSpace
            VersionRow versionrow = new VersionRow();
            ExitRemoveKey = false;
            BlockAddingAndRemovingVersionRows = new object();
            VersionRowSpaceH = versionrow.Height + ExtraVersionRowSpace;
            VersionRowSpaceW = versionrow.Width;
            versionrow.Dispose();

            m_VersionRowFromIndex = new Dictionary<int, VersionRow>();
            this.m_FTObjects = ftobjects;
            m_FTObjects.AddToCallList(this);

            MonitorGroupName = "test";
            this.ListVersionvScrollBar.Maximum = (VersionRowSpaceH * 2) *  m_FTObjects.CurrentAmountOfVersions;

            DisplayVersions();
            

        }

        public string MonitorGroupName
        {
            get
            {
                return m_FTObjects.CurrentMonitorGroup;
            }

            set
            {
                m_FTObjects.CurrentMonitorGroup = value;
            }
        }


        private void DisplayVersions()
        {
            ExitRemoveKey = true;
            lock (BlockAddingAndRemovingVersionRows)
            {
                
                    int index = 0;

                    List<int> HeightSquares = SquaresSpaces(this.ListVersionvScrollBar.Value, this.VersionPanel.Height, VersionRowSpaceH);

                    VersionInfo versionInfo;
                    VersionRow versionRow;

                    foreach (int height in HeightSquares)
                    {
                        index = height / VersionRowSpaceH;

                        if (index <= m_FTObjects.CurrentAmountOfVersions)
                        {
                            versionInfo = m_FTObjects.CurrentVersionList[index];

                            if (m_VersionRowFromIndex.TryGetValue(index, out versionRow) == false)
                            {
                                versionRow = new VersionRow(versionInfo, m_FTObjects);
                                versionRow.Location = new Point(ExtraVersionRowSpace, ExtraVersionRowSpace + height - this.ListVersionvScrollBar.Value);
                                lock (m_VersionRowFromIndex)
                                {
                                    m_VersionRowFromIndex[index] = versionRow;
                                }
                                this.ListVersionPanel.Controls.Add(versionRow);
                                if (versionInfo.versionName.Equals(m_FTObjects.CurrentVersion))
                                {
                                    versionRow.IndicateCurrentVersion();
                                    CurrentVerionRow = versionRow;
                                }
                            }
                            else
                            {
                                versionRow.Location = new Point(ExtraVersionRowSpace, ExtraVersionRowSpace + height - this.ListVersionvScrollBar.Value);
                            }
                        }
                    }

            }
            ExitRemoveKey = false;


        }


        private void RemoveFarAwayVersionRow()
        {
            lock (BlockAddingAndRemovingVersionRows)
            {
                
                    List<int> HeightSquares = SquaresSpaces(this.ListVersionvScrollBar.Value, this.VersionPanel.Height + this.ListVersionvScrollBar.Value, VersionRowSpaceH);


                    if (HeightSquares.Count == 0)
                    {
                        return;
                    }

                    List<int> KeysToRemove = new List<int>();
                    foreach (KeyValuePair<int, VersionRow> vals in m_VersionRowFromIndex)
                    {
                        //If a version row is more than 2 displays away either high or low
                        if (
                            vals.Key < ((HeightSquares[0] - (2 * HeightSquares.Count)) / VersionRowSpaceH)
                            ||
                            vals.Key > ((HeightSquares[HeightSquares.Count - 1] + (2 * HeightSquares.Count)) / VersionRowSpaceH)
                            )
                        {
                            KeysToRemove.Add(vals.Key);
                        }
                        if (ExitRemoveKey == true)
                        {
                            return;
                        }
                    }

                    VersionRow versionRowToRemove;

                    foreach (int keytoremove in KeysToRemove)
                    {
                        if (m_VersionRowFromIndex.TryGetValue(keytoremove, out versionRowToRemove))
                        {
                            versionRowToRemove.Dispose();
                            m_VersionRowFromIndex.Remove(keytoremove);
                        }
                        if (ExitRemoveKey == true)
                        {
                            return;
                        }
                    }
                

            }
        }

        [DebuggerHidden]
        public List<int> SquaresSpaces(int StartPosition, int SizeOfArea, int Space)
        {

            int UseStart = StartPosition - (StartPosition % Space);


            List<int> ReturnList = new List<int>();
            for (int i = UseStart; i < SizeOfArea + StartPosition; i = i + Space)
            {
                ReturnList.Add(i);
            }
            return ReturnList;
        }


        private void InvokeDisplayVersions()
        {
            try
            {
                this.ListVersionPanel.Invoke(new DisplayVersionsDelegate(DisplayVersions));
                this.ListVersionPanel.Invoke(new RemoveFarAwayVersionRowDelegate(RemoveFarAwayVersionRow));
                this.ListVersionPanel.Invalidate();
                this.ListVersionPanel.Update();
            }
            catch (InvalidOperationException)
            {
                //Ignore this 
            }
        }

        private void ListVersionvScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            new Thread(InvokeDisplayVersions).Start();
        }

        private void ListVersionPanel_Resize(object sender, EventArgs e)
        {
            new Thread(InvokeDisplayVersions).Start();
        }

        [DebuggerHidden]
        private void ListVersionPanel_Paint(object sender, PaintEventArgs e)
        {
            List<int> HeightSquares = SquaresSpaces(this.ListVersionvScrollBar.Value, this.VersionPanel.Height, VersionRowSpaceH);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(Color.White);
            Pen pen = new Pen(Color.Black, 12);
            pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;

            Point[] points = new Point[6];
            foreach (int height in HeightSquares)
            {

                points[0].X = ExtraVersionRowSpace;
                points[0].Y = height - this.ListVersionvScrollBar.Value + ExtraVersionRowSpace;

                points[1].X = ExtraVersionRowSpace + VersionRowSpaceW;
                points[1].Y = height - this.ListVersionvScrollBar.Value + ExtraVersionRowSpace;

                points[2].X = ExtraVersionRowSpace + VersionRowSpaceW;
                points[2].Y = height - this.ListVersionvScrollBar.Value + VersionRowSpaceH; 

                points[3].X = ExtraVersionRowSpace;
                points[3].Y = height - this.ListVersionvScrollBar.Value + VersionRowSpaceH;

                points[4].X = ExtraVersionRowSpace;
                points[4].Y = height - this.ListVersionvScrollBar.Value + ExtraVersionRowSpace;

                points[5].X = ExtraVersionRowSpace + VersionRowSpaceW;
                points[5].Y = height - this.ListVersionvScrollBar.Value + ExtraVersionRowSpace;


                e.Graphics.DrawLines(pen, points);
            }

            pen.Dispose();
        }


        #region FolderTrackCallBack Members

        public void NewCurrentVersion(string MonitorGroup, VersionInfo vers)
        {
            if (CurrentVerionRow != null && CurrentVerionRow.IsDisposed == false)
            {
                CurrentVerionRow.UnIndicateCurrentVersion();
            }
            foreach (KeyValuePair<int,VersionRow> kVer in m_VersionRowFromIndex)
            {
                if(kVer.Value.PublicVersionInfo.versionName.Equals(vers.versionName))
                {
                    kVer.Value.IndicateCurrentVersion();
                    CurrentVerionRow = kVer.Value;
                    break;
                }
            }
        }

        public void NewVersion(string MonitorGroup, VersionInfo vers)
        {
            //TODO
        }

        #endregion
    }
}