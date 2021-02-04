using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Linq;

using StudentInfo;
using TxtReadWriteNs;

namespace Commands
{
    //Linq로 재작성
    class Command
    {
        List<Student> students = new List<Student>();
        private TxtReadWrite txtReadWrite;
        public Command()
        {
            txtReadWrite = new TxtReadWrite();
        }
        public int Create(Student newStudent)
        {
            students.Add(newStudent);
            return 1;
        }
        public int Delete(Student newStudent)
        {
            foreach (Student student in students)
            {
                if (student.number.Equals(newStudent.number))
                {
                    students.Remove(student);
                    return 2;
                }
            }
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
                    students[index].phone_num = newStudent.phone_num;
                    return 4;
                }
                index++;
            }
            return 5;
        }
        public List<Student> Read()
        {
            txtReadWrite.ReadTxt(students);
            return students;
        }
    }
}
