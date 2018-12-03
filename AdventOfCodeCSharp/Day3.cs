using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeCSharp
{
    public class Day3
    {
        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\kizer\source\repos\AdventOfCodeCSharp\AdventOfCodeCSharp\Day3.txt");

        public static void ExerciseOne()
        {
            string[,] fabric = new string[1000,1000];
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    fabric[i,j] = ".";
                }
            }

            PrintFabric(fabric);
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
