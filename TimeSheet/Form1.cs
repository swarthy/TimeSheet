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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int i = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            //User usr = User.Get(1);
            //MessageBox.Show(usr.name);
            User.Create("Administrator", "admin", "admin");
            MessageBox.Show(User.Count.ToString());
        }
    }
}
