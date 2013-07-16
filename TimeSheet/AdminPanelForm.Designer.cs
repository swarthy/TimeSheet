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
            this.tbContent.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.tbContent.Size = new System.Drawing.Size(433, 300);
            this.tbContent.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnEditCatalog);
            this.tabPage1.Controls.Add(this.cbCatalogs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(425, 274);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Справочники";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnEditCatalog
            // 
            this.btnEditCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditCatalog.Location = new System.Drawing.Point(321, 6);
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
            this.cbCatalogs.Size = new System.Drawing.Size(309, 21);
            this.cbCatalogs.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(425, 274);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Производственные календари";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(425, 274);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Праздники/Выходные";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // AdminPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 324);
            this.Controls.Add(this.tbContent);
            this.Name = "AdminPanelForm";
            this.Text = "Панель администратора";
            this.tbContent.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbContent;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox cbCatalogs;
        private System.Windows.Forms.Button btnEditCatalog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
    }
}