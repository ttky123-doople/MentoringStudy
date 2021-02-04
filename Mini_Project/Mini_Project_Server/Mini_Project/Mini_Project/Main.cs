using System;
using System.Collections.Generic;
using System.Text;

using StudentInfo;
using ServerXmlNs;

namespace Mini_Project
{
    class MainApp
    {
        static List<Student> students = new List<Student>();
        static void Main(string[] args)
        {
            bool stop = false;
            ServerXml serverXml = new ServerXml();
            serverXml.ServerStarted();
            /*foreach (Student student in students)
            {
                Console.WriteLine(student.name + " " + student.phone_num + " " + student.number);
            }*/
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
