using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
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
            string data = File.ReadAllText(path);
            // split data at each line
            var dataSplit = data.Split(Environment.NewLine).ToList();
            // get height of field
            int height = dataSplit.Count;
            //get number of trees per row
            int width = dataSplit[0].Length;
            List<int[]> trees = new List<int[]>();
            int totalVisible = 0;
            for(var i = 0; i < height; i++)
            {
                var row = dataSplit[i].ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
                trees.Add(row);
            }
            for(var i = 1; i < trees.Count - 1; i++)
            {
                var row = trees[i];
                for (var t = 1; t < row.Length - 1; t++)
                {
                    var treeH = row[t];
                    bool isVisibleTop = true;
                    // check top
                    for (var m = 0; m < i; m++)
                    {
                        int compare = trees[m][t];
                        if (treeH <= compare)
                        {
                            isVisibleTop = false;
                            break;
                        }
                    }

                    // check right
                    bool isVisibleRight = true;
                    // check row level
                    for (var m = t + 1; m < row.Length; m++)
                    {
                        int compare = row[m];
                        if (treeH <= compare)
                        {
                            isVisibleRight = false;
                            break;
                        }
                    }

                    // check bottom 
                    bool isVisibleBottom = true;
                    for (var m = i + 1; m < height; m++)
                    {
                        int compare = trees[m][t];
                        if (treeH <= compare)
                        {
                            isVisibleBottom = false;
                            break;
                        }
                    }

                    // check left
                    bool isVisibleLeft = true;
                    for (var m = 0; m < t; m++)
                    {
                        int compare = row[m];
                        if (treeH <= compare)
                        {
                            isVisibleLeft = false;
                            break;
                        }
                    }


                    if (isVisibleTop || isVisibleRight || isVisibleBottom || isVisibleLeft)
                    {
                        totalVisible++;
                    }
                }
            }
            int downVisible = height * 2;
            int acrossVisible = (width - 2) * 2;
            totalVisible += downVisible + acrossVisible;
            Console.WriteLine("PART ONE");
            Console.WriteLine("Total Visible is " + totalVisible);
        }

        public static void RunProgramTwo()
        {
            string fileName = "input.txt";
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);
            string data = File.ReadAllText(path);
            // split data at each line
            var dataSplit = data.Split(Environment.NewLine).ToList();
            // get height of field
            int height = dataSplit.Count;
            //get number of trees per row
            int width = dataSplit[0].Length;
            List<int[]> trees = new List<int[]>();
            int highestSceneScore = 0;
            for (var i = 0; i < height; i++)
            {
                var row = dataSplit[i].ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
                trees.Add(row);
            }
            for (var i = 1; i < trees.Count - 1; i++)
            {
                var row = trees[i];
                for (var t = 1; t < row.Length - 1; t++)
                {
                    var treeH = row[t];
                    int currentSceneScore = 0;

                    int topScore = 0;
                    // check top
                    for (var m = i - 1; m >= 0; m--)
                    {
                        int compare = trees[m][t];
                        topScore++;
                        if (treeH <= compare)
                        {
                            break;
                        }
                    }

                    // check right
                    int rightScore = 0;
                    // check row level
                    for (var m = t + 1; m < row.Length; m++)
                    {
                        int compare = row[m];
                        rightScore++;
                        if (treeH <= compare)
                        {
                            break;
                        }
                    }

                    // check bottom 
                    int bottomScore = 0;
                    for (var m = i + 1; m < height; m++)
                    {
                        int compare = trees[m][t];
                        bottomScore++;
                        if (treeH <= compare)
                        {
                            break;
                        }
                    }

                    // check left
                    int leftScore = 0;
                    for (var m = t - 1; m >= 0; m--)
                    {
                        int compare = row[m];
                        leftScore++;
                        if (treeH <= compare)
                        {
                            break;
                        }
                    }

                    //get current scene score by formula
                    currentSceneScore = topScore * rightScore * bottomScore * leftScore;
                    // check highest scene score
                    highestSceneScore = currentSceneScore > highestSceneScore ? currentSceneScore : highestSceneScore;
                }
            }

            Console.WriteLine("PART TWO");
            Console.WriteLine("Highest Scenic Score is " + highestSceneScore);
        }
    }
}
