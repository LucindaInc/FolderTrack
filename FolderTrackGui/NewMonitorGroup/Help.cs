using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FolderTrackGuiTest1.NewMonitorGroup
{
    public partial class Help : Form
    {
        int i = 0;
        public static string[] messag =
        {
            "Assume the folder below was on your computer. Notice it has 3 files",
            "Then you decide to delete fish.txt",
            "Next you add to turtl.txt",
            "You're unhappy! You want it back to the way it was",
            "Start up FolderTrack",
            "Tell FolderTrack to \"Use\" version 0",
            "",
            "You're happy! It is the way it was"


        };
        public Help()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            i++;
            if (i == (5 + 1))
            {
                i++;
            }
            else if (i > 7)
            {
                this.Close();
                return;
            }
            this.label1.Text = messag[i];
            switch (i)
            {
                case 0:
                    this.panel1.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources._1;
                    break;
                case 1:
                    this.panel1.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources._2;
                    break;
                case 2:
                    this.panel1.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources._3;
                    break;
                case 3:
                    this.panel1.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources._4;
                    break;
                case 4:
                    this.panel1.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources._5;
                    break;
                case 5:
                    this.panel1.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources._7;
                    break;
                case 7:
                    this.panel1.BackgroundImage = global::FolderTrackGuiTest1.Properties.Resources._8;
                    this.button1.Text = "Done";
                    break;

            }
        }
    }
}