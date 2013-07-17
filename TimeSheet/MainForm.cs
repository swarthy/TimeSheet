using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using TimeSheet.Properties;
using System.Globalization;

/*
 * 
 * 
 * 
 * 
 * 
 * 
 *              TODO: Календарь переработать структуру, а то у календаря может быть названия
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */
namespace TimeSheet
{
    public partial class MainForm : Form
    {
        //BindingSource timeSheetSource;
        //DataTable timeSheetTable = new DataTable();
        public static Color weekEndColor = Color.MistyRose;
        public static Color holyDayColor = Color.LightCoral;
        public static Color shortDayColor = Color.BurlyWood;
        public AppState State = AppState.LPUselect;
        public DBList<LPU> LPUlist = new DBList<LPU>();
        public DBList<Department> Departmentlist = new DBList<Department>();
        public DBList<Post> Posts = new DBList<Post>();
        public DBList<Calendar_Content> Calendars = new DBList<Calendar_Content>();
        public DBList<Flag> Flags = new DBList<Flag>();
        public DBList<SpecialDay> specialDays;
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
                btnAdminPanel.Visible = btnAdminPanel.Enabled = curUsr.Is_admin;
            }
        }
        public TimeSheetInstance currentTimeSheet { get; set; }

        FlagsForm flagForm;        

        public bool Saving = false;

        public MainForm()
        {            
            InitializeComponent();
            #region DB Initialization
            User.Initialize<User>();
            Personal.Initialize<Personal>();
            LPU.Initialize<LPU>();
            Post.Initialize<Post>();
            Department.Initialize<Department>();
            UserDepartment.Initialize<UserDepartment>();
            Calendar.Initialize<Calendar>();
            Calendar_Content.Initialize<Calendar_Content>();
            TimeSheetInstance.Initialize<TimeSheetInstance>();
            TimeSheet_Content.Initialize<TimeSheet_Content>();
            TimeSheet_Day.Initialize<TimeSheet_Day>();
            Flag.Initialize<Flag>();
            SpecialDay.Initialize<SpecialDay>();
            #endregion                                                            
            //timeSheetSource = new BindingSource();
            //timeSheetSource.DataSource = timeSheetTable;
            //dgTimeSheet.DataSource = timeSheetSource;
        }
        void HideAllShowThis(Panel p)
        {            
            foreach (Control c in Controls)            
                if (c.GetType() == typeof(Panel))
                    c.Hide();            
            p.Show();
        }
        void changeState(AppState newstate)
        {
            switch (newstate)
            {
                case AppState.LPUselect:
                    HideAllShowThis(pLPUSelection);
                    break;
                case AppState.Auth:
                    tbAuthLogin.Text = Helper.Get("lastLogin").ToString();
                    HideAllShowThis(pAuth);
                    break;
                case AppState.Workspace:                    
                    HideAllShowThis(pWorkspace);
                    currentUser.TimeSheets.Sort((t1, t2) =>
                    {
                        var r1 = t1.Department.Name.CompareTo(t2.Department.Name);
                        if (r1 == 0)
                        {
                            var r2 = t1.TS_Year.CompareTo(t2.TS_Year);
                            return r2 == 0 ? t1.TS_Month.CompareTo(t2.TS_Month) : r2;
                        }
                        else
                            return r1;
                    });
                    break;
                case AppState.EditTimeSheet:
                    specialDays = SpecialDay.GetAllForYear(currentTimeSheet._GetDate.Year);                    
                    UpdateColumns(currentTimeSheet);
                    Calendars = Calendar_Content.FindAll<Calendar_Content>("cyear = {0}", currentTimeSheet._GetDate.Year);
                    tbCurrentDepartment.Text = currentTimeSheet.Department.Name;
                    tbCurrentDepartmentManager.Text = currentTimeSheet.Department.DepartmentManager.Name;
                    lbCurrentTimeSheetName.Text = currentTimeSheet.Department.Name + " - " + currentTimeSheet._GetDate.ToString("MMMM yyyy", CultureInfo.CurrentCulture);
                    DrawTimeSheetContent();                    
                    pTimeSheetEditor.Show();
                    break;
            }
            Helper.Log("Смена состояния: {0}", newstate);
        }

        private void DrawTimeSheetContent()
        {
            dgTimeSheet.Rows.Clear();
            currentTimeSheet.HM<TimeSheet_Content>("Content", true).ForEach(row =>
            {
                /*var rows = row.AddToTable(timeSheetTable);
                foreach (var r in rows)
                    timeSheetTable.Rows.Add(r);*/
                //для AddToTable
                var temp = row.Render(dgTimeSheet);
                if (temp != null)
                    dgTimeSheet.Rows.AddRange(temp);
            }
                        );
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DB.Connection.State == ConnectionState.Open)
                DB.Connection.Close();            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LPUlist = LPU.All<LPU>();            
            cbLPUList.Items.Clear();            
            foreach (LPU lpu in LPUlist)            
                cbLPUList.Items.Add(lpu);
            Posts = Post.All<Post>();
            Flags = Flag.All<Flag>();            
            //changeState(AppState.LPUselect);  //release
            //DEBUG
            currentLPU = LPU.Get<LPU>(1);
            currentUser = User.Get<User>(22);
            currentTimeSheet = TimeSheetInstance.Get<TimeSheetInstance>(18);
            changeState(AppState.Workspace);//для сортировки списка табелей
            changeState(AppState.EditTimeSheet);            
        }

        private void btnLPUChoiceEnter_Click(object sender, EventArgs e)
        {
            if (cbLPUList.SelectedIndex != -1)
            {
                currentLPU = (LPU)cbLPUList.SelectedItem;
                changeState(AppState.Auth);
            }
        }

        private void btnLoginEnter_Click(object sender, EventArgs e)
        {
            User user = User.Find<User>("login = '{0}' and pass = '{1}' and lpu_id = '{2}'", tbAuthLogin.Text, Helper.getMD5(tbAuthLogin.Text), currentLPU.ID);            
            if (user == null)
                MessageBox.Show("Неверное имя пользователя и/или пароль", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Helper.SetAndSave("lastLogin", tbAuthLogin.Text);
                currentUser = user;                
                changeState(AppState.Workspace);
            }
        }

        private void btnLPUSelect_Click(object sender, EventArgs e)
        {
            changeState(AppState.LPUselect);
        }

        private void btnTimeSheetList_Click(object sender, EventArgs e)
        {
            TimeSheets tsForm = new TimeSheets(this);
            var temp = tsForm.ShowDialog();
            if (temp == System.Windows.Forms.DialogResult.OK)            
                changeState(AppState.EditTimeSheet);            
        }

        public void UpdateColumns(TimeSheetInstance ts)
        {
            var daysCount = ts._DaysInMonth;
            dgTimeSheet.Columns.Clear();
            var fio = dgTimeSheet.Columns.Add("cFIO", "ФИО");
            dgTimeSheet.Columns[fio].Frozen = true;
            dgTimeSheet.Columns[fio].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgTimeSheet.Columns[dgTimeSheet.Columns.Add("cTimeSheetNumber", "Т/н")].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
            if (e.KeyCode == Keys.Enter && dgTimeSheet.SelectedCells.Count > 0 && !Saving)
            {
                         
            }            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            currentUser = null;
            changeState(AppState.Auth);
        }

        private void btnAdminPanel_Click(object sender, EventArgs e)
        {
            AdminPanelForm admin = new AdminPanelForm(this);
            admin.Show();
        }
        
        private void dgTimeSheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
                return;            
            var cell = dgTimeSheet[e.ColumnIndex, e.RowIndex];
            if (cell.OwningColumn.Tag!=null && cell.OwningColumn.Tag.GetType() == typeof(DateTime))
            {
                if (currentTimeSheet.Content.Count == 0)
                    return;                
                var content = GetContentForDayByCell(cell);                
                FlagsForm ff = new FlagsForm(this, cell.Value as TimeSheet_Day);
                if (ff.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (cell.Value == null)
                    {
                        var newCell = new TimeSheet_Day(content, Convert.ToDateTime(cell.OwningColumn.Tag), ff.worked_time, ff.flag);
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
                //if open cell editor window (preDay: cell.value)
                //      saveNewDayItem
            }
        }
        TimeSheet_Content GetContentForDayByCell(DataGridViewCell cell)
        {
            var contentRow = cell.OwningRow.Index;
            while (contentRow >= 0 && dgTimeSheet[0, contentRow].Value == null)
                contentRow--;
            if (contentRow < 0)
                throw new Exception("Что-то пошло не так. Не могу найти к кому отношусь :(\r\nС Уважением, Ячейка Нажатая");
            var content = dgTimeSheet[0, contentRow].Value as TimeSheet_Content;
            return content;
        }
        private void miAddMore_Click(object sender, EventArgs e)
        {
            if (dgTimeSheet.SelectedCells.Count == 1 &&  dgTimeSheet.SelectedCells[0].OwningColumn.Tag!=null && dgTimeSheet.SelectedCells[0].OwningColumn.Tag.GetType() == typeof(DateTime))
            {
                var cell = dgTimeSheet.SelectedCells[0];
                var content = GetContentForDayByCell(cell);
                FlagsForm ff = new FlagsForm(this, cell.Value as TimeSheet_Day);
                if (ff.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {                    
                    var newCell = new TimeSheet_Day(content, Convert.ToDateTime(cell.OwningColumn.Tag), ff.worked_time, ff.flag);
                    newCell.Save();
                    DrawTimeSheetContent();    
                }
            }
        }

        private void miEditPersonal_Click(object sender, EventArgs e)
        {
            if (dgTimeSheet.SelectedCells.Count == 1)
            {
                var cell = dgTimeSheet.SelectedCells[0];
                var content = GetContentForDayByCell(cell);
                var roweditor = new RowEditForm(this, content);
                if (roweditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    roweditor.TSContent.Save();
                    DrawTimeSheetContent();
                }
            }
        }

        private void btnNewRow_Click(object sender, EventArgs e)
        {
            var roweditor = new RowEditForm(this);
            if (roweditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                roweditor.TSContent.Save();
                DrawTimeSheetContent();
            }
        }
    }
    public enum AppState
    {
        LPUselect,
        Auth,
        Workspace,
        EditTimeSheet
    }
}
