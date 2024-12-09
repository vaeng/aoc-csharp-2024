using System;
using System.Collections.Generic;
using System.Numerics;


public static class Part1 {
  public static List<int> rawData = new();
  public static List<int> emptySections = new();
  public static List<int> formattedData = new();
  private static ulong result = 0;

  public static void Parse(List<String> input) {
    int index = 0;
    bool isData = true;
    foreach (string line in input) {
      foreach (char col in line) {
        int amount = col - '0';
        if (isData) {
          for (int i = 0; i < amount; i++) {
            rawData.Add(index);
          }
          index++;
        } else {
          emptySections.Add(col - '0');
        }
        isData = !isData;
      }
    }
  }

  private static void Process() {
    int last = rawData[0];
    bool fromFront = true;
    int frontIndex = 0;
    int backIndex = rawData.Count - 1;
    int emptySectionIndex = 0;
    for (int index = 0; index < rawData.Count; index++) {
      if (fromFront) {
        if (last != rawData[frontIndex]) {
          last = rawData[frontIndex];
          fromFront = false;
        }
      }

      if (!fromFront) {
        if (emptySections[emptySectionIndex] == 0) {
          fromFront = true;
          emptySectionIndex++;
        } else {
          emptySections[emptySectionIndex]--;
        }
      }

      if (fromFront) {
        result += (ulong) index * (ulong) rawData[frontIndex];
        frontIndex++;
      } else {
        result += (ulong) index * (ulong) rawData[backIndex];
        backIndex--;
      }
    }
    Console.WriteLine();
  }
  private static void PrintStart() {
    Console.WriteLine("Start Configuration:");
    int last = rawData[0];
    foreach (var sector in rawData) {
      if (sector != last) {
        for (int i = 0; i < emptySections[last]; i++) {
          Console.Write(".");
        }
        last = sector;
      }
      Console.Write(sector.ToString());
    }
  }
  public static void PrintResult() {
    Console.WriteLine("Formatted Configuration:");
    foreach (var sector in formattedData) {
      Console.WriteLine(sector.ToString());
    }
    Console.WriteLine();
  }
  public static string Solve(List<String> input) {
    Parse(input);
    Process();
    return result.ToString();
  }
}
