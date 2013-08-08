using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using NetWork;
using System.Net;

namespace Client
{
    public delegate void DataEvent(NetData data);
    static class SocketExtensions
    {
        public static bool IsConnected(this Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }
    }
    public class ClientIOAsync
    {
        public DataEvent OnReceiveData;
        public EventHandler OnConnecting, OnConnected, OnServerNotAvailable;
        private Socket clientSocket;
        private IPEndPoint serverPoint;
        public ClientIOAsync()
        {            
        }
        public bool Connected
        {
            get
            {
                //return clientSocket.IsConnected();
                return clientSocket.Connected;
            }
        }
        public void Connect(string ip, int port)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse(ip);
            serverPoint = new IPEndPoint(ipAddress, port);
            clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);            
            try
            {
                if (OnConnecting != null)
                    OnConnecting(this, EventArgs.Empty);
                clientSocket.BeginConnect(serverPoint, new AsyncCallback(ConnectCallBack), null);                
            }
            catch (SocketException ex)
            {
            }
        }
        public void Reconnect()
        {
            if (clientSocket.Connected)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Disconnect(true);
            }
            clientSocket.Close();
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);            
            clientSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            try
            {
                if (OnConnecting != null)
                    OnConnecting(this, EventArgs.Empty);
                clientSocket.BeginConnect(serverPoint, new AsyncCallback(ConnectCallBack), null);
            }
            catch (SocketException ex)
            {
            }
        }
        public void Disconnect()
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Disconnect(false);
            clientSocket.Close();
        }
        public bool Send(NetData data)
        {
            if (!clientSocket.Connected)
                return false;
            var bytes = data.ToBytes();
            try
            {                
                clientSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);                
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Socket exception: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
            return true;
        }
        private void SendCallback(IAsyncResult result)
        {
            try
            {
                clientSocket.EndSend(result);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Socket exception: {0}", ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
        }
        private void ConnectCallBack(IAsyncResult result)
        {
            if (!clientSocket.Connected)
            {
                if (OnServerNotAvailable != null)
                    OnServerNotAvailable(this, EventArgs.Empty);
                return;
            }
            try
            {
                if (OnConnected != null)
                    OnConnected(clientSocket, EventArgs.Empty);
                var byteData = new byte[512];
                clientSocket.BeginReceive(byteData,
                                           0,
                                           byteData.Length,
                                           SocketFlags.None,
                                           new AsyncCallback(ReceiveCallBack),
                                           byteData);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void ReceiveCallBack(IAsyncResult result)
        {
            if (!clientSocket.Connected)
            {
                if (OnServerNotAvailable != null)
                    OnServerNotAvailable(this, EventArgs.Empty);
                return;
            }
            var byteData = result.AsyncState as byte[];            
            try
            {
                int count = clientSocket.EndReceive(result);
                if (count == 0)
                {
                    clientSocket.Close();
                    return;
                }
                NetData msgReceived = new NetData(byteData);

                if (OnReceiveData != null)
                    OnReceiveData(msgReceived);
                
                byteData = new byte[512];
                clientSocket.BeginReceive(byteData,
                                          0,
                                          byteData.Length,
                                          SocketFlags.None,
                                          new AsyncCallback(ReceiveCallBack),
                                          byteData);

            }
            catch (ObjectDisposedException)
            { }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
