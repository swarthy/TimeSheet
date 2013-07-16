namespace TimeSheet
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pLPUSelection = new System.Windows.Forms.Panel();
            this.btnLPUChoiceEnter = new System.Windows.Forms.Button();
            this.lbChoiceLPU = new System.Windows.Forms.Label();
            this.cbLPUList = new System.Windows.Forms.ComboBox();
            this.pAuth = new System.Windows.Forms.Panel();
            this.btnLPUSelect = new System.Windows.Forms.Button();
            this.tbAuthPass = new System.Windows.Forms.TextBox();
            this.tbAuthLogin = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.lbLogin = new System.Windows.Forms.Label();
            this.btnLoginEnter = new System.Windows.Forms.Button();
            this.pWorkspace = new System.Windows.Forms.Panel();
            this.btnAdminPanel = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pTimeSheetEditor = new System.Windows.Forms.Panel();
            this.pDepartment = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCurrentDepartment = new System.Windows.Forms.Label();
            this.tbCurrentDepartmentManager = new System.Windows.Forms.TextBox();
            this.tbCurrentDepartment = new System.Windows.Forms.TextBox();
            this.dgTimeSheet = new System.Windows.Forms.DataGridView();
            this.cFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNewRow = new System.Windows.Forms.Button();
            this.btnTimeSheetList = new System.Windows.Forms.Button();
            this.postBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timeSheetContentBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.timeSheetContentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.pLPUSelection.SuspendLayout();
            this.pAuth.SuspendLayout();
            this.pWorkspace.SuspendLayout();
            this.pTimeSheetEditor.SuspendLayout();
            this.pDepartment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.postBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSheetContentBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSheetContentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pLPUSelection
            // 
            this.pLPUSelection.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pLPUSelection.Controls.Add(this.btnLPUChoiceEnter);
            this.pLPUSelection.Controls.Add(this.lbChoiceLPU);
            this.pLPUSelection.Controls.Add(this.cbLPUList);
            this.pLPUSelection.Location = new System.Drawing.Point(208, 189);
            this.pLPUSelection.Name = "pLPUSelection";
            this.pLPUSelection.Size = new System.Drawing.Size(400, 26);
            this.pLPUSelection.TabIndex = 0;
            // 
            // btnLPUChoiceEnter
            // 
            this.btnLPUChoiceEnter.Location = new System.Drawing.Point(318, 1);
            this.btnLPUChoiceEnter.Name = "btnLPUChoiceEnter";
            this.btnLPUChoiceEnter.Size = new System.Drawing.Size(75, 23);
            this.btnLPUChoiceEnter.TabIndex = 2;
            this.btnLPUChoiceEnter.Text = "Ок";
            this.btnLPUChoiceEnter.UseVisualStyleBackColor = true;
            this.btnLPUChoiceEnter.Click += new System.EventHandler(this.btnLPUChoiceEnter_Click);
            // 
            // lbChoiceLPU
            // 
            this.lbChoiceLPU.AutoSize = true;
            this.lbChoiceLPU.Location = new System.Drawing.Point(5, 6);
            this.lbChoiceLPU.Name = "lbChoiceLPU";
            this.lbChoiceLPU.Size = new System.Drawing.Size(87, 13);
            this.lbChoiceLPU.TabIndex = 1;
            this.lbChoiceLPU.Text = "Выберите ЛПУ:";
            // 
            // cbLPUList
            // 
            this.cbLPUList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLPUList.FormattingEnabled = true;
            this.cbLPUList.Location = new System.Drawing.Point(98, 3);
            this.cbLPUList.Name = "cbLPUList";
            this.cbLPUList.Size = new System.Drawing.Size(214, 21);
            this.cbLPUList.TabIndex = 0;
            // 
            // pAuth
            // 
            this.pAuth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pAuth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pAuth.Controls.Add(this.btnLPUSelect);
            this.pAuth.Controls.Add(this.tbAuthPass);
            this.pAuth.Controls.Add(this.tbAuthLogin);
            this.pAuth.Controls.Add(this.lbPassword);
            this.pAuth.Controls.Add(this.lbLogin);
            this.pAuth.Controls.Add(this.btnLoginEnter);
            this.pAuth.Location = new System.Drawing.Point(306, 154);
            this.pAuth.Name = "pAuth";
            this.pAuth.Size = new System.Drawing.Size(209, 101);
            this.pAuth.TabIndex = 1;
            // 
            // btnLPUSelect
            // 
            this.btnLPUSelect.Location = new System.Drawing.Point(15, 61);
            this.btnLPUSelect.Name = "btnLPUSelect";
            this.btnLPUSelect.Size = new System.Drawing.Size(96, 23);
            this.btnLPUSelect.TabIndex = 5;
            this.btnLPUSelect.Text = "Выбор ЛПУ";
            this.btnLPUSelect.UseVisualStyleBackColor = true;
            this.btnLPUSelect.Click += new System.EventHandler(this.btnLPUSelect_Click);
            // 
            // tbAuthPass
            // 
            this.tbAuthPass.Location = new System.Drawing.Point(80, 35);
            this.tbAuthPass.Name = "tbAuthPass";
            this.tbAuthPass.PasswordChar = '*';
            this.tbAuthPass.Size = new System.Drawing.Size(112, 20);
            this.tbAuthPass.TabIndex = 4;
            this.tbAuthPass.Text = "admin";
            // 
            // tbAuthLogin
            // 
            this.tbAuthLogin.Location = new System.Drawing.Point(80, 9);
            this.tbAuthLogin.Name = "tbAuthLogin";
            this.tbAuthLogin.Size = new System.Drawing.Size(112, 20);
            this.tbAuthLogin.TabIndex = 3;
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(12, 38);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(48, 13);
            this.lbPassword.TabIndex = 2;
            this.lbPassword.Text = "Пароль:";
            // 
            // lbLogin
            // 
            this.lbLogin.AutoSize = true;
            this.lbLogin.Location = new System.Drawing.Point(12, 12);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(41, 13);
            this.lbLogin.TabIndex = 1;
            this.lbLogin.Text = "Логин:";
            // 
            // btnLoginEnter
            // 
            this.btnLoginEnter.Location = new System.Drawing.Point(117, 61);
            this.btnLoginEnter.Name = "btnLoginEnter";
            this.btnLoginEnter.Size = new System.Drawing.Size(75, 23);
            this.btnLoginEnter.TabIndex = 0;
            this.btnLoginEnter.Text = "Вход";
            this.btnLoginEnter.UseVisualStyleBackColor = true;
            this.btnLoginEnter.Click += new System.EventHandler(this.btnLoginEnter_Click);
            // 
            // pWorkspace
            // 
            this.pWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pWorkspace.Controls.Add(this.button1);
            this.pWorkspace.Controls.Add(this.btnAdminPanel);
            this.pWorkspace.Controls.Add(this.btnLogout);
            this.pWorkspace.Controls.Add(this.pTimeSheetEditor);
            this.pWorkspace.Controls.Add(this.btnTimeSheetList);
            this.pWorkspace.Location = new System.Drawing.Point(12, 12);
            this.pWorkspace.Name = "pWorkspace";
            this.pWorkspace.Size = new System.Drawing.Size(792, 413);
            this.pWorkspace.TabIndex = 2;
            // 
            // btnAdminPanel
            // 
            this.btnAdminPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminPanel.Enabled = false;
            this.btnAdminPanel.Location = new System.Drawing.Point(546, 3);
            this.btnAdminPanel.Name = "btnAdminPanel";
            this.btnAdminPanel.Size = new System.Drawing.Size(159, 23);
            this.btnAdminPanel.TabIndex = 6;
            this.btnAdminPanel.Text = "Панель администратора";
            this.btnAdminPanel.UseVisualStyleBackColor = true;
            this.btnAdminPanel.Visible = false;
            this.btnAdminPanel.Click += new System.EventHandler(this.btnAdminPanel_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Location = new System.Drawing.Point(711, 3);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Выход";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // pTimeSheetEditor
            // 
            this.pTimeSheetEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pTimeSheetEditor.Controls.Add(this.pDepartment);
            this.pTimeSheetEditor.Controls.Add(this.dgTimeSheet);
            this.pTimeSheetEditor.Controls.Add(this.btnNewRow);
            this.pTimeSheetEditor.Location = new System.Drawing.Point(3, 32);
            this.pTimeSheetEditor.Name = "pTimeSheetEditor";
            this.pTimeSheetEditor.Size = new System.Drawing.Size(786, 349);
            this.pTimeSheetEditor.TabIndex = 4;
            this.pTimeSheetEditor.Visible = false;
            // 
            // pDepartment
            // 
            this.pDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pDepartment.Controls.Add(this.label1);
            this.pDepartment.Controls.Add(this.lbCurrentDepartment);
            this.pDepartment.Controls.Add(this.tbCurrentDepartmentManager);
            this.pDepartment.Controls.Add(this.tbCurrentDepartment);
            this.pDepartment.Location = new System.Drawing.Point(441, 273);
            this.pDepartment.Name = "pDepartment";
            this.pDepartment.Size = new System.Drawing.Size(342, 73);
            this.pDepartment.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Заведующий отделением";
            // 
            // lbCurrentDepartment
            // 
            this.lbCurrentDepartment.AutoSize = true;
            this.lbCurrentDepartment.Location = new System.Drawing.Point(8, 7);
            this.lbCurrentDepartment.Name = "lbCurrentDepartment";
            this.lbCurrentDepartment.Size = new System.Drawing.Size(65, 13);
            this.lbCurrentDepartment.TabIndex = 1;
            this.lbCurrentDepartment.Text = "Отделение:";
            // 
            // tbCurrentDepartmentManager
            // 
            this.tbCurrentDepartmentManager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCurrentDepartmentManager.Location = new System.Drawing.Point(150, 30);
            this.tbCurrentDepartmentManager.Name = "tbCurrentDepartmentManager";
            this.tbCurrentDepartmentManager.ReadOnly = true;
            this.tbCurrentDepartmentManager.Size = new System.Drawing.Size(189, 20);
            this.tbCurrentDepartmentManager.TabIndex = 0;
            // 
            // tbCurrentDepartment
            // 
            this.tbCurrentDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCurrentDepartment.Location = new System.Drawing.Point(150, 4);
            this.tbCurrentDepartment.Name = "tbCurrentDepartment";
            this.tbCurrentDepartment.ReadOnly = true;
            this.tbCurrentDepartment.Size = new System.Drawing.Size(189, 20);
            this.tbCurrentDepartment.TabIndex = 0;
            // 
            // dgTimeSheet
            // 
            this.dgTimeSheet.AllowUserToOrderColumns = true;
            this.dgTimeSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTimeSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTimeSheet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cFIO,
            this.cPost,
            this.cRate});
            this.dgTimeSheet.Location = new System.Drawing.Point(3, 3);
            this.dgTimeSheet.Name = "dgTimeSheet";
            this.dgTimeSheet.Size = new System.Drawing.Size(780, 251);
            this.dgTimeSheet.TabIndex = 2;
            this.dgTimeSheet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgTimeSheet_KeyDown);
            // 
            // cFIO
            // 
            this.cFIO.HeaderText = "ФИО";
            this.cFIO.Name = "cFIO";
            this.cFIO.ReadOnly = true;
            this.cFIO.Width = 59;
            // 
            // cPost
            // 
            this.cPost.HeaderText = "Должность";
            this.cPost.Name = "cPost";
            this.cPost.ReadOnly = true;
            this.cPost.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cPost.Width = 90;
            // 
            // cRate
            // 
            this.cRate.HeaderText = "Ставка";
            this.cRate.Name = "cRate";
            this.cRate.Width = 68;
            // 
            // btnNewRow
            // 
            this.btnNewRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewRow.Location = new System.Drawing.Point(3, 260);
            this.btnNewRow.Name = "btnNewRow";
            this.btnNewRow.Size = new System.Drawing.Size(144, 23);
            this.btnNewRow.TabIndex = 3;
            this.btnNewRow.Text = "Добавить запись";
            this.btnNewRow.UseVisualStyleBackColor = true;            
            // 
            // btnTimeSheetList
            // 
            this.btnTimeSheetList.Location = new System.Drawing.Point(3, 3);
            this.btnTimeSheetList.Name = "btnTimeSheetList";
            this.btnTimeSheetList.Size = new System.Drawing.Size(86, 23);
            this.btnTimeSheetList.TabIndex = 1;
            this.btnTimeSheetList.Text = "Табели";
            this.btnTimeSheetList.UseVisualStyleBackColor = true;
            this.btnTimeSheetList.Click += new System.EventHandler(this.btnTimeSheetList_Click);
            // 
            // postBindingSource
            // 
            this.postBindingSource.DataSource = typeof(TimeSheet.Post);
            // 
            // timeSheetContentBindingSource1
            // 
            this.timeSheetContentBindingSource1.DataSource = typeof(TimeSheet.TimeSheet_Content);
            // 
            // timeSheetContentBindingSource
            // 
            this.timeSheetContentBindingSource.DataSource = typeof(TimeSheet.TimeSheet_Content);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 437);
            this.Controls.Add(this.pWorkspace);
            this.Controls.Add(this.pAuth);
            this.Controls.Add(this.pLPUSelection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "TimeSheet Manager v 1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pLPUSelection.ResumeLayout(false);
            this.pLPUSelection.PerformLayout();
            this.pAuth.ResumeLayout(false);
            this.pAuth.PerformLayout();
            this.pWorkspace.ResumeLayout(false);
            this.pTimeSheetEditor.ResumeLayout(false);
            this.pDepartment.ResumeLayout(false);
            this.pDepartment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.postBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSheetContentBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeSheetContentBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pLPUSelection;
        private System.Windows.Forms.Button btnLPUChoiceEnter;
        private System.Windows.Forms.Label lbChoiceLPU;
        private System.Windows.Forms.ComboBox cbLPUList;
        private System.Windows.Forms.Panel pAuth;
        private System.Windows.Forms.TextBox tbAuthPass;
        private System.Windows.Forms.TextBox tbAuthLogin;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.Button btnLoginEnter;
        private System.Windows.Forms.Button btnLPUSelect;
        private System.Windows.Forms.Panel pWorkspace;
        private System.Windows.Forms.Button btnTimeSheetList;
        private System.Windows.Forms.DataGridView dgTimeSheet;
        private System.Windows.Forms.BindingSource timeSheetContentBindingSource;
        private System.Windows.Forms.BindingSource timeSheetContentBindingSource1;
        private System.Windows.Forms.Button btnNewRow;
        private System.Windows.Forms.Panel pTimeSheetEditor;
        private System.Windows.Forms.BindingSource postBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn cFIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPost;
        private System.Windows.Forms.DataGridViewTextBoxColumn cRate;
        private System.Windows.Forms.Panel pDepartment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCurrentDepartment;
        private System.Windows.Forms.TextBox tbCurrentDepartmentManager;
        private System.Windows.Forms.TextBox tbCurrentDepartment;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnAdminPanel;
        private System.Windows.Forms.Button button1;



    }
}

