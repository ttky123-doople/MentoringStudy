using System;
using System.Collections.Generic;
using System.Text;

using StudentInfo;
using ServerXmlNs;

namespace MiniProject
{
    class MainApp
    {
        static void Main(string[] args)
        {
            bool stop = false;
            ServerXml serverXml = new ServerXml();
            serverXml.ServerStarted();
            while (!stop)
            {
                try
                {
                    Console.WriteLine("If you want to stop Program, write exit");
                    string MainCommand = Console.ReadLine(); ;
                    if (MainCommand.Equals("exit")) stop = true;
                }
                catch (System.Exception e) { stop = true; }
            }
        }
    }
}
