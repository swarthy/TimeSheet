using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SwarthyComponents.FireBird;
using System.Xml.Linq;

namespace TimeSheetManager
{
    //Все классы должны быть инициализированны при создании формы (см MainForm.MainForm())
    /// <summary>
    /// Класс пользователя системы
    /// </summary>
    public class User : Domain
    {
        /// <summary>
        /// Имя таблицы
        /// </summary>
        new public static string tableName = "USERS";
        /// <summary>
        /// Режим виртуального удаления
        /// </summary>
        new public static bool virtualDeletion = true;
        /// <summary>
        /// Список полей (автоматически заполняется из на основе свойств класса, должен быть переопределен в каждом классе)
        /// </summary>
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        /// <summary>
        /// Отношения 1:M
        /// </summary>
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() {             
            //Табели
            {"TimeSheets", new Link("User_ID", typeof(TimeSheetInstance))},
            {"UserPDP", new Link("User_ID", typeof(UserPDP))}
        };
        /// <summary>
        /// Отношения 1:1 - ссылка у привязываемого объекта
        /// </summary>
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            //Ссылка на запись сотрудника
            { "PersonalProfile", new Link("Personal_TN", typeof(Personal), "Table_Number") },
            //Последний расчетчик
            { "LastRaschetchik", new Link("LastRaschetchik_TN", typeof(Personal), "Table_Number") },
            //ЛПУ
            { "LPU", new Link("LPU_ID", typeof(LPU)) }
        };
        public override string ToString()
        {
            return Login;
        }
        public override string Validation()
        {
            if (Login.Length == 0)
                return "Логин введен неверно";
            if (Pass.Length == 0)
                return "Пароль введен неверно";
            if (LPU == null)
                return "Не указано ЛПУ";
            if (Profile == null)
                return "Не указан профиль";
            if (Role < 0)
                return "Роль введена неверно";
            return "";
        }
        public string _LoginAndProfile
        {
            get
            {
                return string.Format("{0} ({1})", Login, Profile._ShortNameAndNumber);
            }
        }
        public string _RoleString
        {
            get
            {
                return Role == 2 ? "Администратор" : Role == 1 ? "Модератор" : "Табельщик";
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
        public Personal LastRaschetchik
        {
            get
            {
                return BT<Personal>("LastRaschetchik");
            }
            set
            {
                this["LastRaschetchik"] = value;
            }
        }
        public DBList<TimeSheetInstance> TimeSheets
        {
            get
            {
                return HM<TimeSheetInstance>("TimeSheets");
            }
        }
        public DBList<UserPDP> UserPDP
        {
            get
            {
                return HM<UserPDP>("UserPDP");
            }
        }
        #endregion
        /// <summary>
        /// Констуркторы
        /// </summary>
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
    /// <summary>
    /// Отделения
    /// </summary>
    public class Department : Domain
    {
        new public static string tableName = "DEPARTMENT";
        new public static bool virtualDeletion = true;
        new public static string OrderBy = "DEPARTMENT_MANAGER_TN";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>()
        {
            //UserPDP
            {"UserPDP", new Link("Department_Number", typeof(UserPDP), "Department_Number")},
            //Персонал отделения
            {"PersonalOfDepartment", new Link("Department_Number", typeof(Personal), "Department_Number")},
            //Табели
            {"TimeSheets", new Link("Department_Number", typeof(TimeSheetInstance),"Department_Number")}
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            //Заведующий отделения
            {"DepartmentManager", new Link("DEPARTMENT_MANAGER_TN", typeof(Personal), "Table_Number") },
            //ЛПУ
            {"LPU",new Link("LPU_ID",typeof(LPU))}
        };
        public override string ToString()
        {
            return Name;
        }
        public override string Validation()
        {
            if (Name.Length == 0)
                return "Не введено название отделения";
            if (Department_Number <= 0)
                return "Не указан номер отделения";
            if (LPU == null)
                return "Не указано ЛПУ";
            return "";
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
        public DBList<TimeSheetInstance> TimeSheets
        {
            get
            {
                return HM<TimeSheetInstance>("TimeSheets");
            }
        }
        public DBList<UserPDP> UserPDP
        {
            get
            {
                return HM<UserPDP>("UserPDP");
            }
        }
        #endregion
        /// <summary>
        /// Название|Номер отделения|Табельный номер заведующего
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public new static bool TryParseFromString(string str)
        {
            var values = str.SplitAndTrim('|');
            int temp;
            Department department = new Department();
            department.Name = values[0].ToProperCase();
            if (!int.TryParse(values[1], out temp))
                return false;
            department.Department_Number = temp;
            //if (!int.TryParse(values[2], out temp))
            //return false;
            //department["DEPARTMENT_MANAGER_TN"] = temp;
            department["LPU_ID"] = MainForm.curUsr.LPU.ID;
            return department.Save();
        }
        public Department()
            : base(typeof(Department))
        {
        }
        public Department(string name, int department_Number, LPU lpu)
            : base(typeof(Department))
        {
            Name = name;
            LPU = lpu;
            Department_Number = department_Number;
        }
    }
    /// <summary>
    /// Персонал
    /// </summary>
    public class Personal : Domain
    {
        new public static string tableName = "PERSONAL";
        /// <summary>
        /// Порядок сортировки
        /// </summary>
        new public static string OrderBy = "LastName";
        new public static List<string> FieldNames = new List<string>();
        /// <summary>
        /// Отношения 1:M
        /// </summary>
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() { 
            //Профессии
            {"UserPDP", new Link("Personal_TN",typeof(UserPDP), "Table_Number")}
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            //Основная должность
            {"MainPost", new Link("Post_Code",typeof(Post),"Code")},
            //Отделение
            {"Department", new Link("Department_Number",typeof(Department),"Department_Number")},                        
            //Табельщик, ведующий сотрудника
            {"TimeSheetManager", new Link("TimeSheet_Manager",typeof(User))}
        };
        public override string ToString()
        {
            return _ShortName;
        }
        public override string Validation()
        {
            if (Department == null)
                return "Не указано отделение";
            if (string.IsNullOrEmpty(LastName))
                return "Фамилия заполнена неверно";
            if (string.IsNullOrEmpty(FirstName))
                return "Имя заполнено неверно";
            if (string.IsNullOrEmpty(MiddleName))
                return "Отчество заполнено неверно";
            if (Table_Number <= 0)
                return "Табельный номер заполнен неверно";
            if (Priority <= 0)
                return "Приоритет заполнен неверно";
            return "";
        }
        public static DBList<Personal> GetPersonalOfLPU(int lpu_id)
        {
            return Personal.Query<Personal>(@"department, personal
where   personal.Department_Number=department.Department_Number
        and
        department.lpu_id = " + lpu_id);
        }
        public static DBList<Personal> RaschetchikiOfLPU(int lpu_id)
        {
            return Personal.Query<Personal>(@"raschetchiki, personal, department
where   personal.Table_Number=raschetchiki.Personal_TN and personal.DEPARTMENT_NUMBER=department.DEPARTMENT_NUMBER and department.lpu_id = " + lpu_id.ToString());
        }
        public void MakeRaschetchik()
        {
            DB.Query("insert into raschetchiki(personal_tn) values (" + Table_Number + ")");
        }
        public void DeleteRaschetchik()
        {
            DB.Query("delete from raschetchiki where personal_tn = " + Table_Number);
        }
        #region Properties
        public string _ShortName
        {
            get
            {
                if (FirstName.Length == 0 || MiddleName.Length == 0)
                    return LastName;
                return string.Format("{0} {1}. {2}.", LastName, FirstName[0], MiddleName[0]);
            }
        }
        public string _ShortNameAndNumber
        {
            get
            {
                return string.Format("{0} ({1})", _ShortName, Table_Number);
            }
        }
        public string _FullName
        {
            get
            {
                return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName);
            }
        }
        public string _FullNameAndNumber
        {
            get
            {
                return string.Format("{0} ({1})", _FullName, Table_Number);
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
        public Post MainPost
        {
            get
            {
                return BT<Post>("MainPost");
            }
            set
            {
                this["MainPost"] = value;
            }
        }
        public DBList<UserPDP> UserPDP
        {
            get
            {
                return HM<UserPDP>("UserPDP");
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
        /// <summary>
        /// Фамилия|Имя|Отчество|Табельный номер|Код должности|Номер отделения|Приоритет
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public new static bool TryParseFromString(string str)
        {
            var values = str.SplitAndTrim('|');
            int temp;
            Personal personal = new Personal();
            personal.LastName = values[0].ToProperCase();
            personal.FirstName = values[1].ToProperCase();
            personal.MiddleName = values[2].ToProperCase();
            if (!int.TryParse(values[3], out temp))
                return false;
            personal.Table_Number = temp;
            if (!int.TryParse(values[4], out temp))
                return false;
            personal["Post_Code"] = temp;
            if (!int.TryParse(values[5], out temp))
                return false;
            personal["Department_Number"] = temp;
            if (!int.TryParse(values[6], out temp))
                return false;
            personal["Priority"] = temp;
            return personal.Save();
        }
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
            //Post = post;
        }
    }
    /// <summary>
    /// ЛПУ
    /// </summary>
    public class LPU : Domain
    {
        new public static string tableName = "LPU";
        /// <summary>
        /// Виртуальное удаление
        /// </summary>
        new public static bool virtualDeletion = true;
        /// <summary>
        /// Виртуальное удаление записей связанных отношенями 1:M
        /// </summary>
        new public static bool virtualSubDeletion = true;
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>()
        {
            //Отделения
            {"Departments", new Link("LPU_ID", typeof(Department))},
            //Пользователи
            {"Users", new Link("LPU_ID", typeof(User))}
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            //Главный врач
            {"MainDoc", new Link("MAINDOC_TN",typeof(Personal),"Table_Number")}
        };
        public override string ToString()
        {
            return Name;
        }
        public override string Validation()
        {
            if (string.IsNullOrEmpty(Name))
                return "Название ЛПУ задано неверно";
            return "";
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
    /// <summary>
    /// Показатели (Я, В, и т.д.)
    /// </summary>
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
        public override string Validation()
        {
            if (string.IsNullOrEmpty(Name))
                return "Флаг введен неверно";
            if (string.IsNullOrEmpty(Ru_Name))
                return "Визуальный код флага введен неверно";
            return "";
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
    /// <summary>
    /// Должности
    /// </summary>
    public class Post : Domain
    {
        new public static string tableName = "POSTS";
        new public static string OrderBy = "Name";
        new public static bool virtualDeletion = true;
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        public override string ToString()
        {
            return Name;
        }
        public override bool EqualFunc(object obj)
        {
            return (obj as Post).Name == Name;
        }
        public override string Validation()
        {
            if (Name == "")
                return "Название должности указано неверно";
            if (Code == 0)
                return "Код должности указан неверно";
            return "";
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
        public int Code
        {
            get
            {
                return this["Code"] == null ? 0 : (int)this["Code"];
            }
            set
            {
                this["Code"] = value;
            }
        }
        #endregion
        /// <summary>
        /// Название|Код должности
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public new static bool TryParseFromString(string str)
        {
            var values = str.SplitAndTrim('|');
            int temp;
            Post post = new Post();
            post.Name = values[0].ToProperCase();
            if (!int.TryParse(values[1], out temp))
                return false;
            post.Code = temp;
            return post.Save();
        }
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
    /// <summary>
    /// Производственный календарь
    /// </summary>
    public class Calendar : Domain
    {
        new public static string tableName = "CALENDAR";
        new public static string OrderBy = "CYear";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() {
            //Содержимое календаря (месяцы)
            {"Content", new Link("Calendar_ID",typeof(Calendar_Content))}
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            //Имя
            {"Name", new Link("Name_ID",typeof(Calendar_Name))}
        };
        public override string ToString()
        {
            return NameLink.Name;
        }
        public int GetWorkedDaysForMonth(int month)
        {
            return WorkedDaysInMonth(CYear, month);
        }
        public static int WorkedDaysInMonth(int year, int month)
        {
            int all = DateTime.DaysInMonth(year, month);
            var specdays = SpecialDay.FindAll<SpecialDay>("(state = 1 or state = 2) and spec_date >= '01.{0}.{1}' and spec_date <= '{2}.{0}.{1}'", month, year, all);
            return all - specdays.Count;
        }
        public void Generate12Months(double hours = 0)
        {
            for (int i = 1; i <= 12; i++)
                Months.Add(new Calendar_Content(this, i, hours, GetWorkedDaysForMonth(i)));
        }
        #region Properties
        public DBList<Calendar_Content> Months
        {
            get
            {
                return HM<Calendar_Content>("Content");
            }
        }
        public Calendar_Name NameLink
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
            NameLink = name;
            CYear = year;
        }
    }
    /// <summary>
    /// Содержимое календаря (месяц)
    /// </summary>
    public class Calendar_Content : Domain
    {
        new public static string tableName = "CALENDAR_CONTENT";
        new public static string OrderBy = "CMonth";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {            
            {"Calendar", new Link("Calendar_ID",typeof(Calendar)) }
        };
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
    /// <summary>
    /// Имя календаря
    /// </summary>
    public class Calendar_Name : Domain
    {
        new public static string tableName = "CALENDAR_NAMES";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() {
            {"Calendars", new Link("Name_ID",typeof(Calendar))}
        };
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
    /// <summary>
    /// Табель
    /// </summary>
    public class TimeSheetInstance : Domain
    {
        new public static string tableName = "TIMESHEET";
        new public static string OrderBy = "User_ID, TS_YEAR, TS_MONTH, Department_Number";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>() {
            //Содержимое
            {"Content", new Link("TimeSheet_ID",typeof(TimeSheet_Content))}
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() { 
            //Табельщик
            {"User",new Link("User_ID",typeof(User))},
            //Последний, кто вносил изменения
            {"LastEditor",new Link("Last_Editor_ID",typeof(User))},
            //Отделение
            {"Department",new Link("Department_Number",typeof(Department), "Department_Number")},
            //Расчетчик
            {"RaschetchikProfile",new Link("Raschetchik",typeof(Personal), "Table_Number")}
        };
        public override string ToString()
        {
            return string.Format("{0} - {1}", Department.Name, _GetDate.ToString("MMMM yyyy"));
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
        public User LastEditor
        {
            get
            {
                return BT<User>("LastEditor");
            }
            set
            {
                this["LastEditor"] = value;
            }
        }
        public Personal Raschetchik
        {
            get
            {
                return BT<Personal>("RaschetchikProfile");
            }
            set
            {
                this["RaschetchikProfile"] = value;
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
        public XElement XMLSerialize()
        {
            var tabel = XML<TimeSheetInstance>();
            Content.ForEach(c =>
            {
                var line = c.XML<TimeSheet_Content>();
                line.Add(c.Days.XMLSerialization("Days"));
                tabel.Add(line);
            });
            return tabel;
        }
        public TimeSheetInstance()
            : base(typeof(TimeSheetInstance))
        {
        }
        public TimeSheetInstance(User user, Personal raschetchik, Department department, int year, int month)
            : base(typeof(TimeSheetInstance))
        {
            User = user;
            Raschetchik = raschetchik;
            Department = department;
            TS_Year = year;
            TS_Month = month;
        }
    }
    /// <summary>
    /// Содержимое табеля
    /// </summary>
    public class TimeSheet_Content : Domain
    {
        new public static string tableName = "TIMESHEET_CONTENT";
        new public static string OrderBy = "Personal_TN, Priority";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено        
        new public static Dictionary<string, Link> has_many = new Dictionary<string, Link>()
        {
            //Дни в записи сотрудника
            {"Days", new Link("TimeSheet_Content_ID", typeof(TimeSheet_Day))}            
        };
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>(){
            //Сотрудник
            {"Personal", new Link("Personal_TN",typeof(Personal), "TABLE_NUMBER")},
            //Производственный календарь
            {"Calendar", new Link("Calendar_ID",typeof(Calendar))},
            //Табель
            {"TimeSheet", new Link("TimeSheet_ID",typeof(TimeSheetInstance))},
            //Должность
            {"Post", new Link("Post_Code",typeof(Post), "Code") }            
        };
        public override string ToString()
        {
            return Personal.ToString();
        }
        #region Properties
        public bool _PriorityChanged = false;
        public bool _IsPercentType
        {
            get
            {
                return PercentDays != 0;
            }
        }
        public DBList<TimeSheet_Day> Days
        {
            get
            {
                return HM<TimeSheet_Day>("Days");
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
                return this["Rate"] == null ? 1 : (double)this["Rate"];
            }
            set
            {
                this["Rate"] = value;
            }
        }
        public double Percent
        {
            get
            {
                return this["Percent"] == null ? 100 : (double)this["Percent"];
            }
            set
            {
                this["Percent"] = value;
            }
        }
        public int PercentDays
        {
            get
            {
                return this["PercentDays"] == null ? 0 : (int)this["PercentDays"];
            }
            set
            {
                this["PercentDays"] = value;
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
        #endregion

        public DataGridViewRow[] Render(MyDataGridView dg)
        {
            DataGridViewRow[] result;
            if (Days.Count == 0)
            {
                var row = new DataGridViewRow();
                row.CreateCells(dg);
                row.SetValues(Priority, this, Personal.Table_Number, Post, _IsPercentType ? string.Format("{0}%", Percent) : Rate.ToString());
                return new DataGridViewRow[] { row };
            }
            var groups = Days.GroupBy(d => d.Item_Date);
            var needRows = groups.Max(a => a.Count());
            result = new DataGridViewRow[needRows];
            var values = new object[needRows][];
            var colCount = 5 + TimeSheet._DaysInMonth + 4;
            for (int i = 0; i < needRows; i++)
            {
                result[i] = new DataGridViewRow();
                result[i].CreateCells(dg);
                values[i] = new object[colCount];
            }
            values[0][0] = Priority;
            values[0][1] = this;
            values[0][2] = Personal.Table_Number;
            values[0][3] = Post;
            values[0][4] = _IsPercentType ? string.Format("{0}%", Percent) : Rate.ToString();
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
                            if (sdate != null && (sdate.State == 1 || sdate.State == 2))
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
                    values[i++][group.Key.Day + 4] = item;
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
            TimeSheet = timeSheet;
            Post = post;
            Rate = rate;
        }
    }
    /// <summary>
    /// День табеля
    /// </summary>
    public class TimeSheet_Day : Domain
    {
        new public static string tableName = "TIMESHEET_DAYS";
        new public static string OrderBy = "Item_Date";
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>(){
            //Контент табеля (привязка к строке)
            {"TimeSheetContent", new Link("TimeSheet_Content_ID",typeof(TimeSheet_Content))},
            //Показатель
            {"Flag", new Link("Flag_ID",typeof(Flag))},
        };
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
    /// <summary>
    /// Специальные дни (выходные, сокращенные, праздники)
    /// </summary>
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
    /// <summary>
    /// Глобальные настройки системы
    /// </summary>
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
    /// <summary>
    /// Указание для табельщика, какая профессия у сотрудика в данном отделении
    /// </summary>
    public class UserPDP : Domain
    {
        /// <summary>
        /// Имя таблицы
        /// </summary>
        new public static string tableName = "USERPDP";
        /// <summary>
        /// Список полей (автоматически заполняется из на основе свойств класса, должен быть переопределен в каждом классе)
        /// </summary>
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                
        /// <summary>
        /// Отношения 1:1 - ссылка у привязываемого объекта
        /// </summary>
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            //Ссылка на запись сотрудника
            {"Personal", new Link("Personal_TN", typeof(Personal), "Table_Number")},
            {"Department", new Link("Department_Number", typeof(Department), "Department_Number")},
            {"Post", new Link("Post_Code", typeof(Post), "Code")},
            {"User", new Link("User_ID", typeof(User))}
        };
        public override string ToString()
        {
            return string.Format("{0} - {1}", Personal._ShortNameAndNumber, Post.Name);
        }
        public override string Validation()
        {
            if (User == null)
                return "Не указан пользователь";
            if (Personal == null)
                return "Не указан сотрудник";
            if (Department == null)
                return "Не указано отделение";
            if (Post == null)
                return "Не указана должность";
            return "";
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
        #endregion
        /// <summary>
        /// Констуркторы
        /// </summary>
        public UserPDP()
            : base(typeof(UserPDP))
        {
        }
        public UserPDP(User user, Personal personal, Department department, Post post)
            : base(typeof(UserPDP))
        {
            User = user;
            Personal = personal;
            Department = department;
            Post = post;
        }
    }
}
