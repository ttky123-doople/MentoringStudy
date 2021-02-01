using System;

namespace NULL_ByungHap
{
    class Program
    {
        static void Main(string[] args)
        {
            string?[] arr = { "경기도", null, "분당구", null, "황새울로" };

            foreach (var elem in arr)
            {
                string temp = elem ?? "null";
                Console.Write(temp + " ");
            }

            Console.WriteLine();
        }
    }
}
