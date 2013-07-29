namespace TimeSheetManger
{
    partial class AdminPanelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbContent = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbweekend = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cpWeekEnd = new System.Windows.Forms.Panel();
            this.cpShortDay = new System.Windows.Forms.Panel();
            this.cpHolyday = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEditCatalog = new System.Windows.Forms.Button();
            this.cbCatalogs = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbAddMonth = new System.Windows.Forms.GroupBox();
            this.btnHideMonth = new System.Windows.Forms.Button();
            this.dtpPCMonth = new System.Windows.Forms.DateTimePicker();
            this.btnAddNewPCMonth = new System.Windows.Forms.Button();
            this.gbAddName = new System.Windows.Forms.GroupBox();
            this.btnHideName = new System.Windows.Forms.Button();
            this.btnAddNewPCName = new System.Windows.Forms.Button();
            this.tbNewPCName = new System.Windows.Forms.TextBox();
            this.gbValues = new System.Windows.Forms.GroupBox();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.lbHours = new System.Windows.Forms.Label();
            this.tbDays = new System.Windows.Forms.TextBox();
            this.lbDays = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbAddYear = new System.Windows.Forms.GroupBox();
            this.btnHideYear = new System.Windows.Forms.Button();
            this.dtpPCYear = new System.Windows.Forms.DateTimePicker();
            this.btnAddNewPCYear = new System.Windows.Forms.Button();
            this.btnAddPCMonth = new System.Windows.Forms.Button();
            this.btnAddPCYear = new System.Windows.Forms.Button();
            this.btnAddPCalName = new System.Windows.Forms.Button();
            this.btnPCalMonth = new System.Windows.Forms.Label();
            this.lbPYear = new System.Windows.Forms.Label();
            this.lbPCalendarName = new System.Windows.Forms.Label();
            this.cbPMonth = new System.Windows.Forms.ComboBox();
            this.cbPYear = new System.Windows.Forms.ComboBox();
            this.cbPCalendar = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dtpHolydayMonthPicker = new System.Windows.Forms.DateTimePicker();
            this.pbGeneratingWeekEnds = new System.Windows.Forms.ProgressBar();
            this.btnGenerateWeekEnds = new System.Windows.Forms.Button();
            this.rbShortDay = new System.Windows.Forms.RadioButton();
            this.rbHolyDay = new System.Windows.Forms.RadioButton();
            this.rbWeekEnd = new System.Windows.Forms.RadioButton();
            this.rbUsualDay = new System.Windows.Forms.RadioButton();
            this.cdDayColors = new System.Windows.Forms.ColorDialog();
            this.gbColors = new System.Windows.Forms.GroupBox();
            this.cHolydayCalendar = new SwarthyComponents.CalendarView();
            this.tbContent.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbAddMonth.SuspendLayout();
            this.gbAddName.SuspendLayout();
            this.gbValues.SuspendLayout();
            this.gbAddYear.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.gbColors.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbContent
            // 
            this.tbContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbContent.Controls.Add(this.tabPage1);
            this.tbContent.Controls.Add(this.tabPage2);
            this.tbContent.Controls.Add(this.tabPage3);
            this.tbContent.Location = new System.Drawing.Point(12, 12);
            this.tbContent.Name = "tbContent";
            this.tbContent.SelectedIndex = 0;
            this.tbContent.Size = new System.Drawing.Size(469, 284);
            this.tbContent.TabIndex = 0;
            this.tbContent.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbContent_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbColors);
            this.tabPage1.Controls.Add(this.btnEditCatalog);
            this.tabPage1.Controls.Add(this.cbCatalogs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(461, 258);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Справочники";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbweekend
            // 
            this.lbweekend.AutoSize = true;
            this.lbweekend.Location = new System.Drawing.Point(6, 25);
            this.lbweekend.Name = "lbweekend";
            this.lbweekend.Size = new System.Drawing.Size(89, 13);
            this.lbweekend.TabIndex = 7;
            this.lbweekend.Text = "Выходные дни - ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Сокращенные дни - ";
            // 
            // cpWeekEnd
            // 
            this.cpWeekEnd.Location = new System.Drawing.Point(118, 25);
            this.cpWeekEnd.Name = "cpWeekEnd";
            this.cpWeekEnd.Size = new System.Drawing.Size(32, 13);
            this.cpWeekEnd.TabIndex = 6;
            this.cpWeekEnd.DoubleClick += new System.EventHandler(this.cpWeekEnd_DoubleClick);
            // 
            // cpShortDay
            // 
            this.cpShortDay.Location = new System.Drawing.Point(118, 63);
            this.cpShortDay.Name = "cpShortDay";
            this.cpShortDay.Size = new System.Drawing.Size(32, 13);
            this.cpShortDay.TabIndex = 6;
            this.cpShortDay.DoubleClick += new System.EventHandler(this.cpShortDay_DoubleClick);
            // 
            // cpHolyday
            // 
            this.cpHolyday.Location = new System.Drawing.Point(118, 44);
            this.cpHolyday.Name = "cpHolyday";
            this.cpHolyday.Size = new System.Drawing.Size(32, 13);
            this.cpHolyday.TabIndex = 6;
            this.cpHolyday.DoubleClick += new System.EventHandler(this.cpHolyday_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Праздничные дни - ";
            // 
            // btnEditCatalog
            // 
            this.btnEditCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCatalog.Location = new System.Drawing.Point(357, 6);
            this.btnEditCatalog.Name = "btnEditCatalog";
            this.btnEditCatalog.Size = new System.Drawing.Size(98, 23);
            this.btnEditCatalog.TabIndex = 1;
            this.btnEditCatalog.Text = "Редактировать";
            this.btnEditCatalog.UseVisualStyleBackColor = true;
            this.btnEditCatalog.Click += new System.EventHandler(this.btnEditCatalog_Click);
            // 
            // cbCatalogs
            // 
            this.cbCatalogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCatalogs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCatalogs.FormattingEnabled = true;
            this.cbCatalogs.Location = new System.Drawing.Point(6, 6);
            this.cbCatalogs.Name = "cbCatalogs";
            this.cbCatalogs.Size = new System.Drawing.Size(345, 21);
            this.cbCatalogs.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbAddMonth);
            this.tabPage2.Controls.Add(this.gbAddName);
            this.tabPage2.Controls.Add(this.gbValues);
            this.tabPage2.Controls.Add(this.gbAddYear);
            this.tabPage2.Controls.Add(this.btnAddPCMonth);
            this.tabPage2.Controls.Add(this.btnAddPCYear);
            this.tabPage2.Controls.Add(this.btnAddPCalName);
            this.tabPage2.Controls.Add(this.btnPCalMonth);
            this.tabPage2.Controls.Add(this.lbPYear);
            this.tabPage2.Controls.Add(this.lbPCalendarName);
            this.tabPage2.Controls.Add(this.cbPMonth);
            this.tabPage2.Controls.Add(this.cbPYear);
            this.tabPage2.Controls.Add(this.cbPCalendar);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(461, 258);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Производственные календари";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gbAddMonth
            // 
            this.gbAddMonth.Controls.Add(this.btnHideMonth);
            this.gbAddMonth.Controls.Add(this.dtpPCMonth);
            this.gbAddMonth.Controls.Add(this.btnAddNewPCMonth);
            this.gbAddMonth.Location = new System.Drawing.Point(173, 198);
            this.gbAddMonth.Name = "gbAddMonth";
            this.gbAddMonth.Size = new System.Drawing.Size(166, 50);
            this.gbAddMonth.TabIndex = 6;
            this.gbAddMonth.TabStop = false;
            this.gbAddMonth.Text = "Добавление месяца";
            this.gbAddMonth.Visible = false;
            // 
            // btnHideMonth
            // 
            this.btnHideMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHideMonth.Location = new System.Drawing.Point(138, 16);
            this.btnHideMonth.Name = "btnHideMonth";
            this.btnHideMonth.Size = new System.Drawing.Size(22, 23);
            this.btnHideMonth.TabIndex = 4;
            this.btnHideMonth.Text = "X";
            this.btnHideMonth.UseVisualStyleBackColor = true;
            this.btnHideMonth.Click += new System.EventHandler(this.btnHideMonth_Click);
            // 
            // dtpPCMonth
            // 
            this.dtpPCMonth.CustomFormat = "MMMM";
            this.dtpPCMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPCMonth.Location = new System.Drawing.Point(6, 19);
            this.dtpPCMonth.Name = "dtpPCMonth";
            this.dtpPCMonth.Size = new System.Drawing.Size(76, 20);
            this.dtpPCMonth.TabIndex = 5;
            // 
            // btnAddNewPCMonth
            // 
            this.btnAddNewPCMonth.Location = new System.Drawing.Point(88, 16);
            this.btnAddNewPCMonth.Name = "btnAddNewPCMonth";
            this.btnAddNewPCMonth.Size = new System.Drawing.Size(47, 23);
            this.btnAddNewPCMonth.TabIndex = 4;
            this.btnAddNewPCMonth.Text = "Ок";
            this.btnAddNewPCMonth.UseVisualStyleBackColor = true;
            this.btnAddNewPCMonth.Click += new System.EventHandler(this.btnAddNewPCMonth_Click);
            // 
            // gbAddName
            // 
            this.gbAddName.Controls.Add(this.btnHideName);
            this.gbAddName.Controls.Add(this.btnAddNewPCName);
            this.gbAddName.Controls.Add(this.tbNewPCName);
            this.gbAddName.Location = new System.Drawing.Point(173, 89);
            this.gbAddName.Name = "gbAddName";
            this.gbAddName.Size = new System.Drawing.Size(285, 50);
            this.gbAddName.TabIndex = 6;
            this.gbAddName.TabStop = false;
            this.gbAddName.Text = "Добавление названия календаря";
            this.gbAddName.Visible = false;
            // 
            // btnHideName
            // 
            this.btnHideName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHideName.Location = new System.Drawing.Point(257, 16);
            this.btnHideName.Name = "btnHideName";
            this.btnHideName.Size = new System.Drawing.Size(22, 23);
            this.btnHideName.TabIndex = 4;
            this.btnHideName.Text = "X";
            this.btnHideName.UseVisualStyleBackColor = true;
            this.btnHideName.Click += new System.EventHandler(this.btnHideName_Click);
            // 
            // btnAddNewPCName
            // 
            this.btnAddNewPCName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddNewPCName.Location = new System.Drawing.Point(204, 16);
            this.btnAddNewPCName.Name = "btnAddNewPCName";
            this.btnAddNewPCName.Size = new System.Drawing.Size(47, 23);
            this.btnAddNewPCName.TabIndex = 4;
            this.btnAddNewPCName.Text = "Ок";
            this.btnAddNewPCName.UseVisualStyleBackColor = true;
            this.btnAddNewPCName.Click += new System.EventHandler(this.btnAddNewPCName_Click);
            // 
            // tbNewPCName
            // 
            this.tbNewPCName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNewPCName.Location = new System.Drawing.Point(6, 19);
            this.tbNewPCName.Name = "tbNewPCName";
            this.tbNewPCName.Size = new System.Drawing.Size(192, 20);
            this.tbNewPCName.TabIndex = 3;
            // 
            // gbValues
            // 
            this.gbValues.Controls.Add(this.tbHours);
            this.gbValues.Controls.Add(this.lbHours);
            this.gbValues.Controls.Add(this.tbDays);
            this.gbValues.Controls.Add(this.lbDays);
            this.gbValues.Controls.Add(this.btnSave);
            this.gbValues.Location = new System.Drawing.Point(6, 89);
            this.gbValues.Name = "gbValues";
            this.gbValues.Size = new System.Drawing.Size(161, 106);
            this.gbValues.TabIndex = 5;
            this.gbValues.TabStop = false;
            this.gbValues.Text = "Значения";
            // 
            // tbHours
            // 
            this.tbHours.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHours.Location = new System.Drawing.Point(63, 19);
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(92, 20);
            this.tbHours.TabIndex = 3;
            // 
            // lbHours
            // 
            this.lbHours.AutoSize = true;
            this.lbHours.Location = new System.Drawing.Point(6, 22);
            this.lbHours.Name = "lbHours";
            this.lbHours.Size = new System.Drawing.Size(38, 13);
            this.lbHours.TabIndex = 1;
            this.lbHours.Text = "Часы:";
            // 
            // tbDays
            // 
            this.tbDays.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDays.Location = new System.Drawing.Point(63, 45);
            this.tbDays.Name = "tbDays";
            this.tbDays.Size = new System.Drawing.Size(92, 20);
            this.tbDays.TabIndex = 3;
            // 
            // lbDays
            // 
            this.lbDays.AutoSize = true;
            this.lbDays.Location = new System.Drawing.Point(6, 48);
            this.lbDays.Name = "lbDays";
            this.lbDays.Size = new System.Drawing.Size(31, 13);
            this.lbDays.TabIndex = 1;
            this.lbDays.Text = "Дни:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(63, 75);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbAddYear
            // 
            this.gbAddYear.Controls.Add(this.btnHideYear);
            this.gbAddYear.Controls.Add(this.dtpPCYear);
            this.gbAddYear.Controls.Add(this.btnAddNewPCYear);
            this.gbAddYear.Location = new System.Drawing.Point(173, 145);
            this.gbAddYear.Name = "gbAddYear";
            this.gbAddYear.Size = new System.Drawing.Size(166, 50);
            this.gbAddYear.TabIndex = 6;
            this.gbAddYear.TabStop = false;
            this.gbAddYear.Text = "Добавление года";
            this.gbAddYear.Visible = false;
            // 
            // btnHideYear
            // 
            this.btnHideYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHideYear.Location = new System.Drawing.Point(138, 16);
            this.btnHideYear.Name = "btnHideYear";
            this.btnHideYear.Size = new System.Drawing.Size(22, 23);
            this.btnHideYear.TabIndex = 4;
            this.btnHideYear.Text = "X";
            this.btnHideYear.UseVisualStyleBackColor = true;
            this.btnHideYear.Click += new System.EventHandler(this.btnHideYear_Click);
            // 
            // dtpPCYear
            // 
            this.dtpPCYear.CustomFormat = "yyyy";
            this.dtpPCYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPCYear.Location = new System.Drawing.Point(6, 19);
            this.dtpPCYear.Name = "dtpPCYear";
            this.dtpPCYear.Size = new System.Drawing.Size(76, 20);
            this.dtpPCYear.TabIndex = 5;
            // 
            // btnAddNewPCYear
            // 
            this.btnAddNewPCYear.Location = new System.Drawing.Point(88, 16);
            this.btnAddNewPCYear.Name = "btnAddNewPCYear";
            this.btnAddNewPCYear.Size = new System.Drawing.Size(47, 23);
            this.btnAddNewPCYear.TabIndex = 4;
            this.btnAddNewPCYear.Text = "Ок";
            this.btnAddNewPCYear.UseVisualStyleBackColor = true;
            this.btnAddNewPCYear.Click += new System.EventHandler(this.btnAddNewPCYear_Click);
            // 
            // btnAddPCMonth
            // 
            this.btnAddPCMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPCMonth.Location = new System.Drawing.Point(433, 61);
            this.btnAddPCMonth.Name = "btnAddPCMonth";
            this.btnAddPCMonth.Size = new System.Drawing.Size(25, 21);
            this.btnAddPCMonth.TabIndex = 2;
            this.btnAddPCMonth.Text = "+";
            this.btnAddPCMonth.UseVisualStyleBackColor = true;
            this.btnAddPCMonth.Click += new System.EventHandler(this.btnAddPCMonth_Click);
            // 
            // btnAddPCYear
            // 
            this.btnAddPCYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPCYear.Location = new System.Drawing.Point(433, 34);
            this.btnAddPCYear.Name = "btnAddPCYear";
            this.btnAddPCYear.Size = new System.Drawing.Size(25, 21);
            this.btnAddPCYear.TabIndex = 2;
            this.btnAddPCYear.Text = "+";
            this.btnAddPCYear.UseVisualStyleBackColor = true;
            this.btnAddPCYear.Click += new System.EventHandler(this.btnAddPCYear_Click);
            // 
            // btnAddPCalName
            // 
            this.btnAddPCalName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPCalName.Location = new System.Drawing.Point(433, 7);
            this.btnAddPCalName.Name = "btnAddPCalName";
            this.btnAddPCalName.Size = new System.Drawing.Size(25, 21);
            this.btnAddPCalName.TabIndex = 2;
            this.btnAddPCalName.Text = "+";
            this.btnAddPCalName.UseVisualStyleBackColor = true;
            this.btnAddPCalName.Click += new System.EventHandler(this.btnAddPCalName_Click);
            // 
            // btnPCalMonth
            // 
            this.btnPCalMonth.AutoSize = true;
            this.btnPCalMonth.Location = new System.Drawing.Point(3, 65);
            this.btnPCalMonth.Name = "btnPCalMonth";
            this.btnPCalMonth.Size = new System.Drawing.Size(43, 13);
            this.btnPCalMonth.TabIndex = 1;
            this.btnPCalMonth.Text = "Месяц:";
            // 
            // lbPYear
            // 
            this.lbPYear.AutoSize = true;
            this.lbPYear.Location = new System.Drawing.Point(3, 38);
            this.lbPYear.Name = "lbPYear";
            this.lbPYear.Size = new System.Drawing.Size(28, 13);
            this.lbPYear.TabIndex = 1;
            this.lbPYear.Text = "Год:";
            // 
            // lbPCalendarName
            // 
            this.lbPCalendarName.AutoSize = true;
            this.lbPCalendarName.Location = new System.Drawing.Point(3, 11);
            this.lbPCalendarName.Name = "lbPCalendarName";
            this.lbPCalendarName.Size = new System.Drawing.Size(60, 13);
            this.lbPCalendarName.TabIndex = 1;
            this.lbPCalendarName.Text = "Название:";
            // 
            // cbPMonth
            // 
            this.cbPMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPMonth.FormattingEnabled = true;
            this.cbPMonth.Location = new System.Drawing.Point(69, 62);
            this.cbPMonth.Name = "cbPMonth";
            this.cbPMonth.Size = new System.Drawing.Size(358, 21);
            this.cbPMonth.TabIndex = 0;
            this.cbPMonth.SelectedValueChanged += new System.EventHandler(this.cbPMonth_SelectedValueChanged);
            // 
            // cbPYear
            // 
            this.cbPYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPYear.FormattingEnabled = true;
            this.cbPYear.Location = new System.Drawing.Point(69, 35);
            this.cbPYear.Name = "cbPYear";
            this.cbPYear.Size = new System.Drawing.Size(358, 21);
            this.cbPYear.TabIndex = 0;
            this.cbPYear.SelectedValueChanged += new System.EventHandler(this.cbPYear_SelectedValueChanged);
            // 
            // cbPCalendar
            // 
            this.cbPCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPCalendar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPCalendar.FormattingEnabled = true;
            this.cbPCalendar.Location = new System.Drawing.Point(69, 8);
            this.cbPCalendar.Name = "cbPCalendar";
            this.cbPCalendar.Size = new System.Drawing.Size(358, 21);
            this.cbPCalendar.TabIndex = 0;
            this.cbPCalendar.SelectedValueChanged += new System.EventHandler(this.cbPCalendar_SelectedValueChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dtpHolydayMonthPicker);
            this.tabPage3.Controls.Add(this.pbGeneratingWeekEnds);
            this.tabPage3.Controls.Add(this.btnGenerateWeekEnds);
            this.tabPage3.Controls.Add(this.rbShortDay);
            this.tabPage3.Controls.Add(this.rbHolyDay);
            this.tabPage3.Controls.Add(this.rbWeekEnd);
            this.tabPage3.Controls.Add(this.rbUsualDay);
            this.tabPage3.Controls.Add(this.cHolydayCalendar);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(461, 258);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Праздники/Выходные";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dtpHolydayMonthPicker
            // 
            this.dtpHolydayMonthPicker.CustomFormat = "MMMM yyyy";
            this.dtpHolydayMonthPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHolydayMonthPicker.Location = new System.Drawing.Point(9, 9);
            this.dtpHolydayMonthPicker.Name = "dtpHolydayMonthPicker";
            this.dtpHolydayMonthPicker.Size = new System.Drawing.Size(154, 20);
            this.dtpHolydayMonthPicker.TabIndex = 5;
            this.dtpHolydayMonthPicker.ValueChanged += new System.EventHandler(this.dtpHolydayMonthPicker_ValueChanged);
            // 
            // pbGeneratingWeekEnds
            // 
            this.pbGeneratingWeekEnds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbGeneratingWeekEnds.Location = new System.Drawing.Point(3, 237);
            this.pbGeneratingWeekEnds.MarqueeAnimationSpeed = 10;
            this.pbGeneratingWeekEnds.Maximum = 366;
            this.pbGeneratingWeekEnds.Name = "pbGeneratingWeekEnds";
            this.pbGeneratingWeekEnds.Size = new System.Drawing.Size(455, 18);
            this.pbGeneratingWeekEnds.TabIndex = 3;
            this.pbGeneratingWeekEnds.Visible = false;
            // 
            // btnGenerateWeekEnds
            // 
            this.btnGenerateWeekEnds.Location = new System.Drawing.Point(182, 124);
            this.btnGenerateWeekEnds.Name = "btnGenerateWeekEnds";
            this.btnGenerateWeekEnds.Size = new System.Drawing.Size(168, 41);
            this.btnGenerateWeekEnds.TabIndex = 2;
            this.btnGenerateWeekEnds.Text = "Сделать все Сб и Вс выходными днями";
            this.btnGenerateWeekEnds.UseVisualStyleBackColor = true;
            this.btnGenerateWeekEnds.Click += new System.EventHandler(this.btnGenerateWeekEnds_Click);
            // 
            // rbShortDay
            // 
            this.rbShortDay.AutoSize = true;
            this.rbShortDay.Location = new System.Drawing.Point(182, 78);
            this.rbShortDay.Name = "rbShortDay";
            this.rbShortDay.Size = new System.Drawing.Size(168, 17);
            this.rbShortDay.TabIndex = 1;
            this.rbShortDay.Text = "Сокращенный рабочий день";
            this.rbShortDay.UseVisualStyleBackColor = true;
            this.rbShortDay.CheckedChanged += new System.EventHandler(this.rbShortDay_CheckedChanged);
            // 
            // rbHolyDay
            // 
            this.rbHolyDay.AutoSize = true;
            this.rbHolyDay.Location = new System.Drawing.Point(182, 55);
            this.rbHolyDay.Name = "rbHolyDay";
            this.rbHolyDay.Size = new System.Drawing.Size(75, 17);
            this.rbHolyDay.TabIndex = 1;
            this.rbHolyDay.Text = "Праздник";
            this.rbHolyDay.UseVisualStyleBackColor = true;
            this.rbHolyDay.CheckedChanged += new System.EventHandler(this.rbHolyDay_CheckedChanged);
            // 
            // rbWeekEnd
            // 
            this.rbWeekEnd.AutoSize = true;
            this.rbWeekEnd.Location = new System.Drawing.Point(182, 32);
            this.rbWeekEnd.Name = "rbWeekEnd";
            this.rbWeekEnd.Size = new System.Drawing.Size(75, 17);
            this.rbWeekEnd.TabIndex = 1;
            this.rbWeekEnd.Text = "Выходной";
            this.rbWeekEnd.UseVisualStyleBackColor = true;
            this.rbWeekEnd.CheckedChanged += new System.EventHandler(this.rbWeekEnd_CheckedChanged);
            // 
            // rbUsualDay
            // 
            this.rbUsualDay.AutoSize = true;
            this.rbUsualDay.Checked = true;
            this.rbUsualDay.Location = new System.Drawing.Point(182, 9);
            this.rbUsualDay.Name = "rbUsualDay";
            this.rbUsualDay.Size = new System.Drawing.Size(99, 17);
            this.rbUsualDay.TabIndex = 1;
            this.rbUsualDay.TabStop = true;
            this.rbUsualDay.Text = "Обычный день";
            this.rbUsualDay.UseVisualStyleBackColor = true;
            this.rbUsualDay.CheckedChanged += new System.EventHandler(this.rbUsualDay_CheckedChanged);
            // 
            // gbColors
            // 
            this.gbColors.Controls.Add(this.lbweekend);
            this.gbColors.Controls.Add(this.label3);
            this.gbColors.Controls.Add(this.label2);
            this.gbColors.Controls.Add(this.cpWeekEnd);
            this.gbColors.Controls.Add(this.cpHolyday);
            this.gbColors.Controls.Add(this.cpShortDay);
            this.gbColors.Location = new System.Drawing.Point(6, 33);
            this.gbColors.Name = "gbColors";
            this.gbColors.Size = new System.Drawing.Size(168, 98);
            this.gbColors.TabIndex = 10;
            this.gbColors.TabStop = false;
            this.gbColors.Text = "Настройка цветов";
            this.gbColors.Visible = false;
            // 
            // cHolydayCalendar
            // 
            this.cHolydayCalendar.DayPadding = 1;
            this.cHolydayCalendar.DaySize = new System.Drawing.Size(20, 20);
            this.cHolydayCalendar.Location = new System.Drawing.Point(9, 32);
            this.cHolydayCalendar.MarginDays = new System.Drawing.Point(0, 20);
            this.cHolydayCalendar.MarginDaysOfWeek = new System.Drawing.Point(0, 0);
            this.cHolydayCalendar.MarginTitle = new System.Drawing.Point(0, 0);
            this.cHolydayCalendar.Month = 7;
            this.cHolydayCalendar.Name = "cHolydayCalendar";
            this.cHolydayCalendar.OnSelectedDateChanged = null;
            this.cHolydayCalendar.SelectedDate = new System.DateTime(((long)(0)));
            this.cHolydayCalendar.ShowDayOfWeek = true;
            this.cHolydayCalendar.ShowTitle = false;
            this.cHolydayCalendar.Size = new System.Drawing.Size(154, 133);
            this.cHolydayCalendar.TabIndex = 4;
            this.cHolydayCalendar.Year = 2013;
            // 
            // AdminPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 308);
            this.Controls.Add(this.tbContent);
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminPanelForm";
            this.Text = "Панель администратора";
            this.Load += new System.EventHandler(this.AdminPanelForm_Load);
            this.tbContent.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.gbAddMonth.ResumeLayout(false);
            this.gbAddName.ResumeLayout(false);
            this.gbAddName.PerformLayout();
            this.gbValues.ResumeLayout(false);
            this.gbValues.PerformLayout();
            this.gbAddYear.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.gbColors.ResumeLayout(false);
            this.gbColors.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbContent;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cbCatalogs;
        private System.Windows.Forms.Button btnEditCatalog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RadioButton rbShortDay;
        private System.Windows.Forms.RadioButton rbHolyDay;
        private System.Windows.Forms.RadioButton rbWeekEnd;
        private System.Windows.Forms.RadioButton rbUsualDay;
        private System.Windows.Forms.Button btnGenerateWeekEnds;
        private System.Windows.Forms.ProgressBar pbGeneratingWeekEnds;
        private System.Windows.Forms.Label lbPYear;
        private System.Windows.Forms.Label lbPCalendarName;
        private System.Windows.Forms.ComboBox cbPYear;
        private System.Windows.Forms.ComboBox cbPCalendar;
        private System.Windows.Forms.Button btnAddPCMonth;
        private System.Windows.Forms.Button btnAddPCYear;
        private System.Windows.Forms.Button btnAddPCalName;
        private System.Windows.Forms.Label btnPCalMonth;
        private System.Windows.Forms.ComboBox cbPMonth;
        private System.Windows.Forms.TextBox tbDays;
        private System.Windows.Forms.TextBox tbHours;
        private System.Windows.Forms.Label lbDays;
        private System.Windows.Forms.Label lbHours;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbValues;
        private System.Windows.Forms.GroupBox gbAddName;
        private System.Windows.Forms.Button btnAddNewPCName;
        private System.Windows.Forms.TextBox tbNewPCName;
        private System.Windows.Forms.GroupBox gbAddYear;
        private System.Windows.Forms.DateTimePicker dtpPCYear;
        private System.Windows.Forms.Button btnAddNewPCYear;
        private System.Windows.Forms.GroupBox gbAddMonth;
        private System.Windows.Forms.DateTimePicker dtpPCMonth;
        private System.Windows.Forms.Button btnAddNewPCMonth;
        private System.Windows.Forms.Button btnHideMonth;
        private System.Windows.Forms.Button btnHideYear;
        private System.Windows.Forms.Button btnHideName;
        private SwarthyComponents.CalendarView cHolydayCalendar;
        private System.Windows.Forms.DateTimePicker dtpHolydayMonthPicker;
        private System.Windows.Forms.Label lbweekend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel cpWeekEnd;
        private System.Windows.Forms.Panel cpShortDay;
        private System.Windows.Forms.Panel cpHolyday;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog cdDayColors;
        private System.Windows.Forms.GroupBox gbColors;
    }
}