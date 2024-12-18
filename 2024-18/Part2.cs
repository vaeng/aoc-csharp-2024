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

  public static List<Complex> obstacles = new();



  public static void Parse(List<String> input) {
    foreach (var line in input) {
      // given in X, Y, X from the left, Y from the Top edge
      long[] raw = line.Split(',').Select(s => Convert.ToInt64(s)).ToArray();
      if (raw.Length == 2) {
        obstacles.Add(new Complex(raw[0], raw[1]));
      }
    }
  }

  public static bool IsBlocked(Complex pos, List<Complex> objects) {
    foreach (Complex obstacle in obstacles) {
      if (pos == obstacle) {
        return true;
      }
    }
    return false;
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
        char c = obstacles.Contains(new Complex(j, i)) ? '#' : '.';
        Console.Write(c);
      }
      Console.WriteLine();
    }
  }

  public static long CalculateSteps() {
    PriorityQueue<Complex, long> q = new();
    Dictionary<Complex, long> minSteps = new();

    q.Enqueue(new Complex(0, 0), 0);
    minSteps[new Complex(0, 0)] = 0;

    while (q.Count > 0) {
      Complex currentPos = q.Dequeue();
      long currentStep = minSteps[currentPos];
      if (currentPos.Real == cols - 1 && currentPos.Imaginary == rows - 1) {
        return currentStep;
      }
      foreach (var dir in new Complex[] { Up, Down, Left, Right }) {
        Complex nextPos = currentPos + dir;
        if (InBounds(nextPos) && !obstacles.Contains(nextPos)) {
          long nextSteps = currentStep + 1;
          if (!minSteps.ContainsKey(nextPos) || minSteps[nextPos] > nextSteps) {
            minSteps[nextPos] = nextSteps;
            q.Enqueue(nextPos, nextSteps);
          }
        }
      }

    }

    return -1;
  }

  public static string Solve(List<String> input) {
    Parse(input);
    Setup();
    var allObstacles = obstacles.ToList();

    for (int i = 0; i <= allObstacles.Count; i++) {
      obstacles = allObstacles.Take(i).ToList();
      if (CalculateSteps() == -1) {
        PrintMap();
        return obstacles[^1].ToString();
      }
    }
    long result = 0;
    return result.ToString();
  }

  public static void Setup() {
    rows = 71;
    cols = rows;
  }
}
