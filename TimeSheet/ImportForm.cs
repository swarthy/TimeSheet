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
                    Import<Personal>(dlgImportFile.FileName);
                    break;
                case "Должности":
                    Import<Post>(dlgImportFile.FileName);
                    break;
                case "Отделения":
                    Import<Department>(dlgImportFile.FileName);
                    break;                
            }
            
        }
        public void Import<T>(string fileName) where T: Domain, new()
        {

        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            dlgImportFile.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + @"\import";
        }
    }
}
