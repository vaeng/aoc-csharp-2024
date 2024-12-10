using System;
using System.Collections.Generic;
using System.Numerics;


public static class Part2
{

  public static List<int> inputs = new List<int>();
  public static List<int> outputs = new List<int>();

  public static int testCode = 5;

  public static void ParseCommand(ref int pointer, ref List<int> intCodes)
  {
    int opcode = intCodes[pointer] % 100;
    // input
    if (opcode == 3)
    {
      if (intCodes[pointer] != 3) { Console.WriteLine(intCodes[pointer]); }
      intCodes[intCodes[pointer + 1]] = inputs[0];
      pointer += 2;
      return;
    }
    // output
    int mode1 = intCodes[pointer] / 100 % 10;
    int parameter1 = mode1 == 0 ? intCodes[intCodes[pointer + 1]] : intCodes[pointer + 1];
    if (opcode == 4)
    {
      if (intCodes[pointer] != 4) { Console.WriteLine(intCodes[pointer]); }
      outputs.Add(parameter1);
      pointer += 2;
      return;
    }

    int mode2 = intCodes[pointer] / 1000 % 10;
    int parameter2 = mode2 == 0 ? intCodes[intCodes[pointer + 2]] : intCodes[pointer + 2];
    int mode3 = intCodes[pointer] / 10000 % 10;
    int parameter3 = mode3 == 0 ? intCodes[pointer + 3] : throw new InvalidOperationException($"Invalid operation: {intCodes[pointer]}");

    switch (opcode)
    {
      case 1:
        // Console.WriteLine($"1: {parameter1}, 2: {parameter2}, 3: {parameter3}");
        intCodes[parameter3] = parameter1 + parameter2;
        break;
      case 2:
        intCodes[parameter3] = parameter1 * parameter2;
        break;
      case 5: // jump-if-true
        if (parameter1 != 0)
        {
          pointer = parameter2;
        }
        else
        {
          pointer += 3;
        }
        return;
      case 6: // jump-if-false
        if (parameter1 == 0)
        {
          pointer = parameter2;
        }
        else
        {
          pointer += 3;
        }
        return;
      case 7: // less than
        intCodes[parameter3] = (parameter1 < parameter2) ? 1 : 0;
        break;
      case 8: // equals
        intCodes[parameter3] = (parameter1 == parameter2) ? 1 : 0;
        break;
      default:
        throw new InvalidOperationException($"Invalid operation: {intCodes[pointer]}");
    }
    pointer += 4;
  }

  public static void PrintStack(List<int> intCodes)
  {
    foreach (var intCode in intCodes)
    {
      Console.Write($"{intCode},");
    }
    Console.WriteLine();
  }

  public static string Solve(string input)
  {

    var intCodes = input
                  .Split(",", StringSplitOptions.RemoveEmptyEntries)
                  .Select(s => Convert.ToInt32(s))
                  .ToList();

    int pointer = 0;
    inputs.Add(testCode);
    while (intCodes[pointer] != 99)
    {
      ParseCommand(ref pointer, ref intCodes);
      //PrintStack(intCodes);
    }

    long result = outputs[^1];
    inputs.Clear();
    outputs.Clear();
    return result.ToString();
  }
}
