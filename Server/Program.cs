using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SwarthyComponents.FireBird;
using TimeSheetManger;
using System.IO;

namespace Server
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DBInit();
            Server.Initialize();
            Server.OnLogin += (data, ci) => { Console.WriteLine("Auth user {0} from {1}", ci.User.Login, ci.Socket.RemoteEndPoint); };
            Server.OnLogout += (data, ci) => { Console.WriteLine("Logout user {0}", ci.User.Login); };            
            Server.Start();            
            string inp="";
            do
            {
                inp = Console.ReadLine();
                if (inp.ToLower() == "gui")
                    Application.Run(new ServerForm());
            } while (inp.ToLower() != "shutdown");
            Server.Stop();
        }
        static void DBInit()
        {
            DB.ConnectionString = string.Format("UserID=SYSDBA;Password=masterkey;Database=127.0.0.1:{0};Charset=NONE;", GetDBPath());

            Domain.VirtualDeletionField = "DELDATE";
            Domain.VirtualDeletionNotDeletedRecord = "DELDATE is null";
            Domain.VirtualDeletedValue = () => { return DateTime.Today; };

            User.Initialize<User>();
            Personal.Initialize<Personal>();
            UserInfo.Initialize<UserInfo>();            
        }
        static string GetDBPath()
        {
            var path = File.Exists("dbpath.txt")?File.ReadAllText("dbpath.txt"):@"C:\Tabel.fdb";
            if (!File.Exists(path))
                throw new Exception("DB File not exsits! "+path);
            return path;
        }
    }
}
