using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetManager
{
    public partial class Raschetchiki : Form
    {
        MainForm mainForm;
        public Raschetchiki(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void lbRaschetchiki_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = true;
        }

        private void Raschetchiki_Load(object sender, EventArgs e)
        {
            UpdateSource();
            cbLPUPersonal.DataSource = Personal.GetPersonalOfLPU(mainForm.currentLPU.ID);
        }

        void UpdateSource()
        {
            lbRaschetchiki.DataSource = Personal.RaschetchikiOfLPU(mainForm.currentLPU.ID);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbRaschetchiki.SelectedItem != null && MessageBox.Show("Вы уверены, что хотите удалить выбранного сотрудника из списка расчетчиков?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                (lbRaschetchiki.SelectedItem as Personal).DeleteRaschetchik();
                UpdateSource();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbLPUPersonal.SelectedItem != null)
            {
                (cbLPUPersonal.SelectedItem as Personal).MakeRaschetchik();
                UpdateSource();
            }
        }
    }
}
