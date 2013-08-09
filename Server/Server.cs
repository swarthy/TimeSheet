using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetWork;
using System.Net.Sockets;
using TimeSheetManager;
using System.IO;

namespace Server
{
    public static class Server
    {
        public static DataEvent OnLogin, OnLogout;
        internal static AsynchronousIoServer server;
        public static void Initialize()
        {
            int port = 23069;            
            server = new AsynchronousIoServer(port);
            server.LogMethod += Log;
            server.OnReceiveData += new DataEvent(OnReceive);            
        }
        public static void Start()
        {
            server.Start();
        }
        public static void Stop()
        {
            server.Stop();
        }
        static void OnReceive(NetData data, ConnectionInfo ci)
        {
            switch (data.CMD)
            {
                case Command.Login:
                    int temp;
                    if (!int.TryParse(data.Text, out temp))
                    {
                        Log("Auth error: id = {0}", data.Text);
                        return;
                    }
                    ci.User = User.Get<User>(temp);
                    Log("Auth user {0} from {1}", ci.User.Login, ci.Socket.RemoteEndPoint);
                    if (OnLogin != null)
                        OnLogin(data, ci);
                    break;
                case Command.Logout:
                    if (OnLogout != null)
                        OnLogout(data, ci);
                    Log("Logout user {0}", ci.User.Login);
                    ci.User = null;
                    break;
                case Command.Message:
                    Console.WriteLine("[{0}] {1}", ci.User.Login, data.Text);
                    break;
                case Command.ClientVersion:
                    if (Program.Version != "" && Program.Version != data.Text)
                        server.SendToClient(ci, new NetData(Command.LetsUpdate, ""));
                    break;
            }            
        }        
        static void Log(string msg, params object[] args)
        {
            Console.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToShortTimeString(), string.Format(msg, args))); 
        }
        public static void PingAll()
        {
            foreach (var ci in server.Connections)
                server.SendToClient(ci, new NetData(Command.Ping, ""));
        }
    }
}
