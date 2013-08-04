using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Updater
{
    class Program
    {
        const string defaultUpdatesPath = @"\\127.0.0.1\TMUpdates\";
        const string updateListName = @"updatelist.txt";
        static void Main(string[] args)
        {
            Console.Title = "Updater";            
            string updaterDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);            
            string UpdatesPath = args.Length > 0 ? args[0] : defaultUpdatesPath;
            List<string> executeList = new List<string>();
            for (int i = 1; i < args.Length; i++)
                executeList.Add(args[i]);
            if (UpdatesPath.Last() != '\\')
                UpdatesPath += @"\";            
            Console.WriteLine("Путь обновлений: {0}", UpdatesPath);
            if (!Directory.Exists(UpdatesPath))
            {
                Console.WriteLine("Сервер обновлений недоступен. Аварийное завершение.");
                return;
            }
            if (!File.Exists(Path.Combine(UpdatesPath, updateListName)))
            {
                Console.WriteLine("Список обновлений [{0}] не найден. Аварийное завершение.", updateListName);
                return;
            }
            var filelist = File.ReadAllLines(Path.Combine(UpdatesPath, updateListName));
            Console.WriteLine("Начато обновление...");            
            int count=0;
            foreach (var file in filelist)
            {
                count++;
                Console.Clear();
                Console.WriteLine("Начато обновление... {0}%", Math.Round((double)count * 100 / (double)filelist.Length, 2));
                try
                {
                    FileCopy(Path.Combine(UpdatesPath, file), Path.Combine(updaterDir, file), true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Исключение: "+ex.Message);
                }
            }
            Console.WriteLine("Обновление завершено.");
            foreach (var exe in executeList)
                if (File.Exists(exe))
                    Process.Start(exe);
        }
        static void FileCopy(string source, string destination, bool rewrite = false)
        {
            var destpath = Path.GetDirectoryName(destination);
            if (!Directory.Exists(destpath))
                Directory.CreateDirectory(destpath);
            if (File.Exists(source))
                File.Copy(source, destination, rewrite);
        }
    }
}
