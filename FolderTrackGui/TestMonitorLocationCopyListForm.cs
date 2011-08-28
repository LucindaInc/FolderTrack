using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FolderTrackGuiTest1
{
    public partial class TestMonitorLocationCopyListForm : Form
    {
        private Dictionary<string, string> m_CopyLocationFromExtLoca;

        public TestMonitorLocationCopyListForm()
        {
            InitializeComponent();
        }


        public TestMonitorLocationCopyListForm(IList<string> locations)
        {
            InitializeComponent();

            m_CopyLocationFromExtLoca = new Dictionary<string, string>();

        }

        private void InitializeAll(IList<string> locations)
        {
            foreach (string loc in locations)
            {
                m_CopyLocationFromExtLoca[loc] = null;
            }
        }


        private void DisplayCopyList()
        {

        }
    }
}