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
        DBList<User> users;
        DBList<Personal> personals;
        MainForm mainForm;
        string currentTable = "";
        public CatalogEditorForm(MainForm mainform)
        {
            mainForm = mainform;
            InitializeComponent();
            grid.AutoGenerateColumns = false;
        }
        public void OpenUsers()
        {
            users = mainForm.currentLPU.Users;
            currentTable = "users";
            grid.Columns.Clear();

            DataGridViewTextBoxColumn login = new DataGridViewTextBoxColumn();
            login.DataPropertyName = "Login";
            login.HeaderText = "Логин";
            grid.Columns.Add(login);

            DataGridViewTextBoxColumn pass = new DataGridViewTextBoxColumn();
            pass.DataPropertyName = "Pass";
            pass.HeaderText = "Пароль";
            grid.Columns.Add(pass);

            DataGridViewComboBoxColumn lpu = new DataGridViewComboBoxColumn();
            lpu.ValueMember = "_Self";
            lpu.DataPropertyName = "LPU";
            lpu.DisplayMember = "Name";
            lpu.HeaderText = "ЛПУ";
            lpu.DataSource = LPU.All<LPU>();
            grid.Columns.Add(lpu);

            DataGridViewComboBoxColumn profile = new DataGridViewComboBoxColumn();
            profile.ValueMember = "_Self";
            profile.DataPropertyName = "Profile";
            profile.DisplayMember = "_ShortName";
            profile.HeaderText = "Профиль";
            profile.DataSource = Personal.All<Personal>();
            grid.Columns.Add(profile);

            DataGridViewTextBoxColumn role = new DataGridViewTextBoxColumn();
            role.DataPropertyName = "Role";
            role.HeaderText = "Роль";
            grid.Columns.Add(role);

            grid.DataSource = users;
        }

        public void OpenPersonals()
        {
            personals = mainForm.currentLPU.Departments.SelectMany(d => d.PersonalOfDepartment).ToDBList();
            currentTable = "personals";
            grid.Columns.Clear();

            DataGridViewTextBoxColumn tn = new DataGridViewTextBoxColumn();
            tn.DataPropertyName = "Table_Number";
            tn.HeaderText = "Табельный номер";
            tn.SortMode = DataGridViewColumnSortMode.Automatic;
            grid.Columns.Add(tn);
            
            DataGridViewTextBoxColumn lastName = new DataGridViewTextBoxColumn();
            lastName.DataPropertyName = "LastName";
            lastName.HeaderText = "Фамилия";
            lastName.SortMode = DataGridViewColumnSortMode.Automatic;
            grid.Columns.Add(lastName);

            DataGridViewTextBoxColumn firstName = new DataGridViewTextBoxColumn();
            firstName.DataPropertyName = "FirstName";
            firstName.HeaderText = "Имя";
            firstName.SortMode = DataGridViewColumnSortMode.Automatic;
            grid.Columns.Add(firstName);

            DataGridViewTextBoxColumn middleName = new DataGridViewTextBoxColumn();
            middleName.DataPropertyName = "MiddleName";
            middleName.HeaderText = "Отчество";
            middleName.SortMode = DataGridViewColumnSortMode.Automatic;
            grid.Columns.Add(middleName);

            DataGridViewComboBoxColumn post = new DataGridViewComboBoxColumn();
            post.ValueMember = "_Self";
            post.DataPropertyName = "Post";
            post.DisplayMember = "Name";
            post.HeaderText = "Специальность";
            post.DataSource = Post.All<Post>();
            post.SortMode = DataGridViewColumnSortMode.Automatic;
            grid.Columns.Add(post);

            DataGridViewComboBoxColumn department = new DataGridViewComboBoxColumn();
            department.ValueMember = "_Self";
            department.DataPropertyName = "Department";
            department.DisplayMember = "Name";
            department.HeaderText = "Отделение";
            department.DataSource = Department.All<Department>();
            department.SortMode = DataGridViewColumnSortMode.Automatic;
            grid.Columns.Add(department);

            grid.DataSource = personals;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите сохранить изменения?", "Подтверждение сохранения", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;
            switch (currentTable)
            {
                case "users":
                    users.ForEach(u => u.Save());
                    break;
            }
        }
    }    
}
