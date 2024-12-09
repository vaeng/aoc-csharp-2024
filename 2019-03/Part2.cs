using System;
using System.Collections.Generic;
using System.Numerics;



public static class Part2 {
  public static HashSet<Complex> firstWirePositions = new();
  public static HashSet<Complex> secondWirePositions = new();

  public static Dictionary<Complex, int> firstSteps = new();
  public static Dictionary<Complex, int> secondSteps = new();


  public static HashSet<Complex> GeneratePositions(string line, Dictionary<Complex, int> steps) {
    HashSet<Complex> positions = new();
    Complex pos = new Complex(0, 0);
    int step = 0;
    foreach (string command in line.Split(",", StringSplitOptions.RemoveEmptyEntries)) {
      char c = command[0];
      int distance = Convert.ToInt32(command[1..]);
      for (int i = 1; i <= distance; i++) {
        step++;
        pos += c switch {
          'R' => new Complex(1, 0),
          'L' => new Complex(-1, 0),
          'U' => new Complex(0, -1),
          'D' => new Complex(0, 1),
          _ => throw new Exception("")
        };
        positions.Add(pos);
        if (!steps.ContainsKey(pos)) {
          steps[pos] = step;
        }
      }

    }
    return positions;
  }

  public static string Solve(List<String> input) {
    long result = Int64.MaxValue;
    firstWirePositions = GeneratePositions(input[0], firstSteps);
    secondWirePositions = GeneratePositions(input[1], secondSteps);

    firstWirePositions.IntersectWith(secondWirePositions);

    foreach (Complex pos in firstWirePositions) {
      long total = firstSteps[pos] + secondSteps[pos];

      result = total < result ? total : result;
    }

    return result.ToString();
  }
}
