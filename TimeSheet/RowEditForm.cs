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
                per_sel.SelectedUserPDP.Save();
                TSContent.Personal = per_sel.SelectedUserPDP.Personal;
                tbName.Text = per_sel.SelectedUserPDP.Personal._ShortName;
                cbPost.SelectedItem = per_sel.SelectedUserPDP.Post;                
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
            TSContent.Rate = 1;
            TSContent.Percent = 0;
            TSContent.PercentDays = 0;
            if (cbRate.Checked)
            {
                double rate = 1;
                if (!double.TryParse(tbRate.Text, out rate) || rate < 0)
                {
                    MessageBox.Show("Поле \"Ставка\" заполнено неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                TSContent.Rate = rate;
            }
            else
            {
                double percent = 0;
                if (!double.TryParse(tbPercent.Text, out percent) || percent < 0 || percent > 100)
                {
                    MessageBox.Show("Поле \"Проценты\" заполнено неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                TSContent.Percent = percent;

                int percentDays = 0;
                if (!int.TryParse(tbPercentDays.Text, out percentDays) || percentDays <= 0)
                {
                    MessageBox.Show("Поле \"Количество дней\" заполнено неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                TSContent.PercentDays = percentDays;
            }

            int priority = 0;
            if (!int.TryParse(tbPrioriry.Text, out priority) || priority < 0)
            {
                MessageBox.Show("Поле \"Приоритет\" заполнено неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TSContent.Priority != priority)
                TSContent._PriorityChanged = true;
            TSContent.Priority = priority;

            defaultval = cbDefaultValues.Checked;
            if (isNew)
                mainForm.currentTimeSheet.Content.Add(TSContent);            
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        void DrawPRow(TimeSheet_Content content)
        {
            tbName.Text = content.Personal != null ? content.Personal._ShortName : "";
            tbRate.Text = content.Rate.ToString();
            tbPercent.Text = content.Percent.ToString();
            tbPercentDays.Text = content.PercentDays.ToString();
            SetPercentType(content._IsPercentType);
            tbPrioriry.Text = content.Priority.ToString();
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
            SetPercentType(TSContent._IsPercentType);
        }

        private void cbRate_CheckedChanged(object sender, EventArgs e)
        {            
            tbRate.Enabled = cbDefaultValues.Enabled = cbRate.Checked;
        }

        private void cbPercent_CheckedChanged(object sender, EventArgs e)
        {            
            tbPercent.Enabled = tbPercentDays.Enabled = cbPercent.Checked;
        }
        void SetPercentType(bool percent = false)
        {
            if (percent)
                cbPercent.Checked = true;
            else
                cbRate.Checked = true;
        }
    }
}
