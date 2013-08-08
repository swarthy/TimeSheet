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
            server.LogMethod += (msg) => { Console.WriteLine(msg); };
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
                        Console.WriteLine("Auth error: id = " + data.Text);
                        return;
                    }
                    ci.User = User.Get<User>(temp);
                    if (OnLogin != null)
                        OnLogin(data, ci);
                    break;
                case Command.Logout:
                    if (OnLogout != null)
                        OnLogout(data, ci);
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
    }
}
