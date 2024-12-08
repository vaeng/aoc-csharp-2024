using System;
using System.Collections.Generic;
using System.Numerics;


public static class Part1 {
  private enum Direction {
    North,
    South,
    West,
    East,
  }

  private static readonly Dictionary<char, List<Complex>> antennas = new();
  private static readonly HashSet<Complex> antinodes = new();
  private static int rows;
  private static int cols;
  private static List<String> grid = new();


  public static void Parse(List<String> input) {
    rows = input.Count();
    cols = input[0].Length;
    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        char c = input[i][j];
        if (c == '.') {
          continue;
        }
        if (!antennas.ContainsKey(c)) {
          antennas[c] = new List<Complex>();
        }
        antennas[c].Add(new Complex(i, j));
      }
    }
  }

  public static bool InBounds(Complex position) {
    return position.Real >= 0
        && position.Imaginary >= 0
        && position.Real < rows
        && position.Imaginary < cols;
  }

  public static void CalculateAntiNodes(Complex antenna, List<Complex> positions) {
    // for all the combinations of two positions,
    // calculate antinodes and add them to the set
    for (int i = 0; i < positions.Count - 1; ++i) {
      for (int j = i + 1; j < positions.Count; ++j) {
        var pos1 = 2 * positions[i] - positions[j];
        var pos2 = 2 * positions[j] - positions[i];
        if (InBounds(pos1)) {
          antinodes.Add(pos1);
        }
        if (InBounds(pos2)) {
          antinodes.Add(pos2);
        }
      }
    }
  }

  public static void PrintMap() {
    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        if (antinodes.Contains(new Complex(i, j))) {
          Console.Write("#");
        } else {
          Console.Write(grid[i][j]);
        }
      }
      Console.WriteLine();
    }
  }


  public static string Solve(List<String> input) {
    Parse(input);
    grid = input;


    foreach (var (antenna, positions) in antennas) {
      CalculateAntiNodes(antenna, positions);
    }

    // PrintMap();

    return antinodes.Count.ToString();
  }
}
