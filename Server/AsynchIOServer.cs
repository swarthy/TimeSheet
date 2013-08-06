using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using NetWork;
using TimeSheetManger;

namespace Server
{
    public delegate void Logging(string msg);    
    public class ConnectionInfo
    {
        public Socket Socket { get; set; }
        public byte[] Buffer { get; set; }
        public User User { get; set; }
        public DateTime Connected { get; set; }
        public string ShortInfo
        {
            get
            {
                return string.Format("{0}{1}", Socket.RemoteEndPoint, User == null ? "" : " - " + User._LoginAndProfile);
            }
        }
    }
    public delegate void ConnectionInfoEvent(ConnectionInfo info);
    public delegate void DataEvent(NetData data, ConnectionInfo ci);
    class AsynchronousIoServer
    {
        public ConnectionInfoEvent OnClientConnect, OnClientDisconnect;
        public DataEvent OnReceiveData;        
        public Logging LogMethod;

        public void Log(string message, params object[] args)
        {
            if (LogMethod != null)
            {
                try
                {
                    LogMethod(string.Format("[{0}] {1}", DateTime.Now.ToShortTimeString(), string.Format(message, args)));
                }
                catch (Exception ex)
                {
                    LogMethod(string.Format("[{0}] {1}", DateTime.Now.ToShortTimeString(), ex.Message));
                }
            }
        }
        private Socket serverSocket;
        private int _port = 23069;
        private string _IP = "127.0.0.1";        

        public AsynchronousIoServer(string ip, int port)
        {
            _IP = ip;
            _port = port;
        }

        public List<ConnectionInfo> Connections { get; set; }

        //private List<ConnectionInfo> Connections =
            //new List<ConnectionInfo>();

        internal void Start()
        {
            SetupServerSocket();
            for (int i = 0; i < 2; i++)
                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), serverSocket);
        }

        internal void Stop()
        {
            if (serverSocket.Connected)
            {
                while (Connections.Count > 0)
                    CloseConnection(Connections.First());
                serverSocket.Close();
            }
        }

        internal void SendToClient(ConnectionInfo info, NetData data)
        {
            var bytes = data.ToBytes();
            info.Socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(SendCallback), info);
        }

        private void SetupServerSocket()
        {
            try
            {                
                serverSocket = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);
                Connections = new List<ConnectionInfo>();
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(_IP), _port);
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(10);
                Log("Server started at {0}", DateTime.Now);
            }
            catch (Exception ex)
            {
                Log("On start exception: {0}", ex.Message);
            }
        }        

        private void AcceptCallback(IAsyncResult result)
        {
            ConnectionInfo connection = new ConnectionInfo();
            try
            {
                Socket s = (Socket)result.AsyncState;
                connection.Socket = s.EndAccept(result);
                connection.Connected = DateTime.Now;
                connection.Buffer = new byte[512];
                lock (Connections) Connections.Add(connection);
                Log("Connected: {0}", connection.Socket.RemoteEndPoint);

                //продолжаем принимать подключения
                serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), result.AsyncState);

                if (OnClientConnect != null)
                    OnClientConnect(connection);

                //переходим для текущего подключения в режим чтения
                connection.Socket.BeginReceive(connection.Buffer,
                    0, connection.Buffer.Length, SocketFlags.None,
                    new AsyncCallback(ReceiveCallback),
                    connection);
            }
            catch (ObjectDisposedException)
            { }
            catch (SocketException exc)
            {
                CloseConnection(connection);
                Log("Socket exception: " +
                    exc.SocketErrorCode);
            }
            catch (Exception exc)
            {
                CloseConnection(connection);
                Log("Exception: " + exc);
            }
        }
        private void SendCallback(IAsyncResult result)
        {
            ConnectionInfo ci = (ConnectionInfo)result.AsyncState;
            ci.Socket.EndSend(result);
        }
        private void ReceiveCallback(IAsyncResult result)
        {
            ConnectionInfo connection = (ConnectionInfo)result.AsyncState;
            try
            {
                int bytesRead = connection.Socket.EndReceive(result);
                if (bytesRead != 0)
                {
                    NetData data = new NetData(connection.Buffer);
                    if (OnReceiveData != null)
                        OnReceiveData(data, connection);
                    //продолжаем читать
                    connection.Socket.BeginReceive(
                        connection.Buffer, 0,
                        connection.Buffer.Length, SocketFlags.None,
                        new AsyncCallback(ReceiveCallback), connection);
                }
                else
                    CloseConnection(connection);
            }
            catch (ObjectDisposedException)
            { }
            catch (SocketException exc)
            {
                CloseConnection(connection);
                Log("Socket exception: " +
                    exc.SocketErrorCode);
            }
            catch (Exception exc)
            {
                CloseConnection(connection);
                Log("Exception: " + exc);
            }
        }

        internal void CloseConnection(ConnectionInfo ci, bool fireDiscEvent = true)
        {
            Log("Disconnected: {0}", ci.Socket.RemoteEndPoint);
            ci.Socket.Shutdown(SocketShutdown.Both);
            ci.Socket.Close();
            lock (Connections) Connections.Remove(ci);
            if (fireDiscEvent && OnClientDisconnect != null)
                OnClientDisconnect(ci);
        }
    }
}