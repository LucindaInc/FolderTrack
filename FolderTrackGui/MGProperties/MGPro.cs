using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FolderTrack.ExclusionRules;
using FolderTrack.Types;
using FolderTrackGuiTest1.Filters;

namespace FolderTrackGuiTest1.MGProperties
{
    public partial class MGPro : Form, FilterObject.FilterObCa
    {
        public MGPro()
        {
            InitializeComponent();
        }
        public List<GuiInfoMGProperties> Rules;
        public List<FilterObject> FilterO;
        public List<string> monitor;
        public List<string> FilterString;

        public void SetOptions(Options o)
        {

            DialogResult = DialogResult.Cancel;
            this.Rules = o.proper;
            defaultFilterList1.SetRules(Rules);
            FilterO = new List<FilterObject>();
            FilterObject defFiltOb;
            defFiltOb = new FilterObject();
            defFiltOb.discription = "Choose files and folders to NOT track";
            defFiltOb.Custom = true;
            defFiltOb.filter = "";
         //   defFiltOb.mode = FilterObject.FilterObjectMode.CHOOSE;
            defFiltOb.use = false;
            defFiltOb.AddToCa(this);
         //   FilterO.Add(defFiltOb);
            foreach (string listIt in o.filterList)
            {
                defFiltOb = new FilterObject();
                defFiltOb.filter = listIt;
                defFiltOb.use = true;
                defFiltOb.mode = FilterObject.FilterObjectMode.KEEP;
                FilterO.Add(defFiltOb);

            }
            if (monitor != null && FilterString != null)
            {
                customFilterList1.SetMonitor(monitor,FilterString);
            }
            customFilterList1.SetCus(FilterO);
            
        }

        #region FilterObCa Members

        public void Use(bool use, string filter)
        {
            
        }

        public void FilterObList(List<FilterObject> fi)
        {
            bool AddFil;
            List<FilterObject> AddFilterO = new List<FilterObject>();
            foreach (FilterObject newF in fi)
            {
                AddFil = true;
                foreach(FilterObject oldF in FilterO)
                {
                    if (newF.Equals(oldF))
                    {
                        AddFil = false;
                        oldF.use = newF.use;
                        break;
                    }
                }
                if (AddFil)
                {
                    AddFilterO.Add(newF);
                }
            }
            
            customFilterList1.AddFil(AddFilterO);
        }

        #endregion

        public FilterChangeOb GetLists()
        {
            return customFilterList1.GetLists();
        }

        private void MGPro_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        
    }
}
