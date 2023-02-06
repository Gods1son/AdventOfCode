using System;
using System.IO;
using System.Text;

namespace Day10
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
            string line;
            int cycles = 0;
            int points = 1;
            int signalStrength = 0;
            int target = 20;
            int addTarget = 40;
            int maxTarget = 220;
            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("noop"))
                    {
                        cycles += 1;
                        continue;
                    }
                    cycles += 2;
                    int number = 0;
                    if(line.IndexOf("addx -") > -1)
                    {
                        //subtraction
                        number = 0 - int.Parse(line.Substring(6));
                    }
                    else
                    {
                        //addition
                        number = int.Parse(line.Substring(5));
                    }
                    if(cycles >= target)
                    {
                        signalStrength += (target * points);
                        target += addTarget;
                        if(target > maxTarget)
                        {
                            Console.WriteLine("PART ONE");
                            Console.WriteLine("Max strength is " + signalStrength);
                            return;
                        }
                    }
                    points += number;
                }
            }
        }

        public static StringBuilder sB = new StringBuilder();
        public static int cyclesTwo = 0;
        public static int pointsTwo = 1;
        public static void RunProgramTwo()
        {
            
            string fileName = "input.txt";
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);
            string line;
            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    ProcessCycle();
                    if (!line.StartsWith("noop"))
                    {
                        ProcessCycle();
                        int number = 0;
                        if (line.IndexOf("addx -") > -1)
                        {
                            //subtraction
                            number = 0 - int.Parse(line.Substring(6));
                        }
                        else
                        {
                            //addition
                            number = int.Parse(line.Substring(5));
                        }
                        pointsTwo += number;
                    }
                }
                Console.WriteLine("PART TWO");
                Console.WriteLine(sB.ToString());
            }
        }

        static void ProcessCycle()
        {
            var spritePosition = pointsTwo;
            if (spritePosition - 1 <= cyclesTwo && cyclesTwo <= spritePosition + 1)
            {
                sB.Append("#");
            }
            else
            {
                sB.Append(".");
            }

            cyclesTwo++;
            if (cyclesTwo % 40 == 0)
            {
                sB.AppendLine();
                cyclesTwo = 0;
            }
        }
    }
}
