using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetManger
{
    public class MyDataGridView:DataGridView
    {
        public event EventHandler RowEditStarted;        
        public MyDataGridView()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();            
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && RowEditStarted != null)
            {
                RowEditStarted(this, e);
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }
        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (RowEditStarted != null)
                RowEditStarted(this, e);
            base.OnCellDoubleClick(e);
        }
    }
}
