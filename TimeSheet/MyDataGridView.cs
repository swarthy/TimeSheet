using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeSheetManager
{
    public class MyDataGridView:DataGridView
    {
        public event EventHandler RowEditStarted;
        public bool RestrictSelection = false;
        public int StartColumnSelect, StopColumnSelect, StartRowSelect, StopRowSelect;
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
            if (e.KeyCode == Keys.Enter)
            {
                if (RowEditStarted != null)
                    RowEditStarted(this, e);
                e.Handled = true;                
            }            
            base.OnKeyDown(e);
        }
        /*
        protected override void SetSelectedCellCore(int columnIndex, int rowIndex, bool selected)
        {
            if (selected && (OnMouseLeftPressed || ModifierKeys == Keys.Control || ModifierKeys == Keys.Shift))
                selected = InAllowedField(rowIndex, columnIndex);
            base.SetSelectedCellCore(columnIndex, rowIndex, selected);
        }*/
        public bool InAllowedField(int row, int col)
        {
            return col <= StopColumnSelect &&
                    col >= StartColumnSelect &&
                    row <= StopRowSelect &&
                    row >= StartRowSelect;
        }
        bool OnMouseLeftPressed = false;
        /*protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                OnMouseLeftPressed = true;
            base.OnCellMouseDown(e);
        }
        protected override void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                OnMouseLeftPressed = false;
            base.OnCellMouseUp(e);
        }*/
        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (RowEditStarted != null)
                RowEditStarted(this, e);            
            base.OnCellDoubleClick(e);
        }
    }
}
