using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace FolderTrackGuiTest1
{

    public class PanelList<T> : Panel 
    {

       
        bool eventualCall = false;
        bool callRemove = false;
        bool callScroll = false;
        bool callData = false;

        public interface PanelData
        {
            Panel getPanel(T data);
        }

        public interface PanelFunction
        {
            void DoFunction(object data);
        }

        public interface CallAllDa
        {
            void CallAll(object data);
        }

        private Dictionary<T,object> m_FunctionCall;

        private Dictionary<int, Panel> m_PanelFromIndex;

        private Dictionary<T, Panel> m_PanelDataAr;


        private List<T> m_DataList;

        private int m_ExtraRowSpace;

        private System.Windows.Forms.VScrollBar vScrollBar;

        private PanelData m_PanelFromData;

        private object BlockAddingAndRemovingRows;

        private int RowPanelHeight;

        private int RowPanelWidth;

        private bool ExitRemoveKey;

        private object callAllData;

        private delegate void DisplayVersionsDelegate();

        private delegate void RemoveAllDelegate();

        private delegate bool AddScrollBarIfNeededDelegate();

        private delegate void RemoveVersionsDelegate(Panel panel, int index);

        private delegate void VoidListTDelegate(List<T> datalist);

        private delegate void VoidNoArgDelegate();
        public delegate void VoidPanelDelegate(Panel p);

        DisplayVersionsDelegate InvokeDisplay;

        RemoveVersionsDelegate InvokeRemovePanel;

        AddScrollBarIfNeededDelegate InvokeAddScrollBarIfNeeded;
        RemoveAllDelegate RemoveAllDel;

        int hStart = 0;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        public PanelList()
        {
            InitializeComponent();

            InvokeDisplay = new DisplayVersionsDelegate(Display);
            RemoveAllDel = new RemoveAllDelegate(InvokeRemoveAll);
            InvokeAddScrollBarIfNeeded = new AddScrollBarIfNeededDelegate(AddScrollBarIfNeeded);
            BlockAddingAndRemovingRows = new object();
            m_DataList = new List<T>();
            m_PanelFromIndex = new Dictionary<int, Panel>();
            m_FunctionCall = new Dictionary<T, object>();
            m_PanelDataAr = new Dictionary<T, Panel>();
            InvokeRemovePanel = new RemoveVersionsDelegate(RemovePanel);
            this.DoubleBuffered = true;
        }


        public void AddFunctionCallToData(T tdata, object data)
        {
            m_FunctionCall[tdata] = data;

            CallInvoke();

        }
        /// <summary>
        /// Do not call display
        /// </summary>
        /// <param name="tdata"></param>
        /// <param name="data"></param>
        public void AddFunctionCallToDataH(T tdata, object data)
        {
            m_FunctionCall[tdata] = data;

        }

        public void AddFunctionCallToData(Dictionary<T,object> Data)
        {
            foreach (KeyValuePair<T, object> da in Data)
            {
                m_FunctionCall[da.Key] = da.Value;
            }



            //if (this.Handle != null)
            //   {
            CallInvoke();
            //   }
        }


        public IList<Panel> ReturnCurrent()
        {
            Panel [] panelList= new Panel[m_PanelFromIndex.Values.Count];
            m_PanelFromIndex.Values.CopyTo(panelList, 0);
            return panelList;
        }

        public void ClearFunctionData()
        {
            m_FunctionCall.Clear();
        }

        public void AddDataBottom( T data)
        {
            lock (m_DataList)
            {
                this.Invoke(RemoveAllDel);
                m_DataList.Add(data);
                HandleDataChange();
            }
        }

        public void AddDataTop(T data)
        {
            lock (m_DataList)
            {
                this.Invoke(RemoveAllDel);
                m_DataList.Insert(0, data);
                HandleDataChange();
            }
        }

        public void AddDataTopH(IList<T> dataList)
        {
            lock (m_DataList)
            {
                callRemove = true;
                m_DataList.InsertRange(0, dataList);
                callData = true;
            }
        }

        public void AddDataTop(IList<T> dataList)
        {
            lock (m_DataList)
            {
                this.Invoke(RemoveAllDel);
                m_DataList.InsertRange(0, dataList);
                HandleDataChange();
            }
        }

        public void AddData(IList<T> dataList)
        {
            lock (m_DataList)
            {
                this.Invoke(RemoveAllDel);
                m_DataList.AddRange(dataList);
                HandleDataChange();
            }
        }

        public void RemoveData(T data)
        {
            lock (m_DataList)
            {
                this.Invoke(RemoveAllDel);
                m_DataList.Remove(data);
                HandleDataChange();
            }

        }

        public void RemoveDataH(T data)
        {
            lock (m_DataList)
            {
                callRemove = true;
                m_DataList.Remove(data);
                callData = true;
            }

        }

        

        public void ClearAllData()
        {
            lock (m_DataList)
            {
                if (m_DataList.Count > 0)
                {
                    this.Invoke(RemoveAllDel);
                    m_DataList.Clear();
                    HandleDataChange();
                }
            }
        }

        public void ClearAllDataH()
        {
            lock (m_DataList)
            {
                if (m_DataList.Count > 0)
                {
                    callRemove = true;
                    m_DataList.Clear();
                    callData = true;
                }
            }
        }

        /// <summary>
        /// Do not call display
        /// </summary>
        /// <param name="dataList"></param>
        public void ClearAndAddDataH(IList<T> dataList)
        {
            this.Invoke(new VoidListTDelegate(iClearAndDataH), new object[] { dataList });
        }

        private void iClearAndDataH(IList<T> dataList)
        {
            lock (m_DataList)
            {
                callRemove = true;
                m_DataList.Clear();
                m_DataList.AddRange(dataList);
                callData = true;
                
            }
        }


        public void ClearAndAddData(IList<T> dataList)
        {
            
            lock (m_DataList)
            {
                
                this.Invoke(RemoveAllDel);
                m_DataList.Clear();
                m_DataList.AddRange(dataList);
                HandleDataChange();

            }
        }

        public void CallAll(object data)
        {
            this.callAllData = data;
            CallInvoke();
        }
        /// <summary>
        /// Hold display
        /// </summary>
        /// <param name="data"></param>
        public void CallAllH(object data)
        {
            this.callAllData = data;
        }

        public void CallInvoke()
        {
            
            new Thread(tCallInvoke).Start();
        }

        private void tCallInvoke()
        {

            if (eventualCall == false)
            {
                eventualCall = true;
                Thread.Sleep(250);
                if (callRemove)
                {
                    this.Invoke(RemoveAllDel);
                    callRemove = false;
                }
                if (callScroll)
                {
                    this.vScrollBar.Invoke(InvokeAddScrollBarIfNeeded);
                    callScroll = false;
                }
                if (callData)
                {
                    HandleDataChange(true);
                    callData = false;

                }
                if (this.InvokeRequired)
                {
                    this.Invoke(InvokeDisplay);
                }
                else
                {
                    Display();
                }
                
                eventualCall = false;

           }
        }


        private void HandleDataChange()
        {
            HandleDataChange(false);
        }

        private void HandleDataChange(bool holdInvoke)
        {
            if (m_DataList.Count > 0)
            {
                Panel panelRow = m_PanelFromData.getPanel(m_DataList[0]);
                RowPanelHeight = panelRow.Height + m_ExtraRowSpace;
                RowPanelWidth = panelRow.Width;
                CalcHStar();
                //Dispose
                panelRow.Visible = false;
                panelRow.Dispose();
                panelRow = null;
            }

            bool invokeDis = true;
            if (holdInvoke == true)
            {
                callScroll = true;
            }
            else
            {
                try
                {
                    object returnva = this.vScrollBar.Invoke(InvokeAddScrollBarIfNeeded);
                    invokeDis = (Boolean)returnva;

                }
                catch (Exception e)
                {
                    int a = 0;
                    a++;
                    a--;
                }

                if (invokeDis == true)
                {
                    try
                    {
                        CallInvoke();
                    }
                    catch (Exception e)
                    {
                        e.ToString();
                        Display();
                    }
                }
            }
        }

        
        

        private void Display()
        {
            
            //If panel size is 0 nothing to do
            if (RowPanelHeight == 0 || m_PanelFromData == null)
            {
                return;
            }

            ExitRemoveKey = true;
            lock (BlockAddingAndRemovingRows)
            {

                int index = 0;
                List<int> HeightSquares = null;
                try
                {

                    HeightSquares = SquaresSpaces(this.vScrollBar.Value, this.Height, RowPanelHeight);
                }
                catch (Exception)
                {
                    return;
                }

                Panel panelRow;

                List<int> ItemsMoved = new List<int>();
                object data;
                T tdata;
               
                this.SuspendLayout();
                foreach (int height in HeightSquares)
                {
                    index = height / RowPanelHeight;

                    if (index < m_DataList.Count && index >= 0)
                    {
                        ItemsMoved.Add(index);
                        tdata = m_DataList[index];
                        if (m_PanelFromIndex.TryGetValue(index, out panelRow) == false)
                        {

                            panelRow = m_PanelFromData.getPanel(tdata);
                            panelRow.Width = this.Width - (hStart*2);
                            this.Controls.Add(panelRow);
                            
                            

                            try
                            {
                                panelRow.Location = new Point(hStart, m_ExtraRowSpace + height - this.vScrollBar.Value);
                            }
                            catch (Exception)
                            {
                                return;
                            }
                            lock (m_PanelFromIndex)
                            {
                                m_PanelFromIndex[index] = panelRow;
                            }
                            lock (m_PanelDataAr)
                            {
                                m_PanelDataAr[tdata] = panelRow;
                            }
                       //     this.Controls.Add(panelRow);
                            
                            if(m_FunctionCall.TryGetValue(tdata, out data))
                            {
                                if (panelRow is PanelFunction)
                                {
                                    ((PanelFunction)panelRow).DoFunction(data);
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                panelRow.Width = this.Width - (hStart * 2);
                                panelRow.Location = new Point(hStart, m_ExtraRowSpace + height - this.vScrollBar.Value);
                                
                            }
                            catch (Exception)
                            {
                                return;
                            }
                            if (m_FunctionCall.TryGetValue(tdata, out data))
                            {
                                if (panelRow is PanelFunction)
                                {
                                    ((PanelFunction)panelRow).DoFunction(data);
                                }

                                if (panelRow is CallAllDa)
                                {
                                    if (this.callAllData != null)
                                    {
                                        ((CallAllDa)panelRow).CallAll(this.callAllData);
                                    }
                                }
                            }
                        }
                    }
                }

                Point AwayPoint = new Point(RowPanelHeight * -1, RowPanelHeight * -1);
                foreach (KeyValuePair<int, Panel> kv in m_PanelFromIndex)
                {
                    if (ItemsMoved.Contains(kv.Key) == false)
                    {
                        kv.Value.Location = AwayPoint;
                    }

                    if (kv.Value is CallAllDa)
                    {
                        if (this.callAllData != null)
                        {
                            ((CallAllDa)kv.Value).CallAll(this.callAllData);
                        }
                    }

                }

            }

            try
            {
                this.ResumeLayout(false);
            }
            catch (Exception)
            {
                //Throw away
            }
            this.PerformLayout();
            this.Invalidate();
            ExitRemoveKey = false;



        }


        private void RemoveFarAwayRowPanel()
        {
            lock (BlockAddingAndRemovingRows)
            {
                List<int> HeightSquares;

                try
                {
                    HeightSquares = SquaresSpaces(this.vScrollBar.Value, this.Height, RowPanelHeight);
                }
                catch (Exception)
                {
                    return;
                }


                if (HeightSquares.Count == 0)
                {
                    return;
                }

                List<int> KeysToRemove = new List<int>();
                foreach (KeyValuePair<int, Panel> vals in m_PanelFromIndex)
                {
                    //If a version row is more than 2 displays away either high or low
                    if (
                        vals.Key < ((HeightSquares[0] - (2 * HeightSquares.Count)) / RowPanelHeight)
                        ||
                        vals.Key > ((HeightSquares[HeightSquares.Count - 1] + (2 * HeightSquares.Count)) / RowPanelHeight)
                        )
                    {
                        KeysToRemove.Add(vals.Key);
                    }
                    if (ExitRemoveKey == true)
                    {
                        return;
                    }
                }

                Panel panelRowToRemove;

                foreach (int keytoremove in KeysToRemove)
                {
                    if (m_PanelFromIndex.TryGetValue(keytoremove, out panelRowToRemove))
                    {
                        RemovePanel(panelRowToRemove, keytoremove);
                    }
                    if (ExitRemoveKey == true)
                    {
                        return;
                    }
                }


            }
        }

        private void RemovePanel(Panel panelRowToRemove, int keytoremove)
        {
            this.Controls.Remove(panelRowToRemove);
            //Dispose
            panelRowToRemove.Visible = false;
            panelRowToRemove.Dispose();
            m_PanelFromIndex.Remove(keytoremove);
            T KeyToRe = default(T);
            T defT = KeyToRe;
            lock (m_PanelDataAr)
            {
                foreach (KeyValuePair<T, Panel> k in m_PanelDataAr)
                {
                    if (k.Value.Equals(panelRowToRemove))
                    {
                        KeyToRe = k.Key;
                        break;
                    }
                }
                if (KeyToRe.Equals(defT) == false)
                {
                    m_PanelDataAr.Remove(KeyToRe);
                }
            }
        }


        private void InvokeRemoveAll()
        {
            
            foreach (KeyValuePair<int, Panel> kvp in m_PanelFromIndex)
            {
                //Dispose
                this.Controls.Remove(kvp.Value);
                RemKvP(kvp.Value);
            }
            m_PanelFromIndex.Clear();
            lock (m_PanelDataAr)
            {
                m_PanelDataAr.Clear();
            }

        }

        private void RemKvP(Panel p)
        {
            if (p.InvokeRequired)
            {
                p.Invoke(new VoidPanelDelegate(RemKvP));
                return;
            }
            p.Visible = false;
            p.Dispose();
        }

        public PanelData PanelFromData
        {
            get
            {
                return m_PanelFromData;
            }
            set
            {
                m_PanelFromData = value;
            }
        }

        public void CalcHStar()
        {
            hStart = this.vScrollBar.Width * 2;
        }

        private void InitializeComponent()
        {
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.Controls.Add(this.vScrollBar);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Location = new System.Drawing.Point(0, 0);
        //    this.Size = new System.Drawing.Size(292, 266);
            this.TabIndex = 0;


            // 
            // vScrollBar1
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
         //   this.vScrollBar.Location = new System.Drawing.Point(275, 0);
            this.vScrollBar.Name = "vScrollBar1";
       //     this.vScrollBar.Size = new System.Drawing.Size(17, 266);
            this.vScrollBar.TabIndex = 0;
            this.vScrollBar.ValueChanged += new EventHandler(vScrollBar_ValueChanged);
            this.vScrollBar.Visible = false;

            this.Resize += new EventHandler(PanelList_Resize);


            this.ResumeLayout(false);
        }

        void PanelList_Resize(object sender, EventArgs e)
        {
     //       InvokeRemoveAll();
            
                object returnva;
                bool invokeDis = true;

                try
                {

                    invokeDis = AddScrollBarIfNeeded();
                }
                catch (Exception)
                {
                    try
                    {
                        returnva = this.vScrollBar.Invoke(InvokeAddScrollBarIfNeeded);
                        invokeDis = ((Boolean)returnva);
                    }
                    catch (Exception) { } //Throw away
                }



                try
                {
                    if (this.Handle != null && invokeDis == true)
                    {
                        this.Invoke(new VoidNoArgDelegate( ResizeInvokeDisplay));
                    }
                }
                catch (Exception exc)
                {
                    //throw away
                }
            
        }

        private void ResizeInvokeDisplay()
        {
            Size value = this.Size;
            Thread.Sleep(100);
            if(value.Equals(this.Size))
            {
                try
                {
                    CallInvoke();
                }
                catch (InvalidOperationException)
                {
                    //Throw away
                }
            }
        }

        bool AddScrollBarIfNeeded()
        {
            try
            {
                if (RowPanelHeight > 0)
                {
                    this.vScrollBar.Minimum = 0;
                    //Add 1 because of the 0 and another 1 for extra space
                    this.vScrollBar.Maximum = ((m_DataList.Count + 2) - (this.Height / RowPanelHeight)) * RowPanelHeight;
                }
                if (RowPanelHeight * m_DataList.Count > this.Height)
                {

                    this.vScrollBar.Visible = true;
                    this.vScrollBar.LargeChange = RowPanelHeight * 2;
                    this.vScrollBar.SmallChange = RowPanelHeight;
                }
                else
                {
                    bool returnValu = (this.vScrollBar.Value == this.vScrollBar.Minimum);
                    this.vScrollBar.Value = this.vScrollBar.Minimum;
                    this.vScrollBar.Visible = false;
                    return returnValu;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        void vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(DisplayWhenScrollPaused);
                thread.Start();
            }
            catch
            {
                //Throw away
            }
        }

        private void DisplayWhenScrollPaused()
        {
            try
            {
                int value = this.vScrollBar.Value;
                Thread.Sleep(100);
                if (value == this.vScrollBar.Value)
                {
                    try
                    {
                        CallInvoke();
                    }
                    catch (InvalidOperationException)
                    {
                        //Throw away
                    }

                }
            }
            catch (Exception)
            {
                //Throw away
            }
        }


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

        /// <summary>
        /// The space between panels
        /// </summary>
        public int ExtraRowSpace
        {
            get
            {
                return m_ExtraRowSpace;
            }

            set
            {
                m_ExtraRowSpace = value;
            }
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

        public void SynchData(List<T> data)
        {
            List<T> dataToRemove = new List<T>();
            bool dataAdded = false;
            foreach (T dataItem in m_DataList)
            {
                if (data.Contains(dataItem) == false)
                {
                    dataToRemove.Add(dataItem);
                }
            }

            foreach (T dataItem in dataToRemove)
            {
                m_DataList.Remove(dataItem);
            }

            foreach (T dataItem in data)
            {
                if (m_DataList.Contains(dataItem) == false)
                {
                    m_DataList.Add(dataItem);
                    dataAdded = true;
                }
            }
            if (dataToRemove.Count > 0)
            {
                this.Invoke(new VoidListTDelegate(InvokeRemoveList), new object[] { dataToRemove });
            }

            if (dataToRemove.Count > 0 || dataAdded == true)
            {
                HandleDataChange();
            }

        }

        private void InvokeRemoveList(List<T> datalist)
        {
            Panel Dapa;
            List<Panel> panelToRemove = new List<Panel>();
            foreach (T kvp in datalist)
            {
                if (m_PanelDataAr.TryGetValue(kvp, out Dapa))
                {
                    this.Controls.Remove(Dapa);
                    //Dispose
                    Dapa.Visible = false;
                    panelToRemove.Add(Dapa);
                    m_PanelDataAr.Remove(kvp);
                }
            }
            List<int> iToRem = new List<int>();
            foreach (KeyValuePair<int, Panel> kv in m_PanelFromIndex)
            {
                if (panelToRemove.Contains(kv.Value))
                {
                    iToRem.Add(kv.Key);
                }
            }

            foreach (int intT in iToRem)
            {
                m_PanelFromIndex.Remove(intT);
            }
        }

    }


}
