using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using SwarthyComponents.FireBird;
using System.Threading;

namespace TimeSheetManager
{
    public partial class ImportForm : Form
    {
        MainForm mainForm;
        string catalogName = "";
        bool fileSelected = false;
        bool isImporting = false;
        Thread ImportThread;

        public ImportForm(MainForm mainform, string catalog)
        {
            InitializeComponent();
            catalogName = catalog;
            Text = "Импорт данных: " + catalog;
            mainForm = mainform;
        }        
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            btnStartImport.Enabled = fileSelected = dlgImportFile.ShowDialog() == System.Windows.Forms.DialogResult.OK;
            if (fileSelected)
                tbFileName.Text = dlgImportFile.FileName;
        }        
        private void btnStartImport_Click(object sender, EventArgs e)
        {
            isImporting = true;
            ImportThread = new Thread(new ThreadStart(startImport));
            ImportThread.Start();
        }
        void startImport()
        {
            switch (catalogName)
            {
                case "Персонал":
                    Import(dlgImportFile.FileName, 0);
                    break;
                case "Должности":
                    Import(dlgImportFile.FileName, 1);
                    break;
                case "Отделения":
                    Import(dlgImportFile.FileName, 2);
                    break;
            }            
        }        
        public void Import(string fileName, int code)
        {            
            StreamReader sr = new StreamReader(fileName);            
            int totalCount = 0, successCount = 0;
            while (!sr.EndOfStream && isImporting)
            {
                string str = sr.ReadLine();
                if (str.Trim().Length == 0)
                    continue;
                bool res;
                try
                {
                    res = code == 0 ? Personal.TryParseFromString(str) : code == 1 ? Post.TryParseFromString(str) : code == 2 ? Department.TryParseFromString(str) : false;
                    if (res)
                        successCount++;
                    else
                        AppendTextBox(string.Format("[{0}] Ошибка: \"{1}\" - Не сохранился\r\n", totalCount, str));                                        
                }
                catch (Exception ex)
                {                    
                    AppendTextBox(string.Format("[{0}] Ошибка: \"{1}\" - {2}\r\n", totalCount, str, ex.Message));                                        
                }
                totalCount++;
                SetStatusLabel(string.Format("Импортировано: {0}/{1}", successCount, totalCount));                
            }            
            MessageBox.Show(string.Format("Количество ошибок: {0}/{1}", totalCount-successCount,totalCount), "Импорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnStartImport.Enabled = true;

        }        
        private void ImportForm_Load(object sender, EventArgs e)
        {
            dlgImportFile.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + @"\import";            
        }
        public void AppendTextBox(string value)
        {
            if (tbErrors.InvokeRequired)            
                Invoke(new MethodInvoker(() => { tbErrors.AppendText(value); }));            
            else
                tbErrors.AppendText(value);
        }
        public void SetStatusLabel(string value)
        {
            if (lbStatus.InvokeRequired)
                Invoke(new MethodInvoker(() => { lbStatus.Text = value; }));
            else
                lbStatus.Text = value;
        }
                
        private void ImportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isImporting = false;
        }

    }
}
