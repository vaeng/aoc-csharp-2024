using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part2
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

  public static long PossibleCombos(string design)
  {
    PriorityQueue<string, int> pq = new();
    pq.Enqueue(design, design.Length);

    Dictionary<string, HashSet<string>> backTrack = new();

    while (pq.Count > 0)
    {
      var current = pq.Dequeue();
      if(backTrack.ContainsKey(current)) {
        continue;
      } else {
        backTrack[current] = new();
      }

      if (current.Length == 0)
      {
        continue;
      }
      foreach (var pattern in possiblePatterns)
      {
        if (pattern.Length > current.Length || pattern.Length == 0)
        {
          continue;
        }

        //Console.WriteLine($"Checking for {pattern} in {current}, Pattern cache size: {possiblePatterns.Count}, Heap size: {pq.Count}");
        if (current.StartsWith(pattern))
        {
          string next = current[(pattern.Length)..];
          if (backTrack[current].Contains(next))
          {
            continue;
          }
          else
          {
            backTrack[current].Add(next);
            pq.Enqueue(next, next.Length);
          }
        }
      }
    }

    long possibilities = sumPaths(backTrack, design, "");
/*
    foreach (var (node, parents) in backTrack) {
      Console.Write($"{node}: ");
      foreach (var parent in parents)
      {
        Console.Write($"{parent},");
      }
      Console.WriteLine();
    }
*/
    return possibilities;
  }

  public static Dictionary<string, long> cache = new();

  public static long sumPaths(Dictionary<string, HashSet<string>> tree, string from, string to)
  {
    if(cache.ContainsKey(from)) {
      return cache[from];
    }
    long sum = 0;
    foreach (string parent in tree[from])
    {
      if (parent == to)
      {
        sum += 1;
      }
      else
      {
        sum += sumPaths(tree, parent, to);
      }
    }
    cache[from] = sum;
    return sum;
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
    foreach (var pattern in patterns)
    {
      AddAndSort(pattern);
    }

    int i = 1;
    foreach (var design in designs)
    {
      long combos = PossibleCombos(design);
      //Console.WriteLine($"[{100 * i / designs.Count}%] -- {design} Combos: {combos}");
      result += combos;
      i++;
    }

    return result.ToString();
  }
}
