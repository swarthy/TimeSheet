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
    public partial class AddUserToFavoriteForm : Form
    {
        MainForm mainform;
        PersonalListForm pl;
        List<Personal> personals;
        public Personal SelectedPersonal { get; set; }        
        public AddUserToFavoriteForm(MainForm mainForm, PersonalListForm personList)
        {
            InitializeComponent();
            this.mainform = mainForm;
            pl = personList;
            SelectedPersonal = null;
        }

        private void lbPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelectPersonal.Enabled = true;
        }

        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            if (lbPersonal.SelectedIndex >= 0)
            {
                SelectedPersonal = personals[lbPersonal.SelectedIndex];
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void lbPersonal_DoubleClick(object sender, EventArgs e)
        {
            btnSelectPersonal_Click(this, EventArgs.Empty);
        }

        private void AddUserToFavoriteForm_Load(object sender, EventArgs e)
        {
            lbPersonal.Items.Clear();
            personals = mainform.currentTimeSheet.Department.PersonalOfDepartment.Where(pd => pl.personals.FindIndex(p => p.ID == pd.ID) == -1).ToList();
            lbDepartmentName.Text = mainform.currentTimeSheet.Department.Name;
            personals.Sort((p1, p2) => p1.Name.CompareTo(p2.Name));
            personals.ForEach(p => lbPersonal.Items.Add(p));
        }
    }
}
