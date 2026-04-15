using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleApp
{
    public class Programm
    {
        public static string filePath = "C:/Users/User/Documents/Schule/CHC/CHC";
        public static ReadWrite _rw = new ReadWrite();
        public static void Main()
        {
            _rw.WriteIntoFile(filePath,"test.txt", "test1");
            Console.WriteLine(_rw.ReadFromFile(filePath, "test.txt"));
        }

        

        
    }
}
