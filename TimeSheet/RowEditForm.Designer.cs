namespace TimeSheetManger
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnSelectPersonal = new System.Windows.Forms.Button();
            this.cbPost = new System.Windows.Forms.ComboBox();
            this.tbRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCalendar = new System.Windows.Forms.ComboBox();
            this.cbDefaultValues = new System.Windows.Forms.CheckBox();
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
            this.tbName.Location = new System.Drawing.Point(81, 22);
            this.tbName.Name = "tbName";
            this.tbName.ReadOnly = true;
            this.tbName.Size = new System.Drawing.Size(150, 20);
            this.tbName.TabIndex = 1;
            // 
            // btnSelectPersonal
            // 
            this.btnSelectPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPersonal.Location = new System.Drawing.Point(237, 20);
            this.btnSelectPersonal.Name = "btnSelectPersonal";
            this.btnSelectPersonal.Size = new System.Drawing.Size(75, 23);
            this.btnSelectPersonal.TabIndex = 2;
            this.btnSelectPersonal.Text = "Выбрать";
            this.btnSelectPersonal.UseVisualStyleBackColor = true;
            this.btnSelectPersonal.Click += new System.EventHandler(this.btnSelectPersonal_Click);
            // 
            // cbPost
            // 
            this.cbPost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPost.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPost.FormattingEnabled = true;
            this.cbPost.Location = new System.Drawing.Point(81, 50);
            this.cbPost.Name = "cbPost";
            this.cbPost.Size = new System.Drawing.Size(150, 21);
            this.cbPost.TabIndex = 3;
            // 
            // tbRate
            // 
            this.tbRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRate.Location = new System.Drawing.Point(81, 112);
            this.tbRate.Name = "tbRate";
            this.tbRate.Size = new System.Drawing.Size(150, 20);
            this.tbRate.TabIndex = 4;
            this.tbRate.Text = "1,0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ставка:";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(237, 133);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
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
            this.cbCalendar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCalendar.FormattingEnabled = true;
            this.cbCalendar.Location = new System.Drawing.Point(81, 80);
            this.cbCalendar.Name = "cbCalendar";
            this.cbCalendar.Size = new System.Drawing.Size(150, 21);
            this.cbCalendar.TabIndex = 3;
            // 
            // cbDefaultValues
            // 
            this.cbDefaultValues.AutoSize = true;
            this.cbDefaultValues.Location = new System.Drawing.Point(15, 139);
            this.cbDefaultValues.Name = "cbDefaultValues";
            this.cbDefaultValues.Size = new System.Drawing.Size(218, 17);
            this.cbDefaultValues.TabIndex = 7;
            this.cbDefaultValues.Text = "Заполнить значениями по умолчанию";
            this.cbDefaultValues.UseVisualStyleBackColor = true;
            this.cbDefaultValues.Visible = false;
            // 
            // RowEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 168);
            this.Controls.Add(this.cbDefaultValues);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbCalendar;
        private System.Windows.Forms.CheckBox cbDefaultValues;
    }
}