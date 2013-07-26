﻿using System;
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
        DBList<LPU> lpuList;
        DBList<Post> posts;
        DBList<Department> departments;
        DBList<Flag> flags;
        DBBindingSource<Domain> bs = new DBBindingSource<Domain>();
        MainForm mainForm;
                        
        public CatalogEditorForm(MainForm mainform)
        {
            mainForm = mainform;
            InitializeComponent();
            grid.AutoGenerateColumns = false;                 
        }
        public void OpenUsers()
        {
            users = mainForm.currentLPU.Users;            
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

            bs.DataSource = users;
            grid.DataSource = bs;
        }

        public void OpenPersonals()
        {
            personals = mainForm.currentLPU.Departments.SelectMany(d => d.PersonalOfDepartment).ToDBList();                        
            grid.Columns.Clear();

            DataGridViewTextBoxColumn tn = new DataGridViewTextBoxColumn();
            tn.DataPropertyName = "Table_Number";
            tn.HeaderText = "Табельный номер";            
            grid.Columns.Add(tn);
            
            DataGridViewTextBoxColumn lastName = new DataGridViewTextBoxColumn();
            lastName.DataPropertyName = "LastName";
            lastName.HeaderText = "Фамилия";            
            grid.Columns.Add(lastName);

            DataGridViewTextBoxColumn firstName = new DataGridViewTextBoxColumn();
            firstName.DataPropertyName = "FirstName";
            firstName.HeaderText = "Имя";            
            grid.Columns.Add(firstName);

            DataGridViewTextBoxColumn middleName = new DataGridViewTextBoxColumn();
            middleName.DataPropertyName = "MiddleName";
            middleName.HeaderText = "Отчество";            
            grid.Columns.Add(middleName);

            DataGridViewComboBoxColumn post = new DataGridViewComboBoxColumn();
            post.ValueMember = "_Self";
            post.DataPropertyName = "Post";
            post.DisplayMember = "Name";
            post.HeaderText = "Специальность";
            post.DataSource = Post.All<Post>();
            grid.Columns.Add(post);

            DataGridViewComboBoxColumn department = new DataGridViewComboBoxColumn();
            department.ValueMember = "_Self";
            department.DataPropertyName = "Department";
            department.DisplayMember = "Name";
            department.HeaderText = "Отделение";            
            department.DataSource = mainForm.currentLPU.Departments;            
            grid.Columns.Add(department);

            DataGridViewComboBoxColumn tsmanager = new DataGridViewComboBoxColumn();
            tsmanager.ValueMember = "_Self";            
            tsmanager.DataPropertyName = "TimeSheetManager";
            tsmanager.DisplayMember = "_LoginAndProfile";
            tsmanager.HeaderText = "Табельщик";
            tsmanager.DataSource = mainForm.currentLPU.Users;
            grid.Columns.Add(tsmanager);

            personals.Sort((x, y) => x._FullName.CompareTo(y._FullName));

            bs.DataSource = personals;
            grid.DataSource = bs;          
        }

        public void OpenLPU()
        {
            lpuList = LPU.All<LPU>();            
            grid.Columns.Clear();

            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.DataPropertyName = "Name";
            name.HeaderText = "Название";
            grid.Columns.Add(name);

            DataGridViewComboBoxColumn maindoc = new DataGridViewComboBoxColumn();
            maindoc.ValueMember = "_Self";
            maindoc.DataPropertyName = "MainDoc";
            maindoc.DisplayMember = "_ShortName";
            maindoc.HeaderText = "Главный врач";
            maindoc.DataSource = mainForm.currentLPU.Departments.SelectMany(d => d.PersonalOfDepartment).ToDBList();
            grid.Columns.Add(maindoc);

            bs.DataSource = lpuList;
            grid.DataSource = bs;
        }

        public void OpenPosts()
        {
            posts = Post.All<Post>();
            grid.Columns.Clear();

            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.DataPropertyName = "Name";
            name.HeaderText = "Название";
            grid.Columns.Add(name);

            bs.DataSource = posts;
            grid.DataSource = bs;
        }

        public void OpenDepartments()
        {
            departments = mainForm.currentLPU.Departments;
            grid.Columns.Clear();

            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.DataPropertyName = "Name";
            name.HeaderText = "Название";
            grid.Columns.Add(name);

            DataGridViewTextBoxColumn departmentNumber = new DataGridViewTextBoxColumn();
            departmentNumber.DataPropertyName = "Department_Number";
            departmentNumber.HeaderText = "Номер отделения";
            grid.Columns.Add(departmentNumber);

            DataGridViewComboBoxColumn departmentmanager = new DataGridViewComboBoxColumn();
            departmentmanager.ValueMember = "_Self";
            departmentmanager.DataPropertyName = "DepartmentManager";
            departmentmanager.DisplayMember = "_ShortName";
            departmentmanager.HeaderText = "Заведующий отделения";
            departmentmanager.DataSource = mainForm.currentLPU.Departments.SelectMany(d => d.PersonalOfDepartment).ToDBList();
            grid.Columns.Add(departmentmanager);

            DataGridViewComboBoxColumn lpu = new DataGridViewComboBoxColumn();
            lpu.ValueMember = "_Self";
            lpu.DataPropertyName = "LPU";
            lpu.DisplayMember = "Name";
            lpu.HeaderText = "ЛПУ";
            lpu.DataSource = LPU.All<LPU>();
            grid.Columns.Add(lpu);

            bs.DataSource = departments;
            grid.DataSource = bs;
        }

        public void OpenFlags()
        {
            flags = Flag.All<Flag>();
            grid.Columns.Clear();

            DataGridViewTextBoxColumn ru_name = new DataGridViewTextBoxColumn();
            ru_name.DataPropertyName = "Ru_Name";
            ru_name.HeaderText = "Отображаемое название [3]";
            grid.Columns.Add(ru_name);

            DataGridViewTextBoxColumn description = new DataGridViewTextBoxColumn();
            description.DataPropertyName = "Description";
            description.HeaderText = "Описание";
            grid.Columns.Add(description);

            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.DataPropertyName = "Name";
            name.HeaderText = "Системный код (НЕ ТРОГАТЬ) [3]";
            grid.Columns.Add(name);

            bs.DataSource = flags;
            grid.DataSource = bs;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {                  
            if (MessageBox.Show("Вы уверены, что хотите сохранить изменения?", "Подтверждение сохранения", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;
            foreach (var item in bs)
            {
                (item as Domain).Save();
            }            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить запись?","Подтверждение удаления",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.Yes)
                bs.Remove(bs.Current as Domain, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }    
}
