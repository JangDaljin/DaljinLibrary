using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using D_Util.D_InterNet;

namespace D_TcpClient
{
    public class D_TcpClientSocket
    {
        private Socket m_TcpClientSocket = null;
        private byte[] m_Readbuffer = null;

        public bool Connected { get; private set; }
        
        private readonly IPEndPoint SERVER_IPENDPOINT = null;
        private readonly string SERVER_IP = null;
        private readonly int SERVER_PORT = 0;

        public event ConnectCallback ConnectHandler;
        public event ReceiveCallback ReceiveHandler;
        public event CloseCallback CloseHandler;
        


        /*==============================================================================*/
        /*                                  생성자(IP , PORT)                           */
        /*==============================================================================*/
        public D_TcpClientSocket(string _IP = D_Configuration.SERVER_IP, int _PORT = D_Configuration.SERVER_PORT,
                                 ConnectCallback connectCallback = null,
                                 ReceiveCallback receiveDataCallback = null,
                                 CloseCallback closeCallback = null)
        {
            SERVER_IP = (D_Util.ValidData.IsIP(_IP)) ? _IP : throw new ArgumentException("IP is wrong", "_IP");
            SERVER_PORT = (D_Util.ValidData.IsPORT(_PORT)) ? _PORT : throw new ArgumentException("PORT is wrong", "_PORT");
            SERVER_IPENDPOINT = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT);
            Connected = false;
            m_Readbuffer = new byte[1024];

            ConnectHandler  += connectCallback;
            ReceiveHandler  += receiveDataCallback;
            CloseHandler += closeCallback;
        }
        /*==============================================================================*/



        /*==============================================================================*/
        /*                                     연결                                     */
        /*==============================================================================*/
        public async void Connect(int timeout = 10000)
        {
            await Task.Run(new Action( ()=> {

                bool result = false;
                
                if (m_TcpClientSocket != null)
                {
                    Close();
                }

                m_TcpClientSocket = new Socket(AddressFamily.InterNetwork,
                                                SocketType.Stream,
                                                ProtocolType.Tcp);
                    
                try
                {
                    IAsyncResult ar = m_TcpClientSocket.BeginConnect((EndPoint)SERVER_IPENDPOINT,
                                                    null,
                                                    null);
                    result = ar.AsyncWaitHandle.WaitOne(timeout, true);
                    if (result)
                    {
                        m_TcpClientSocket.EndConnect(ar);
                    }
                    else
                    {
                        m_TcpClientSocket.Close();
                    }
                }
                catch (SocketException e) when (e.ErrorCode == (int)SOCKET_ERROR_CODE.NOT_FOUND_SERVER)
                {
                    Console.WriteLine("Not found server");
                    result = false;
                }
                catch(Exception)
                {
                    result = false;
                }
                finally
                {
                    Connected = result;                             //연결 상태 ON
                    ConnectHandler?.Invoke(result);                 //콜백 함수 실행

                    if(!result) { m_TcpClientSocket = null; }
                }


            }));
            
        }
        /*==============================================================================*/

        /*==============================================================================*/
        /*                                     읽기                                     */
        /*==============================================================================*/
        public void Receive()
        {
            
            if (IsConnected() == false)
            {
                Console.WriteLine("Socket is not opened. so, Can't read data.");
                return;
            }

            ReceiveStart();
        }
        /*==============================================================================*/

        /*==============================================================================*/
        /*                                 읽기 콜백                                    */
        /*==============================================================================*/
        private void AsyncReceiveCallback(IAsyncResult _ar)
        {
            int recvByte = 0;

            try
            {
                recvByte = m_TcpClientSocket.EndReceive(_ar);
            }
            catch(SocketException e) when (e.ErrorCode == (int)SOCKET_ERROR_CODE.DISCONNECTED)
            {
                Console.WriteLine("Server has disconnected");
                Close();
                return;
            }
            catch(NullReferenceException)
            {
                Close();
                return;
            }
            

            if (recvByte > 0)
            {
                ReceiveHandler?.Invoke(m_Readbuffer);

                Array.Clear(m_Readbuffer, 0, m_Readbuffer.Length);
            }

            ReceiveStart();
        }
        /*==============================================================================*/

        /*==============================================================================*/
        /*                                 읽기 시작                                    */
        /*==============================================================================*/
        private void ReceiveStart()
        {
            try
            {
                m_TcpClientSocket.BeginReceive(m_Readbuffer, 0, m_Readbuffer.Length, SocketFlags.None,
                    AsyncReceiveCallback, null);
            }
            catch (SocketException e) when (e.ErrorCode == (int)SOCKET_ERROR_CODE.DISCONNECTED)
            {
                Console.WriteLine("Server has disconnected you");
                Close();
                return;
            }
        }
        /*==============================================================================*/



        /*==============================================================================*/
        /*                                     쓰기                                     */
        /*==============================================================================*/
        public void Write(byte[] buffer)
        {
            if(IsConnected() == false) { return; }

            try
            {
                m_TcpClientSocket.BeginSend(buffer, 0, buffer.Length,
                                            SocketFlags.None,
                                            AsyncSendCallback, null);
            }
            catch(Exception)
            {
                Console.WriteLine("[CLIENT]Write() Catch");
                Close();
                return;
            }
        }
        /*==============================================================================*/



        /*==============================================================================*/
        /*                                쓰기 콜백                                     */
        /*==============================================================================*/
        private void AsyncSendCallback(IAsyncResult ar)
        {
            try
            {
                m_TcpClientSocket.EndSend(ar);
            }
            catch(Exception)
            {
                Console.WriteLine("[CLIENT]SendCallback() catch");
                Close();
                return;
            }
        }
        /*==============================================================================*/



        /*==============================================================================*/
        /*                                연결 확인                                     */
        /*==============================================================================*/
        private bool IsConnected()
        {
            bool res = false;
            if(Connected == true)
            {
                res = true;
            }
            return res;
        }
        /*==============================================================================*/



        /*==============================================================================*/
        /*                                     종료                                     */
        /*==============================================================================*/
        public void Close()
        {

            m_TcpClientSocket?.Close();
            m_TcpClientSocket = null;
            m_Readbuffer = null;
            Connected = false;

            CloseHandler?.Invoke();

            ConnectHandler = null;
            ReceiveHandler = null;
            CloseHandler = null;
        }
        /*==============================================================================*/
    }
}
