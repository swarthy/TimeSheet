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
    public partial class AdminPanelForm : Form
    {
        MainForm mainForm;
        DBList<SpecialDay> specialDays;
        bool programChanges = false;
        SpecialDay currentSelected = null;
        string[] tableNames = new string[]{
            "USERS", "PERSONAL", "POSTS", "DEPARTMENT", "FLAGS", "LPU"
        };        
        public AdminPanelForm(MainForm mainform)
        {
            mainForm = mainform;
            InitializeComponent();
        }

        private void btnEditCatalog_Click(object sender, EventArgs e)
        {
            if (cbCatalogs.SelectedIndex == -1)
                return;
            CatalogEditorForm form = new CatalogEditorForm(mainForm, tableNames[cbCatalogs.SelectedIndex], cbCatalogs.SelectedItem.ToString());
            form.Show();
        }

        private void AdminPanelForm_Load(object sender, EventArgs e)
        {
            specialDays = SpecialDay.All<SpecialDay>();
            calHolydays.BoldedDates = specialDays.Select<SpecialDay, DateTime>(sd => sd.Spec_Date).ToArray();            
        }

        private void calHolydays_DateChanged(object sender, DateRangeEventArgs e)
        {   
            currentSelected = specialDays.FindOrCreate(new { Spec_Date = e.Start });            
            programChanges = true;            
            switch (currentSelected.State)
            {
                case 0://обычный
                    rbUsualDay.Checked = true;
                    break;
                case 1://выходной
                    rbWeekEnd.Checked = true;
                    break;
                case 2://праздник
                    rbHolyDay.Checked = true;
                    break;
                case 3://сокращенный рабочий день
                    rbShortDay.Checked = true;
                    break;
            }
            programChanges = false;
        }

        private void rbUsualDay_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!programChanges && rb!=null && rb.Checked)
                currentSelected.Delete();
        }

        private void rbWeekEnd_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!programChanges && rb != null && rb.Checked)
            {
                currentSelected.State = 1;
                currentSelected.Save();
            }
        }

        private void rbHolyDay_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!programChanges && rb != null && rb.Checked)
            {
                currentSelected.State = 2;
                currentSelected.Save();
            }
        }

        private void rbShortDay_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!programChanges && rb != null && rb.Checked)
            {
                currentSelected.State = 3;
                currentSelected.Save();
            }
        }
    }
}
