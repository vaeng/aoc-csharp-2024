using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part2 {
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

  public static ulong Calculate(char c) {
    ulong result = 0;
    int i = 0;
    while (true) {
      string bitPart = i > 9 ? $"{c}{i}" : $"{c}0{i}";
      if (!wireStates.ContainsKey(bitPart)) {
        break;
      }
      result += wireStates[bitPart] ? (ulong) Math.Pow(2, i) : 0;
      i++;
    }
    return result;
  }

  public static void Set(char c, ulong value) {
    int i = 0;
    while (true) {
      string bitPart = i > 9 ? $"{c}{i}" : $"{c}0{i}";
      if (!wireStates.ContainsKey(bitPart)) {
        break;
      }
      wireStates[bitPart] = (value & (1UL << i)) != 0;
      i++;
    }
  }

  public static List<string> GetFaultyZGates(ulong expectedZ, ulong calculatedZ) {
    int i = 0;
    List<string> faultyGates = new();
    while (true) {
      string bitPart = i > 9 ? $"z{i}" : $"z0{i}";
      if (!wireStates.ContainsKey(bitPart)) {
        break;
      }
      if ((expectedZ & (1UL << i)) != (wireStates[bitPart] ? 1UL : 0UL)) {
        faultyGates.Add(bitPart);
      }
      i++;
    }
    return faultyGates;
  }

  public static List<string> GetRealtedGates(string wire) {
    List<string> relatedGates = new();
    relatedGates.Add(wire);
    Queue<string> queue = new();
    queue.Enqueue(wire);
    while (queue.Count > 0) {
      var currentWire = queue.Dequeue();
      foreach (var gate in gates) {
        if (gate.output == currentWire) {
          relatedGates.Add(gate.input1);
          relatedGates.Add(gate.input2);
          queue.Enqueue(gate.input1);
          queue.Enqueue(gate.input2);
        }
      }
    }
    return relatedGates;
  }

  public static void PrinItems(IEnumerable<string> items) {
    foreach (var item in items) {
      Console.Write($"{item}, ");
    }
    Console.WriteLine();
  }

  public static ulong GetMax(char c) {
    ulong max = 0;
    foreach (var wire in wireStates.Keys) {
      if (wire[0] == c) {
        max = Math.Max(max, UInt64.Parse(wire[1..]));
      }
    }
    return max;
  }


  public static string Solve(List<String> input) {
    Parse(input);
    // ExecuteGates();

    // get max z
    ulong maxZ = 45; //ZUGetMax('z');
    ulong maxX = GetMax('x');
    ulong maxY = GetMax('y');
    Dictionary<ulong, string> andGates = new();
    Dictionary<ulong, string> xorGates = new();
    Dictionary<ulong, string> xor2Gates = new();
    Dictionary<ulong, string> and2Gates = new();
    Dictionary<ulong, string> orGates = new();

    orGates[1] = "tdp";

    // check if each z is connected to XOR gate with correct x and y wires
    for (ulong i = 0; i <= maxZ; i++) {
      string xWire = i > 9 ? $"x{i}" : $"x0{i}";
      string yWire = i > 9 ? $"y{i}" : $"y0{i}";
      string zWire = i > 9 ? $"z{i}" : $"z0{i}";
      // Console.WriteLine($"Checking {xWire} {yWire} {zWire}");
      foreach (var gate in gates) {
        if ((gate.input1 == xWire && gate.input2 == yWire)
            || (gate.input2 == xWire && gate.input1 == yWire)) {
          if (gate.operation == "AND") {
            andGates[i] = gate.output;
          } else if (gate.operation == "XOR") {
            xorGates[i] = gate.output;
          }
        }
      }
    }

    Console.WriteLine($"AND Gates: {andGates.Count}");
    Console.WriteLine($"XOR Gates: {xorGates.Count}");

    for (ulong i = 1; i < maxZ; i++) {
      // check if second level or is correct
      string zWire = i > 9 ? $"z{i}" : $"z0{i}";
      string lowAND = andGates[i];
      string lowXOR = xorGates[i];
      foreach (var gate in gates) {
        if (gate.output == zWire) {
          if (gate.operation == "XOR") {
            if (!(gate.input1 == lowXOR || gate.input2 == lowXOR)) {
              Console.WriteLine($"A final XOR gate should be wired to low xor {lowXOR}!");
              Console.WriteLine($"Faulty gate {gate.input1} {gate.operation} {gate.input2} -> {gate.output} ");
            }
          } else {
            Console.WriteLine($"A XOR gate should be wired to {zWire}!");
            Console.WriteLine($"Faulty gate {gate.input1} {gate.operation} {gate.input2} -> {gate.output} ");
          }
        }

      }

    }
    // use displayed solutions to manually draw graphs and find:
    List<string> solution = ["gws", "nnt", "npf", "z13", "z19", "cph", "z33", "hgj"  ];

    solution.Sort();
    PrinItems(solution);
    return 0.ToString();
  }
}

