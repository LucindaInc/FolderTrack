using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace FolderTrackGuiTest1.License
{
    public partial class PleaseRegisterForm : Form
    {
        FTObjects m_FTObjects;
        private delegate void VoidDelegate();
        public PleaseRegisterForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.No;
        }

        public PleaseRegisterForm(string programNumber, FTObjects ftobject, bool rem)
        {
            InitializeComponent();

            if (rem)
            {
                this.RegisterLabel.Text = "Please Register";
                this.RegisterLabel.ForeColor = Color.SeaGreen;
                this.MessageLabel.Text = "If you register then FolderTrack will do the requested action";
            }
            else
            {
                this.RegisterLabel.Text = "Thanks For Registering";
                this.RegisterLabel.ForeColor = Color.SeaGreen;
                this.MessageLabel.Text = "";
            }
            this.m_FTObjects = ftobject;
            this.DialogResult = DialogResult.No;
            this.SetToProgramNumberTextBox.Text = programNumber;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            new Thread(SetRegister).Start();
            
        }

        private void SetRegister()
        {
            if (this.LicenseTextBox.InvokeRequired == true)
            {
                this.LicenseTextBox.Invoke(new VoidDelegate(SetRegister));
                return;
            }
            this.RegisterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterLabel.Text = "Registering ...";
            new Thread(new ParameterizedThreadStart(Set)).Start(this.LicenseTextBox.Text);
          
       }

        private void Set(object licensetext)
        {
            bool returnV = this.m_FTObjects.SetLicense((string) licensetext);
            if (returnV == true)
            {
                Success();

            }
            else
            {
                SetMessage();
            }
        }

        private void SetMessage()
        {
            if (this.RegisterLabel.InvokeRequired)
            {
                this.RegisterLabel.Invoke(new VoidDelegate(SetMessage));
                return;
            }
           
               this.RegisterLabel.Font =  new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.RegisterLabel.Text = "Register failed! Retry or email nick@foldertrack.com";
          
            
        }

        private void Success()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new VoidDelegate( Success));
                return;
            }
            this.DialogResult = DialogResult.Yes;
            this.Close();

        }

        private void DontRegButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }




    }
}