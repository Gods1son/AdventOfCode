using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        public static Dictionary<string, int> items = new Dictionary<string, int>();
        
        static void Main(string[] args)
        {
            try
            {
                // generate the pointing system
                for (int i = 0; i < 52; i++)
                {
                    if (i < 26)
                    {
                        items.Add(string.Format("{0}", Convert.ToChar('a' + i)), (i + 1));
                    }
                    else
                    {
                        items.Add(string.Format("{0}", Convert.ToChar('A' + (i - 26))), (i + 1));
                    }
                }
                RunProgramPartOne();
                RunProgramPartTwo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message?.ToString());
            }
        }

        public static void RunProgramPartOne()
        {
            string fileName = "input.txt";
            string line;
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);
            int totalPoints = 0;
            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    int half =  (int)((line.Length / 2));
                    // divide runsacks into two compartments
                    var firstComp = line.Substring(0, half).ToCharArray().ToList();
                    var secondComp = line.Substring(half).ToCharArray().ToList();
                    // find item in both compartments
                    char result = firstComp.Intersect(secondComp).FirstOrDefault();
                    string item = result.ToString();
                    // check point from dictionary
                    int point = items[item];
                    //sum up points
                    totalPoints += point;
                }
            }
            Console.WriteLine("Part One");
            Console.WriteLine("Total point is " + totalPoints);
        }

        public static void RunProgramPartTwo()
        {
            const int GROUP = 3;
            string fileName = "input.txt";
            string line;
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);
            int totalPoints = 0;
            List<List<char>> chars = new List<List<char>>();
            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    var grp = line.ToCharArray().ToList();
                    if (chars.Count < GROUP) // add 3 groups together 
                    {
                        chars.Add(grp);
                    }
                    if(chars.Count == GROUP) // find badge among group of 3 and reset list
                    {
                        var firstGrp = chars[0];
                        var secondGrp = chars[1];
                        var thirdGrp = chars[2];
                        char result = firstGrp.Intersect(secondGrp).Intersect(thirdGrp).FirstOrDefault();
                        string item = result.ToString();
                        // check point from dictionary
                        int point = items[item];
                        //sum up points
                        totalPoints += point;
                        // reset list
                        chars = new List<List<char>>();
                    }
                }
            }
            Console.WriteLine("Part Two");
            Console.WriteLine("Total point among group of 3 is " + totalPoints);
        }
    }
}
