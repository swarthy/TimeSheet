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
        public static DayOfWeek[] WeekEnd = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
        MainForm mainForm;
        int lastYear;
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
            UpdateSpecialDaysFromServer(DateTime.Today.Year);
        }

        void UpdateSpecialDaysFromServer(int year)
        {
            Enabled = false;
            lastYear = year;
            specialDays = SpecialDay.GetAllForYear(year);
            calHolydays.BoldedDates = specialDays.Select<SpecialDay, DateTime>(sd => sd.Spec_Date).ToArray();
            calHolydays.UpdateBoldedDates();
            Enabled = true;
        }

        private void calHolydays_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (lastYear != e.Start.Year)
                UpdateSpecialDaysFromServer(e.Start.Year);
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
            if (!programChanges && rb != null && rb.Checked)
            {
                currentSelected.State = 0;
                currentSelected.Delete();
                calHolydays.RemoveBoldedDate(currentSelected.Spec_Date);
                calHolydays.UpdateBoldedDates();
            }                
        }

        private void rbWeekEnd_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!programChanges && rb != null && rb.Checked)
            {
                currentSelected.State = 1;
                currentSelected.Save();                
                calHolydays.AddBoldedDate(currentSelected.Spec_Date);
                calHolydays.UpdateBoldedDates();
            }
        }

        private void rbHolyDay_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!programChanges && rb != null && rb.Checked)
            {
                currentSelected.State = 2;
                currentSelected.Save();
                calHolydays.AddBoldedDate(currentSelected.Spec_Date);
                calHolydays.UpdateBoldedDates();
            }
        }

        private void rbShortDay_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!programChanges && rb != null && rb.Checked)
            {
                currentSelected.State = 3;
                currentSelected.Save();
                calHolydays.AddBoldedDate(currentSelected.Spec_Date);
                calHolydays.UpdateBoldedDates();
            }
        }

        private void btnGenerateWeekEnds_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены? Предыдущие настройки для Сб и Вс будут сброшены.\r\nЗадание новых настроек может занять некоторое время.", "Подтверждение сброса настроек", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                GenerateWeekEnd(calHolydays.SelectionStart.Year, WeekEnd);
            
        }
        void GenerateWeekEnd(int year, params DayOfWeek[] weekend)
        {
            Enabled = false;
            pbGeneratingWeekEnds.Visible = true;
            pbGeneratingWeekEnds.Value = 0;
            var exists = SpecialDay.FindAll<SpecialDay>("spec_date >= '{0}' and spec_date <= '{1}'", new DateTime(calHolydays.SelectionStart.Year, 1, 1).ToShortDateString(), new DateTime(calHolydays.SelectionStart.Year, 12, 31).ToShortDateString());
            int daysInYear = DateTime.IsLeapYear(year) ? 366 : 365;
            var start = new DateTime(year, 1, 1);
            for(int i=0;i<daysInYear;i++)
            {
                if (weekend.Contains(start.AddDays(i).DayOfWeek))
                {
                    var wed = exists.FindOrCreate(new { spec_date = start.AddDays(i) });
                    wed.State = 1;
                    wed.Save();                    
                }
                pbGeneratingWeekEnds.Value++;
            }
            UpdateSpecialDaysFromServer(year);
            pbGeneratingWeekEnds.Visible = false;
            Enabled = true;
            MessageBox.Show("Для того, чтобы изменения вступили в силу, необходимо перезапустить программу.");
        }
    }
}
