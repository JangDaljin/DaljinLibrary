using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryTestApp
{
    public partial class AppStartForm : Form
    {
        public AppStartForm()
        {
            InitializeComponent();
        }

        private void btn_TcpClientStart_Click(object sender, EventArgs e)
        {
            TcpClient.TcpClientForm form_TcpClient = new TcpClient.TcpClientForm();
            form_TcpClient.Show();
        }

        private void btn_TcpServerStart_Click(object sender, EventArgs e)
        {
            TcpServer.ServerForm form_TcpServer = new TcpServer.ServerForm();
            form_TcpServer.Show();
        }
    }
}
