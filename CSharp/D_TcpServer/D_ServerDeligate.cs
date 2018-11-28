using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_TcpServer
{
    public delegate void AcceptCallback(string _IP, int _PORT);
    public delegate void ReceiveCallback(byte[] data, string _IP, int _PORT);
    public delegate void DisconnectCallback(bool res, string _IP, int _PORT);
}
