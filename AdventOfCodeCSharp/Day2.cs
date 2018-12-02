using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeCSharp
{
    public class Day2
    {
        public static void ExerciseOne()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\kizer\source\repos\AdventOfCodeCSharp\AdventOfCodeCSharp\Day2.txt");
            int doubles = 0;
            int tripples = 0;
            foreach (string line in lines)
            {
                Dictionary<char, int> letters = new Dictionary<char, int>();
                foreach (char letter in line)
                {
                    if (letters.ContainsKey(letter))
                    {
                        letters[letter]++;
                    }
                    else
                    {
                        letters.Add(letter, 1);
                    }
                }

                bool hasDouble = false;
                bool hasTripple = false;
                foreach (int value in letters.Values)
                {
                    if (value == 2 && !hasDouble)
                    {
                        doubles++;
                        hasDouble = true;
                    } else if (value == 3 && !hasTripple)
                    {
                        tripples++;
                        hasTripple = true;
                    }

                    if (hasDouble && hasTripple)
                    {
                        break;
                    }
                }
            }
            Console.Out.Write(doubles * tripples);
            Console.ReadKey();
        }

        public static void ExerciseTwo()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\kizer\source\repos\AdventOfCodeCSharp\AdventOfCodeCSharp\Day2.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = i + 1; j < lines.Length - 1; j++)
                {
                    int differentChars = 0;
                    int position = 0;

                    for (int k = 0; k < lines[i].Length; k++)
                    {
                        if (lines[i][k] != lines[j][k])
                        {
                            differentChars++;
                            position = k;
                        }
                    }

                    if (differentChars == 1)
                    {
                        string charToRemove = Convert.ToString(lines[i][position]);
                        Console.Out.WriteLine($"{lines[i]}");
                        Console.Out.WriteLine($"{lines[j]}");
                        Console.Out.WriteLine(position);
                        Console.Out.WriteLine($"{lines[i].Replace(charToRemove, string.Empty)}");
                        Console.ReadKey();
                    }
                }
            }
        }
    }
}
