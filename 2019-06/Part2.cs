using System;
using System.Collections.Generic;
using System.Numerics;



public static class Part2
{

  public static Dictionary<string, string> orbits = new();
  public static Dictionary<string, long> counter = new();

  public static void Parse(List<string> input)
  {
    foreach (var line in input)
    {
      string[] raw = line.Split(')');
      orbits[raw[1]] = raw[0];
    }
  }

  public static void PrintMap()
  {
    foreach (var (obj, orbiter) in orbits)
    {
      Console.WriteLine($"{obj}: {orbiter}");
    }
  }

  public static long GetOrbitCounter(string obj)
  {
    long result = 0;
    if (counter.ContainsKey(obj))
    {
      result =  counter[obj];
    }
    else
    {
      result = 1 + GetOrbitCounter(orbits[obj]);
    }
    counter[obj] = result;
    return result;
  }

  public static long GetStepsToCommonObj(string obj)
  {

    if (counter.ContainsKey(obj))
    {
      return - counter[obj];
    }
    else
    {
      return 1 + GetStepsToCommonObj(orbits[obj]);
    }
  }


  public static string Solve(List<String> input)
  {
    Parse(input);

    long results = 0;

    counter["COM"] = 0;

    long stepsToCOM = GetOrbitCounter("YOU");
    foreach (var (obj, cnt) in counter) {
      Console.WriteLine($"{obj}: {cnt}");
    }

    results = stepsToCOM + GetStepsToCommonObj("SAN") - 2;



    return results.ToString();
  }
}
