using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part2 {

  public static int rows = 0;
  public static int cols = 0;

  public static Dictionary<Complex, char> map = new();
  public static HashSet<Complex> visited = new();

  public static readonly Complex Right = Complex.One;
  public static readonly Complex Left = -Complex.One;
  public static readonly Complex Down = -Complex.ImaginaryOne;
  public static readonly Complex Up = Complex.ImaginaryOne;


  public static void Parse(List<String> input) {
    rows = input.Count();
    cols = input[0].Length;
    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        if (input[i][j] == ' ') { continue; }
        map[new Complex(j, rows - i - 1)] = input[i][j];
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


  public static long CountEdges(HashSet<Complex> group) {
    long edges = 0;
    List<Complex> leftEdges = new();
    List<Complex> upEdges = new();
    List<Complex> downEdges = new();
    List<Complex> rightEdges = new();
    // fill edge lists
    foreach (Complex pos in group) {
      if (!InBounds(pos + Left) || map[pos + Left] != map[pos]) {
        leftEdges.Add(pos);
      }
      if (!InBounds(pos + Right) || map[pos + Right] != map[pos]) {
        rightEdges.Add(pos);
      }
      if (!InBounds(pos + Up) || map[pos + Up] != map[pos]) {
        upEdges.Add(pos);
      }
      if (!InBounds(pos + Down) || map[pos + Down] != map[pos]) {
        downEdges.Add(pos);
      }
    }
    // check vertical edges
    foreach (var edge in leftEdges) {
      if (!leftEdges.Contains(edge + Up)) {
        edges++;
      }
    }
    foreach (var edge in rightEdges) {
      if (!rightEdges.Contains(edge + Up)) {
        edges++;
      }
    }
    // check horizontal edges
    foreach (var edge in upEdges) {
      if (!upEdges.Contains(edge + Left)) {
        edges++;
      }
    }
    foreach (var edge in downEdges) {
      if (!downEdges.Contains(edge + Left)) {
        edges++;
      }
    }


    return edges;
  }

  public static string Solve(List<String> input) {
    Parse(input);
    long result = 0;

    foreach (var (pos, kind) in map) {
      if (visited.Contains(pos)) {
        continue;
      }

      HashSet<Complex> localGroup = new();
      localGroup.Add(pos);
      visited.Add(pos);
      Queue<Complex> neighbors = GetNeighborsUnbounded(pos);
      while (neighbors.Count > 0) {
        Complex neighbor = neighbors.Dequeue();
        if (InBounds(neighbor) && map[neighbor] == kind) {
          // same kind, in bounds
          if (!visited.Contains(neighbor)) {
            visited.Add(neighbor);
            localGroup.Add(neighbor);
            foreach (var newNeighbor in GetNeighborsUnbounded(neighbor)) {
              neighbors.Enqueue(newNeighbor);
            }
          }
        }

      }
      long edges = CountEdges(localGroup);
      result += localGroup.Count * edges;
    }


    return result.ToString();
  }
}
