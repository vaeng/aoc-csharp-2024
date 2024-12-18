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

  public static long CalculateSteps() {
    PriorityQueue<(Complex, Complex), long> q = new();
    Dictionary<(Complex, Complex), long> minCost = new();
    Dictionary <(Complex, Complex), HashSet<(Complex, Complex)>> backTrace = new();

    q.Enqueue((start, Right), 0);
    minCost[(start, Right)] = 0;
    long bestCost = long.MaxValue;
    backTrace[(start, Right)] = new();

    while (q.Count > 0) {
      var (currentPos, currentDir) = q.Dequeue();
      long currentCost = minCost[(currentPos, currentDir)];

      if(currentPos == end) {
        bestCost = Math.Min(currentCost, bestCost);
      }

      foreach (var nextDir in new Complex[] { Up, Down, Left, Right }) {
        Complex nextPos = currentPos + nextDir;
        if (InBounds(nextPos) && !obstacles.Contains(nextPos)) {
          long nextCost = currentCost + (currentDir == nextDir ? 1 : 1001);
          if (!minCost.ContainsKey((nextPos, nextDir)) || minCost[(nextPos, nextDir)] >= nextCost) {
            if (!backTrace.ContainsKey((nextPos, nextDir)) || minCost[(nextPos, nextDir)] > nextCost) {
              backTrace[(nextPos, nextDir)] = new();
            }
            backTrace[(nextPos, nextDir)].Add((currentPos, currentDir));
          }
          if (!minCost.ContainsKey((nextPos, nextDir)) || minCost[(nextPos, nextDir)] > nextCost) {
            minCost[(nextPos, nextDir)] = nextCost;
            q.Enqueue((nextPos, nextDir), nextCost);
          }
        }
      }
    }
    Queue<(Complex, Complex)> bestSeats = new();
    //get initial best seats
    foreach (var dir in new Complex[] { Up, Down, Left, Right }) {
      if(minCost.ContainsKey((end, dir)) && minCost[(end, dir)] == bestCost) {
        bestSeats.Enqueue((end, dir));
      }
    }

    HashSet<Complex> seats = new();

    while(bestSeats.Count > 0) {
      var seat = bestSeats.Dequeue();
      seats.Add(seat.Item1);
      foreach(var otherSeat in backTrace[seat]) {
        bestSeats.Enqueue(otherSeat);
      }
    }


    return seats.Count;
  }

  public static string Solve(List<String> input) {
    Parse(input);
    long result = CalculateSteps();
    return result.ToString();
  }

}
