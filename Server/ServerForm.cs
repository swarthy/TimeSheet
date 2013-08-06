using System;
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
        }

        List<ConnectionInfo> Connections
        {
            get
            {
                return rbAll.Checked ? Server.server.Connections : lbOnline.SelectedItems.Cast<ConnectionInfo>().ToList();
            }
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            selected = null;
            foreach (var item in Connections)
                Server.server.CloseConnection(item, false);
            refreshOnlineList();
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
            foreach (var item in Connections)
                Server.server.SendToClient(item, new NetData(Command.ServerIsShutingDown, "Test"));
        }
        bool shutingDown = false;
        IDisposable timer;
        private void btnShutDown_Click(object sender, EventArgs e)
        {
            shutingDown = !shutingDown;
            if (shutingDown)
            {
                btnShutDown.Text = "Отмена";
                timer = EasyTimer.SetTimeout(() => {
                    Server.server.Connections.ForEach(c => Server.server.SendToClient(c,new NetData(Command.ServerIsShutingDown,"")));
                    EasyTimer.SetTimeout(() => { Environment.Exit(0);}, 20000); },//вырубать не сразу
                    (int)timeout.Value * 60000);
            }
            else
            {
                btnShutDown.Text = "Ок";
                if (timer!=null)
                    timer.Dispose();
            }
        }
    }
}
