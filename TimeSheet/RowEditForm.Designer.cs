namespace TimeSheetManager
{
    partial class RowEditForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RowEditForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnSelectPersonal = new System.Windows.Forms.Button();
            this.cbPost = new System.Windows.Forms.ComboBox();
            this.tbRate = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCalendar = new System.Windows.Forms.ComboBox();
            this.cbDefaultValues = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbOrder = new System.Windows.Forms.Label();
            this.cbRate = new System.Windows.Forms.RadioButton();
            this.cbPercent = new System.Windows.Forms.RadioButton();
            this.tbPercent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tbPrioriry = new SwarthyComponents.WinForms.FocusTextBox();
            this.tbPercentDays = new SwarthyComponents.WinForms.FocusTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сотрудник:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Должность:";
            // 
            // tbName
            // 
            this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbName.Location = new System.Drawing.Point(114, 22);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(253, 20);
            this.tbName.TabIndex = 1;
            // 
            // btnSelectPersonal
            // 
            this.btnSelectPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPersonal.Location = new System.Drawing.Point(373, 20);
            this.btnSelectPersonal.Name = "btnSelectPersonal";
            this.btnSelectPersonal.Size = new System.Drawing.Size(75, 22);
            this.btnSelectPersonal.TabIndex = 2;
            this.btnSelectPersonal.Text = "Список";
            this.btnSelectPersonal.UseVisualStyleBackColor = true;
            this.btnSelectPersonal.Click += new System.EventHandler(this.btnSelectPersonal_Click);
            // 
            // cbPost
            // 
            this.cbPost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbPost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPost.FormattingEnabled = true;
            this.cbPost.Location = new System.Drawing.Point(114, 50);
            this.cbPost.Name = "cbPost";
            this.cbPost.Size = new System.Drawing.Size(334, 21);
            this.cbPost.TabIndex = 3;
            // 
            // tbRate
            // 
            this.tbRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRate.Location = new System.Drawing.Point(114, 112);
            this.tbRate.Name = "tbRate";
            this.tbRate.Size = new System.Drawing.Size(334, 20);
            this.tbRate.TabIndex = 5;
            this.tbRate.Text = "1,0";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(292, 235);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Ок";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Календарь:";
            // 
            // cbCalendar
            // 
            this.cbCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCalendar.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbCalendar.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbCalendar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalendar.FormattingEnabled = true;
            this.cbCalendar.Location = new System.Drawing.Point(114, 80);
            this.cbCalendar.Name = "cbCalendar";
            this.cbCalendar.Size = new System.Drawing.Size(334, 21);
            this.cbCalendar.TabIndex = 4;
            // 
            // cbDefaultValues
            // 
            this.cbDefaultValues.AutoSize = true;
            this.cbDefaultValues.Location = new System.Drawing.Point(15, 139);
            this.cbDefaultValues.Name = "cbDefaultValues";
            this.cbDefaultValues.Size = new System.Drawing.Size(218, 17);
            this.cbDefaultValues.TabIndex = 6;
            this.cbDefaultValues.Text = "Заполнить значениями по умолчанию";
            this.toolTip.SetToolTip(this.cbDefaultValues, resources.GetString("cbDefaultValues.ToolTip"));
            this.cbDefaultValues.UseVisualStyleBackColor = true;
            this.cbDefaultValues.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(373, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lbOrder
            // 
            this.lbOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbOrder.AutoSize = true;
            this.lbOrder.Location = new System.Drawing.Point(12, 240);
            this.lbOrder.Name = "lbOrder";
            this.lbOrder.Size = new System.Drawing.Size(64, 13);
            this.lbOrder.TabIndex = 9;
            this.lbOrder.Text = "Приоритет:";
            // 
            // cbRate
            // 
            this.cbRate.AutoSize = true;
            this.cbRate.Checked = true;
            this.cbRate.Location = new System.Drawing.Point(15, 113);
            this.cbRate.Name = "cbRate";
            this.cbRate.Size = new System.Drawing.Size(64, 17);
            this.cbRate.TabIndex = 11;
            this.cbRate.TabStop = true;
            this.cbRate.Text = "Ставка:";
            this.cbRate.UseVisualStyleBackColor = true;
            this.cbRate.CheckedChanged += new System.EventHandler(this.cbRate_CheckedChanged);
            // 
            // cbPercent
            // 
            this.cbPercent.AutoSize = true;
            this.cbPercent.Location = new System.Drawing.Point(15, 163);
            this.cbPercent.Name = "cbPercent";
            this.cbPercent.Size = new System.Drawing.Size(79, 17);
            this.cbPercent.TabIndex = 12;
            this.cbPercent.Text = "Проценты:";
            this.cbPercent.UseVisualStyleBackColor = true;
            this.cbPercent.CheckedChanged += new System.EventHandler(this.cbPercent_CheckedChanged);
            // 
            // tbPercent
            // 
            this.tbPercent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPercent.Enabled = false;
            this.tbPercent.Location = new System.Drawing.Point(114, 162);
            this.tbPercent.Name = "tbPercent";
            this.tbPercent.Size = new System.Drawing.Size(334, 20);
            this.tbPercent.TabIndex = 6;
            this.tbPercent.Text = "100";
            this.toolTip.SetToolTip(this.tbPercent, "Проценты не могут быть < 0 или > 100");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Количество дней:";
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 5;
            this.toolTip.AutoPopDelay = 8000;
            this.toolTip.InitialDelay = 50;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 1;
            // 
            // tbPrioriry
            // 
            this.tbPrioriry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbPrioriry.Location = new System.Drawing.Point(114, 237);
            this.tbPrioriry.Name = "tbPrioriry";
            this.tbPrioriry.Size = new System.Drawing.Size(28, 20);
            this.tbPrioriry.TabIndex = 10;
            this.tbPrioriry.Text = "0";
            this.toolTip.SetToolTip(this.tbPrioriry, resources.GetString("tbPrioriry.ToolTip"));
            // 
            // tbPercentDays
            // 
            this.tbPercentDays.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPercentDays.Enabled = false;
            this.tbPercentDays.Location = new System.Drawing.Point(114, 188);
            this.tbPercentDays.Name = "tbPercentDays";
            this.tbPercentDays.Size = new System.Drawing.Size(334, 20);
            this.tbPercentDays.TabIndex = 6;
            this.tbPercentDays.Text = "0";
            this.toolTip.SetToolTip(this.tbPercentDays, "Количество дней не может быть <= 0");
            // 
            // RowEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(460, 268);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbPercent);
            this.Controls.Add(this.cbRate);
            this.Controls.Add(this.tbPrioriry);
            this.Controls.Add(this.lbOrder);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbDefaultValues);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.tbPercentDays);
            this.Controls.Add(this.tbPercent);
            this.Controls.Add(this.tbRate);
            this.Controls.Add(this.cbCalendar);
            this.Controls.Add(this.cbPost);
            this.Controls.Add(this.btnSelectPersonal);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(330, 170);
            this.Name = "RowEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Запись в табеле";
            this.Load += new System.EventHandler(this.RowEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnSelectPersonal;
        private System.Windows.Forms.ComboBox cbPost;
        private System.Windows.Forms.TextBox tbRate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbCalendar;
        private System.Windows.Forms.CheckBox cbDefaultValues;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbOrder;
        private SwarthyComponents.WinForms.FocusTextBox tbPrioriry;
        private System.Windows.Forms.RadioButton cbRate;
        private System.Windows.Forms.RadioButton cbPercent;
        private System.Windows.Forms.TextBox tbPercent;
        private SwarthyComponents.WinForms.FocusTextBox tbPercentDays;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip;
    }
}