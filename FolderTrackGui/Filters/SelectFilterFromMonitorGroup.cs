using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Utilities;
using FolderTrack;
using System.Threading;
using FolderTrack.Types;
using System.IO;
using FolderTrack.ExclusionRules;

namespace FolderTrackGuiTest1.Filters
{
    public partial class SelectFilterFromMonitorGroup :  Form , FilterObject.FilterObCa
    {
        private Dictionary<string, TreeNode> TreeNodeFromString;
        private delegate void VoidNoArgDelegate();
        TreeNode[] nodeArr;
        Filter filt;
        FolderTrack.Types.MGProperties MgprF;
        List<string> EveryFolderAndFile;
        CustomFilterList cusfil;
        List<string> FilterString;
        List<FilterObject> FilterO;
        List<FilterObject> UserAddFilter;
        Options m_options;
        public SelectFilterFromMonitorGroup(Options o, List<string> monitor)
        {
            InitializeComponent();
            m_options = o;
            this.FilterString = o.filterList;
            this.ResizeRedraw = true;
            this.splitContainer1.Panel1.Resize += new EventHandler(Panel1_Resize);
            cusfil = new CustomFilterList();
            FilterObject defFiltOb;
            FilterO = new List<FilterObject>();
            UserAddFilter = new List<FilterObject>();
            MgprF = new FolderTrack.Types.MGProperties();
            
            foreach (string listIt in FilterString)
            {
                defFiltOb = new FilterObject();
                defFiltOb.mgpro = null;
                defFiltOb.filter = listIt;
                defFiltOb.use = true;
                defFiltOb.mode = FilterObject.FilterObjectMode.KEEP;
                defFiltOb.AddToCa(this);
                FilterO.Add(defFiltOb);
                UserAddFilter.Add(defFiltOb);

            }
            foreach (GuiInfoMGProperties mgpro in o.proper)
            {
                defFiltOb = new FilterObject();
                defFiltOb.mgpro = mgpro;
                
                defFiltOb.filter = mgpro.description;
                defFiltOb.discription = mgpro.title;
                defFiltOb.use = mgpro.active;
                defFiltOb.mode = FilterObject.FilterObjectMode.PROFILT;
                defFiltOb.AddToCa(this);
                FilterO.Add(defFiltOb);
            }

            cusfil.SetCus(FilterO);
            cusfil.Dock = DockStyle.Fill;
            
            this.splitContainer1.Panel1.Controls.Add(cusfil);
            TreeNodeFromString = new Dictionary<string, TreeNode>();
            filt = new Filter();
            filt.AddFilter(FilterString);
            PopulateTree(monitor);
        }

        void Panel1_Resize(object sender, EventArgs e)
        {
            this.cusfil.Refresh();
        }


        #region FilterObCa Members

        public void FilterObList(List<FilterObject> fi)
        {
            
        }

        class UseStr
        {
            public bool use;
            public string filter;
        }

        public void Use(bool use, string filterStrin)
        {
            UseStr usr = new UseStr();
            usr.use = use;
            usr.filter = filterStrin;

            new Thread(new ParameterizedThreadStart(InvoUse)).Start(usr);

        }
        public void InvoUse(object UseStO)
        {
            UseStr usr = (UseStr) UseStO;
            bool use = usr.use;
            string filterStrin = usr.filter;

            if (use == true)
            {
                
                filt.AddFilter(filterStrin);
            }
            else if (use == false)
            {
                //F
                filt.RemoveFilter(filterStrin);
            }

            this.MonGroTreeView.Invoke(new VoidNoArgDelegate(SetFilteredFiles));
        }

        #endregion
        

        private void PopulateTree(List<string> monitorGro)
        {
            TreeNode nodeToadd;
            string direc;

            EveryFolderAndFile = new List<string>();

            foreach (string fold in monitorGro)
            {
                if (ZlpIOHelper.DirectoryExists(fold))
                {
                    EveryFolderAndFile.AddRange(Util.GetDirectoriesAndFile(fold));
                }
                else
                {
                    EveryFolderAndFile.Add(fold);
                }
            }

            foreach (string fol in EveryFolderAndFile)
            {

                direc = ZlpPathHelper.GetDirectoryNameFromFilePath(fol);
                nodeToadd = null;
                foreach (TreeNode SearcNod in TreeNodeFromString.Values)
                {
                    nodeToadd = FindNo(SearcNod, direc);
                    if (nodeToadd != null)
                    {
                        break;
                    }
                }

                if (nodeToadd != null)
                {
                    nodeToadd.Nodes.Add(fol);
                }
                else
                {
                    TreeNodeFromString[fol] = new TreeNode(fol);
                }
            }

            List<TreeNode> remove = new List<TreeNode>();
            foreach (TreeNode TreeNod in TreeNodeFromString.Values)
            {
                direc = ZlpPathHelper.GetDirectoryNameFromFilePath(TreeNod.Text);
                nodeToadd = null;
                foreach (TreeNode SerNod in TreeNodeFromString.Values)
                {
                    nodeToadd = FindNo(SerNod, direc);
                    if (nodeToadd != null)
                    {
                        nodeToadd.Nodes.Add(TreeNod);
                        remove.Add(TreeNod);
                        break;
                    }
                }
            }

            foreach (TreeNode nod in remove)
            {
                TreeNodeFromString.Remove(nod.Text);
            }

            nodeArr = new TreeNode[TreeNodeFromString.Values.Count];
            TreeNodeFromString.Values.CopyTo(nodeArr, 0);
          //  this.MonGroTreeView.Invoke(new VoidNoArgDelegate(InvokeChangeFolderUnitTree));
            SetFilteredFiles();
            InvokeChangeFolderUnitTree();

        }

        private void SetFilteredFiles()
        {
            TreeNode nodeToadd;
            string filWoPa;
            MgprF.SetOptions(m_options.proper);
            int type;
            foreach (string fil in EveryFolderAndFile)
            {
                nodeToadd = null;
                foreach (TreeNode SearcNod in TreeNodeFromString.Values)
                {
                    nodeToadd = FindNo(SearcNod, fil);
                    if (nodeToadd != null)
                    {
                        break;
                    }
                }
                if (nodeToadd != null)
                {
                    if (ZlpIOHelper.DirectoryExists(fil))
                    {
                        filWoPa = fil + Path.DirectorySeparatorChar;
                        type = Delimiter.FOLDER;
                    }
                    else
                    {
                        filWoPa = fil;
                        type = Delimiter.FILE;
                    }
                    FolderUnit fu = new FolderUnit(fil,type);
                    if (filt.isFiltered(filWoPa) || MgprF.isExcluded(filWoPa,fu))
                    {
                        nodeToadd.BackColor = Color.LightCoral;
                    }
                    else
                    {
                        nodeToadd.BackColor = Color.White;
                    }
                }
            }
            
        }


        private void InvokeChangeFolderUnitTree()
        {
            this.MonGroTreeView.Nodes.Clear();
            
            this.MonGroTreeView.Nodes.AddRange(nodeArr);
            this.MonGroTreeView.ExpandAll();
        }

        public TreeNode FindNo(TreeNode node, string nodeS)
        {
            TreeNode searN;
            if (node.Text.Equals(nodeS))
            {
                return node;
            }
            else
            {
                searN = null;
                foreach (TreeNode nN in node.Nodes)
                {
                    searN = FindNo(nN, nodeS);
                    if (searN != null)
                    {
                        return searN;
                    }
                }

            }
            return null;
        }

        private void MonGroTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            new Thread(new ParameterizedThreadStart(HandleFilDoubleClick)).Start(e.Node.Text);
        }

        public void HandleFilDoubleClick(object patO)
        {
            string pat = (string) patO;
            FolderUnit fole = new FolderUnit();
            fole.externalLocation = pat;
            if (ZlpIOHelper.DirectoryExists(pat))
            {
                fole.externalLocation += Path.DirectorySeparatorChar;
                fole.type = Delimiter.FOLDER;
            }
            else
            {
                fole.type = Delimiter.FILE;
            }
            FilterFileForm fiFor = new FilterFileForm(fole, FilterString, null, null);
            fiFor.ShowDialog();
            if (fiFor.DialogResult == DialogResult.OK)
            {
               FilterChangeOb list= fiFor.GetLists();
               foreach (string addl in list.addFilfer)
               {
                   filt.AddFilter(addl);
               }

               foreach (string remol in list.removeFilter)
               {
                   filt.RemoveFilter(remol);
               }
            
                FilterObject defFiltOb;
                List<FilterObject> Addit = new List<FilterObject>();
                foreach (string listIt in list.addFilfer)
                {
                    if (FilterString.Contains(listIt) == false)
                    {
                        defFiltOb = new FilterObject();
                        defFiltOb.filter = listIt;
                        defFiltOb.use = true;
                        defFiltOb.mode = FilterObject.FilterObjectMode.KEEP;
                        defFiltOb.AddToCa(this);
                        //Note needed because cusfil.AddFil(Addit); 
                        // will do FilterO.Add(defFiltOb);
                        Addit.Add(defFiltOb);
                        FilterString.Add(listIt);
                    }

                }

                cusfil.AddFil(Addit);
                UserAddFilter.AddRange(Addit);
            }
            try
            {
                this.MonGroTreeView.Invoke(new VoidNoArgDelegate(SetFilteredFiles));
            }
            catch (InvalidOperationException)
            {
                //throw away
            }
        }

        private void MonGroTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MonGroTreeView.SelectedNode = null;
        }

        public FilterChangeOb GetFilterList()
        {
            return CustomFilterList.getFilterObFrom(UserAddFilter);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }





       


       

       
    }
}
