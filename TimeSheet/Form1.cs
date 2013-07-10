using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace TimeSheet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            User.Initialize<User>();
            Personal.Initialize<Personal>();
            LPU.Initialize<LPU>();
            Department.Initialize<Department>();
            UserDepartment.Initialize<UserDepartment>();
            Calendar.Initialize<Calendar>();
            Calendar_Content.Initialize<Calendar_Content>();
        }        
        private void button1_Click(object sender, EventArgs e)
        {
            //User usr = User.Get(1);
            //MessageBox.Show(usr.name);
            //User.Create("Administrator", "admin", "admin");
            //MessageBox.Show(User.Count.ToString());
            //User.Initialize("users", "id", "name", "login", "pass");            
            /*Domain test = new Domain();
            test["id"] = 1;
            test["login"] = "a1";
            test["pass"] = "a2";
            test["name"] = "a3";
            User usr = test.ParseTo<User>();            */
            //List<User> ls2 = User.FindAll<User>("NAME = 'Administrator' or login = '{0}'","admin");                        
            //MessageBox.Show(User.Count<User>().ToString());            
            //User u = User.Get<User>(22);
            //User u = User.Get<User>(22);
            //object t = u["personal"];
            //var c = Personal.Get<Personal>(1).H1<User>(""            
            /*User u = User.Get<User>(22);
            u.PersonalList.Remove(u.PersonalList.Last());
            u.Save();*/


            //User u = User.Get<User>(22);
            //Personal[] p = new Personal[] { Personal.Get<Personal>(1), Personal.Get<Personal>(2), Personal.Get<Personal>(3) };
            
            //for (int i = 0; i < p.Length; i++)
                //u.PersonalList.Add(p[i]);
            //u.Save();
            //User.Get<User>(22).PersonalList.ForEach(p => MessageBox.Show(p.User.ToString()));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DB.Connection.State == ConnectionState.Open)
                DB.Connection.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //User u = User.Get<User>(22);
            User u = new User("temp", "temp");            
            Department d = Department.Get<Department>(1);
            var ud = new UserDepartment(u, d);
            ud.Save();
        }
    }
    public enum AppState
    {
        LPU,
        Auth
    }
    #region DB Classes

    public class User : Domain
    {
        new public static string tableName = "USERS";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() { 
            {"PersonalList", new Link("TIMESHEET_MANAGER",typeof(Personal))},
            {"UserDepartments", new Link("User_ID", typeof(UserDepartment))}
        };
        new public static Dictionary<string, Link> has_one = new Dictionary<string, Link>() {
            { "PersonalProfile", new Link("PERSONAL_ID", typeof(Personal)) },
            { "LPU", new Link("LPU_ID", typeof(LPU)) }
        };
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
        public DBList<Personal> PersonalLink
        {
            get
            {
                return this.HM<Personal>("personal");
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
        public User(string login, string password)
            : base(typeof(User))
        {
            Login = login;
            Pass = Helper.getMD5(password);
        }
    }
    public class UserDepartment : Domain
    {
        new public static string tableName = "USERS_DEPARTMENT";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>();
        new public static Dictionary<string, Link> has_one = new Dictionary<string, Link>() {
            { "User", new Link("USER_ID", typeof(User)) },
            { "Department", new Link("DEPARTMENT_ID", typeof(Department)) }
        };
        #region Properties
        public User User
        {
            get
            {
                return H1<User>("User");
            }
            set
            {
                Fields["User"] = value;
            }
        }
        public Department Department
        {
            get
            {
                return H1<Department>("Department");
            }
            set
            {
                Fields["Department"] = value;
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
    public class Personal : Domain
    {
        new public static string tableName = "PERSONAL";
        new public static List<string> FieldNames = new List<string>();
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>();
        new public static Dictionary<string, Link> has_one = new Dictionary<string, Link>() {
            {"LPU", new Link("LPU_ID",typeof(LPU)) },
            {"DEPARTMENT", new Link("DEPARTMENT_ID",typeof(Department))},
            {"TimeSheetManager", new Link("TIMESHEET_MANAGER",typeof(User))}
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
        public int Table_Number
        {
            get
            {
                return (int)this["tablenumber"];
            }
            set
            {
                this["tablenumber"] = value;
            }
        }
        public string Pos
        {
            get
            {
                return this["pos"].ToString();
            }
            set
            {
                this["pos"] = value;
            }
        }
        public Department Department
        {
            get
            {
                return H1<Department>("DEPARTMENT");
            }
            set
            {
                Fields["DEPARTMENT"] = value;
            }
        }
        public User TimeSheetManager
        {
            get
            {
                return H1<User>("TimeSheetManager");
            }
            set
            {
                Fields["TimeSheetManager"] = value;
            }
        }
        public LPU LPU
        {
            get
            {
                return H1<LPU>("LPU");
            }
            set
            {
                Fields["LPU"] = value;
            }
        }
        #endregion
        public Personal()
            : base(typeof(Personal))
        {
        }
        public Personal(string name, string position)
            : base(typeof(Personal))
        {
            Name = name;
            Pos = position;            
        }
    }
    public class LPU : Domain
    {
        new public static string tableName = "LPU";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>();
        new public static Dictionary<string, Link> has_one = new Dictionary<string, Link>();
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
    public class Calendar : Domain
    {
        new public static string tableName = "LPU";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() {
            {"Content", new Link("CALENDAR_ID",typeof(Calendar_Content))}
        };
        new public static Dictionary<string, Link> has_one = new Dictionary<string, Link>();
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
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>();
        new public static Dictionary<string, Link> has_one = new Dictionary<string, Link>();
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
    public class Department : Domain
    {
        new public static string tableName = "DEPARTMENT";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>()
        {
            {"DepartmentUsers", new Link("Department_ID", typeof(UserDepartment))}
        };
        new public static Dictionary<string, Link> has_one = new Dictionary<string, Link>() {
            {"DepartmentManager", new Link("PERSONAL_ID", typeof(Personal)) },
            {"LPU",new Link("LPU_ID",typeof(LPU))}
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
        public DBList<UserDepartment> DepartmentUsers
        {
            get
            {
                return HM<UserDepartment>("DepartmentUsers");
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
    #endregion
}
