using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SwarthyComponents;
using System.Globalization;
using SwarthyComponents.WinForms;
using SwarthyComponents.FireBird;

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
            gbAddMonth.Location = gbAddYear.Location = gbAddName.Location;
            cHolydayCalendar.OnSelectedDateChanged += new EventHandler(cHolydayCalendar_DateChanged);
            cHolydayCalendar.SelectedDate = DateTime.Today;
            if (mainform.currentUser._IS_ADMIN)
            {
                foreach (var item in SuperAdminCatalogs)
                    cbCatalogs.Items.Add(item);
                gbColors.Show();
                btnImport.Show();
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
            form.MyAfterOpenningInit();
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
            dgAllMonthData.Rows.Clear();
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
                cbPMonth.Items.Clear();
                tbDays.Text = tbHours.Text = "";
                currentCalendarName.Calendars.ForEach(c => cbPYear.Items.Add(c.CYear));
            }
        }


        private void cbPYear_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbPYear.SelectedIndex != -1)
            {
                currentCalendar = currentCalendarName.Calendars[cbPYear.SelectedIndex];
                RenderAllMonthData();
            }
            //if (cbPCalendar != null)
            //{
                cbPMonth.Items.Clear();                
                tbDays.Text = tbHours.Text = "";
                currentCalendar.Months.ForEach(m => cbPMonth.Items.Add(new DateTime(currentCalendar.CYear, m.CMonth, 1).ToString("MMMM")));                
            //}
        }

        private void btnAddPCalName_Click(object sender, EventArgs e)
        {
            gbAddName.Show();
            gbAddMonth.Hide();
            gbAddYear.Hide();
            needFreezeOtherTabs = true;
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
        bool editing = false;
        private void btnEditSaveDaysHours_Click(object sender, EventArgs e)
        {   
            if (currentContent != null)
            {
                editing = !editing;
                if (editing)
                {
                    needFreezeOtherTabs = true;
                    btnEditSaveDaysHours.Image = Properties.Resources.Save_16x16;
                    btnEditSaveDaysHours.Text = "Сохранить";
                    tbHours.Enabled = tbDays.Enabled = true;
                    groupPanelSelectControls.Enabled = gbAddYear.Enabled = gbAddName.Enabled = gbAddMonth.Enabled = dgAllMonthData.Enabled = false;
                    return;
                }
                else
                {
                    needFreezeOtherTabs = false;
                    btnEditSaveDaysHours.Image = Properties.Resources.Edit_16x16;
                    btnEditSaveDaysHours.Text = "Редактировать";
                    tbHours.Enabled = tbDays.Enabled = false;
                    groupPanelSelectControls.Enabled = gbAddYear.Enabled = gbAddName.Enabled = gbAddMonth.Enabled = dgAllMonthData.Enabled = true;
                }

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
                RenderAllMonthData();
            }
        }

        private void btnAddNewPCName_Click(object sender, EventArgs e)
        {
            if (tbPNewCName.Text.Trim().Length == 0)
            {
                gbAddName.Hide();
                return;
            }
            var cn = new Calendar_Name(tbPNewCName.Text);
            cn.Save();
            RenderPCalendars();
            gbAddName.Hide();            
        }

        private void btnAddNewPCYear_Click(object sender, EventArgs e)
        {            
            int year;
            if (!int.TryParse(tbPYear.Text, out year))
            {
                MessageBox.Show("Год введен неверно.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Calendar c = new Calendar(currentCalendarName, year);
            currentCalendarName.Calendars.Add(c);
            currentCalendarName.Save();
            currentCalendarName.Calendars.Sort((x, y) => x.CYear.CompareTo(y.CYear));
            cbPCalendar_SelectedValueChanged(sender, e);
            //RenderPCalendars();
            gbAddYear.Hide();            
        }

        private void btnAddNewPCMonth_Click(object sender, EventArgs e)
        {
            Calendar_Content c = new Calendar_Content(currentCalendar, cbPAddMonth.SelectedIndex + 1, 7 * DateTime.DaysInMonth(currentCalendar.CYear, cbPAddMonth.SelectedIndex + 1), DateTime.DaysInMonth(currentCalendar.CYear, cbPAddMonth.SelectedIndex + 1));
            currentCalendar.Months.Add(c);
            currentCalendar.Save();
            currentCalendar.Months.Sort((x, y) => x.CMonth.CompareTo(y.CMonth));
            cbPYear_SelectedValueChanged(sender, e);
            RenderAllMonthData();
            gbAddMonth.Hide();                        
        }

        private void btnAddPCYear_Click(object sender, EventArgs e)
        {
            if (currentCalendarName != null)
            {
                gbAddYear.Show();
                gbAddName.Hide();
                gbAddMonth.Hide();
                needFreezeOtherTabs = true;
            }
        }

        private void btnAddPCMonth_Click(object sender, EventArgs e)
        {
            if (currentCalendar != null)
            {
                gbAddMonth.Show();
                gbAddName.Hide();
                gbAddYear.Hide();
                needFreezeOtherTabs = true;
            }
        }

        private void btnHideName_Click(object sender, EventArgs e)
        {
            gbAddName.Hide();            
            tbPNewCName.Text = "";
            needFreezeOtherTabs = false;
        }

        private void btnHideYear_Click(object sender, EventArgs e)
        {
            gbAddYear.Hide();
            needFreezeOtherTabs = false;
        }

        private void btnHideMonth_Click(object sender, EventArgs e)
        {
            gbAddMonth.Hide();
            needFreezeOtherTabs = false;
        }

        void RenderAllMonthData()
        {
            if (cbPYear.SelectedItem != null)
            {
                dgAllMonthData.Rows.Clear();
                for(int i=0;i<DateTimeFormatInfo.CurrentInfo.MonthNames.Length-1;i++)
                {
                    var dbmonth = currentCalendar.Months.Find(m => m.CMonth == i + 1);
                    dgAllMonthData.Rows.Add(new object[] { DateTimeFormatInfo.CurrentInfo.MonthNames[i], dbmonth == null ? 0 : dbmonth.Hours, dbmonth == null ? 0 : dbmonth.Days });
                }                
            }
        }

        #endregion

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
        private void btnEditSaveDayType_Click(object sender, EventArgs e)
        {
            editing = !editing;
            if (editing)
            {
                needFreezeOtherTabs = true;
                btnEditSaveDayType.Image = Properties.Resources.Save_16x16;
                btnEditSaveDayType.Text = "Сохранить";
                tbHours.Enabled = tbDays.Enabled = true;
                groupPandelDayTypes.Enabled = true;
                dtpHolydayMonthPicker.Enabled = cHolydayCalendar.Enabled = btnGenerateWeekEnds.Enabled = false;
                return;
            }
            else
            {
                needFreezeOtherTabs = false;
                btnEditSaveDayType.Image = Properties.Resources.Edit_16x16;
                btnEditSaveDayType.Text = "Редактировать";
                groupPandelDayTypes.Enabled = false;
                dtpHolydayMonthPicker.Enabled = cHolydayCalendar.Enabled = btnGenerateWeekEnds.Enabled = true;
            }
            if (rbUsualDay.Checked)
            {
                currentSelected.State = 0;
                cHolydayCalendar.FormatedDate.Remove(currentSelected.Spec_Date);
            }
            else
                if (rbWeekEnd.Checked)
                {
                    cHolydayCalendar.FormatedDate[currentSelected.Spec_Date] = weekEnd;
                    currentSelected.State = 1;
                }
                else
                    if (rbHolyDay.Checked)
                    {
                        cHolydayCalendar.FormatedDate[currentSelected.Spec_Date] = holyDay;
                        currentSelected.State = 2;
                    }
                    else
                        if (rbShortDay.Checked)
                        {
                            cHolydayCalendar.FormatedDate[currentSelected.Spec_Date] = shortDay;
                            currentSelected.State = 3;
                        }
            if (currentSelected.State != 0)
                currentSelected.Save();
            else
                currentSelected.Delete();
            cHolydayCalendar.Refresh();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (cbCatalogs.SelectedIndex == -1)
                return;            
            ImportForm form = new ImportForm(mainForm, cbCatalogs.Text);            
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Personal.TryParseFromString("Иванов|Владимир|Анатольевич|1|12345|1");
        }
    }
}
