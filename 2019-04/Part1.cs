using System;
using System.Collections.Generic;
using System.Numerics;



public static class Part1 {

  public static bool HasAdjacent(int i) {
    int i1 = i % 10;
    int i2 = i / 10 % 10;
    int i3 = i / 100 % 10;
    int i4 = i / 1000 % 10;
    int i5 = i / 10000 % 10;
    int i6 = i / 100000;
    return (i1 == i2
            || i2 == i3
            || i3 == i4
            || i4 == i5
            || i5 == i6);
  }

  public static bool NeverDecreases(int i) {
    int i1 = i % 10;
    int i2 = i / 10 % 10;
    int i3 = i / 100 % 10;
    int i4 = i / 1000 % 10;
    int i5 = i / 10000 % 10;
    int i6 = i / 100000;
    return (i1 >= i2
            && i2 >= i3
            && i3 >= i4
            && i4 >= i5
            && i5 >= i6);
  }

  public static string Solve(List<String> input) {
    int[] limits = input[0].Split('-').Select(x => Convert.ToInt32(x)).ToArray();

    int results = 0;

    for (int i = limits[0]; i <= limits[1]; ++i) {
      if (HasAdjacent(i) && NeverDecreases(i)) {
        results++;
      }
    }

    return results.ToString();
  }
}