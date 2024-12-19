using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part1
{


  public static HashSet<string> patterns = new();
  public static List<string> designs = new();

  internal class ByLengthSorter : IComparer<string>
  {
    public int Compare(string? x, string? y)
    {
      if (x == null || y == null)
      {
        return 0;
      }
      return -x.Length.CompareTo(y.Length);
    }
  }

  private static ByLengthSorter lengthSorter = new();

  public static HashSet<string> possiblePatterns = new();
  public static void Parse(List<String> input)
  {

    patterns = input[0].Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToHashSet();

    foreach (var line in input[2..])
    {
      if (line.Length > 0)
      {
        designs.Add(line);
      }
    }
  }

  public static bool IsPossible(string design)
  {
    PriorityQueue<string, int> pq = new();
    pq.Enqueue(design, design.Length);

    HashSet<string> alreadyChecked = new();

    while (pq.Count > 0)
    {
      var current = pq.Dequeue();
      if(!alreadyChecked.Add(current)) {
        continue;
      }
      if (current.Length == 0)
      {
        AddAndSort(current);
        return true;
      }
      foreach (var pattern in possiblePatterns)
      {
        if(pattern.Length > current.Length || pattern.Length == 0) {
          continue;
        }

        //Console.WriteLine($"Checking for {pattern} in {current}, Pattern cache size: {possiblePatterns.Count}, Heap size: {pq.Count}");
        if (current.StartsWith(pattern))
        {
          pq.Enqueue(current[(pattern.Length)..], current.Length - pattern.Length);
        }
      }
    }

    return false;
  }

  public static void AddAndSort(string newPattern)
  {
    if (newPattern.Length > 0)
    {
      possiblePatterns.Add(newPattern);
    }
    //possiblePatterns.Sort(lengthSorter);
  }

  public static string Solve(List<String> input)
  {
    Parse(input);

    long result = 0;
    foreach (var pattern in patterns) {
      AddAndSort(pattern);
    }

    int i = 1;
    foreach (var design in designs)
    {
      //Console.WriteLine($"{100 * i / designs.Count}% Analysing: {design}\nPattern Database size: {possiblePatterns.Count}");
      result += IsPossible(design) ? 1 : 0;
      i++;
    }

    return result.ToString();
  }
}
