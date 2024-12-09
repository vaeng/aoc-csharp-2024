using System;
using System.Collections.Generic;
using System.Numerics;


public static class Part2 {
  public static List<(long, int)> unFormattedData = new();
  private static ulong result = 0;

  public static void Parse(List<String> input) {
    long index = 0;
    foreach (string line in input) {
      bool isEmpty = false;
      foreach (char c in line) {
        if (isEmpty) {
          unFormattedData.Add((-1, c - '0'));
        } else {
          unFormattedData.Add((index, c - '0'));
          index++;
        }
        isEmpty = !isEmpty;
      }
    }
  }

  public static void Print(List<(long, int)> data) {
    foreach (var (index, amount) in data) {
      for (var i = 0; i < amount; i++) {
        if (index == -1) {
          Console.Write(".");
        } else {
          Console.Write(index);
        }
      }
    }
    Console.WriteLine();
  }

  public static int GetFittingSpace(int size, int maxPosition) {
    int pos = 0;
    foreach (var (index, amount) in unFormattedData) {
      if (index == -1 && amount >= size) {
        break;
      }
      pos++;
      if (pos == maxPosition) {
        return -1;
      }
    }
    return pos;
  }

  public static void Process() {
    int backIndex = unFormattedData.Count - 1;
    while (backIndex > 0) {
      var (index, amount) = unFormattedData[backIndex];
      if (index != -1) {
        int insertPos = GetFittingSpace(amount, backIndex);
        if (insertPos != -1) {
          var (_, emptySpace) = unFormattedData[insertPos];
          unFormattedData[backIndex] = (-1, amount);
          unFormattedData[insertPos] = (-1, emptySpace - amount);
          unFormattedData.Insert(insertPos, (index, amount));
          continue;
        }
      }
      backIndex--;
    }
  }

  private static void GenerateChecksum() {
    long index = 0;
    foreach (var (id, amount) in unFormattedData) {
      if (id == -1) {
        index += amount;
      } else {
        for (int i = 0; i < amount; i++) {
          result += (ulong) index * (ulong) id;
          index++;
        }
      }
    }
  }

  public static string Solve(List<String> input) {
    Parse(input);
    Process();
    GenerateChecksum();
    return result.ToString();
  }
}
