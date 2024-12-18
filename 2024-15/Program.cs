using System;
using System.Collections.Generic;

internal class Program {
  private static void Main(string[] args) {
    if (args.Length == 0) {
      Console.WriteLine("Please provide a parameter (1 or 2).");
      return;
    }

    // Parse the parameter
    if (int.TryParse(args[0], out int parameter)) {
      string input = "";

      try {
        input = File.ReadAllText("input");

        if (parameter == 1) {
          var watch = System.Diagnostics.Stopwatch.StartNew();
          Console.WriteLine($"Solution for part 1:\n{Part1.Solve(input)}");
          watch.Stop();
          var elapsedMs = watch.ElapsedMilliseconds;
          Console.WriteLine("Execution time in ms: " + elapsedMs);
        } else if (parameter == 2) {
          var watch = System.Diagnostics.Stopwatch.StartNew();
          Console.WriteLine($"Solution for part 2:\n{Part2.Solve(input)}");
          watch.Stop();
          var elapsedMs = watch.ElapsedMilliseconds;
          Console.WriteLine("Execution time in ms: " + elapsedMs);
        } else {
          Console.WriteLine("Invalid input. Please provide a numeric parameter (1 or 2).");
        }
      } catch (Exception ex) { Console.WriteLine("An error occurred: " + ex.Message); }

    }
  }
}


// the code that you want to measure comes here

