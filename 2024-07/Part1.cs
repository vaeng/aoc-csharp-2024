using System;
using System.Collections.Generic;
using System.Numerics;


public static class Part1 {
  private static readonly List<ulong> results = new();
  private static readonly List<List<ulong>> operands = new();

  public static void Parse(List<String> input) {
    foreach (var line in input) {
      char[] charSeparators = new char[] { ' ', ':' };
      List<ulong> numbers = line.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToUInt64(s)).ToList();
      results.Add(numbers[0]);
      operands.Add(numbers[1..]);
    }
  }

  public static ulong RecursiveTest(ulong goal, ulong head, List<ulong> rest) {
    if (head > goal) return 0;
    if (rest.Count == 0) return head == goal ? goal : 0;
    return Math.Max(
      RecursiveTest(goal, head + rest[0], rest[1..]),
      RecursiveTest(goal, head * rest[0], rest[1..])
    );
  }

  public static string Solve(List<String> input) {
    Parse(input);


    ulong result = 0;

    for (int i = 0; i < results.Count; i++) {
      result += RecursiveTest(results[i], 0, operands[i]);
    }




    return result.ToString();
  }
}
