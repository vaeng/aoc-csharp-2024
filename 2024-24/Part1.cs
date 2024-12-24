using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part1 {
  public static Dictionary<string, bool> wireStates = [];

  public static List<Gate> gates = new();

  public struct Gate {
    public string input1;
    public string input2;
    public string output;
    public string operation;

    public Gate(string input1, string input2, string output, string operation) {
      this.input1 = input1;
      this.input2 = input2;
      this.output = output;
      this.operation = operation;
    }

    public bool CanExecute() {
      return wireStates.ContainsKey(input1) && wireStates.ContainsKey(input2);
    }

    public void Execute() {
      // Console.WriteLine($"Executing {input1} {operation} {input2} -> {output}");
      switch (operation) {
        case "AND":
          wireStates[output] = wireStates[input1] & wireStates[input2];
          break;
        case "OR":
          wireStates[output] = wireStates[input1] | wireStates[input2];
          break;
        case "XOR":
          wireStates[output] = wireStates[input1] ^ wireStates[input2];
          break;
      }
    }
  }
  public static void Parse(List<string> input) {

    foreach (var line in input) {
      var parts = line.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries).ToArray();
      if (parts.Length == 2) {
        wireStates[parts[0]] = parts[1] == "1" ? true : false;
      }
      parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
      if (parts.Length == 5) {
        gates.Add(new Gate(parts[0], parts[2], parts[4], parts[1]));
      }
    }

  }

  public static void ExecuteGates() {
    while (gates.Count > 0) {
      for (int i = 0; i < gates.Count; i++) {
        if (gates[i].CanExecute()) {
          gates[i].Execute();
          gates.RemoveAt(i);
        }
      }
    }
  }

  public static long CalculateSignal() {
    long result = 0;
    int i = 0;
    while(true) {
      string bitPart = i > 9 ? $"z{i}" : $"z0{i}";
      if (!wireStates.ContainsKey(bitPart)) {
        break;
      }
      result += wireStates[bitPart] ? (long)Math.Pow(2, i) : 0;
      i++;
    }
    return result;
  }


  public static string Solve(List<String> input) {
    Parse(input);

    ExecuteGates();
    long result = CalculateSignal();

    return result.ToString();
  }
}
