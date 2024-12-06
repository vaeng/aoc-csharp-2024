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

  private static readonly Dictionary<Complex, bool> obstacles = new();
  private static readonly Dictionary<Complex, bool> visited = new();
  private static Complex position;
  private static Direction direction = Direction.North;
  private static int rows;
  private static int cols;

  public static void Parse(List<String> input) {
    rows = input.Count();
    cols = input[0].Length;
    for (int i = 0; i < rows; i++) {
      for (int j = 0; j < cols; j++) {
        if (input[i][j] == '#') {
          obstacles[new Complex(i, j)] = true;
        } else if (input[i][j] == '^') {
          position = new Complex(i, j);
        }
      }
    }
  }

  public static bool InBounds() {
    return position.Real >= 0
        && position.Imaginary >= 0
        && position.Real < rows
        && position.Imaginary < cols;
  }

  public static bool IsBlocked(Complex pos) {
    return obstacles.ContainsKey(pos);
  }

  public static void Patrol() {
    visited[position] = true;
    if (direction == Direction.North) {
      if (!IsBlocked(position - Complex.One)) {
        position = position - Complex.One;
      } else {
        direction = Direction.East;
      }
    } else if (direction == Direction.East) {
      if (!IsBlocked(position + Complex.ImaginaryOne)) {
        position = position + Complex.ImaginaryOne;
      } else {
        direction = Direction.South;
      }
    } else if (direction == Direction.South) {
      if (!IsBlocked(position + Complex.One)) {
        position = position + Complex.One;
      } else {
        direction = Direction.West;
      }
    } else { // Direction.West
      if (!IsBlocked(position - Complex.ImaginaryOne)) {
        position = position - Complex.ImaginaryOne;
      } else {
        direction = Direction.North;
      }
    }

  }

  public static string Solve(List<String> input) {
    Parse(input);

    while (InBounds()) {
      Patrol();
    }
    int result = visited.Count;


    return result.ToString();
  }
}
