using System;
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
            flEditBox.Controls.Add(tb);
            tb.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {                    
                    grid.Focus();
                    e.Handled = true;
                    //здесь можно будет обработать состояние: добавление/изменение и вызвать нужное событие
                    CompleteEnteringValues(this, e);
                }

            };
        }
        bool addingNewRow = false;
        void CompleteEnteringValues(object sender, EventArgs e)
        {
            if (addingNewRow && AddingComplete != null)
                AddingComplete(sender, e);
            else
                if (EditingComplete != null)
                    EditingComplete(sender, e);
            gbValues.Hide();
        }
        public CatalogEditorForm(MainForm mainform, string catalogTitle)
        {
            mainForm = mainform;
            InitializeComponent();
            grid.AutoGenerateColumns = false;
            Text = string.Format("Справочник: {0}", catalogTitle);            
        }
        public void MyInit()
        {
            Button btnSave = new Button();
            btnSave.Text = "Сохранить";
            btnSave.Click += CompleteEnteringValues;
            flEditBox.Controls.Add(btnSave);

            Button btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Click += (s, e) => { gbValues.Hide(); addingNewRow = false; };
            flEditBox.Controls.Add(btnCancel);
        }
        public void OpenUsers()
        {
            users = mainForm.currentLPU.Users;            
            BindingListView<User> view = new BindingListView<User>(users);
            
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

            grid.RowEditStarted +=
            delegate
            {
                gbValues.Show();
                var usr = (bs.Current as ObjectView<User>).Object;
                loginETB.Text = usr.Login;
                passETB.Text = usr.Pass;
                lpuEB.SelectedItem = usr.LPU;
                profileEB.SelectedItem = usr.Profile;
                roleETB.Text = usr.Role.ToString();
                flEditBox.Controls[0].Focus();
            };

            EditingComplete += (s, e) => {
                var usr = (bs.Current as ObjectView<User>).Object;
                usr.Login = loginETB.Text;
                usr.Pass = passETB.Text;
                usr.LPU = lpuEB.SelectedItem as LPU;
                usr.Profile = profileEB.SelectedItem as Personal;
                int tempRole;
                if (!int.TryParse(roleETB.Text, out tempRole))
                {
                    MessageBox.Show("Роль введена неверно", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                usr.Role = tempRole;
                usr.Save();
                grid.Refresh();
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
                        return;
                    }
                    usr.Role = tempRole;
                    usr.Save();
                    users.Add(usr);
                    grid.Refresh();
                };

            bs.DataSource = view;
            grid.DataSource = bs;            
        }

        public void OpenPersonals()
        {
            personals = LPUPersonals;
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


            DataGridViewTextBoxColumn priority = new DataGridViewTextBoxColumn();
            priority.DataPropertyName = "Priority";
            priority.HeaderText = "Приоритет";
            grid.Columns.Add(priority);
            

            personals.Sort((x, y) => x._FullName.CompareTo(y._FullName));

            //view = new BindingListView<Domain>(personals);            
            //grid.DataSource = view;
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
            maindoc.DisplayMember = "_ShortNameAndNumber";
            maindoc.HeaderText = "Главный врач";
            maindoc.DataSource = Personal.All<Personal>();
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
            /*
            if (MessageBox.Show("Вы уверены, что хотите сохранить изменения?", "Подтверждение сохранения", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;
            foreach (var item in bs)
            {
                (item as Domain).Save();
            } */           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (RowDeleting != null)
                RowDeleting(sender, e);
            //if (MessageBox.Show("Вы действительно хотите удалить запись?\r\nЭто может повлечь за собой необратимое нарушение целостности данных.","Подтверждение удаления",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.Yes)
                //bs.Remove(bs.Current as Domain, true);
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
                foreach (Control c in flFilters.Controls)
                {
                    var type = c.GetType();
                    if (type == typeof(MyTB))                    
                        (c as MyTB).Text = "";                    
                    else
                        if (type == typeof(MyCB))                        
                            (c as MyCB).SelectedItem = null;
                }
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
            foreach (Control c in flFilters.Controls)
            {
                var type = c.GetType();
                if (type == typeof(MyTB))
                    (c as MyTB).Text = "";
                else
                    if (type == typeof(MyCB))
                        (c as MyCB).SelectedItem = null;
            }
            gbValues.Show();
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
        }
        public MyCB(object source, TextChangedCustomEventCB cb) :
            base()
        {
            DataSource = source;
            SelectedItem = null;
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
