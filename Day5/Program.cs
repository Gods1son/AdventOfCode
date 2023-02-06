using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day5
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
            // string line;
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);

            using (var sr = new StreamReader(path))
            {
                var file = sr.ReadToEnd();
                var files = file.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                var separate = files.IndexOf("");
                var stacks = files.Take(separate - 1).ToList();
                List<List<string>> stacksMaster = new List<List<string>>();
                var instructs = files.TakeLast(files.Count - separate - 1).ToList();

                // arrange into stacks
                foreach (string stack in stacks)
                {
                    int count = (int)(stack.Length / 3);
                    for (var i = 0; i < count; i++)
                    {
                        var start = (3 * i) + i;
                        if(start < stack.Length)
                        {
                            var crate = stack.Substring(start, 3).Trim().Replace("[", "").Replace("]", "");
                            if (stacksMaster.Count < (i + 1))
                            {
                                stacksMaster.Add(new List<string>());
                            }
                            if (crate != "")
                            {
                                stacksMaster[i].Insert(0, crate);
                            }
                        }
                    }
                }
                // do the move
                foreach (string inst in instructs)
                {
                    // move 1 from 2 to 1
                    var numbs = 
                    Regex.Matches(inst, @"\d+").OfType<Match>().Select(e => int.Parse(e.Value)).ToList();
                    int toMove = numbs[0];
                    int moveFrom = numbs[1];
                    int moveTo = numbs[2];

                    // [1,2,3,4,5,6,7]
                    //FOR PART ONE, REVERSE the order
                    //var copy = stacksMaster[moveFrom - 1].TakeLast(toMove).Reverse().ToList();

                    // FOR PART TWO, NO NEED TO DO THE REVERSE
                    var copy = stacksMaster[moveFrom - 1].TakeLast(toMove).ToList();

                    // copy to dest
                    stacksMaster[moveTo - 1].AddRange(copy);
                    // remove from source
                    stacksMaster[moveFrom - 1] = stacksMaster[moveFrom - 1].Take(stacksMaster[moveFrom - 1].Count() - toMove).ToList();
                }
                // get last elements of list
                string finalText = "";
                foreach (var stacs in stacksMaster)
                {
                    finalText += stacs.Last();
                }
                Console.WriteLine("The last crates are " + finalText);
            }
        }
    }
}
