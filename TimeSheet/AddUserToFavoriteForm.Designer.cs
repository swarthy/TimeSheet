namespace TimeSheetManger
{
    partial class AddUserToFavoriteForm
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
            this.btnSelectPersonal = new System.Windows.Forms.Button();
            this.lbDepartmentName = new System.Windows.Forms.Label();
            this.lbDepartmentLabel = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbShowAll = new System.Windows.Forms.CheckBox();
            this.cbPersonal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbPost = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectPersonal
            // 
            this.btnSelectPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPersonal.Location = new System.Drawing.Point(230, 118);
            this.btnSelectPersonal.Name = "btnSelectPersonal";
            this.btnSelectPersonal.Size = new System.Drawing.Size(85, 23);
            this.btnSelectPersonal.TabIndex = 9;
            this.btnSelectPersonal.Text = "Выбор";
            this.btnSelectPersonal.UseVisualStyleBackColor = true;
            this.btnSelectPersonal.Click += new System.EventHandler(this.btnSelectPersonal_Click);
            // 
            // lbDepartmentName
            // 
            this.lbDepartmentName.AutoSize = true;
            this.lbDepartmentName.Location = new System.Drawing.Point(83, 6);
            this.lbDepartmentName.Name = "lbDepartmentName";
            this.lbDepartmentName.Size = new System.Drawing.Size(60, 13);
            this.lbDepartmentName.TabIndex = 6;
            this.lbDepartmentName.Text = "отделение";
            // 
            // lbDepartmentLabel
            // 
            this.lbDepartmentLabel.AutoSize = true;
            this.lbDepartmentLabel.Location = new System.Drawing.Point(12, 6);
            this.lbDepartmentLabel.Name = "lbDepartmentLabel";
            this.lbDepartmentLabel.Size = new System.Drawing.Size(65, 13);
            this.lbDepartmentLabel.TabIndex = 5;
            this.lbDepartmentLabel.Text = "Отделение:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(321, 118);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbShowAll
            // 
            this.cbShowAll.AutoSize = true;
            this.cbShowAll.Location = new System.Drawing.Point(15, 58);
            this.cbShowAll.Name = "cbShowAll";
            this.cbShowAll.Size = new System.Drawing.Size(153, 17);
            this.cbShowAll.TabIndex = 11;
            this.cbShowAll.Text = "Показать весь персонал";
            this.cbShowAll.UseVisualStyleBackColor = true;
            this.cbShowAll.CheckedChanged += new System.EventHandler(this.cbShowAll_CheckedChanged);
            // 
            // cbPersonal
            // 
            this.cbPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPersonal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbPersonal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPersonal.DisplayMember = "_FullNameAndNumber";
            this.cbPersonal.FormattingEnabled = true;
            this.cbPersonal.Location = new System.Drawing.Point(86, 31);
            this.cbPersonal.Name = "cbPersonal";
            this.cbPersonal.Size = new System.Drawing.Size(310, 21);
            this.cbPersonal.TabIndex = 12;
            this.cbPersonal.SelectedIndexChanged += new System.EventHandler(this.cbPersonal_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Сотрудник:";
            // 
            // cbPost
            // 
            this.cbPost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbPost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbPost.FormattingEnabled = true;
            this.cbPost.Location = new System.Drawing.Point(86, 81);
            this.cbPost.Name = "cbPost";
            this.cbPost.Size = new System.Drawing.Size(310, 21);
            this.cbPost.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Должность:";
            // 
            // AddUserToFavoriteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(408, 153);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbPost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPersonal);
            this.Controls.Add(this.cbShowAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelectPersonal);
            this.Controls.Add(this.lbDepartmentName);
            this.Controls.Add(this.lbDepartmentLabel);
            this.Name = "AddUserToFavoriteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Персонал";
            this.Load += new System.EventHandler(this.AddUserToFavoriteForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectPersonal;
        private System.Windows.Forms.Label lbDepartmentName;
        private System.Windows.Forms.Label lbDepartmentLabel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbShowAll;
        private System.Windows.Forms.ComboBox cbPersonal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPost;
        private System.Windows.Forms.Label label2;
    }
}