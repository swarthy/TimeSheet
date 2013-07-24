using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetManger
{
    public class MyDataGridView:DataGridView
    {
        public MyDataGridView()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }
    }
}
