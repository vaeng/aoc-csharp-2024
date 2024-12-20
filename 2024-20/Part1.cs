using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part1 {

  public static int rows = 0;
  public static int cols = 0;

  public static readonly Complex Up = Complex.ImaginaryOne;
  public static readonly Complex Down = -Complex.ImaginaryOne;
  public static readonly Complex Left = -Complex.One;
  public static readonly Complex Right = Complex.One;

  public static HashSet<Complex> obstacles = new();

  public static Complex start = new Complex(0, 0);
  public static Complex end = new Complex(0, 0);

  public static long benchmark = 0;

  public static long hundredPlusCheats = 0;

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

  public static long CalculateSteps() {
    PriorityQueue<(Complex, Complex), long> q = new();
    Dictionary<(Complex, Complex), long> minCost = new();

    q.Enqueue((start, Right), 0);
    minCost[(start, Right)] = 0;

    while (q.Count > 0) {
      var (currentPos, currentDir) = q.Dequeue();
      long currentCost = minCost[(currentPos, currentDir)];
      if (currentPos == end) {
        return currentCost;
      }
      foreach (var nextDir in new Complex[] { Up, Down, Left, Right }) {
        Complex nextPos = currentPos + nextDir;
        if (InBounds(nextPos) && !obstacles.Contains(nextPos)) {
          long nextCost = currentCost + (currentDir == nextDir ? 1 : 1);
          if (!minCost.ContainsKey((nextPos, nextDir)) || minCost[(nextPos, nextDir)] > nextCost) {
            minCost[(nextPos, nextDir)] = nextCost;
            q.Enqueue((nextPos, nextDir), nextCost);
          }
        }
      }
    }
    return -1;
  }

  public static string Solve(List<String> input) {
    Parse(input);
    //PrintMap();
    benchmark = CalculateSteps();
    var allObstacles = obstacles.ToHashSet();

    foreach (var obstacle in allObstacles) {
      obstacles.Remove(obstacle);
      long cheatedRun = CalculateSteps();
      obstacles.Add(obstacle);
      if (benchmark - cheatedRun >= 100) {
        hundredPlusCheats++;
      }
    }



    return hundredPlusCheats.ToString();
  }

}
