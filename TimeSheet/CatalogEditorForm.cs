using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace TimeSheetManger
{
    public partial class CatalogEditorForm : Form
    {
        MainForm mainForm;
        DataTable tbl;
        FbDataAdapter adapter;
        
        public CatalogEditorForm(MainForm mainform, string table, string caption="")
        {
            mainForm = mainform;            
            InitializeComponent();
            tbl = new DataTable(table);
            Text = "Справочник: " + caption;
            Init();            
        }
        public void Init()
        {            
            adapter = new FbDataAdapter("select * from " + tbl.TableName, DB.Connection);
            adapter.Fill(tbl);
            
            FbCommandBuilder cmd_builder = new FbCommandBuilder(adapter);
            adapter.DeleteCommand = cmd_builder.GetDeleteCommand(); 
            adapter.UpdateCommand = cmd_builder.GetUpdateCommand();            
            adapter.InsertCommand = cmd_builder.GetInsertCommand();
            
            dgTable.DataSource = tbl;
            if (!mainForm.currentUser._IS_ADMIN)
            {
                dgTable.Columns["ID"].Visible = false;
                if (dgTable.Columns.Contains("_DELETED"))//часть ветки VirtualDelete
                    dgTable.Columns["_DELETED"].Visible = false;
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {            
            if (tbl.HasErrors)
                tbl.GetErrors().ToList().ForEach(er => er.ClearErrors());
            try
            {                
                var count = adapter.Update(tbl);
                MessageBox.Show(string.Format("Успешно сохранено. Затронуто {0} строк.", count), "Состояние данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (count > 0)
                    updateTable();
            }
            catch
            {
                if (tbl.HasErrors)
                    MessageBox.Show("Количество ошибок: " + tbl.GetErrors().Length, "Состояние данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        void updateTable()
        {
            tbl.Clear();
            adapter.Fill(tbl);            
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            updateTable();
        }

        private void dgTable_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить выбранную строку?\r\nЭто может привести к нарушению целостности данных в других таблицах и/или справочниках.\r\nДля редактирования данных строки дважды щелкните по выбранной ячейке или нажмите F2", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                e.Cancel = true;
        }
    }
}
