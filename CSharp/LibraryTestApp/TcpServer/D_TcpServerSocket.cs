using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Collections.Concurrent;
using D_Util.D_InterNet;

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
                    Socket ClientSoc = null;
                    try
                    {
                        ClientSoc = m_ServerSocket.Accept();
                    }
                    catch(SocketException e) when (e.ErrorCode == (int)SOCKET_ERROR_CODE.CLOSE_INTERRUPT)
                    {
                        Console.WriteLine("SOCKET INTERRUPT");
                        return;
                    }

                    ClientInfo con_CI = new ClientInfo();
                    con_CI.Soc = ClientSoc;
                    con_CI.IP = IPAddress.Parse(((IPEndPoint)ClientSoc.RemoteEndPoint).Address.ToString()).ToString();
                    con_CI.PORT = ((IPEndPoint)ClientSoc.RemoteEndPoint).Port;

                    Dict_ClientInfo.TryAdd(string.Format("{0}:{1}", con_CI.IP ,con_CI.PORT) , con_CI);

                    AcceptHandler?.Invoke(con_CI.IP, con_CI.PORT);

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
            catch(SocketException soc_e) when (soc_e.ErrorCode == (int)SOCKET_ERROR_CODE.DISCONNECTED)
            {
                Console.WriteLine(string.Format("{0}:{1} has disconnected" , _CI.IP , _CI.PORT));
                Disconnect(_CI.IP, _CI.PORT);
                return;
            }
            catch(ObjectDisposedException od_e)
            {
                Console.WriteLine(string.Format("{0}:{1} Already Close", _CI.IP, _CI.PORT));
                return;
            }

            if(data_len > 0)
            {
                ReceiveHandler?.Invoke(_CI.buffer , _CI.IP , _CI.PORT);
                Array.Clear(_CI.buffer, 0, data_len);
            }
            ReceiveStart(_CI);
        }

        public async void SendOne(byte[] data , string _IP , int _PORT)
        {
            ClientInfo _CI = null;
            Dict_ClientInfo.TryGetValue(string.Format("{0}:{1}", _IP, _PORT), out _CI);

            await Task.Run(
                    new Action(
                        () =>
                            {
                                _CI.Soc.Send(data, 0, data.Length, SocketFlags.None);
                            }
                    ));
        }

        public void Disconnect(string _IP , int _PORT)
        {
            ClientInfo temp = null;
            bool result = Dict_ClientInfo.TryRemove(string.Format("{0}:{1}",_IP ,_PORT) , out temp);
            temp?.Soc.Close();
            DisconnectHandler?.Invoke(result, temp.IP , temp.PORT);
        }

        public void Disconnect(string _IP)
        {
            var res = from ci in Dict_ClientInfo
                      where ci.Key.Contains(_IP)
                      select ci;

            string[] temp_CI = null;
            string _CI_IP = "";
            int _CI_PORT = 0;
            foreach (var _CI in res)
            {
                temp_CI = _CI.Key.Split(':');
                _CI_IP = temp_CI[0];
                _CI_PORT = Int32.Parse(temp_CI[1]);
                Disconnect(_CI_IP, _CI_PORT);
            }

        }
        
        public void Close()
        {
            m_ServerSocket.Close();


            string[] temp_CI = null;
            string _CI_IP = "";
            int _CI_PORT = 0;
            foreach(KeyValuePair<string , ClientInfo> _CI in Dict_ClientInfo)
            {
                temp_CI = _CI.Key.Split(':');
                _CI_IP = temp_CI[0];
                _CI_PORT = Int32.Parse(temp_CI[1]);
                Disconnect(_CI_IP, _CI_PORT);
            }
        }


        
    }
}
