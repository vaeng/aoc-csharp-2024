using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;

public static class Part1
{
  public static List<ulong> secrets = new();

  public static void Parse(List<String> input)
  {
    foreach(string line in input) {
      if(line.Length > 0 ) {
        secrets.Add(UInt64.Parse(line));
      }
    }
  }

  public static ulong nextSecret(ulong secret) {
    ulong first = secret ^ (secret * 64 % 16777216);
    ulong second = first ^ (first / 32);
    ulong third = second  ^( second * 2048 % 16777216);
    ulong nextSecret =  third % 16777216;

    return nextSecret;
  }

  public static string Solve(List<String> input)
  {
    Parse(input);
    for(int i = 0; i < secrets.Count; i++) {
      for(int j = 0; j < 2000; j++) {
        //Console.WriteLine($"{nextSecret(secrets[i])}");
        secrets[i] = nextSecret(secrets[i]);
      }
    }
    ulong result = 0;
    foreach(ulong secret in secrets) {
      result += secret;
    }




    return result.ToString();
  }
}
