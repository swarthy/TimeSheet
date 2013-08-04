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
using System.Xml.Linq;
using System.IO;
using System.Data.OleDb;
using System.Threading;

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
                btnExportTimeSheetXML.Show();
                btnExportTimeSheetDBF.Show();
            }
            else
                if (mainform.currentUser._IS_MODERATOR)
                    foreach (var item in AdminCatalogs)
                        cbCatalogs.Items.Add(item);
        }
        DateTime ExportStartTime;
        private void AdminPanelForm_Load(object sender, EventArgs e)
        {
            UpdateSpecialDaysFromServer(DateTime.Today.Year);            
            RenderPCalendars();
            OnDBFExportProgress += delegate {
                pbExportProgress.Increment(1);
                lbExportStatus.Text = string.Format("Обработано записей: {0}/{1}\r\nДо окончания операции: {2}", pbExportProgress.Value, pbExportProgress.Maximum, TimeSpan.FromSeconds(((DateTime.Now - ExportStartTime).TotalSeconds / pbExportProgress.Value) * (pbExportProgress.Maximum - pbExportProgress.Value)));

            };
            OnDBFExportEnd += delegate { pbExportProgress.Visible = false; Enabled = true; MessageBox.Show("Экспорт в DBF завершен."); lbExportStatus.Text = ""; };
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

        private void btnExportTimeSheetXML_Click(object sender, EventArgs e)
        {
            WaitScreen waitScreen = new WaitScreen(true);
            Helper.DirectoryCreateIfNotExists("export\\xml");
            //TimeSheetInstance.All<TimeSheetInstance>().ForEach(ts =>
            mainForm.currentLPU.Departments.SelectMany<Department, TimeSheetInstance>(d => d.TimeSheets).ToList().ForEach(ts=>
            {
                XDocument doc = new XDocument();
                var tabel = ts.XML<TimeSheetInstance>();
                ts.Content.ForEach(c =>
                {
                    var line = c.XML<TimeSheet_Content>();
                    line.Add(c.Days.XMLSerialization("Days"));
                    tabel.Add(line);
                });
                doc.Add(tabel);
                doc.Save(string.Format("export\\xml\\{0}_{1}_{2}.xml", ts._GetDate.ToShortDateString(), ts.Department.Department_Number, ts.Department.Name));
            });
            waitScreen.Close();
            MessageBox.Show("Экспорт в XML завершен");
        }
        public static EventHandler OnDBFExportProgress, OnDBFExportBegin, OnDBFExportEnd;
        void DBFTimeSheetsExport()
        {
            if (OnDBFExportBegin != null)
                OnDBFExportBegin(this, EventArgs.Empty);
            var path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "export\\dbf");            
            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=dBase IV", path);            
            OleDbConnection connection = new OleDbConnection(connectionString);
            OleDbCommand command = connection.CreateCommand();
            connection.Open();
            //Создание файла
            command.CommandText = @"CREATE TABLE Tabel (USERTN Integer, TN Integer, LName Character(50), FName Character(50), MName Character(50), DNumber Integer, DName Character(50), TSYear Integer, TSMonth Integer, PCode Integer, PName Character(50), CID Integer, CName Character(50), DayDate Date, DayHours Integer, DayFlag Character(10))";
            command.ExecuteNonQuery();
            daysForDBF.ForEach(day =>
            {
                var insrtcmd = connection.CreateCommand();
                insrtcmd.CommandText = "insert into Tabel (USERTN, TN, LName, FName, MName, DNumber, DName, TSYear, TSMonth, PCode, PName, CID, CName, DayDate, DayHours, DayFlag) VALUES (@USERTN, @TN, @LName, @FName, @MName, @DNumber, @DName, @TSYear, @TSMonth, @PCode, @PName, @CID, @CName, @DayDate, @DayHours, @DayFlag)";
                insrtcmd.Parameters.AddWithValue("@USERTN", day.TimeSheetContent.TimeSheet.User.Profile.Table_Number);//тн сотрудника в табеле
                insrtcmd.Parameters.AddWithValue("@TN", day.TimeSheetContent.Personal.Table_Number);//тн сотрудника в табеле
                insrtcmd.Parameters.AddWithValue("@LName", day.TimeSheetContent.Personal.LastName);
                insrtcmd.Parameters.AddWithValue("@FName", day.TimeSheetContent.Personal.FirstName);
                insrtcmd.Parameters.AddWithValue("@MName", day.TimeSheetContent.Personal.MiddleName);
                insrtcmd.Parameters.AddWithValue("@DNumber", day.TimeSheetContent.TimeSheet.Department.Department_Number);
                insrtcmd.Parameters.AddWithValue("@DName", day.TimeSheetContent.TimeSheet.Department.Name);
                insrtcmd.Parameters.AddWithValue("@TSYear", day.TimeSheetContent.TimeSheet.TS_Year);
                insrtcmd.Parameters.AddWithValue("@TSMonth", day.TimeSheetContent.TimeSheet.TS_Month);
                insrtcmd.Parameters.AddWithValue("@PCode", day.TimeSheetContent.Post.Code);
                insrtcmd.Parameters.AddWithValue("@PName", day.TimeSheetContent.Post.Name);
                insrtcmd.Parameters.AddWithValue("@CID", day.TimeSheetContent.Calendar.ID);
                insrtcmd.Parameters.AddWithValue("@CName", day.TimeSheetContent.Calendar.NameLink.Name);
                insrtcmd.Parameters.AddWithValue("@DayDate", day.Item_Date);
                insrtcmd.Parameters.AddWithValue("@DayHours", day.Worked_Time.TotalHours);
                insrtcmd.Parameters.AddWithValue("@DayFlag", day.Flag.Name);
                #region ConstTest
                /*insrtcmd.Parameters.AddWithValue("@USERTN", 1);//тн сотрудника в табеле
                insrtcmd.Parameters.AddWithValue("@TN", 1);//тн сотрудника в табеле
                insrtcmd.Parameters.AddWithValue("@LName", "asdf");
                insrtcmd.Parameters.AddWithValue("@FName", "asdf");
                insrtcmd.Parameters.AddWithValue("@MName", "asdf");
                insrtcmd.Parameters.AddWithValue("@DNumber", 2);
                insrtcmd.Parameters.AddWithValue("@DName", "asdf");
                insrtcmd.Parameters.AddWithValue("@TSYear", 1234);
                insrtcmd.Parameters.AddWithValue("@TSMonth", 1234);
                insrtcmd.Parameters.AddWithValue("@PCode", 1234);
                insrtcmd.Parameters.AddWithValue("@PName", "asdf");
                insrtcmd.Parameters.AddWithValue("@CID", 123);
                insrtcmd.Parameters.AddWithValue("@CName", "asdf");
                insrtcmd.Parameters.AddWithValue("@DayDate", DateTime.Today);
                insrtcmd.Parameters.AddWithValue("@DayHours", 123.5f);
                insrtcmd.Parameters.AddWithValue("@DayFlag", "v");*/
                #endregion
                insrtcmd.ExecuteNonQuery();
                if (OnDBFExportProgress!=null)
                    OnDBFExportProgress(this, EventArgs.Empty);
            });
            connection.Close();
            if (OnDBFExportEnd != null)
                OnDBFExportEnd(this, EventArgs.Empty);
        }
        DBList<TimeSheet_Day> daysForDBF;
        private void btnExportTimeSheetDBF_Click(object sender, EventArgs e)
        {
            //daysForDBF = TimeSheet_Day.All<TimeSheet_Day>();//без сортировки и по всем ЛПУ
            Enabled = false;            
            pbExportProgress.Value = 0;
            pbExportProgress.Visible = true;
            lbExportStatus.Text = "Загрузка данных...";
            daysForDBF = mainForm.currentLPU.Departments.SelectMany<Department, TimeSheetInstance>(d => d.TimeSheets).SelectMany<TimeSheetInstance, TimeSheet_Content>(ts => ts.Content).SelectMany<TimeSheet_Content, TimeSheet_Day>(tc => tc.Days).ToDBList();
            pbExportProgress.Maximum = daysForDBF.Count;
            ExportStartTime = DateTime.Now;
            var destinationDir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "export\\dbf");
            Helper.DirectoryCreateIfNotExists(destinationDir);
            File.Delete(Path.Combine(destinationDir, "Tabel.dbf"));            
            DBFTimeSheetsExport();
            Enabled = true;
        }
    }
}
