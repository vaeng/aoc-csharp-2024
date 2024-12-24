using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;

public static class Part1 {

  public static Dictionary<string, List<string>> connections = new();
  public static HashSet<List<string>> triplets = new();
  public static void Parse(List<String> input) {
    foreach (var line in input) {
      if (line.Length > 0) {
        string[] computers = line.Split('-').ToArray();
        if (!connections.ContainsKey(computers[0])) {
          connections[computers[0]] = new();
        }
        if (!connections.ContainsKey(computers[1])) {
          connections[computers[1]] = new();
        }
        connections[computers[1]].Add(computers[0]);
        connections[computers[0]].Add(computers[1]);
      }
    }
  }

  public static void GetTriplets() {
    foreach (var (server, clients) in connections) {
      //Console.Write($"{server} has the following connections: ");
      foreach (var client in clients) {
        foreach (var subclient in connections[client]) {
          if (clients.Contains(subclient)) {
            if (server[0] == 't' || client[0] == 't' || subclient[0] == 't') {
              List<string> triple = new() { server, client, subclient };
              triple.Sort();
              if (!triplets.Contains(triple)) {
                triplets.Add(triple);
                Console.WriteLine($"{client} {server} {subclient}");
              }
            }
          }
        }
      }
      //Console.WriteLine();
    }
  }

  public static string Solve(List<String> input) {
    Parse(input);
    long result = 0;
    GetTriplets();



    return connections.Keys.Count.ToString();
  }

}
