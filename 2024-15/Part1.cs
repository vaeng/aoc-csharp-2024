using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part1
{

  public static int rows = 0;
  public static int cols = 0;

  public static HashSet<Complex> boxes = new();
  public static HashSet<Complex> walls = new();
  public static Complex robot = new();

  public static List<Complex> movements = new();
  public static readonly Complex Up = Complex.ImaginaryOne;
  public static readonly Complex Down = -Complex.ImaginaryOne;
  public static readonly Complex Left = -Complex.One;
  public static readonly Complex Right = Complex.One;



  public static void Parse(String input)
  {

    string[] parts = input.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

    string[] mapData = parts[0].Split('\n', StringSplitOptions.RemoveEmptyEntries);
    rows = mapData.Length;
    cols = mapData[0].Length;

    for (int i = 0; i < rows; i++)
    {
      for (int j = 0; j < cols; j++)
      {
        if (mapData[i][j] == '.') { continue; }
        else if (mapData[i][j] == '@') { robot = new Complex(j, rows - i - 1); }
        else if (mapData[i][j] == '#') { walls.Add(new Complex(j, rows - i - 1)); }
        else if (mapData[i][j] == 'O') { boxes.Add(new Complex(j, rows - i - 1)); }
      }
    }
    foreach (string line in parts[1..])
    {
      foreach (char c in line)
      {
        if (c == '^') { movements.Add(Up); }
        else if (c == '<') { movements.Add(Left); }
        else if (c == '>') { movements.Add(Right); }
        else if (c == 'v') { movements.Add(Down); }
      }
    }
  }



  public static bool InBounds(Complex position)
  {
    return position.Real >= 0
        && position.Imaginary >= 0
        && position.Real < cols
        && position.Imaginary < rows;
  }

  public static bool CanMove(Complex entity, Complex movement)
  {
    if (walls.Contains(entity + movement))
    {
      return false;
    }
    if (boxes.Contains(entity + movement))
    {
      return CanMove(entity + movement, movement);
    }
    return true;
  }

  public static void Move(Complex movement)
  {
    robot += movement;
    Complex firstBox = robot;
    if (!boxes.Contains(firstBox))
    {
      return;
    }
    Complex firstFree = firstBox;
    while (boxes.Contains(firstFree))
    {
      firstFree = firstFree + movement;
    }
    // swap the first block, now occupied by robot
    boxes.Remove(firstBox);
    boxes.Add(firstFree);
  }

  public static void PrintMap()
  {
    for (int i = rows - 1; i >= 0; i--)
    {
      for (int j = 0; j < cols; j++)
      {
        char c = '.';
        Complex pos = new Complex(j, i);
        if (robot == pos)
        {
          c = '@';
        }
        else if (walls.Contains(pos))
        {
          c = '#';
        }
        else if (boxes.Contains(pos))
        {
          c = 'O';
        }
        Console.Write(c);
      }
      Console.WriteLine();
    }
  }

  public static long CalculateGPS()
  {
    double sum = 0;
    foreach (var box in boxes)
    {
      sum += box.Real;
      sum += (rows - box.Imaginary - 1) * 100;
    }
    return (long)sum;
  }

  public static string Solve(String input)
  {
    Parse(input);
    long result = 0;

    foreach (var box in boxes)
    {
      if (!InBounds(box))
      {
        Console.WriteLine("box out of bounds: " + box.ToString());
      }
    }
    foreach (var movement in movements)
    {
      if (CanMove(robot, movement))
      {
        Move(movement);
      }
    }
    //PrintMap();

    result = CalculateGPS();


    return result.ToString();
  }
}
