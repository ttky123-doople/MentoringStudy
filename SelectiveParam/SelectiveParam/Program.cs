using System;

namespace SelectiveParam
{
    class Program
    {
       
        static void Person(string name="None", int age=0, string email = "None")
        {
            Console.WriteLine("Name: " + name + " age: " + age + " email: " + email);
        }
        static void Main(string[] args)
        {
            Person("Foo");
            Person("Foo", 26);
            Person("Foo", 26, "ttky123@doople.net");
            Person(name: "Foo", age: 26, email: "ttky123@doople.net");
        }
    }
    
}
