namespace TimeSheet
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
            this.btnEditCatalog = new System.Windows.Forms.Button();
            this.cbCatalogs = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.calHolydays = new System.Windows.Forms.MonthCalendar();
            this.rbUsualDay = new System.Windows.Forms.RadioButton();
            this.rbWeekEnd = new System.Windows.Forms.RadioButton();
            this.rbHolyDay = new System.Windows.Forms.RadioButton();
            this.rbShortDay = new System.Windows.Forms.RadioButton();
            this.btnGenerateWeekEnds = new System.Windows.Forms.Button();
            this.pbGeneratingWeekEnds = new System.Windows.Forms.ProgressBar();
            this.tbContent.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            this.tbContent.Size = new System.Drawing.Size(402, 227);
            this.tbContent.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnEditCatalog);
            this.tabPage1.Controls.Add(this.cbCatalogs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(394, 185);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Справочники";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnEditCatalog
            // 
            this.btnEditCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCatalog.Location = new System.Drawing.Point(290, 6);
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
            this.cbCatalogs.Items.AddRange(new object[] {
            "Пользователи",
            "Персонал",
            "Должности",
            "Отделения",
            "Показатели",
            "ЛПУ"});
            this.cbCatalogs.Location = new System.Drawing.Point(6, 6);
            this.cbCatalogs.Name = "cbCatalogs";
            this.cbCatalogs.Size = new System.Drawing.Size(278, 21);
            this.cbCatalogs.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(394, 185);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Производственные календари";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.pbGeneratingWeekEnds);
            this.tabPage3.Controls.Add(this.btnGenerateWeekEnds);
            this.tabPage3.Controls.Add(this.rbShortDay);
            this.tabPage3.Controls.Add(this.rbHolyDay);
            this.tabPage3.Controls.Add(this.rbWeekEnd);
            this.tabPage3.Controls.Add(this.rbUsualDay);
            this.tabPage3.Controls.Add(this.calHolydays);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(394, 201);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Праздники/Выходные";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // calHolydays
            // 
            this.calHolydays.Location = new System.Drawing.Point(9, 9);
            this.calHolydays.MaxSelectionCount = 1;
            this.calHolydays.Name = "calHolydays";
            this.calHolydays.ShowWeekNumbers = true;
            this.calHolydays.TabIndex = 0;
            this.calHolydays.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calHolydays_DateChanged);
            // 
            // rbUsualDay
            // 
            this.rbUsualDay.AutoSize = true;
            this.rbUsualDay.Checked = true;
            this.rbUsualDay.Location = new System.Drawing.Point(207, 9);
            this.rbUsualDay.Name = "rbUsualDay";
            this.rbUsualDay.Size = new System.Drawing.Size(99, 17);
            this.rbUsualDay.TabIndex = 1;
            this.rbUsualDay.TabStop = true;
            this.rbUsualDay.Text = "Обычный день";
            this.rbUsualDay.UseVisualStyleBackColor = true;
            this.rbUsualDay.CheckedChanged += new System.EventHandler(this.rbUsualDay_CheckedChanged);
            // 
            // rbWeekEnd
            // 
            this.rbWeekEnd.AutoSize = true;
            this.rbWeekEnd.Location = new System.Drawing.Point(207, 32);
            this.rbWeekEnd.Name = "rbWeekEnd";
            this.rbWeekEnd.Size = new System.Drawing.Size(75, 17);
            this.rbWeekEnd.TabIndex = 1;
            this.rbWeekEnd.Text = "Выходной";
            this.rbWeekEnd.UseVisualStyleBackColor = true;
            this.rbWeekEnd.CheckedChanged += new System.EventHandler(this.rbWeekEnd_CheckedChanged);
            // 
            // rbHolyDay
            // 
            this.rbHolyDay.AutoSize = true;
            this.rbHolyDay.Location = new System.Drawing.Point(207, 55);
            this.rbHolyDay.Name = "rbHolyDay";
            this.rbHolyDay.Size = new System.Drawing.Size(75, 17);
            this.rbHolyDay.TabIndex = 1;
            this.rbHolyDay.Text = "Праздник";
            this.rbHolyDay.UseVisualStyleBackColor = true;
            this.rbHolyDay.CheckedChanged += new System.EventHandler(this.rbHolyDay_CheckedChanged);
            // 
            // rbShortDay
            // 
            this.rbShortDay.AutoSize = true;
            this.rbShortDay.Location = new System.Drawing.Point(207, 78);
            this.rbShortDay.Name = "rbShortDay";
            this.rbShortDay.Size = new System.Drawing.Size(168, 17);
            this.rbShortDay.TabIndex = 1;
            this.rbShortDay.Text = "Сокращенный рабочий день";
            this.rbShortDay.UseVisualStyleBackColor = true;
            this.rbShortDay.CheckedChanged += new System.EventHandler(this.rbShortDay_CheckedChanged);
            // 
            // btnGenerateWeekEnds
            // 
            this.btnGenerateWeekEnds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateWeekEnds.Location = new System.Drawing.Point(207, 130);
            this.btnGenerateWeekEnds.Name = "btnGenerateWeekEnds";
            this.btnGenerateWeekEnds.Size = new System.Drawing.Size(168, 41);
            this.btnGenerateWeekEnds.TabIndex = 2;
            this.btnGenerateWeekEnds.Text = "Сделать все Сб и Вс выходными днями";
            this.btnGenerateWeekEnds.UseVisualStyleBackColor = true;
            this.btnGenerateWeekEnds.Click += new System.EventHandler(this.btnGenerateWeekEnds_Click);
            // 
            // pbGeneratingWeekEnds
            // 
            this.pbGeneratingWeekEnds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbGeneratingWeekEnds.Location = new System.Drawing.Point(9, 177);
            this.pbGeneratingWeekEnds.MarqueeAnimationSpeed = 10;
            this.pbGeneratingWeekEnds.Maximum = 366;
            this.pbGeneratingWeekEnds.Name = "pbGeneratingWeekEnds";
            this.pbGeneratingWeekEnds.Size = new System.Drawing.Size(366, 15);
            this.pbGeneratingWeekEnds.TabIndex = 3;
            this.pbGeneratingWeekEnds.Visible = false;
            // 
            // AdminPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 251);
            this.Controls.Add(this.tbContent);
            this.Name = "AdminPanelForm";
            this.Text = "Панель администратора";
            this.Load += new System.EventHandler(this.AdminPanelForm_Load);
            this.tbContent.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbContent;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cbCatalogs;
        private System.Windows.Forms.Button btnEditCatalog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.MonthCalendar calHolydays;
        private System.Windows.Forms.RadioButton rbShortDay;
        private System.Windows.Forms.RadioButton rbHolyDay;
        private System.Windows.Forms.RadioButton rbWeekEnd;
        private System.Windows.Forms.RadioButton rbUsualDay;
        private System.Windows.Forms.Button btnGenerateWeekEnds;
        private System.Windows.Forms.ProgressBar pbGeneratingWeekEnds;
    }
}