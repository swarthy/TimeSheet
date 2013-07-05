using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;

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
    public class DBObject
    {        
        static public int Count(string TableName)
        {
            DB.Connection.Open();
            FbCommand command = new FbCommand(string.Format("select count(*) from {0};", TableName), DB.Connection);
            FbDataReader data = command.ExecuteReader();
            int count = 0;
            if (data.Read())
                count = (int)data["COUNT"];
            else
                throw new Exception(string.Format("Can't count records in '{0}' table", TableName));
            DB.Connection.Close();
            return count;
        }
        public virtual void Save()
        {
        }
        public virtual void Delete()
        {
        }
    }
    public class User : DBObject
    {        
        public int id;
        public string login, password, name;
        public User()
        {
        }
        public User(string login, string password, string name)
        {
            this.login = login;
            this.password = password;
            this.name = name;
        }
        public static int Count
        {
            get
            {
                return DBObject.Count("USERS");
            }
        }
        public static User Get(int id)
        {            
            User user = new User();
            user.id = id;
            DB.Connection.Open();
            FbCommand command = new FbCommand("select NAME, LOGIN, PASS from USERS where ID = ?", DB.Connection);
            command.Parameters.AddWithValue("p0", id);
            FbDataReader data = command.ExecuteReader();
            if (data.Read())
            {
                user.name = (string)data["NAME"];
                user.login = (string)data["LOGIN"];
                user.password = (string)data["PASS"];
            }
            else
                throw new Exception("Record not found");
            DB.Connection.Close();
            return user;
        }
        public override void Save()
        {
            DB.Connection.Open();
            FbCommand command = new FbCommand("update USERS set NAME=?, LOGIN=?, PASS=? where ID = ?", DB.Connection);
            command.Parameters.AddWithValue("p0", name);
            command.Parameters.AddWithValue("p1", login);
            command.Parameters.AddWithValue("p2", password);
            command.Parameters.AddWithValue("p3", id);
            command.ExecuteNonQuery();
            DB.Connection.Close();
        }        
        public static User Create(string Name, string Login, string Password)
        {
            User usr = new User(Login, Helper.getMD5(Password), Name);            
            DB.Connection.Open();            
            FbCommand command = new FbCommand("insert into USERS(NAME, LOGIN, PASS) values (?, ?, ?) returning ID;", DB.Connection);
            command.Parameters.AddWithValue("p0", usr.name);
            command.Parameters.AddWithValue("p1", usr.login);
            command.Parameters.AddWithValue("p2", usr.password);
            usr.id = (int)command.ExecuteScalar();
            DB.Connection.Close();
            return usr;
        }
        public static User FindOrCreate(string Login, string Password)
        {

        }
    }

}
