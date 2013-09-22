namespace TimeSheetManager
{
    partial class FlagsForm
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
            this.tbMinutes = new System.Windows.Forms.TextBox();
            this.lbValue = new System.Windows.Forms.Label();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.pFlags = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // tbMinutes
            // 
            this.tbMinutes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMinutes.Location = new System.Drawing.Point(67, 51);
            this.tbMinutes.Name = "tbMinutes";
            this.tbMinutes.Size = new System.Drawing.Size(47, 20);
            this.tbMinutes.TabIndex = 5;
            this.tbMinutes.Text = "0";
            this.tbMinutes.Enter += new System.EventHandler(this.tbMinutes_Enter);
            this.tbMinutes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbHours_KeyDown);
            this.tbMinutes.Leave += new System.EventHandler(this.tbMinutes_Leave);
            // 
            // lbValue
            // 
            this.lbValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbValue.AutoSize = true;
            this.lbValue.Location = new System.Drawing.Point(12, 54);
            this.lbValue.Name = "lbValue";
            this.lbValue.Size = new System.Drawing.Size(49, 13);
            this.lbValue.TabIndex = 5;
            this.lbValue.Text = "Минуты:";
            // 
            // tbHours
            // 
            this.tbHours.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbHours.Location = new System.Drawing.Point(67, 25);
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(47, 20);
            this.tbHours.TabIndex = 4;
            this.tbHours.Text = "0";
            this.tbHours.Enter += new System.EventHandler(this.tbHours_Enter);
            this.tbHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbHours_KeyDown);
            this.tbHours.Leave += new System.EventHandler(this.tbHours_Leave);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Часы:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::TimeSheetManager.Properties.Resources.Cancel_16x16;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(120, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Image = global::TimeSheetManager.Properties.Resources.Check_16x16;
            this.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOk.Location = new System.Drawing.Point(120, 21);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(78, 24);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Ок";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pFlags
            // 
            this.pFlags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pFlags.AutoSize = true;
            this.pFlags.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pFlags.Location = new System.Drawing.Point(12, 12);
            this.pFlags.Name = "pFlags";
            this.pFlags.Size = new System.Drawing.Size(0, 0);
            this.pFlags.TabIndex = 7;
            // 
            // FlagsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(209, 84);
            this.ControlBox = false;
            this.Controls.Add(this.pFlags);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbHours);
            this.Controls.Add(this.lbValue);
            this.Controls.Add(this.tbMinutes);
            this.Controls.Add(this.btnOk);
            this.Name = "FlagsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Показатели";
            this.Load += new System.EventHandler(this.FlagsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox tbMinutes;
        private System.Windows.Forms.Label lbValue;
        private System.Windows.Forms.TextBox tbHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pFlags;
    }
}