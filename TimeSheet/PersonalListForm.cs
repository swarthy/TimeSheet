using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheet
{
    public partial class PersonalListForm : Form
    {
        MainForm mainform;
        public List<Personal> personals;
        public Personal SelectedPersonal { get; set; }
        public PersonalListForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainform = mainForm;
            SelectedPersonal = null;
        }

        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            if (lbPersonal.SelectedIndex >= 0)
            {
                SelectedPersonal = personals[lbPersonal.SelectedIndex];
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите сотрудника");
        }

        void DrawPersonal(List<Personal> list)
        {
            lbPersonal.Items.Clear();
            list.ForEach(p => lbPersonal.Items.Add(p));
        }

        private void PersonalListForm_Load(object sender, EventArgs e)
        {   
            //personals = Personal.FindAll<Personal>("DEPARTMENT_ID = '{0}' and TIMESHEET_MANAGER = '{1}'", mainform.currentTimeSheet.Department.ID, mainform.currentUser.ID);
            personals = mainform.currentUser.PersonalLink.Where(p => p.Department.ID == mainform.currentTimeSheet.Department.ID).ToList();
            lbDepartmentName.Text = mainform.currentTimeSheet.Department.Name;
            personals.Sort((p1,p2)=>p1.Name.CompareTo(p2.Name));
            DrawPersonal(personals);
        }

        private void lbPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelectPersonal.Enabled = lbPersonal.SelectedIndex != -1;
        }

        private void lbPersonal_DoubleClick(object sender, EventArgs e)
        {
            if (lbPersonal.SelectedIndex!=-1)
                btnSelectPersonal_Click(this, EventArgs.Empty);
        }

        private void llbAddPersonal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            var add_per = new AddUserToFavoriteForm(mainform, this);
            var res = add_per.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                mainform.currentUser.PersonalLink.Add(add_per.SelectedPersonal);                
                mainform.currentUser.Save();            
                if (!personals.Contains(add_per.SelectedPersonal))
                    personals.Add(add_per.SelectedPersonal);
                DrawPersonal(personals);
            }
        }
        
        private void lbPersonal_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.Clicks==1)
            {                
                lbPersonal.SelectedIndex = lbPersonal.IndexFromPoint(e.Location);
                if (lbPersonal.SelectedIndex != -1)
                    cmsItem.Show(Cursor.Position);
            }
        }

        private void удалитьИзЭтогоСпискаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbPersonal.SelectedIndex != -1)
            {
                if (MessageBox.Show(string.Format("Вы действительно хотите удалить сотрудника {0} из этого списка?", lbPersonal.SelectedItem), "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    mainform.currentUser.PersonalLink.Remove((Personal)lbPersonal.SelectedItem);
                    personals.Remove((Personal)lbPersonal.SelectedItem);
                    lbPersonal.Items.Remove(lbPersonal.SelectedItem);
                }
            }
        }

    }
}
