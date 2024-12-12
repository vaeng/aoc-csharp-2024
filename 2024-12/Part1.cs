using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part1 {

  public static int rows = 0;
  public static int cols = 0;

  public static Dictionary<Complex, char> map = new();
  public static HashSet<Complex> visited = new();

  public static readonly Complex Up = -Complex.One;
  public static readonly Complex Down = Complex.One;
  public static readonly Complex Left = -Complex.ImaginaryOne;
  public static readonly Complex Right = Complex.ImaginaryOne;


  public static void Parse(List<String> input) {
    rows = input.Count();
    cols = input[0].Length;
    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        if (input[i][j] == ' ') { continue; }
        map[new Complex(i, j)] = input[i][j];
      }
    }
  }

  public static bool InBounds(Complex position) {
    return position.Real >= 0
        && position.Imaginary >= 0
        && position.Real < rows
        && position.Imaginary < cols;
  }

  public static Queue<Complex> GetNeighborsUnbounded(Complex pos) {
    Queue<Complex> neighbors = new Queue<Complex>();
    foreach (Complex dir in new Complex[] { Up, Down, Left, Right }) {
      neighbors.Enqueue(pos + dir);
    }
    return neighbors;
  }

  public static string Solve(List<String> input) {
    Parse(input);
    long result = 0;

    foreach (var (pos, kind) in map) {
      long area = 1;
      long circumference = 0;
      if (!visited.Contains(pos)) {
        visited.Add(pos);
        Queue<Complex> neighbors = GetNeighborsUnbounded(pos);
        while (neighbors.Count > 0) {
          Complex neighbor = neighbors.Dequeue();
          // check in bounds and if same kind
          if (!InBounds(neighbor) || map[neighbor] != kind) {
            circumference++;
          } else {
            // same kind, in bounds
            if (!visited.Contains(neighbor)) {
              visited.Add(neighbor);
              area++;
              foreach (var newNeighbor in GetNeighborsUnbounded(neighbor))
                neighbors.Enqueue(newNeighbor);
            }
          }
        }
      }
      result += area * circumference;
    }


    return result.ToString();
  }
}
