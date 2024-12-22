using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;

public static class Part2
{
  public static List<ulong> secrets = new();

  public static void Parse(List<String> input)
  {
    foreach (string line in input)
    {
      if (line.Length > 0)
      {
        secrets.Add(UInt64.Parse(line));
      }
    }
  }

  public static ulong nextSecret(ulong secret)
  {
    ulong first = secret ^ (secret * 64 % 16777216);
    ulong second = first ^ (first / 32);
    ulong third = second ^ (second * 2048 % 16777216);
    ulong nextSecret = third % 16777216;

    return nextSecret;
  }

  public static string Solve(List<String> input)
  {
    Parse(input);
      Dictionary<(int, int, int, int), ulong> sequenceAmounts = new();
    for (int i = 0; i < secrets.Count; i++)
    {
      Dictionary<(int, int, int, int), int> localSequenceAmounts = new();
      List<int> last = new() { (int)(secrets[i] % 10), 0, 0, 0, 0 };
      for (int j = 0; j < 2000; j++)
      {
        last.RemoveAt(4);
        ulong next = nextSecret(secrets[i]);
        last.Insert(0, (int)(next % 10));
        if (j >= 3)
        {
          (int, int, int, int) changes = (0, 0, 0, 0);

          changes.Item1 = -last[1] + last[0];
          changes.Item2 = -last[2] + last[1];
          changes.Item3 = -last[3] + last[2];
          changes.Item4 = -last[4] + last[3];
          if(!localSequenceAmounts.ContainsKey(changes)) {
            localSequenceAmounts[changes] = (int)(next % 10);
          }
          //Console.WriteLine($"{next} {changes}");
        }
        secrets[i] = next;
      }
      foreach(var (sequence, amount) in localSequenceAmounts) {
        if(!sequenceAmounts.ContainsKey(sequence)) {
          sequenceAmounts[sequence] = (ulong)amount;
        } else {
          sequenceAmounts[sequence] += (ulong)amount;
        }
      }
    }
    foreach(var (sequence, amount) in sequenceAmounts) {
      //Console.WriteLine($"{sequence} {amount}");
    }
    ulong result = sequenceAmounts.Values.Max();
    return result.ToString();
  }
}
