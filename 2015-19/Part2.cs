using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part2
{
  public static Dictionary<string, List<string>> replacements = new();
  public static string medicine = "";

  public static HashSet<string> lastStepsCreations = new();
  public static HashSet<string> thisStepsCreations = new();
  public static void Parse(List<String> input)
  {

    foreach (var line in input)
    {
      var parts = line.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
      if (parts.Length == 2)
      {
        if (!replacements.ContainsKey(parts[0]))
        {
          replacements[parts[0]] = new();
        }
        replacements[parts[0]].Add(parts[1]);
      }
      else if (parts.Length == 1)
      {
        medicine = line;
      }
    }

  }

  public static void MakeStep()
  {
    foreach (var (replaces, replacementList) in replacements)
    {
      int i = 0;
      int start = 0;
      foreach (string molecule in lastStepsCreations)
      {
        while (true)
        {
          i = molecule.IndexOf(replaces, start, molecule.Length - start);
          start = i + replaces.Length;
          if (i == -1)
          {
            break;
          }
          string mod = molecule.Remove(i, replaces.Length);
          foreach (var part in replacementList)
          {
            thisStepsCreations.Add(mod.Insert(i, part));
          }
        }
      }
    }
  }

  public static void StepBackwards()
  {
    foreach (var (replaces, replacementList) in replacements)
    {
      foreach (string molecule in lastStepsCreations)
      {
        foreach (var part in replacementList)
        {
          int i = 0;
          int start = 0;
          while (true)
          {
            i = molecule.IndexOf(part, start, molecule.Length - start);
            start = i + part.Length;
            if (i == -1)
            {
              break;
            }
            string mod = molecule.Remove(i, part.Length);

            thisStepsCreations.Add(mod.Insert(i, replaces));
          }
        }
      }
    }
  }

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

  public static void PruneBySize(int top)
  {
    if (top > thisStepsCreations.Count)
    {
      return;
    }
    //var creations = thisStepsCreations.ToList();
    //creations.Sort(lengthSorter);
    thisStepsCreations = thisStepsCreations.OrderBy(x => x.Length).Take(top).ToHashSet();
  }

  public static long CountSteps(string source, string target)
  {
    long stepCounter = 0;

    thisStepsCreations.Clear();
    thisStepsCreations.Add(source);

    while (!thisStepsCreations.Contains(target))
    {
      stepCounter++;
      lastStepsCreations.Clear();
      (thisStepsCreations, lastStepsCreations) = (lastStepsCreations, thisStepsCreations);
      //MakeStep();
      StepBackwards();
      PruneBySize(1);
      /*
      foreach (var creation in thisStepsCreations)
      {
        Console.Write(creation);
        Console.Write(",");
      }
      Console.WriteLine();
      */
      Console.WriteLine($"Creationsize: {thisStepsCreations.Count}");
    }

    return stepCounter;
  }


  public static string Solve(List<String> input)
  {
    Parse(input);


    long result = CountSteps(medicine, "e");
    /*
        foreach(var creation in thisStepsCreations) {
          Console.WriteLine(creation);
        }
    */
    return result.ToString();
  }
}
