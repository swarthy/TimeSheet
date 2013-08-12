using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SwarthyComponents.FireBird;
using TimeSheetManager;
using System.IO;
using System.Diagnostics;

namespace Server
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>                
        public static string ClientForUpdateVersion = "";
        public static DateTime StartTime;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            Console.Title = "Tabel Server Console";
            DBInit();
            GetServerVersion();
            Server.Initialize();
            Server.OnLogin += (data, ci) => {  };
            Server.OnLogout += (ci) => { File.AppendAllText(string.Format("logs\\{0}log.txt", DateTime.Today.ToString("yyyyMM")), string.Format("{0} logout at {1}\r\n", ci.User._LoginAndProfile, DateTime.Now)); };            
            Server.Start();            
            StartTime = DateTime.Now;
            Console.WriteLine("Actual Client Version: {0}", ClientForUpdateVersion);
            string inp="";
            do
            {
                inp = Console.ReadLine();
                switch(inp.ToLower())
                {
                    case "gui":
                        Application.Run(new ServerForm());
                        break;
                    case "status":
                        Server.PingAll();
                        Console.WriteLine("==============Online users ({1}) [{0}]==============", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), Server.server.Connections.Count);
                    Console.WriteLine("==============================================================");
                    foreach (var item in Server.server.Connections)                    
                        Console.WriteLine("{0}\r\nOnline: {1}{2}\r\n", item.ShortInfo, DateTime.Now - item.Connected, item.User==null || item.User.Information == null ? "" : "\r\nInformation: " + item.User.Information.Info);
                    Console.WriteLine("==============================================================");
                        break;
                    case "uptime":
                        Console.WriteLine("Uptime: {0}", DateTime.Now - StartTime);
                        break;
                    case "help":
                        Console.WriteLine("gui - запустить графический интерфейс\r\nstatus - вывести список пользователей, находящихся в сети\r\nuptime - время работы сервера\r\nexit | shutdown - выключение сервера\r\nhelp - вывод этой подсказки");
                        break;
                }
            } while (inp.ToLower() != "shutdown" && inp.ToLower() != "exit");
            Console.WriteLine("Server is shutting down...");
            File.AppendAllText(string.Format("logs\\{0}log.txt", DateTime.Today.ToString("yyyyMM")), string.Format("Server stoped: {0}\r\n", DateTime.Now));
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
        static void GetServerVersion()
        {
            if (File.Exists(@"Updates\updatelist.txt"))
            {
                string[] files = File.ReadAllLines(@"Updates\updatelist.txt");
                if (files.Length > 0 && File.Exists("Updates\\"+files[0]))                
                    ClientForUpdateVersion = FileVersionInfo.GetVersionInfo("Updates\\"+files[0]).FileVersion;                
            }            
        }
    }
}
