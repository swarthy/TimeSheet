using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace TimeSheet
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
            lbTimeSheets.Items.Clear();            
            mainForm.currentUser.HM<TimeSheetInstance>("TimeSheets", true).ForEach(ts => lbTimeSheets.Items.Add(ts.Department.Name + " - " + ts._GetDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture)));
        }

        private void lbTimeSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOpen.Enabled = btnDelete.Enabled = lbTimeSheets.SelectedIndex != -1;            
        }

        private void lbTimeSheets_DoubleClick(object sender, EventArgs e)
        {
            OpenTimeSheet(lbTimeSheets.SelectedIndex);
        }
        void OpenTimeSheet(int index)
        {
            if (index != -1)
            {
                mainForm.currentTimeSheet = mainForm.currentUser.TimeSheets[index];
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            lbTimeSheets_DoubleClick(this, EventArgs.Empty);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            var createForm = new AddTimeSheetInstanceForm(mainForm);
            var res = createForm.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                mainForm.currentUser.TimeSheets.Add(createForm.TimeSheetIns);
                createForm.TimeSheetIns.Save();
                lbTimeSheets.Items.Add(createForm.TimeSheetIns.Department.Name + " - " + createForm.TimeSheetIns._GetDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbTimeSheets.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы действительно хотите удалить табель?", "Подтверждение удаления", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    var ind = lbTimeSheets.SelectedIndex;
                    lbTimeSheets.Items.RemoveAt(ind);
                    mainForm.currentUser.TimeSheets[ind].Delete();
                }
            }
        }
    }
}
