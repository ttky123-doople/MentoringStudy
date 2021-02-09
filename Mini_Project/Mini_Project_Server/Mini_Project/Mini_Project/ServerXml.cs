using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

using StudentInfo;
using Commands;
using System.Text;

namespace ServerXmlNs
{
    public class ServerXml
    {
        TcpClient tcpClient;
        NetworkStream networkStream;
        Socket clientSocket;
        IPEndPoint iPEndPoint;

        string clientIP;
        string port;
        string command;
        string studentNum;
        string phoneNum;
        string studentName;
        Command commandExe;
        
        
        public ServerXml() { commandExe = new Command();}

        public void DataParsing(RequestMsg.Root data)
        {
            try
            {
                clientIP = data.Client.IP;
                port = data.Client.Port;
                command = data.Content.Command;
                studentNum = data.Content.Data.Student.Number;
                phoneNum = data.Content.Data.Student.Phone;
                studentName = data.Content.Data.Student.Name;

                Console.WriteLine(clientIP + " " + port + " command: " + command + " studentNum: " + studentNum + " phoneNum: " + phoneNum + " studentName: " + studentName);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

            Student student = new Student();
            student.name = studentName;
            student.phoneNum = phoneNum;
            student.number = studentNum;
            if (command.Equals("Update")) ResponseXml(commandExe.Update(student));
            else if (command.Equals("Create")) ResponseXml(commandExe.Create(student));
            else if (command.Equals("Delete")) ResponseXml(commandExe.Delete(student));
            else if (command.Equals("Read")) ResponseAllXml(commandExe.Read());
            else Console.WriteLine("Wrong Command");
        }
        public void ResponseXml(int num)
        {
            string sendData = "";
            switch (num)
            {
                case 1:
                    sendData += "Addition Success";
                    break;
                case 2:
                    sendData += "Removal Success";
                    break;
                case 3:
                    sendData += "Removal Fail: NoNumber";
                    break;
                case 4:
                    sendData += "Update Success";
                    break;
                case 5:
                    sendData += "Update Fail: NoData";
                    break;
                default:
                    sendData += "Command Fallout";
                    break;
            }

            ResponseMsg.Root rootResponseMsg = new ResponseMsg.Root(); ;
            rootResponseMsg.Response = sendData;
            XmlSerializer serializer = new XmlSerializer(typeof(ResponseMsg.Root));
            serializer.Serialize(networkStream, rootResponseMsg);
            Console.WriteLine("ResponseXml() Success");
        }
        public void ResponseAllXml(List<Student> students)
        {
            ResponseList.Root rootResponseList = new ResponseList.Root();
            rootResponseList.Response = "Students List";

            foreach (Student student in students)
            {
                ResponseList.Student tempStudent = new ResponseList.Student();
                tempStudent.Name = student.name;
                tempStudent.Number = student.number;
                tempStudent.Phone = student.phoneNum;
                rootResponseList.Data.Student.Add(tempStudent);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(ResponseList.Root));
            serializer.Serialize(networkStream, rootResponseList);
            Console.WriteLine("ResponseAllXml() Success");
        }
        public void ServerStarted()
        {
            Console.WriteLine(commandExe.GetList());
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 6000);
            tcpListener.Start();
            Task ServerStart = Task.Run(() =>
            {
                tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("Connecting");
                networkStream = tcpClient.GetStream();
                clientSocket = tcpClient.Client;
                iPEndPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
                byte[] bytes = new byte[1024];
                while (true)
                {
                    int length = 0;
                    string data = null;
                    while ((length = networkStream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        Console.WriteLine("Reading");
                        data = Encoding.Default.GetString(bytes, 0, length);
                        XmlSerializer serializer = new XmlSerializer(typeof(RequestMsg.Root));
                        StringReader reader = new StringReader(data);
                        RequestMsg.Root responseMsg = (RequestMsg.Root)serializer.Deserialize(reader);
                        DataParsing(responseMsg);
                        break;
                    };
                }
            });
        }
    }
}



