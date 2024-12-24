using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.Json;

public static class Part1 {
  public static List<string> passwords = new();

  public static Complex KeypressToPos(char c) {
    switch (c) {
      case '0':
      case '^':
        return new Complex(1, -3);
      case '1':
        return new Complex(0, -2);
      case '2':
        return new Complex(1, -2);
      case '3':
        return new Complex(2, -2);
      case '4':
        return new Complex(0, -1);
      case '5':
        return new Complex(1, -1);
      case '6':
        return new Complex(2, -1);
      case '7':
        return new Complex(0, 0);
      case '8':
        return new Complex(1, 0);
      case '9':
        return new Complex(2, 0);
      case 'A':
        return new Complex(2, -3);
      case '>':
        return new Complex(2, -4);
      case '<':
        return new Complex(0, -4);
      case 'v':
        return new Complex(1, -4);
      default:
        throw new Exception($"Invalid Keypad Input: {c}");
    }
  }

  public static string PasswordToDirectionalKeypad(string password) {
    string keypresses = "";
    // Arm starts at 'A'
    Complex last = KeypressToPos('A');
    foreach (char c in password) {
      Complex target = KeypressToPos(c);
      Complex diff = target - last;
      if (last.Imaginary == -3 && target.Real == 0) {
        keypresses += new string('^', (int) Math.Abs(diff.Imaginary));
        diff = new(diff.Real, 0);
      }

      if (target.Imaginary == -3 && last.Real == 0) {
        keypresses += new string('>', (int) Math.Abs(diff.Real));
        diff = new(0, diff.Imaginary);
      }

      if (diff.Imaginary < 0) {
        keypresses += new string('v', (int) -diff.Imaginary);
      }

      if (diff.Real < 0) {
        keypresses += new string('<', (int) -diff.Real);
      }
      if (diff.Imaginary > 0) {
        keypresses += new string('^', (int) diff.Imaginary);
      }
      if (diff.Real > 0) {
        keypresses += new string('>', (int) diff.Real);
      }


      keypresses += "A";
      last = target;
    }

    return keypresses;
  }

  public static void Parse(List<String> input) {
    foreach (string line in input) {
      if (line.Length > 0) {
        passwords.Add(line);
      }
    }
  }

  public static Dictionary<(char, char), ulong> ArrowsToGroups(string arrows) {
    Dictionary<(char, char), ulong> groups = new();
    arrows = "A" + arrows;
    for (int i = 0; i < arrows.Length - 1; i++) {
      char a = arrows[i];
      char b = arrows[i + 1];
      if (groups.ContainsKey((a, b))) {
        groups[(a, b)]++;
      } else {
        groups[(a, b)] = 1;
      }
    }
    return groups;
  }

  public static Dictionary<(char, char), string> minInstructions = new(){
    {('^', '^'), "A"},
    {('^', 'v'), "vA"},
    {('^', '<'), "v<A"},
    {('^', '>'), "v>A"},
    {('^', 'A'), ">A"},

    {('v', '^'), "^A"},
    {('v', 'v'), "A"},
    {('v', '<'), "<A"},
    {('v', '>'), ">A"},
    {('v', 'A'), "^>A"},

    {('<', '^'), ">^A"},
    {('<', 'v'), ">A"},
    {('<', '<'), "A"},
    {('<', '>'), ">>A"},
    {('<', 'A'), ">>^A"},

    {('>', '^'), "<^A"},
    {('>', 'v'), "<A"},
    {('>', '<'), "<<A"},
    {('>', '>'), "A"},
    {('>', 'A'), "^A"},

    {('A', '^'), "<A"},
    {('A', 'v'), "<vA"},
    {('A', '<'), "v<<A"},
    {('A', '>'), "vA"},
    {('A', 'A'), "A"}
};

  private static Dictionary<(char, char), ulong> IterateStep(Dictionary<(char, char), ulong> lastIteration) {
    Dictionary<(char, char), ulong> groups = new();
    foreach (var (key, value) in lastIteration) {
      if (minInstructions.ContainsKey(key)) {
        string minInstruction = minInstructions[key];
        var newGroups = ArrowsToGroups(minInstruction);
        foreach (var (newKey, newValue) in newGroups) {
          if (groups.ContainsKey(newKey)) {
            groups[newKey] += newValue * value;
          } else {
            groups[newKey] = newValue * value;
          }
        }
      } else {
        throw new Exception("No instruction for " + key);
      }
    }
    return groups;
  }

  public static ulong ShortestSequenceLength(string arrows, int iterations) {
    var Iteration = ArrowsToGroups(arrows);
    for (int i = 0; i < iterations; i++) {
      Iteration = IterateStep(Iteration);
    }
    return Iteration.Values.ToList().Aggregate((i1, i2) => i1 + i2);
  }


  public static string Solve(List<String> input) {
    Parse(input);
    ulong result = 0;

    foreach (string password in passwords) {
      string arrows = PasswordToDirectionalKeypad(password);
      ulong length = ShortestSequenceLength(arrows, 2);
      ulong numerical = UInt64.Parse(password[..^1]);
      Console.WriteLine($"{password} -> {arrows}: {length} *  {numerical}");
      result += length * numerical;
    }

    return result.ToString();
  }
}
