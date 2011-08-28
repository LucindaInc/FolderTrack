using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FolderTrack.Types;
using System.IO;
using System.Runtime.InteropServices;
using ZetaLongPaths;

namespace FolderTrackGuiTest1
{
    public partial class Explor : Form
    {
        DataManager m_DataManager;
        DataReceiver m_DataReceiver;
        string m_Name;
        Dictionary<string, TreeNode> TreeNodeFromString;

        string m_VersionName;

        [Serializable]
        public struct ShellExecuteInfo
        {
            public int Size;
            public uint Mask;
            public IntPtr hwnd;
            public string Verb;
            public string File;
            public string Parameters;
            public string Directory;
            public uint Show;
            public IntPtr InstApp;
            public IntPtr IDList;
            public string Class;
            public IntPtr hkeyClass;
            public uint HotKey;
            public IntPtr Icon;
            public IntPtr Monitor;
        }

        public const uint SW_NORMAL = 1;

        [DllImport("shell32.dll", SetLastError = true)]
        extern public static bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);

        public Explor()
        {
            InitializeComponent();
        }

        public Explor(DataManager datamanager, DataReceiver datareceiver, string name)
        {
            InitializeComponent();
            m_DataManager = datamanager;
            m_DataReceiver = datareceiver;
            m_Name = name;
            TreeNodeFromString = new Dictionary<string, TreeNode>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PopulateTree();
        }

        private void PopulateTree()
        {
            m_VersionName = this.VersionNameTextBox.Text;
            VersionInfo versioninfo;
            if (m_DataManager.VersionInfoFromVersionName.TryGetValue(m_VersionName, out versioninfo))
            {
                FolderUnit [] FolderUnitArr = m_DataReceiver.GetFolderUnit(m_Name, versioninfo);
                TreeNode tr;
                string direc;
                TreeNode nodeToadd;
                foreach (FolderUnit fol in FolderUnitArr)
                {

                    direc = ZlpPathHelper.GetDirectoryNameFromFilePath(fol.externalLocation);
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
                        nodeToadd.Nodes.Add(fol.externalLocation);
                    }
                    else
                    {
                        TreeNodeFromString[fol.externalLocation] = new TreeNode(fol.externalLocation);
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

                TreeNode [] nodeArr = new TreeNode[TreeNodeFromString.Values.Count];
                TreeNodeFromString.Values.CopyTo(nodeArr,0);
                this.treeView1.Nodes.AddRange(nodeArr);
            }
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

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            VersionInfo versioninfo;
            if (m_DataManager.VersionInfoFromVersionName.TryGetValue(m_VersionName, out versioninfo))
            {

                string VersionFile = e.Node.Text;

                string CopyTo = @"C:\" + ZlpPathHelper.GetFileNameFromFilePath(e.Node.Text);

                m_DataReceiver.CopyFolderUnit(m_Name, versioninfo, VersionFile, CopyTo);


                try
                {
                    System.Diagnostics.Process.Start(CopyTo);
                }
                catch (Win32Exception ex)
                {
                    if (ex.NativeErrorCode == 1155)
                    {
                        OpenAs(CopyTo);
                    }
                    else
                    {
                        int a = 3 + 2;
                        a++;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// Author :: Vivek.T
        /// Email:: vivekthangaswamy@rediffmail.com
        /// url : http://www.codeproject.com/KB/shell/openwith.aspx
        /// </summary>
        /// <param name="file"></param>
        static void OpenAs(string file)
        {
            ShellExecuteInfo sei = new ShellExecuteInfo();
            sei.Size = Marshal.SizeOf(sei);
            sei.Verb = "openas";
            sei.File = file;
            sei.Show = SW_NORMAL;
            if (!ShellExecuteEx(ref sei))
                throw new System.ComponentModel.Win32Exception();
        }


    }
}