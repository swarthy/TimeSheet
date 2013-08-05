using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetManger
{
    public partial class RowEditForm : Form
    {
        MainForm mainForm;
        bool isNew = false;
        public bool defaultval = false;
        public TimeSheet_Content TSContent { get; set; }
        public RowEditForm(MainForm form, TimeSheet_Content row = null, bool defaultval = false)
        {
            mainForm = form;
            TSContent = row ?? new TimeSheet_Content();
            isNew = row == null;
            this.defaultval = defaultval;
            InitializeComponent();
        }

        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            var per_sel = new PersonalListForm(mainForm);            
            if (per_sel.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TSContent.Personal = per_sel.SelectedPersonal;
                tbName.Text = per_sel.SelectedPersonal._ShortName;
                //cbPost.SelectedItem = per_sel.SelectedPersonal.Post;                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbPost.SelectedIndex != -1)
                TSContent.Post = (Post)cbPost.SelectedItem;
            else
            {
                MessageBox.Show("Выберите должность", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbCalendar.SelectedIndex != -1)
                TSContent.Calendar = (Calendar)cbCalendar.SelectedItem;
            else
            {
                MessageBox.Show("Выберите календарь", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            
            if (TSContent.Personal == null)
            {
                MessageBox.Show("Выберите сотрудника", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double rate = 1;
            if (!double.TryParse(tbRate.Text, out rate) || rate < 0)
            {
                MessageBox.Show("Поле ставка заполнено неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TSContent.Rate = rate;
            defaultval = cbDefaultValues.Checked;
            if (isNew)
                mainForm.currentTimeSheet.Content.Add(TSContent);
            //TSContent.TimeSheetManger = mainForm.currentTimeSheet;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
        void DrawPRow(TimeSheet_Content content)
        {
            tbName.Text = content.Personal != null ? content.Personal._ShortName : "";
            tbRate.Text = content.Rate.ToString();
            cbPost.SelectedItem = content.Post;
            cbCalendar.SelectedItem = content.Calendar;
        }

        private void RowEditForm_Load(object sender, EventArgs e)
        {
            cbPost.Items.Clear();
            cbCalendar.Items.Clear();
            mainForm.Posts.ForEach(p => cbPost.Items.Add(p));
            mainForm.Calendars.ForEach(c => cbCalendar.Items.Add(c));
            cbDefaultValues.Visible = defaultval;
            if (TSContent != null)            
                DrawPRow(TSContent);                
        }
    }
}
