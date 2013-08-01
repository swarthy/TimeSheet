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

namespace TimeSheetManger
{
    public partial class ImportForm : Form
    {
        MainForm mainForm;
        string catalogName = "";
        public ImportForm(MainForm mainform, string catalog)
        {
            InitializeComponent();
            catalogName = catalog;
            Text = "Импорт данных: " + catalog;
            mainForm = mainform;
        }
        bool fileSelected = false;
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            btnStartImport.Enabled = fileSelected = dlgImportFile.ShowDialog() == System.Windows.Forms.DialogResult.OK;
            if (fileSelected)
                tbFileName.Text = dlgImportFile.FileName;
        }

        private void btnStartImport_Click(object sender, EventArgs e)
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
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                bool res;
                try
                {
                    res = code == 0 ? Personal.TryParseFromString(str) : code == 1 ? Post.TryParseFromString(str) : code == 2 ? Department.TryParseFromString(str) : false;
                    if (res)
                        successCount++;
                }
                catch (Exception ex)
                {
                    tbErrors.Text += string.Format("[{0}] Ошибка: \"{1}\" - {2}\r\n", totalCount, str, ex.Message);
                }
                totalCount++;
                lbStatus.Text = string.Format("Статус: {0}/{1}", successCount, totalCount);
            }
            MessageBox.Show(string.Format("Импорт завершен.\r\nКоличество ошибок: {0}/{1}", totalCount-successCount,totalCount), "Статус", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            dlgImportFile.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + @"\import";
        }
    }
}
