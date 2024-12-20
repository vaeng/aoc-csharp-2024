using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part1 {
  public static Dictionary<string, List<string>> replacements = new();
  public static string medicine = "";

  public static HashSet<string> creations = new();
  public static void Parse(List<String> input) {

    foreach (var line in input) {
      var parts = line.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
      if (parts.Length == 2) {
        if (!replacements.ContainsKey(parts[0])) {
          replacements[parts[0]] = new();
        }
        replacements[parts[0]].Add(parts[1]);
      } else if (parts.Length == 1) {
        medicine = line;
      }
    }

  }


  public static string Solve(List<String> input) {
    Parse(input);

    foreach (var (replaces, replacementList) in replacements) {
      int i = 0;
      int start = 0;
      while (true) {
        i = medicine.IndexOf(replaces, start, medicine.Length - start);
        start = i + replaces.Length;
        if (i == -1) {
          break;
        }
        string mod = medicine.Remove(i, replaces.Length);
        foreach (var part in replacementList) {
          creations.Add(mod.Insert(i, part));
        }
      }
    }
    /*
    foreach(var creation in creations) {
      Console.WriteLine(creation);
    }
    */
    long result = creations.Count;
    return result.ToString();
  }
}
