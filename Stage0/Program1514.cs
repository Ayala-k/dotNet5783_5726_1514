using System;

namespace targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Wellcome1514();
            Wellcome5726();
            Console.ReadKey();
        }

        static partial void Wellcome5726();
        private static void Wellcome1514()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}