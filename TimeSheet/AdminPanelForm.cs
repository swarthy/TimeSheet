﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SwarthyComponents;

namespace TimeSheetManger
{
    public partial class AdminPanelForm : Form
    {
        public static string[] SuperAdminCatalogs = new string[] { "Пользователи", "Персонал", "Должности", "Отделения", "Показатели", "ЛПУ" };
        public static string[] AdminCatalogs = new string[] { "Персонал", "Должности", "Отделения" };
        public static DayOfWeek[] WeekEnd = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday };
        MainForm mainForm;
        int lastYear;
        DBList<SpecialDay> specialDays;
        bool programChanges = false;
        SpecialDay currentSelected = null;
        TabPage currentOpened = null;
        bool needFreezeOtherTabs = false;
        string[] tableNames = new string[]{
            "USERS", "PERSONAL", "POSTS", "DEPARTMENT", "FLAGS", "LPU"
        };


        DBList<Calendar_Name> AllCalNames;
        StyleSettings weekEnd, holyDay, shortDay;
        public AdminPanelForm(MainForm mainform)
        {
            mainForm = mainform;
            InitializeComponent();
            weekEnd = new StyleSettings(MainForm.weekEndColor, Color.Silver);
            holyDay = new StyleSettings(MainForm.holyDayColor, Color.Silver);
            shortDay = new StyleSettings(MainForm.shortDayColor, Color.Silver);
            cpWeekEnd.BackColor = MainForm.weekEndColor;
            cpHolyday.BackColor = MainForm.holyDayColor;
            cpShortDay.BackColor = MainForm.shortDayColor;
            cHolydayCalendar.OnSelectedDateChanged += new EventHandler(cHolydayCalendar_DateChanged);
            if (mainform.currentUser._IS_ADMIN)
            {
                foreach (var item in SuperAdminCatalogs)
                    cbCatalogs.Items.Add(item);
                gbColors.Show();
            }
            else
                if (mainform.currentUser._IS_MODERATOR)
                    foreach (var item in AdminCatalogs)
                        cbCatalogs.Items.Add(item);
        }
        private void AdminPanelForm_Load(object sender, EventArgs e)
        {
            UpdateSpecialDaysFromServer(DateTime.Today.Year);            
            RenderPCalendars();
        }

        #region Справочники
        private void btnEditCatalog_Click(object sender, EventArgs e)
        {
            if (cbCatalogs.SelectedIndex == -1)
                return;
            CatalogEditorForm form = new CatalogEditorForm(mainForm, cbCatalogs.Text);
            switch (cbCatalogs.Text)
            {
                case "Пользователи":
                    form.OpenUsers();
                    break;
                case "Персонал":
                    form.OpenPersonals();
                    break;
                case "ЛПУ":
                    form.OpenLPU();
                    break;
                case "Должности":
                    form.OpenPosts();
                    break;
                case "Отделения":
                    form.OpenDepartments();
                    break;
                case "Показатели":
                    form.OpenFlags();
                    break;
            }
            form.ShowDialog();
        }
        #endregion
        #region Праздничные, выходные, сокращенные дни
        void UpdateSpecialDaysFromServer(int year)
        {
            Enabled = false;
            lastYear = year;            
            specialDays = SpecialDay.GetAllForYear(year);
            cHolydayCalendar.FormatedDate.Clear();
            specialDays.ForEach(sd => {
                switch (sd.State)
                {
                    case 1:
                        cHolydayCalendar.FormatedDate.Add(sd.Spec_Date, weekEnd);
                        break;
                    case 2:
                        cHolydayCalendar.FormatedDate.Add(sd.Spec_Date, holyDay);
                        break;
                    case 3:
                        cHolydayCalendar.FormatedDate.Add(sd.Spec_Date, shortDay);
                        break;
                }                
            });
            //cHolydayCalendar.FormatedDate.Add(
            //calHolydays.BoldedDates = specialDays.Select<SpecialDay, DateTime>(sd => sd.Spec_Date).ToArray();
            //calHolydays.UpdateBoldedDates();
            Enabled = true;
        }

        private void cHolydayCalendar_DateChanged(object sender, EventArgs e)
        {
            if (lastYear != cHolydayCalendar.SelectedDate.Year)
                UpdateSpecialDaysFromServer(cHolydayCalendar.SelectedDate.Year);
            currentSelected = specialDays.FindOrCreate(new { Spec_Date = cHolydayCalendar.SelectedDate });            
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
            if (!programChanges && rb != null && rb.Checked && MessageBox.Show("Вы действительно хотите изменить тип дня?","Подтверждение изменений",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.Yes)
            {
                currentSelected.State = 0;
                currentSelected.Delete();
                cHolydayCalendar.FormatedDate.Remove(currentSelected.Spec_Date);
                cHolydayCalendar.Refresh();
            }                
        }

        private void rbWeekEnd_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!programChanges && rb != null && rb.Checked && MessageBox.Show("Вы действительно хотите изменить тип дня?","Подтверждение изменений",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.Yes)
            {
                currentSelected.State = 1;
                currentSelected.Save();
                cHolydayCalendar.FormatedDate[currentSelected.Spec_Date] = weekEnd;
                cHolydayCalendar.Refresh();
            }
        }

        private void rbHolyDay_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!programChanges && rb != null && rb.Checked && MessageBox.Show("Вы действительно хотите изменить тип дня?", "Подтверждение изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                currentSelected.State = 2;
                currentSelected.Save();
                cHolydayCalendar.FormatedDate[currentSelected.Spec_Date] = holyDay;
                cHolydayCalendar.Refresh();
            }
        }

        private void rbShortDay_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (!programChanges && rb != null && rb.Checked && MessageBox.Show("Вы действительно хотите изменить тип дня?", "Подтверждение изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                currentSelected.State = 3;
                currentSelected.Save();
                cHolydayCalendar.FormatedDate[currentSelected.Spec_Date] = shortDay;
                cHolydayCalendar.Refresh();
            }
        }

        private void btnGenerateWeekEnds_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены? Предыдущие настройки для Сб и Вс будут сброшены.\r\nЗадание новых настроек может занять некоторое время.", "Подтверждение сброса настроек", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                GenerateWeekEnd(cHolydayCalendar.SelectedDate.Year, WeekEnd);
            
        }
        void GenerateWeekEnd(int year, params DayOfWeek[] weekend)
        {
            Enabled = false;
            pbGeneratingWeekEnds.Visible = true;
            pbGeneratingWeekEnds.Value = 0;
            var exists = SpecialDay.FindAll<SpecialDay>("spec_date >= '{0}' and spec_date <= '{1}'", new DateTime(year, 1, 1).ToShortDateString(), new DateTime(year, 12, 31).ToShortDateString());
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
        #endregion
        #region Производственные календари
        Calendar_Name currentCalendarName = null;
        Calendar currentCalendar = null;
        Calendar_Content currentContent = null;

        void RenderPCalendars()
        {
            currentContent = null;
            currentCalendar = null;
            currentCalendarName = null;
            AllCalNames = Calendar_Name.All<Calendar_Name>();
            cbPCalendar.Items.Clear();
            cbPYear.Items.Clear();
            cbPMonth.Items.Clear();
            AllCalNames.ForEach(n => cbPCalendar.Items.Add(n));
        }



        private void cbPCalendar_SelectedValueChanged(object sender, EventArgs e)
        {
            currentCalendarName = cbPCalendar.SelectedItem as Calendar_Name;
            if (currentCalendarName != null)
            {
                cbPYear.Items.Clear();
                currentCalendarName.Calendars.ForEach(c => cbPYear.Items.Add(c.CYear));
            }
        }


        private void cbPYear_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbPYear.SelectedIndex != -1)
                currentCalendar = currentCalendarName.Calendars[cbPYear.SelectedIndex];
            if (cbPCalendar != null)
            {
                cbPMonth.Items.Clear();
                currentCalendar.Months.ForEach(m => cbPMonth.Items.Add(new DateTime(currentCalendar.CYear, m.CMonth, 1).ToString("MMMM")));
            }
        }

        #endregion

        private void btnAddPCalName_Click(object sender, EventArgs e)
        {
            gbAddName.Show();
            gbAddMonth.Hide();
            gbAddYear.Hide();
        }

        private void cbPMonth_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbPMonth.SelectedIndex != -1)
                currentContent = currentCalendar.Months[cbPMonth.SelectedIndex];
            if (currentContent != null)
            {
                tbDays.Text = currentContent.Days.ToString();
                tbHours.Text = currentContent.Hours.ToString();
            }
        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentContent != null)
            {
                double hours;
                int days;
                if (!double.TryParse(tbHours.Text, out hours))
                {
                    MessageBox.Show("Неправильно введено количество часов","Ошибка ввода");
                    return;
                }
                if (!int.TryParse(tbDays.Text, out days))
                {
                    MessageBox.Show("Неправильно введено количество дней. Только целые числа.", "Ошибка ввода");
                    return;
                }
                currentContent.Hours = hours;
                currentContent.Days = days;
                currentContent.Save();
            }
        }

        private void btnAddNewPCName_Click(object sender, EventArgs e)
        {
            if (tbNewPCName.Text.Trim().Length == 0)
            {
                gbAddName.Hide();
                return;
            }
            var cn = new Calendar_Name(tbNewPCName.Text);
            cn.Save();
            RenderPCalendars();
            gbAddName.Hide();
        }

        private void btnAddNewPCYear_Click(object sender, EventArgs e)
        {            
            Calendar c = new Calendar(currentCalendarName, dtpPCYear.Value.Year);            
            currentCalendarName.Calendars.Add(c);
            currentCalendarName.Save();
            RenderPCalendars();
            gbAddYear.Hide();
        }

        private void btnAddNewPCMonth_Click(object sender, EventArgs e)
        {
            Calendar_Content c = new Calendar_Content(currentCalendar, dtpPCMonth.Value.Month, 7 * DateTime.DaysInMonth(currentCalendar.CYear, dtpPCMonth.Value.Month), DateTime.DaysInMonth(currentCalendar.CYear, dtpPCMonth.Value.Month));
            currentCalendar.Months.Add(c);
            currentCalendar.Save();
            RenderPCalendars();
            gbAddMonth.Hide();
        }

        private void btnAddPCYear_Click(object sender, EventArgs e)
        {
            if (currentCalendarName != null)
            {
                gbAddYear.Show();
                gbAddName.Hide();
                gbAddMonth.Hide();
            }
        }

        private void btnAddPCMonth_Click(object sender, EventArgs e)
        {
            if (currentCalendar != null)
            {
                gbAddMonth.Show();
                gbAddName.Hide();
                gbAddYear.Hide();
            }
        }

        private void btnHideName_Click(object sender, EventArgs e)
        {
            gbAddName.Hide();
            tbNewPCName.Text = "";
        }

        private void btnHideYear_Click(object sender, EventArgs e)
        {
            gbAddYear.Hide();
        }

        private void btnHideMonth_Click(object sender, EventArgs e)
        {
            gbAddMonth.Hide();
        }

        private void dtpHolydayMonthPicker_ValueChanged(object sender, EventArgs e)
        {
            cHolydayCalendar.SetDate(dtpHolydayMonthPicker.Value.Month, dtpHolydayMonthPicker.Value.Year);
        }

        private void cpWeekEnd_DoubleClick(object sender, EventArgs e)
        {
            cdDayColors.Color = cpWeekEnd.BackColor;
            if (cdDayColors.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cpWeekEnd.BackColor = MainForm.weekEndColor = cdDayColors.Color;                 
                DBSettings.Set("color_weekend", cdDayColors.Color.ToArgb().ToString());
                MessageBox.Show("Чтобы изменения вступили в силу необходимо перезапустить программу", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cpHolyday_DoubleClick(object sender, EventArgs e)
        {
            cdDayColors.Color = cpHolyday.BackColor;
            if (cdDayColors.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cpHolyday.BackColor = MainForm.holyDayColor = cdDayColors.Color;                 
                DBSettings.Set("color_holyday", cdDayColors.Color.ToArgb().ToString());
                MessageBox.Show("Чтобы изменения вступили в силу необходимо перезапустить программу", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cpShortDay_DoubleClick(object sender, EventArgs e)
        {
            cdDayColors.Color = cpShortDay.BackColor;
            if (cdDayColors.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                cpShortDay.BackColor = MainForm.shortDayColor = cdDayColors.Color;                
                DBSettings.Set("color_shortday", cdDayColors.Color.ToArgb().ToString());                
                MessageBox.Show("Чтобы изменения вступили в силу необходимо перезапустить программу", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void tbContent_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (currentOpened != e.TabPage && needFreezeOtherTabs)
            {
                e.Cancel = true;
                MessageBox.Show("Переключение вкладок отменено. Сначала завершите редактирование.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                currentOpened = e.TabPage;
        }
    }
}
