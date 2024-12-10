using System;
using System.Collections.Generic;
using System.Numerics;


public static class Part1 {

  public static List<int> inputs = new List<int>();
  public static List<int> outputs = new List<int>();

  public static void ParseCommand(ref int pointer, ref List<int> intCodes) {
    int opcode = intCodes[pointer] % 100;
    // input
    if (opcode == 3) {
      intCodes[intCodes[pointer + 1]] = inputs[0];
      pointer += 2;
      return;
    }
    // output
    if (opcode == 4) {
      outputs.Add(intCodes[intCodes[pointer + 1]]);
      pointer += 2;
      return;
    }
    int mode1 = intCodes[pointer] / 100 % 10;
    int mode2 = intCodes[pointer] / 1000 % 10;
    int parameter1 = mode1 == 0 ? intCodes[intCodes[pointer + 1]] : intCodes[pointer + 1];
    int parameter2 = mode2 == 0 ? intCodes[intCodes[pointer + 2]] : intCodes[pointer + 2];

    intCodes[intCodes[pointer + 3]] = opcode switch {
      1 => parameter1 + parameter2,
      2 => parameter1 * parameter2,
      _ => throw new InvalidOperationException($"Invalid operation: {intCodes[pointer]}")
    };
    pointer += 4;
  }

  public static void PrintStack(List<int> intCodes) {
    foreach (var intCode in intCodes) {
      Console.Write($"{intCode},");
    }
    Console.WriteLine();
  }

  public static string Solve(string input) {

    var intCodes = input
                  .Split(",", StringSplitOptions.RemoveEmptyEntries)
                  .Select(s => Convert.ToInt32(s))
                  .ToList();

    int pointer = 0;
    inputs.Add(1);
    while (intCodes[pointer] != 99) {
      ParseCommand(ref pointer, ref intCodes);
    }
    foreach (var output in outputs) {
      Console.Write($"{output}, ");
    }
    Console.WriteLine();
    long result = outputs[^1];
    return result.ToString();
  }
}
