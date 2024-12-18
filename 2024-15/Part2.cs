using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part2
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
        else if (mapData[i][j] == '@') { robot = new Complex(j * 2, rows - i - 1); }
        else if (mapData[i][j] == '#')
        {
          walls.Add(new Complex(j * 2, rows - i - 1));
          walls.Add(new Complex(j * 2 + 1, rows - i - 1));
        }
        else if (mapData[i][j] == 'O') { boxes.Add(new Complex(j * 2, rows - i - 1)); }
      }
    }
    cols *= 2;
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

  public static (bool, Complex) ContainsBox(Complex pos)
  {
    foreach (Complex box in boxes)
    {
      if (box == pos || box + Right == pos) { return (true, box); }
    }
    return (false, new Complex(0, 0));
  }

  public static HashSet<Complex> needsMovement = new();

  public static bool CalculatePossibleMovement(Complex movement)
  {
    if (walls.Contains(robot + movement))
    {
      return false;
    }
    var (occupied, box) = ContainsBox(robot + movement);
    if (occupied)
    {
      Queue<Complex> boxesToCheck = new();
      boxesToCheck.Enqueue(box);
      while (boxesToCheck.Count > 0)
      {
        Complex nextBox = boxesToCheck.Dequeue();
        needsMovement.Add(nextBox);
        if (walls.Contains(nextBox + movement) || walls.Contains(nextBox + Right + movement))
        {
          return false;
        }
        var (leftOccupied, nextLeft) = ContainsBox(nextBox + movement);
        var (rightOccupied, nextRight) = ContainsBox(nextBox + Right + movement);
        if (leftOccupied && nextLeft != nextBox)
        {
          boxesToCheck.Enqueue(nextLeft);
        }
      if (rightOccupied && nextRight != nextBox && nextRight != nextLeft)
        {
          boxesToCheck.Enqueue(nextRight);
        }
      }

    }

    return true;
  }


  public static void Move(Complex movement)
  {
    needsMovement.Clear();
    bool canMove = CalculatePossibleMovement(movement);
    if (!canMove)
    {
      return;
    }
    else
    {
      robot += movement;
      foreach (Complex needsMove in needsMovement)
      {
        boxes.Remove(needsMove);
      }
      foreach (Complex needsMove in needsMovement)
      {
        boxes.Add(needsMove + movement);
      }
    }
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

          Console.Write('[');
          c = ']';
          j++;
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

  public static string TranslateMovement(Complex movement) {
    if (movement == Up) {
      return "^";
    } else if (movement == Down) {
      return "v";
    } else if (movement == Left) {
      return "<";
    } else { // Right
      return ">";
    }
  }

  public static string Solve(String input)
  {
    Parse(input);

    long result = 0;
    int i = 0;
    int start = 20;
    foreach (var movement in movements)
    {
      Move(movement);
      /*
      if(i >= start && i < start + 5) {
        Console.WriteLine($"Move {i} {TranslateMovement(movement)}:");
        PrintMap();
        Console.WriteLine();
      }
      i++;
      */
    }

    // PrintMap();

    result = CalculateGPS();


    return result.ToString();
  }
}
