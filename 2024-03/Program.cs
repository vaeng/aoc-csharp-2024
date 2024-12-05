using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a parameter (1 or 2).");
            return;
        }

        // Parse the parameter
        if (int.TryParse(args[0], out int parameter))
        {
            List<string> input = new();

            try
            {
                input = File.ReadLines("input").ToList();

                if (parameter == 1)
                {
                    Console.WriteLine($"Solution for part 1:\n{Part1.Solve(input)}");
                }
                else if (parameter == 2)
                {
                    Console.WriteLine($"Solution for part 2:\n{Part2.Solve(input)}");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please provide a numeric parameter (1 or 2).");
                }
            }
            catch (Exception ex) { Console.WriteLine("An error occurred: " + ex.Message); }

        }
    }
}
