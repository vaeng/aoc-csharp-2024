using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

public static class Part1
{

  public struct Robot
  {
    public Robot(Complex p, Complex v)
    {
      P_0 = p;
      V = v;
    }
    public Complex P_0 { get; init; }
    public Complex V { get; init; }

    public Complex Position(long seconds, long xLimit, long yLimit)
    {
      Complex rawPos = P_0 + seconds * V;
      double wrappedX = WrapToOtherSide(rawPos.Real, xLimit);
      double wrappedY = WrapToOtherSide(rawPos.Imaginary, yLimit);
      return new Complex(wrappedX, wrappedY);
    }
    public override string ToString() => $"P_0: {P_0}, V: {V}";
  }

  public static double WrapToOtherSide(double raw, long limit)
  {
    return (raw % limit + limit) % limit;
  }

  public static List<Robot> robots = new();

  public static void Parse(List<String> input)
  {
    foreach (string line in input)
    {
      string[] temp = line[2..].Split(new string[] { " v=" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
      long[] p = temp[0].Split(',').Select(s => Convert.ToInt64(s)).ToArray();
      long[] v = temp[1].Split(',').Select(s => Convert.ToInt64(s)).ToArray();
      robots.Add(new Robot(new Complex(p[0], p[1]), new Complex(v[0], v[1])));
    }
  }

  public static int QuadrantNumber(Complex pos, long xLimit, long yLimit)
  {
    if (pos.Real == xLimit / 2 || pos.Imaginary == yLimit / 2)
    {
      return 0;
    }
    int quadrant = (pos.Real < xLimit / 2) ? 1 : 2;
    quadrant += (pos.Imaginary < yLimit / 2) ? 0 : 2;

    return quadrant;
  }

  public static long CalculateResultAndPrintMap(long seconds, long xLimit, long yLimit)
  {
    int[] quads = { 0, 0, 0, 0, 0 };
    Dictionary<Complex, int> map = new();

    foreach (var robot in robots)
    {
      Complex pos = robot.Position(seconds, xLimit, yLimit);
      quads[QuadrantNumber(pos, xLimit, yLimit)]++;
      if (map.ContainsKey(pos))
      {
        map[pos]++;
      }
      else
      {
        map[pos] = 1;
      }
    }

    for (int j = 0; j < yLimit; ++j)
    {
      for (int i = 0; i < xLimit; ++i)
      {
        Complex pos = new Complex(i, j);
        if (map.ContainsKey(pos))
        {
          Console.Write('■');
          //Console.Write(map[pos]);
        }
        else
        {
          Console.Write(' ');
        }
      }
      Console.WriteLine();
    }

    long result = 1;
    foreach (var quad in quads[1..])
    {
      result *= quad;
    }
    return result;
  }

  public static string Solve(List<String> input)
  {
    Parse(input);


    long xLimit = 101; //11;
    long yLimit = 103; // 7;
    int secondsMin = 100;
    int secondsMax = 100;

    long result = 0;
        for (int i = secondsMin; i <= secondsMax; i++)
        {
            // Clear the terminal screen entirely
            Console.Clear();

            // Print the header
            Console.WriteLine($"\n\nCurrent state after {i} seconds:");

            // Calculate and print the result
            result = CalculateResultAndPrintMap(i, xLimit, yLimit);

            // Wait for 1 second before the next iteration

            // easter image after 89
            // strip after 11
            Thread.Sleep(10);
        }

    return result.ToString();
  }
}
