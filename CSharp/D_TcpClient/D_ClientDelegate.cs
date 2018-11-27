using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_TcpClient
{
    public delegate void ConnectCallback(bool result);
    public delegate void CloseCallback(bool result);
    public delegate void ReadDataCallback(byte[] data);
}
