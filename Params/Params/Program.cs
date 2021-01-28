using System;

namespace Params
{
    class Program
    {
        static int Sum(params int[] arr)
        {
            int sum = 0;

            foreach (int elem in arr)
            {
                sum += elem;
            }

            return sum;
        }


        static void Main(string[] args)
        {
            int n1 = Sum(1, 2, 3, 4, 5, 6, 7, 8, 9);
            int n2 = Sum(1, 2, 3, 4, 5);

            Console.WriteLine("Sum with 9 params:  " + n1);
            Console.WriteLine("Sum with 5 params:  " + n2);
        }
        
    }
}
