using System;
using System.Collections.Generic;
using System.Text;

namespace _1_N_Chatting_Server
{
    public class Student
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Number { get; set; }

        public string Command { get; set; }
        public Student()
        {
            Name = "";
            Phone = "";
            Number = "";
            Command = "";
        }
    }
}
