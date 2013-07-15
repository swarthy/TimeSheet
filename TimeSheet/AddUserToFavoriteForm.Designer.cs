namespace TimeSheet
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
            this.lbPersonal = new System.Windows.Forms.ListBox();
            this.lbDepartmentName = new System.Windows.Forms.Label();
            this.lbDepartmentLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectPersonal
            // 
            this.btnSelectPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPersonal.Enabled = false;
            this.btnSelectPersonal.Location = new System.Drawing.Point(496, 334);
            this.btnSelectPersonal.Name = "btnSelectPersonal";
            this.btnSelectPersonal.Size = new System.Drawing.Size(85, 23);
            this.btnSelectPersonal.TabIndex = 9;
            this.btnSelectPersonal.Text = "Выбор";
            this.btnSelectPersonal.UseVisualStyleBackColor = true;
            this.btnSelectPersonal.Click += new System.EventHandler(this.btnSelectPersonal_Click);
            // 
            // lbPersonal
            // 
            this.lbPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPersonal.FormattingEnabled = true;
            this.lbPersonal.Location = new System.Drawing.Point(12, 35);
            this.lbPersonal.Name = "lbPersonal";
            this.lbPersonal.Size = new System.Drawing.Size(569, 290);
            this.lbPersonal.TabIndex = 7;
            this.lbPersonal.SelectedIndexChanged += new System.EventHandler(this.lbPersonal_SelectedIndexChanged);
            this.lbPersonal.DoubleClick += new System.EventHandler(this.lbPersonal_DoubleClick);
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
            // AddUserToFavoriteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 369);
            this.Controls.Add(this.btnSelectPersonal);
            this.Controls.Add(this.lbPersonal);
            this.Controls.Add(this.lbDepartmentName);
            this.Controls.Add(this.lbDepartmentLabel);
            this.Name = "AddUserToFavoriteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Персонал отделения";
            this.Load += new System.EventHandler(this.AddUserToFavoriteForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectPersonal;
        private System.Windows.Forms.ListBox lbPersonal;
        private System.Windows.Forms.Label lbDepartmentName;
        private System.Windows.Forms.Label lbDepartmentLabel;
    }
}