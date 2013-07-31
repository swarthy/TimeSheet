namespace TimeSheetManger
{
    partial class CatalogEditorForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.flEditBox = new System.Windows.Forms.FlowLayoutPanel();
            this.flFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.gbValues = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnFiltersEnable = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.bs = new System.Windows.Forms.BindingSource(this.components);
            this.grid = new TimeSheetManger.MyDataGridView();
            this.gbFilters.SuspendLayout();
            this.gbValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(481, 289);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // flEditBox
            // 
            this.flEditBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flEditBox.Location = new System.Drawing.Point(3, 16);
            this.flEditBox.Name = "flEditBox";
            this.flEditBox.Size = new System.Drawing.Size(538, 28);
            this.flEditBox.TabIndex = 5;
            // 
            // flFilters
            // 
            this.flFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flFilters.Location = new System.Drawing.Point(3, 16);
            this.flFilters.Name = "flFilters";
            this.flFilters.Size = new System.Drawing.Size(538, 31);
            this.flFilters.TabIndex = 7;
            // 
            // gbFilters
            // 
            this.gbFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFilters.Controls.Add(this.flFilters);
            this.gbFilters.Location = new System.Drawing.Point(12, 180);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(544, 50);
            this.gbFilters.TabIndex = 8;
            this.gbFilters.TabStop = false;
            this.gbFilters.Text = "Фильтры";
            this.gbFilters.Visible = false;
            // 
            // gbValues
            // 
            this.gbValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbValues.Controls.Add(this.flEditBox);
            this.gbValues.Location = new System.Drawing.Point(12, 236);
            this.gbValues.Name = "gbValues";
            this.gbValues.Size = new System.Drawing.Size(544, 47);
            this.gbValues.TabIndex = 9;
            this.gbValues.TabStop = false;
            this.gbValues.Text = "Значения";
            this.gbValues.Visible = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Image = global::TimeSheetManger.Properties.Resources.Print_16x16;
            this.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrint.Location = new System.Drawing.Point(400, 289);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 11;
            this.btnPrint.Text = "Печать";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Image = global::TimeSheetManger.Properties.Resources.Add_16x16;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(105, 289);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(86, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnFiltersEnable
            // 
            this.btnFiltersEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFiltersEnable.Image = global::TimeSheetManger.Properties.Resources.Rename_16x16;
            this.btnFiltersEnable.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFiltersEnable.Location = new System.Drawing.Point(15, 289);
            this.btnFiltersEnable.Name = "btnFiltersEnable";
            this.btnFiltersEnable.Size = new System.Drawing.Size(84, 23);
            this.btnFiltersEnable.TabIndex = 6;
            this.btnFiltersEnable.Text = "Фильтры";
            this.btnFiltersEnable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFiltersEnable.UseVisualStyleBackColor = true;
            this.btnFiltersEnable.Click += new System.EventHandler(this.btnFiltersEnable_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Image = global::TimeSheetManger.Properties.Resources.Delete_16x16;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(197, 289);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(12, 12);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.ShowEditingIcon = false;
            this.grid.Size = new System.Drawing.Size(544, 218);
            this.grid.TabIndex = 0;
            this.grid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grid_DataError);
            // 
            // CatalogEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(568, 324);
            this.ControlBox = false;
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.gbValues);
            this.Controls.Add(this.gbFilters);
            this.Controls.Add(this.btnFiltersEnable);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Name = "CatalogEditorForm";
            this.Text = "Справочник";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CatalogEditorForm_Load);
            this.gbFilters.ResumeLayout(false);
            this.gbValues.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyDataGridView grid;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FlowLayoutPanel flEditBox;
        private System.Windows.Forms.Button btnFiltersEnable;
        private System.Windows.Forms.FlowLayoutPanel flFilters;
        private System.Windows.Forms.GroupBox gbFilters;
        private System.Windows.Forms.GroupBox gbValues;
        private System.Windows.Forms.BindingSource bs;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnPrint;
    }
}