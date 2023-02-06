using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day11
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

        public class Monkey
        {
            public Monkey(string name, List<long> items, string operationSign, string operationBy, long divisibleBy, int ifTrue, int ifFalse)
            {
                this.Name = name;
                this.Items = items;
                this.OperationSign = operationSign;
                this.OperationBy = operationBy;
                this.DivisibleBy = divisibleBy;
                this.IfTrue = ifTrue;
                this.IfFalse = ifFalse;
                this.TotalInspected = 0;
            }
            public string Name { get; set; }
            public List<long> Items { get; set; }
            public string OperationSign { get; set; }
            public string OperationBy { get; set; }
            public long DivisibleBy { get; set; }
            public int IfTrue { get; set; }
            public int IfFalse { get; set; }
            public int TotalInspected { get; set; }
        }

        public static void RunProgram()
        {
            string fileName = "input.txt";
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);

            List<Monkey> monkeys = new List<Monkey>();
            string instruct = File.ReadAllText(path);
            var totalMonkey = instruct.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach(var each in totalMonkey)
            {
                var profile = each.Split(Environment.NewLine).ToList();
                string name = profile[0].Replace(":", "");
                string startText = "Starting items: ";
                string items = profile[1].Substring(startText.Length + 1);
                List<long> itemList = items.Split(',').Select(x => long.Parse(x.Trim())).ToList();

                // operation
                string operateText = "  Operation: new = old ";
                string operation = profile[2].Substring(operateText.Length);
                var operate = operation.Split(" ");
                string operateSign = operate[0];
                string operateBy = operate[1];
                // test
                string testText = "Test: divisible by ";
                long divisibleBy = long.Parse(profile[3].Substring(testText.Length + 1));
                // if true
                string trueText = "    If true: throw to monkey ";
                int ifTrue = int.Parse(profile[4].Substring(trueText.Length));
                // if false
                string falseText = "    If false: throw to monkey ";
                int ifFalse = int.Parse(profile[5].Substring(falseText.Length));
                Monkey m = new Monkey(name, itemList, operateSign, operateBy, divisibleBy, ifTrue, ifFalse);
                monkeys.Add(m);
            }

            // perform task for each monkey
            int round = 0;
            int totalRound = 20;
            while(round < totalRound)
            {
                foreach (Monkey m in monkeys)
                {
                    foreach (long item in m.Items.ToList())
                    {
                        //each item
                        long by = 0;
                        if (m.OperationBy == "old")
                        {
                            by = item;
                        }
                        else
                        {
                            by = long.Parse(m.OperationBy);
                        }
                        // sign
                        long worryLevel = item;
                        if (m.OperationSign == "*")
                        {
                            worryLevel = item * by;
                        }
                        else if (m.OperationSign == "+")
                        {
                            worryLevel = item + by;
                        }

                        //divide worry by 3
                        worryLevel /= 3;
                        if (worryLevel % m.DivisibleBy == 0)
                        {
                            // true, transfer to true
                            monkeys[m.IfTrue].Items.Add(worryLevel);
                        }
                        else
                        {
                            // false, transfer to false
                            monkeys[m.IfFalse].Items.Add(worryLevel);
                        }
                        // remove from monkey
                        m.TotalInspected += 1;
                        m.Items.Remove(item);
                    }
                }
                round++;
            }

            // top 2 monkeys
            var mkeys = monkeys.OrderByDescending(x => x.TotalInspected).Take(2).ToList();
            var finalNumbers = mkeys[0].TotalInspected * mkeys[1].TotalInspected;
            Console.WriteLine("PART ONE");
            Console.WriteLine("Total is " + finalNumbers);
        }

        public static void RunProgramTwo()
        {
            string fileName = "input.txt";
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);

            List<Monkey> monkeys = new List<Monkey>();
            string instruct = File.ReadAllText(path);
            var totalMonkey = instruct.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var each in totalMonkey)
            {
                var profile = each.Split(Environment.NewLine).ToList();
                string name = profile[0].Replace(":", "");
                string startText = "Starting items: ";
                string items = profile[1].Substring(startText.Length + 1);
                List<long> itemList = items.Split(',').Select(x => long.Parse(x.Trim())).ToList();

                // operation
                string operateText = "  Operation: new = old ";
                string operation = profile[2].Substring(operateText.Length);
                var operate = operation.Split(" ");
                string operateSign = operate[0];
                string operateBy = operate[1];
                // test
                string testText = "Test: divisible by ";
                long divisibleBy = long.Parse(profile[3].Substring(testText.Length + 1));
                // if true
                string trueText = "    If true: throw to monkey ";
                int ifTrue = int.Parse(profile[4].Substring(trueText.Length));
                // if false
                string falseText = "    If false: throw to monkey ";
                int ifFalse = int.Parse(profile[5].Substring(falseText.Length));
                Monkey m = new Monkey(name, itemList, operateSign, operateBy, divisibleBy, ifTrue, ifFalse);
                monkeys.Add(m);
            }

            // perform task for each monkey
            int round = 0;
            int totalRound = 10000;
            var cutOff = 1L;
            foreach (var monkey in monkeys)
            {
                cutOff *= monkey.DivisibleBy;
            }
            while (round < totalRound)
            {
                foreach (Monkey m in monkeys)
                {
                    foreach (long item in m.Items.ToList())
                    {
                        //each item
                        long by = 0;
                        if (m.OperationBy == "old")
                        {
                            by = item;
                        }
                        else
                        {
                            by = long.Parse(m.OperationBy);
                        }
                        // sign
                        long worryLevel = item;
                        if (m.OperationSign == "*")
                        {
                            worryLevel = item * by;
                        }
                        else if (m.OperationSign == "+")
                        {
                            worryLevel = item + by;
                        }

                        if (worryLevel % m.DivisibleBy == 0)
                        {
                            // true, transfer to true
                            monkeys[m.IfTrue].Items.Add(worryLevel % cutOff);
                        }
                        else
                        {
                            // false, transfer to false
                            monkeys[m.IfFalse].Items.Add(worryLevel % cutOff);
                        }
                        // remove from monkey
                        m.TotalInspected += 1;
                        m.Items.Remove(item);
                    }
                }
                round++;
            }

            // top 2 monkeys
            var mkeys = monkeys.OrderByDescending(x => x.TotalInspected).Take(2).ToList();
            var finalNumbers = new BigInteger(mkeys[0].TotalInspected) * new BigInteger(mkeys[1].TotalInspected);
            Console.WriteLine("PART TWO");
            Console.WriteLine("Total is " + finalNumbers);
        }
    }
}
