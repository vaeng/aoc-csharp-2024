using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;

public static class Part1 {

  public static List<List<int>> locks = new();
  public static List<List<int>> keys = new();

  public static int height = 0;

  public static void Parse(List<String> input) {
    bool processingLock = false;
    bool processingKey = false;
    int localHeight = 0;
    List<int> currentProcess = new();
    foreach (var line in input) {
      if (line.Length == 0) {
        if (processingLock) {
          locks.Add(currentProcess);
          processingLock = false;
        } else if (processingKey) {
          keys.Add(currentProcess);
          processingKey = false;
        }
        if (localHeight > 0) {
          height = localHeight;
        }
        currentProcess = new();
      } else if (processingKey || processingLock) {
        for (int i = 0; i < line.Length; i++) {
          if (line[i] == '#') {
            currentProcess[i] += 1;
          }
        }
        if (height == 0) {
          localHeight += 1;
        }
      } else {
        if (line.StartsWith("#")) {
          processingLock = true;
        } else if (line.StartsWith(".")) {
          processingKey = true;
        }
        for (int i = 0; i < line.Length; i++) {
          currentProcess.Add(0);
        }
      }
    }
    if (processingLock) {
      locks.Add(currentProcess);
    } else if (processingKey) {
      keys.Add(currentProcess);
    }
  }

  public static void PrintList(List<int> l) {
    foreach (var i in l) {
    }
  }


  public static string Solve(List<String> input) {
    Parse(input);
    long result = 0;

    foreach (var key in keys) {
      PrintList(key);
      foreach (var lock_ in locks) {
        PrintList(lock_);
        bool fits = true;
        foreach (var sum in key.Zip(lock_, (a, b) => (a + b))) {
          if (sum >= height + 1) {
            fits = false;
            break;
          }
        }
        if (fits) {
          result += 1;
        }
      }
    }

    return result.ToString();
  }

}
