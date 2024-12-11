using System;
using System.Collections.Generic;
using System.Numerics;



public static class Part1
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


  public static string Solve(List<String> input)
  {
    Parse(input);

    long results = 0;

    counter["COM"] = 0;


    foreach (var obj in orbits.Keys)
    {
      results += GetOrbitCounter(obj);
    }



    return results.ToString();
  }
}
