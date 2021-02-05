using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using StudentInfo;

namespace TxtReadWriteNs
{
    class TxtReadWrite
    {
        private static string currDir = Environment.CurrentDirectory;
        public void ReadTxt(List<Student> students)
        {
            string[] lines = File.ReadAllLines(currDir+"/Student.txt");
            string[] tempWords = new string[3];
            int count = 0;
            try
            {
                foreach (string line in lines)
                {
                    tempWords = line.Split(' ');
                    if (tempWords.Length != 3) return;
                    students.Add(new Student() { name = tempWords[0], phone_num = tempWords[1], number = tempWords[2] });
                    count++;

                }
                Console.WriteLine("ReadTxt() Success");
            }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("ReadTxt() Fail"); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        public void WriteTxt(List<Student> students)
        {
            string number = "";
            string name = "";
            string phone_num = "";
            using (StreamWriter outputTxt = new StreamWriter("Student.txt", false))
            {
                foreach (Student student in students)
                {
                    name = student.name;
                    phone_num = student.phone_num;
                    number = student.number;

                    string line = name + " " + phone_num + " " + number;
                    outputTxt.WriteLine(line);
                }
            }
            Console.WriteLine("WriteTxt() Success");
        }
        public void AddWriteTxt(Student newStudent)
        {
            string name = newStudent.name;
            string phone_num = newStudent.phone_num;
            string number = newStudent.number;
            using (StreamWriter outputTxt = new StreamWriter("Student.txt", true))
            {
                string line = name + " " + phone_num + " " + number;
                outputTxt.WriteLine(line);
            }
            Console.WriteLine("AddWriteTxt() Success");
        }
    }
}
