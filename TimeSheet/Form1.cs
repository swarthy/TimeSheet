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
            //User.Create("Administrator", "admin", "admin");
            //MessageBox.Show(User.Count.ToString());
            //User.Initialize("users", "id", "name", "login", "pass");
            //List<User> ls = User.FindAll(new Dictionary<string, string> { { "Name", "Administrator" } });
            Domain test = new Domain();
            test["id"] = 1;
            test["login"] = "a1";
            test["pass"] = "a2";
            test["name"] = "a3";
            User usr = test.ParseTo<User>();            
        }
    }
}
