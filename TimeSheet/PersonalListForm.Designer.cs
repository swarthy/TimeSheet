namespace TimeSheetManger
{
    partial class PersonalListForm
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
            this.lbDepartmentLabel = new System.Windows.Forms.Label();
            this.lbDepartmentName = new System.Windows.Forms.Label();
            this.lbUsersPDP = new System.Windows.Forms.ListBox();
            this.cmsItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.удалитьИзЭтогоСпискаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.llbAddPersonal = new System.Windows.Forms.LinkLabel();
            this.btnSelectPersonal = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmsItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbDepartmentLabel
            // 
            this.lbDepartmentLabel.AutoSize = true;
            this.lbDepartmentLabel.Location = new System.Drawing.Point(12, 9);
            this.lbDepartmentLabel.Name = "lbDepartmentLabel";
            this.lbDepartmentLabel.Size = new System.Drawing.Size(65, 13);
            this.lbDepartmentLabel.TabIndex = 0;
            this.lbDepartmentLabel.Text = "Отделение:";
            // 
            // lbDepartmentName
            // 
            this.lbDepartmentName.AutoSize = true;
            this.lbDepartmentName.Location = new System.Drawing.Point(83, 9);
            this.lbDepartmentName.Name = "lbDepartmentName";
            this.lbDepartmentName.Size = new System.Drawing.Size(60, 13);
            this.lbDepartmentName.TabIndex = 1;
            this.lbDepartmentName.Text = "отделение";
            // 
            // lbUsersPDP
            // 
            this.lbUsersPDP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbUsersPDP.ContextMenuStrip = this.cmsItem;
            this.lbUsersPDP.FormattingEnabled = true;
            this.lbUsersPDP.Location = new System.Drawing.Point(12, 38);
            this.lbUsersPDP.Name = "lbUsersPDP";
            this.lbUsersPDP.Size = new System.Drawing.Size(289, 277);
            this.lbUsersPDP.TabIndex = 2;
            this.lbUsersPDP.SelectedIndexChanged += new System.EventHandler(this.lbPersonal_SelectedIndexChanged);
            this.lbUsersPDP.DoubleClick += new System.EventHandler(this.lbPersonal_DoubleClick);
            this.lbUsersPDP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbPersonal_MouseDown);
            // 
            // cmsItem
            // 
            this.cmsItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьИзЭтогоСпискаToolStripMenuItem});
            this.cmsItem.Name = "cmsItem";
            this.cmsItem.Size = new System.Drawing.Size(208, 48);
            // 
            // удалитьИзЭтогоСпискаToolStripMenuItem
            // 
            this.удалитьИзЭтогоСпискаToolStripMenuItem.Name = "удалитьИзЭтогоСпискаToolStripMenuItem";
            this.удалитьИзЭтогоСпискаToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.удалитьИзЭтогоСпискаToolStripMenuItem.Text = "Удалить из этого списка";
            this.удалитьИзЭтогоСпискаToolStripMenuItem.Click += new System.EventHandler(this.удалитьИзЭтогоСпискаToolStripMenuItem_Click);
            // 
            // llbAddPersonal
            // 
            this.llbAddPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.llbAddPersonal.AutoSize = true;
            this.llbAddPersonal.Location = new System.Drawing.Point(9, 344);
            this.llbAddPersonal.Name = "llbAddPersonal";
            this.llbAddPersonal.Size = new System.Drawing.Size(191, 13);
            this.llbAddPersonal.TabIndex = 3;
            this.llbAddPersonal.TabStop = true;
            this.llbAddPersonal.Text = "Добавить сотрудника в этот список";
            this.llbAddPersonal.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llbAddPersonal_LinkClicked);
            // 
            // btnSelectPersonal
            // 
            this.btnSelectPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPersonal.Enabled = false;
            this.btnSelectPersonal.Location = new System.Drawing.Point(216, 339);
            this.btnSelectPersonal.Name = "btnSelectPersonal";
            this.btnSelectPersonal.Size = new System.Drawing.Size(85, 23);
            this.btnSelectPersonal.TabIndex = 4;
            this.btnSelectPersonal.Text = "Выбор";
            this.btnSelectPersonal.UseVisualStyleBackColor = true;
            this.btnSelectPersonal.Click += new System.EventHandler(this.btnSelectPersonal_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(216, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // PersonalListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 403);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelectPersonal);
            this.Controls.Add(this.lbUsersPDP);
            this.Controls.Add(this.llbAddPersonal);
            this.Controls.Add(this.lbDepartmentName);
            this.Controls.Add(this.lbDepartmentLabel);
            this.Name = "PersonalListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Сотрудники пользователя";
            this.Load += new System.EventHandler(this.PersonalListForm_Load);
            this.cmsItem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbDepartmentLabel;
        private System.Windows.Forms.Label lbDepartmentName;
        private System.Windows.Forms.ListBox lbUsersPDP;
        private System.Windows.Forms.LinkLabel llbAddPersonal;
        private System.Windows.Forms.Button btnSelectPersonal;
        private System.Windows.Forms.ContextMenuStrip cmsItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьИзЭтогоСпискаToolStripMenuItem;
        private System.Windows.Forms.Button btnCancel;
    }
}