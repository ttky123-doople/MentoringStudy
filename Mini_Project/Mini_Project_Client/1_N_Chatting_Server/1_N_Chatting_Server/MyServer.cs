using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.IO;

namespace _1_N_Chatting_Server
{
    class MyServer
    {
        public MyServer()
        {
            AsyncServerStart();
        }

        private void AsyncServerStart()
        {
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Any, 19013));

            listener.Start();
            Console.WriteLine("Server Started");

            while (true)
            {
                TcpClient accpetedClient = listener.AcceptTcpClient();
                ClientData clientData = new ClientData(accpetedClient);

                clientData.client.GetStream().BeginRead(clientData.readByteData, 0, clientData.readByteData.Length, new AsyncCallback(DataReceived), clientData);
            }
        }
        private void DataReceived(IAsyncResult ar)
        {
            ClientData callbackClient = ar.AsyncState as ClientData;
            Student s = new Student();

    /*
            var stream = callbackClient.client.GetStream();

            var sr = new StreamReader(stream);

            Console.WriteLine(sr.ReadToEnd()); ;
    */
            //XmlSerializer xs = new XmlSerializer(typeof(Student));
            //s = (Student)xs.Deserialize(callbackClient.client.GetStream());Console.WriteLine("무야호~");
            //callbackClient.client.GetStream().Flush();
            //Console.WriteLine(s.Command + " " + s.Name + " " + s.Phone + " " + s.Number);
            int bytesRead = callbackClient.client.GetStream().EndRead(ar);
            string readString = Encoding.Default.GetString(callbackClient.readByteData, 0, bytesRead);
            byte[] writeByte = new byte[1024];

            //var sr = new StringReader(readString);
            //XmlSerializer xs = new XmlSerializer(typeof(Student));
            //s = (Student)xs.Deserialize(sr);


            //Console.WriteLine("s.Command = " + s.Command);
            




            writeByte = Encoding.Default.GetBytes(readString);
            callbackClient.client.GetStream().Write(writeByte, 0, writeByte.Length);
            Console.WriteLine("user{0}:\n{1}", callbackClient.clientNumber, readString);
            callbackClient.client.GetStream().BeginRead(callbackClient.readByteData, 0, callbackClient.readByteData.Length, new AsyncCallback(DataReceived), callbackClient);

        }
    }
}
