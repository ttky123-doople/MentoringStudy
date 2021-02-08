using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

using StudentInfo;
using TxtReadWriteNs;

namespace Commands
{
    //Linq로 재작성
    public class Command
    {
        public List<Student> students{ get;set; }
        private TxtReadWrite txtReadWrite;
        public Command()
        {
            students = new List<Student>();
            txtReadWrite = new TxtReadWrite();
        }
        public int Create(Student newStudent)
        {
            students.Add(newStudent);
            txtReadWrite.AddWriteTxt(newStudent);
            Console.WriteLine("Addition Success");
            return 1;
        }
        public int Delete(Student newStudent)
        {
            foreach (Student student in students)
            {
                if (student.number.Equals(newStudent.number))
                {
                    students.Remove(student);
                    txtReadWrite.WriteTxt(students);
                    Console.WriteLine("Removal Success");
                    return 2;
                }
            }
            Console.WriteLine("Removal Fail");
            return 3;
        }
        public int Update(Student newStudent)
        {
            int index = 0;
            foreach (Student student in students)
            {
                if (student.number.Equals(newStudent.number))
                {
                    students[index].name = newStudent.name;
                    students[index].phoneNum = newStudent.phoneNum;
                    txtReadWrite.WriteTxt(students);
                    Console.WriteLine("Update() Success");
                    return 4;
                }
                index++;
            }
            Console.WriteLine("Update() Fail");
            return 5;
        }
        public List<Student> Read()
        {
            txtReadWrite.ReadTxt(students);
            Console.WriteLine("Read() Success");
            return students;
        }
        public string GetList()
        {
            txtReadWrite.ReadTxt(students);
            return "GetList() Success";
        }
    }
}
