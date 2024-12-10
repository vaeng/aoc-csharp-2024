using System;
using System.Collections.Generic;
using System.Diagnostics;

internal class Program
{
  private static void Main(string[] args)
  {
    if (args.Length == 0)
    {
      Console.WriteLine("Please provide a parameter (1 or 2).");
      return;
    }

    // Parse the parameter
    if (int.TryParse(args[0], out int parameter))
    {
      string input = "";

      try
      {
        input = File.ReadAllText("input");

        if (parameter == 1)
        {
          var watch = System.Diagnostics.Stopwatch.StartNew();
          Console.WriteLine($"Solution for part 1:\n{Part1.Solve(input)}");
          watch.Stop();
          var elapsedMs = watch.ElapsedMilliseconds;
          Console.WriteLine("Execution time in ms: " + elapsedMs);
        }
        else if (parameter == 2)
        {
          var watch = System.Diagnostics.Stopwatch.StartNew();
          Console.WriteLine($"Solution for part 2:\n{Part2.Solve(input)}");
          watch.Stop();
          var elapsedMs = watch.ElapsedMilliseconds;
          Console.WriteLine("Execution time in ms: " + elapsedMs);
        }
        else if (parameter == 0)
        {
          Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
          Trace.AutoFlush = true;
          Trace.Indent();
          Console.WriteLine("TestMode:");
          Part2.testCode = 8;
          Trace.WriteLine("1" == Part2.Solve("3,9,8,9,10,9,4,9,99,-1,8"), "position mode equal");
          Part2.testCode = 7;
          Trace.WriteLine("0" == Part2.Solve("3,9,8,9,10,9,4,9,99,-1,8"), "position mode not equal");
          Part2.testCode = 18;
          Trace.WriteLine("0" == Part2.Solve("3,9,7,9,10,9,4,9,99,-1,8"), "position mode less than");
          Part2.testCode = 7;
          Trace.WriteLine("1" == Part2.Solve("3,9,7,9,10,9,4,9,99,-1,8"), "position mode not less than");
          Part2.testCode = 8;
          Trace.WriteLine("1" == Part2.Solve("3,3,1108,-1,8,3,4,3,99"), "immediate mode equal");
          Part2.testCode = 7;
          Trace.WriteLine("0" == Part2.Solve("3,3,1108,-1,8,3,4,3,99"), "immediate mode not equal");
          Part2.testCode = 8;
          Trace.WriteLine("0" == Part2.Solve("3,3,1107,-1,8,3,4,3,99"), "immediate mode less than");
          Part2.testCode = 7;
          Trace.WriteLine("1" == Part2.Solve("3,3,1107,-1,8,3,4,3,99"), "immediate mode not less than");
          Part2.testCode = 0;
          Trace.WriteLine("0" == Part2.Solve("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9"), "position jump code zero");
          Part2.testCode = 7;
          Trace.WriteLine("1" == Part2.Solve("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9"), "position jump code non zero");
          Part2.testCode = 0;
          Trace.WriteLine("0" == Part2.Solve("3,3,1105,-1,9,1101,0,0,12,4,12,99,1"), "immediate jump code zero");
          Part2.testCode = 7;
          Trace.WriteLine("1" == Part2.Solve("3,3,1105,-1,9,1101,0,0,12,4,12,99,1"), "immediate jump code non zero");
          Part2.testCode = 7;
          Trace.WriteLine("999" == Part2.Solve("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99"), "big example less than 8");
          Part2.testCode = 8;
          Trace.WriteLine("1000" == Part2.Solve("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99"), "big example equal 8");
          Part2.testCode = 9;
          Trace.WriteLine("1001" == Part2.Solve("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99"), "big example more than 8");
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


// the code that you want to measure comes here

