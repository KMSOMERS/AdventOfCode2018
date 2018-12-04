using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace AdventOfCodeCSharp
{
    public class Day3
    {
        private class Claim
        {
            public string ID { get; set; }
            public int StartX { get; set; }
            public int StartY { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public bool IsValid { get; set; } = true;

            public Claim(string line)
            {
                ParseClaim(line.Split(" "));
            }

            //Example format for the claim
            //#123 @ 3,2: 5x4
            //ID @ POSITION X,Y: WIDTH x HEIGHT
            private void ParseClaim(string[] sections)
            {
                ID = sections[0];

                string[] coords = sections[2].Split(",");
                StartX = Convert.ToInt32(coords[0]);
                StartY = Convert.ToInt32(coords[1].Replace(":", string.Empty));

                string[] xy = sections[3].Split("x");
                Width = Convert.ToInt32(xy[0]);
                Height = Convert.ToInt32(xy[1]);
            }
        }

        public static void ExerciseOne()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Projects\AdventOfCode\AdventOfCode2018\AdventOfCodeCSharp\Day3.txt");

            string[,] fabric = new string[1000,1000];
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    fabric[i,j] = ".";
                }
            }

            foreach (var line in lines)
            {
                Claim claim = new Claim(line);
                for (int i = 0; i < claim.Width; i++)
                {
                    for (int j = 0; j < claim.Height; j++)
                    {
                        if (fabric[claim.StartX + i, claim.StartY + j] == "X")
                        {
                            fabric[claim.StartX + i, claim.StartY + j] = "#";
                        }
                        else if (fabric[claim.StartX + i, claim.StartY + j] == ".")
                        {
                            fabric[claim.StartX + i, claim.StartY + j] = "X";
                        }
                    }
                }
            }

            int overlap = 0;

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (fabric[i, j] == "#")
                    {
                        overlap++;
                    }
                }
            }

            Console.Write(overlap);
            Console.ReadKey();
        }

        public static void ExerciseTwo()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Projects\AdventOfCode\AdventOfCode2018\AdventOfCodeCSharp\Day3.txt");
            Dictionary<string, Claim> claims = new Dictionary<string, Claim>();

            string[,] fabric = new string[1000, 1000];
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    fabric[i, j] = ".";
                }
            }

            foreach (var line in lines)
            {
                Claim claim = new Claim(line);
                for (int i = 0; i < claim.Width; i++)
                {
                    for (int j = 0; j < claim.Height; j++)
                    {
                        if (fabric[claim.StartX + i, claim.StartY + j] == ".")
                        {
                            fabric[claim.StartX + i, claim.StartY + j] = claim.ID;
                        }
                        else
                        {
                            claims[fabric[claim.StartX + i, claim.StartY + j]].IsValid = false;
                            claim.IsValid = false;
                        }
                    }
                }
                claims.Add(claim.ID, claim);
            }

            foreach (string key in claims.Keys)
            {
                if (claims[key].IsValid)
                {
                    Console.WriteLine(claims[key].ID);
                }
            }

            Console.ReadKey();
        }

        public static void PrintFabric(string[,] fabric)
        {
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    Console.Out.Write(fabric[i, j]);
                }
                Console.Out.Write("\n");
            }
        }

    }
}
