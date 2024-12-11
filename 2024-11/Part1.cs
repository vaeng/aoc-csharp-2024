using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part1 {

  public static List<long> stones = new();

  public static void Parse(List<String> input) {

    foreach (var line in input) {
      foreach (var stone in line.Split(' ')) {
        stones.Add(Convert.ToInt64(stone));
      }
    }
  }

  public static long Blink(int blinksleft, long stone) {
    if (blinksleft == 0) {
      return 1;
    } else {
      if (stone == 0) {
        return Blink(blinksleft - 1, 1);
      }
      string stoneString = stone.ToString();
      if (stoneString.Length % 2 == 0) {
        return Blink(blinksleft - 1, Convert.ToInt64(stoneString[..(stoneString.Length / 2)]))
        + Blink(blinksleft - 1, Convert.ToInt64(stoneString[(stoneString.Length / 2)..]));
      }
      return Blink(blinksleft - 1, stone * 2024);
    }
  }


  public static string Solve(List<String> input) {
    Parse(input);
    long result = 0;

    int blinks = 25;

    foreach (var stone in stones) {
      result += Blink(blinks, stone);
    }


    return result.ToString();
  }
}
