using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Xml;
using System.Net;
using System.Threading;

namespace MiniProject_Client
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Temp
            /*string selfIp = "MyIP";
     string port = "MyPort";
     string inputNum = "MyNum";
     string inputPhone = "MyPhone";
     string inputName = "MyName";
     string command = "MyCommand";
     string msg = @"<?xml version=""1.0"" encoding=""utf - 8""?>
<Root>
<Client>
<IP>" + selfIp + @"</IP>
<Port>" + port + @"</Port>
</Client>;
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
     Console.WriteLine(msg);*/ 
            #endregion

            Console.WriteLine("IP");
            string ip = Console.ReadLine();
            Console.WriteLine("Port");
            string port = Console.ReadLine();

            m_Client myClient = new m_Client(ip, port);

        }
    }
}
