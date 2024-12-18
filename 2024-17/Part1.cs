using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Text;

public static class Part1
{

  public static long RegisterA = 0;
  public static long RegisterB = 0;
  public static long RegisterC = 0;

  public static int InstructionPointer = 0;

  public static List<long> Program = new();
  public static List<long> Output = new();


  public static void Parse(List<String> input)
  {

    RegisterA = Convert.ToInt64(input[0][12..]);
    RegisterB = Convert.ToInt64(input[1][12..]);
    RegisterC = Convert.ToInt64(input[2][12..]);
    Program = input[4][9..].Split(',').Select(s => Convert.ToInt64(s)).ToList();

  }

  public static string PrintFormattedOutput()
  {
    StringBuilder sb = new();
    for (int i = 0; i < Output.Count; i++)
    {
      sb.Append($"{Output[i]}");
      if (i != Output.Count - 1)
      {
        sb.Append(",");
      }
    }
    return sb.ToString();
  }

  public static long GetCombo(long operand) {
    return operand switch {
      4 => RegisterA,
      5 => RegisterB,
      6 => RegisterC,
      >6 => throw new Exception($"Unknown operation: {operand}"),
      <0 => throw new Exception($"Unknown operation: {operand}"),
      _ => operand,
    };
  }

  public static void adv(long operand)
  {
    RegisterA = RegisterA / (long) Math.Pow(2, GetCombo(operand));
    return;
  }
  public static void bxl(long operand)
  {
    RegisterB = RegisterB ^ operand;
    return;
  }
  public static void bst(long operand)
  {
    RegisterB = GetCombo(operand) % 8;
    return;
  }
  public static void jnz(long operand)
  {
    if(RegisterA != 0) {
      InstructionPointer = (int) operand - 2;
      // will be incremented by two for all instructions
    }
    return;
  }
  public static void bxc(long operand)
  {
    RegisterB = RegisterB ^ RegisterC;
    return;
  }
  public static void outOp(long operand)
  {
    Output.Add(GetCombo(operand) % 8);
    return;
  }
  public static void bdv(long operand)
  {
    RegisterB = RegisterA / (long) Math.Pow(2, GetCombo(operand));
    return;
  }
  public static void cdv(long operand)
  {
    RegisterC = RegisterA / (long) Math.Pow(2, GetCombo(operand));
    return;
  }

  public static void RunProgram()
  {
    if (InstructionPointer >= Program.Count)
    {
      return;
    }

    long op = Program[InstructionPointer];
    long operand = Program[InstructionPointer + 1];
    //Console.WriteLine($"Op: {op}, Operand: {operand}");
    switch (op)
    {
      case 0:
        adv(operand);
        break;
      case 1:
        bxl(operand);
        break;
      case 2:
        bst(operand);
        break;
      case 3:
        jnz(operand);
        break;
      case 4:
        bxc(operand);
        break;
      case 5:
        outOp(operand);
        break;
      case 6:
        bdv(operand);
        break;
      case 7:
        cdv(operand);
        break;
      default:
        throw new Exception($"Unknown operation: {op}");
    }
    //Console.WriteLine($"A: {RegisterA}, B: {RegisterB}, C: {RegisterC}");
    InstructionPointer += 2;
    RunProgram();
  }

  public static void RunOptimizedProgram(long A) {
    while(A > 0) {
      Output.Add(((A % 8) ^ 1 ^ 4) ^ ((A % 8) / (long)Math.Pow(2, (A % 8) ^ 1)));
      A = A / 8;
    }
  }


  public static string Solve(List<String> input)
  {
    Parse(input);

    RunProgram();
    RunOptimizedProgram(RegisterA);

    return PrintFormattedOutput().ToString();
  }
}
