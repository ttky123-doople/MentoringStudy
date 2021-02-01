using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace ChattingClient
{
    class Program
    {


        static void Main(string[] args)
        {

            Console.WriteLine("Chatting_Client\n\n\n\n");
            Console.Write("Server IP: ");
            string ip = Console.ReadLine();
            Console.Write("\n\n");
            Console.Write("Server Port: ");
            int port = Convert.ToInt32(Console.ReadLine());
            Console.Write("\n\n");

            myClient mclient = new myClient();
            mclient.Run(ip, port);


        }
    }
    class myClient
    {
        TcpClient client = null; 
        public void Run(string ip, int port)
        {

            Console.WriteLine("Chatting Client");
            Connect(ip, port);
            Console.WriteLine("Connection to Server Successful");
            while(true)
            {
                SendMessage();
            }
        }
        private void SendMessage()
        {
            Console.Write("Message: ");
            string message = Console.ReadLine();
            byte[] byteData = new byte[message.Length];
            byteData = Encoding.Default.GetBytes(message);
            client.GetStream().Write(byteData, 0, byteData.Length);
            Console.WriteLine("Message sent Successfuly\n\n\n");

        }
        private void Connect(string ip, int port)
        {
            client = new TcpClient();
            client.Connect(ip, port);
        }

    }
}



