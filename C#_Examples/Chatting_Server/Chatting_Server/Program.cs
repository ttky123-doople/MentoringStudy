using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chatting_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Chatting_Server \n\n\n\n");
            Console.Write("Port: ");
            int port = Convert.ToInt32(Console.ReadLine());
            TcpListener server = new TcpListener(IPAddress.Any, port);

            server.Start();

            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("Client Online\n\n\n");


            while (true)
            {
                byte[] byteData = new byte[1024]; 
                client.GetStream().Read(byteData, 0, byteData.Length); 

                string strData = Encoding.Default.GetString(byteData); 

                int endPoint = strData.IndexOf('\0'); string parsedMessage = strData.Substring(0, endPoint + 1); 

                Console.WriteLine("Client Message " + parsedMessage + "\n\n\n");

            }
        }
    }
}
