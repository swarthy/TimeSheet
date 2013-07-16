using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheet
{
    public partial class AdminPanelForm : Form
    {
        MainForm mainForm;
        string[] tableNames = new string[]{
            "USERS", "PERSONAL", "POSTS", "DEPARTMENT", "FLAGS", "LPU"
        };        
        public AdminPanelForm(MainForm mainform)
        {
            mainForm = mainform;
            InitializeComponent();
        }

        private void btnEditCatalog_Click(object sender, EventArgs e)
        {
            if (cbCatalogs.SelectedIndex == -1)
                return;
            CatalogEditorForm form = new CatalogEditorForm(mainForm, tableNames[cbCatalogs.SelectedIndex], cbCatalogs.SelectedItem.ToString());
            form.Show();
        }
    }
}
