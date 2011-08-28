using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Threading;

namespace FolderTrackGuiTest1
{
    public partial class VersionRelationShipTress : Form
    {
        VersionBoxManager mana;
        Dictionary<string, VersionLeaf> VersionLeafFromString;
        DataManager m_DataManager;
        List<string> VersionL;
        Button focuBu;

        [Serializable()]
        public class VersionBoxManager
        {
            public int rowMax;
            public int colMax;

            public Dictionary<string, VersionBox> VersionBoxFromRowCol = new Dictionary<string, VersionBox>();

            public Dictionary<string, VersionBox> VersionBoxFromVersion = new Dictionary<string, VersionBox>();

            public List<VersionBox> returnChange;
            public List<BoxLoc> removeBox;
            public List<VersionBox> returnChangeAdd(VersionInfo versio)
            {
                returnChange = null;
                removeBox = null;
                returnChange = new List<VersionBox>();
                removeBox = new List<BoxLoc>();
                addVersion(versio);
                return returnChange;
            }

            public VersionBox getVersionBoxOfVersion(string version)
            {
                VersionBox ReturnVersionBox;
                VersionBoxFromVersion.TryGetValue(version, out ReturnVersionBox);
                if (ReturnVersionBox == null)
                {
                    ReturnVersionBox = addVersion(version);
                }
                return ReturnVersionBox;
            }

            public void rowChan(int row)
            {
                if (row > rowMax)
                {
                    returnChange = null;
                    removeBox = null;
                    rowMax = row;
                }
            }

            public void colChan(int col)
            {
                if (col > colMax)
                {
                    colMax = col;
                }
            }

            private VersionBox addVersion(string version)
            {

                return addVersion(new VersionInfo(version));
            }

            public VersionBox addVersion(VersionInfo version)
            {
                VersionBox ReturnVersionBox;
                VersionBoxFromVersion.TryGetValue(version.versionName, out ReturnVersionBox);
                if (ReturnVersionBox == null)
                {
                    ReturnVersionBox = new VersionBox(this, version);
                    VersionBoxFromVersion[version.versionName] = ReturnVersionBox;
                    string Mother = DeCostaNumbers.GetMother(version.versionName);
                    if (Mother.Equals("-1") == false)
                    {
                        VersionBox MotherVersionBox;
                        VersionBoxFromVersion.TryGetValue(Mother, out MotherVersionBox);
                        if (MotherVersionBox == null)
                        {
                            MotherVersionBox = addVersion(Mother);
                        }
                        MotherVersionBox.ColSpan += 1;
                        ReturnVersionBox.m_Mother = MotherVersionBox;
                    }
                    return ReturnVersionBox;
                }
                else
                {
                    ReturnVersionBox.m_version = version;
                    return ReturnVersionBox;
                }

            }

            public int Row
            {
                get
                {
                    return rowMax;
                }
            }

            public int Col
            {
                get
                {
                    return colMax;
                }
            }
        }

        public class BoxLoc
        {
            public BoxLoc(int row, int col)
            {
                this.row = row;
                this.col = col;
            }
            public int row;
            public int col;
        }

        [Serializable()]
        public class VersionBox
        {
            private int m_row;
            private int m_col;
            private int m_ColSpan;
            public VersionInfo m_version;
            public VersionBox m_Mother;
            public VersionBox m_LittleSister;
            public VersionBox m_FirstDaughter;
            VersionBoxManager m_Manger;
            public List<VersionBox> m_Daughters;

            public VersionBox(VersionBoxManager Manger, VersionInfo version)
            {
                m_row = 0;
                m_col = 0;
                m_version = version;
                m_Manger = Manger;
                m_ColSpan = 1;
                m_Daughters = new List<VersionBox>();
                VersionBox verb = null;
                if (version.versionName.Equals("0") == false)
                {
                    string ds = DeCostaNumbers.GetMother(version.versionName);
                    verb = m_Manger.getVersionBoxOfVersion(ds);
                    verb.m_Daughters.Add(this);
                }

                Row = calcRow(m_version.versionName);
                if (m_version.versionName.Equals("1"))
                {
                    m_version.versionName = "1";
                }
                string sisterVersion = DeCostaNumbers.GetBigSister(version.versionName);
                if (sisterVersion.Equals(DeCostaNumbers.AUNT))
                {
                    if (m_Mother == null)
                    {
                        verb.m_FirstDaughter = this;
                        Col = verb.Col;
                    }
                }
                else if (sisterVersion.Equals(DeCostaNumbers.NONE))
                {
                    Col = 0;
                }
                else
                {
                    VersionBox Sister = m_Manger.getVersionBoxOfVersion(sisterVersion);
                    Sister.m_LittleSister = this;
                    Col = 1 + Col + Sister.Col + Sister.ColSpan;
                }
            }

            public static int calcRow(string version)
            {
                string[] VersionSplit = version.Split('.');
                string numberS;
                int ReturnRow = 0;
                foreach (string VerSpl in VersionSplit)
                {
                    numberS = DeCostaNumbers.RemoveLetters(VerSpl);
                    ReturnRow = ReturnRow + Convert.ToInt32(numberS);
                }
                return ReturnRow;
            }

            public void Notify(VersionBox vers)
            {
                //     m_NotifyList.Add(vers);
            }

            public void NewCol(int col)
            {
                Col += col;
            }

            public void DaughterColSpan(int colSpa)
            {
                if ((colSpa - Col) > ColSpan)
                {
                    ColSpan = colSpa - Col;
                }
            }

            public void FirstDaughter(int col)
            {
                Col = col;
            }

            public void SisterCol(int col)
            {
                Col = col + 1;
            }

            public int Row
            {
                get
                {
                    return  m_row;
                }
                set
                {
                    if (m_Manger.removeBox != null)
                    {
                        m_Manger.removeBox.Add(new BoxLoc(Row, m_col));
                    }
                    m_row = value;

                    m_Manger.rowChan(m_row);
                    if (m_Manger.returnChange != null)
                    {
                        m_Manger.returnChange.Add(this);
                    }
                }
            }


            public int Col
            {
                get
                {
                    return m_col;
                }
                set
                {
                    if (m_Manger.removeBox != null)
                    {
                        m_Manger.removeBox.Add(new BoxLoc(Row, m_col));
                    }
                    VersionBox VerB;
                    if (this.m_version.versionName.Equals("0.2.14"))
                    {
                        string a = "b";
                    }
                    if (m_Manger.VersionBoxFromRowCol.TryGetValue(StringRowCol(Row, Col, ColSpan), out VerB))
                    {
                        if (VerB == this)
                        {
                            if (StringRowCol(Row, Col, ColSpan).Equals("row: 16 col: 163"))
                            {
                                string a = "b";
                            }
                            m_Manger.VersionBoxFromRowCol.Remove(StringRowCol(Row, Col, ColSpan));


                        }
                    }
                    m_col = value;

                    if (m_Manger.VersionBoxFromRowCol.TryGetValue(StringRowCol(Row, Col, ColSpan), out VerB)) //6.4c.5.1
                    {
                        if (VerB.m_version.versionName.Equals("6.4c.5.1"))
                        {
                            string a = "b";
                        }
                    }
                    m_Manger.VersionBoxFromRowCol[StringRowCol(Row, Col,ColSpan)] = this;

                    if (m_LittleSister != null)
                    {
                        m_LittleSister.SisterCol(m_col + m_ColSpan);
                    }
                    if (m_FirstDaughter != null)
                    {
                        m_FirstDaughter.FirstDaughter(m_col);
                    }
                    if (m_Mother != null)
                    {
                        m_Mother.DaughterColSpan(m_col + m_ColSpan);
                    }
                    m_Manger.colChan(m_col + m_ColSpan);
                    if (m_Manger.returnChange != null)
                    {
                        m_Manger.returnChange.Add(this);
                    }
                }
            }
            public static string StringRowCol(int row, int col)
            {
                return "row: " + row + " col: " + col;

            }

            /// <summary>
            /// Returns a row froma StringRowCol
            /// </summary>
            /// <param name="stringrowco"></param>
            /// <returns></returns>
            public static int RowFromString(string stringrowco)
            {
                int rowEnd = stringrowco.IndexOf('c') - 5;
                string row = stringrowco.Substring(4, rowEnd);
                return Convert.ToInt32(row);
            }

            public static int ColFromString(string stringrowco)
            {
                int colStart = stringrowco.IndexOf('l') + 3;
                string col = stringrowco.Substring(colStart);
                return Convert.ToInt32(col);
            }

            public static string StringRowCol(int row, int col, int colspan)
            {
                return "row: " + row + " col: " + (col + ((colspan + 1)/2));

            }
            public int ColSpan
            {

                get
                {
                    return m_ColSpan;
                }
                set
                {
                    VersionBox VerB;
                    if (m_Manger.VersionBoxFromRowCol.TryGetValue(StringRowCol(Row, Col, ColSpan), out VerB))
                    {
                        if (VerB == this)
                        {
                            if (StringRowCol(Row, Col, ColSpan).Equals("row: 16 col: 163"))
                            {
                                string a = "b";
                            }
                            m_Manger.VersionBoxFromRowCol.Remove(StringRowCol(Row, Col, ColSpan));


                        }
                    }
                    m_ColSpan = value;
                    if (m_Manger.VersionBoxFromRowCol.TryGetValue(StringRowCol(Row, Col, ColSpan), out VerB)  ) //6.4c.5.1
                    {
                        if (VerB.m_version.versionName.Equals("6.4c.5.1"))
                        {
                            string a = "b";
                        }
                    }
                    m_Manger.VersionBoxFromRowCol[StringRowCol(Row, Col, ColSpan)] = this;
                    if (m_Mother != null && m_ColSpan > 1)
                    {
                        m_Mother.DaughterColSpan(m_col + m_ColSpan);
                    }

                    if (m_LittleSister != null && m_ColSpan > 1)
                    {
                        m_LittleSister.SisterCol(m_col + m_ColSpan);
                    }
                    m_Manger.colChan(m_ColSpan);
                    if (m_Manger.returnChange != null)
                    {
                        m_Manger.returnChange.Add(this);
                    }
                }
            }

        }

        private delegate void ManaDelegate();

        public VersionRelationShipTress()
        {
            InitializeComponent();
            mana = new VersionBoxManager();
            VersionLeafFromString = new Dictionary<String, VersionLeaf>();
            VersionL = new List<String>();
        }

        public VersionRelationShipTress(DataManager datamanager)
        {
            InitializeComponent();
            mana = new VersionBoxManager();
            VersionLeafFromString = new Dictionary<String, VersionLeaf>();
            m_DataManager = datamanager;
            addVersion(m_DataManager.VersionList);
        //    Functions.SerializeToFile<VersionBoxManager>("versionBoxMan",mana);
            VersionL = new List<String>();
        }

        public void addVersion(VersionInfo vers)
        {
            List<VersionBox> newC = mana.returnChangeAdd(vers);
            this.SuspendLayout();
            VersionLeaf vler;
            //Todo if there is a row add then everything must be redrawn
            //make that more clear bu setting a variable
            //then check that and redraw
            //if (mana.removeBox == null && newC == null)
            //{
            //    addAllFromMana();
            //}
            //else
            //{
            //    foreach (BoxLoc bo in mana.removeBox)
            //    {
            //        VersionBoxFromString.TryGetValue("row" + bo.row + "col" + bo.col, out vler);
            //        if (vler != null)
            //        {
            //            vler.Visible = false;
            //        }
            //    }
            //    this.tableLayoutPanel1.ColumnCount = mana.Col + 1;
            //    this.tableLayoutPanel1.RowCount = mana.Row + 1;
            //    foreach (VersionBox vb in newC)
            //    {
            //        VersionBoxFromString.TryGetValue("row" + vb.Row + "col" + vb.Col, out vler);
            //        if (vler != null)
            //        {
            //            vler.setVersionInfo(vb.m_version);
            //        }
            //        else
            //        {
            //            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            //            VersionLeaf leaf = new VersionLeaf(vb.m_version);
            //            leaf.setText("Col: " + vb.Col + " Row: " + vb.Row + " ColSpan: " + vb.ColSpan);
            //            leaf.Anchor = System.Windows.Forms.AnchorStyles.None;
            //            leaf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            //            leaf.Location = new System.Drawing.Point(4, 4);
            //            leaf.Size = new System.Drawing.Size(150, 150);
            //            this.tableLayoutPanel1.Controls.Add(leaf, vb.Col, vb.Row);
            //            VersionBoxFromString["row" + vb.Row + "col" + vb.Col] = leaf;
            //            if (vb.ColSpan > 0)
            //            {
            //                this.tableLayoutPanel1.SetColumnSpan(leaf, vb.ColSpan);
            //            }
            //            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            //        }

            //    }
            //}
            //this.ResumeLayout(false);
            //this.PerformLayout();
        }

        private void addAllFromMana()
        {
            List<int> HeightSquares = SquaresSpaces(this.VersionTreePanel.Top, this.panel1.Height, 200);
            List<int> WidthSquares = SquaresSpaces(this.VersionTreePanel.Left, this.panel1.Width, 200);
            string stringR;
            VersionBox versB;
            VersionLeaf vlef;


            int count = 0;

            int colL = WidthSquares[0] / 200;
            int rowL = mana.rowMax - HeightSquares[HeightSquares.Count - 1] / 200;
            int colH = WidthSquares[WidthSquares.Count - 1] / 200;
            int rowH = mana.rowMax - HeightSquares[0] / 200;


            int Crow;
            int Ccol;
            bool remove;
            List<string> VersiontoRem = new List<string>();
            foreach (string rowcol in VersionLeafFromString.Keys)
            {
                Crow = VersionBox.RowFromString(rowcol);
                Ccol = VersionBox.ColFromString(rowcol);
                remove = false;
                if (Crow * 5 < rowL)
                {
                    remove = true;
                }
                else if (Crow > rowH * 5)
                {
                    remove = true;
                }
                else if (Ccol * 5 < colL)
                {
                    remove = true;
                }
                else if (Ccol > colL * 5)
                {
                    remove = true;
                }

                if (remove)
                {
                    this.VersionTreePanel.Controls.Remove(VersionLeafFromString[rowcol]);
                    VersionLeafFromString[rowcol].Dispose();
                    VersionL.Remove(rowcol);
                    VersiontoRem.Add(rowcol);
                }
            }

            foreach (string ro in VersiontoRem)
            {
                VersionLeafFromString.Remove(ro);
            }

            


            foreach (int heigh in HeightSquares)
            {
                foreach (int width in WidthSquares)
                {
                    stringR  = VersionBox.StringRowCol(( mana.rowMax - (heigh / (200)) ), width / (200));

                    int row = VersionBox.RowFromString(stringR);
                    if (mana.VersionBoxFromRowCol.TryGetValue(stringR, out versB) && VersionL.Contains(stringR) == false)
                    {
                        vlef = new VersionLeaf(versB.m_version);
                        vlef.Location = new Point(width+25, heigh+25);
                        vlef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        vlef.Size = new Size(150, 150);
                        this.VersionTreePanel.Controls.Add(vlef);
                        VersionL.Add(stringR);
                        VersionLeafFromString[stringR] = vlef;
                    }

                }
            }

            


        }



        public void callMana()
        {
            this.VersionTreePanel.Invoke(new ManaDelegate(addAllFromMana));
        }

        public List<int> SquaresSpaces(int X, int width, int space)
        {
            List<int> ReturnList = new List<int>();
            X = -X;
            int origX = X;
            if (X != 0)
            {
                int pluse = X * 1 / space;

                //Find the nearest multiple of 80 that is equal to or greater than X
                X = (pluse * space == X) ? X : (pluse + 1) * space;
                X -= space;
            }

            for (int i = X; i < width + origX; i = i + space)
            {
                ReturnList.Add(i);
            }
            return ReturnList;
        }

        public void addVersion(SortedList<DateTime,VersionInfo> version)
        {

            foreach (VersionInfo ver in version.Values)
            {
                mana.addVersion(ver);
            }

         //   mana = Functions.DeserializeFromFile<VersionBoxManager>("versionBoxMan");
            addAllFromMana();
            this.VersionTreePanel.Height = (mana.Row + 1) * 200;
            this.VersionTreePanel.Width = (mana.Col + 1) * 200;


        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            this.VersionTextBox.Focus();

            this.label1.Text = Convert.ToString(
                "Bottom " + this.VersionTreePanel.Bottom +
                "\n Top " + this.VersionTreePanel.Top +
                "\n Left " + this.VersionTreePanel.Left +
                "\n Right " + this.VersionTreePanel.Right +
                "\n X " + +this.VersionTreePanel.DisplayRectangle.X +
                "\n Y " + this.VersionTreePanel.DisplayRectangle.Y +
                "\n Height " + this.VersionTreePanel.Height +
                "\n Width " + this.VersionTreePanel.Width +
                "\n D Top " + this.VersionTreePanel.DisplayRectangle.Top +
                "\n D Bot " + this.VersionTreePanel.DisplayRectangle.Bottom +
                "\n D Hei " + this.panel1.Height +
                "\n D Wid " + this.panel1.Width);
        //    this.label1.Location = new Point(this.VersionTreePanel.Left, this.VersionTreePanel.Top);

            Thread manaThread = new Thread(callMana);
            manaThread.Start();
         //   addAllFromMana();
        }

        private void VersionRelationShipTress_ResizeEnd(object sender, EventArgs e)
        {
            this.label1.Text = Convert.ToString(
                "Bottom " + this.VersionTreePanel.Bottom +
                "\n Top " + this.VersionTreePanel.Top +
                "\n Left " + this.VersionTreePanel.Left +
                "\n Right " + this.VersionTreePanel.Right +
                "\n X " + +this.VersionTreePanel.DisplayRectangle.X +
                "\n Y " + this.VersionTreePanel.DisplayRectangle.Y +
                "\n Height " + this.VersionTreePanel.Height +
                "\n Width " + this.VersionTreePanel.Width +
                "\n D Top " + this.VersionTreePanel.DisplayRectangle.Top +
                "\n D Bot " + this.VersionTreePanel.DisplayRectangle.Bottom +
                "\n D Hei " + this.panel1.Height +
                "\n D Wid " + this.panel1.Width);
        //    this.label1.Location = new Point(this.VersionTreePanel.Left ,this.VersionTreePanel.Top);
        //    addAllFromMana();
        }

        public void SetFoc(Button br)
        {
            focuBu = br;
        }

        private void VersionTreePanel_Paint(object sender, PaintEventArgs e)
        {
             
            Pen pe = new Pen(Color.Red,7);
            pe.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            Point [] pt = new Point[4];
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
       //     Dictionary<string, VersionLeaf> VersionConnected = new Dictionary<string, VersionLeaf>();
          //  VersionConnected.Values
            VersionBox verb2;
            foreach (VersionLeaf le in VersionLeafFromString.Values)
            {
                foreach (VersionBox verb in mana.getVersionBoxOfVersion(le.m_VersionInfo.versionName).m_Daughters)
                {
                    if (verb != null)
                    {
                        pt[0] = new Point(le.Location.X + 75, le.Location.Y);
                        pt[1] = new Point(le.Location.X + 75, le.Location.Y - 25);
                        pt[2] = new Point(((verb.Col + ((verb.ColSpan + 1) / 2)) * 200) + 100, le.Location.Y - 25);
                        pt[3] = new Point(((verb.Col + ((verb.ColSpan + 1) / 2)) * 200) + 100, ((mana.rowMax - (verb.Row)) * 200) + 175);
                         e.Graphics.DrawBezier(pe, pt[0], pt[1], pt[2], pt[3]);
             //           VersionConnected.Add(verb.m_version.versionName);
                    }
                }

                verb2 = mana.getVersionBoxOfVersion(le.m_VersionInfo.versionName).m_Mother;
                if (verb2 != null)
                {
                    pt[0] = new Point(le.Location.X + 75, le.Location.Y + 150);
                    pt[1] = new Point(le.Location.X + 75, le.Location.Y + 150 + 25);
                    pt[2] = new Point(((verb2.Col + ((verb2.ColSpan + 1) / 2)) * 200) + 100, le.Location.Y + 150 + 25);
                    pt[3] = new Point(((verb2.Col + ((verb2.ColSpan + 1) / 2)) * 200) + 100, le.Location.Y + 150 + 25 + 25);
                    e.Graphics.DrawBezier(pe, pt[3], pt[2], pt[1], pt[0]);
                }
                
            }
      //      VersionBox verb2;
      //      foreach (VersionLeaf le in VersionLeafFromString.Values)
       //     {
       //         if (VersionConnected.Contains(le.m_VersionInfo.versionName))
       //         {
        //            continue;
      //          }
          //      verb2 = mana.getVersionBoxOfVersion(le.m_VersionInfo.versionName).m_Mother;
        //        if (verb2 != null)
        //        {
        //            pt[0] = new Point(le.Location.X + 75, le.Location.Y + 150);
         //           pt[1] = new Point(le.Location.X + 75, le.Location.Y + 150 + 25);
        //            pt[2] = new Point(((verb2.Col + ((verb2.ColSpan + 1) / 2)) * 200) + 100, le.Location.Y + 150 + 25);
        //            pt[3] = new Point(((verb2.Col + ((verb2.ColSpan + 1) / 2)) * 200) + 100, le.Location.Y + 150 + 25 + 25);
         //           e.Graphics.DrawBezier(pe, pt[3], pt[2], pt[1], pt[0]);
       //         }
          //  }
            pe.Dispose();
        }
    }
}