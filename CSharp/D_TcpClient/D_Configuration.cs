using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D_TcpClient
{
    public enum D_MSG{
        DISCONNECT = 0,
        CONNECT = 1
    }

    class D_Configuration
    {
        public const string SERVER_IP ="127.0.0.1";
        public const int SERVER_PORT = 0; 
        
    }
}
