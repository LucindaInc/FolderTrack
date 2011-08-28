using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.Drawing;
using System.Threading;

namespace FolderTrackGuiTest1.AllVersionsTab
{
    class RelationshipPanel : Panel, FolderTrack.WCFContracts.FolderTrackCallBack
    {
        int width;
        int height;
        
        VersionBoxManager mana;
        Dictionary<string, VersionMini> VersionLeafFromString;
        Dictionary<string, VersionMini> VersionLeafFromVersion;
        DataManager m_DataManager;
        private FTObjects m_FTObjects;
        List<string> VersionL;
        Button focuBu;
        public bool m_NotMonitor;

        private delegate void VoidStringVersionInDelega(string s, VersionInfo v);
        private delegate void VoidStringDelegate(string st);
        private delegate void VoidControlPointDelegate(Control cont, Point poin);
        private delegate void VoidControlDelegate(Control cont);

        public string currentVersion;
        public string lastCurrentVersion = null;

        [Serializable()]
        public class VersionBoxManager
        {
            public int rowMax;
            public int colMax;
            public int largestColumnNumber = -1;

            public Dictionary<string, ColNum> PointFromVersion = new Dictionary<string, ColNum>();

            public Dictionary<string, VersionBox> VersionBoxFromVersion = new Dictionary<string, VersionBox>();

            public Dictionary<int, List<Column>> ColumnListFromColumnNumber = new Dictionary<int, List<Column>>();

            public Dictionary<string, List<string>> DaughtersFrom = new Dictionary<string, List<string>>();

            public List<VersionBox> returnChange;
            public List<ColNum> removeBox;
            

            public VersionBoxManager()
            {
                colMax = 0;
                rowMax = 0;
                largestColumnNumber = -1;
            }

            public void ClearAll()
            {
                PointFromVersion.Clear();
                VersionBoxFromVersion.Clear();
                ColumnListFromColumnNumber.Clear();
                DaughtersFrom.Clear();
                colMax = 0;
                rowMax = 0;
                largestColumnNumber = -1;
            }

            public bool TryGetValue(string stringR, out VersionInfo vers)
            {
                int rCol = ColFromString(stringR);
                if (rCol >= ColumnListFromColumnNumber.Count)
                {
                    vers = null;
                    return false;
                }
                int rR = RowFromString(stringR);
                foreach (Column c in ColumnListFromColumnNumber[rCol])
                {
                    if (c.TryGetValue(rR, out vers))
                    {
                        return true;
                    }
                }
                vers = null;
                return false;
            }

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

            public List<VersionBox> returnChangeAdd(VersionInfo versio)
            {
                returnChange = null;
                removeBox = null;
                returnChange = new List<VersionBox>();
                removeBox = new List<ColNum>();
             //   addVersion(versio);
                return returnChange;
            }

            public VersionBox getVersionBoxOfVersion(string version)
            {
                VersionBox ReturnVersionBox;
                VersionBoxFromVersion.TryGetValue(version, out ReturnVersionBox);
                if (ReturnVersionBox == null)
                {
           //         ReturnVersionBox = addVersion(version);
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

            public void addVersion(List<VersionInfo> versionList)
            {
                foreach (VersionInfo versio in versionList)
                {
                    addVersion(versio);
                }
            }

            public void addVersion(VersionInfo version)
            {
                if (PointFromVersion.ContainsKey(version.versionName))
                {
                    return;
                }
                
                int colNum = -1;
                int row = calcRow(version.versionName);
                string g = DeCostaNumbers.GetMother(version.versionName);
                List<VersionInfo> CallBe = new List<VersionInfo>();

                while (PointFromVersion.ContainsKey(g) == false && version.versionName.Equals("0") == false && g.Equals("0") == false)
                {
                    CallBe.Add(new VersionInfo(g));
                    g = DeCostaNumbers.GetMother(g);
                }
                if (CallBe.Count > 0)
                {
                    CallBe.Reverse();
                    addVersion(CallBe);
                }

               
                if (DaughtersFrom.ContainsKey(g) == false)
                {
                    DaughtersFrom[g] = new List<string>();
                }
                DaughtersFrom[g].Add(version.versionName);
                List<Column> ColList;
                int potentColm;
                int potentCols;
                if (row > rowMax)
                {
                    rowMax = row;
                }
                foreach (KeyValuePair<int, List<Column>> k in ColumnListFromColumnNumber)
                {
                    foreach (Column c in k.Value)
                    {
                        //will reject the add and return false
                        // if version is not a first daugher
                        //decendant in this column
                        if (c.AddVersion(version))
                        {
                            colNum = k.Key;
                            break;
                        }
                    }
                    if (colNum != -1)
                    {
                        break;
                    }
                }

                if (colNum == -1)
                {
                    Column c = new Column(this.PointFromVersion);
                    c.AddVersion(version);
                    int start = NumberOfDot(version.versionName);
                    potentColm = -1;
                    potentCols = -1;

                    string moth = DeCostaNumbers.GetMother(version.versionName);
                    string sis = DeCostaNumbers.GetBigSister(version.versionName);

                    int mothR = calcRow(moth);
                    int sisR = -1;
                    if (sis != DeCostaNumbers.AUNT && sis != DeCostaNumbers.NONE)
                    {
                        sisR = calcRow(sis);
                    }

                    if (ColumnListFromColumnNumber.ContainsKey(start) == false)
                    {
                        ColumnListFromColumnNumber[start] = new List<Column>();
                        if (start > largestColumnNumber)
                        {
                            largestColumnNumber = start;
                        }
                    }

                    VersionInfo vers;
                    //
                    for (int i = start, cols = 0; cols < ColumnListFromColumnNumber.Count & i <= largestColumnNumber; i++)
                    {
                        if(ColumnListFromColumnNumber.ContainsKey(i) == true)
                        {
                            cols++;
                            foreach (Column co in ColumnListFromColumnNumber[i])
                            {
                                if (sis != DeCostaNumbers.AUNT && sis != DeCostaNumbers.NONE)
                                {
                                    if (co.TryGetValue(sisR, out vers))
                                    {
                                        if (vers.versionName.Equals(sis))
                                        {
                                            potentCols = i + 1;
                                            break;

                                        }
                                    }
                                }

                                if (co.TryGetValue(mothR, out vers))
                                {
                                    if (vers.versionName.Equals(moth))
                                    {
                                        potentColm = i + 1;

                                    }
                                }
                            }
                        }
                        if (potentCols != -1)
                        {
                            break;
                        }
                    }

                    if (potentColm == -1 && potentCols == -1)
                    {
                        potentColm = start;
                    }
                    else if (potentCols != -1)
                    {
                        potentColm = potentCols;
                    }


                    if (ColumnListFromColumnNumber.TryGetValue(potentColm, out ColList) == false)
                    {
                        ColumnListFromColumnNumber[potentColm] = new List<Column>();
                        ColList = ColumnListFromColumnNumber[potentColm];
                        if (potentColm > largestColumnNumber)
                        {
                            largestColumnNumber = potentColm;
                        }
                    }
                    
                    ColList.Add(c);
                    colNum = potentColm;
                    c.Col = colNum;
                }
                PointFromVersion[version.versionName].col = colNum;
                CheckColumn(colNum);

            }

            public void CheckColumn(int colNum)
            {
                int move;
                List<Column> testCol;
                Column column;
                testCol = ColumnListFromColumnNumber[colNum];
                for (int con = 0; con < testCol.Count; con++)
                {
                    

                    for (int teCo = con + 1; teCo < testCol.Count; teCo++)
                    {
                        if (Column.CanFit(testCol[con], testCol[teCo]) == false)
                        {
                            move = Column.MoveRight(testCol[con].testVers, testCol[teCo].testVers);
                            if (ColumnListFromColumnNumber.ContainsKey(colNum + 1) == false)
                            {
                                ColumnListFromColumnNumber[colNum + 1] = new List<Column>();
                                if ((colNum + 1) > largestColumnNumber)
                                {
                                    largestColumnNumber = (colNum + 1);
                                }
                            }
                            if (move == 1)
                            {
                                column = testCol[con];
                                ColumnListFromColumnNumber[colNum].Remove(column);
                                ColumnListFromColumnNumber[colNum + 1].Add(column);
                            }
                            else
                            {
                                column = testCol[teCo];
                                ColumnListFromColumnNumber[colNum].Remove(column);
                                ColumnListFromColumnNumber[colNum + 1].Add(column);
                            }
                            column.Col = colNum + 1;
                            PushRest(column,colNum,con);
                            CheckColumn(colNum + 1);
                            CheckColumn(colNum);
                            return;
                        }
                    }
                }
            }

            private void PushRest(Column column, int colNum, int start)
            {
                int move;
                List<Column> testCol;
                
                testCol = ColumnListFromColumnNumber[colNum];

                for (int teCo = start; teCo < testCol.Count; teCo++)
                {
                    if (Column.CanFit(column, testCol[teCo]) == false)
                    {
                        move = Column.MoveRight(column.testVers, testCol[teCo].testVers);
                        if (ColumnListFromColumnNumber.ContainsKey(colNum + 1) == false)
                        {
                            ColumnListFromColumnNumber[colNum + 1] = new List<Column>();
                            if ((colNum + 1) > largestColumnNumber)
                            {
                                largestColumnNumber = (colNum + 1);
                            }
                        }
                        if (move == 1)
                        {
                       //     ColList = testCol[con];
                       //     ColumnListFromColumnNumber[colNum].Remove(ColList);
                      //      ColumnListFromColumnNumber[colNum + 1].Add(ColList);
                        }
                        else
                        {
                            column = testCol[teCo];
                            ColumnListFromColumnNumber[colNum].Remove(column);
                            ColumnListFromColumnNumber[colNum + 1].Add(column);
                            column.Col = colNum + 1;
                        }
                    }
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
                    return ColumnListFromColumnNumber.Count;
                }
            }
        }

        public static int NumberOfDot(string ver)
        {
            int count = 0;
            foreach (char a in ver)
            {
                if(a.Equals('.'))
                {
                    count++;
                }
            }
            return count;
        }

        public class Column
        {
            Dictionary<int, VersionInfo> versionList;
            int minRow;
            int maxRow;
            ColNum colnum;
            Dictionary<string, ColNum> PointFromVersion;
            public string testVers=String.Empty;

            public bool TryGetValue(int row, out VersionInfo vers)
            {
                return versionList.TryGetValue(row, out vers);
            }
            public Column(Dictionary<string, ColNum> PointFromVersion)
            {
                minRow = Int32.MaxValue;
                maxRow = Int32.MinValue;
                colnum = new ColNum();
                this.PointFromVersion = PointFromVersion;
                versionList = new Dictionary<int, VersionInfo>();
            }

            public int Col
            {
                set
                {
                    colnum.col = value;
                }
            }


            public static bool CanFit(Column c1, Column c2)
            {
                if(c1.minRow > c2.maxRow)
                {
                    return true;
                }
                else if(c2.minRow > c1.maxRow)
                {
                    return true;
                }

                return false;
            }

            public static int MoveRight(string vers1, string vers2)
            {
                string[] v1ar = vers1.Split('.');
                string[] v2ar = vers2.Split('.');
                int length;
                string c1 = String.Empty;
                string c2 = String.Empty;

                string c1nl;
                string c2nl;

                if (v1ar.Length > v2ar.Length)
                {
                    length = v2ar.Length;
                }
                else
                {
                    length = v1ar.Length;
                }

                for (int i = 0; i < length; i++)
                {
                    if (v1ar[i].Equals(v2ar[i]) == false)
                    {
                        c1 = v1ar[i];
                        c2 = v2ar[i];
                        break;
                    }
                }

                c1nl = DeCostaNumbers.RemoveLetters(c1);
                c2nl = DeCostaNumbers.RemoveLetters(c2);
                int c1i;
                int c2i;
                if (c1nl.Equals(c2nl))
                {
                    c1nl = DeCostaNumbers.GetLetters(c1);
                    c2nl = DeCostaNumbers.GetLetters(c2);
                    int com = String.Compare(c1nl,c2nl);
                    if(com > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    c1i = Convert.ToInt32(c1nl);
                    c2i = Convert.ToInt32(c2nl);
                    if(c1i < c2i)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }


                

            }
            public bool AddVersion(VersionInfo vers)
            {
                if(versionList.Count > 0)
                {
                    if(DeCostaNumbers.FirstDaughterDecendent(testVers,vers.VersionName) == false)
                    {
                        return false;
                    }
                }
                int row = calcRow(vers.versionName);
                this.PointFromVersion[vers.versionName] = this.colnum;
                versionList[row] = vers;
                if(row < minRow)
                {
                    minRow = row;
                }
                if(row > maxRow)
                {
                    maxRow = row;
                }

                testVers = vers.versionName;
                return true;
            }
        }

        public class ColNum
        {
            public ColNum()
            {
            }

            public ColNum(int col)
            {
                this.col = col;
            }
            public int col;
        }


        public static int calcRow(string version)
        {
            string[] VersionSplit = version.Split('.');
            string numberS;
            int ReturnRow = 0;
            foreach (string VerSpl in VersionSplit)
            {
                numberS = FolderTrackGuiTest1.DeCostaNumbers.RemoveLetters(VerSpl);
                ReturnRow = ReturnRow + Convert.ToInt32(numberS);
            }
            return ReturnRow;
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
                VersionBox motherVersB = null;
                Row = calcRow(m_version.versionName);
           //     Col = setCol(m_version.versionName);
                

                

                
           
                
            }


        //    private int setCol(string versionName, int newCol) 
        //    {
        //        if(versionName.Equals("0"))
        //        {
        //            return 0;
        //        }
        //        string ds = DeCostaNumbers.GetMother(versionName);
        //        VersionBox motherVersB;
        //        motherVersB = m_Manger.getVersionBoxOfVersion(ds);
        //        motherVersB.m_Daughters.Add(this);

        ////        setCol(versionName, Row ,motherVersB.Col);
               
        //    }

            private void setCol(VersionBox tryVerB, int tryRow, int tryCol)
            {
                VersionBox verB;

            //    if (m_Manger.VersionBoxFromRowCol.TryGetValue(StringRowCol(tryRow, tryCol), out verB))
             //   {
                    //DeCostaNumbers.Relationship rela = DeCostaNumbers.getRelationship(this.m_version, verB);
                    //if (rela == DeCostaNumbers.BIG_SISTER)
                    //{
                    //    setCol(tryVerB, tryRow, tryCol + 1);
                    //}
                    //else if (rela == DeCostaNumbers.LIL_SISTER)
                    //{
                    //    setCol(verB, tryRow, verB.Col + 1);
                    //    tryVerB.Col = tryCol;
                    //}
                    //else if (rela == DeCostaNumbers.BIG_COUSIN)
                    //{
                    //    setCol(verB, tryRow, verB.Col + 1);
                    //    tryVerB.Col = tryCol;
                    //}
                    //else if (rela == DeCostaNumbers.LIL_COUSIN)
                    //{
                    //    setCol(tryVerB, tryRow, tryCol + 1);
                    //}
          //      }
          //      else
          //      {
             //       tryVerB.Col = tryCol;
           //    }
            }

  

            public void Notify(VersionBox vers)
            {
                //     m_NotifyList.Add(vers);
            }

            
            public int Row
            {
                get
                {
                    return m_row;
                }
                set
                {
          //          if (m_Manger.removeBox != null)
         //           {
         //               m_Manger.removeBox.Add(new ColNum(Row, m_col));
        //            }
        //            m_row = value;

        //            m_Manger.rowChan(m_row);
        //            if (m_Manger.returnChange != null)
        //            {
        //                m_Manger.returnChange.Add(this);
         //           }
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
                    m_col = value;
                    
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
                return "row: " + row + " col: " + (col + ((colspan + 1) / 2));

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
            
                    m_ColSpan = value;
                    
               //     m_Manger.VersionBoxFromRowCol[StringRowCol(Row, Col, ColSpan)] = this;
                    if (Row == 2)
                    {
                        string a = "b";
                    }
                    if (m_Mother != null && m_ColSpan > 1)
                    {
              //          m_Mother.DaughterColSpan(m_col + m_ColSpan);
                    }

                    if (m_LittleSister != null && m_ColSpan > 1)
                    {
                        //If the little sister is about to overwrite this in the VersionBoxFromRowCol array
                        if (StringRowCol(Row, m_col + 1, 1).Equals(StringRowCol(Row, Col, ColSpan)))
                        {
                            int tmp = VersionBox.ColFromString(StringRowCol(Row, Col, ColSpan));
                    //        m_LittleSister.SisterCol(m_col, m_ColSpan, tmp);
                        }
                        else
                        {
                //            m_LittleSister.SisterCol(m_col, m_ColSpan, -1);
                        }
                    }
                    m_Manger.colChan(m_ColSpan);
                    if (m_Manger.returnChange != null)
                    {
                        m_Manger.returnChange.Add(this);
                    }
                }
            }

        }

        private delegate void VoidNoArgDelegate();
        private delegate void VoidBoolDelegate(bool b);
        private delegate void VoidStringListVersDelegate(string s, List<VersionInfo> v);

        int space = 25;

        public RelationshipPanel()
        {
            InitializeComponent();
            
            this.DoubleBuffered = true;
            mana = new VersionBoxManager();
            VersionLeafFromString = new Dictionary<string, VersionMini>();
            VersionLeafFromVersion = new Dictionary<string, VersionMini>();
            VersionL = new List<string>();
            VersionMini v = new VersionMini();
            height = v.Size.Height + space;
            this.vScrollBar1.SmallChange = height / 3;
            this.vScrollBar1.LargeChange = height;
            width = v.Size.Width + space;
            this.hScrollBar1.SmallChange = width / 3;
            this.hScrollBar1.LargeChange = width;
            v.Dispose();
        }

        public FTObjects P_FTObjects
        {
            set
            {
                mana.ClearAll();
                Clear();
                this.m_FTObjects = value;
            }
        }

        private void FunctionsThatNeedCompleteVersionInfo()
        {
            if (m_FTObjects.NameReady() == false )
            {
                
                return;
            }

            addVersion(m_FTObjects.CurrentVersionList);
        }

        private void addVersion(IList<VersionInfo> version)
        {

            foreach (VersionInfo ver in version)
            {
                mana.addVersion(ver);
            }

            //   mana = Functions.DeserializeFromFile<VersionBoxManager>("versionBoxMan");
            this.Invoke(new VoidBoolDelegate(addAllFromMana), new object[] { false });
            new Thread(HandleScrollBars).Start();


        }

        public void HandleScrollBars()
        {
            if (this.vScrollBar1.InvokeRequired == true)
            {
                
                    this.vScrollBar1.Invoke(new VoidNoArgDelegate(HandleScrollBars));
                
                return;
            }
            //Because when the version boxes are added 100 is added to their location
            int maxv = (mana.Row - (this.Height / (height + 100)) + 1) * (height + 100);

            if (maxv > 0)
            {
                this.vScrollBar1.Maximum = maxv;
                this.vScrollBar1.Visible = true;
            }
            else
            {
                this.vScrollBar1.Visible = false;
            }
            //Because when the version boxes are added 100 is added to their location
            int maxh = (mana.Col - (this.Width / (width + 100)) + 1) * (width + 100);
            if (maxh > 0)
            {
                this.hScrollBar1.Maximum = maxh;
                this.hScrollBar1.Visible = true;
            }
            else
            {
                this.hScrollBar1.Visible = false;
            }
        }

        private void removeControl(string rowcol)
        {
            this.Controls.Remove(VersionLeafFromString[rowcol]);
        }

        private void addControl(Control at)
        {
            this.Controls.Add(at);
        }

        private void VerLoc(Control cont, Point pon)
        {
            cont.Location = pon;
        }

        private void addAllFromMana(bool RunScrollTest)
        {

            if (RunScrollTest)
            {
                int val = this.vScrollBar1.Value + this.hScrollBar1.Value;
                Thread.Sleep(50);
                if (val != vScrollBar1.Value + this.hScrollBar1.Value)
                {
                    return;
                }
            }

            List<int> HeightSquares = SquaresSpaces( this.vScrollBar1.Value, this.Height, height);
            List<int> WidthSquares = SquaresSpaces(this.hScrollBar1.Value, this.Width, width);
            string stringR;
            VersionInfo versB;
            VersionMini vlef;
            VersionMini NotSetle;


            

            int count = 0;

            int colL = WidthSquares[0] / width;
            int rowL = mana.rowMax - HeightSquares[HeightSquares.Count - 1] / height;
            int colH = WidthSquares[WidthSquares.Count - 1] / width;
            int rowH = mana.rowMax - HeightSquares[0] / height;


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
                else if (Ccol > colH * 5)
                {
                    remove = true;
                }

                if (remove)
                {
                    this.Invoke(new VoidStringDelegate(removeControl), new object[] { rowcol});
                    VersionLeafFromString[rowcol].Dispose();
                    VersionL.Remove(rowcol);
                    VersiontoRem.Add(rowcol);
                }
            }
            string vtorem;
            foreach (string ro in VersiontoRem)
            {
                vtorem = VersionLeafFromString[ro].m_VersionInfo.VersionName;
                VersionLeafFromVersion.Remove(vtorem);
                VersionLeafFromString.Remove(ro);
            }


            int row;
            int col;
            Point ptt;
            foreach (KeyValuePair<string, VersionMini> k in VersionLeafFromString)
            {
                row = VersionBox.RowFromString(k.Key);
                col = VersionBox.ColFromString(k.Key);
                ptt = new Point(col * width + 100 - this.hScrollBar1.Value, ((mana.rowMax - row) * height) + 100 - (this.vScrollBar1.Value));
                this.Invoke(new VoidControlPointDelegate(VerLoc), new object[] { k.Value, ptt });
            //            k.Value.Location = new Point(col * width + 100 - this.hScrollBar1.Value, ((mana.rowMax - row) * height) + 100 - (this.vScrollBar1.Value));
            }


            string originalStr;
            foreach (int he in HeightSquares)
            {
                foreach (int wi in WidthSquares)
                {
                    stringR = VersionBox.StringRowCol( mana.rowMax - (he / (height)), wi / (width));

                    row =  VersionBox.RowFromString(stringR);
                    col = VersionBox.ColFromString(stringR);
                    if (mana.TryGetValue(stringR, out versB))
                    {
                        originalStr = versB.versionName;
                        versB = m_FTObjects.VersionInfoFromVersionName(versB.versionName);
                        
                            if (VersionL.Contains(stringR) == false)
                            {
                                if (versB != null)
                                {
                                    vlef = new VersionMini(versB, m_FTObjects);
                                }
                                else
                                {
                                    vlef = new VersionMini(new VersionInfo(originalStr),null,true);
                                }
                                vlef.Location = new Point(col * width + 100 - this.hScrollBar1.Value, ((mana.rowMax - row) * height) + 100 - (this.vScrollBar1.Value));

                                this.Invoke(new VoidControlDelegate(addControl), new object[] { vlef });
                              //  this.Controls.Add(vlef);
                                VersionL.Add(stringR);
                                VersionLeafFromString[stringR] = vlef;
                                VersionLeafFromVersion[originalStr] = vlef;

                                if (originalStr.Equals(currentVersion))
                                {
                                    vlef.SetCurrent(true);
                                }
                                if (versB.Removed == true)
                                {
                                    vlef.SetRemoved(true);
                                }
                                else
                                {
                                    vlef.SetRemoved(false);
                                }
                                vlef.DontMonitor(m_NotMonitor);
                            }
                        
                        
                    }


                }
            }


            this.Invalidate(false);


        }

        


        public static List<int> SquaresSpaces(int StartPosition, int SizeOfArea, int Space)
        {

            int UseStart = StartPosition - (StartPosition % Space);


            List<int> ReturnList = new List<int>();
            for (int i = UseStart; i < SizeOfArea + StartPosition; i = i + Space)
            {
                ReturnList.Add(i);
            }
            return ReturnList;
        }

        public void callMana()
        {

            try
            {
                this.Invoke(new VoidBoolDelegate(addAllFromMana), new object[] { true });
            }
            catch (Exception)
            {
            }
            
        }


        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Thread manaThread = new Thread(callMana);
            manaThread.Start();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Thread manaThread = new Thread(callMana);
            manaThread.Start();
        }

        private void RelationshipPanel_Resize(object sender, EventArgs e)
        {
            new Thread(WriteResizeDebug).Start();
            Thread manaThread = new Thread(callMana);
            manaThread.Start();
            new Thread(HandleScrollBars).Start();
        }

        public void WriteResizeDebug()
        {
            Util.UserDebug("Relationship Panel Resized");
        }

        public void Clear()
        {

            if (this.hScrollBar1.InvokeRequired == true)
            {
                this.hScrollBar1.Invoke(new VoidNoArgDelegate(Clear));
                return;
            }

        //   this.SuspendLayout();
           foreach(KeyValuePair<string, VersionMini> f in VersionLeafFromVersion)
           {
                this.Controls.Remove(f.Value);
                f.Value.Dispose();
           }
        //   this.ResumeLayout(false);

           VersionLeafFromString.Clear();
           VersionL.Clear();
           VersionLeafFromVersion.Clear();
            
        }
        
        private void RelationshipPanel_Paint(object sender, PaintEventArgs e)
        {
            
            Pen pe = new Pen(Color.Black, 3);
            
            pe.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            Point[] pt = new Point[4];
            e.Graphics.Clear(System.Drawing.SystemColors.Control);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            VersionBox verb2;
            int row;
            int col;
            string moth;
            string getlocaa;
            foreach (VersionMini le in VersionLeafFromString.Values)
           {
              
               if (mana.DaughtersFrom.ContainsKey(le.m_VersionInfo.versionName))
               {
                   
                   
                   foreach (string getloca in mana.DaughtersFrom[le.m_VersionInfo.versionName])
                   {
                        row = calcRow(getloca);
                        col = mana.PointFromVersion[getloca].col;
                   
                        pt[0] = new Point(le.Location.X + (width / 2), le.Location.Y);
                        pt[1] = new Point(le.Location.X + (width / 2), le.Location.Y - (space / 2));
                        pt[2] = new Point(col * width + 100 - this.hScrollBar1.Value + (width / 2), le.Location.Y - (space / 2));
                        pt[3] = new Point(col * width + 100 - this.hScrollBar1.Value + (width / 2), (((mana.rowMax - row) * height) + 100 - (this.vScrollBar1.Value)) + (height - space));
                        e.Graphics.DrawBezier(pe, pt[0], pt[1], pt[2], pt[3]);
                        
                    }
               }
               if (le.m_VersionInfo.versionName.Equals("0") == false)
               {
                   
                   getlocaa = DeCostaNumbers.GetMother(le.m_VersionInfo.versionName);
                   row = calcRow(getlocaa);
                   col = mana.PointFromVersion[getlocaa].col;

                   pt[3] = new Point(le.Location.X + (width / 2), le.Location.Y + (height - 25));
                   pt[2] = new Point(le.Location.X + (width / 2), (((mana.rowMax - row) * height) + 100 - (this.vScrollBar1.Value)) - (space / 2));
                   pt[1] = new Point(col * width + 100 - this.hScrollBar1.Value + (width / 2), (((mana.rowMax - row) * height) + 100 - (this.vScrollBar1.Value)) - (space /2));
                   pt[0] = new Point(col * width + 100 - this.hScrollBar1.Value + (width / 2), (((mana.rowMax - row) * height) + 100 - (this.vScrollBar1.Value)) );
               
                   e.Graphics.DrawBezier(pe, pt[0], pt[1], pt[2], pt[3]);
   
               }


            

            }
            pe.Dispose();
        }





        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.SuspendLayout();
            // 
            // RelationshipPanel
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hScrollBar1);
            this.Location = new System.Drawing.Point(12, 24);
            this.Name = "RelationshipPanel";
            this.Size = new System.Drawing.Size(554, 304);
            this.TabIndex = 0;
            this.Paint += new System.Windows.Forms.PaintEventHandler(RelationshipPanel_Paint);
            this.Resize += new System.EventHandler(RelationshipPanel_Resize);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(537, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 287);
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 287);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(554, 17);
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // RelationshipDesigner
            // 
            this.ResumeLayout(false);

        }

        public void SetDontMonitor(object notMonitor)
        {
            bool notMonitoBool = (bool)notMonitor;
            this.m_NotMonitor = notMonitoBool;
            foreach (VersionMini e in VersionLeafFromVersion.Values)
            {
                e.DontMonitor(notMonitoBool);
            }

        }

        #endregion

        
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;

        #region FolderTrackCallBack Members

        public void NewCurrentVersion(string MonitorGroup, VersionInfo vers)
        {
            VersionMini tryomi;
            if (currentVersion != null)
            {
                if (VersionLeafFromVersion.TryGetValue(currentVersion, out tryomi))
                {
                    tryomi.SetCurrent(false);
                }
            }
            currentVersion = vers.versionName;
            if (VersionLeafFromVersion.TryGetValue(currentVersion, out tryomi))
            {
                Util.DBug2("RelationshipPanel", "set");
                tryomi.SetCurrent(true);
            }
            
        }

        public void NewVersion(string MonitorGroup, List<VersionInfo> vers)
        {
    //        if (this.vScrollBar1.InvokeRequired)
    //        {
    //            this.vScrollBar1.Invoke(new VoidStringVersionInDelega(NewVersion), new object[] { MonitorGroup, vers });
   //             return;
   //         }
            mana.addVersion(vers);

            addAllFromMana(false);


            HandleScrollBars();
        }


        public void DeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            VersionMini outVersionL;

            if (VersionLeafFromVersion.TryGetValue(version.versionName, out outVersionL))
            {
                outVersionL.SetRemoved(true);
            }
        }


        public void DeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
        }


        public void UndeleteVersionC(string MonitorGroup, VersionInfo version)
        {
            VersionMini outVersionL;

            if (VersionLeafFromVersion.TryGetValue(version.versionName, out outVersionL))
            {
                outVersionL.SetRemoved(false);
            }
            else
            {
                List<VersionInfo> sendLi = new List<VersionInfo>();
                sendLi.Add(version);
                NewVersion(MonitorGroup, sendLi);
            }
        }


        public void UndeleteVersionCList(string MonitorGroup, List<VersionInfo> version)
        {
        }

        public void NewVersionInformation(string MonitorGroup, List<VersionInfo> vers)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new VoidStringListVersDelegate(NewVersionInformation), new object[] { MonitorGroup, vers });
                return;
            }
            
            Clear();
            addAllFromMana(false);
            
            
        }

        public void DontMonitor(string MonitorGroup)
        {
            new Thread(new ParameterizedThreadStart(SetDontMonitor)).Start(true);
        }

        public void SendData(List<ChangeInstruction> data, int idnum, bool done)
        {
        }
        public void SendDataV(List<VersionInfo> data, string name, int idnum, bool done)
        {
            if (done == true)
            {
                FunctionsThatNeedCompleteVersionInfo();
            }
        }


        public void RestartMonitor(string MonitorGroup)
        {
            new Thread(new ParameterizedThreadStart(SetDontMonitor)).Start(false);
        }

        public void SendNewUserVersion(string MonitorGroup, List<string> UserVersion)
        {
            
        }

        public void PleaseRegister()
        {

        }

        public void TaskUpdate(TaskGroup[] task)
        {

        }


        public void NewDiscription(string MonitorGroup, VersionInfo version)
        {
           VersionMini vmin;
           if (VersionLeafFromVersion.TryGetValue(version.versionName, out vmin))
           {
               vmin.SetDiscription(version.freeText);
           }
        }

        #endregion
    }
}
