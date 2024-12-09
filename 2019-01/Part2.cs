using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part2 {
  public static long CalculateFuelRequirements(long mass) {
    long requiredFuel = 0;
    long delta = mass / 3 - 2;
    while (delta > 0) {
      requiredFuel += delta;
      delta = delta / 3 - 2;
    }
    return requiredFuel;
  }
  public static string Solve(List<String> input) {
    long requiredFuel = 0;

    foreach (var part in input) {
      long mass = Convert.ToInt64(part);
      requiredFuel += CalculateFuelRequirements(mass);
    }
    return requiredFuel.ToString();
  }
}
