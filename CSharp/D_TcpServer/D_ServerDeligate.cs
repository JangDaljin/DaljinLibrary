using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_TcpServer
{
    public delegate void AcceptCallback(string IP);
    public delegate void ReceiveCallback(byte[] data, string IP);
    public delegate void DisconnectCallback(bool res, string IP);
}
