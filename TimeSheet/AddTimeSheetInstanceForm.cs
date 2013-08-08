using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetManager
{
    public partial class AddTimeSheetInstanceForm : Form
    {
        MainForm mainform;
        public TimeSheetInstance TimeSheetIns = null;
        public AddTimeSheetInstanceForm(MainForm mainForm)
        {
            mainform = mainForm;
            InitializeComponent();
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            if (cbDepartment.SelectedIndex != -1)
            {
                var search = TimeSheetInstance.Find<TimeSheetInstance>(new { user_id = mainform.currentUser.ID, department_number = mainform.currentLPU.Departments[cbDepartment.SelectedIndex].Department_Number, ts_year = dtpTS_Date.Value.Year, ts_month = dtpTS_Date.Value.Month });
                if (search != null)
                {
                    MessageBox.Show("Такой табель уже существует! Табельщик: "+search.User.Profile._ShortNameAndNumber);
                    return;
                }
                TimeSheetIns = new TimeSheetInstance(mainform.currentUser, mainform.currentLPU.Departments[cbDepartment.SelectedIndex], dtpTS_Date.Value.Year, dtpTS_Date.Value.Month);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите отделение!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AddTimeSheetInstanceForm_Load(object sender, EventArgs e)
        {
            cbDepartment.Items.Clear();
            mainform.currentLPU.Departments.ForEach(d => cbDepartment.Items.Add(d));
        }
    }
}
