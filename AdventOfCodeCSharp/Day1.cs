using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeCSharp
{
    public class Day1
    {
        public static void ExerciseOne()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\kizer\source\repos\AdventOfCodeCSharp\AdventOfCodeCSharp\Day1.txt");
            int start = 0;
            foreach (string instruction in lines)
            {
                char direction = instruction[0];
                if (direction == '+')
                {
                    start += Convert.ToInt32(instruction.Substring(1, instruction.Length - 1));
                }
                else
                {
                    start -= Convert.ToInt32(instruction.Substring(1, instruction.Length - 1));
                }
            }
            Console.Out.Write(start);
            Console.ReadKey();
        }

        public static void ExerciseTwo()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\kizer\source\repos\AdventOfCodeCSharp\AdventOfCodeCSharp\Day1.txt");
            int start = 0;
            HashSet<int> frequencies = new HashSet<int>();
            while (true)
            {
                foreach (string instruction in lines)
                {
                    char direction = instruction[0];
                    if (direction == '+')
                    {
                        start += Convert.ToInt32(instruction.Substring(1, instruction.Length - 1));
                    }
                    else
                    {
                        start -= Convert.ToInt32(instruction.Substring(1, instruction.Length - 1));
                    }

                    if (!frequencies.Contains(start))
                    {
                        frequencies.Add(start);
                    }
                    else
                    {
                        Console.Out.Write(start);
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
