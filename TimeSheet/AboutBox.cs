using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace TimeSheetManger
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();            
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "http://www.visualpharm.com";
            llIconsSite.Links.Add(link);
        }

        private void llIconsSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData as string);
        }
    }
}
