using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using SwarthyComponents.FireBird;

namespace TimeSheetManger
{
    //Все классы должны быть инициализированны при создании формы (см MainForm.MainForm())
    public class User : Domain
    {
        new public static string tableName = "USERS";
        new public static bool virtualDeletion = true;
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() { 
            {"PersonalList", new Link("TIMESHEET_MANAGER",typeof(Personal))},            
            {"TimeSheets", new Link("USER_ID",typeof(TimeSheetInstance))}
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            { "PersonalProfile", new Link("PERSONAL_TN", typeof(Personal), "Table_Number") },
            { "LPU", new Link("LPU_ID", typeof(LPU)) }
        };
        public User _Self
        {
            get
            {
                return this;
            }
        }
        public override string ToString()
        {
            return Login;
        }
        public string _LoginAndProfile
        {
            get
            {
                return string.Format("{0} ({1})", Login, Profile._ShortNameAndNumber);
            }
        }
        #region Properties
        public string Login
        {
            get
            {
                return this["login"] == null ? "" : this["login"].ToString();
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
                return this["pass"] == null ? "" : this["pass"].ToString();
            }
            set
            {
                this["pass"] = value;
            }
        }
        public bool _IS_MODERATOR
        {
            get
            {
                return Role > 0;
            }            
        }
        public bool _IS_ADMIN
        {
            get
            {
                return Role > 1;
            }
        }
        public int Role
        {
            get
            {
                return Convert.ToInt32(this["ROLE"]);
            }
            set
            {
                this["ROLE"] = value;
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
        /*public DBList<UserDepartment> UserDepartments
        {
            get
            {
                return HM<UserDepartment>("UserDepartments");
            }
        }*/
        #endregion
        public User()
            : base(typeof(User))
        {
        }
        public User(LPU Lpu, Personal profile, int role, string login, string password)
            : base(typeof(User))
        {
            LPU = Lpu;
            Role = role;
            Profile = profile;
            Login = login;
            //Pass = Helper.getMD5(password);
            Pass = password;
        }
    }    
    public class Department : Domain
    {
        new public static string tableName = "DEPARTMENT";
        new public static bool virtualDeletion = true;
        new public static string OrderBy = "DEPARTMENT_MANAGER_TN";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>()
        {            
            {"PersonalOfDepartment", new Link("Department_NUMBER",typeof(Personal),"DEPARTMENT_NUMBER")}
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            {"DepartmentManager", new Link("DEPARTMENT_MANAGER_TN", typeof(Personal), "Table_Number") },
            {"LPU",new Link("LPU_ID",typeof(LPU))}
        };
        public Department _Self
        {
            get
            {
                return this;
            }
        }
        public override string ToString()
        {
            return Name;
        }
        #region Properties
        public string Name
        {
            get
            {
                return this["name"] == null ? "" : this["name"].ToString();
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
                return this["Department_Number"] == null ? 0 : (int)this["Department_Number"];
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
        public Department(string name, int department_number, LPU lpu)
            : base(typeof(Department))
        {
            Name = name;
            LPU = lpu;
            Department_Number = department_number;
        }
    }
    public class Personal : Domain
    {
        new public static string tableName = "PERSONAL";
        new public static string OrderBy = "LastName";
        new public static List<string> FieldNames = new List<string>();
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {            
            {"Post", new Link("POST_ID",typeof(Post)) },
            {"Department", new Link("DEPARTMENT_NUMBER",typeof(Department),"DEPARTMENT_NUMBER")},
            {"TimeSheetManager", new Link("TIMESHEET_MANAGER",typeof(User))}
        };
        public Personal _Self
        {
            get
            {
                return this;
            }
        }
        public override string ToString()
        {
            return _ShortName;
        }
        #region Properties
        public string _ShortName
        {
            get
            {
                if (FirstName.Length == 0 || MiddleName.Length == 0)
                    return LastName;
                return string.Format("{0} {1}.{2}.", LastName, FirstName[0], MiddleName[0]);
            }
        }
        public string _ShortNameAndNumber
        {
            get
            {
                if (FirstName.Length == 0 || MiddleName.Length == 0)
                    return LastName;
                return string.Format("{0} {1}.{2}. ({3})", LastName, FirstName[0], MiddleName[0], Table_Number);
            }
        }

        public string _FullName
        {
            get
            {
                return string.Format("{0} {1} {2}",LastName, FirstName, MiddleName);
            }
        }
        public string FirstName
        {
            get
            {
                return this["FirstName"] == null ? "" : this["FirstName"].ToString();
            }
            set
            {
                this["FirstName"] = value;
            }
        }
        public string MiddleName
        {
            get
            {
                return this["MiddleName"] == null ? "" : this["MiddleName"].ToString();
            }
            set
            {
                this["MiddleName"] = value;
            }
        }
        public string LastName
        {
            get
            {
                return this["LastName"] == null ? "" : this["LastName"].ToString();
            }
            set
            {
                this["LastName"] = value;
            }
        }
        public int Table_Number
        {
            get
            {
                return this["Table_Number"] == null ? 0 : (int)this["Table_Number"];
            }
            set
            {
                this["Table_Number"] = value;
            }
        }
        public int Priority
        {
            get
            {
                return this["Priority"] == null ? 0 : (int)this["Priority"];
            }
            set
            {
                this["Priority"] = value;
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
        #endregion
        public Personal()
            : base(typeof(Personal))
        {
        }
        public Personal(Department department, string lastName, string firstName, string middleName, Post post)
            : base(typeof(Personal))
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Department = department;
            Post = post;
        }
    }
    public class LPU : Domain
    {
        new public static string tableName = "LPU";
        new public static bool virtualDeletion = true;
        new public static bool virtualSubDeletion = true;
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>()
        {
            {"Departments", new Link("LPU_ID", typeof(Department))},
            {"Users", new Link("LPU_ID", typeof(User))}
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            {"MainDoc", new Link("MAINDOC_TN",typeof(Personal),"Table_Number")}
        };
        public LPU _Self
        {
            get
            {
                return this;
            }
        }
        public override string ToString()
        {
            return Name;
        }
        #region Properties
        public string Name
        {
            get
            {
                return this["name"] == null ? "" : this["name"].ToString();
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
        public DBList<User> Users
        {
            get
            {
                return HM<User>("Users");
            }
        }
        public Personal MainDoc
        {
            get
            {
                return BT<Personal>("MainDoc");
            }
            set
            {
                this["MainDoc"] = value;
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
    public class Flag : Domain
    {
        new public static string tableName = "FLAGS";
        new public static string OrderBy = "ru_name";
        new public static bool virtualDeletion = true;
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                        
        public override string ToString()
        {
            return Ru_Name;
        }
        public Flag _Self
        {
            get
            {
                return this;
            }
        }
        #region Properties
        public string Name
        {
            get
            {
                return this["name"] == null ? "" : this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }
        public string Ru_Name
        {
            get
            {
                return this["Ru_Name"] == null ? "" : this["Ru_Name"].ToString();
            }
            set
            {
                this["Ru_Name"] = value;
            }
        }
        public string Description
        {
            get
            {
                return this["Description"] == null ? "" : this["Description"].ToString();
            }
            set
            {
                this["Description"] = value;
            }
        }
        #endregion
        public Flag()
            : base(typeof(Flag))
        {
        }
        public Flag(string name, string ru_name)
            : base(typeof(Flag))
        {
            Name = name;
            Ru_Name = ru_name;
        }
    }
    public class Post : Domain
    {
        new public static string tableName = "POSTS";
        new public static string OrderBy = "Name";
        new public static bool virtualDeletion = true;
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        public Post _Self
        {
            get
            {
                return this;
            }
        }
        public override string ToString()
        {
            return Name;
        }
        #region Properties
        public string Name
        {
            get
            {
                return this["name"] == null ? "" : this["name"].ToString();
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
        new public static string tableName = "CALENDAR";
        new public static string OrderBy = "CYear";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() {
            {"Content", new Link("CALENDAR_ID",typeof(Calendar_Content))}
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            {"Name", new Link("NAME_ID",typeof(Calendar_Name))}
        };
        public Calendar _Self
        {
            get
            {
                return this;
            }
        }
        public override string ToString()
        {
            return Name.Name;
        }
        public void Generate12Months()
        {
            for (int i = 1; i <= 12; i++)            
                Months.Add(new Calendar_Content(this, i, 0, 0));            
        }
        #region Properties
        public DBList<Calendar_Content> Months
        {
            get
            {
                return HM<Calendar_Content>("Content");
            }
        }
        public Calendar_Name Name
        {
            get
            {
                return BT<Calendar_Name>("Name");
            }
            set
            {
                this["Name"] = value;
            }
        }
        public int CYear
        {
            get
            {
                return this["CYear"] == null ? 0 : (int)this["CYear"];
            }
            set
            {
                this["CYear"] = value;
            }
        }        

        #endregion
        public Calendar()
            : base(typeof(Calendar))
        {
        }
        public Calendar(Calendar_Name name, int year)
            : base(typeof(Calendar))
        {
            Name = name;
            CYear = year;            
        }
    }
    public class Calendar_Content : Domain
    {
        new public static string tableName = "CALENDAR_CONTENT";
        new public static string OrderBy = "CMonth";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {            
            {"Calendar", new Link("CALENDAR_ID",typeof(Calendar)) }
        };
        public Calendar_Content _Self
        {
            get
            {
                return this;
            }
        }
        public override string ToString()
        {
            return string.Format(new DateTime(Calendar.CYear, CMonth, 1).ToString("MMMM yyyy"));            
        }
        #region Properties
        public Calendar Calendar
        {
            get
            {
                return BT<Calendar>("Calendar");
            }
            set
            {
                this["Calendar"] = value;
            }
        }
        public int CMonth
        {
            get
            {
                return this["CMonth"] == null ? 0 : (int)this["CMonth"];
            }
            set
            {
                this["CMonth"] = value;
            }
        }        
        public double Hours
        {
            get
            {
                return this["hours"] == null ? 0 : (double)this["hours"];
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
                return this["days"] == null ? 0 : (int)this["days"];
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
        public Calendar_Content(Calendar calendar, int month, double hours, int days)
            : base(typeof(Calendar_Content))
        {
            Calendar = calendar;
            CMonth = month;
            Hours = hours;
            Days = days;
        }
    }
    public class Calendar_Name : Domain
    {
        new public static string tableName = "CALENDAR_NAMES";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() {
            {"Calendars", new Link("NAME_ID",typeof(Calendar))}
        };
        public Calendar_Name _Self
        {
            get
            {
                return this;
            }
        }
        public override string ToString()
        {
            return Name;
        }
        #region Properties
        public DBList<Calendar> Calendars
        {
            get
            {
                return HM<Calendar>("Calendars");
            }
        }
        public string Name
        {
            get
            {
                return this["name"] == null ? "" : this["name"].ToString();
            }
            set
            {
                this["name"] = value;
            }
        }  
        #endregion
        public Calendar_Name()
            : base(typeof(Calendar_Name))
        {
        }
        public Calendar_Name(string name)
            : base(typeof(Calendar_Name))
        {
            Name = name;
        }
    }
    public class TimeSheetInstance : Domain
    {
        new public static string tableName = "TIMESHEET";
        new public static string OrderBy = "TS_YEAR, TS_MONTH, DEPARTMENT_NUMBER";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() {
            {"Content", new Link("TimeSheet_ID",typeof(TimeSheet_Content))}
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() { 
            {"User",new Link("User_ID",typeof(User))},            
            {"Department",new Link("DEPARTMENT_NUMBER",typeof(Department), "DEPARTMENT_NUMBER")},
        };
        public override string ToString()
        {
            return string.Format("{0} - {1}", Department.Name, _GetDate.ToString("MMMM yyyy"));
        }
        public TimeSheetInstance _Self
        {
            get
            {
                return this;
            }
        }
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
                return this["TS_Month"] == null ? 0 : (int)this["TS_Month"];
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
                return this["TS_Year"] == null ? 0 : (int)this["TS_Year"];
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
        new public static string OrderBy = "Personal_TN";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>()
        {
            {"Days", new Link("TimeSheet_Content_ID", typeof(TimeSheet_Day))}            
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>(){
            {"Personal", new Link("Personal_TN",typeof(Personal), "TABLE_NUMBER")},
            {"Calendar", new Link("CALENDAR_ID",typeof(Calendar))},
            {"Post", new Link("POST_ID",typeof(Post))},
            {"TimeSheetManger", new Link("TIMESHEET_ID",typeof(TimeSheetInstance))}            
        };
        public TimeSheet_Content _Self
        {
            get
            {
                return this;
            }
        }
        public override string ToString()
        {
            return Personal.ToString();
        }
        #region Properties
        public DBList<TimeSheet_Day> Days
        {
            get
            {
                return HM<TimeSheet_Day>("Days");
            }
        }
        public TimeSheetInstance TimeSheetManger
        {
            get
            {
                return BT<TimeSheetInstance>("TimeSheetManger");
            }
            set
            {
                this["TimeSheetManger"] = value;
            }
        }
        public Calendar Calendar
        {
            get
            {
                return BT<Calendar>("Calendar");
            }
            set
            {
                this["Calendar"] = value;
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
                return this["Rate"] == null ? 1 : (double)this["Rate"];
            }
            set
            {
                this["Rate"] = value;
            }
        }
        #endregion

        public DataGridViewRow[] Render(MyDataGridView dg)
        {            
            DataGridViewRow[] result;
            if (Days.Count == 0)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dg);
                row.SetValues(this, Personal.Table_Number, Post, Rate);
                return new DataGridViewRow[] { row };
            }
            var groups = Days.GroupBy(d => d.Item_Date);
            var needRows = groups.Max(a => a.Count());
            result = new DataGridViewRow[needRows];
            var values = new object[needRows][];
            var colCount = 4 + TimeSheetManger._DaysInMonth + 4;
            for (int i = 0; i < needRows; i++)
            {
                result[i] = new DataGridViewRow();
                result[i].CreateCells(dg);
                values[i] = new object[colCount];
            }
            values[0][0] = this;
            values[0][1] = Personal.Table_Number;
            values[0][2] = Post;
            values[0][3] = Rate;
            foreach (IGrouping<DateTime, TimeSheet_Day> group in groups)
            {
                int i = 0;
                foreach (var item in group)
                {
                    switch (item.Flag.Name)
                    {
                        case "ya":                            
                            values[i][colCount - 4] = Convert.ToInt32(values[i][colCount - 4]) + 1;//дни явок
                            values[i][colCount - 3] = Convert.ToDouble(values[i][colCount - 3]) + item.Worked_Time.TotalHours;//часы - всего
                            var sdate = MainForm.specialDays.Find(sd => sd.Spec_Date == item.Item_Date);
                            if (sdate!=null && (sdate.State == 1 || sdate.State == 2))
                                values[i][colCount - 1] = Convert.ToDouble(values[i][colCount - 1]) + item.Worked_Time.TotalHours;//часы - выходные
                            break;
                        case "n":
                            values[i][colCount - 2] = Convert.ToDouble(values[i][colCount - 2]) + item.Worked_Time.TotalHours;//часы - ночные
                            values[i][colCount - 3] = Convert.ToDouble(values[i][colCount - 3]) + item.Worked_Time.TotalHours;//часы - всего
                            break;
                        case "v":
                            if (item.Worked_Time.TotalHours > 0)
                            {
                                values[i][colCount - 1] = Convert.ToDouble(values[i][colCount - 1]) + item.Worked_Time.TotalHours;//часы - выходные
                                values[i][colCount - 3] = Convert.ToDouble(values[i][colCount - 3]) + item.Worked_Time.TotalHours;//часы - всего
                            }
                            break;
                        case "rp":
                            values[i][colCount - 4] = Convert.ToInt32(values[i][colCount - 4]) + 1;//дни явок
                            values[i][colCount - 1] = Convert.ToDouble(values[i][colCount - 1]) + item.Worked_Time.TotalHours;//часы - выходные
                            values[i][colCount - 3] = Convert.ToDouble(values[i][colCount - 3]) + item.Worked_Time.TotalHours;//часы - всего   
                            break;
                    }
                    values[i++][group.Key.Day + 3] = item;
                }
            }
            for (int i = 0; i < needRows; i++)
            {
                result[i].SetValues(values[i]);                
            }
            result[0].Cells[0].Tag = needRows;
            return result;
        }
        
        /*
         *может быть... когда нибудь... а пока что так, как выше :)
        public List<DataRow> AddToTable(DataTable table)
        {
            //table.LoadDataRow(new object[] {},LoadOption.
            //var mainrow = table.NewRow();
            List<DataRow> contentRows = new List<DataRow>();
            contentRows.Add(table.NewRow());
            contentRows[0][0] = Personal;
            contentRows[0][1] = Personal.Table_Number;
            contentRows[0][2] = Post;
            contentRows[0][3] = Rate;
            for (int i = 0; i < Days.Count; i++)
            {
                int indx = 0;
                var cPos = Days[i].Item_Date.Day;
                while (contentRows[indx][cPos+3].GetType()==typeof(TimeSheet_Day))
                {                    
                    indx++;
                    if (indx == contentRows.Count)
                        contentRows.Add(table.NewRow());
                }
                contentRows[indx][cPos] = Days[i];
            }
            return contentRows;
        }*/
        public TimeSheet_Content()
            : base(typeof(TimeSheet_Content))
        {
        }
        public TimeSheet_Content(Personal personal, TimeSheetInstance timeSheet, Post post, double rate = 1)
            : base(typeof(TimeSheet_Content))
        {
            Personal = personal;
            TimeSheetManger = timeSheet;
            Post = post;
            Rate = rate;
        }
    }
    public class TimeSheet_Day : Domain
    {
        new public static string tableName = "TIMESHEET_DAYS";
        new public static string OrderBy = "Item_Date";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>(){
            {"TimeSheetContent", new Link("TimeSheet_Content_ID",typeof(TimeSheet_Content))},
            {"Flag", new Link("Flag_ID",typeof(Flag))},
        };
        public TimeSheet_Day _Self
        {
            get
            {
                return this;
            }
        }
        public override string ToString()
        {
            return Flag + (Worked_Time.TotalHours == 0 ? "" : " " + Worked_Time.ToString("hh':'mm"));
        }
        #region Properties
        public Flag Flag
        {
            get
            {
                return BT<Flag>("Flag");
            }
            set
            {
                this["Flag"] = value;
            }
        }
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
                return this["Item_Date"] == null ? DateTime.MinValue : (DateTime)this["Item_Date"];
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
                return TimeSpan.FromHours(this["Worked_Time"] == null ? 0 : (double)this["Worked_Time"]);
            }
            set
            {
                this["Worked_Time"] = value.TotalHours;
            }
        }
        #endregion
        public TimeSheet_Day()
            : base(typeof(TimeSheet_Day))
        {
        }
        public TimeSheet_Day(TimeSheet_Content content, DateTime date, TimeSpan worked_time, Flag flag)
            : base(typeof(TimeSheet_Day))
        {
            TimeSheetContent = content;
            Item_Date = date;
            Worked_Time = worked_time;
            Flag = flag;
        }
        public TimeSheet_Day(DateTime date, TimeSpan worked_time, Flag flag)
            : base(typeof(TimeSheet_Day))
        {            
            Item_Date = date;
            Worked_Time = worked_time;
            Flag = flag;
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
                return this["Spec_Date"] == null ? DateTime.MinValue : (DateTime)this["Spec_Date"];
            }
            set
            {
                this["Spec_Date"] = value;
            }
        }
        public static SpecialDay myGet(DateTime date)
        {
            return SpecialDay.Find<SpecialDay>(new { spec_date = date });
        }
        public int State
        {
            get
            {
                return Convert.ToInt32(this["State"]);
            }
            set
            {
                this["State"] = value;
            }
        }
        #endregion
        public SpecialDay _Self
        {
            get
            {
                return this;
            }
        }
        public static DBList<SpecialDay> GetAllForYear(int year)
        {
            string start = new DateTime(year, 1, 1).ToShortDateString(), end = new DateTime(year, 12, 31).ToShortDateString();
            return SpecialDay.FindAll<SpecialDay>("spec_date >= '{0}' and spec_date <= '{1}'", start, end);
        }
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
    public class DBSettings : Domain
    {
        new public static string tableName = "SETTINGS";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        #region Properties        
        public string Setting_Key
        {
            get
            {
                return this["Setting_Key"] == null ? "" : this["Setting_Key"].ToString();
            }
            set
            {
                this["Setting_Key"] = value;
            }
        }
        public string Setting_Value
        {
            get
            {
                return this["Setting_Value"] == null ? "" : this["Setting_Value"].ToString();
            }
            set
            {
                this["Setting_Value"] = value;
            }
        }        
        #endregion
        public static string Get(string key)
        {
            var search_result = DBSettings.Find<DBSettings>(new { Setting_Key = key });
            return search_result == null ? "" : search_result.Setting_Value;
        }
        public static void Set(string key, string value)
        {
            var setting = DBSettings.Find<DBSettings>(new { Setting_Key = key });
            if (setting == null)
                setting = new DBSettings(key, value);
            else
                setting.Setting_Value = value;
            setting.Save();
        }
        public DBSettings _Self
        {
            get
            {
                return this;
            }
        }
        
        public DBSettings()
            : base(typeof(DBSettings))
        {
        }
        public DBSettings(string key, string value)
            : base(typeof(DBSettings))
        {
            Setting_Key = key;
            Setting_Value = value;
        }
    }
}
