using System;

namespace TypeCast_Class
{
    class Mammal
    {
        public void Reproduce()
        {
            Console.WriteLine("Reproduced a " + this.GetType().Name);
        }
    }
    class Dog : Mammal
    {
        public void Bark()
        {
            Console.WriteLine("Bark");
        }
    }
    class Cat : Mammal
    {
        public void Meow()
        {
            Console.WriteLine("Meow");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Hello World");
        }
    }
}
