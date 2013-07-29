namespace TimeSheetManger
{
    partial class AboutBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.llIconsSite = new System.Windows.Forms.LinkLabel();
            this.lbIconsSite = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lbProductName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // llIconsSite
            // 
            this.llIconsSite.AutoSize = true;
            this.llIconsSite.Location = new System.Drawing.Point(217, 127);
            this.llIconsSite.Name = "llIconsSite";
            this.llIconsSite.Size = new System.Drawing.Size(113, 13);
            this.llIconsSite.TabIndex = 0;
            this.llIconsSite.TabStop = true;
            this.llIconsSite.Text = "www.visualpharm.com";
            this.llIconsSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llIconsSite_LinkClicked);
            // 
            // lbIconsSite
            // 
            this.lbIconsSite.AutoSize = true;
            this.lbIconsSite.Location = new System.Drawing.Point(12, 127);
            this.lbIconsSite.Name = "lbIconsSite";
            this.lbIconsSite.Size = new System.Drawing.Size(199, 13);
            this.lbIconsSite.TabIndex = 1;
            this.lbIconsSite.Text = "Изображения предоставлены сайтом";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(253, 166);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Закрыть";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lbProductName
            // 
            this.lbProductName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProductName.Location = new System.Drawing.Point(12, 9);
            this.lbProductName.Name = "lbProductName";
            this.lbProductName.Size = new System.Drawing.Size(316, 17);
            this.lbProductName.TabIndex = 3;
            this.lbProductName.Text = "Учет использования рабочего времени";
            this.lbProductName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 201);
            this.ControlBox = false;
            this.Controls.Add(this.lbProductName);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lbIconsSite);
            this.Controls.Add(this.llIconsSite);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel llIconsSite;
        private System.Windows.Forms.Label lbIconsSite;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lbProductName;
    }
}