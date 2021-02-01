using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Chatting_With_Async_Server
{
    class ClientData
    {
        public TcpClient client { get; set; }
        public byte[] readByteData { get; set; }

        public ClientData(TcpClient client)
        {
            this.client = client;
            this.readByteData = new byte[1024];
        }
    }
}
