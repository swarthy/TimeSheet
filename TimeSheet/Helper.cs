using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.ComponentModel;

namespace TimeSheet
{
    public static class Helper
    {
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
        public static void Set(string key, object value)
        {
            Properties.Settings.Default[key] = value;
        }
        public static void SetCurrentUser(string key, object value)
        {
            if (MainForm.curUsr==null)
                return;
            Set(MainForm.curUsr.Profile.Table_Number.ToString() + key, value);
        }
        public static void SetAndSaveCurrentUser(string key, object value)
        {
            SetCurrentUser(key, value);
            Properties.Settings.Default.Save();
        }
        public static object GetCurrentUser(string key, object value)
        {
            if (MainForm.curUsr == null)
                return null;
            return Get(MainForm.curUsr.Profile.Table_Number.ToString() + key);
        }
        public static object Get(string key)
        {
            return Properties.Settings.Default[key];
        }
        public static void SetAndSave(string key, object value)
        {
            Set(key, value);
            Properties.Settings.Default.Save();
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
            Console.WriteLine(string.Format(msg, args));
        }
        public static string FormatSpan(TimeSpan span)
        {
            return string.Format("{0:00}:{1:00}", Math.Truncate(span.TotalHours), span.Minutes);
        }
    }
}
