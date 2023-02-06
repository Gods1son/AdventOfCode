using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RunProgram();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message?.ToString());
            }
        }

        public static void RunProgram()
        {
            // the input data is inside the text file below
            string fileName = "input.txt";
            //for storing the max calories
            int maxCalories = 0;
            //for adding the calories per Elf
            int totalCalories = 0;
            string line;
            List<int> allCalories = new List<int>();

            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);

            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        int cal = int.Parse(line);
                        // add all the calories before the white line
                        totalCalories += cal;
                    }
                    if (string.IsNullOrWhiteSpace(line) || sr.EndOfStream)
                    {
                        // check if the current total calories is higher than the record max calories
                        maxCalories = totalCalories > maxCalories ? totalCalories : maxCalories;

                        // for part two, add total calories into the list
                        allCalories.Add(totalCalories);

                        // reset total Calories back to 0 after white line for next Elf
                        totalCalories = 0;
                    }
                }
            }

            Console.WriteLine("Part One");
            Console.WriteLine("The max calories is " + maxCalories);
            
            //get top 3 and sum
            int top3 = allCalories.OrderByDescending(x => x).Take(3).Sum();
            Console.WriteLine("Part Two");
            Console.WriteLine("The calories by top 3 Elfs is " + top3);
        }
    }
}
