using System;

namespace Local_Method
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 1;
            int b = 2;
            int c = 3;
            Console.WriteLine(MyCalc(a, b, c));
        }

        static int MyCalc(int a, int b, int c)
        {
            int n1 = MyFormula(a, b);
            int n2 = MyFormula(b, c);
            int n3 = MyFormula(a, c);

            return Math.Max(n1,Math.Max(n2, n3));
            int MyFormula(int n1, int n2)
            {
                int result = n1 * n2;
                return result;
            }
        }
    }
}
