using System;
using System.Windows.Forms;
using SwarthyComponents.FireBird;

namespace TimeSheetManger
{
    public partial class AddUserToFavoriteForm : Form
    {
        MainForm mainform;
        PersonalListForm pl;
        public UserPDP UPDP { get; set; }
        public AddUserToFavoriteForm(MainForm mainForm, PersonalListForm personList)
        {
            InitializeComponent();
            this.mainform = mainForm;
            pl = personList;
        }
        
        private void btnSelectPersonal_Click(object sender, EventArgs e)
        {
            if (cbPost.SelectedIndex == -1 || cbPersonal.SelectedIndex == -1)
                return;
            UPDP = new UserPDP();
            UPDP.User = mainform.currentUser;
            UPDP.Department = mainform.currentTimeSheet.Department;
            UPDP.Personal = cbPersonal.SelectedItem as Personal;
            UPDP.Post = cbPost.SelectedItem as Post;
            var search = UserPDP.Find<UserPDP>(new { user_id = UPDP.User.ID, department_number = UPDP.Department.Department_Number, post_code = UPDP.Post.Code, personal_tn = UPDP.Personal.Table_Number });
            if (search != null)
            {
                MessageBox.Show("Такое сочетание сотрудника и должности уже существует");
                return;
            }
            this.DialogResult = DialogResult.OK;
            Close();
        }
        DBList<Personal> AllPersonal, DepartmentPersonal;
        private void AddUserToFavoriteForm_Load(object sender, EventArgs e)
        {            
            lbDepartmentName.Text = mainform.currentTimeSheet.Department.Name;
            cbPersonal.DataSource = mainform.currentTimeSheet.Department.PersonalOfDepartment;
            cbPost.DataSource = Post.All<Post>();
            AllPersonal = Personal.GetPersonalOfLPU(mainform.currentLPU.ID);
            DepartmentPersonal = mainform.currentTimeSheet.Department.PersonalOfDepartment;
        }

        private void cbShowAll_CheckedChanged(object sender, EventArgs e)
        {
            cbPersonal.DataSource = cbShowAll.Checked ? AllPersonal : DepartmentPersonal;
        }

        private void cbPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPost.SelectedItem = cbPersonal.SelectedItem == null ? (cbPersonal.SelectedItem as Personal).MainPost : null;
        }
    }
}
