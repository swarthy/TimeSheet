﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetWork;

namespace Server
{
    public partial class ServerForm : Form
    {        
        public ServerForm()
        {
            InitializeComponent();            
        }
        ConnectionInfo selected =  null;

        private void ServerForm_Load(object sender, EventArgs e)
        {   
            refreshOnlineList();
            Server.server.OnClientConnect += delegate { refreshOnlineListCall(); };
            Server.server.OnClientDisconnect += delegate { refreshOnlineListCall(); };
            Server.OnLogin += delegate { refreshOnlineListCall(); };
            Server.OnLogout += delegate { refreshOnlineListCall(); };
        }
        void refreshOnlineListCall()
        {
            if (InvokeRequired) BeginInvoke(new Action(refreshOnlineList)); else refreshOnlineList();
        }
        void refreshOnlineList()
        {
            lbOnline.DataSource = null;
            lbOnline.Items.Clear();
            selected = null;
            lbOnline.DataSource = Server.server.Connections;
            lbOnline.DisplayMember = "ShortInfo";
            lbOnline.Refresh();
            tssOnlineCount.Text = lbOnline.Items.Count.ToString();
        }

        private void lbOnline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbOnline.SelectedItem != null)
            {
                selected = lbOnline.SelectedItem as ConnectionInfo;
                updateInfo();
                timerInfoUpdater.Enabled = true;
            }
        }
        private void updateInfo()
        {
            if (selected == null)
                return;
            lbUserInfoText.Text = string.Format("{0}IP: {1}\r\nНачало сессии: {2}\r\nПродолжительность сеанса: {3}", selected.User == null ? "" : string.Format("Логин: {0}\r\nПрофиль: {1}\r\nДоп. информация: {2}\r\n", selected.User.Login, selected.User.Profile._FullNameAndNumber, selected.User.Information == null ? "" : selected.User.Information.Info), selected.Socket.RemoteEndPoint, selected.Connected, DateTime.Now - selected.Connected);
        }

        private void timerInfoUpdater_Tick(object sender, EventArgs e)
        {
            updateInfo();
            tssUptime.Text = (DateTime.Now - Program.StartTime).ToString();
        }

        List<ConnectionInfo> Connections
        {
            get
            {
                return rbAll.Checked ? Server.server.Connections : lbOnline.SelectedItems.Cast<ConnectionInfo>().ToList();
            }
        }
                
        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (tbMessage.Text.Trim().Length > 0)
            {
                NetData data = new NetData(Command.Notification, tbMessage.Text.Trim());
                foreach (var item in Connections)
                    Server.server.SendToClient(item, data);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", "Подтверждение отключения пользователей", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                return;
            foreach (var item in Connections)
                Server.server.SendToClient(item, new NetData(Command.ServerIsShutingDown, "Test"));
        }
        bool shutingDown = false;
        IDisposable timer;
        IDisposable interval;
        DateTime ShutDownTime;
        private void btnShutDown_Click(object sender, EventArgs e)
        {
            shutingDown = !shutingDown;
            if (shutingDown)
            {
                btnShutDown.Text = "Отмена";
                ShutDownTime = DateTime.Now + TimeSpan.FromMinutes((int)timeout.Value);
                timer = EasyTimer.SetTimeout(() => {
                    Server.server.Connections.ForEach(c => Server.server.SendToClient(c,new NetData(Command.ServerIsShutingDown,"")));
                    EasyTimer.SetTimeout(() => { Environment.Exit(0);}, 20000); },//вырубать не сразу
                    (int)timeout.Value * 60000);
                interval = EasyTimer.SetInterval(() => { Invoke((Action)(() => { lbCountDown.Text = string.Format("До выключения сервера: {0}", ShutDownTime - DateTime.Now); })); }, 1000);
            }
            else
            {
                lbCountDown.Text = "";                
                btnShutDown.Text = "Ок";
                if (interval != null)
                    interval.Dispose();
                if (timer!=null)
                    timer.Dispose();
            }
        }
    }
}
