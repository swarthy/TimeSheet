using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TimeSheetManger
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AppendPrivatePath("lib");            
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("Приложение уже запущено", "Ограничение доступа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }   
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            Application.Run(new MainForm());            
        }
    }
}
