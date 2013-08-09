using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace TimeSheetManager
{
    public partial class TimeSheets : Form
    {
        MainForm mainForm;        
        public TimeSheets(MainForm form)
        {
            InitializeComponent();
            mainForm = form;
        }

        private void TimeSheets_Load(object sender, EventArgs e)
        {
            if (!mainForm.currentUser._IS_ADMIN)
                clmUser.Visible = false;
            grid.Rows.Clear();            
            if (mainForm.currentUser._IS_ADMIN)
                mainForm.currentLPU.Users.SelectMany(u => u.HM<TimeSheetInstance>("TimeSheets", true)).ToList().ForEach(ts => grid.Rows.Add(ts.Department.Name, ts._GetDate.ToString("MMMM yyyy"), ts.User.Profile, ts.Raschetchik, ts.ID));
            else
                mainForm.currentUser.HM<TimeSheetInstance>("TimeSheets", true).ForEach(ts => grid.Rows.Add(ts.Department.Name, ts._GetDate.ToString("MMMM yyyy"), ts.User.Profile, ts.ID));
        }

        void OpenTimeSheet(int ID)
        {
            if (ID > 0)
            {
                mainForm.currentTimeSheet = TimeSheetInstance.Get<TimeSheetInstance>(ID);
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }
        
        private void btnNew_Click(object sender, EventArgs e)
        {
            var createForm = new AddTimeSheetInstanceForm(mainForm);
            var res = createForm.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                mainForm.currentUser.TimeSheets.Add(createForm.TimeSheetIns);                
                createForm.TimeSheetIns.Save();
                grid.Rows.Add(createForm.TimeSheetIns.Department.Name, createForm.TimeSheetIns._GetDate.ToString("MMMM yyyy"), createForm.TimeSheetIns.User.Profile, createForm.TimeSheetIns.Raschetchik, createForm.TimeSheetIns.ID);
            }
            createForm.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count == 1 && MessageBox.Show("Вы действительно хотите удалить табель?", "Подтверждение удаления", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                var id = Convert.ToInt32(grid.SelectedRows[0].Cells["clmID"].Value);
                TimeSheetInstance.Get<TimeSheetInstance>(id).Delete();
                if (mainForm.currentTimeSheet.ID == id)
                {
                    mainForm.changeState(AppState.Desktop);
                    mainForm.currentTimeSheet = null;
                }
                grid.Rows.Remove(grid.SelectedRows[0]);
            }
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            OpenTimeSheet(Convert.ToInt32(grid.Rows[e.RowIndex].Cells["clmID"].Value));
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (grid.SelectedRows.Count != 1)
                return;
            OpenTimeSheet(Convert.ToInt32(grid.SelectedRows[0].Cells["clmID"].Value));
        }
    }
}
