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
    public partial class RowEditForm : Form
    {
        MainForm mainForm;                
        public TimeSheet_Content TSContent { get; set; }
        public RowEditForm(MainForm form, TimeSheet_Content row = null)
        {
            mainForm = form;
            TSContent = row ?? new TimeSheet_Content();
            InitializeComponent();
        }

        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            var per_sel = new PersonalListForm(mainForm);
            var res = per_sel.ShowDialog();
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                TSContent.Personal = per_sel.SelectedPersonal;
                tbName.Text = per_sel.SelectedPersonal.Name;
                cbPost.SelectedItem = per_sel.SelectedPersonal.Post;                
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
                TSContent.CalendarIns = (Calendar_Content)cbCalendar.SelectedItem;
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
            TSContent.TimeSheet = mainForm.currentTimeSheet;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
        void DrawPRow(TimeSheet_Content content)
        {            
            tbName.Text = content.Personal != null ? content.Personal.Name : "";
            tbRate.Text = content.Rate.ToString();
            cbPost.SelectedItem = content.Post;
            cbCalendar.SelectedItem = content.CalendarIns;
        }

        private void RowEditForm_Load(object sender, EventArgs e)
        {
            cbPost.Items.Clear();
            cbCalendar.Items.Clear();
            mainForm.Posts.ForEach(p => cbPost.Items.Add(p));
            mainForm.Calendars.ForEach(c => cbCalendar.Items.Add(c));
            if (TSContent != null)            
                DrawPRow(TSContent);                
        }
    }
}
