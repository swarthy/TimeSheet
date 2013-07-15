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

namespace TimeSheet
{
    public partial class Form1 : Form
    {
        public AppState State = AppState.LPUselect;
        public DBList<LPU> LPUlist = new DBList<LPU>();
        public DBList<Department> Departmentlist = new DBList<Department>();
        public DBList<Post> Posts = new DBList<Post>();        
        public LPU currentLPU = null;
        public User currentUser = null;
        public TimeSheetInstance currentTimeSheet = null;

        FlagsForm flagForm;        

        public bool Saving = false;

        public Form1()
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
            SpecialDay.Initialize<SpecialDay>();
            #endregion                                                            
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
                    UpdateColumns(currentTimeSheet);
                    tbCurrentDepartment.Text = currentTimeSheet.Department.Name;
                    tbCurrentDepartmentManager.Text = currentTimeSheet.Department.DepartmentManager.Name;
                    currentTimeSheet.HM<TimeSheet_Content>("Content", true).ForEach(row => dgTimeSheet.Rows.Add(row.ForTable(currentTimeSheet)));                    
                    pTimeSheetEditor.Show();
                    break;
            }
            Helper.Log("Смена состояния: {0}", newstate);
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DB.Connection.State == ConnectionState.Open)
                DB.Connection.Close();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LPUlist = LPU.All<LPU>();            
            cbLPUList.Items.Clear();            
            foreach (LPU lpu in LPUlist)            
                cbLPUList.Items.Add(lpu);
            Posts = Post.All<Post>();
            changeState(AppState.LPUselect);  //release
            //DEBUG
            /*currentLPU = LPU.Get<LPU>(1);
            currentUser = User.Get<User>(22);
            currentTimeSheet = TimeSheetInstance.Get<TimeSheetInstance>(18);
            changeState(AppState.Workspace);//для сортировки списка табелей
            changeState(AppState.EditTimeSheet);            */
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

        private void btnNewRow_Click(object sender, EventArgs e)
        {
            RowEditForm re = new RowEditForm(this);
            var res = re.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                re.TSContent.Save();
                dgTimeSheet.Rows.Add(re.TSContent.ForTable(currentTimeSheet));
            }
        }
        public void UpdateColumns(TimeSheetInstance ts)
        {
            var daysCount = ts._DaysInMonth;
            dgTimeSheet.Columns.Clear();
            dgTimeSheet.Columns[dgTimeSheet.Columns.Add("cFIO", "ФИО")].ReadOnly = true;
            dgTimeSheet.Columns[dgTimeSheet.Columns.Add("cPost", "Должность")].ReadOnly = true;
            dgTimeSheet.Columns[dgTimeSheet.Columns.Add("cRate", "Ставка")].ReadOnly = true;

            for (int i = 0; i < daysCount; i++)
            {
                var ind = dgTimeSheet.Columns.Add(string.Format("cDay{0}", i + 1), (i + 1).ToString());
                dgTimeSheet.Columns[ind].Width = 56;
                dgTimeSheet.Columns[ind].Tag = "DayCell";
                dgTimeSheet.Columns[ind].ReadOnly = true;
            }
                
            dgTimeSheet.Columns[dgTimeSheet.Columns.Add("cDaysCount1", "Дни явок")].ReadOnly = true;
            dgTimeSheet.Columns[dgTimeSheet.Columns.Add("cHoursCount1", "Часов всего")].ReadOnly = true;
            dgTimeSheet.Columns[dgTimeSheet.Columns.Add("cHoursCount2", "Часов ночных")].ReadOnly = true;
            dgTimeSheet.Columns[dgTimeSheet.Columns.Add("cHoursCount3", "Часов выходных, праздничных")].ReadOnly = true;
            dgTimeSheet.Columns[dgTimeSheet.Columns.Add("cTimeSheetNumber", "Табельный номер")].ReadOnly = true;            
        }

        private void dgTimeSheet_KeyDown(object sender, KeyEventArgs e)
        {
            flagForm = new FlagsForm(this);
            if (e.KeyCode == Keys.Enter && dgTimeSheet.SelectedCells.Count > 0 && !Saving)
            {
                bool needWindow = true;
                Saving = true;
                    for (int i = 0; i < dgTimeSheet.SelectedCells.Count; i++)
                    {                        
                        if ((dgTimeSheet.SelectedCells[i].OwningColumn.Tag as string) == "DayCell")
                        {
                            if (!SetTimeSheetContent(dgTimeSheet.SelectedCells[i].RowIndex, dgTimeSheet.SelectedCells[i].ColumnIndex, needWindow))
                                break;
                            needWindow = false;
                            Saving = true;
                        }
                    }
                    Saving = false;
                e.Handled = true;                
            }
            
        }
        public bool SetTimeSheetContent(int row, int col, bool needWindow = false)
        {
            if (dgTimeSheet.Rows[row].Cells[0].Value == null)
            {
                MessageBox.Show("Пустая строка");
                return false;//добавление данных в новую пустую строку            
            }            
            var content = dgTimeSheet.Rows[row].Cells[0].Value as TimeSheet_Content;
            var search_date = new DateTime(currentTimeSheet.TS_Year, currentTimeSheet.TS_Month, col - 2);
            var day = content.Days.FindOrCreate(new { item_date = search_date });
            if (needWindow)
            {
                flagForm = new FlagsForm(this, day);                
                var res = flagForm.ShowDialog();
                if (res != System.Windows.Forms.DialogResult.OK)                
                    return false;
            }
            day.Worked_Time = flagForm.worked_time;
            day["flag"] = flagForm.flag;
            dgTimeSheet[col, row].Value = TimeSheet_Day.ru_flag_list[day._FlagInd] + " " + day.Worked_Time.ToString("hh':'mm");
            content.UpdateSummary(dgTimeSheet.Rows[row]);
            day.Save();
            return true;
        }

        
        private void dgTimeSheet_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Подтверждение удаления", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                var content = e.Row.Cells[0].Value as TimeSheet_Content;                
                content.Delete();
            }
            else
                e.Cancel = true;
        }

        private void dgTimeSheet_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((dgTimeSheet[e.ColumnIndex, e.RowIndex].OwningColumn.Tag as string) == "DayCell" && !Saving)                            
                SetTimeSheetContent(e.RowIndex, e.ColumnIndex, true);            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            changeState(AppState.Auth);
        }
    }
    public enum AppState
    {
        LPUselect,
        Auth,
        Workspace,
        EditTimeSheet
    }
    #region DB Classes

    public class User : Domain
    {
        new public static string tableName = "USERS";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() { 
            {"PersonalList", new Link("TIMESHEET_MANAGER",typeof(Personal))},
            {"UserDepartments", new Link("User_ID", typeof(UserDepartment))},
            {"TimeSheets", new Link("USER_ID",typeof(TimeSheetInstance))}
        };        
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            { "PersonalProfile", new Link("PERSONAL_ID", typeof(Personal)) },
            { "LPU", new Link("LPU_ID", typeof(LPU)) }
        };
        public override string ToString()
        {
            return Login;
        }
        #region Properties
        public string Login
        {
            get
            {
                return this["login"].ToString();
            }
            set
            {
                this["login"] = value;
            }
        }
        public string Pass
        {
            get
            {
                return this["pass"].ToString();
            }
            set
            {
                this["pass"] = value;
            }
        }        
        public LPU LPU
        {
            get
            {
                return BT<LPU>("LPU");
            }
            set
            {
                this["LPU"] = value;
            }
        }
        public Personal Profile
        {
            get
            {
                return BT<Personal>("PersonalProfile");
            }
            set
            {
                this["PersonalProfile"] = value;
            }
        }
        public DBList<TimeSheetInstance> TimeSheets
        {
            get
            {
                return HM<TimeSheetInstance>("TimeSheets");
            }
            set
            {
                this["TimeSheets"] = value;
            }
        }
        public DBList<Personal> PersonalLink
        {
            get
            {
                return this.HM<Personal>("PersonalList");
            }
        }
        public DBList<UserDepartment> UserDepartments
        {
            get
            {
                return HM<UserDepartment>("UserDepartments");
            }
        }
        #endregion
        public User()
            : base(typeof(User))
        {
        }
        public User(LPU Lpu, Personal profile, string login, string password)
            : base(typeof(User))
        {
            LPU = Lpu;
            Profile = profile;
            Login = login;
            Pass = Helper.getMD5(password);
        }
    }
    public class UserDepartment : Domain
    {
        new public static string tableName = "USERS_DEPARTMENT";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            { "User", new Link("USER_ID", typeof(User)) },
            { "Department", new Link("DEPARTMENT_ID", typeof(Department)) }
        };
        #region Properties
        public User User
        {
            get
            {
                return BT<User>("User");
            }
            set
            {
                this["User"] = value;
            }
        }
        public Department Department
        {
            get
            {
                return BT<Department>("Department");
            }
            set
            {
                this["Department"] = value;
            }
        }        
        #endregion
        public UserDepartment()
            : base(typeof(UserDepartment))
        {
        }
        public UserDepartment(User user, Department department)
            : base(typeof(UserDepartment))
        {
            User = user;
            Department = department;
        }
    }
    public class Department : Domain
    {
        new public static string tableName = "DEPARTMENT";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>()
        {
            {"DepartmentUsers", new Link("Department_ID", typeof(UserDepartment))},
            {"PersonalOfDepartment", new Link("Department_ID",typeof(Personal))}
        };        
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            {"DepartmentManager", new Link("PERSONAL_ID", typeof(Personal)) },
            {"LPU",new Link("LPU_ID",typeof(LPU))}
        };
        public override string ToString()
        {
            return Name;
        }
        #region Properties
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }
        public int Department_Number
        {
            get
            {
                return (int)this["Department_Number"];
            }
            set
            {
                this["Department_Number"] = value;
            }
        }
        public Personal DepartmentManager
        {
            get
            {
                return BT<Personal>("DepartmentManager");
            }
            set
            {
                this["DepartmentManager"] = value;
            }
        }
        public LPU LPU
        {
            get
            {
                return BT<LPU>("LPU");
            }
            set
            {
                this["LPU"] = value;
            }
        }
        public DBList<UserDepartment> DepartmentUsers
        {
            get
            {
                return HM<UserDepartment>("DepartmentUsers");
            }
        }        
        public DBList<Personal> PersonalOfDepartment
        {
            get
            {
                return HM<Personal>("PersonalOfDepartment");
            }
        }
        #endregion
        public Department()
            : base(typeof(Department))
        {
        }
        public Department(string name, int department_number)
            : base(typeof(Department))
        {
            Name = name;
            Department_Number = department_number;
        }
    }
    public class Personal : Domain
    {
        new public static string tableName = "PERSONAL";
        new public static List<string> FieldNames = new List<string>();        
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            //{"LPU", new Link("LPU_ID",typeof(LPU)) },
            {"Post", new Link("POST_ID",typeof(Post)) },
            {"Department", new Link("DEPARTMENT_ID",typeof(Department))},
            {"TimeSheetManager", new Link("TIMESHEET_MANAGER",typeof(User))}
        };
        public override string ToString()
        {
            return Name;
        }
        #region Properties
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }
        public int Table_Number
        {
            get
            {
                return (int)this["Table_Number"];
            }
            set
            {
                this["Table_Number"] = value;
            }
        }
        public Post Post
        {
            get
            {
                return BT<Post>("Post");
            }
            set
            {
                this["Post"] = value;
            }
        }
        public Department Department
        {
            get
            {
                return BT<Department>("Department");
            }
            set
            {
                this["Department"] = value;
            }
        }
        public User TimeSheetManager
        {
            get
            {
                return BT<User>("TimeSheetManager");
            }
            set
            {
                this["TimeSheetManager"] = value;
            }
        }
        /*
        public LPU LPU
        {
            get
            {
                return BT<LPU>("LPU");
            }
            set
            {
                this["LPU"] = value;
            }
        }*/
        #endregion
        public Personal()
            : base(typeof(Personal))
        {
        }
        public Personal(Department department, string name, Post post)
            : base(typeof(Personal))
        {
            Name = name;            
            Department = department;
            Post = post;
        }
    }
    public class LPU : Domain
    {
        new public static string tableName = "LPU";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>()
        {
            {"Departments", new Link("LPU_ID", typeof(Department))}            
        };     
        public override string ToString()
        {
            return Name;
        }
        #region Properties
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }
        public DBList<Department> Departments
        {
            get
            {
                return HM<Department>("Departments");
            }
        }  
        #endregion
        public LPU()
            : base(typeof(LPU))
        {
        }
        public LPU(string name)
            : base(typeof(LPU))
        {
            Name = name;
        }
    }
    public class Post : Domain
    {
        new public static string tableName = "POSTS";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        public override string ToString()
        {
            return Name;
        }
        #region Properties
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }
        #endregion
        public Post()
            : base(typeof(Post))
        {
        }
        public Post(string name)
            : base(typeof(Post))
        {
            Name = name;
        }
    }
    public class Calendar : Domain
    {
        new public static string tableName = "LPU";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() {
            {"Content", new Link("CALENDAR_ID",typeof(Calendar_Content))}
        };        
        #region Properties
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }
        #endregion
        public Calendar()
            : base(typeof(Calendar))
        {
        }
        public Calendar(string name)
            : base(typeof(Calendar))
        {
            Name = name;
        }
    }
    public class Calendar_Content : Domain
    {
        new public static string tableName = "CALENDAR_CONTENT";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        #region Properties
        public int Month
        {
            get
            {
                return (int)this["Month"];
            }
            set
            {
                this["Month"] = value;
            }
        }
        public double Hours
        {
            get
            {
                return (int)this["hours"];
            }
            set
            {
                this["hours"] = value;
            }
        }
        public int Days
        {
            get
            {
                return (int)this["days"];
            }
            set
            {
                this["days"] = value;
            }
        }
        #endregion
        public Calendar_Content()
            : base(typeof(Calendar_Content))
        {
        }
        public Calendar_Content(int month, double hours, int days)
            : base(typeof(Calendar_Content))
        {
            Month = month;
            Hours = hours;
            Days = days;
        }
    }
    public class TimeSheetInstance : Domain
    {
        new public static string tableName = "TIMESHEET";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() {
            {"Content", new Link("TimeSheet_ID",typeof(TimeSheet_Content))}
        };        
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() { 
            {"User",new Link("User_ID",typeof(User))},            
            {"Department",new Link("DEPARTMENT_ID",typeof(Department))},
        };
        #region Properties
        public User User
        {
            get
            {
                return BT<User>("User");
            }
            set
            {
                this["User"] = value;
            }
        }        
        public Department Department
        {
            get
            {
                return BT<Department>("Department");
            }
            set
            {
                this["Department"] = value;
            }
        }        
        public int TS_Month
        {
            get
            {
                return (int)this["TS_Month"];
            }
            set
            {
                this["TS_Month"] = value;
            }
        }
        public int TS_Year
        {
            get
            {
                return (int)this["TS_Year"];
            }
            set
            {
                this["TS_Year"] = value;
            }
        }
        public DateTime _GetDate
        {
            get
            {
                return new DateTime(TS_Year, TS_Month, 1);
            }
        }
        public DBList<TimeSheet_Content> Content
        {
            get
            {
                return HM<TimeSheet_Content>("Content");
            }
        }
        public int _DaysInMonth
        {
            get
            {
                return DateTime.DaysInMonth(TS_Year, TS_Month);
            }
        }
        #endregion
        public TimeSheetInstance()
            : base(typeof(TimeSheetInstance))
        {
        }
        public TimeSheetInstance(User user, Department department, int year, int month)
            : base(typeof(TimeSheetInstance))
        {
            User = user;
            Department = department;
            TS_Year = year;
            TS_Month = month;
        }
    }
    public class TimeSheet_Content : Domain
    {        
        new public static string tableName = "TIMESHEET_CONTENT";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>()
        {
            {"Days", new Link("TimeSheet_Content_ID", typeof(TimeSheet_Day))}            
        };         
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>(){
            {"Personal", new Link("Personal_ID",typeof(Personal))},
            {"TimeSheet", new Link("TIMESHEET_ID",typeof(TimeSheetInstance))},
            {"Post", new Link("POST_ID",typeof(Post))}
        };
        public override string ToString()
        {
            return Personal.ToString();
        }      
        public object[] ForTable(TimeSheetInstance ts)
        {
            List<object> result = new List<object>()
            {
                this,
                Post,
                Rate
            };            
            for (int i = 0; i < ts._DaysInMonth; i++)                            
                result.Add(Days.Find(c => c.Item_Date.Day == i + 1));
            //явок
            result.Add(Days.Where(d => d.Flag == "ya").Count());
            //часов всего
            TimeSpan summAll = new TimeSpan();
            Days.ForEach(d => summAll += d.Worked_Time);
            result.Add(Helper.FormatSpan(summAll));
            //часов ночных
            TimeSpan summNight = new TimeSpan();
            Days.Where(d => d.Flag == "n").ToList().ForEach(d => summNight += d.Worked_Time);
            result.Add(Helper.FormatSpan(summNight));
            //часов праздничных
            TimeSpan summWeekEndHolyday = new TimeSpan();
            Days.Where(d => d.Flag == "v" || d.Flag == "rp").ToList().ForEach(d => summWeekEndHolyday += d.Worked_Time);
            result.Add(Helper.FormatSpan(summWeekEndHolyday));
            //табельный номер
            result.Add(Personal.Table_Number);
            return result.ToArray();
        }
        public void UpdateSummary(DataGridViewRow row)
        {
            var col = row.Cells.Count-5;
            var result = new List<object>();
            //явок
            row.Cells[col++].Value = Days.Where(d => d.Flag == "ya").Count();
            //часов всего
            TimeSpan summAll = new TimeSpan();
            Days.ForEach(d => summAll += d.Worked_Time);
            row.Cells[col++].Value=Helper.FormatSpan(summAll);
            //часов ночных
            TimeSpan summNight = new TimeSpan();
            Days.Where(d => d.Flag == "n").ToList().ForEach(d => summNight += d.Worked_Time);
            row.Cells[col++].Value=Helper.FormatSpan(summNight);
            //часов праздничных
            TimeSpan summWeekEndHolyday = new TimeSpan();
            Days.Where(d => d.Flag == "v" || d.Flag == "rp").ToList().ForEach(d => summWeekEndHolyday += d.Worked_Time);
            row.Cells[col++].Value=Helper.FormatSpan(summWeekEndHolyday);
        }
        #region Properties
        public DBList<TimeSheet_Day> Days
        {
            get
            {
                return HM<TimeSheet_Day>("Days");
            }
        }
        public TimeSheetInstance TimeSheet
        {
            get
            {
                return BT<TimeSheetInstance>("TimeSheet");
            }
            set
            {
                this["TimeSheet"] = value;
            }
        }
        public Post Post
        {
            get
            {
                return BT<Post>("Post");
            }
            set
            {
                this["Post"] = value;
            }
        }
        public Personal Personal
        {
            get
            {
                return BT<Personal>("Personal");
            }
            set
            {
                this["Personal"] = value;
            }
        }                
        public double Rate
        {
            get
            {
                return this["Rate"] == null ? 0 : (double)this["Rate"];
            }
            set
            {
                this["Rate"] = value;
            }
        }        
        #endregion
        public TimeSheet_Content()
            : base(typeof(TimeSheet_Content))
        {            
        }
        public TimeSheet_Content(Personal personal, TimeSheetInstance timeSheet, Post post, double rate = 1)
            : base(typeof(TimeSheet_Content))
        {
            Personal = personal;
            TimeSheet = timeSheet;
            Post = post;            
            Rate = rate;            
        }
    }
    public class TimeSheet_Day : Domain
    {
        public static List<string> flag_list = new List<string>() {
                "v", "n", "g", "o", "b", "r", "s", "p", "k", "a", "vu", "ou", "zn", "zp", "zs", "rp", "f", "ya"
            };
        public static List<string> ru_flag_list = new List<string>() {
                "В", "Н", "Г", "О", "Б", "Р", "С", "П", "К", "А", "ВУ", "ОУ", "ЗН", "ЗП", "ЗС", "РП", "Ф", "Я"
            };
        new public static string tableName = "TIMESHEET_DAYS";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>(){
            {"TimeSheetContent", new Link("TimeSheet_Content_ID",typeof(TimeSheet_Content))}            
        };
        public override string ToString()
        {
            return ru_flag_list[_FlagInd] + " " + Worked_Time.ToString("hh':'mm");
        }
        #region Properties
        public TimeSheet_Content TimeSheetContent
        {
            get
            {
                return BT<TimeSheet_Content>("TimeSheetContent");
            }
            set
            {
                this["TimeSheetContent"] = value;
            }
        }
        public DateTime Item_Date
        {
            get
            {
                return (DateTime)this["Item_Date"];
            }
            set
            {
                this["Item_Date"] = value;
            }
        }
        public TimeSpan Worked_Time
        {
            get
            {
                return TimeSpan.FromHours(this["Worked_Time"]==null?0:(double)this["Worked_Time"]);
            }
            set
            {
                this["Worked_Time"] = value.TotalHours;
            }
        }
        #region Флаги
        public int _FlagInd
        {
            get
            {
                return flag_list.IndexOf(Flag);
            }
            set
            {
                Flag = flag_list[value];
            }
        }
        public string Flag
        {
            get
            {
                return (this["flag"]??"").ToString();
            }
            set
            {
                this["flag"] = value.ToLower();
            }
        }
        /*
        public int _CheckedFlag
        {
            get
            {
                return flag_list.FindIndex(f => getFlag(f));
            }
            set
            {
                this[flag_list[value]] = (short)1;
            }
        }
        
        public bool getFlag(string key)
        {
            return ((short)this[key]) == 1;
        }
        
        public bool V { get { return getFlag("v"); } set { this["v"] = value; } }
        public bool N { get { return getFlag("n"); } set { this["n"] = value; } }
        public bool G { get { return getFlag("g"); } set { this["g"] = value; } }
        public bool O { get { return getFlag("o"); } set { this["o"] = value; } }
        public bool B { get { return getFlag("b"); } set { this["b"] = value; } }
        public bool R { get { return getFlag("r"); } set { this["r"] = value; } }
        public bool S { get { return getFlag("s"); } set { this["s"] = value; } }
        public bool P { get { return getFlag("p"); } set { this["p"] = value; } }
        public bool K { get { return getFlag("k"); } set { this["k"] = value; } }
        public bool A { get { return getFlag("a"); } set { this["a"] = value; } }
        public bool VU { get { return getFlag("vu"); } set { this["vu"] = value; } }
        public bool OU { get { return getFlag("ou"); } set { this["ou"] = value; } }
        public bool ZN { get { return getFlag("zn"); } set { this["zn"] = value; } }
        public bool ZP { get { return getFlag("zp"); } set { this["zp"] = value; } }
        public bool ZS { get { return getFlag("zs"); } set { this["zs"] = value; } }
        public bool RP { get { return getFlag("rp"); } set { this["rp"] = value; } }
        public bool F { get { return getFlag("f"); } set { this["f"] = value; } }
        public bool YA { get { return getFlag("ya"); } set { this["ya"] = value; } }
         * */
        #endregion
        #endregion
        public TimeSheet_Day()
            : base(typeof(TimeSheet_Day))
        {            
        }
        public TimeSheet_Day(TimeSheet_Content content, DateTime date, TimeSpan worked_time, double rate = 1)
            : base(typeof(TimeSheet_Day))
        {
            TimeSheetContent = content;                        
            Item_Date = date;
            Worked_Time = worked_time;                        
        }
    }
    public class SpecialDay : Domain
    {
        new public static string tableName = "SPECIALDAYS";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        #region Properties
        public DateTime Spec_Date
        {
            get
            {
                return (DateTime)this["Spec_Date"];
            }
            set
            {
                this["Spec_Date"] = value;
            }
        }

        #endregion
        public SpecialDay()
            : base(typeof(SpecialDay))
        {
        }
        public SpecialDay(DateTime date)
            : base(typeof(SpecialDay))
        {
            Spec_Date = date;
        }
    }

    #endregion
}
