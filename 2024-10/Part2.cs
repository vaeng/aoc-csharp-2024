using System;
using System.Collections.Generic;
using System.Numerics;


public static class Part2 {

  public static int rows = 0;
  public static int cols = 0;

  public static Dictionary<Complex, int> map = new();

  public static readonly Complex Up = -Complex.One;
  public static readonly Complex Down = Complex.One;
  public static readonly Complex Left = -Complex.ImaginaryOne;
  public static readonly Complex Right = Complex.ImaginaryOne;


  public static void Parse(List<String> input) {
    rows = input.Count();
    cols = input[0].Length;
    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        if (input[i][j] == '.') { continue; }
        map[new Complex(i, j)] = (int) input[i][j] - '0';
      }
    }
  }

  public static bool InBounds(Complex position) {
    return position.Real >= 0
        && position.Imaginary >= 0
        && position.Real < rows
        && position.Imaginary < cols;
  }

  public static long CalculateTrailheadScore(Complex pos, ref Dictionary<Complex, int> map) {
    return
      CheckDirection(pos, pos + Up, ref map)
    + CheckDirection(pos, pos + Down, ref map)
    + CheckDirection(pos, pos + Right, ref map)
    + CheckDirection(pos, pos + Left, ref map);
  }

  public static long CheckDirection(Complex from, Complex to, ref Dictionary<Complex, int> map) {
    if (!map.ContainsKey(to)) { return 0; }

    if (map[to] == map[from] + 1) {
      if (map[to] == 9) {
        // Console.WriteLine(to);
        return 1;
      }
      return CalculateTrailheadScore(to, ref map);
    }
    return 0;
  }

  public static string Solve(List<String> input) {
    Parse(input);
    long result = 0;

    foreach (var (pos, height) in map) {
      if (height == 0) {

        result += CalculateTrailheadScore(pos, ref map);
      }
    }


    return result.ToString();
  }
}

