using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;

using StudentInfo;
using Commands;

namespace ServerXmlNs
{
    class ServerXml
    {
        TcpClient tcpClient;
        NetworkStream networkStream;
        Socket clientSocket;
        IPEndPoint iPEndPoint;

        string ClientIP;
        string port;
        string command;
        string StudentNum;
        string PhoneNum;
        string StudentName;
        Command commandExe;

        public ServerXml() { commandExe = new Command(); }

        public void DataParsing(string data)
        {
            try
            {
                data = data.Replace(" ","");
                int startP = data.IndexOf("<IP>") + 4;
                int endP = data.IndexOf("</IP>") - startP;
                ClientIP = data.Substring(startP, endP);

                startP = data.IndexOf("<Port>") + 6;
                endP = data.IndexOf("</Port>") - startP;
                port = data.Substring(startP, endP);

                startP = data.IndexOf("<Command>") + 9;
                endP = data.IndexOf("</Command>", startP) - startP;
                command = data.Substring(startP, endP);

                startP = data.IndexOf("<Number>") + 8;
                endP = data.IndexOf("</Number>", startP) - startP;
                StudentNum = data.Substring(startP, endP);

                startP = data.IndexOf("<Phone>") + 7;
                endP = data.IndexOf("</Phone>", startP) - startP;
                PhoneNum = data.Substring(startP, endP);

                startP = data.IndexOf("<Name>") + 6;
                endP = data.IndexOf("</Name>", startP) - startP;
                StudentName = data.Substring(startP, endP);

                //Console.WriteLine(ClientIP + " " + port + " " + command + " " + StudentNum + " " + PhoneNum + " " + StudentName);
            } catch (ArgumentOutOfRangeException e)
            {
                throw e;
            }

            Student student = new Student();
            student.name = StudentName;
            student.phone_num = PhoneNum;
            student.number = StudentNum;
            commandExe.Read();
            if (command.Equals("Update")) ResponseXml(commandExe.Update(student));
            else if (command.Equals("Create")) ResponseXml(commandExe.Create(student));
            else if (command.Equals("Delete")) ResponseXml(commandExe.Delete(student));
            else if (command.Equals("Read")) ResponseAllXml(commandExe.Read());
            else Console.WriteLine("Wrong Command");
        }
        public void ResponseXml(int num)
        {
            string SendData = "<Root><Response>";
            switch (num)
            {
                case 1:
                    SendData += "Addition Success";
                    break;
                case 2:
                    SendData += "Removal Success";
                    break;
                case 3:
                    SendData += "Removal Fail: NoNumber";
                    break;
                case 4:
                    SendData += "Update Success";
                    break;
                case 5:
                    SendData += "Update Fail: NoData";
                    break;
                default:
                    SendData += "CommandFallout";
                    break;
            }
            SendData += "</Reponse></Root>";
            SendData = SendData.Replace(" ", "");
            byte[] data = System.Text.Encoding.Default.GetBytes(SendData);
            networkStream.Write(data, 0, data.Length);
        }
        public void ResponseAllXml(List<Student>students)
        {
            string SendData = "<Root><Response>StudentsList</Reponse><Data>";
            foreach (Student student in students)
            {
                SendData += "<Student><Number>" + student.number + "</Number><Phone>" + student.phone_num +
                    "</Phone><Name>" + student.name + "</Name></Student>";
            }
            SendData += "</Data></Root>";
            SendData = SendData.Replace(" ", "");
            byte[] data = System.Text.Encoding.Default.GetBytes(SendData);
            networkStream.Write(data, 0,data.Length);
            Console.WriteLine("ResponseAllXml() Success");
        }
        public void ServerStarted()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 6000);
            tcpListener.Start();
            Task ServerStart = Task.Run(() =>
            {
                tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("Connecting");
                networkStream = tcpClient.GetStream();
                clientSocket = tcpClient.Client;
                iPEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
                int length;
                string data = null;
                byte[] bytes = new byte[9999];
                while (true)
                {
                    while ((length = networkStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = Encoding.Default.GetString(bytes, 0, length);
                        DataParsing(data);
                    };
                }
            });
        }
    }
}



