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
            User.Initialize<User>();
            Personal.Initialize<Personal>();
        }        
        private void button1_Click(object sender, EventArgs e)
        {
            //User usr = User.Get(1);
            //MessageBox.Show(usr.name);
            //User.Create("Administrator", "admin", "admin");
            //MessageBox.Show(User.Count.ToString());
            //User.Initialize("users", "id", "name", "login", "pass");            
            /*Domain test = new Domain();
            test["id"] = 1;
            test["login"] = "a1";
            test["pass"] = "a2";
            test["name"] = "a3";
            User usr = test.ParseTo<User>();            */
            //List<User> ls2 = User.FindAll<User>("NAME = 'Administrator' or login = '{0}'","admin");                        
            //MessageBox.Show(User.Count<User>().ToString());            
            //User u = User.Get<User>(22);
            //User u = User.Get<User>(22);
            //object t = u["personal"];
            //var c = Personal.Get<Personal>(1).H1<User>(""
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DB.Connection.State == ConnectionState.Open)
                DB.Connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User u = new User();            
        }

    }
}
