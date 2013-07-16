using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace TimeSheet
{
    public partial class CatalogEditorForm : Form
    {
        MainForm mainForm;
        DataTable tbl;
        FbDataAdapter adapter;
        FbTransaction transaction;
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
        }
        void updateTransaction()
        {
            transaction = DB.Connection.BeginTransaction();
            adapter.DeleteCommand.Transaction = transaction;
            adapter.UpdateCommand.Transaction = transaction;
            adapter.InsertCommand.Transaction = transaction;
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
    }
}
