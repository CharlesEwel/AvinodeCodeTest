using System;
using System.IO;

namespace AvinodeCodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:/Users/Chuck/Downloads/Wyvern Menu.txt";
            Console.WriteLine(File.ReadAllText(path));
            Console.Read();
        }
    }
}
