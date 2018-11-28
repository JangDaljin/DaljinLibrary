using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpServer
{
    public partial class ServerForm : Form
    {
        private D_TcpServer.D_TcpServerSocket m_ServerSocket = null;
        private readonly int MAX_LISTEN_COUNT = 10;
    
        public ServerForm()
        {
            InitializeComponent();
        }

        private void btn_OpenClose_Click(object sender, EventArgs e)
        {
            if (!D_Util.D_InterNet.IsPORT(tb_Port.Text))
            {
                MessageBox.Show("포트번호가 잘못되었습니다.");
                return;
            }

            if (m_ServerSocket == null)
            {
                m_ServerSocket = new D_TcpServer.D_TcpServerSocket(
                                                                    int.Parse(tb_Port.Text), 
                                                                    MAX_LISTEN_COUNT, 
                                                                    AcceptCallback, 
                                                                    ReceiveCallback, 
                                                                    DisconnectionCallback);
                m_ServerSocket.Accept();

                tb_ReceiveMsg.Text += "***서버가 열렸습니다.***\r\n";
                    
            }
            else
            {
                m_ServerSocket.Close();
                m_ServerSocket = null;

                tb_ReceiveMsg.Text += "***서버가 닫혔습니다.***\r\n";
            }
            
        }

        private void AcceptCallback(string _IP , int _PORT)
        {
            this.Invoke(
                new MethodInvoker(() =>
                   {
                       chlb_ClientList.Items.Add(String.Format("{0}:{1}", _IP, _PORT));
                   }));
            
        }

        private void ReceiveCallback(byte[] data, string _IP , int _PORT)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(tb_ReceiveMsg.Text);
            sb.Append(_IP+":"+_PORT);
            sb.Append(":");
            sb.Append(Encoding.UTF8.GetString(data));

            this.Invoke(new MethodInvoker(
                () =>
                {
                    tb_ReceiveMsg.Text = sb.ToString();
                }));
            
        }

        private void DisconnectionCallback(bool res , string _IP , int _PORT)
        {
            this.Invoke(
                new MethodInvoker(
                    () =>
                    {
                        if (res)
                        {
                            chlb_ClientList.Items.Remove(string.Format("{0}:{1}",_IP , _PORT));
                        }
                    }));

        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            string[] temp_info = null;
            string _IP = "";
            int _PORT = 0;
            while(chlb_ClientList.CheckedItems.Count >0)
            {
                temp_info = chlb_ClientList.CheckedItems[0].ToString().Split(':');
                _IP = temp_info[0];
                _PORT = Int32.Parse(temp_info[1]);
                m_ServerSocket.Disconnect(_IP , _PORT);
            }
        }

        private void tb_SendMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                string[] temp_info = null;
                string _IP = "";
                int _PORT = 0;
                byte[] data = Encoding.UTF8.GetBytes(tb_SendMsg.Text);
                while (chlb_ClientList.CheckedItems.Count > 0)
                {
                    temp_info = chlb_ClientList.CheckedItems[0].ToString().Split(':');
                    _IP = temp_info[0];
                    _PORT = Int32.Parse(temp_info[1]);
                    m_ServerSocket.SendOne(data , _IP, _PORT);
                }
                tb_SendMsg.Text = "";
            }
        }
    }
}
