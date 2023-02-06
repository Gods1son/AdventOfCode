using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
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
            string fileName = "input.txt";
            string line;
            int totalIntersection = 0;
            int anyIntersection = 0;
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);
            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    // split by comma
                    var fst = line.Split(",")[0];
                    var snd = line.Split(",")[1];

                    //split each by dash
                    var firstFst = int.Parse(fst.Split("-")[0]);
                    var secondFst = int.Parse(fst.Split("-")[1]);
                    var diffFst = secondFst - firstFst;
                    List<int> arrayFst = new List<int>();
                    for(var i = 0; i <= diffFst; i++)
                    {
                        int value = firstFst + i;
                        arrayFst.Add(value);
                    }

                    //split each by dash
                    var firstSnd = int.Parse(snd.Split("-")[0]);
                    var secondSnd = int.Parse(snd.Split("-")[1]);
                    var diffSnd = secondSnd - firstSnd;
                    List<int> arraySnd = new List<int>();
                    for (var i = 0; i <= diffSnd; i++)
                    {
                        int value = firstSnd + i;
                        arraySnd.Add(value);
                    }

                    // check intersection and if it equals
                    int arrayFstC = arrayFst.Count();
                    int arraySndC = arraySnd.Count();
                    var intersectC = arrayFst.Intersect(arraySnd).Count();

                    // part one, total intersection
                    if(intersectC == arrayFstC || intersectC == arraySndC)
                    {
                        totalIntersection += 1;
                    }

                    //part two, any intersection
                    if(intersectC > 0)
                    {
                        anyIntersection += 1;
                    }
                }
            }
            Console.WriteLine("Part One");
            Console.WriteLine("Total intersection is " + totalIntersection);
            
            Console.WriteLine("Part Two");
            Console.WriteLine("Any intersection is " + anyIntersection);
        }
    }
}
