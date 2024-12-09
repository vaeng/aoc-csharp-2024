using System;
using System.Collections.Generic;
using System.Numerics;


public static class Part1 {
  public static string Solve(List<String> input) {
    ulong result = 0;

    foreach (var part in input) {
      ulong mass = Convert.ToUInt64(part);
      result += mass / 3 - 2;
    }
    return result.ToString();
  }
}
