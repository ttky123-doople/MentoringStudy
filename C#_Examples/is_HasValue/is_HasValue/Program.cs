using System;

namespace is_HasValue
{
    class Program
    {
        static void Main(string[] args)
        {
            int? a = 100;

            if(a.HasValue)
            {
                Console.WriteLine("a is {0}",a);
                //Console.WriteLine(valueOfA);
            }

            else
            {
                Console.WriteLine("a is Null");
            }
        }
    }
}
