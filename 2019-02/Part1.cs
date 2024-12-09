using System;
using System.Collections.Generic;
using System.Numerics;


public static class Part1 {

  public static void PrintStack(List<int> intCodes) {
    foreach (var intCode in intCodes) {
      Console.Write($"{intCode},");
    }
    Console.WriteLine();
  }
  public static string Solve(List<String> input) {

    var intCodes = input[0]
                  .Split(",", StringSplitOptions.RemoveEmptyEntries)
                  .Select(s => Convert.ToInt32(s))
                  .ToList();

    intCodes[1] = 12;
    intCodes[2] = 2;


    int index = 0;
    while (intCodes[index] != 99) {
      // PrintStack(intCodes);
      if (intCodes[index] == 1) {
        intCodes[intCodes[index + 3]] = intCodes[intCodes[index + 1]] + intCodes[intCodes[index + 2]];
      } else if (intCodes[index] == 2) {
        intCodes[intCodes[index + 3]] = intCodes[intCodes[index + 1]] * intCodes[intCodes[index + 2]];
      } else {
        throw new InvalidOperationException($"Invalid operation: {intCodes[index]}");
      }
      index += 4;
    }

    long result = intCodes[0];
    return result.ToString();
  }
}
