namespace Server
{
    partial class ServerForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerForm));
            this.tcWorkspace = new System.Windows.Forms.TabControl();
            this.tbUsers = new System.Windows.Forms.TabPage();
            this.gbManagment = new System.Windows.Forms.GroupBox();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbSelected = new System.Windows.Forms.RadioButton();
            this.lbOnline = new System.Windows.Forms.ListBox();
            this.gbUserInfo = new System.Windows.Forms.GroupBox();
            this.lbUserInfoText = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShutDown = new System.Windows.Forms.Button();
            this.tbReason = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timeout = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssOnlineCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerInfoUpdater = new System.Windows.Forms.Timer(this.components);
            this.lbCountDown = new System.Windows.Forms.Label();
            this.tcWorkspace.SuspendLayout();
            this.tbUsers.SuspendLayout();
            this.gbManagment.SuspendLayout();
            this.gbUserInfo.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeout)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcWorkspace
            // 
            this.tcWorkspace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcWorkspace.Controls.Add(this.tbUsers);
            this.tcWorkspace.Controls.Add(this.tabPage1);
            this.tcWorkspace.Location = new System.Drawing.Point(12, 12);
            this.tcWorkspace.Name = "tcWorkspace";
            this.tcWorkspace.SelectedIndex = 0;
            this.tcWorkspace.Size = new System.Drawing.Size(702, 340);
            this.tcWorkspace.TabIndex = 0;
            // 
            // tbUsers
            // 
            this.tbUsers.Controls.Add(this.gbManagment);
            this.tbUsers.Controls.Add(this.lbOnline);
            this.tbUsers.Controls.Add(this.gbUserInfo);
            this.tbUsers.Location = new System.Drawing.Point(4, 22);
            this.tbUsers.Name = "tbUsers";
            this.tbUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tbUsers.Size = new System.Drawing.Size(694, 314);
            this.tbUsers.TabIndex = 0;
            this.tbUsers.Text = "Пользователи";
            this.tbUsers.UseVisualStyleBackColor = true;
            // 
            // gbManagment
            // 
            this.gbManagment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbManagment.Controls.Add(this.btnSendMsg);
            this.gbManagment.Controls.Add(this.label5);
            this.gbManagment.Controls.Add(this.tbMessage);
            this.gbManagment.Controls.Add(this.btnDisconnect);
            this.gbManagment.Controls.Add(this.label4);
            this.gbManagment.Controls.Add(this.rbAll);
            this.gbManagment.Controls.Add(this.rbSelected);
            this.gbManagment.Location = new System.Drawing.Point(236, 123);
            this.gbManagment.Name = "gbManagment";
            this.gbManagment.Size = new System.Drawing.Size(452, 176);
            this.gbManagment.TabIndex = 5;
            this.gbManagment.TabStop = false;
            this.gbManagment.Text = "Управление";
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendMsg.Location = new System.Drawing.Point(360, 147);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(85, 23);
            this.btnSendMsg.TabIndex = 8;
            this.btnSendMsg.Text = "Отправить";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Сообщение:";
            // 
            // tbMessage
            // 
            this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessage.Location = new System.Drawing.Point(9, 63);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(345, 107);
            this.tbMessage.TabIndex = 6;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(274, 18);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(130, 23);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Отключить безопасно";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Пользователи:";
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(224, 19);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(44, 17);
            this.rbAll.TabIndex = 3;
            this.rbAll.Text = "Все";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rbSelected
            // 
            this.rbSelected.AutoSize = true;
            this.rbSelected.Checked = true;
            this.rbSelected.Location = new System.Drawing.Point(95, 19);
            this.rbSelected.Name = "rbSelected";
            this.rbSelected.Size = new System.Drawing.Size(123, 17);
            this.rbSelected.TabIndex = 2;
            this.rbSelected.TabStop = true;
            this.rbSelected.Text = "Только выбранные";
            this.rbSelected.UseVisualStyleBackColor = true;
            // 
            // lbOnline
            // 
            this.lbOnline.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOnline.FormattingEnabled = true;
            this.lbOnline.Location = new System.Drawing.Point(6, 6);
            this.lbOnline.Name = "lbOnline";
            this.lbOnline.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbOnline.Size = new System.Drawing.Size(224, 290);
            this.lbOnline.TabIndex = 0;
            this.lbOnline.SelectedIndexChanged += new System.EventHandler(this.lbOnline_SelectedIndexChanged);
            // 
            // gbUserInfo
            // 
            this.gbUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbUserInfo.Controls.Add(this.lbUserInfoText);
            this.gbUserInfo.Location = new System.Drawing.Point(236, 6);
            this.gbUserInfo.Name = "gbUserInfo";
            this.gbUserInfo.Size = new System.Drawing.Size(452, 111);
            this.gbUserInfo.TabIndex = 0;
            this.gbUserInfo.TabStop = false;
            this.gbUserInfo.Text = "Информация";
            // 
            // lbUserInfoText
            // 
            this.lbUserInfoText.AutoSize = true;
            this.lbUserInfoText.Location = new System.Drawing.Point(6, 16);
            this.lbUserInfoText.Name = "lbUserInfoText";
            this.lbUserInfoText.Size = new System.Drawing.Size(35, 13);
            this.lbUserInfoText.TabIndex = 0;
            this.lbUserInfoText.Text = "label1";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(694, 314);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Сервер";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbCountDown);
            this.groupBox1.Controls.Add(this.btnShutDown);
            this.groupBox1.Controls.Add(this.tbReason);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.timeout);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 213);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выключение сервера";
            // 
            // btnShutDown
            // 
            this.btnShutDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShutDown.Location = new System.Drawing.Point(257, 181);
            this.btnShutDown.Name = "btnShutDown";
            this.btnShutDown.Size = new System.Drawing.Size(75, 23);
            this.btnShutDown.TabIndex = 5;
            this.btnShutDown.Text = "Ок";
            this.btnShutDown.UseVisualStyleBackColor = true;
            this.btnShutDown.Click += new System.EventHandler(this.btnShutDown_Click);
            // 
            // tbReason
            // 
            this.tbReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbReason.Location = new System.Drawing.Point(6, 67);
            this.tbReason.Multiline = true;
            this.tbReason.Name = "tbReason";
            this.tbReason.Size = new System.Drawing.Size(326, 108);
            this.tbReason.TabIndex = 4;
            this.tbReason.Text = "Плановое техническое обслуживание.\r\nРабота будет возобновлена через 1 час.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Причина:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "минут";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Задержка:";
            // 
            // timeout
            // 
            this.timeout.Location = new System.Drawing.Point(73, 19);
            this.timeout.Maximum = new decimal(new int[] {
            7200,
            0,
            0,
            0});
            this.timeout.Name = "timeout";
            this.timeout.Size = new System.Drawing.Size(66, 20);
            this.timeout.TabIndex = 0;
            this.timeout.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssOnlineCount,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 380);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(726, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssOnlineCount
            // 
            this.tssOnlineCount.Name = "tssOnlineCount";
            this.tssOnlineCount.Size = new System.Drawing.Size(62, 17);
            this.tssOnlineCount.Text = "Онлайн: 0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(649, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // timerInfoUpdater
            // 
            this.timerInfoUpdater.Interval = 1000;
            this.timerInfoUpdater.Tick += new System.EventHandler(this.timerInfoUpdater_Tick);
            // 
            // lbCountDown
            // 
            this.lbCountDown.AutoSize = true;
            this.lbCountDown.Location = new System.Drawing.Point(6, 186);
            this.lbCountDown.Name = "lbCountDown";
            this.lbCountDown.Size = new System.Drawing.Size(0, 13);
            this.lbCountDown.TabIndex = 6;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 402);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tcWorkspace);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerForm";
            this.Text = "Tabel Server GUI";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.tcWorkspace.ResumeLayout(false);
            this.tbUsers.ResumeLayout(false);
            this.gbManagment.ResumeLayout(false);
            this.gbManagment.PerformLayout();
            this.gbUserInfo.ResumeLayout(false);
            this.gbUserInfo.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeout)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tcWorkspace;
        private System.Windows.Forms.TabPage tbUsers;
        private System.Windows.Forms.ListBox lbOnline;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssOnlineCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox gbUserInfo;
        private System.Windows.Forms.Label lbUserInfoText;
        private System.Windows.Forms.Timer timerInfoUpdater;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown timeout;
        private System.Windows.Forms.Button btnShutDown;
        private System.Windows.Forms.TextBox tbReason;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbSelected;
        private System.Windows.Forms.GroupBox gbManagment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label lbCountDown;

    }
}

