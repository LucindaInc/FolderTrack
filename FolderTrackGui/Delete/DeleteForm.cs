using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using FolderTrack.Delete;

namespace FolderTrackGuiTest1.Delete
{
    public partial class DeleteForm : Form
    {
        public DeleteRules m_DeleteRules;

        delegate void VoidBoolDelegate(bool b);

        delegate void VoidStringDelegate(string s);

        public DeleteForm()
        {
            InitializeComponent();
            m_DeleteRules = new FolderTrack.Delete.DeleteRules();
            
        }

        public DeleteForm(DeleteRules rules)
        {
            InitializeComponent();
         //   this.radioButton1.Visible = false;
            m_DeleteRules = rules;
            SetDeleteRules(m_DeleteRules);
            this.PermATextBox.TextChanged += new System.EventHandler(this.PermATextBox_TextChanged);
            this.KeepLastTextBox.TextChanged += new System.EventHandler(this.KeepLastTextBox_TextChanged);
            this.KeepDescriptionsCheckBox.CheckedChanged += new System.EventHandler(this.KeepDescriptionsCheckBox_CheckedChanged);
        }

        Color inactiveColor = System.Drawing.Color.LightCoral;
        Color activeColor = System.Drawing.Color.LightBlue;

        private void SetDeleteRules(DeleteRules rules)
        {
            iSetNeverPermRadio(rules.perm_delete_never);
            iSetNPermPanel(rules.perm_delete_never);
            iSetPermRadio(!rules.perm_delete_never);
            iSetPermPanel(!rules.perm_delete_never);

            iSetKeepEverRadio(!rules.auto_delete);
            iSetKeepEverPanel(!rules.auto_delete);
            iRemoveVerRadio(rules.auto_delete);
            iRemoveVerPanel(rules.auto_delete);
            KeepLastTextBox.Text = Convert.ToString(rules.auto_delete_vers);
            KeepDescriptionsCheckBox.Checked = rules.keep_vers_with_desc;
            PermATextBox.Text = Convert.ToString(rules.perm_delete_after);
            switch(rules.deteleteinter)
            {
                case DeleteRules.DeleteI.DAY:
                    DaysRadioButton.Checked = true;
                    break;

                case DeleteRules.DeleteI.MONTH:
                    MonthsRadioButton.Checked = true;
                    break;

                case DeleteRules.DeleteI.YEAR:
                    YearsRadioButton.Checked = true;
                    break;
            }
        }

        private void NeverPermanentlyRadioButton_Click(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(tSetPermDeletNever)).Start(true);
        }

        private void NPermDeletePanel_Click(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(tSetPermDeletNever)).Start(true);
        }

        private void PermDeleRadioButton_Click(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(tSetPermDeletNever)).Start(false);
        }

        private void PermDeleteafterPanel_Click(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(tSetPermDeletNever)).Start(false);
        }

       

        private void tSetPermDeletNever(object perm_delete_neverO)
        {
            bool perm_delete_never = (bool)perm_delete_neverO;
            m_DeleteRules.perm_delete_never = perm_delete_never;

            NeverPermanentlyRadioButton.Invoke(new VoidBoolDelegate(iSetNeverPermRadio),
                new object[] {m_DeleteRules.perm_delete_never});

            NPermDeletePanel.Invoke(new VoidBoolDelegate(iSetNPermPanel),
                new object[] {m_DeleteRules.perm_delete_never});
            PermDeleRadioButton.Invoke(new VoidBoolDelegate(iSetPermRadio),
                new object[] {!m_DeleteRules.perm_delete_never});
            PermDeleteafterPanel.Invoke(new VoidBoolDelegate(iSetPermPanel),
                new object[] {!m_DeleteRules.perm_delete_never});

            tSetString(m_DeleteRules);

        }

        private void iSetNeverPermRadio(bool checkedv)
        {
            if (NeverPermanentlyRadioButton.Checked != checkedv)
            {
                NeverPermanentlyRadioButton.Checked = checkedv;
            }
        }

        private void iSetNPermPanel(bool checkedv)
        {
            if (checkedv == true)
            {
                NPermDeletePanel.BackColor = activeColor;
            }
            else
            {
                NPermDeletePanel.BackColor = inactiveColor;
            }
        }

        private void iSetPermRadio(bool checkedv)
        {
            if (PermDeleRadioButton.Checked != checkedv)
            {
                PermDeleRadioButton.Checked = checkedv;
            }
        }

        private void iSetPermPanel(bool checkedv)
        {
            if (checkedv == true)
            {
                PermDeleteafterPanel.BackColor = activeColor;
            }
            else
            {
                PermDeleteafterPanel.BackColor = inactiveColor;
            }
        }

        private void KeepEverythingradioButton_Click(object sender, EventArgs e)
        {
            
        }


        private void RemoveVeRadioButton_Click(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(tSetAutoDelete)).Start(true);
        }

        private void tSetAutoDelete(object auto_deleteO)
        {
            bool auto_delete = (bool) auto_deleteO;

            m_DeleteRules.auto_delete = auto_delete;

            radioButton2.Invoke(new VoidBoolDelegate(iSetKeepEverRadio),
                new object[] { !m_DeleteRules.auto_delete });
            KeepEverythongPanel.Invoke(new VoidBoolDelegate(iSetKeepEverPanel),
                new object[] { !m_DeleteRules.auto_delete });
            RemoveVeRadioButton.Invoke(new VoidBoolDelegate(iRemoveVerRadio),
                new object[] { m_DeleteRules.auto_delete });
            RemoveVersionPanel.Invoke(new VoidBoolDelegate(iRemoveVerPanel),
                new object[] { m_DeleteRules.auto_delete });

            tSetString(m_DeleteRules);
        }

        private void iSetKeepEverRadio(bool checkedv)
        {
            if (radioButton2.Checked != checkedv)
            {
                radioButton2.Checked = checkedv;
            }
        }

        private void iSetKeepEverPanel(bool checkedv)
        {
            if (checkedv == true)
            {
                KeepEverythongPanel.BackColor = activeColor;
            }
            else
            {
                KeepEverythongPanel.BackColor = inactiveColor;
            }
           
        }

        private void iRemoveVerRadio(bool checkedv)
        {
            if (RemoveVeRadioButton.Checked != checkedv)
            {
                RemoveVeRadioButton.Checked = checkedv;
            }
        }

        private void iRemoveVerPanel(bool checkedv)
        {
            if (checkedv == true)
            {
                RemoveVersionPanel.BackColor = activeColor;
            }
            else
            {
                RemoveVersionPanel.BackColor = inactiveColor;
            }
        }

        private void iSetDeleteString(string descrip)
        {
            DeleteLabel.Text = descrip;
        }

        public static string CreateDeleteString(DeleteRules deleterules)
        {
            string returnString = "";

            if (deleterules.perm_delete_never == true &&
                deleterules.auto_delete == false)
            {
                returnString = "Never automatically remove versions from FolderTrack and keep versions manualy removed forever";
            }
            else if(deleterules.perm_delete_never == false &&
                    deleterules.auto_delete == true)
            {
                
                returnString = AutDeleteStr(deleterules);

                returnString += ". Keep removed versions for " + SetPermString(deleterules);
            }
            else if (deleterules.auto_delete == true &&
                     deleterules.perm_delete_never == true)
            {
                returnString = AutDeleteStr(deleterules);
                returnString += " and never permanently delete anything";
            }
            else if (deleterules.auto_delete == false &&
                deleterules.perm_delete_never == false)
            {
                returnString = "Never automatically remove anything and only keep manually deleted versions for ";
                returnString += SetPermString(deleterules);

            }

            return returnString;
            
        }
        private static string AutDeleteStr(DeleteRules deleterules)
        {
            string returnString;

            if(deleterules.auto_delete_vers == 0)
                {
                    if (deleterules.keep_vers_with_desc == false)
                    {
                        returnString = "Only keep the current version in FolderTrack";
                    }
                    else
                    {
                        returnString = "Only keep the current version plus versions with descriptions in FolderTrack";
                    }
                }
                else if(deleterules.auto_delete_vers == 1)
                {
                    if (deleterules.keep_vers_with_desc == false)
                    {
                        returnString = "Only keep the current and previous created version in FolderTrack";
                    }
                    else
                    {
                        returnString = "Only keep the current, previous and versions with descriptions in FolderTrack";
                    }
                }
                else if(deleterules.auto_delete_vers == 2)
                {
                    if (deleterules.keep_vers_with_desc == false)
                    {
                        returnString = "Only keep the current and last two created versions in FolderTrack";
                    }
                    else
                    {
                        returnString = "Keep the current and the last two created versions plus versions with descriptions in FolderTrack";
                    }
                }
                else
                {
                    if (deleterules.keep_vers_with_desc == false)
                    {
                        returnString = "Only keep current and last" + (deleterules.auto_delete_vers - 1) + " created versions in FolderTrack";
                    }
                    else
                    {
                        returnString = "Keep the current and last " + (deleterules.auto_delete_vers - 1) + " created versions from FolderTrack plus all versions with descriptions";
                    }
                }

            return returnString;
        }

        private static string SetPermString(DeleteRules deleterules)
        {
            string returnString = deleterules.perm_delete_after + " " + deleterules.deteleteinter.ToString().ToLower();
            if (deleterules.perm_delete_after > 1)
            {
                returnString += "s";
            }
            returnString += " before permanently deleting";

            return returnString;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if(DaysRadioButton.Checked == true)
            {
                m_DeleteRules.deteleteinter = DeleteRules.DeleteI.DAY;
            }
            else if (MonthsRadioButton.Checked == true)
            {
                m_DeleteRules.deteleteinter = DeleteRules.DeleteI.MONTH;
            }
            else if (YearsRadioButton.Checked == true)
            {
                m_DeleteRules.deteleteinter = DeleteRules.DeleteI.YEAR;
            }

            m_DeleteRules.keep_vers_with_desc = KeepDescriptionsCheckBox.Checked;
            try
            {
                m_DeleteRules.perm_delete_after = Convert.ToInt32(PermATextBox.Text);
            }
            catch
            {
            }
            

            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

    

        private void KeepEverythingClick(object sender, EventArgs e)
        {
            new Thread(new ParameterizedThreadStart(tSetAutoDelete)).Start(false);
        }

        private void tSetString(object deleterulesO)
        {
            DeleteRules deleterules = (DeleteRules)deleterulesO;

            string deleteSt = CreateDeleteString(deleterules);

            if(DeleteLabel.InvokeRequired == true)
            {
                DeleteLabel.Invoke(new VoidStringDelegate(iSetDeleteString),
                    new object[] { deleteSt });
            }
            else
            {
                    iSetDeleteString(deleteSt);
            }
        }

        private void DaysRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.DaysRadioButton.Checked == true)
            {
                m_DeleteRules.deteleteinter = DeleteRules.DeleteI.DAY;
                new Thread(new ParameterizedThreadStart(tSetString)).Start(m_DeleteRules);

            }
        }

        private void MonthsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.MonthsRadioButton.Checked == true)
            {
                m_DeleteRules.deteleteinter = DeleteRules.DeleteI.MONTH;
                new Thread(new ParameterizedThreadStart(tSetString)).Start(m_DeleteRules);
            }
        }

        private void YearsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.YearsRadioButton.Checked == true)
            {
                m_DeleteRules.deteleteinter = DeleteRules.DeleteI.YEAR;
                new Thread(new ParameterizedThreadStart(tSetString)).Start(m_DeleteRules);
            }
        }

        private void PermATextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_DeleteRules.perm_delete_after = Convert.ToInt32(PermATextBox.Text);
                new Thread(new ParameterizedThreadStart(tSetString)).Start(m_DeleteRules);
            }
            catch
            {
            }
        }

        private void KeepLastTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                m_DeleteRules.auto_delete_vers = Convert.ToInt32(KeepLastTextBox.Text);
                new Thread(new ParameterizedThreadStart(tSetString)).Start(m_DeleteRules);
            }
            catch
            {
            }
        }

        private void KeepDescriptionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_DeleteRules.keep_vers_with_desc = KeepDescriptionsCheckBox.Checked;
            new Thread(new ParameterizedThreadStart(tSetString)).Start(m_DeleteRules);
        }
 


    }

        
}
