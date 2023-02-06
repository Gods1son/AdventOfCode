using System;
using System.IO;

namespace Day2
{
    class Program
    {
        public const string ROCK = "ROCK";
        public const string PAPER = "PAPER";
        public const string SCISSORS = "SCISSORS";

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
            /*
             * GAME RULE
                First Column for opponent
                A = Rock
                B = Paper
                C = Scissors

                Second Column for me
                X = Rock  -- 1 point
                Y = Paper -- 2 points
                Z = Scissors -- 3 points

                0 point for losing
                3 points for a draw
                6 points for winning

                Rock beats Scissors
                Scissors beats Paper
                Paper beats Rock
             */

            string fileName = "input.txt";
            string line;
            int totalScore = 0;
            string dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = Path.Combine(dir, fileName);
            using (var sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string opp = line.Split(" ")[0];
                    string me = line.Split(" ")[1];
                    string oppChoice = CheckOppChoice(opp);
                    string myChoice = CheckMyChoice(me);

                    //for part two
                    string overrideChoice = OverrideMyChoice(oppChoice, me);
                    myChoice = overrideChoice;
                    //for part two

                    int gameScore = CheckGameScore(oppChoice, myChoice);
                    int myChoiceScore = CheckMyChoiceScore(myChoice);
                    totalScore += (gameScore + myChoiceScore);
                }
            }

            Console.WriteLine("My total score is " + totalScore);
        }

        public static string CheckOppChoice(string input)
        {
            string choice = "";
            if(input == "A")
            {
                choice = ROCK;
            }else if(input == "B")
            {
                choice = PAPER;
            }
            else if (input == "C")
            {
                choice = SCISSORS;
            }
            return choice;
        }

        public static string CheckMyChoice(string input)
        {
            string choice = "";
            if (input == "X")
            {
                choice = ROCK;
            }
            else if (input == "Y")
            {
                choice = PAPER;
            }
            else if (input == "Z")
            {
                choice = SCISSORS;
            }
            return choice;
        }

        public static int CheckGameScore(string opp, string me)
        {
            int score = 0;

            //it's a draw if we choose the same thing
            if (opp == me)
            {
                score = 3;
            }
            // I win if
            else if ((me == ROCK && opp == SCISSORS) || (me == SCISSORS && opp == PAPER) || (me == PAPER && opp == ROCK))
            {
                score = 6;
            }
            return score;
        }

        public static int CheckMyChoiceScore(string me)
        {
            // score I get based on my choice
            int myChoiceScore = 0;
            if (me == ROCK)
            {
                myChoiceScore = 1;
            }
            else if (me == PAPER)
            {
                myChoiceScore = 2;
            }
            else if (me == SCISSORS)
            {
                myChoiceScore = 3;
            }

            return myChoiceScore;
        }

        public static string OverrideMyChoice(string opp, string outcome)
        {
            string finalChoice = "";
            // X = lose, Y = draw, Z = win

            // to draw, choose same thing as opponent
            if(outcome == "Y")
            {
                finalChoice = opp;
            }
            else if (outcome == "X") // to lose
            {
                if(opp == ROCK)
                {
                    finalChoice = SCISSORS;
                } 
                else if(opp == PAPER)
                {
                    finalChoice = ROCK;
                }
                else if(opp == SCISSORS)
                {
                    finalChoice = PAPER;
                }
            }
            else if (outcome == "Z") // to win
            {
                if (opp == ROCK)
                {
                    finalChoice = PAPER;
                }
                else if (opp == PAPER)
                {
                    finalChoice = SCISSORS;
                }
                else if (opp == SCISSORS)
                {
                    finalChoice = ROCK;
                }
            }

            return finalChoice;
        }
    }
}
