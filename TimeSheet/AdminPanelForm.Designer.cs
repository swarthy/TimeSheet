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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminPanelForm));
            this.tbContent = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnExportTimeSheetDBF = new System.Windows.Forms.Button();
            this.btnExportTimeSheetXML = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.gbColors = new System.Windows.Forms.GroupBox();
            this.lbweekend = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cpWeekEnd = new System.Windows.Forms.Panel();
            this.cpHolyday = new System.Windows.Forms.Panel();
            this.cpShortDay = new System.Windows.Forms.Panel();
            this.btnEditCatalog = new System.Windows.Forms.Button();
            this.cbCatalogs = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupPanelSelectControls = new System.Windows.Forms.Panel();
            this.lbPCalendarName = new System.Windows.Forms.Label();
            this.cbPCalendar = new System.Windows.Forms.ComboBox();
            this.cbPYear = new System.Windows.Forms.ComboBox();
            this.cbPMonth = new System.Windows.Forms.ComboBox();
            this.lbPYear = new System.Windows.Forms.Label();
            this.btnPCalMonth = new System.Windows.Forms.Label();
            this.btnAddPCMonth = new System.Windows.Forms.Button();
            this.btnAddPCalName = new System.Windows.Forms.Button();
            this.btnAddPCYear = new System.Windows.Forms.Button();
            this.dgAllMonthData = new System.Windows.Forms.DataGridView();
            this.clmMonth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbAddMonth = new System.Windows.Forms.GroupBox();
            this.cbPAddMonth = new System.Windows.Forms.ComboBox();
            this.btnHideMonth = new System.Windows.Forms.Button();
            this.btnAddNewPCMonth = new System.Windows.Forms.Button();
            this.gbAddName = new System.Windows.Forms.GroupBox();
            this.btnHideName = new System.Windows.Forms.Button();
            this.btnAddNewPCName = new System.Windows.Forms.Button();
            this.tbPNewCName = new System.Windows.Forms.TextBox();
            this.gbValues = new System.Windows.Forms.GroupBox();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.lbHours = new System.Windows.Forms.Label();
            this.tbDays = new System.Windows.Forms.TextBox();
            this.lbDays = new System.Windows.Forms.Label();
            this.btnEditSaveDaysHours = new System.Windows.Forms.Button();
            this.gbAddYear = new System.Windows.Forms.GroupBox();
            this.tbPYear = new System.Windows.Forms.TextBox();
            this.btnHideYear = new System.Windows.Forms.Button();
            this.btnAddNewPCYear = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupPandelDayTypes = new System.Windows.Forms.Panel();
            this.rbUsualDay = new System.Windows.Forms.RadioButton();
            this.rbWeekEnd = new System.Windows.Forms.RadioButton();
            this.rbHolyDay = new System.Windows.Forms.RadioButton();
            this.rbShortDay = new System.Windows.Forms.RadioButton();
            this.dtpHolydayMonthPicker = new System.Windows.Forms.DateTimePicker();
            this.pbGeneratingWeekEnds = new System.Windows.Forms.ProgressBar();
            this.btnGenerateWeekEnds = new System.Windows.Forms.Button();
            this.btnEditSaveDayType = new System.Windows.Forms.Button();
            this.cHolydayCalendar = new SwarthyComponents.WinForms.CalendarView();
            this.cdDayColors = new System.Windows.Forms.ColorDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tbContent.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbColors.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupPanelSelectControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAllMonthData)).BeginInit();
            this.gbAddMonth.SuspendLayout();
            this.gbAddName.SuspendLayout();
            this.gbValues.SuspendLayout();
            this.gbAddYear.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupPandelDayTypes.SuspendLayout();
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
            this.tbContent.Size = new System.Drawing.Size(456, 405);
            this.tbContent.TabIndex = 0;
            this.tbContent.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tbContent_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.progressBar);
            this.tabPage1.Controls.Add(this.btnExportTimeSheetDBF);
            this.tabPage1.Controls.Add(this.btnExportTimeSheetXML);
            this.tabPage1.Controls.Add(this.btnImport);
            this.tabPage1.Controls.Add(this.gbColors);
            this.tabPage1.Controls.Add(this.btnEditCatalog);
            this.tabPage1.Controls.Add(this.cbCatalogs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(448, 379);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Справочники";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnExportTimeSheetDBF
            // 
            this.btnExportTimeSheetDBF.Image = global::TimeSheetManger.Properties.Resources.Download_16x161;
            this.btnExportTimeSheetDBF.Location = new System.Drawing.Point(6, 166);
            this.btnExportTimeSheetDBF.Name = "btnExportTimeSheetDBF";
            this.btnExportTimeSheetDBF.Size = new System.Drawing.Size(195, 23);
            this.btnExportTimeSheetDBF.TabIndex = 13;
            this.btnExportTimeSheetDBF.Text = "Экспорт всех табелей в DBF";
            this.btnExportTimeSheetDBF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportTimeSheetDBF.UseVisualStyleBackColor = true;
            this.btnExportTimeSheetDBF.Visible = false;
            this.btnExportTimeSheetDBF.Click += new System.EventHandler(this.btnExportTimeSheetDBF_Click);
            // 
            // btnExportTimeSheetXML
            // 
            this.btnExportTimeSheetXML.Image = global::TimeSheetManger.Properties.Resources.Download_16x16;
            this.btnExportTimeSheetXML.Location = new System.Drawing.Point(6, 137);
            this.btnExportTimeSheetXML.Name = "btnExportTimeSheetXML";
            this.btnExportTimeSheetXML.Size = new System.Drawing.Size(195, 23);
            this.btnExportTimeSheetXML.TabIndex = 12;
            this.btnExportTimeSheetXML.Text = "Экспорт всех табелей в XML";
            this.btnExportTimeSheetXML.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExportTimeSheetXML.UseVisualStyleBackColor = true;
            this.btnExportTimeSheetXML.Visible = false;
            this.btnExportTimeSheetXML.Click += new System.EventHandler(this.btnExportTimeSheetXML_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.Image = global::TimeSheetManger.Properties.Resources.Upload_16x16;
            this.btnImport.Location = new System.Drawing.Point(332, 33);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(110, 23);
            this.btnImport.TabIndex = 11;
            this.btnImport.Text = "Импорт";
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Visible = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
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
            this.gbColors.Size = new System.Drawing.Size(164, 98);
            this.gbColors.TabIndex = 10;
            this.gbColors.TabStop = false;
            this.gbColors.Text = "Настройка цветов";
            this.gbColors.Visible = false;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Праздничные дни - ";
            // 
            // cpWeekEnd
            // 
            this.cpWeekEnd.Location = new System.Drawing.Point(118, 25);
            this.cpWeekEnd.Name = "cpWeekEnd";
            this.cpWeekEnd.Size = new System.Drawing.Size(32, 13);
            this.cpWeekEnd.TabIndex = 6;
            this.cpWeekEnd.DoubleClick += new System.EventHandler(this.cpWeekEnd_DoubleClick);
            // 
            // cpHolyday
            // 
            this.cpHolyday.Location = new System.Drawing.Point(118, 44);
            this.cpHolyday.Name = "cpHolyday";
            this.cpHolyday.Size = new System.Drawing.Size(32, 13);
            this.cpHolyday.TabIndex = 6;
            this.cpHolyday.DoubleClick += new System.EventHandler(this.cpHolyday_DoubleClick);
            // 
            // cpShortDay
            // 
            this.cpShortDay.Location = new System.Drawing.Point(118, 63);
            this.cpShortDay.Name = "cpShortDay";
            this.cpShortDay.Size = new System.Drawing.Size(32, 13);
            this.cpShortDay.TabIndex = 6;
            this.cpShortDay.DoubleClick += new System.EventHandler(this.cpShortDay_DoubleClick);
            // 
            // btnEditCatalog
            // 
            this.btnEditCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCatalog.Image = global::TimeSheetManger.Properties.Resources.Edit_16x16;
            this.btnEditCatalog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditCatalog.Location = new System.Drawing.Point(332, 4);
            this.btnEditCatalog.Name = "btnEditCatalog";
            this.btnEditCatalog.Size = new System.Drawing.Size(110, 23);
            this.btnEditCatalog.TabIndex = 1;
            this.btnEditCatalog.Text = "Редактировать";
            this.btnEditCatalog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
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
            this.cbCatalogs.Size = new System.Drawing.Size(320, 21);
            this.cbCatalogs.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupPanelSelectControls);
            this.tabPage2.Controls.Add(this.dgAllMonthData);
            this.tabPage2.Controls.Add(this.gbAddMonth);
            this.tabPage2.Controls.Add(this.gbAddName);
            this.tabPage2.Controls.Add(this.gbValues);
            this.tabPage2.Controls.Add(this.gbAddYear);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(448, 379);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Производственные календари";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupPanelSelectControls
            // 
            this.groupPanelSelectControls.Controls.Add(this.lbPCalendarName);
            this.groupPanelSelectControls.Controls.Add(this.cbPCalendar);
            this.groupPanelSelectControls.Controls.Add(this.cbPYear);
            this.groupPanelSelectControls.Controls.Add(this.cbPMonth);
            this.groupPanelSelectControls.Controls.Add(this.lbPYear);
            this.groupPanelSelectControls.Controls.Add(this.btnPCalMonth);
            this.groupPanelSelectControls.Controls.Add(this.btnAddPCMonth);
            this.groupPanelSelectControls.Controls.Add(this.btnAddPCalName);
            this.groupPanelSelectControls.Controls.Add(this.btnAddPCYear);
            this.groupPanelSelectControls.Location = new System.Drawing.Point(3, 3);
            this.groupPanelSelectControls.Name = "groupPanelSelectControls";
            this.groupPanelSelectControls.Size = new System.Drawing.Size(442, 80);
            this.groupPanelSelectControls.TabIndex = 9;
            // 
            // lbPCalendarName
            // 
            this.lbPCalendarName.AutoSize = true;
            this.lbPCalendarName.Location = new System.Drawing.Point(0, 6);
            this.lbPCalendarName.Name = "lbPCalendarName";
            this.lbPCalendarName.Size = new System.Drawing.Size(60, 13);
            this.lbPCalendarName.TabIndex = 1;
            this.lbPCalendarName.Text = "Название:";
            // 
            // cbPCalendar
            // 
            this.cbPCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPCalendar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPCalendar.FormattingEnabled = true;
            this.cbPCalendar.Location = new System.Drawing.Point(66, 3);
            this.cbPCalendar.Name = "cbPCalendar";
            this.cbPCalendar.Size = new System.Drawing.Size(336, 21);
            this.cbPCalendar.TabIndex = 0;
            this.cbPCalendar.SelectedValueChanged += new System.EventHandler(this.cbPCalendar_SelectedValueChanged);
            // 
            // cbPYear
            // 
            this.cbPYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPYear.FormattingEnabled = true;
            this.cbPYear.Location = new System.Drawing.Point(66, 30);
            this.cbPYear.Name = "cbPYear";
            this.cbPYear.Size = new System.Drawing.Size(336, 21);
            this.cbPYear.TabIndex = 0;
            this.cbPYear.SelectedValueChanged += new System.EventHandler(this.cbPYear_SelectedValueChanged);
            // 
            // cbPMonth
            // 
            this.cbPMonth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPMonth.FormattingEnabled = true;
            this.cbPMonth.Location = new System.Drawing.Point(66, 57);
            this.cbPMonth.Name = "cbPMonth";
            this.cbPMonth.Size = new System.Drawing.Size(336, 21);
            this.cbPMonth.TabIndex = 0;
            this.cbPMonth.SelectedValueChanged += new System.EventHandler(this.cbPMonth_SelectedValueChanged);
            // 
            // lbPYear
            // 
            this.lbPYear.AutoSize = true;
            this.lbPYear.Location = new System.Drawing.Point(0, 33);
            this.lbPYear.Name = "lbPYear";
            this.lbPYear.Size = new System.Drawing.Size(28, 13);
            this.lbPYear.TabIndex = 1;
            this.lbPYear.Text = "Год:";
            // 
            // btnPCalMonth
            // 
            this.btnPCalMonth.AutoSize = true;
            this.btnPCalMonth.Location = new System.Drawing.Point(0, 60);
            this.btnPCalMonth.Name = "btnPCalMonth";
            this.btnPCalMonth.Size = new System.Drawing.Size(43, 13);
            this.btnPCalMonth.TabIndex = 1;
            this.btnPCalMonth.Text = "Месяц:";
            // 
            // btnAddPCMonth
            // 
            this.btnAddPCMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPCMonth.Location = new System.Drawing.Point(408, 57);
            this.btnAddPCMonth.Name = "btnAddPCMonth";
            this.btnAddPCMonth.Size = new System.Drawing.Size(25, 21);
            this.btnAddPCMonth.TabIndex = 2;
            this.btnAddPCMonth.Text = "+";
            this.btnAddPCMonth.UseVisualStyleBackColor = true;
            this.btnAddPCMonth.Click += new System.EventHandler(this.btnAddPCMonth_Click);
            // 
            // btnAddPCalName
            // 
            this.btnAddPCalName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPCalName.Location = new System.Drawing.Point(408, 3);
            this.btnAddPCalName.Name = "btnAddPCalName";
            this.btnAddPCalName.Size = new System.Drawing.Size(25, 21);
            this.btnAddPCalName.TabIndex = 2;
            this.btnAddPCalName.Text = "+";
            this.btnAddPCalName.UseVisualStyleBackColor = true;
            this.btnAddPCalName.Click += new System.EventHandler(this.btnAddPCalName_Click);
            // 
            // btnAddPCYear
            // 
            this.btnAddPCYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPCYear.Location = new System.Drawing.Point(408, 30);
            this.btnAddPCYear.Name = "btnAddPCYear";
            this.btnAddPCYear.Size = new System.Drawing.Size(25, 21);
            this.btnAddPCYear.TabIndex = 2;
            this.btnAddPCYear.Text = "+";
            this.btnAddPCYear.UseVisualStyleBackColor = true;
            this.btnAddPCYear.Click += new System.EventHandler(this.btnAddPCYear_Click);
            // 
            // dgAllMonthData
            // 
            this.dgAllMonthData.AllowUserToAddRows = false;
            this.dgAllMonthData.AllowUserToDeleteRows = false;
            this.dgAllMonthData.AllowUserToResizeColumns = false;
            this.dgAllMonthData.AllowUserToResizeRows = false;
            this.dgAllMonthData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAllMonthData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAllMonthData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmMonth,
            this.clmHours,
            this.clmDays});
            this.dgAllMonthData.Location = new System.Drawing.Point(192, 89);
            this.dgAllMonthData.MultiSelect = false;
            this.dgAllMonthData.Name = "dgAllMonthData";
            this.dgAllMonthData.ReadOnly = true;
            this.dgAllMonthData.RowHeadersVisible = false;
            this.dgAllMonthData.ShowCellErrors = false;
            this.dgAllMonthData.ShowCellToolTips = false;
            this.dgAllMonthData.ShowEditingIcon = false;
            this.dgAllMonthData.ShowRowErrors = false;
            this.dgAllMonthData.Size = new System.Drawing.Size(253, 287);
            this.dgAllMonthData.TabIndex = 8;
            // 
            // clmMonth
            // 
            this.clmMonth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmMonth.HeaderText = "Месяц";
            this.clmMonth.Name = "clmMonth";
            this.clmMonth.ReadOnly = true;
            // 
            // clmHours
            // 
            this.clmHours.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmHours.HeaderText = "Часы";
            this.clmHours.Name = "clmHours";
            this.clmHours.ReadOnly = true;
            // 
            // clmDays
            // 
            this.clmDays.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmDays.HeaderText = "Дни";
            this.clmDays.Name = "clmDays";
            this.clmDays.ReadOnly = true;
            // 
            // gbAddMonth
            // 
            this.gbAddMonth.Controls.Add(this.cbPAddMonth);
            this.gbAddMonth.Controls.Add(this.btnHideMonth);
            this.gbAddMonth.Controls.Add(this.btnAddNewPCMonth);
            this.gbAddMonth.Location = new System.Drawing.Point(6, 310);
            this.gbAddMonth.Name = "gbAddMonth";
            this.gbAddMonth.Size = new System.Drawing.Size(180, 50);
            this.gbAddMonth.TabIndex = 6;
            this.gbAddMonth.TabStop = false;
            this.gbAddMonth.Text = "Добавление месяца";
            this.gbAddMonth.Visible = false;
            // 
            // cbPAddMonth
            // 
            this.cbPAddMonth.AutoCompleteCustomSource.AddRange(new string[] {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"});
            this.cbPAddMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPAddMonth.FormattingEnabled = true;
            this.cbPAddMonth.Items.AddRange(new object[] {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"});
            this.cbPAddMonth.Location = new System.Drawing.Point(6, 18);
            this.cbPAddMonth.Name = "cbPAddMonth";
            this.cbPAddMonth.Size = new System.Drawing.Size(87, 21);
            this.cbPAddMonth.TabIndex = 5;
            // 
            // btnHideMonth
            // 
            this.btnHideMonth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHideMonth.Location = new System.Drawing.Point(152, 16);
            this.btnHideMonth.Name = "btnHideMonth";
            this.btnHideMonth.Size = new System.Drawing.Size(22, 23);
            this.btnHideMonth.TabIndex = 4;
            this.btnHideMonth.Text = "X";
            this.btnHideMonth.UseVisualStyleBackColor = true;
            this.btnHideMonth.Click += new System.EventHandler(this.btnHideMonth_Click);
            // 
            // btnAddNewPCMonth
            // 
            this.btnAddNewPCMonth.Location = new System.Drawing.Point(99, 16);
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
            this.gbAddName.Controls.Add(this.tbPNewCName);
            this.gbAddName.Location = new System.Drawing.Point(6, 201);
            this.gbAddName.Name = "gbAddName";
            this.gbAddName.Size = new System.Drawing.Size(180, 50);
            this.gbAddName.TabIndex = 6;
            this.gbAddName.TabStop = false;
            this.gbAddName.Text = "Добавление календаря";
            this.gbAddName.Visible = false;
            // 
            // btnHideName
            // 
            this.btnHideName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHideName.Location = new System.Drawing.Point(152, 16);
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
            this.btnAddNewPCName.Location = new System.Drawing.Point(99, 16);
            this.btnAddNewPCName.Name = "btnAddNewPCName";
            this.btnAddNewPCName.Size = new System.Drawing.Size(47, 23);
            this.btnAddNewPCName.TabIndex = 4;
            this.btnAddNewPCName.Text = "Ок";
            this.btnAddNewPCName.UseVisualStyleBackColor = true;
            this.btnAddNewPCName.Click += new System.EventHandler(this.btnAddNewPCName_Click);
            // 
            // tbPNewCName
            // 
            this.tbPNewCName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPNewCName.Location = new System.Drawing.Point(6, 19);
            this.tbPNewCName.Name = "tbPNewCName";
            this.tbPNewCName.Size = new System.Drawing.Size(87, 20);
            this.tbPNewCName.TabIndex = 3;
            // 
            // gbValues
            // 
            this.gbValues.Controls.Add(this.tbHours);
            this.gbValues.Controls.Add(this.lbHours);
            this.gbValues.Controls.Add(this.tbDays);
            this.gbValues.Controls.Add(this.lbDays);
            this.gbValues.Controls.Add(this.btnEditSaveDaysHours);
            this.gbValues.Location = new System.Drawing.Point(6, 89);
            this.gbValues.Name = "gbValues";
            this.gbValues.Size = new System.Drawing.Size(180, 106);
            this.gbValues.TabIndex = 5;
            this.gbValues.TabStop = false;
            this.gbValues.Text = "Значения";
            // 
            // tbHours
            // 
            this.tbHours.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHours.Enabled = false;
            this.tbHours.Location = new System.Drawing.Point(63, 19);
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(111, 20);
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
            this.tbDays.Enabled = false;
            this.tbDays.Location = new System.Drawing.Point(63, 45);
            this.tbDays.Name = "tbDays";
            this.tbDays.Size = new System.Drawing.Size(111, 20);
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
            // btnEditSaveDaysHours
            // 
            this.btnEditSaveDaysHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditSaveDaysHours.Image = global::TimeSheetManger.Properties.Resources.Edit_16x16;
            this.btnEditSaveDaysHours.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditSaveDaysHours.Location = new System.Drawing.Point(63, 75);
            this.btnEditSaveDaysHours.Name = "btnEditSaveDaysHours";
            this.btnEditSaveDaysHours.Size = new System.Drawing.Size(111, 23);
            this.btnEditSaveDaysHours.TabIndex = 4;
            this.btnEditSaveDaysHours.Text = "Редактировать";
            this.btnEditSaveDaysHours.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditSaveDaysHours.UseVisualStyleBackColor = true;
            this.btnEditSaveDaysHours.Click += new System.EventHandler(this.btnEditSaveDaysHours_Click);
            // 
            // gbAddYear
            // 
            this.gbAddYear.Controls.Add(this.tbPYear);
            this.gbAddYear.Controls.Add(this.btnHideYear);
            this.gbAddYear.Controls.Add(this.btnAddNewPCYear);
            this.gbAddYear.Location = new System.Drawing.Point(6, 257);
            this.gbAddYear.Name = "gbAddYear";
            this.gbAddYear.Size = new System.Drawing.Size(180, 50);
            this.gbAddYear.TabIndex = 6;
            this.gbAddYear.TabStop = false;
            this.gbAddYear.Text = "Добавление года";
            this.gbAddYear.Visible = false;
            // 
            // tbPYear
            // 
            this.tbPYear.Location = new System.Drawing.Point(6, 18);
            this.tbPYear.Name = "tbPYear";
            this.tbPYear.Size = new System.Drawing.Size(87, 20);
            this.tbPYear.TabIndex = 5;
            // 
            // btnHideYear
            // 
            this.btnHideYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHideYear.Location = new System.Drawing.Point(152, 16);
            this.btnHideYear.Name = "btnHideYear";
            this.btnHideYear.Size = new System.Drawing.Size(22, 23);
            this.btnHideYear.TabIndex = 4;
            this.btnHideYear.Text = "X";
            this.btnHideYear.UseVisualStyleBackColor = true;
            this.btnHideYear.Click += new System.EventHandler(this.btnHideYear_Click);
            // 
            // btnAddNewPCYear
            // 
            this.btnAddNewPCYear.Location = new System.Drawing.Point(99, 16);
            this.btnAddNewPCYear.Name = "btnAddNewPCYear";
            this.btnAddNewPCYear.Size = new System.Drawing.Size(47, 23);
            this.btnAddNewPCYear.TabIndex = 4;
            this.btnAddNewPCYear.Text = "Ок";
            this.btnAddNewPCYear.UseVisualStyleBackColor = true;
            this.btnAddNewPCYear.Click += new System.EventHandler(this.btnAddNewPCYear_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupPandelDayTypes);
            this.tabPage3.Controls.Add(this.dtpHolydayMonthPicker);
            this.tabPage3.Controls.Add(this.pbGeneratingWeekEnds);
            this.tabPage3.Controls.Add(this.btnGenerateWeekEnds);
            this.tabPage3.Controls.Add(this.btnEditSaveDayType);
            this.tabPage3.Controls.Add(this.cHolydayCalendar);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(448, 379);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Праздники/Выходные";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupPandelDayTypes
            // 
            this.groupPandelDayTypes.Controls.Add(this.rbUsualDay);
            this.groupPandelDayTypes.Controls.Add(this.rbWeekEnd);
            this.groupPandelDayTypes.Controls.Add(this.rbHolyDay);
            this.groupPandelDayTypes.Controls.Add(this.rbShortDay);
            this.groupPandelDayTypes.Enabled = false;
            this.groupPandelDayTypes.Location = new System.Drawing.Point(182, 9);
            this.groupPandelDayTypes.Name = "groupPandelDayTypes";
            this.groupPandelDayTypes.Size = new System.Drawing.Size(174, 95);
            this.groupPandelDayTypes.TabIndex = 7;
            // 
            // rbUsualDay
            // 
            this.rbUsualDay.AutoSize = true;
            this.rbUsualDay.Checked = true;
            this.rbUsualDay.Location = new System.Drawing.Point(3, 3);
            this.rbUsualDay.Name = "rbUsualDay";
            this.rbUsualDay.Size = new System.Drawing.Size(99, 17);
            this.rbUsualDay.TabIndex = 1;
            this.rbUsualDay.TabStop = true;
            this.rbUsualDay.Text = "Обычный день";
            this.rbUsualDay.UseVisualStyleBackColor = true;
            // 
            // rbWeekEnd
            // 
            this.rbWeekEnd.AutoSize = true;
            this.rbWeekEnd.Location = new System.Drawing.Point(3, 26);
            this.rbWeekEnd.Name = "rbWeekEnd";
            this.rbWeekEnd.Size = new System.Drawing.Size(75, 17);
            this.rbWeekEnd.TabIndex = 1;
            this.rbWeekEnd.Text = "Выходной";
            this.rbWeekEnd.UseVisualStyleBackColor = true;
            // 
            // rbHolyDay
            // 
            this.rbHolyDay.AutoSize = true;
            this.rbHolyDay.Location = new System.Drawing.Point(3, 49);
            this.rbHolyDay.Name = "rbHolyDay";
            this.rbHolyDay.Size = new System.Drawing.Size(75, 17);
            this.rbHolyDay.TabIndex = 1;
            this.rbHolyDay.Text = "Праздник";
            this.rbHolyDay.UseVisualStyleBackColor = true;
            // 
            // rbShortDay
            // 
            this.rbShortDay.AutoSize = true;
            this.rbShortDay.Location = new System.Drawing.Point(3, 72);
            this.rbShortDay.Name = "rbShortDay";
            this.rbShortDay.Size = new System.Drawing.Size(168, 17);
            this.rbShortDay.TabIndex = 1;
            this.rbShortDay.Text = "Сокращенный рабочий день";
            this.rbShortDay.UseVisualStyleBackColor = true;
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
            this.pbGeneratingWeekEnds.Location = new System.Drawing.Point(3, 358);
            this.pbGeneratingWeekEnds.MarqueeAnimationSpeed = 10;
            this.pbGeneratingWeekEnds.Maximum = 366;
            this.pbGeneratingWeekEnds.Name = "pbGeneratingWeekEnds";
            this.pbGeneratingWeekEnds.Size = new System.Drawing.Size(442, 18);
            this.pbGeneratingWeekEnds.TabIndex = 3;
            this.pbGeneratingWeekEnds.Visible = false;
            // 
            // btnGenerateWeekEnds
            // 
            this.btnGenerateWeekEnds.Location = new System.Drawing.Point(9, 171);
            this.btnGenerateWeekEnds.Name = "btnGenerateWeekEnds";
            this.btnGenerateWeekEnds.Size = new System.Drawing.Size(154, 34);
            this.btnGenerateWeekEnds.TabIndex = 2;
            this.btnGenerateWeekEnds.Text = "Сделать все Сб и Вс выходными днями";
            this.btnGenerateWeekEnds.UseVisualStyleBackColor = true;
            this.btnGenerateWeekEnds.Click += new System.EventHandler(this.btnGenerateWeekEnds_Click);
            // 
            // btnEditSaveDayType
            // 
            this.btnEditSaveDayType.Image = global::TimeSheetManger.Properties.Resources.Edit_16x16;
            this.btnEditSaveDayType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditSaveDayType.Location = new System.Drawing.Point(182, 110);
            this.btnEditSaveDayType.Name = "btnEditSaveDayType";
            this.btnEditSaveDayType.Size = new System.Drawing.Size(112, 23);
            this.btnEditSaveDayType.TabIndex = 6;
            this.btnEditSaveDayType.Text = "Редактировать";
            this.btnEditSaveDayType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditSaveDayType.UseVisualStyleBackColor = true;
            this.btnEditSaveDayType.Click += new System.EventHandler(this.btnEditSaveDayType_Click);
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
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 195);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(195, 23);
            this.progressBar.TabIndex = 14;
            this.progressBar.Visible = false;
            // 
            // AdminPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 429);
            this.Controls.Add(this.tbContent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdminPanelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Панель администратора";
            this.Load += new System.EventHandler(this.AdminPanelForm_Load);
            this.tbContent.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gbColors.ResumeLayout(false);
            this.gbColors.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupPanelSelectControls.ResumeLayout(false);
            this.groupPanelSelectControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAllMonthData)).EndInit();
            this.gbAddMonth.ResumeLayout(false);
            this.gbAddName.ResumeLayout(false);
            this.gbAddName.PerformLayout();
            this.gbValues.ResumeLayout(false);
            this.gbValues.PerformLayout();
            this.gbAddYear.ResumeLayout(false);
            this.gbAddYear.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupPandelDayTypes.ResumeLayout(false);
            this.groupPandelDayTypes.PerformLayout();
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
        private System.Windows.Forms.Button btnEditSaveDaysHours;
        private System.Windows.Forms.GroupBox gbValues;
        private System.Windows.Forms.GroupBox gbAddName;
        private System.Windows.Forms.Button btnAddNewPCName;
        private System.Windows.Forms.TextBox tbPNewCName;
        private System.Windows.Forms.GroupBox gbAddYear;
        private System.Windows.Forms.Button btnAddNewPCYear;
        private System.Windows.Forms.GroupBox gbAddMonth;
        private System.Windows.Forms.Button btnAddNewPCMonth;
        private System.Windows.Forms.Button btnHideMonth;
        private System.Windows.Forms.Button btnHideYear;
        private System.Windows.Forms.Button btnHideName;
        private SwarthyComponents.WinForms.CalendarView cHolydayCalendar;
        private System.Windows.Forms.DateTimePicker dtpHolydayMonthPicker;
        private System.Windows.Forms.Label lbweekend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel cpWeekEnd;
        private System.Windows.Forms.Panel cpShortDay;
        private System.Windows.Forms.Panel cpHolyday;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColorDialog cdDayColors;
        private System.Windows.Forms.GroupBox gbColors;
        private System.Windows.Forms.TextBox tbPYear;
        private System.Windows.Forms.ComboBox cbPAddMonth;
        private System.Windows.Forms.DataGridView dgAllMonthData;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMonth;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmDays;
        private System.Windows.Forms.Panel groupPanelSelectControls;
        private System.Windows.Forms.Button btnEditSaveDayType;
        private System.Windows.Forms.Panel groupPandelDayTypes;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExportTimeSheetXML;
        private System.Windows.Forms.Button btnExportTimeSheetDBF;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}