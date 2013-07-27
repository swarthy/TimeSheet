using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;

namespace TimeSheetManger
{
    public static class Helper
    {
        public static IniFile settings;    
        public static Random R = new Random();
        public static string getMD5(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
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
                var adr = Get("server", "ip");
                return adr == "" ? "127.0.0.1" : adr;
            }
        }
        public static string ServerFile
        {
            get
            {
                var file = Get("server", "file");
                return string.IsNullOrEmpty(file) ? "c:\\FBDB.fdb" : file;
            }
        }
        public static string ServerUpdatePath
        {
            get
            {
                var updates = Get("server", "updates");
                var dir = string.IsNullOrEmpty(updates) ? "TMUpdates" : updates;
                return string.Format("\\\\{0}\\{1}\\", ServerIP, dir);
            }
        }
        public static string GetFileVersion(string filePath)
        {                        
            return FileVersionInfo.GetVersionInfo(filePath).FileVersion;       
        }
    }
}
