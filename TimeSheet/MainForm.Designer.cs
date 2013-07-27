namespace TimeSheetManger
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
            this.lbAuthSelectedLPU = new System.Windows.Forms.Label();
            this.btnLPUSelect = new System.Windows.Forms.Button();
            this.tbAuthPass = new System.Windows.Forms.TextBox();
            this.tbAuthLogin = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.lbLogin = new System.Windows.Forms.Label();
            this.btnLoginEnter = new System.Windows.Forms.Button();
            this.pWorkspace = new System.Windows.Forms.Panel();
            this.lbCurrentTimeSheetName = new System.Windows.Forms.Label();
            this.btnAdminPanel = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.pTimeSheetEditor = new System.Windows.Forms.Panel();
            this.pColors = new System.Windows.Forms.Panel();
            this.lbweekend = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cpWeekEnd = new System.Windows.Forms.Panel();
            this.cpShortDay = new System.Windows.Forms.Panel();
            this.cpHolyday = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.pDepartment = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCurrentDepartment = new System.Windows.Forms.Label();
            this.tbCurrentDepartmentManager = new System.Windows.Forms.TextBox();
            this.tbCurrentDepartment = new System.Windows.Forms.TextBox();
            this.dgTimeSheet = new TimeSheetManger.MyDataGridView();
            this.cFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsDaysMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.редактироватьВыделеннуюЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddMore = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьВыделеннуюЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miEditPersonal = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьЗаписиЭтогоСотрудникаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewRow = new System.Windows.Forms.Button();
            this.btnTimeSheetList = new System.Windows.Forms.Button();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslStatusLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatusRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.cdDayColors = new System.Windows.Forms.ColorDialog();
            this.pLPUSelection.SuspendLayout();
            this.pAuth.SuspendLayout();
            this.pWorkspace.SuspendLayout();
            this.pTimeSheetEditor.SuspendLayout();
            this.pColors.SuspendLayout();
            this.pDepartment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeSheet)).BeginInit();
            this.cmsDaysMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pLPUSelection
            // 
            this.pLPUSelection.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pLPUSelection.Controls.Add(this.btnLPUChoiceEnter);
            this.pLPUSelection.Controls.Add(this.lbChoiceLPU);
            this.pLPUSelection.Controls.Add(this.cbLPUList);
            this.pLPUSelection.Location = new System.Drawing.Point(147, 189);
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
            this.pAuth.Controls.Add(this.lbAuthSelectedLPU);
            this.pAuth.Controls.Add(this.btnLPUSelect);
            this.pAuth.Controls.Add(this.tbAuthPass);
            this.pAuth.Controls.Add(this.tbAuthLogin);
            this.pAuth.Controls.Add(this.lbPassword);
            this.pAuth.Controls.Add(this.lbLogin);
            this.pAuth.Controls.Add(this.btnLoginEnter);
            this.pAuth.Location = new System.Drawing.Point(245, 144);
            this.pAuth.Name = "pAuth";
            this.pAuth.Size = new System.Drawing.Size(214, 121);
            this.pAuth.TabIndex = 1;
            // 
            // lbAuthSelectedLPU
            // 
            this.lbAuthSelectedLPU.AutoSize = true;
            this.lbAuthSelectedLPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbAuthSelectedLPU.Location = new System.Drawing.Point(12, 11);
            this.lbAuthSelectedLPU.Name = "lbAuthSelectedLPU";
            this.lbAuthSelectedLPU.Size = new System.Drawing.Size(158, 13);
            this.lbAuthSelectedLPU.TabIndex = 6;
            this.lbAuthSelectedLPU.Text = "ГБУЗ Саракташская ЦРБ";
            // 
            // btnLPUSelect
            // 
            this.btnLPUSelect.Location = new System.Drawing.Point(15, 83);
            this.btnLPUSelect.Name = "btnLPUSelect";
            this.btnLPUSelect.Size = new System.Drawing.Size(84, 23);
            this.btnLPUSelect.TabIndex = 5;
            this.btnLPUSelect.Text = "Выход";
            this.btnLPUSelect.UseVisualStyleBackColor = true;
            this.btnLPUSelect.Click += new System.EventHandler(this.btnLPUSelect_Click);
            // 
            // tbAuthPass
            // 
            this.tbAuthPass.Location = new System.Drawing.Point(66, 57);
            this.tbAuthPass.Name = "tbAuthPass";
            this.tbAuthPass.PasswordChar = '*';
            this.tbAuthPass.Size = new System.Drawing.Size(126, 20);
            this.tbAuthPass.TabIndex = 4;
            this.tbAuthPass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbAuthPass_KeyDown);
            // 
            // tbAuthLogin
            // 
            this.tbAuthLogin.Location = new System.Drawing.Point(66, 31);
            this.tbAuthLogin.Name = "tbAuthLogin";
            this.tbAuthLogin.Size = new System.Drawing.Size(126, 20);
            this.tbAuthLogin.TabIndex = 3;
            this.tbAuthLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbAuthPass_KeyDown);
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(12, 60);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(48, 13);
            this.lbPassword.TabIndex = 2;
            this.lbPassword.Text = "Пароль:";
            // 
            // lbLogin
            // 
            this.lbLogin.AutoSize = true;
            this.lbLogin.Location = new System.Drawing.Point(12, 34);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(41, 13);
            this.lbLogin.TabIndex = 1;
            this.lbLogin.Text = "Логин:";
            // 
            // btnLoginEnter
            // 
            this.btnLoginEnter.Location = new System.Drawing.Point(108, 83);
            this.btnLoginEnter.Name = "btnLoginEnter";
            this.btnLoginEnter.Size = new System.Drawing.Size(84, 23);
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
            this.pWorkspace.Controls.Add(this.lbCurrentTimeSheetName);
            this.pWorkspace.Controls.Add(this.btnAdminPanel);
            this.pWorkspace.Controls.Add(this.btnLogout);
            this.pWorkspace.Controls.Add(this.pTimeSheetEditor);
            this.pWorkspace.Controls.Add(this.btnTimeSheetList);
            this.pWorkspace.Location = new System.Drawing.Point(12, 12);
            this.pWorkspace.Name = "pWorkspace";
            this.pWorkspace.Size = new System.Drawing.Size(670, 400);
            this.pWorkspace.TabIndex = 2;
            // 
            // lbCurrentTimeSheetName
            // 
            this.lbCurrentTimeSheetName.AutoSize = true;
            this.lbCurrentTimeSheetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbCurrentTimeSheetName.Location = new System.Drawing.Point(95, 6);
            this.lbCurrentTimeSheetName.Name = "lbCurrentTimeSheetName";
            this.lbCurrentTimeSheetName.Size = new System.Drawing.Size(0, 17);
            this.lbCurrentTimeSheetName.TabIndex = 7;
            // 
            // btnAdminPanel
            // 
            this.btnAdminPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminPanel.Enabled = false;
            this.btnAdminPanel.Location = new System.Drawing.Point(424, 3);
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
            this.btnLogout.Location = new System.Drawing.Point(589, 3);
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
            this.pTimeSheetEditor.Controls.Add(this.pColors);
            this.pTimeSheetEditor.Controls.Add(this.btnExportToExcel);
            this.pTimeSheetEditor.Controls.Add(this.pDepartment);
            this.pTimeSheetEditor.Controls.Add(this.dgTimeSheet);
            this.pTimeSheetEditor.Controls.Add(this.btnNewRow);
            this.pTimeSheetEditor.Location = new System.Drawing.Point(3, 32);
            this.pTimeSheetEditor.Name = "pTimeSheetEditor";
            this.pTimeSheetEditor.Size = new System.Drawing.Size(664, 365);
            this.pTimeSheetEditor.TabIndex = 4;
            this.pTimeSheetEditor.Visible = false;
            // 
            // pColors
            // 
            this.pColors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pColors.Controls.Add(this.lbweekend);
            this.pColors.Controls.Add(this.label3);
            this.pColors.Controls.Add(this.cpWeekEnd);
            this.pColors.Controls.Add(this.cpShortDay);
            this.pColors.Controls.Add(this.cpHolyday);
            this.pColors.Controls.Add(this.label2);
            this.pColors.Location = new System.Drawing.Point(3, 302);
            this.pColors.Name = "pColors";
            this.pColors.Size = new System.Drawing.Size(158, 59);
            this.pColors.TabIndex = 8;
            // 
            // lbweekend
            // 
            this.lbweekend.AutoSize = true;
            this.lbweekend.Location = new System.Drawing.Point(3, 0);
            this.lbweekend.Name = "lbweekend";
            this.lbweekend.Size = new System.Drawing.Size(89, 13);
            this.lbweekend.TabIndex = 7;
            this.lbweekend.Text = "Выходные дни - ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Сокращенные дни - ";
            // 
            // cpWeekEnd
            // 
            this.cpWeekEnd.Location = new System.Drawing.Point(115, 0);
            this.cpWeekEnd.Name = "cpWeekEnd";
            this.cpWeekEnd.Size = new System.Drawing.Size(32, 13);
            this.cpWeekEnd.TabIndex = 6;
            this.cpWeekEnd.DoubleClick += new System.EventHandler(this.cpWeekEnd_DoubleClick);
            // 
            // cpShortDay
            // 
            this.cpShortDay.Location = new System.Drawing.Point(115, 38);
            this.cpShortDay.Name = "cpShortDay";
            this.cpShortDay.Size = new System.Drawing.Size(32, 13);
            this.cpShortDay.TabIndex = 6;
            this.cpShortDay.DoubleClick += new System.EventHandler(this.cpShortDay_DoubleClick);
            // 
            // cpHolyday
            // 
            this.cpHolyday.Location = new System.Drawing.Point(115, 19);
            this.cpHolyday.Name = "cpHolyday";
            this.cpHolyday.Size = new System.Drawing.Size(32, 13);
            this.cpHolyday.TabIndex = 6;
            this.cpHolyday.DoubleClick += new System.EventHandler(this.cpHolyday_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Праздничные дни - ";
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportToExcel.Location = new System.Drawing.Point(153, 273);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(132, 23);
            this.btnExportToExcel.TabIndex = 5;
            this.btnExportToExcel.Text = "Экспорт в Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // pDepartment
            // 
            this.pDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pDepartment.Controls.Add(this.label1);
            this.pDepartment.Controls.Add(this.lbCurrentDepartment);
            this.pDepartment.Controls.Add(this.tbCurrentDepartmentManager);
            this.pDepartment.Controls.Add(this.tbCurrentDepartment);
            this.pDepartment.Location = new System.Drawing.Point(297, 273);
            this.pDepartment.Name = "pDepartment";
            this.pDepartment.Size = new System.Drawing.Size(364, 56);
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
            this.tbCurrentDepartmentManager.Size = new System.Drawing.Size(211, 20);
            this.tbCurrentDepartmentManager.TabIndex = 0;
            // 
            // tbCurrentDepartment
            // 
            this.tbCurrentDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbCurrentDepartment.Location = new System.Drawing.Point(150, 4);
            this.tbCurrentDepartment.Name = "tbCurrentDepartment";
            this.tbCurrentDepartment.ReadOnly = true;
            this.tbCurrentDepartment.Size = new System.Drawing.Size(211, 20);
            this.tbCurrentDepartment.TabIndex = 0;
            // 
            // dgTimeSheet
            // 
            this.dgTimeSheet.AllowUserToAddRows = false;
            this.dgTimeSheet.AllowUserToDeleteRows = false;
            this.dgTimeSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTimeSheet.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgTimeSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTimeSheet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cFIO,
            this.cPost,
            this.cRate});
            this.dgTimeSheet.ContextMenuStrip = this.cmsDaysMenu;
            this.dgTimeSheet.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgTimeSheet.Location = new System.Drawing.Point(3, 3);
            this.dgTimeSheet.Name = "dgTimeSheet";
            this.dgTimeSheet.ReadOnly = true;
            this.dgTimeSheet.Size = new System.Drawing.Size(658, 264);
            this.dgTimeSheet.TabIndex = 2;
            this.dgTimeSheet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTimeSheet_CellDoubleClick);
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
            this.cRate.ReadOnly = true;
            this.cRate.Width = 68;
            // 
            // cmsDaysMenu
            // 
            this.cmsDaysMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редактироватьВыделеннуюЗаписьToolStripMenuItem,
            this.miAddMore,
            this.удалитьВыделеннуюЗаписьToolStripMenuItem,
            this.toolStripMenuItem1,
            this.miEditPersonal,
            this.удалитьЗаписиЭтогоСотрудникаToolStripMenuItem});
            this.cmsDaysMenu.Name = "cmsDaysMenu";
            this.cmsDaysMenu.Size = new System.Drawing.Size(269, 120);
            // 
            // редактироватьВыделеннуюЗаписьToolStripMenuItem
            // 
            this.редактироватьВыделеннуюЗаписьToolStripMenuItem.Name = "редактироватьВыделеннуюЗаписьToolStripMenuItem";
            this.редактироватьВыделеннуюЗаписьToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.редактироватьВыделеннуюЗаписьToolStripMenuItem.Text = "Редактировать выделенную ячейку";
            this.редактироватьВыделеннуюЗаписьToolStripMenuItem.ToolTipText = "Enter";
            this.редактироватьВыделеннуюЗаписьToolStripMenuItem.Click += new System.EventHandler(this.редактироватьВыделеннуюЗаписьToolStripMenuItem_Click);
            // 
            // miAddMore
            // 
            this.miAddMore.Name = "miAddMore";
            this.miAddMore.Size = new System.Drawing.Size(268, 22);
            this.miAddMore.Text = "Добавить дополнительную ячейку";
            this.miAddMore.ToolTipText = "Space";
            this.miAddMore.Click += new System.EventHandler(this.miAddMore_Click);
            // 
            // удалитьВыделеннуюЗаписьToolStripMenuItem
            // 
            this.удалитьВыделеннуюЗаписьToolStripMenuItem.Name = "удалитьВыделеннуюЗаписьToolStripMenuItem";
            this.удалитьВыделеннуюЗаписьToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.удалитьВыделеннуюЗаписьToolStripMenuItem.Text = "Удалить выделенную ячейку";
            this.удалитьВыделеннуюЗаписьToolStripMenuItem.ToolTipText = "Delete";
            this.удалитьВыделеннуюЗаписьToolStripMenuItem.Click += new System.EventHandler(this.удалитьВыделеннуюЗаписьToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(265, 6);
            // 
            // miEditPersonal
            // 
            this.miEditPersonal.Name = "miEditPersonal";
            this.miEditPersonal.Size = new System.Drawing.Size(268, 22);
            this.miEditPersonal.Text = "Редактировать данные сотрудника";
            this.miEditPersonal.ToolTipText = "Shift + Enter";
            this.miEditPersonal.Click += new System.EventHandler(this.miEditPersonal_Click);
            // 
            // удалитьЗаписиЭтогоСотрудникаToolStripMenuItem
            // 
            this.удалитьЗаписиЭтогоСотрудникаToolStripMenuItem.Name = "удалитьЗаписиЭтогоСотрудникаToolStripMenuItem";
            this.удалитьЗаписиЭтогоСотрудникаToolStripMenuItem.Size = new System.Drawing.Size(268, 22);
            this.удалитьЗаписиЭтогоСотрудникаToolStripMenuItem.Text = "Удалить запись сотрудника";
            this.удалитьЗаписиЭтогоСотрудникаToolStripMenuItem.ToolTipText = "Shift + Delete";
            this.удалитьЗаписиЭтогоСотрудникаToolStripMenuItem.Click += new System.EventHandler(this.удалитьЗаписиЭтогоСотрудникаToolStripMenuItem_Click);
            // 
            // btnNewRow
            // 
            this.btnNewRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewRow.Location = new System.Drawing.Point(3, 273);
            this.btnNewRow.Name = "btnNewRow";
            this.btnNewRow.Size = new System.Drawing.Size(144, 23);
            this.btnNewRow.TabIndex = 3;
            this.btnNewRow.Text = "Добавить запись";
            this.btnNewRow.UseVisualStyleBackColor = true;
            this.btnNewRow.Click += new System.EventHandler(this.btnNewRow_Click);
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
            // dlgSaveFile
            // 
            this.dlgSaveFile.Filter = "Книга Excel|*.xlsx";
            this.dlgSaveFile.Title = "Экспорт табеля";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatusLeft,
            this.tspbProgress,
            this.tsslSpace,
            this.tsslStatusRight});
            this.statusStrip1.Location = new System.Drawing.Point(0, 415);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(694, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslStatusLeft
            // 
            this.tsslStatusLeft.Name = "tsslStatusLeft";
            this.tsslStatusLeft.Size = new System.Drawing.Size(45, 17);
            this.tsslStatusLeft.Text = "Готово";
            // 
            // tspbProgress
            // 
            this.tspbProgress.Name = "tspbProgress";
            this.tspbProgress.Size = new System.Drawing.Size(100, 16);
            this.tspbProgress.Visible = false;
            // 
            // tsslSpace
            // 
            this.tsslSpace.Name = "tsslSpace";
            this.tsslSpace.Size = new System.Drawing.Size(634, 17);
            this.tsslSpace.Spring = true;
            // 
            // tsslStatusRight
            // 
            this.tsslStatusRight.Name = "tsslStatusRight";
            this.tsslStatusRight.Size = new System.Drawing.Size(0, 17);
            // 
            // cdDayColors
            // 
            this.cdDayColors.AnyColor = true;
            this.cdDayColors.FullOpen = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 437);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pWorkspace);
            this.Controls.Add(this.pAuth);
            this.Controls.Add(this.pLPUSelection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(710, 475);
            this.Name = "MainForm";
            this.Text = "Учет использования рабочего времени";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pLPUSelection.ResumeLayout(false);
            this.pLPUSelection.PerformLayout();
            this.pAuth.ResumeLayout(false);
            this.pAuth.PerformLayout();
            this.pWorkspace.ResumeLayout(false);
            this.pWorkspace.PerformLayout();
            this.pTimeSheetEditor.ResumeLayout(false);
            this.pColors.ResumeLayout(false);
            this.pColors.PerformLayout();
            this.pDepartment.ResumeLayout(false);
            this.pDepartment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeSheet)).EndInit();
            this.cmsDaysMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private MyDataGridView dgTimeSheet;        
        private System.Windows.Forms.Button btnNewRow;
        private System.Windows.Forms.Panel pTimeSheetEditor;
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
        private System.Windows.Forms.Label lbCurrentTimeSheetName;
        private System.Windows.Forms.ContextMenuStrip cmsDaysMenu;
        private System.Windows.Forms.ToolStripMenuItem miAddMore;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miEditPersonal;
        private System.Windows.Forms.ToolStripMenuItem удалитьВыделеннуюЗаписьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьЗаписиЭтогоСотрудникаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem редактироватьВыделеннуюЗаписьToolStripMenuItem;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.Label lbAuthSelectedLPU;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusLeft;
        private System.Windows.Forms.ToolStripProgressBar tspbProgress;
        private System.Windows.Forms.ToolStripStatusLabel tsslSpace;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusRight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel cpShortDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel cpHolyday;
        private System.Windows.Forms.Label lbweekend;
        private System.Windows.Forms.Panel cpWeekEnd;
        private System.Windows.Forms.ColorDialog cdDayColors;
        private System.Windows.Forms.Panel pColors;



    }
}

