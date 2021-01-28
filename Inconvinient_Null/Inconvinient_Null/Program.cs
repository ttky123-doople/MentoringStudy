using System;

namespace Inconvinient_Null
{
    class Program
    {
        static void Main(string[] args)
        {
            string? a = "hello world";
            string str = "a is ";
            string temp = a ?? "null";
            Console.WriteLine(str + temp);

        }
    }
}
