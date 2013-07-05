using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;
using System.Reflection;

namespace TimeSheet
{
    public static class DB
    {
        internal static FbConnection Connection = GetNewConnection;        
        public static FbConnection GetNewConnection
        {
            get
            {
                return new FbConnection("UserID=SYSDBA;Password=masterkey;" +
                                  "Database=c:/FBDB.FDB;" +
                                  "DataSource=localhost;Charset=NONE;");
            }
        }
    }
    public class Domain
    {        
        public Dictionary<string, object> Fields = new Dictionary<string, object>();
        public object this[string key]
        {
            get
            {
                return Fields[key.ToUpper()];
            }
            set
            {
                Fields[key.ToUpper()] = value;
            }
        }
        public T ParseTo<T>()where T:new()
        {
            if (!typeof(T).IsSubclassOf(typeof(Domain)))
                throw new Exception("Parsing class must be a derived for Domain");
            T newinst = new T();
            (newinst as Domain).Fields = this.Fields;
            typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance).ToList().ForEach(fi => fi.SetValue(newinst, this[fi.Name.ToUpper()]));
            return newinst;
        }
        public override string ToString()
        {
            return Fields.Aggregate(new StringBuilder(), (sb, kvp) => sb.AppendFormat("{0}{1} = \'{2}\'", sb.Length > 0 ? ", " : "", kvp.Key, kvp.Value), sb => sb.ToString());
        }
        protected static List<Domain> FindAll<T>(Dictionary<string, string> restrictions)
        {
            List<Domain> result = new List<Domain>();
            Type type = typeof(T);
            string tableName = (string)type.GetFields(BindingFlags.Public | BindingFlags.Static)[0].GetValue(null);
            List<string> FieldNames = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).ToList().Select<FieldInfo, string>(fi => fi.Name.ToUpper()).ToList();
            string fldNames = FieldNames.Aggregate("", (acc, str) => { return acc + (acc.Length > 0 ? ", " : "") + str; });
            string rest = restrictions.Aggregate(new StringBuilder(), (sb, kvp) => sb.AppendFormat("{0}{1} = \'{2}\'", sb.Length > 0 ? " AND " : "", kvp.Key.ToUpper(), kvp.Value), sb => sb.ToString());
            DB.Connection.Open();
            FbCommand command = new FbCommand(string.Format("select {0} from {1} where {2}", fldNames, tableName, rest), DB.Connection);
            //try
            //{
            FbDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                Domain temp = new Domain();
                FieldNames.ForEach(fn => { temp[fn] = data[fn]; });
                result.Add(temp);
                //temp.ConvertTo<User>();                
                //result.Add();
                //result.Add((T)(object)temp);
                //result.Add((T)(Convert.ChangeType(temp, typeof(T))));

            }
            //}
            //catch (Exception e)
            //{
            //MessageBox.Show(e.Message);
            //}
            DB.Connection.Close();
            return result;
        }        
        //static public int Count(string TableName)
        //{
        //    DB.Connection.Open();
        //    FbCommand command = new FbCommand(string.Format("select count(*) from {0};", TableName), DB.Connection);
        //    FbDataReader data = command.ExecuteReader();
        //    int count = 0;
        //    if (data.Read())
        //        count = (int)data["COUNT"];
        //    else
        //        throw new Exception(string.Format("Can't count records in '{0}' table", TableName));
        //    DB.Connection.Close();
        //    return count;
        //}        
    }
    
    public class User:Domain
    {        
        /// <summary>
        /// Поля должны быть скрыты и называться как поля в БД (регистронезависимо)
        /// </summary>
        
        int id;
        string name, login, pass;
        public static string tableName = "USERS";
#region Fields
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }

        }
        public string Password
        {
            get
            {
                return pass;
            }
            set
            {
                pass = value;
            }
        }
#endregion
        public User()            
        {            
        }
        public User(Domain d)
        {
            id = (int)d["id"];
            name = d["name"].ToString();
            pass = d["pass"].ToString();
            login = d["login"].ToString();
        }
        public User(string login, string password, string name)
        {
            
        }
        public static List<User> FindAll(Dictionary<string, string> reflictions)
        {
            return FindAll<User>(reflictions).Select<Domain, User>(d => new User(d)).ToList();
        }
        
        //public static User Get(int id)
        //{            
        //    User user = new User();
        //    user.id = id;
        //    DB.Connection.Open();
        //    FbCommand command = new FbCommand("select NAME, LOGIN, PASS from USERS where ID = ?", DB.Connection);
        //    command.Parameters.AddWithValue("p0", id);
        //    FbDataReader data = command.ExecuteReader();
        //    if (data.Read())
        //    {
        //        user.name = (string)data["NAME"];
        //        user.login = (string)data["LOGIN"];
        //        user.password = (string)data["PASS"];
        //    }
        //    else
        //        throw new Exception("Record not found");
        //    DB.Connection.Close();
        //    return user;
        //}
        //public override void Save()
        //{
        //    DB.Connection.Open();
        //    FbCommand command = new FbCommand("update USERS set NAME=?, LOGIN=?, PASS=? where ID = ?", DB.Connection);
        //    command.Parameters.AddWithValue("p0", name);
        //    command.Parameters.AddWithValue("p1", login);
        //    command.Parameters.AddWithValue("p2", password);
        //    command.Parameters.AddWithValue("p3", id);
        //    command.ExecuteNonQuery();
        //    DB.Connection.Close();
        //}        
        //public static User Create(string Name, string Login, string Password)
        //{
        //    User usr = new User(Login, Helper.getMD5(Password), Name);            
        //    DB.Connection.Open();            
        //    FbCommand command = new FbCommand("insert into USERS(NAME, LOGIN, PASS) values (?, ?, ?) returning ID;", DB.Connection);
        //    command.Parameters.AddWithValue("p0", usr.name);
        //    command.Parameters.AddWithValue("p1", usr.login);
        //    command.Parameters.AddWithValue("p2", usr.password);
        //    usr.id = (int)command.ExecuteScalar();
        //    DB.Connection.Close();
        //    return usr;
        //}
        //public static User FindOrCreate(string Login, string Password)
        //{
        //    return null;
        //}
    }

}
