﻿using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Threading;
using SwarthyComponents.FireBird;
using System.Diagnostics;
using NetWork;
using Client;

namespace TimeSheetManager
{
    public partial class MainForm : Form
    {        
        public static Color weekEndColor = Color.MistyRose;
        public static Color holyDayColor = Color.LightCoral;
        public static Color shortDayColor = Color.BurlyWood;
        public static bool Closing = false;
        public AppState State = AppState.LPUselect;
        public DBList<LPU> LPUlist = new DBList<LPU>();
        public DBList<Department> Departmentlist = new DBList<Department>();
        public DBList<Post> Posts = new DBList<Post>();
        public DBList<Calendar> Calendars = new DBList<Calendar>();
        public DBList<Flag> Flags = new DBList<Flag>();
        public static DBList<SpecialDay> specialDays;        
        public LPU currentLPU { get; set; }
        public static User curUsr = null;
        public User currentUser
        {
            get
            {
                return curUsr;
            }
            set
            {
                curUsr = value;
                if (value != null)
                {
                    miCurrentUser.Text = string.Format("{0} ({1})",value.Login, value._RoleString);
                    miAdminPanel.Visible = curUsr._IS_MODERATOR;
                    if (value._IS_ADMIN)                    
                        AdminAfterLoginCheck();                    
                }
            }
        }
        public TimeSheetInstance currentTimeSheet { get; set; }
        public static ClientIOAsync Client;

        public string StatusLeft
        {
            get
            {
                return tsslStatusLeft.Text;
            }
            set
            {
                tsslStatusLeft.Text = value;
            }
        }
        public string StatusRight
        {
            get
            {
                return tsslStatusRight.Text;
            }
            set
            {
                tsslStatusRight.Text = value;
            }
        }

        FlagsForm flagForm;        

        public bool Saving = false;

        public MainForm()
        {   
            InitializeComponent();            
            ttsVersion.Text = "Версия: " + Helper.GetFileVersion(Application.ExecutablePath);
            Helper.settings = new IniFile(Environment.CurrentDirectory + @"\settings.ini");
            dlgSaveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);            
            #region DB Initialization
            #if DEBUG
            DBHelper.Log += Helper.Log;//подписываемся на логи
            #endif            
            DB.ConnectionString = string.Format("UserID=SYSDBA;Password=masterkey;Database={0}:{1};Charset=NONE;", Helper.ServerIP, Helper.ServerFile);            
            Domain.VirtualDeletionField = "DELDATE";
            Domain.VirtualDeletionNotDeletedRecord = "DELDATE is null";
            Domain.VirtualDeletedValue = () => { return DateTime.Today; };

            User.Initialize<User>();
            Personal.Initialize<Personal>();
            LPU.Initialize<LPU>();
            Post.Initialize<Post>();
            Department.Initialize<Department>();            
            Calendar.Initialize<Calendar>();
            Calendar_Name.Initialize<Calendar_Name>();
            Calendar_Content.Initialize<Calendar_Content>();
            TimeSheetInstance.Initialize<TimeSheetInstance>();
            TimeSheet_Content.Initialize<TimeSheet_Content>();
            TimeSheet_Day.Initialize<TimeSheet_Day>();
            Flag.Initialize<Flag>();
            SpecialDay.Initialize<SpecialDay>();
            DBSettings.Initialize<DBSettings>();
            UserPDP.Initialize<UserPDP>();
            #endregion
        }
        void StartUpdate()
        {
            if (!File.Exists("Updater.exe"))
            {
                using (FileStream exeFile = new FileStream("Updater.exe", FileMode.Create))
                    exeFile.Write(Properties.Resources.Updater, 0, Properties.Resources.Updater.Length);
            }
            ProcessStartInfo si = new ProcessStartInfo("cmd", string.Format("/C ping 127.0.0.1 -n 3 > nul && updater.exe {0} {1}", Helper.ServerUpdatePath, Path.GetFileName(Application.ExecutablePath)));
            si.CreateNoWindow = false;
            si.UseShellExecute = false;
            si.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(si);
            Environment.Exit(0);
        }
        void AdminAfterLoginCheck()
        {
            WaitScreen waitScreen = new WaitScreen(true);
            StatusLeft = "Проверка корректности БД.";
            if (currentLPU.MainDoc == null)
            {
                waitScreen.Close();
                MessageBox.Show("У данного ЛПУ не указан главный врач.\r\nВозможно нарушение отчетности.", "Нарушена целостность данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            waitScreen.Show();
            var invalidDepartments = currentLPU.Departments.FindAll(d => d.DepartmentManager == null);
            waitScreen.Close();
            Ready();
            if (invalidDepartments.Count != 0)            
                MessageBox.Show("У следующих отделений не указан заведующий: " +
                       invalidDepartments.Aggregate<Department, string>("", (s, d) => s += "\r\n" + d.Name),
                       "Нарушена целостность данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        Color GetColor(string key)
        {            
            var saved = DBSettings.Get("color_" + key);
            if (saved=="")
                return key == "weekend" ? Color.MistyRose : key == "holyday" ? Color.LightCoral : key == "shortday" ? Color.BurlyWood : Color.Black;
            return Color.FromArgb(Convert.ToInt32(saved));
        }
        void HideAllShowThis(Panel p)
        {            
            foreach (Control c in Controls)
                if (c.GetType() == typeof(Panel))                
                    c.Hide();                                    
            p.Show();
        }
        public void Ready()
        {
            StatusLeft = "Готово";
        }
        public void ReadyR()
        {
            StatusRight = "";
        }
        bool wantChangeLPU = false;
        public void changeState(AppState newstate)
        {
            switch (newstate)
            {
                case AppState.LPUselect:
                    var lastOpenedData = Helper.Get("LPU", "lastOpened");
                    var lastOpened = lastOpenedData == "" ? 0 : Convert.ToInt32(lastOpenedData);
                    if (lastOpened != 0 && !wantChangeLPU)
                    {                        
                        currentLPU = LPU.Get<LPU>(lastOpened);
                        if (currentLPU != null)
                        {
                            changeState(AppState.Auth);
                            return;
                        }
                    }
                    msMainMenu.Hide();
                    HideAllShowThis(pLPUSelection);
                    break;
                case AppState.Auth:
                    lbAuthSelectedLPU.Text = currentLPU.Name;
                    tbAuthLogin.Text = Helper.Get("user", "lastLogin").ToString();
                    msMainMenu.Hide();
                    HideAllShowThis(pAuth);
                    break;
                case AppState.Desktop:                    
                    HideAllShowThis(pDesktop);
                    currentUser.TimeSheets.Sort((t1, t2) =>                    
                        t1.Department.Name.CompareTo(t2.Department.Name)                        
                   );
                    msMainMenu.Show();
                    break;                
                case AppState.EditTimeSheet:
                    WaitScreen waitScreen = new WaitScreen(true);                    
                    specialDays = SpecialDay.GetAllForYear(currentTimeSheet._GetDate.Year);                    
                    UpdateColumns(currentTimeSheet);                    
                    Calendars = Calendar.FindAll<Calendar>(new { cyear = currentTimeSheet._GetDate.Year });                    
                    tbCurrentDepartment.Text = currentTimeSheet.Department.Name;
                    tbCurrentDepartmentManager.Text = currentTimeSheet.Department.DepartmentManager == null ? "" : currentTimeSheet.Department.DepartmentManager._FullName;
                    tbCurrentTimeSheetLastEditor.Text = currentTimeSheet.LastEditor == null ? "" : currentTimeSheet.LastEditor.Profile._FullName;
                    lbCurrentTimeSheetName.Text = string.Format("{0} - {1} Расчетчик: {2}", currentTimeSheet.Department.Name, currentTimeSheet._GetDate.ToString("MMMM yyyy"), currentTimeSheet.Raschetchik);
                    DrawTimeSheetContent();
                    HideAllShowThis(pWorkspace);
                    waitScreen.Close();                                    
                    break;
            }
            State = newstate;
        }

        private void DrawTimeSheetContent()
        {
            dgTimeSheet.Rows.Clear();
            dgTimeSheet.StartColumnSelect = dgTimeSheet.StartRowSelect = 0;
            var content = currentTimeSheet.Content;            
            content.Sort((x, y) => x.Personal.Priority == y.Personal.Priority ? x.Priority == y.Priority ? x.Personal.LastName.CompareTo(y.Personal.LastName) : y.Priority.CompareTo(x.Priority) : y.Personal.Priority.CompareTo(x.Personal.Priority));
            content.ForEach(row =>
            {   
                var temp = row.Render(dgTimeSheet);
                if (temp != null)
                    dgTimeSheet.Rows.AddRange(temp);
            }
            );
            dgTimeSheet.StopRowSelect = dgTimeSheet.Rows.Count - 1;
            dgTimeSheet.StopColumnSelect = dgTimeSheet.Columns.Count - 1;
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DB.Connection.State == ConnectionState.Open)
            {
                DB.Connection.Close();
                DB.Connection.Dispose();
            }
            if (Client != null && Client.Connected)
            {
                Client.Disconnect();
                Client.Dispose();
            }
        }
        void SetControlsEnabled(bool value)
        {            
            foreach (Control c in Controls)
                if (c.Name != "statusBar")
                    c.Enabled = value;
        }

        WaitScreen connectWaitScreen = new WaitScreen();
        private void MainForm_Load(object sender, EventArgs e)
        {            
            ExcelManager.OnProgress += delegate { Invoke((Action)(() => tspbProgress.Increment(1))); };
            ExcelManager.OnSavingStart += delegate { Invoke((Action)(() => { StatusLeft = "Сохранение..."; tspbProgress.Visible = false; })); };
            ExcelManager.OnExportEnd += delegate { Invoke((Action)(() => { Ready(); })); };
                        
            Domain.OnFindBegin += delegate { Invoke((Action)(() => { StatusRight = "Запрос к БД..."; })); };
            Domain.OnFindEnd += delegate { Invoke((Action)(() => { ReadyR();})); };
            if (!Helper.IgnoreServerOffline)
            {
                Client = new ClientIOAsync();            
                Client.OnReceiveData += (data) => { if (Closing)return; MessageParse(data); };
                Client.OnConnecting += delegate { if (Closing)return; tssServerConnection.Text = "Подключение..."; tssServerConnection.Image = Properties.Resources.Synchronize_16x16; Invoke((Action)(() => {  connectWaitScreen.Show(); })); };
                Client.OnConnected += delegate { if (Closing)return; tssServerConnection.Text = "Online"; tssServerConnection.Image = Properties.Resources.Hard_Disk_16x16; Invoke((Action)(() => {  connectWaitScreen.Close(); Client.Send(new NetData(Command.ClientVersion, Helper.AppVersion)); })); };
                Client.OnServerNotAvailable += delegate { if (Closing)return; tssServerConnection.Text = "Сервер недоступен"; tssServerConnection.Image = Properties.Resources.Cancel_16x16; Invoke((Action)(() => {  connectWaitScreen.Close(); })); };
                Client.Connect(Helper.ServerIP, 23069);
            }
            LPUlist = LPU.All<LPU>();            
            cbLPUList.Items.Clear();            
            foreach (LPU lpu in LPUlist)            
                cbLPUList.Items.Add(lpu);
            Posts = Post.All<Post>();
            Flags = Flag.All<Flag>();            
            changeState(AppState.LPUselect);  //release
            #if DEBUG
            currentLPU = LPU.Get<LPU>(1);            
            currentUser = User.Get<User>(71);            
            changeState(AppState.Desktop);
            miAdminPanel_Click(this, e);
            #endif
            ActiveControl = tbAuthPass;
        }

        private void btnLPUChoiceEnter_Click(object sender, EventArgs e)
        {
            if (cbLPUList.SelectedIndex != -1)
            {
                currentLPU = (LPU)cbLPUList.SelectedItem;
                Helper.Set("LPU", "lastOpened", currentLPU.ID);
                changeState(AppState.Auth);
            }
        }

        private void btnLoginEnter_Click(object sender, EventArgs e)
        {
            User user = User.Find<User>("login = '{0}' and pass = '{1}' and lpu_id = '{2}'", tbAuthLogin.Text, tbAuthPass.Text, currentLPU.ID);                        
            if (user == null)
                MessageBox.Show("Неверное имя пользователя и/или пароль", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (Client!=null)
                    Client.Send(new NetData(Command.Login, user.ID.ToString()));
                Helper.Set("user", "lastLogin", tbAuthLogin.Text);
                currentUser = user;
                changeState(AppState.Desktop);
                tbAuthPass.Text = "";                
            }
        }

        private void btnLPUSelect_Click(object sender, EventArgs e)
        {
            wantChangeLPU = true;
            changeState(AppState.LPUselect);
        }

        private void btnTimeSheetList_Click(object sender, EventArgs e)
        {
            TimeSheets tsForm = new TimeSheets(this);            
            if (tsForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)            
                changeState(AppState.EditTimeSheet);            
        }

        public void UpdateColumns(TimeSheetInstance ts)
        {
            var daysCount = ts._DaysInMonth;
            dgTimeSheet.Columns.Clear();

            var prior = dgTimeSheet.Columns.Add("cPriority", "П");
            dgTimeSheet.Columns[prior].Frozen = true;
            dgTimeSheet.Columns[prior].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            var fio = dgTimeSheet.Columns.Add("cFIO", "ФИО");
            dgTimeSheet.Columns[fio].Frozen = true;
            dgTimeSheet.Columns[fio].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            var tn = dgTimeSheet.Columns.Add("cTimeSheetNumber", "Т/н");
            dgTimeSheet.Columns[tn].Frozen = true;
            dgTimeSheet.Columns[tn].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgTimeSheet.Columns.Add("cPost", "Должность");
            dgTimeSheet.Columns[dgTimeSheet.Columns.Add("cRate", "Ставка")].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            var date = ts._GetDate;
            for (int i = 0; i < daysCount; i++)
            {
                var ind = dgTimeSheet.Columns.Add(string.Format("cDay{0}", i + 1), date.ToString("d ddd"));                
                var spec = specialDays.Find(sd => sd.Spec_Date == date);
                if (spec != null)
                {
                    switch (spec.State)
                    {
                        case 1:
                            dgTimeSheet.Columns[ind].DefaultCellStyle.BackColor = weekEndColor;
                            break;
                        case 2:
                            dgTimeSheet.Columns[ind].DefaultCellStyle.BackColor = holyDayColor;
                            break;
                        case 3:
                            dgTimeSheet.Columns[ind].DefaultCellStyle.BackColor = shortDayColor;
                            break;
                    }
                }                
                dgTimeSheet.Columns[ind].MinimumWidth = 55;
                dgTimeSheet.Columns[ind].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgTimeSheet.Columns[ind].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgTimeSheet.Columns[ind].Tag = date;
                date = date.AddDays(1);
            }
   
            dgTimeSheet.Columns.Add("cDaysCount1", "Дни явок");            
            dgTimeSheet.Columns.Add("cHoursCount1", "Часов всего");            
            dgTimeSheet.Columns.Add("cHoursCount2", "Часов ночных");            
            dgTimeSheet.Columns.Add("cHoursCount3", "Часов выходных, праздничных");            

            for (int i = 0; i < dgTimeSheet.Columns.Count; i++)
            {                
                dgTimeSheet.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgTimeSheet.Columns[i].ReadOnly = true;
            }
        }

        private void dgTimeSheet_KeyDown(object sender, KeyEventArgs e)
        {
            flagForm = new FlagsForm(this);
            if (dgTimeSheet.SelectedCells.Count > 0 && !Saving)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (e.Shift)
                            miEditPersonal_Click(this, EventArgs.Empty);                        
                        else
                            редактироватьВыделеннуюЗаписьToolStripMenuItem_Click(this, EventArgs.Empty);
                        break;                    
                    case Keys.Delete:
                        if (e.Shift)
                            удалитьЗаписиЭтогоСотрудникаToolStripMenuItem_Click(this, EventArgs.Empty);
                        else
                            удалитьВыделеннуюЗаписьToolStripMenuItem_Click(this, EventArgs.Empty);
                        break;
                    case Keys.Space:
                        miAddMore_Click(this, EventArgs.Empty);
                        break;                    
                }                
            }
        }

        private void miLogout_Click(object sender, EventArgs e)
        {
            currentUser = null;
            if (Client!=null)
                Client.Send(new NetData(Command.Logout, ""));
            changeState(AppState.Auth);
        }

        private void miAdminPanel_Click(object sender, EventArgs e)
        {
            AdminPanelForm admin = new AdminPanelForm(this);
            admin.ShowDialog();
            if (State == AppState.EditTimeSheet)
                changeState(AppState.EditTimeSheet);            
        }
        
        private void dgTimeSheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
                return;
            if (e.ColumnIndex <= 4)
            {
                miEditPersonal_Click(sender, EventArgs.Empty);
                return;
            }
            var cell = dgTimeSheet[e.ColumnIndex, e.RowIndex];
            if (cell.OwningColumn.Tag!=null && cell.OwningColumn.Tag.GetType() == typeof(DateTime))
            {
                if (currentTimeSheet.Content.Count == 0)
                    return;
                int contentPosition, contentOldCount;
                var content = GetContentForDayByCell(cell, out contentPosition, out contentOldCount);
                if (content._IsPercentType)
                    return;
                FlagsForm ff = new FlagsForm(this, cell.Value as TimeSheet_Day);
                if (ff.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                    
                    WaitScreen waitScreen = new WaitScreen(true);
                    if (cell.Value == null)
                    {
                        var newCell = new TimeSheet_Day(Convert.ToDateTime(cell.OwningColumn.Tag), ff.worked_time, ff.flag);
                        content.Days.Add(newCell);
                        newCell.Save();
                        cell.Value = newCell;
                    }
                    else
                    {
                        var existCell = cell.Value as TimeSheet_Day;
                        existCell.Flag = ff.flag;
                        existCell.Worked_Time = ff.worked_time;
                        existCell.Save();
                    }
                    currentTimeSheet.LastEditor = currentUser;
                    currentTimeSheet.Save();
                    DrawContent(content, contentPosition, contentOldCount);                    
                    waitScreen.Close();
                }
            }
        }
        void DrawContent(TimeSheet_Content content, int rowStart, int rowCount)
        {
            var updatedrows = content.Render(dgTimeSheet);
            for (int i = 0; i < rowCount; i++)
                dgTimeSheet.Rows.RemoveAt(rowStart);
            dgTimeSheet.Rows.InsertRange(rowStart, updatedrows);            
        }
        TimeSheet_Content GetContentForDayByCell(DataGridViewCell cell, out int rowPos, out int rowCount)
        {
            var contentRow = cell.OwningRow.Index;
            while (contentRow >= 0 && dgTimeSheet[1, contentRow].Value == null)
                contentRow--;
            if (contentRow < 0)
                throw new Exception("Что-то пошло не так. Не могу найти к кому отношусь :(\r\nС Уважением, Ячейка Нажатая");
            var content = dgTimeSheet[1, contentRow].Value as TimeSheet_Content;
            rowPos = contentRow;
            rowCount = dgTimeSheet[1, contentRow].Tag == null ? 1 : Convert.ToInt32(dgTimeSheet[1, contentRow].Tag);
            return content;
        }
        private void miAddMore_Click(object sender, EventArgs e)
        {
            if (dgTimeSheet.SelectedCells.Count == 1 && dgTimeSheet.SelectedCells[0].OwningColumn.Tag != null && dgTimeSheet.SelectedCells[0].OwningColumn.Tag.GetType() == typeof(DateTime) && !Saving)
            {                
                var cell = dgTimeSheet.SelectedCells[0];                
                FlagsForm ff = new FlagsForm(this, cell.Value as TimeSheet_Day);
                if (ff.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                    
                    WaitScreen waitScreen = new WaitScreen(true);
                    int rowStart, rowCount;
                    var content = GetContentForDayByCell(cell, out rowStart, out rowCount);
                    var newCell = new TimeSheet_Day(Convert.ToDateTime(cell.OwningColumn.Tag), ff.worked_time, ff.flag);
                    content.Days.Add(newCell);
                    newCell.Save();                    
                    DrawContent(content, rowStart, rowCount);
                    currentTimeSheet.LastEditor = currentUser;
                    currentTimeSheet.Save();
                    waitScreen.Close();
                }                
            }
        }

        private void miEditPersonal_Click(object sender, EventArgs e)
        {
            if (dgTimeSheet.SelectedCells.Count == 1)
            {
                var cell = dgTimeSheet.SelectedCells[0];
                int rowStart, rowCount;
                var content = GetContentForDayByCell(cell, out rowStart, out rowCount);
                var roweditor = new RowEditForm(this, content);
                if (roweditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                    
                    WaitScreen waitScreen = new WaitScreen(true);
                    roweditor.TSContent.Save();
                    if (roweditor.TSContent._PriorityChanged)
                        DrawTimeSheetContent();
                    else
                        DrawContent(content, rowStart, rowCount);
                    currentTimeSheet.LastEditor = currentUser;
                    currentTimeSheet.Save();
                    waitScreen.Close();
                }
            }
        }

        private void btnNewRow_Click(object sender, EventArgs e)
        {
            var roweditor = new RowEditForm(this, null, true);
            if (roweditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {                
                WaitScreen waitScreen = new WaitScreen(true);
                var newContent = roweditor.TSContent;                
                var cal = newContent.Calendar.Months.Find(m => m.CMonth == currentTimeSheet.TS_Month);
                double avg = 0;
                if (cal != null)
                    avg = cal.Hours / cal.Days;
                newContent.Save();
                if (roweditor.defaultval)
                {
                    for (int i = 0; i < currentTimeSheet._DaysInMonth; i++)   
                    {
                        var date = new DateTime(currentTimeSheet.TS_Year, currentTimeSheet.TS_Month, i + 1);
                        var sp = specialDays.Find(sd => sd.Spec_Date == date);
                        Flag flag = Flags.Find(f => f.Name == "ya");//default
                        TimeSpan worked = TimeSpan.FromHours(avg);
                        if (sp != null)
                        {
                            switch (sp.State)
                            {
                                case 1://выходной
                                case 2://праздник
                                    flag = Flags.Find(f => f.Name == "v");
                                    worked = TimeSpan.Zero;
                                    break;
                                case 3://сокращенный
                                    worked = worked.Subtract(TimeSpan.FromHours(1));
                                    break;
                            }
                        }
                        newContent.Days.Add(new TimeSheet_Day(date, worked, flag));
                    }
                    newContent.Save();
                }
                currentTimeSheet.LastEditor = currentUser;
                currentTimeSheet.Save();
                DrawTimeSheetContent();                
                waitScreen.Close();
            }            
        }

        private void удалитьВыделеннуюЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgTimeSheet.SelectedCells.Count == 0 || Saving)
                return;
            if (MessageBox.Show(string.Format("Вы действительно хотите удалить выделенн{0} запис{1}?", dgTimeSheet.SelectedCells.Count > 1 ? "ые" : "ую", dgTimeSheet.SelectedCells.Count > 1 ? "и" : "ь"), "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                return;            
            WaitScreen waitScreen = new WaitScreen(true);
            var firstCell = dgTimeSheet.SelectedCells[0];
            int rowStart, rowCount;
            var content = GetContentForDayByCell(firstCell, out rowStart, out rowCount);
            foreach (DataGridViewCell cell in dgTimeSheet.SelectedCells)
            {
                if (cell.Value != null && cell.OwningColumn.Tag != null && cell.OwningColumn.Tag.GetType() == typeof(DateTime))
                {
                    var day = cell.Value as TimeSheet_Day;                    
                    content.Days.Remove(day, true);
                }
            }
            currentTimeSheet.LastEditor = currentUser;
            currentTimeSheet.Save();
            DrawContent(content, rowStart, rowCount);            
            waitScreen.Close();
        }

        private void удалитьЗаписиЭтогоСотрудникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgTimeSheet.SelectedCells.Count == 1 && MessageBox.Show("Вы уверены, что хотите удалить выделенную запись?","Подтверждение удаления",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.Yes)
            {
                WaitScreen waitScreen = new WaitScreen(true);
                var cell = dgTimeSheet.SelectedCells[0];                
                int rowStart, rowCount;
                var content = GetContentForDayByCell(cell, out rowStart, out rowCount);
                currentTimeSheet.Content.Remove(content);
                content.Delete();
                for (int i = 0; i < rowCount; i++)
                    dgTimeSheet.Rows.RemoveAt(rowStart);
                currentTimeSheet.LastEditor = currentUser;
                currentTimeSheet.Save();
                waitScreen.Close();
            }
        }

        private void редактироватьВыделеннуюЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            if (dgTimeSheet.SelectedCells.Count > 0 && !Saving)
            {
                var firstCell = dgTimeSheet.SelectedCells[dgTimeSheet.SelectedCells.Count - 1];
                int contentPosition, contentOldCount;
                var content = GetContentForDayByCell(firstCell, out contentPosition, out contentOldCount);
                if (content._IsPercentType)
                    return;
                FlagsForm ff = new FlagsForm(this, firstCell.Value as TimeSheet_Day);
                if (ff.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                    
                    WaitScreen waitScreen = new WaitScreen(true);
                    foreach (DataGridViewCell cell in dgTimeSheet.SelectedCells)
                    {
                        if (cell.OwningColumn.Tag == null || cell.OwningColumn.Tag.GetType() != typeof(DateTime))
                            continue;
                        if (cell.Value == null)
                        {
                            var newCell = new TimeSheet_Day(Convert.ToDateTime(cell.OwningColumn.Tag), ff.worked_time, ff.flag);
                            content.Days.Add(newCell);
                            newCell.Save();
                            cell.Value = newCell;
                        }
                        else
                        {
                            var existCell = cell.Value as TimeSheet_Day;
                            existCell.Flag = ff.flag;
                            existCell.Worked_Time = ff.worked_time;
                            existCell.Save();
                        }                        
                    }
                    currentTimeSheet.LastEditor = currentUser;
                    currentTimeSheet.Save();
                    DrawContent(content, contentPosition, contentOldCount);                    
                    waitScreen.Close();
                }
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            dlgSaveFile.FileName = string.Format("{0} - {1}", currentTimeSheet.Department.Name, currentTimeSheet._GetDate.ToString("MMMM yyyy"));
            dlgSaveFile.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + @"\export";
            if (currentTimeSheet.Content.Count > 0 && dlgSaveFile.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                StatusLeft = "Заполнение шаблона...";
                tspbProgress.Value = 0;
                tspbProgress.Maximum = currentTimeSheet.Content.Count;
                tspbProgress.Visible = true;
                var tempPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\template\temp.xlsx";
                if (!File.Exists(tempPath))
                {
                    MessageBox.Show("Файл шаблона temp.xlsx не найден!", "Ошибка экспорта", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                WaitScreen waitScreen = new WaitScreen(true);
                try
                {
                    Thread exprt = new Thread(a => ExcelManager.ExportContent(currentTimeSheet, System.IO.Path.GetDirectoryName(Application.ExecutablePath) + @"\template\temp.xlsx", dlgSaveFile.FileName));
                    exprt.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка экспорта", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    waitScreen.Close();
                }
                waitScreen.Close();
                //Process.Start("export\\");
            }
        }
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            #if !DEBUG
            if (MessageBox.Show("Завершить работу с программой?", "Подтверждение выхода", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
            else
#endif
                Closing = true;
        }

        private void tbAuthPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                btnLoginEnter_Click(this, EventArgs.Empty);                                
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Helper.EnvExitWithSocketAndDBClosing();
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();            
        }

        private void cmsDaysMenu_Opening(object sender, CancelEventArgs e)
        {
            if (dgTimeSheet.SelectedCells.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            var firstCell = dgTimeSheet.SelectedCells[dgTimeSheet.SelectedCells.Count - 1];
            bool state = firstCell.OwningColumn.Tag != null && firstCell.OwningColumn.Tag.GetType() == typeof(DateTime);                            
            редактироватьВыделеннуюЗаписьToolStripMenuItem.Enabled = miAddMore.Enabled = удалитьВыделеннуюЗаписьToolStripMenuItem.Enabled = state;
            miEditPersonal.Enabled = удалитьЗаписиЭтогоСотрудникаToolStripMenuItem.Enabled = !state;            
        }

        private void reconnectTimer_Tick(object sender, EventArgs e)
        {
            if (Client != null && !Client.Connected)            
                Client.Reconnect();            
        }
        private void MessageParse(NetData data)
        {            
            switch(data.CMD)
            {
                case Command.Notification:
                    MessageBox.Show(data.Text, "Системное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case Command.ServerIsShutingDown:
                    MessageBox.Show("Сервер завершает работу. Приложение будет закрыто.", "Системное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Environment.Exit(0);
                    //Helper.EnvExitWithSocketAndDBClosing();
                    break;
                case Command.LetsUpdate:
                    MessageBox.Show("Требуется обновить программу. Нажмите Ок для продолжения.", "Системное сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StartUpdate();
                    break;
            }
        }
    }
    public enum AppState
    {
        LPUselect,
        Auth,
        Desktop,
        Workspace,
        EditTimeSheet
    }
}
