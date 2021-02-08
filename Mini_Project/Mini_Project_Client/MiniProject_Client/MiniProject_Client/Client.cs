using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Xml;
using System.Net;
using System.Threading;
using System.Collections.Concurrent;

namespace MiniProject_Client
{
    class Client
    {
        TcpClient client = null;
        string ServerIp = "";
        string selfIp = "";
        string inputName="";
        string inputPhone="";
        string inputNum="";
        Thread receiveThread = null;
        ConcurrentBag<string> sentMsgBag;
        ConcurrentBag<string> rcvMsgBag;
        string port;
        public Client(string ip, string port )
        {
            receiveThread = new Thread(ReceiveMessage);
            client = new TcpClient();
            ServerIp = ip;
            this.port = port;
            sentMsgBag = new ConcurrentBag<string>();
            rcvMsgBag = new ConcurrentBag<string>();
            Connect();
            Run();
        }
        public void Run()
        {
            receiveThread = new Thread(ReceiveMessage);
            selfIp = GetSelfIP();
            while (true)
            {
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("==========MiniProject_Client==========");
                Console.WriteLine("1.서버연결");
                Console.WriteLine("2.Message 보내기");
                Console.WriteLine("3.보낸 Message확인");
                Console.WriteLine("4.받은 Message확인");
                Console.WriteLine("0.종료");
                Console.WriteLine("==============================");
                

                string key = Console.ReadLine();
                int order = 0;
                if (int.TryParse(key, out order))
                {
                    switch (order)
                    {
                        //1:연결
                        //2:메세지 보내기
                        //3:보낸 메세지 확인
                        //4:받은 메세지 확인(필요여부 생각)
                        //5:연결 종료
                        case 1:
                            {
                                if (client != null)
                                    Console.WriteLine("Already Connected to Server");
                                else
                                    Connect();
                                break;
                            }
                        case 2:
                            {
                                SendMessage();

                                break;
                            }
                        case 3:
                            {
                                ViewSentMsg();
                                break;
                            }
                        case 4:
                            {
                                ViewRecvMsg();
                                break;
                            }
                        case 0:
                            {
                                receiveThread.Abort();
                                if (client != null)
                                    client.Close();
                                return;
                            }
                    }
                }
            }
        }
        private void SendMessage()
        {
            string command = "";
            string message = @"<?xml version=""1.0"" encoding=""utf - 8""?>
<Root>
<Client>
    <IP>" + selfIp + @"</IP>
    <Port>" + port + @"</Port>
</Client>
<Content>
     <Command>" + command + @"</Command>
     <Data>"+@"
          <Student>
             <Number>" + inputNum + @"</Number>
             <Phone>"+ inputPhone + @"</Phone>
             <Name>" + inputName + @"</Name>
          </Student>
     </Data>
</Content>
</Root>";
          
            Console.WriteLine("==========Service==========");
            Console.WriteLine("1.Create");
            Console.WriteLine("2.Read");
            Console.WriteLine("3.Update");
            Console.WriteLine("4.Delete");
            Console.WriteLine("0.종료");
            Console.WriteLine("==============================");
            string serviceKey = Console.ReadLine();
            int serviceOrder;
            if (int.TryParse(serviceKey, out serviceOrder))
            {
                switch (serviceOrder)
                {
                    case 0:
                        {
                            return;
                        }
                    case 1:
                        {
                            command = "Create";
                            Console.Write("Name: ");
                            inputName = Console.ReadLine();
                            Console.Write("Phone: ");
                            inputPhone = Console.ReadLine();
                            Console.Write("Number: ");
                            inputNum = Console.ReadLine();
                            
                            message = @"<?xml version=""1.0"" encoding=""utf - 8""?>
<Root>
<Client>
    <IP>" + selfIp + @"</IP>
    <Port>" + port + @"</Port>
</Client>
<Content>
     <Command>" + command + @"</Command>
     <Data>" + @"
          <Student>
             <Number>" + inputNum + @"</Number>
             <Phone>" + inputPhone + @"</Phone>
             <Name>" + inputName + @"</Name>
          </Student>
     </Data>
</Content>
</Root>";
                            break;
                        }
                    case 2:
                        {   //Read는 파일 내용 쭉 읽어오기
                            command = "Read";
                            message = @"<?xml version=""1.0"" encoding=""utf - 8""?>
<Root>
<Client>
    <IP>" + selfIp + @"</IP>
    <Port>" + port + @"</Port>
</Client>
<Content>
     <Command>" + command + @"</Command>
     <Data>" + @"
     </Data>
</Content>
</Root>";
                            break;
                        }

                    case 3: //업데이트는 키를 먼저, 나머지 자료를 갱신
                        {
                            command = "Update";
                            Console.Write("Number: ");
                            inputNum = Console.ReadLine();
                            Console.Write("Name: ");
                            inputName = Console.ReadLine();
                            Console.Write("Phone: ");
                            inputPhone = Console.ReadLine();
                            
                            message = @"<?xml version=""1.0"" encoding=""utf - 8""?>
<Root>
<Client>
    <IP>" + selfIp + @"</IP>
    <Port>" + port + @"</Port>
</Client>
<Content>
     <Command>" + command + @"</Command>
     <Data>" + @"
          <Student>
             <Number>" + inputNum + @"</Number>
             <Phone>" + inputPhone + @"</Phone>
             <Name>" + inputName + @"</Name>
          </Student>
     </Data>
</Content>
</Root>";
                            break;
                        }
                    case 4: //삭제는 키만 있으면 됨
                        {
                            command = "Delete";
                            Console.Write("Number: ");
                            inputNum = Console.ReadLine();
                            message = @"<?xml version=""1.0"" encoding=""utf - 8""?>
<Root>
<Client>
    <IP>" + selfIp + @"</IP>
    <Port>" + port + @"</Port>
</Client>
<Content>
     <Command>" + command + @"</Command>
     <Data>" + @"
          <Student>
             <Number>" + inputNum + @"</Number>
          </Student>
     </Data>
</Content>
</Root>";
                            break;
                        }
                }
            }
            if (string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Message Empty");
                Console.ReadKey();
                return;
            }

            byte[] msgByte = new byte[1024];
            msgByte = Encoding.Default.GetBytes(message);
            client.GetStream().Write(msgByte, 0, msgByte.Length);
            sentMsgBag.Add(DateTime.Now.ToString("[yyyy-MM-dd hh:mm:ss]\n " + message));
            #region XMLFormat
            /*
 * <?xml version="1.0" encoding="utf-8"?>
<Client>
<IP>192.168.0.254</IP>
<Port>3654</Port>
</Client>
<Content>
<Command>update</Command>
<data>
<strudent>
 <Number>16</Number>
 <Phone>010-564-654514</Phone>
 <Name>Park</Name>
</strudent>
</data>
</Content>*/
            //client.GetStream().Write(); 
            #endregion

        }



        private string GetSelfIP()
        {
            string strHostName = "";

            strHostName = Dns.GetHostName();

            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList;

            return addr[addr.Length - 1].ToString();
        }

        private void Connect()
        {
            client = new TcpClient();
            try
            {
                
                client.Connect(IPAddress.Parse(ServerIp), int.Parse(this.port));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
                return;
            }
            receiveThread.Start();
            Console.WriteLine("Connected to Server");
        }

        private void ReceiveMessage()
        {
            string recvMessage = "";
            //List<string> recvMsgList = new List<string>();
            while (true)
            {
                byte[] receiveByte = new byte[1024];
                client.GetStream().Read(receiveByte, 0, receiveByte.Length);
                recvMessage = Encoding.Default.GetString(receiveByte, 0, receiveByte.Length);
                #region XmlParsing
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(recvMessage);
                XmlNodeList xnList = doc.GetElementsByTagName("Response");
                rcvMsgBag.Add(DateTime.Now.ToString("[yyyy-MM-dd hh:mm:ss]\n " + recvMessage));
                foreach(XmlNode elem in xnList)
                {
                    switch(elem.InnerText)
                    {
                        case "StudentsList": 
                            {
                                foreach(var item in elem.ChildNodes)
                                {
                                    Console.WriteLine(elem.Name + ": " + elem.InnerText);
                                }
                                Console.ReadKey();
                                break; 
                            }
                        default:
                            {
                                Console.WriteLine(elem.InnerText);
                                break;
                            }

                    }
                }
                #endregion
                Thread.Sleep(50);
            }
        }
        private void ViewRecvMsg()
        {
            if (rcvMsgBag.Count == 0)
            {
                Console.WriteLine("No message");
                Console.ReadKey();
                return;
            }
                
            foreach(var elem in rcvMsgBag)
            {
                Console.WriteLine(elem);
                Console.WriteLine();
            }
            Console.WriteLine("==================================================");
        }
        private void ViewSentMsg()
        {
            if (sentMsgBag.Count == 0)
            {
                Console.WriteLine("No message");
                Console.ReadKey();
                return;
            }

            foreach (var elem in sentMsgBag)
            {
                Console.WriteLine(elem);
                Console.WriteLine();
            }
            Console.WriteLine("==================================================");
        }
    }
}
