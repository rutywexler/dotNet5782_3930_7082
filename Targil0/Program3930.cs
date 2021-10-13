using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome3930();
            Welcome7082();
            Console.ReadLine();


        }
        static partial void Welcome7082();
        private static void welcome3930()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application");
        }
    }
