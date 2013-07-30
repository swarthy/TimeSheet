﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using Equin.ApplicationFramework;

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
        MainForm mainForm;
        DBList<Personal> personalsOfLPU(LPU lpu)
        {
            return lpu.Departments.SelectMany(d => d.PersonalOfDepartment).ToDBList();
        }
        DBList<Personal> LPUPersonals
        {
            get
            {
                return personalsOfLPU(mainForm.currentLPU);
            }
        }

        public event EventHandler EditingComplete;        
        public event EventHandler AddingComplete;
        public event EventHandler RowDeleting;
        void aF(Control tb)
        {
            flFilters.Controls.Add(tb);
        }
        void aE(Control tb)
        {
            tb.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    CompleteEnteringValues(this, e);
                }
                else
                    if (e.KeyCode == Keys.Escape)
                    {
                        e.Handled = true;
                        cancelEditing();
                    }
            };
            flEditBox.Controls.Add(tb);
        }
        bool addingNewRow = false;
        bool success = false;
        void CompleteEnteringValues(object sender, EventArgs e)
        {
            if (addingNewRow && AddingComplete != null)
                AddingComplete(sender, e);
            else
                if (EditingComplete != null)
                    EditingComplete(sender, e);
            if (success)
            {
                MessageBox.Show("Success!");
                cancelEditing();
            }
            else
                MessageBox.Show("Fail!");
        }
        void beginEditing()
        {
            gbValues.Show();
            setControlsEnabled(false);            
            flEditBox.Controls[0].Focus();
        }
        void cancelEditing()
        {
            addingNewRow = false;
            gbValues.Hide();            
            setControlsEnabled(true);
            grid.Focus();
        }
        void setControlsEnabled(bool value)
        {
            btnAdd.Enabled = btnDelete.Enabled = btnFiltersEnable.Enabled = btnCancel.Enabled = value;
        }        
        public CatalogEditorForm(MainForm mainform, string catalogTitle)
        {
            mainForm = mainform;
            InitializeComponent();
            grid.AutoGenerateColumns = false;
            AddingComplete += (s, e) => { success = true; };
            EditingComplete += (s, e) => { success = true; };
            grid.RowEditStarted += (s, e) => { beginEditing(); };
            Text = string.Format("Справочник: {0}", catalogTitle);            
        }
        public void MyAfterOpenningInit()//вызывается после Open<Domain>
        {
            grid.DataSource = bs;
            Button btnSave = new Button();
            btnSave.Text = "Сохранить";
            btnSave.Click += CompleteEnteringValues;
            flEditBox.Controls.Add(btnSave);
            
            Button btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Click += (s, e) => { cancelEditing(); };
            flEditBox.Controls.Add(btnCancel);
        }
        //done
        public void OpenUsers()
        {
            users = mainForm.currentLPU.Users;            
            BindingListView<User> view = new BindingListView<User>(users);
            #region Столбцы и элементы управления
            grid.Columns.Clear();
            
            DataGridViewTextBoxColumn login = new DataGridViewTextBoxColumn();
            login.DataPropertyName = "Login";
            login.HeaderText = "Логин";
            grid.Columns.Add(login);
            var loginFTB = new MyTB((box) => { view.ApplyFilter(u => u.Login.Contains(box.Text)); });            
            aF(loginFTB);
            var loginETB = new MyTB();
            aE(loginETB);

            DataGridViewTextBoxColumn pass = new DataGridViewTextBoxColumn();
            pass.DataPropertyName = "Pass";
            pass.HeaderText = "Пароль";
            grid.Columns.Add(pass);
            var passFTB = new MyTB((box) => { view.ApplyFilter(u => u.Pass.Contains(box.Text)); });            
            aF(passFTB);
            var passETB = new MyTB();
            aE(passETB);
            
            DataGridViewTextBoxColumn lpu = new DataGridViewTextBoxColumn();
            lpu.DataPropertyName = "LPU";
            lpu.HeaderText = "ЛПУ";
            grid.Columns.Add(lpu);
            var lpuFB = new MyCB(LPU.All<LPU>(), (box) => { view.ApplyFilter(u => u.LPU.Name.Contains(box.Text)); });
            aF(lpuFB);
            var lpuEB = new MyCB(LPU.All<LPU>());
            aE(lpuEB);
            
            DataGridViewTextBoxColumn profile = new DataGridViewTextBoxColumn();
            profile.DataPropertyName = "Profile";
            profile.HeaderText = "Профиль";
            grid.Columns.Add(profile);
            var profileFB = new MyCB(LPUPersonals, (box) => { view.ApplyFilter(u => u.Profile.ToString().Contains(box.Text)); });
            aF(profileFB);
            var profileEB = new MyCB(LPUPersonals);            
            aE(profileEB);

            DataGridViewTextBoxColumn role = new DataGridViewTextBoxColumn();
            role.DataPropertyName = "Role";
            role.HeaderText = "Роль";
            grid.Columns.Add(role);            
            var roleFTB = new MyTB((box) => { view.ApplyFilter(u => u.Role.ToString().Contains(box.Text)); });            
            aF(roleFTB);
            var roleETB = new MyTB();
            aE(roleETB);
            #endregion
            grid.RowEditStarted +=
            delegate
            {
                var usr = (bs.Current as ObjectView<User>).Object;
                loginETB.Text = usr.Login;
                passETB.Text = usr.Pass;
                lpuEB.SelectedItem = usr.LPU;
                profileEB.SelectedItem = usr.Profile;
                roleETB.Text = usr.Role.ToString();
            };

            EditingComplete += (s, e) =>
            {
                var usr = (bs.Current as ObjectView<User>).Object;
                usr.Login = loginETB.Text;
                usr.Pass = passETB.Text;
                usr.LPU = lpuEB.SelectedItem as LPU;
                usr.Profile = profileEB.SelectedItem as Personal;
                int tempRole;
                if (!int.TryParse(roleETB.Text, out tempRole))
                {
                    MessageBox.Show("Роль введена неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    success = false;
                    return;
                }
                usr.Role = tempRole;
                if (!usr.Save())
                {
                    success = false;
                    return;
                }
                view.Refresh();
            };

            AddingComplete += (s, e) =>
            {
                var usr = new User();
                usr.Login = loginETB.Text;
                usr.Pass = passETB.Text;
                usr.LPU = lpuEB.SelectedItem as LPU;
                usr.Profile = profileEB.SelectedItem as Personal;
                int tempRole;
                if (!int.TryParse(roleETB.Text, out tempRole))
                {
                    MessageBox.Show("Роль введена неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    success = false;
                    return;
                }
                usr.Role = tempRole;
                if (!usr.Save())
                {
                    success = false;
                    return;
                }
                users.Add(usr);
                view.Refresh();
            };

            RowDeleting += (s, e) =>
            {
                users.Remove((bs.Current as ObjectView<User>).Object, true);
                view.Refresh();
            };
            bs.DataSource = view;
        }
        //done
        public void OpenPersonals()
        {
            personals = LPUPersonals;
            BindingListView<Personal> view = new BindingListView<Personal>(personals);
            #region Столбцы и элементы управления
            grid.Columns.Clear();

            DataGridViewTextBoxColumn tn = new DataGridViewTextBoxColumn();
            tn.DataPropertyName = "Table_Number";
            tn.HeaderText = "Табельный номер";
            grid.Columns.Add(tn);
            var tnFTB = new MyTB((box) => { view.ApplyFilter(p => p.Table_Number.ToString().Contains(box.Text)); });
            aF(tnFTB);
            var tnETB = new MyTB();
            aE(tnETB);

            DataGridViewTextBoxColumn lastName = new DataGridViewTextBoxColumn();
            lastName.DataPropertyName = "LastName";
            lastName.HeaderText = "Фамилия";
            grid.Columns.Add(lastName);
            var lastNameFTB = new MyTB((box) => { view.ApplyFilter(p => p.LastName.Contains(box.Text)); });
            aF(lastNameFTB);
            var lastNameETB = new MyTB();
            aE(lastNameETB);

            DataGridViewTextBoxColumn firstName = new DataGridViewTextBoxColumn();
            firstName.DataPropertyName = "FirstName";
            firstName.HeaderText = "Имя";
            grid.Columns.Add(firstName);
            var firstNameFTB = new MyTB((box) => { view.ApplyFilter(p => p.FirstName.Contains(box.Text)); });
            aF(firstNameFTB);
            var firstNameETB = new MyTB();
            aE(firstNameETB);

            DataGridViewTextBoxColumn middleName = new DataGridViewTextBoxColumn();
            middleName.DataPropertyName = "MiddleName";
            middleName.HeaderText = "Отчество";
            grid.Columns.Add(middleName);
            var middleNameFTB = new MyTB((box) => { view.ApplyFilter(p => p.MiddleName.Contains(box.Text)); });
            aF(middleNameFTB);
            var middleNameETB = new MyTB();
            aE(middleNameETB);

            DataGridViewTextBoxColumn post = new DataGridViewTextBoxColumn();
            post.DataPropertyName = "Post";
            post.HeaderText = "Должность";
            grid.Columns.Add(post);
            var postFB = new MyCB(Post.All<Post>(), (box) => { view.ApplyFilter(u => u.Post.Name.Contains(box.Text)); });
            aF(postFB);
            var postEB = new MyCB(Post.All<Post>());
            aE(postEB);

            DataGridViewTextBoxColumn department = new DataGridViewTextBoxColumn();
            department.DataPropertyName = "Department";
            department.HeaderText = "Отделение";
            grid.Columns.Add(department);
            var DepartmentFB = new MyCB(mainForm.currentLPU.Departments.ToList(), (box) => { view.ApplyFilter(u => u.Department.Name.Contains(box.Text)); });
            aF(DepartmentFB);
            var DepartmentEB = new MyCB(mainForm.currentLPU.Departments.ToList());
            aE(DepartmentEB);

            DataGridViewTextBoxColumn tsManager = new DataGridViewTextBoxColumn();
            tsManager.DataPropertyName = "TimeSheetManager";
            tsManager.HeaderText = "Табельщик";
            grid.Columns.Add(tsManager);
            var tsManagerFB = new MyCB(mainForm.currentLPU.Users.ToList(), (box) => { view.ApplyFilter((p) => { if (p.TimeSheetManager == null) return true; else return p.TimeSheetManager.ToString().Contains(box.Text); }); });
            aF(tsManagerFB);
            var tsManagerEB = new MyCB(mainForm.currentLPU.Users.ToList());
            aE(tsManagerEB);

            DataGridViewTextBoxColumn priority = new DataGridViewTextBoxColumn();
            priority.DataPropertyName = "Priority";
            priority.HeaderText = "Приоритет";
            grid.Columns.Add(priority);
            var priorityFTB = new MyTB((box) => { view.ApplyFilter(u => u.Priority.ToString().Contains(box.Text)); });
            aF(priorityFTB);
            var priorityETB = new MyTB();
            aE(priorityETB);

            #endregion

            grid.RowEditStarted +=
            delegate
            {
                var personal = (bs.Current as ObjectView<Personal>).Object;
                tnETB.Text = personal.Table_Number.ToString();
                lastNameETB.Text= personal.LastName;
                middleNameETB.Text = personal.MiddleName;
                firstNameETB.Text = personal.FirstName;
                postEB.SelectedItem = personal.Post;
                DepartmentEB.SelectedItem = personal.Department;
                tsManagerEB.SelectedItem = personal.TimeSheetManager;
                priorityETB.Text = personal.Priority.ToString();
            };

            EditingComplete += (s, e) =>
            {
                var personal = (bs.Current as ObjectView<Personal>).Object;
                
                int temp;
                if (!int.TryParse(tnETB.Text, out temp))
                {
                    MessageBox.Show("Табельный номер введен неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    success = false;
                    return;
                }
                personal.Table_Number = temp;
                personal.FirstName = firstNameETB.Text;
                personal.LastName = lastNameETB.Text;
                personal.MiddleName = middleNameETB.Text;
                personal.Post = postEB.SelectedItem as Post;
                personal.Department = DepartmentEB.SelectedItem as Department;
                personal.TimeSheetManager = tsManagerEB.SelectedItem as User;
                                
                if (!int.TryParse(tnETB.Text, out temp))
                {
                    MessageBox.Show("Приоритет введен неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    success = false;
                    return;
                }
                personal.Priority = temp;
                
                if (!personal.Save())
                {
                    success = false;
                    return;
                }
                view.Refresh();
            };

            AddingComplete += (s, e) =>
            {
                var personal = new Personal();
                int temp;
                if (!int.TryParse(tnETB.Text, out temp))
                {
                    MessageBox.Show("Табельный номер введен неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    success = false;
                    return;
                }
                personal.Table_Number = temp;
                personal.FirstName = firstNameETB.Text;
                personal.LastName = lastNameETB.Text;
                personal.MiddleName = middleNameETB.Text;
                personal.Post = postEB.SelectedItem as Post;
                personal.Department = DepartmentEB.SelectedItem as Department;
                personal.TimeSheetManager = tsManagerEB.SelectedItem as User;

                if (!int.TryParse(tnETB.Text, out temp))
                {
                    MessageBox.Show("Приоритет введен неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    success = false;
                    return;
                }
                personal.Priority = temp;

                if (!personal.Save())
                {
                    success = false;
                    return;
                }
                personals.Add(personal);                
                view.Refresh();
            };

            RowDeleting += (s, e) =>
            {
                personals.Remove((bs.Current as ObjectView<Personal>).Object, true);
                view.Refresh();
            };
            bs.DataSource = view;
        }
        //done
        public void OpenLPU()
        {
            lpuList = LPU.All<LPU>();
            BindingListView<LPU> view = new BindingListView<LPU>(lpuList);            
            grid.Columns.Clear();

            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.DataPropertyName = "Name";
            name.HeaderText = "Название";
            grid.Columns.Add(name);
            var nameFTB = new MyTB((box) => { view.ApplyFilter(l => l.Name.Contains(box.Text)); });
            aF(nameFTB);
            var nameETB = new MyTB();
            aE(nameETB);

            DataGridViewTextBoxColumn maindoc = new DataGridViewTextBoxColumn();
            maindoc.DataPropertyName = "MainDoc";
            maindoc.HeaderText = "Главный врач";
            grid.Columns.Add(maindoc);
            var maindocFB = new MyCB(LPUPersonals, (box) => { view.ApplyFilter(l => l.MainDoc.ToString().Contains(box.Text)); });
            aF(maindocFB);
            var maindocEB = new MyCB(LPUPersonals);
            aE(maindocEB);


            grid.RowEditStarted +=
            delegate
            {
                var lpu = (bs.Current as ObjectView<LPU>).Object;
                nameETB.Text = lpu.Name;
                maindocEB.SelectedItem = lpu.MainDoc;                
            };

            EditingComplete += (s, e) =>
            {                
                var lpu = (bs.Current as ObjectView<LPU>).Object;
                lpu.Name = nameETB.Text;
                lpu.MainDoc = maindocEB.SelectedItem as Personal;                
                if (!lpu.Save())
                {
                    success = false;
                    return;
                }
                view.Refresh();
            };

            AddingComplete += (s, e) =>
            {
                var lpu = new LPU();
                lpu.Name = nameETB.Text;
                lpu.MainDoc = maindocEB.SelectedItem as Personal;
                if (!lpu.Save())
                {
                    success = false;
                    return;
                }
                lpuList.Add(lpu);
                view.Refresh();
            };

            RowDeleting += (s, e) =>
            {
                lpuList.Remove((bs.Current as ObjectView<LPU>).Object, true);
                view.Refresh();
            };
            bs.DataSource = view;
        }
        //done
        public void OpenPosts()
        {
            posts = Post.All<Post>();
            BindingListView<LPU> view = new BindingListView<LPU>(posts);
            grid.Columns.Clear();

            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.DataPropertyName = "Name";
            name.HeaderText = "Название";
            grid.Columns.Add(name);
            var nameFTB = new MyTB((box) => { view.ApplyFilter(l => l.Name.Contains(box.Text)); });
            aF(nameFTB);
            var nameETB = new MyTB();
            aE(nameETB);


            grid.RowEditStarted +=
            delegate
            {
                var post = (bs.Current as ObjectView<Post>).Object;
                nameETB.Text = post.Name;                
            };

            EditingComplete += (s, e) =>
            {
                var post = (bs.Current as ObjectView<Post>).Object;
                post.Name = nameETB.Text;
                if (!post.Save())
                {
                    success = false;
                    return;
                }
                view.Refresh();
            };

            AddingComplete += (s, e) =>
            {
                var post = new Post();
                post.Name = nameETB.Text;
                if (!post.Save())
                {
                    success = false;
                    return;
                }
                posts.Add(post);
                view.Refresh();
            };

            RowDeleting += (s, e) =>
            {
                lpuList.Remove((bs.Current as ObjectView<LPU>).Object, true);
                view.Refresh();
            };
            bs.DataSource = view;
        }

        public void OpenDepartments()
        {
            throw new NotImplementedException("Отделения будут доступны в следующих версиях");
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
            departmentmanager.DisplayMember = "_ShortNameAndNumber";
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

        //done
        public void OpenFlags()
        {
            flags = Flag.All<Flag>();

            BindingListView<Flag> view = new BindingListView<Flag>(flags);            

            grid.Columns.Clear();

            DataGridViewTextBoxColumn ru_name = new DataGridViewTextBoxColumn();
            ru_name.DataPropertyName = "Ru_Name";
            ru_name.HeaderText = "Отображаемое название [3]";
            grid.Columns.Add(ru_name);
            var ru_nameFTB = new MyTB((box) => { view.ApplyFilter(f => f.Ru_Name.Contains(box.Text)); });
            aF(ru_nameFTB);
            var ru_nameETB = new MyTB();
            aE(ru_nameETB);

            DataGridViewTextBoxColumn description = new DataGridViewTextBoxColumn();
            description.DataPropertyName = "Description";
            description.HeaderText = "Описание";
            grid.Columns.Add(description);
            var descriptionFTB = new MyTB((box) => { view.ApplyFilter(f => f.Description.Contains(box.Text)); });
            aF(descriptionFTB);
            var descriptionETB = new MyTB();
            aE(descriptionETB);

            DataGridViewTextBoxColumn name = new DataGridViewTextBoxColumn();
            name.DataPropertyName = "name";
            name.HeaderText = "Код [НЕ ТРОГАТЬ!]";
            grid.Columns.Add(name);
            var nameFTB = new MyTB((box) => { view.ApplyFilter(f => f.Name.Contains(box.Text)); });
            aF(nameFTB);
            var nameETB = new MyTB();
            aE(nameETB);
            
            grid.RowEditStarted +=
            delegate
            {
                var flag = (bs.Current as ObjectView<Flag>).Object;
                ru_nameETB.Text = flag.Ru_Name;
                descriptionETB.Text = flag.Description;
                nameETB.Text = flag.Name;
            };

            EditingComplete += (s, e) =>
            {
                var flag = (bs.Current as ObjectView<Flag>).Object;
                flag.Ru_Name = ru_nameETB.Text;
                flag.Description = descriptionETB.Text;
                flag.Name = nameETB.Text;

                if (!flag.Save())
                {
                    success = false;
                    return;
                }
                view.Refresh();
            };

            AddingComplete += (s, e) =>
            {
                var flag = new Flag();
                flag.Ru_Name = ru_nameETB.Text;
                flag.Description = descriptionETB.Text;
                flag.Name = nameETB.Text;
                if (!flag.Save())
                {
                    success = false;
                    return;
                }
                flags.Add(flag);
                view.Refresh();
            };

            RowDeleting += (s, e) =>
            {
                flags.Remove((bs.Current as ObjectView<Flag>).Object, true);
                view.Refresh();
            };
            bs.DataSource = view;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (RowDeleting != null && MessageBox.Show("Вы действительно хотите удалить запись?\r\nЭто может повлечь за собой необратимое нарушение целостности данных.", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                RowDeleting(sender, e);            
        }
        bool FiltersEnabled = false;
        void ToggleFiltersEnabled()
        {
            FiltersEnabled = !FiltersEnabled;
            if (FiltersEnabled)
            {
                grid.Height -= gbFilters.Height + 10;
                gbFilters.Show();
            }
            else
            {
                gbFilters.Hide();
                ClearValues(flFilters.Controls);
                grid.Height += gbFilters.Height + 10;
            }
        }
        private void btnFiltersEnable_Click(object sender, EventArgs e)
        {
            ToggleFiltersEnabled();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {           
            addingNewRow = true;
            ClearValues(flEditBox.Controls);
            beginEditing();
        }
        void ClearValues(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                var type = c.GetType();
                if (type == typeof(MyTB))
                    (c as MyTB).Text = "";
                else
                    if (type == typeof(MyCB))
                        (c as MyCB).SelectedIndex = -1;
                        //(c as MyCB).SelectedItem = null;
            }
        }

        private void CatalogEditorForm_Load(object sender, EventArgs e)
        {
            ClearValues(flFilters.Controls);
        }
    }


    public delegate void TextChangedCustomEventTB(TextBox textbox);
    public delegate void TextChangedCustomEventCB(ComboBox combobox);

    public class MyCB : ComboBox
    {
        TextChangedCustomEventCB textChanged;
        public MyCB(object source) :
            base()
        {
            DataSource = source;
            SelectedItem = null;
            SelectedIndex = -1;
        }
        public MyCB(object source, TextChangedCustomEventCB cb) :
            base()
        {
            DataSource = source;
            SelectedItem = null;
            SelectedIndex = -1;
            textChanged = cb;
        }
        protected override void OnTextChanged(EventArgs e)
        {
            if (textChanged != null)
                textChanged(this);
            base.OnTextChanged(e);
        }
    }    
    public class MyTB : TextBox
    {
        TextChangedCustomEventTB textChanged;
        public MyTB() :
            base()
        {
        }
        public MyTB(TextChangedCustomEventTB tc) :
            base()
        {
            textChanged = tc;
        }
        protected override void OnTextChanged(EventArgs e)
        {
            if (textChanged != null)
                textChanged(this);
            base.OnTextChanged(e);
        }
    }
}
