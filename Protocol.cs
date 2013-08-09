using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NetWork
{    
    public enum Command
    {
        Null,
        Ping,
        //клиентские
        Login,
        Logout,
        Message,
        ClientVersion,

        //серверные
        Notification,
        LetsUpdate,
        ServerIsShutingDown        
    }
    
    public class NetData
    {        
        public string Text;
        public Command CMD;
        public override string ToString()
        {
            return string.Format("{0}: {2}", CMD, Text);
        }
        public NetData()
        {
            CMD = Command.Null;
            Text = null;            
        }
        public NetData(Command cmd, string message)
        {
            CMD = cmd;            
            Text = message;
        }       
        public static Command GetCommand(byte[] data)
        {
            if (data.Length >= 4)
                return (Command)BitConverter.ToInt32(data, 0);
            else
                return Command.Null;
        }
        public NetData(byte[] data)
        {
            //команда
            CMD = (Command)BitConverter.ToInt32(data, 0);
                        
            //длина текста
            int msgLen = BitConverter.ToInt32(data, 4);
                        
            //текст
            if (msgLen > 0)
                Text = Encoding.UTF8.GetString(data, 8, msgLen);
            else
                Text = null;
        }
                
        public byte[] ToBytes()
        {
            List<byte> result = new List<byte>();

            //команда
            result.AddRange(BitConverter.GetBytes((int)CMD));
                        
            //текст
            if (Text != null)
            {   
                var msg = Encoding.UTF8.GetBytes(Text);
                result.AddRange(BitConverter.GetBytes(msg.Length));
                result.AddRange(msg);
            }
            else
                result.AddRange(BitConverter.GetBytes(0));
            return result.ToArray();
        }        
    }
}
