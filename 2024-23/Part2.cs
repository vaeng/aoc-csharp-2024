using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part2 {

  public static int rows = 0;
  public static int cols = 0;

  public static readonly Complex Up = Complex.ImaginaryOne;
  public static readonly Complex Down = -Complex.ImaginaryOne;
  public static readonly Complex Left = -Complex.One;
  public static readonly Complex Right = Complex.One;

  public static HashSet<Complex> obstacles = new();

  public static Complex start = new Complex(0, 0);
  public static Complex end = new Complex(0, 0);


  public static void Parse(List<String> input) {
    rows = input.Count;
    cols = input[0].Length;

    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        Complex pos = new Complex(j, i);
        if (input[i][j] == '#') {
          obstacles.Add(pos);
        } else if (input[i][j] == 'S') {
          start = pos;
        } else if (input[i][j] == 'E') {
          end = pos;
        }
      }
    }
  }

  public static bool InBounds(Complex position) {
    return position.Real >= 0
        && position.Imaginary >= 0
        && position.Real < cols
        && position.Imaginary < rows;
  }

  public static void PrintMap() {
    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        Complex pos = new Complex(j, i);
        char c = '.';
        if (obstacles.Contains(pos)) {
          c = '#';
        } else if (pos == start) {
          c = 'S';
        } else if (pos == end) {
          c = 'E';
        }
        Console.Write(c);
      }
      Console.WriteLine();
    }
  }

  public static Dictionary<Complex, long> CalculateSteps(Complex source, Complex target) {
    PriorityQueue<Complex, long> q = new();
    Dictionary<Complex, long> stepsDict = new();

    q.Enqueue(source, 0);
    stepsDict[source] = 0;

    while (q.Count > 0) {
      var currentPos = q.Dequeue();
      long currentCost = stepsDict[currentPos];
      foreach (var nextDir in new Complex[] { Up, Down, Left, Right }) {
        Complex nextPos = currentPos + nextDir;
        if (InBounds(nextPos) && !obstacles.Contains(nextPos)) {
          long nextCost = currentCost + 1;
          if (!stepsDict.ContainsKey(nextPos) || stepsDict[nextPos] > nextCost) {
            stepsDict[nextPos] = nextCost;
            q.Enqueue(nextPos, nextCost);
          }
        }
      }
    }
    return stepsDict;
  }

  public static string Solve(List<String> input) {
    Parse(input);
    //PrintMap();
    var stepsFromStart = CalculateSteps(start, end);
    var stepsFromEnd = CalculateSteps(end, start);

    long maxCheatingDistance = 20;

    long benchmark = stepsFromStart.Values.Max();

    Dictionary<long, List<(Complex, Complex)>> cheatedDistances = new();

    foreach (var (sourcePos, steps) in stepsFromStart) {
      for (long i = -maxCheatingDistance; i <= maxCheatingDistance; i++) {
        for (long j = -maxCheatingDistance; j <= maxCheatingDistance; j++) {
          long cheat = Math.Abs(i) + Math.Abs(j);
          if (cheat > maxCheatingDistance) {
            continue;
          }
          Complex targetPos = sourcePos + new Complex(i, j);
          if (stepsFromStart.ContainsKey(targetPos)) {
            long savedDistance = benchmark - (stepsFromStart[sourcePos] + stepsFromEnd[targetPos] + cheat);
            if (savedDistance < 1) {
              continue;
            }
            if (!cheatedDistances.ContainsKey(savedDistance)) {
              cheatedDistances[savedDistance] = new();
            }
            cheatedDistances[savedDistance].Add((sourcePos, targetPos));
          }
        }

      }
    }

    long result = 0;

    foreach (var (distance, cheatList) in cheatedDistances.OrderBy(x => x.Key)) {
      if (distance >= 100) {
        result += cheatList.Count;
      }
    }

    return result.ToString();
  }

}
