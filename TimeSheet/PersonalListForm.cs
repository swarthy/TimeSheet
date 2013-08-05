using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetManger
{
    public partial class PersonalListForm : Form
    {
        MainForm mainform;
        public List<UserPDP> usersPDP;
        public UserPDP SelectedUserPDP{ get; set; }
        public PersonalListForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainform = mainForm;
            SelectedUserPDP = null;
        }

        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            if (lbUsersPDP.SelectedIndex >= 0)
            {
                SelectedUserPDP = lbUsersPDP.SelectedItem as UserPDP;                
                this.DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите сотрудника");
        }

        void DrawUserPDP(List<UserPDP> list)
        {
            lbUsersPDP.Items.Clear();
            list.ForEach(p => lbUsersPDP.Items.Add(p));
        }

        private void PersonalListForm_Load(object sender, EventArgs e)
        {
            usersPDP = mainform.currentUser.UserPDP.Where(updp => updp.Department.Department_Number == mainform.currentTimeSheet.Department.Department_Number).ToList();
            lbDepartmentName.Text = mainform.currentTimeSheet.Department.Name;
            DrawUserPDP(usersPDP);
        }

        private void lbPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelectPersonal.Enabled = lbUsersPDP.SelectedIndex != -1;
        }

        private void lbPersonal_DoubleClick(object sender, EventArgs e)
        {
            if (lbUsersPDP.SelectedIndex!=-1)
                btnSelectPersonal_Click(this, EventArgs.Empty);
        }

        private void llbAddPersonal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            var add_per = new AddUserToFavoriteForm(mainform, this);
            var res = add_per.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                mainform.currentUser.UserPDP.Add(add_per.UPDP);
                mainform.currentUser.Save();
                if (!usersPDP.Contains(add_per.UPDP))
                    usersPDP.Add(add_per.UPDP);
                DrawUserPDP(usersPDP);
            }
        }
        
        private void lbPersonal_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.Clicks==1)
            {                
                lbUsersPDP.SelectedIndex = lbUsersPDP.IndexFromPoint(e.Location);
                if (lbUsersPDP.SelectedIndex != -1)
                    cmsItem.Show(Cursor.Position);
            }
        }

        private void удалитьИзЭтогоСпискаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbUsersPDP.SelectedIndex != -1)
            {
                if (MessageBox.Show(string.Format("Вы действительно хотите удалить сотрудника {0} из этого списка?", lbUsersPDP.SelectedItem), "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    mainform.currentUser.UserPDP.Remove((UserPDP)lbUsersPDP.SelectedItem);
                    usersPDP.Remove((UserPDP)lbUsersPDP.SelectedItem);
                    (lbUsersPDP.SelectedItem as UserPDP).Delete();
                    lbUsersPDP.Items.Remove(lbUsersPDP.SelectedItem);                    
                }
            }
        }

    }
}
