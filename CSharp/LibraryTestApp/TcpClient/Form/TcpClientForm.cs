using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpClient
{
    public partial class TcpClientForm : Form
    {

        D_TcpClient.D_TcpClientSocket m_Socket = null;
        string SERVER_IP = null;
        int SERVER_PORT = -1;

        public TcpClientForm()
        {
            InitializeComponent();
        }

        private async void btn_Connection_Click(object sender, EventArgs e)
        {
            btn_Connection.Enabled = false;
            btn_Stop.Enabled = true;
            chb_Retry.Enabled = false;

            SERVER_IP = D_Util.D_InterNet.IsIP(tb_ServerIP.Text.ToString()) ? tb_ServerIP.Text.ToString() : D_TcpClient.D_Configuration.SERVER_IP;
            SERVER_PORT = D_Util.D_InterNet.IsPORT(tb_ServerPort.Text.ToString()) ? int.Parse(tb_ServerPort.Text.ToString()) : D_TcpClient.D_Configuration.SERVER_PORT;

            if(m_Socket == null)
            {
                m_Socket = new D_TcpClient.D_TcpClientSocket(SERVER_IP, SERVER_PORT);


                bool res = await m_Socket.Connect(Connect_Callback, chb_Retry.Checked, 2000);

                Console.WriteLine("Control check");
            }
            else
            {
                ClientClose();
            }
         

            btn_Connection.Enabled = true;
            btn_Stop.Enabled = false;
            chb_Retry.Enabled = true;
        }

        private void Connect_Callback(bool result)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (result)
                {
                    tb_ReadMsg.Text += "***서버와 연결되었습니다.***\r\n";
                    btn_Connection.Text = "해제";
                    tb_SendMsg.Enabled = true;

                    m_Socket.ReceiveStart(Read_Callback);
                }
                else
                {
                    tb_ReadMsg.Text += "***서버와 연결이 되지않습니다.***\r\n";
                    btn_Connection.Text = "연결";
                    tb_SendMsg.Enabled = false;
                    m_Socket = null;
                }
            }
            ));
        }

        private void Read_Callback(byte[] data)
        {
            
            StringBuilder sb = new StringBuilder();
            this.Invoke(new MethodInvoker(() =>
            {
                tb_ReadMsg.Text = "YES";
                sb.Append(tb_ReadMsg.Text);
                sb.Append(Encoding.UTF8.GetString(data));
                sb.Append("\r\n");
                tb_ReadMsg.Text = sb.ToString();
            }));
        }

        private void ClientClose()
        {
            m_Socket.Close();
            tb_ReadMsg.Text += "***서버와 연결이 끊겼습니다.***\r\n"; 
            btn_Connection.Text = "연결";
            tb_SendMsg.Enabled = false;
            m_Socket = null;
        }

        private void tb_SendMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                m_Socket?.Write(Encoding.UTF8.GetBytes(tb_SendMsg.Text.ToString()));
                tb_SendMsg.Text = "";
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            m_Socket.ConnectRetry = false;
        }

        private void chb_Retry_CheckedChanged(object sender, EventArgs e)
        {
            if(((CheckBox)sender).Checked == true)
            {
                btn_Stop.Visible = true;
            }
            else
            {
                btn_Stop.Visible = false;
            }
        }
    }
}
