using System;
using System.Collections.Generic;
using System.Numerics;


public static class Part2 {
  public static long CalculateResult(List<int> intCodes, int noun, int verb) {
    intCodes[1] = noun;
    intCodes[2] = verb;

    int index = 0;
    while (intCodes[index] != 99) {
      if (intCodes[index] == 1) {
        intCodes[intCodes[index + 3]] = intCodes[intCodes[index + 1]] + intCodes[intCodes[index + 2]];
      } else if (intCodes[index] == 2) {
        intCodes[intCodes[index + 3]] = intCodes[intCodes[index + 1]] * intCodes[intCodes[index + 2]];
      } else {
        // throw new InvalidOperationException($"Invalid operation: {intCodes[index]}");
        return -1;
      }
      index += 4;
    }

    return intCodes[0];
  }

  public static string Solve(List<String> input) {
    var intCodes = input[0]
                  .Split(",", StringSplitOptions.RemoveEmptyEntries)
                  .Select(s => Convert.ToInt32(s))
                  .ToList();

    long target = 19690720;

    long result = 0;
    for (int noun = 0; noun < 100; noun++) {
      for (int verb = 0; verb < 100; verb++) {
        if (CalculateResult(intCodes.ToList(), noun, verb) == target) {
          result = noun * 100 + verb;
          break;
        }
      }
    }
    return result.ToString();
  }
}
