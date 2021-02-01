using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chatting_With_Async_Server
{
    class MyServer
    {
        public MyServer()
        {
            AsyncServerStart();
        }

        private void AsyncServerStart()
        {
            TcpListener listner = new TcpListener(new IPEndPoint(IPAddress.Any, 19013));

            listner.Start();
            Console.WriteLine("Server Started\nWaiting for client Connection");


            TcpClient acceptedClient = listner.AcceptTcpClient();
            ClientData clientData = new ClientData(acceptedClient);

            clientData.client.GetStream().BeginRead(clientData.readByteData, 0, clientData.readByteData.Length, new AsyncCallback(DataReceived), clientData);


            while (true)
            {
                Console.WriteLine("Server Running...");
                Thread.Sleep(1000);
            }
        }

        private void DataReceived(IAsyncResult ar)
        {
            // 콜백메서드입니다.(피호출자가 호출자의 해당 메서드를 실행시켜줍니다) 
            // 즉 데이터를 읽었을때 실행됩니다. 
            // 콜백으로 받아온 Data를 ClientData로 형변환 해줍니다. 
            ClientData callbackClient = ar.AsyncState as ClientData;
            //실제로 넘어온 크기를 받아옵니다 
            int bytesRead = callbackClient.client.GetStream().EndRead(ar);
            // 문자열로 넘어온 데이터를 파싱해서 출력해줍니다. 
            string readString = Encoding.Default.GetString(callbackClient.readByteData, 0, bytesRead); Console.WriteLine(readString);
            // 비동기서버에서 가장중요한 핵심입니다. 
            // 비동기서버는 while문을 돌리지않고 콜백메서드에서 다시 읽으라고 비동기명령을 내립니다. 
            callbackClient.client.GetStream().BeginRead(callbackClient.readByteData, 0, callbackClient.readByteData.Length, new AsyncCallback(DataReceived), callbackClient);

        }
    }
}


