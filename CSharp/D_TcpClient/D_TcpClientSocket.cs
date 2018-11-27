using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D_TcpClient
{
    public class D_TcpClientSocket
    {
        private Socket m_TcpClientSocket = null;
        private byte[] m_Readbuffer = null;

        public bool Connected { get; private set; }
        public bool ConnectRetry { get; set; }
        
        private readonly IPEndPoint SERVER_IPENDPOINT = null;
        private readonly string SERVER_IP = null;
        private readonly int SERVER_PORT = 0;

        public event ConnectCallback ConnectHandler;
        public event CloseCallback CloseHandler;
        public event ReadDataCallback ReadHandler;
        
        private EventWaitHandle ConnectWaitHandler = null;


        /*==============================================================================*/
        /*                                  생성자(IP , PORT)                           */
        /*==============================================================================*/
        public D_TcpClientSocket(string _IP = D_Configuration.SERVER_IP, int _PORT = D_Configuration.SERVER_PORT)
        {
            SERVER_IP = (D_Util.D_InterNet.IsIP(_IP)) ? _IP : throw new ArgumentException("IP is wrong", "_IP");
            SERVER_PORT = (D_Util.D_InterNet.IsPORT(_PORT)) ? _PORT : throw new ArgumentException("PORT is wrong", "_PORT");
            SERVER_IPENDPOINT = new IPEndPoint(IPAddress.Parse(SERVER_IP), SERVER_PORT);
            Connected = false;
            m_Readbuffer = new byte[1024];
        }
        /*==============================================================================*/



        /*==============================================================================*/
        /*                                     연결                                     */
        /*==============================================================================*/
        public async Task<bool> Connect(ConnectCallback callback = null, bool _retry = false , int timeout = 10000)
        {
            ConnectRetry = _retry;
            return await Task<bool>.Run(new Func<bool>( ()=> {

                bool result = false;

                do
                {
                    if (m_TcpClientSocket != null)
                    {
                        Close();
                    }

                    m_TcpClientSocket = new Socket(AddressFamily.InterNetwork,
                                                   SocketType.Stream,
                                                   ProtocolType.Tcp);
                    ConnectHandler += callback;
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
                    catch (Exception e)
                    {
                        Console.WriteLine("Connection Error");
                        Console.WriteLine(e.Message);

                        result = false;
                    }
                    finally
                    {
                        Connected = result;                             //연결 상태 ON
                        ConnectHandler?.Invoke(result);                 //콜백 함수 실행

                        if(!result) { Close(); }
                    }
                } while (ConnectRetry && IsConnected() == false);
                return result;

            }));
            
        }
        /*==============================================================================*/




        /*==============================================================================*/
        /*                                 연결 콜백                                    */
        /*==============================================================================*/
        //사용 안함
        [Obsolete("Connection is block method")]
        private void AsyncConnectCallback(IAsyncResult ar)
        {
            bool result = false;
            try
            {
                m_TcpClientSocket.EndConnect(ar);
                result = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("EndConnect Error");
                Console.WriteLine(e.Message);
                result = false;
            }
            finally
            {
                Connected = result;                 //연결 상태 ON
                ConnectWaitHandler.Set();           //Block 해제
            }
            ConnectHandler?.Invoke(result);                 //콜백 함수 실행
        }
        /*==============================================================================*/



        /*==============================================================================*/
        /*                                     읽기                                     */
        /*==============================================================================*/
        public void Read(ReadDataCallback callback)
        {
            if (IsConnected() == false)
            {
                Console.WriteLine("Socket is not opened. so, Can't read data.");
                return;
            }

            ReadHandler += callback;

            try
            {
                m_TcpClientSocket.BeginReceive(m_Readbuffer, 0, m_Readbuffer.Length,
                                               SocketFlags.None,
                                               AsyncReceiveCallBack, null);
            }
            catch(Exception e)
            {
                Console.WriteLine("Read Error");
                Console.WriteLine(e.Message);
            }
            
        }
        /*==============================================================================*/



        /*==============================================================================*/
        /*                                읽기 콜백                                     */
        /*==============================================================================*/
        private void AsyncReceiveCallBack(IAsyncResult ar)
        {
            int recvByte = 0;
            try
            {
                recvByte = m_TcpClientSocket.EndReceive(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine("EndReceive Error");
                Console.WriteLine(e.Message);
            }


            if (recvByte > 0)
            {
                ReadHandler?.Invoke(m_Readbuffer);

                Array.Clear(m_Readbuffer, 0, m_Readbuffer.Length);
            }

            if (IsConnected() == false) { return; }

            try
            {
                m_TcpClientSocket.BeginReceive(m_Readbuffer, 0, m_Readbuffer.Length,
                                               SocketFlags.None,
                                               AsyncReceiveCallBack, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("BeginReceive Error");
                Console.WriteLine(e.Message);
            }

        }
        /*==============================================================================*/



        /*==============================================================================*/
        /*                                     쓰기                                     */
        /*==============================================================================*/
        public void Write(byte[] buffer)
        {
            if(IsConnected() == false) { return; }

            m_TcpClientSocket.BeginSend(buffer, 0, buffer.Length,
                                        SocketFlags.None,
                                        AsyncSendCallback, null);
        }
        /*==============================================================================*/



        /*==============================================================================*/
        /*                                쓰기 콜백                                     */
        /*==============================================================================*/
        private void AsyncSendCallback(IAsyncResult ar)
        {
            m_TcpClientSocket.EndSend(ar);
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
        public void Close(CloseCallback callback = null)
        {
            CloseHandler += callback;

            m_TcpClientSocket.Close();
            m_TcpClientSocket = null;
            m_Readbuffer = null;
            Connected = false;
            

            CloseHandler?.Invoke(true);

            ConnectHandler = null;
            ReadHandler = null;
            CloseHandler = null;


        }
        /*==============================================================================*/
    }
}
