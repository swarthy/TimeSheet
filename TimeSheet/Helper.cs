using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using SwarthyComponents.FireBird;

namespace TimeSheetManager
{
    public static class Helper
    {
        public static IniFile settings;    
        public static Random R = new Random();
        /*public static string getMD5(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }*/        
        public static void Set(string section, string key, object value)
        {
            settings.IniWriteValue(section, key, value.ToString());
        }
        public static void SetForCurrentUser(string section, string key, object value)
        {
            if (MainForm.curUsr==null)
                return;
            Set(section, MainForm.curUsr.Profile.Table_Number.ToString() + key, value);
        }        
        public static string GetForCurrentUser(string section, string key)
        {
            if (MainForm.curUsr == null)
                return null;
            return Get(section, MainForm.curUsr.Profile.Table_Number.ToString() + key);
        }
        public static string Get(string section, string key)
        {
            return settings.IniReadValue(section, key);
        }
        
        public static object[] EmptyArray(int size)
        {
            var temp = new object[size];
            for (int i = 0; i < size; i++)
                temp[i] = null;
            return temp;
        }
        public static IDictionary<string, object> AnonymousObjectToDictionary(object propertyBag)
        {
            var result = new Dictionary<string, object>();
            if (propertyBag != null)
            {
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(propertyBag))
                {
                    result.Add(property.Name, property.GetValue(propertyBag));
                }
            }
            return result;
        }
        public static void Log(string msg,params object[] args)
        {
            #if DEBUG
                Console.WriteLine(string.Format(msg, args));
            #endif
        }
        public static string FormatSpan(TimeSpan span)
        {
            return string.Format("{0:00}:{1:00}", Math.Truncate(span.TotalHours), span.Minutes);
        }
        public static string ServerIP
        {
            get
            {
                var adr = Get("server", "host");
                return adr == "" ? "127.0.0.1" : adr;
            }
        }
        public static string ServerFile
        {
            get
            {
                var file = Get("server", "file");
                return string.IsNullOrEmpty(file) ? "c:\\Tabel.fdb" : file;
            }
        }
        public static string ServerUpdatePath
        {
            get
            {
                var updates = Get("server", "updatesPath");
                var dir = string.IsNullOrEmpty(updates) ? @"TabelUpdates\" : updates;
                if (dir.Last() != '\\')
                    dir += @"\";
                return Path.Combine(string.Format(@"\\{0}", ServerIP), dir);
            }
        }
        public static string AppVersion
        {
            get
            {
                return GetFileVersion(Application.ExecutablePath);
            }
        }
        public static string GetFileVersion(string filePath)
        {                        
            return FileVersionInfo.GetVersionInfo(filePath).FileVersion;       
        }
        public static void DirectoryCreateIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        public static void EnvExitWithSocketAndDBClosing()
        {
            if (MainForm.Client != null && MainForm.Client.Connected)
                MainForm.Client.Disconnect();
            if (DB.Connected)
                DB.Connection.Close();
            Environment.Exit(0);
        }
    }
    public class WaitScreen
    {        
        private object lockObject = new object();        
        private Form waitScreen;
        public WaitScreen(bool show = false)
        {
            if (show)
                Show();
        }

                
        public void Close()
        {
            lock (this.lockObject)
            {
                if (this.IsShowing)
                {
                    try
                    {
                        this.waitScreen.Invoke(new MethodInvoker(this.CloseWindow));
                    }
                    catch (NullReferenceException)
                    {
                    }
                    this.waitScreen = null;
                }
            }
        }

        private void CloseWindow()
        {
            this.waitScreen.Dispose();
        }

        public void Show()
        {
            if (this.IsShowing)            
                this.Close();                        
            using (ManualResetEvent event2 = new ManualResetEvent(false))
            {
                Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadStart));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start(event2);
                event2.WaitOne();
            }
        }

        private void ThreadStart(object parameter)
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            ManualResetEvent event2 = (ManualResetEvent)parameter;
            Application.EnableVisualStyles();
            this.waitScreen = new WaitingForm();
            this.waitScreen.Tag = event2;            
            this.waitScreen.ControlBox = false;            
            this.waitScreen.Shown += new EventHandler(this.WaitScreenShown);                        
            Application.Run(this.waitScreen);
            Application.ExitThread();
        }

        private void WaitScreenShown(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            form.Shown -= new EventHandler(this.WaitScreenShown);
            ManualResetEvent tag = (ManualResetEvent)form.Tag;
            form.Tag = null;
            tag.Set();
        }
                
        public bool IsShowing
        {
            get
            {
                return (this.waitScreen != null);
            }
        }
    }

}
