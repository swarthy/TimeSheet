namespace TimeSheetManger
{
    partial class TimeSheets
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
            this.lbTimeSheets = new System.Windows.Forms.ListBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbTimeSheets
            // 
            this.lbTimeSheets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTimeSheets.FormattingEnabled = true;
            this.lbTimeSheets.Location = new System.Drawing.Point(12, 12);
            this.lbTimeSheets.Name = "lbTimeSheets";
            this.lbTimeSheets.Size = new System.Drawing.Size(387, 277);
            this.lbTimeSheets.TabIndex = 0;
            this.lbTimeSheets.SelectedIndexChanged += new System.EventHandler(this.lbTimeSheets_SelectedIndexChanged);
            this.lbTimeSheets.DoubleClick += new System.EventHandler(this.lbTimeSheets_DoubleClick);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNew.Location = new System.Drawing.Point(12, 300);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 1;
            this.btnNew.Text = "Новый";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(93, 300);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Открыть";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(324, 300);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // TimeSheets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 335);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.lbTimeSheets);
            this.Name = "TimeSheets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Табели";
            this.Load += new System.EventHandler(this.TimeSheets_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbTimeSheets;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnDelete;
    }
}