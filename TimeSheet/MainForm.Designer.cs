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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLPUChoiceEnter = new System.Windows.Forms.Button();
            this.lbChoiceLPU = new System.Windows.Forms.Label();
            this.cbLPUList = new System.Windows.Forms.ComboBox();
            this.pAuth = new System.Windows.Forms.Panel();
            this.lbAuthSelectedLPU = new System.Windows.Forms.Label();
            this.tbAuthPass = new System.Windows.Forms.TextBox();
            this.tbAuthLogin = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.lbLogin = new System.Windows.Forms.Label();
            this.pWorkspace = new System.Windows.Forms.Panel();
            this.lbCurrentTimeSheetName = new System.Windows.Forms.Label();
            this.pTimeSheetEditor = new System.Windows.Forms.Panel();
            this.pDepartment = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCurrentDepartment = new System.Windows.Forms.Label();
            this.tbCurrentDepartmentManager = new System.Windows.Forms.TextBox();
            this.tbCurrentDepartment = new System.Windows.Forms.TextBox();
            this.cmsDaysMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.редактироватьВыделеннуюЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddMore = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьВыделеннуюЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miEditPersonal = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьЗаписиЭтогоСотрудникаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.tsslStatusLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslStatusRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.ttsVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.msMainMenu = new System.Windows.Forms.MenuStrip();
            this.pDesktop = new System.Windows.Forms.Panel();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnNewRow = new System.Windows.Forms.Button();
            this.deskbtnTimeSheets = new System.Windows.Forms.Button();
            this.tssServerConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.пользовательToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCurrentUser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miTimeSheets = new System.Windows.Forms.ToolStripMenuItem();
            this.miAdminPanel = new System.Windows.Forms.ToolStripMenuItem();
            this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLPUSelect = new System.Windows.Forms.Button();
            this.btnLoginEnter = new System.Windows.Forms.Button();
            this.reconnectTimer = new System.Windows.Forms.Timer(this.components);
            this.dgTimeSheet = new TimeSheetManger.MyDataGridView();
            this.cFIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pLPUSelection.SuspendLayout();
            this.pAuth.SuspendLayout();
            this.pWorkspace.SuspendLayout();
            this.pTimeSheetEditor.SuspendLayout();
            this.pDepartment.SuspendLayout();
            this.cmsDaysMenu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.msMainMenu.SuspendLayout();
            this.pDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeSheet)).BeginInit();
            this.SuspendLayout();
            // 
            // pLPUSelection
            // 
            this.pLPUSelection.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pLPUSelection.Controls.Add(this.btnExit);
            this.pLPUSelection.Controls.Add(this.btnLPUChoiceEnter);
            this.pLPUSelection.Controls.Add(this.lbChoiceLPU);
            this.pLPUSelection.Controls.Add(this.cbLPUList);
            this.pLPUSelection.Location = new System.Drawing.Point(147, 189);
            this.pLPUSelection.Name = "pLPUSelection";
            this.pLPUSelection.Size = new System.Drawing.Size(400, 62);
            this.pLPUSelection.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(318, 30);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.pAuth.Location = new System.Drawing.Point(245, 153);
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
            // pWorkspace
            // 
            this.pWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pWorkspace.Controls.Add(this.lbCurrentTimeSheetName);
            this.pWorkspace.Controls.Add(this.pTimeSheetEditor);
            this.pWorkspace.Location = new System.Drawing.Point(12, 27);
            this.pWorkspace.Name = "pWorkspace";
            this.pWorkspace.Size = new System.Drawing.Size(670, 380);
            this.pWorkspace.TabIndex = 2;
            // 
            // lbCurrentTimeSheetName
            // 
            this.lbCurrentTimeSheetName.AutoSize = true;
            this.lbCurrentTimeSheetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbCurrentTimeSheetName.Location = new System.Drawing.Point(6, 3);
            this.lbCurrentTimeSheetName.Name = "lbCurrentTimeSheetName";
            this.lbCurrentTimeSheetName.Size = new System.Drawing.Size(0, 17);
            this.lbCurrentTimeSheetName.TabIndex = 7;
            // 
            // pTimeSheetEditor
            // 
            this.pTimeSheetEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pTimeSheetEditor.Controls.Add(this.btnExportToExcel);
            this.pTimeSheetEditor.Controls.Add(this.pDepartment);
            this.pTimeSheetEditor.Controls.Add(this.dgTimeSheet);
            this.pTimeSheetEditor.Controls.Add(this.btnNewRow);
            this.pTimeSheetEditor.Location = new System.Drawing.Point(6, 23);
            this.pTimeSheetEditor.Name = "pTimeSheetEditor";
            this.pTimeSheetEditor.Size = new System.Drawing.Size(664, 354);
            this.pTimeSheetEditor.TabIndex = 4;
            // 
            // pDepartment
            // 
            this.pDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pDepartment.Controls.Add(this.label1);
            this.pDepartment.Controls.Add(this.lbCurrentDepartment);
            this.pDepartment.Controls.Add(this.tbCurrentDepartmentManager);
            this.pDepartment.Controls.Add(this.tbCurrentDepartment);
            this.pDepartment.Location = new System.Drawing.Point(297, 262);
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
            this.cmsDaysMenu.Opening += new System.ComponentModel.CancelEventHandler(this.cmsDaysMenu_Opening);
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
            // dlgSaveFile
            // 
            this.dlgSaveFile.Filter = "Книга Excel|*.xlsx";
            this.dlgSaveFile.Title = "Экспорт табеля";
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslStatusLeft,
            this.tspbProgress,
            this.tsslSpace,
            this.tsslStatusRight,
            this.tssServerConnection,
            this.ttsVersion});
            this.statusBar.Location = new System.Drawing.Point(0, 415);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(694, 22);
            this.statusBar.TabIndex = 3;
            this.statusBar.Text = "statusStrip1";
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
            this.tspbProgress.Value = 55;
            this.tspbProgress.Visible = false;
            // 
            // tsslSpace
            // 
            this.tsslSpace.Name = "tsslSpace";
            this.tsslSpace.Size = new System.Drawing.Size(525, 17);
            this.tsslSpace.Spring = true;
            // 
            // tsslStatusRight
            // 
            this.tsslStatusRight.Name = "tsslStatusRight";
            this.tsslStatusRight.Size = new System.Drawing.Size(0, 17);
            // 
            // ttsVersion
            // 
            this.ttsVersion.Name = "ttsVersion";
            this.ttsVersion.Size = new System.Drawing.Size(46, 17);
            this.ttsVersion.Text = "Version";
            // 
            // msMainMenu
            // 
            this.msMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.пользовательToolStripMenuItem,
            this.miTimeSheets,
            this.miAdminPanel,
            this.miAbout});
            this.msMainMenu.Location = new System.Drawing.Point(0, 0);
            this.msMainMenu.Name = "msMainMenu";
            this.msMainMenu.Size = new System.Drawing.Size(694, 24);
            this.msMainMenu.TabIndex = 4;
            this.msMainMenu.Text = "menuStrip1";
            // 
            // pDesktop
            // 
            this.pDesktop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pDesktop.Controls.Add(this.deskbtnTimeSheets);
            this.pDesktop.Location = new System.Drawing.Point(12, 27);
            this.pDesktop.Name = "pDesktop";
            this.pDesktop.Size = new System.Drawing.Size(670, 380);
            this.pDesktop.TabIndex = 5;
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportToExcel.Image")));
            this.btnExportToExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportToExcel.Location = new System.Drawing.Point(153, 262);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(132, 23);
            this.btnExportToExcel.TabIndex = 5;
            this.btnExportToExcel.Text = "Экспорт в Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnNewRow
            // 
            this.btnNewRow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewRow.Image = global::TimeSheetManger.Properties.Resources.Add_16x16;
            this.btnNewRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewRow.Location = new System.Drawing.Point(3, 262);
            this.btnNewRow.Name = "btnNewRow";
            this.btnNewRow.Size = new System.Drawing.Size(144, 23);
            this.btnNewRow.TabIndex = 3;
            this.btnNewRow.Text = "Добавить запись";
            this.btnNewRow.UseVisualStyleBackColor = true;
            this.btnNewRow.Click += new System.EventHandler(this.btnNewRow_Click);
            // 
            // deskbtnTimeSheets
            // 
            this.deskbtnTimeSheets.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deskbtnTimeSheets.Image = global::TimeSheetManger.Properties.Resources.Calendar_32x32;
            this.deskbtnTimeSheets.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.deskbtnTimeSheets.Location = new System.Drawing.Point(15, 13);
            this.deskbtnTimeSheets.Name = "deskbtnTimeSheets";
            this.deskbtnTimeSheets.Size = new System.Drawing.Size(140, 40);
            this.deskbtnTimeSheets.TabIndex = 0;
            this.deskbtnTimeSheets.Text = "Табели";
            this.deskbtnTimeSheets.UseVisualStyleBackColor = true;
            this.deskbtnTimeSheets.Click += new System.EventHandler(this.btnTimeSheetList_Click);
            // 
            // tssServerConnection
            // 
            this.tssServerConnection.Image = ((System.Drawing.Image)(resources.GetObject("tssServerConnection.Image")));
            this.tssServerConnection.Name = "tssServerConnection";
            this.tssServerConnection.Size = new System.Drawing.Size(63, 17);
            this.tssServerConnection.Text = "Сервер";
            // 
            // пользовательToolStripMenuItem
            // 
            this.пользовательToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCurrentUser,
            this.toolStripMenuItem2,
            this.miLogout,
            this.miExit});
            this.пользовательToolStripMenuItem.Image = global::TimeSheetManger.Properties.Resources.User_16x16;
            this.пользовательToolStripMenuItem.Name = "пользовательToolStripMenuItem";
            this.пользовательToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.пользовательToolStripMenuItem.Text = "Пользователь";
            // 
            // miCurrentUser
            // 
            this.miCurrentUser.Enabled = false;
            this.miCurrentUser.Name = "miCurrentUser";
            this.miCurrentUser.Size = new System.Drawing.Size(200, 22);
            this.miCurrentUser.Text = "user";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(197, 6);
            // 
            // miLogout
            // 
            this.miLogout.Image = ((System.Drawing.Image)(resources.GetObject("miLogout.Image")));
            this.miLogout.Name = "miLogout";
            this.miLogout.Size = new System.Drawing.Size(200, 22);
            this.miLogout.Text = "Сменить пользователя";
            this.miLogout.Click += new System.EventHandler(this.miLogout_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(200, 22);
            this.miExit.Text = "Выйти из программы";
            this.miExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // miTimeSheets
            // 
            this.miTimeSheets.Image = global::TimeSheetManger.Properties.Resources.Calendar_16x16;
            this.miTimeSheets.Name = "miTimeSheets";
            this.miTimeSheets.Size = new System.Drawing.Size(75, 20);
            this.miTimeSheets.Text = "Табели";
            this.miTimeSheets.Click += new System.EventHandler(this.btnTimeSheetList_Click);
            // 
            // miAdminPanel
            // 
            this.miAdminPanel.Image = global::TimeSheetManger.Properties.Resources.Settings_16x16;
            this.miAdminPanel.Name = "miAdminPanel";
            this.miAdminPanel.Size = new System.Drawing.Size(170, 20);
            this.miAdminPanel.Text = "Панель администратора";
            this.miAdminPanel.Click += new System.EventHandler(this.miAdminPanel_Click);
            // 
            // miAbout
            // 
            this.miAbout.Image = global::TimeSheetManger.Properties.Resources.Information_16x16;
            this.miAbout.Name = "miAbout";
            this.miAbout.Size = new System.Drawing.Size(110, 20);
            this.miAbout.Text = "О программе";
            this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
            // 
            // btnLPUSelect
            // 
            this.btnLPUSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnLPUSelect.Image")));
            this.btnLPUSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLPUSelect.Location = new System.Drawing.Point(15, 83);
            this.btnLPUSelect.Name = "btnLPUSelect";
            this.btnLPUSelect.Size = new System.Drawing.Size(84, 23);
            this.btnLPUSelect.TabIndex = 5;
            this.btnLPUSelect.Text = "Выход";
            this.btnLPUSelect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLPUSelect.UseVisualStyleBackColor = true;
            this.btnLPUSelect.Click += new System.EventHandler(this.btnLPUSelect_Click);
            // 
            // btnLoginEnter
            // 
            this.btnLoginEnter.Image = global::TimeSheetManger.Properties.Resources.Check_16x16;
            this.btnLoginEnter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoginEnter.Location = new System.Drawing.Point(108, 83);
            this.btnLoginEnter.Name = "btnLoginEnter";
            this.btnLoginEnter.Size = new System.Drawing.Size(84, 23);
            this.btnLoginEnter.TabIndex = 0;
            this.btnLoginEnter.Text = "Вход";
            this.btnLoginEnter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoginEnter.UseVisualStyleBackColor = true;
            this.btnLoginEnter.Click += new System.EventHandler(this.btnLoginEnter_Click);
            // 
            // reconnectTimer
            // 
            this.reconnectTimer.Enabled = true;
            this.reconnectTimer.Interval = 60000;
            this.reconnectTimer.Tick += new System.EventHandler(this.reconnectTimer_Tick);
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
            this.dgTimeSheet.Size = new System.Drawing.Size(658, 253);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 437);
            this.Controls.Add(this.pWorkspace);
            this.Controls.Add(this.pDesktop);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.msMainMenu);
            this.Controls.Add(this.pLPUSelection);
            this.Controls.Add(this.pAuth);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMainMenu;
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
            this.pDepartment.ResumeLayout(false);
            this.pDepartment.PerformLayout();
            this.cmsDaysMenu.ResumeLayout(false);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.msMainMenu.ResumeLayout(false);
            this.msMainMenu.PerformLayout();
            this.pDesktop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTimeSheet)).EndInit();
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
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusLeft;
        internal System.Windows.Forms.ToolStripProgressBar tspbProgress;
        private System.Windows.Forms.ToolStripStatusLabel tsslSpace;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatusRight;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.MenuStrip msMainMenu;
        private System.Windows.Forms.ToolStripMenuItem пользовательToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miLogout;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miTimeSheets;
        private System.Windows.Forms.ToolStripMenuItem miAdminPanel;
        private System.Windows.Forms.ToolStripMenuItem miAbout;
        private System.Windows.Forms.Panel pDesktop;
        private System.Windows.Forms.Button deskbtnTimeSheets;
        private System.Windows.Forms.ToolStripMenuItem miCurrentUser;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripStatusLabel ttsVersion;
        private System.Windows.Forms.ToolStripStatusLabel tssServerConnection;
        private System.Windows.Forms.Timer reconnectTimer;



    }
}

