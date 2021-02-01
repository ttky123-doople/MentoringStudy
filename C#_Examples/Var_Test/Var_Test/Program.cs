using System;

namespace Var_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] strArr = { "경기도", "성남시", "분당구", "황새울로" };

            foreach(var elem in strArr)
            {
                Console.Write(elem + " ");
            }
            Console.WriteLine();
        }
    }
}
