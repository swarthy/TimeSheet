using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SwarthyComponents.FireBird;
using System.Xml.Linq;

namespace TimeSheetManger
{
    //Все классы должны быть инициализированны
    public class User : Domain
    {        
        new public static string tableName = "USERS";        
        new public static bool virtualDeletion = true;        
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                                
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {
            //Ссылка на запись сотрудника
            { "PersonalProfile", new Link("Personal_TN", typeof(Personal), "Table_Number") }        
        };
        new public static Dictionary<string, Link> has_one = new Dictionary<string, Link>() {                         
            {"UserInfo", new Link("User_ID", typeof(UserInfo))}            
        };
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
        public UserInfo Information
        {
            get
            {
                return H1<UserInfo>("UserInfo");
            }
            set
            {
                this["UserInfo"] = value;
                value.User = this;
            }
        }
        #endregion        
        public User()
            : base(typeof(User))
        {
        }        
    }
    public class Personal : Domain
    {
        new public static string tableName = "PERSONAL";
        /// <summary>
        /// Порядок сортировки
        /// </summary>
        new public static string OrderBy = "LastName";
        new public static List<string> FieldNames = new List<string>();

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
        #endregion
        public Personal()
            : base(typeof(Personal))
        {
        }
    }
    public class UserInfo : Domain
    {
        new public static string tableName = "USERINFORMATION";        
        new public static List<string> FieldNames = new List<string>();//обязательно должно быть переопределено                                
        new public static Dictionary<string, Link> belongs_to = new Dictionary<string, Link>() {            
            { "User", new Link("User_ID", typeof(User)) }        
        };
        
        #region Properties
        public string Info
        {
            get
            {
                return this["Info"] == null ? "" : this["Info"].ToString();
            }
            set
            {
                this["Info"] = value;
            }
        }        
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
        #endregion
        public UserInfo()
            : base(typeof(UserInfo))
        {
        }
    }
    
}
