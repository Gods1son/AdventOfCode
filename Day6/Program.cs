using System;
using System.Collections.Generic;
using System.IO;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RunProgram();
                RunProgramTwo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message?.ToString());
            }
        }

        public static void RunProgram()
        {
            string fileName = "input.txt";
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);
            string line = File.ReadAllText(path);
            var window = new List<char>();
            
            for(var i = 3; i < line.Length; i++)
            {
                window.Clear();
                bool valid = true;
                for (int prevChar = 0; prevChar < 4; prevChar++)
                {   
                    var character = line[i - prevChar];
                    if(window.Contains(character))
                    {
                        valid = false;
                        break;
                    }
                    else
                    {
                        window.Add(character);
                    }
                }
                if (valid)
                {
                    Console.WriteLine("The position is " + (i + 1));
                    break;
                }
            }
        }

        public static void RunProgramTwo()
        {
            string fileName = "input.txt";
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);
            string line = File.ReadAllText(path);
            var window = new List<char>();
            var windowSize = 14;
            for (var i = windowSize - 1; i < line.Length; i++)
            {
                window.Clear();
                bool valid = true;
                for (int prevChar = 0; prevChar < windowSize; prevChar++)
                {
                    var character = line[i - prevChar];
                    if (window.Contains(character))
                    {
                        valid = false;
                        break;
                    }
                    else
                    {
                        window.Add(character);
                    }
                }
                if (valid)
                {
                    Console.WriteLine("The position is " + (i + 1));
                    break;
                }
            }
        }
    }
}
