using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FolderTrack.Types;
using ZetaLongPaths;

namespace FolderTrackGuiTest1.Filters
{
    public partial class FilterFileForm : Form
    {
        

        public PanelList<FilterObject> FilterPanelList;

        List<FilterObject> FilterObList;
        List<string> CurrentFilters;

        public FilterFileForm(FolderUnit path, List<string> CurList, List<string> FilterSt, List<string> monitor)
        {
            InitializeComponent();
            this.ResizeRedraw = true;
            FilterPanelList = new PanelList<FilterObject>();
            this.FilterPanel.Controls.Add(FilterPanelList);
            FilterRowFromFilterObject fromMo = new FilterRowFromFilterObject();
            fromMo.monitor = monitor;
            fromMo.FilterStrings = FilterSt;
            FilterPanelList.PanelFromData = fromMo;
            FilterPanelList.Dock = DockStyle.Fill;
            FilterObList = new List<FilterObject>();
            
            if (path != null && path.externalLocation.Length > 0)
            {
                FillFilterPanelList(path, CurList, FilterSt);
            }
            this.DialogResult = DialogResult.Cancel;

        }

        public void FillFilterPanelList(FolderUnit path,List<string> CurList,List<string> FilterSt)
        {
            List<string> CpyFil = new List<string>();
            if (FilterSt != null)
            {
                CpyFil.AddRange(FilterSt);
            }
            string filPath = path.externalLocation;
            if (path.type == Delimiter.FOLDER)
            {
                if (filPath.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)) == false)
                {
                    filPath = filPath + Path.DirectorySeparatorChar;
                }
            }

            FilterObject fil = new FilterObject();

            fil.filter = filPath;
            fil.Custom = false;
            if (CurList.Contains(fil.filter))
            {
                fil.discription = "Exact Path (already in use)";
                fil.mode = FilterObject.FilterObjectMode.KEEP;
                fil.use = true;
            }        
            else
            {
                fil.discription = "Exact Path";
                fil.mode = FilterObject.FilterObjectMode.SELECT;
                fil.use = false;
            }
            CpyFil.Remove(fil.filter);
            FilterObList.Add(fil);

            fil = new FilterObject();
            string filSla = filPath;
            if(filSla.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
            {
                filSla = filSla.Substring(0, filSla.Length - 2);
            }

            fil.filter = ZlpPathHelper.GetDirectoryNameFromFilePath(filSla);
            if (fil.filter.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)) == false)
            {
                fil.filter = fil.filter + Path.DirectorySeparatorChar;
            }
            fil.Custom = false;
            if (CurList.Contains(fil.filter))
            {
                fil.discription = "Parent Folder (already in use)";
                fil.mode = FilterObject.FilterObjectMode.KEEP;
                fil.use = true;
            }
            else
            {
                fil.discription = "Parent Folder";
                fil.mode = FilterObject.FilterObjectMode.SELECT;
                fil.use = false;
            }
            CpyFil.Remove(fil.filter);
            FilterObList.Add(fil);
            
            if (path.type == Delimiter.FILE && ZlpPathHelper.GetExtension(filPath).Length > 0)
            {
                string extention = "*" + ZlpPathHelper.GetExtension(filPath);
                if (extention != null && extention.Length > 0)
                {
                    fil = new FilterObject();
                    fil.filter = extention;
                    fil.Custom = false;
                    if (CurList.Contains(fil.filter))
                    {
                        fil.discription = "Extention (already in use)";
                        fil.mode = FilterObject.FilterObjectMode.KEEP;
                        fil.use = true;
                    }
                    else
                    {
                        fil.discription = "Extention";
                        fil.mode = FilterObject.FilterObjectMode.SELECT;
                        fil.use = false;
                    }
                    CpyFil.Remove(fil.filter);
                    FilterObList.Add(fil);
                }
            }
            
            string customPa = ZlpPathHelper.GetFileNameFromFilePath(filPath);
            if (customPa.Length == 0)
            {
                customPa = filPath.Substring(0, filPath.Length - 1);
                customPa = Path.DirectorySeparatorChar + 
                           ZlpPathHelper.GetFileNameFromFilePath(customPa) + 
                           Path.DirectorySeparatorChar;
            }
            
            fil = new FilterObject();
            fil.filter = "*" + customPa + "*";

            if (CurList.Contains(fil.filter))
            {
                fil.discription = "Custom (already in use)";
                fil.Custom = false;
                fil.mode = FilterObject.FilterObjectMode.KEEP;
                fil.use = true;
                CpyFil.Remove(fil.filter);
                FilterObList.Add(fil);

                fil = new FilterObject();
                fil.discription = "Custom";
                fil.Custom = true;
                fil.filter = "";
                fil.mode = FilterObject.FilterObjectMode.SELECT;
                fil.use = false;
                FilterObList.Add(fil);

            }
            else
            {
                fil.discription = "Custom";
                fil.Custom = true;
                fil.mode = FilterObject.FilterObjectMode.SELECT;
                fil.use = false;
                CpyFil.Remove(fil.filter);
                FilterObList.Add(fil);
            }
            foreach (string filt in CpyFil)
            {
                fil = new FilterObject();
                fil.filter = filt;
                if (CurList.Contains(filt))
                {
                    fil.discription = "Curently filtering " + filPath + " (already in use)";
                    fil.mode = FilterObject.FilterObjectMode.KEEP;
                    fil.use = true;
                    
                }
                else
                {
                    fil.discription = "Use to filter " + filPath + " (not in use)";
                    fil.mode = FilterObject.FilterObjectMode.SELECT;
                    fil.use = false;
                }
                fil.Custom = false;
                
                FilterObList.Insert(0,fil);
            }


            FilterPanelList.AddData(FilterObList);

        }

        public FilterChangeOb GetLists()
        {
            FilterChangeOb filCg = new FilterChangeOb();

            filCg.addFilfer = new List<string>();
            filCg.removeFilter = new List<string>();

            foreach (FilterObject fil in FilterObList)
            {
                if (fil.use)
                {
                    filCg.addFilfer.Add(fil.filter);
                }
                else
                {
                    if (fil.mode == FilterObject.FilterObjectMode.KEEP)
                    {
                        filCg.removeFilter.Add(fil.filter);
                    }
                }
            }
            return filCg;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}