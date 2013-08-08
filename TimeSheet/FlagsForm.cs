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
    public partial class FlagsForm : Form
    {
        MainForm mainForm;
        public int Hours=0, Minutes=0;
        public Flag flag = null;
        public TimeSpan worked_time
        {
            get
            {
                return new TimeSpan(Hours, Minutes, 0);
            }
        }
        private TimeSheet_Day preDay = null;
        public FlagsForm(MainForm mainform, TimeSheet_Day day = null)
        {
            mainForm = mainform;
            preDay = day;
            InitializeComponent();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(tbHours.Text, out Hours) || Hours < 0)
            {
                MessageBox.Show("Количество часов введено неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(tbMinutes.Text, out Minutes) || Minutes < 0 || Minutes > 59)
            {
                MessageBox.Show("Количество минут введено неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var c = pFlags.Controls.OfType<RadioButton>();
            if (c.Any(cc => cc.Checked))
                flag = c.First(b => b.Checked).Tag as Flag;
            else
            {
                MessageBox.Show("Выберите показатель", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
        private void FlagsForm_Load(object sender, EventArgs e)
        {
            var needCh = preDay != null;
            for (int i = 0; i < mainForm.Flags.Count; i++)
                //CreateRadioButton(mainForm.Flags[i].Name, mainForm.Flags[i].Description, 12 + i % 6 * 40, 12 + (i / 6) * 23, mainForm.Flags[i], 100 + i, needCh ? mainForm.Flags[i].ID == preDay.Flag.ID : false);
                CreateRadioButton(mainForm.Flags[i].Name, string.Format("[{1}] {0}",mainForm.Flags[i].Description,mainForm.Flags[i].Ru_Name), 0, i * 18, mainForm.Flags[i], 100 + i, needCh ? mainForm.Flags[i].ID == preDay.Flag.ID : false);
            if (needCh)
                flag = preDay.Flag;
            if (preDay!=null && preDay.Worked_Time != null)
            {
                tbHours.Text = preDay.Worked_Time.Hours.ToString();
                tbMinutes.Text = preDay.Worked_Time.Minutes.ToString();
            }            
            Location = Cursor.Position;            
            Location.Offset(10, 10);
            Rectangle scr = Screen.FromPoint(Location).Bounds;
            if ((Location + Size).X>scr.Width)            
                Left = scr.Width - Width;
            if ((Location + Size).Y > scr.Height)
                Top = scr.Height - Height;
            
        }        
        RadioButton CreateRadioButton(string name, string text, int x, int y,  Flag rbflag, int tabIndex = 0, bool check=false)
        {
            var rb = new RadioButton();
            rb.AutoSize = true;
            rb.Location = new System.Drawing.Point(x, y);
            rb.Name = name;
            rb.Text = text;            
            rb.Checked = check;
            rb.Size = new System.Drawing.Size(85, 17);
            rb.TabStop = true;
            rb.UseVisualStyleBackColor = true;
            rb.Tag = rbflag;
            pFlags.Controls.Add(rb);
            return rb;
        }
    }
}
