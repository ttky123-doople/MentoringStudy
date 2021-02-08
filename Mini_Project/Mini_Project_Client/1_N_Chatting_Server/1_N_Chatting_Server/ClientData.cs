using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace _1_N_Chatting_Server
{
    class ClientData
    {

        public TcpClient client { get; set; }
        public byte[] readByteData { get; set; }
        public int clientNumber;

        public ClientData(TcpClient client)
        {
            this.client = client;
            this.readByteData = new byte[1024];

            string clientEndPoint = client.Client.RemoteEndPoint.ToString();

            char[] point = { '.', ':' };
            string[] splittedData = clientEndPoint.Split(point);
            this.clientNumber = int.Parse(splittedData[3]);
            Console.WriteLine($"user{0} online", clientNumber);
        }
    }
}
