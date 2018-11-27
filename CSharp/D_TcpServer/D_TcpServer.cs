using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Collections.Concurrent;

namespace D_TcpServer
{
    public class D_TcpServerSocket
    {
        private class ClientInfo
        { 
            public Socket Soc;
            public byte[] buffer;
            public string IP;
            public int PORT;

            public ClientInfo()
            {
                buffer = new byte[1024];
            }

            public ClientInfo(int buffersize = 1024)
            {
                buffer = new byte[buffersize];
            }
        }

        private Socket m_ServerSocket = null;
        private ConcurrentDictionary<string , ClientInfo> Dict_ClientInfo = null;
        public bool Closed { get; private set; } = true;

        public event AcceptCallback AcceptHandler;
        public event DisconnectCallback DisconnectHandler;
        public event ReceiveCallback ReceiveHandler;

        ///<param name="acceptCallback">(string IP, int PORT)=>{...}</Param>
        ///<param name="receiveCallback">(byte[] data)=>{...}</Param>
        ///<param name="closeCallback">(bool res)=>{...}</Param>
        public D_TcpServerSocket(int _BINDPORT = 0, int _ListenCount = 10 , 
                                    AcceptCallback acceptCallback = null ,
                                    ReceiveCallback receiveCallback = null,
                                    DisconnectCallback closeCallback = null)
        {
            m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_ServerSocket.Bind(new IPEndPoint(IPAddress.Any, _BINDPORT));
            m_ServerSocket.Listen(_ListenCount);

            AcceptHandler   +=  acceptCallback;
            ReceiveHandler  +=  receiveCallback;
            DisconnectHandler    +=  closeCallback;

            Dict_ClientInfo = new ConcurrentDictionary<string , ClientInfo>();
        }

        public async void Accept()
        {
            Closed = false;

            await Task.Run(new Action(() =>
            {
                while (!Closed)
                {
                    Socket ClientSoc = m_ServerSocket.Accept();

                    ClientInfo con_CI = new ClientInfo();
                    con_CI.Soc = ClientSoc;
                    con_CI.IP = IPAddress.Parse(((IPEndPoint)ClientSoc.RemoteEndPoint).Address.ToString()).ToString();
                    con_CI.PORT = ((IPEndPoint)ClientSoc.RemoteEndPoint).Port;

                    Dict_ClientInfo.TryAdd(con_CI.IP , con_CI);

                    AcceptHandler?.Invoke(con_CI.IP);

                    ReceiveStart(con_CI);
                }
            }));
        }

        private void ReceiveStart(ClientInfo _CI)
        {
            _CI.Soc.BeginReceive(_CI.buffer, 0, _CI.buffer.Length, SocketFlags.None, ReceiveCallback, _CI);
        }

        private void ReceiveCallback(IAsyncResult _ar)
        {
            ClientInfo _CI = (ClientInfo)(_ar.AsyncState);
            int data_len = 0;
            try
            {
                data_len = _CI.Soc.EndReceive(_ar);
            }
            catch(SocketException soc_e)
            {
                Console.WriteLine(soc_e.Message);
                Disconnect(_CI.IP);
                return;
            }
            catch(ObjectDisposedException od_e)
            {
                Console.WriteLine(od_e);
                return;
            }

            if(data_len > 0)
            {
                ReceiveHandler?.Invoke(_CI.buffer , _CI.IP);
                Array.Clear(_CI.buffer, 0, data_len);
            }
            ReceiveStart(_CI);
        }

        public void SendOne(string _IP , int _PORT)
        {
            
        }

        public void SendAll()
        {

        }

        public void Disconnect(string _IP)
        {
            ClientInfo temp = null;
            bool result = Dict_ClientInfo.TryRemove(_IP , out temp);
            temp?.Soc.Close();
            DisconnectHandler?.Invoke(result, temp?.IP);
        }
        
        public void Close()
        {
            m_ServerSocket.Shutdown(SocketShutdown.Both);
            m_ServerSocket.Close();
            
            foreach(KeyValuePair<string , ClientInfo> ci in Dict_ClientInfo)
            {
                Disconnect(ci.Key);
            }
        }


        
    }
}
