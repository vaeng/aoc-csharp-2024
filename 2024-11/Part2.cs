using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;

public static class Part2 {

  public static List<long> stones = new();
  public static ConcurrentDictionary<(long, long), long> lookup = new();

  public static void Parse(List<String> input) {

    foreach (var line in input) {
      foreach (var stone in line.Split(' ')) {
        stones.Add(Convert.ToInt64(stone));
      }
    }
  }

  public static long Blink(int blinksleft, long stone) {
    if (lookup.ContainsKey((blinksleft, stone))) {
      return lookup[(blinksleft, stone)];
    }

    long lookup_entry = 0;
    if (blinksleft == 0) {
      lookup_entry = 1;
    } else {
      string stoneString = stone == 0 ? "" : stone.ToString();
      if (stone == 0) {
        lookup_entry = Blink(blinksleft - 1, 1);
      } else if (stoneString.Length % 2 == 0) {
        lookup_entry = Blink(blinksleft - 1, Convert.ToInt64(stoneString[..(stoneString.Length / 2)]))
        + Blink(blinksleft - 1, Convert.ToInt64(stoneString[(stoneString.Length / 2)..]));
      } else {

        lookup_entry = Blink(blinksleft - 1, stone * 2024);
      }
    }
    lookup.TryAdd((blinksleft, stone), lookup_entry);
    return lookup_entry;
  }


  public static string Solve(List<String> input) {
    Parse(input);
    long result = 0;

    int blinks = 75;

    foreach (var stone in stones) {
      result += Blink(blinks, stone);
    }


    return result.ToString();
  }
}
