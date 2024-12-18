using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Text;

public static class Part2
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

  public static string PrintFormattedOutput(List<long> l)
  {
    StringBuilder sb = new();
    for (int i = 0; i < l.Count; i++)
    {
      sb.Append($"{l[i]}");
      if (i != l.Count - 1)
      {
        sb.Append(",");
      }
    }
    return sb.ToString();
  }

  public static long GetCombo(long operand)
  {
    return operand switch
    {
      4 => RegisterA,
      5 => RegisterB,
      6 => RegisterC,
      > 6 => throw new Exception($"Unknown operation: {operand}"),
      < 0 => throw new Exception($"Unknown operation: {operand}"),
      _ => operand,
    };
  }

  public static void adv(long operand)
  {
    RegisterA = RegisterA / (long)Math.Pow(2, GetCombo(operand));
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
    if (RegisterA != 0)
    {
      InstructionPointer = (int)operand - 2;
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
    RegisterB = RegisterA / (long)Math.Pow(2, GetCombo(operand));
    return;
  }
  public static void cdv(long operand)
  {
    RegisterC = RegisterA / (long)Math.Pow(2, GetCombo(operand));
    return;
  }

  public static void RunOneInstruction()
  {
    long op = Program[InstructionPointer];
    long operand = Program[InstructionPointer + 1];

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
    InstructionPointer += 2;
  }

  public static bool LoopAndCheckProgram()
  {
    while (true)
    {
      for (int i = 0; i < Output.Count; i++)
      {
        if (Program[i] != Output[i])
        {
          // no success with this Register, try next
          return false;
        }
      }
      // so far all the same
      if (Output.Count == Program.Count)
      {
        // same length!
        return true;
      }
      if (InstructionPointer >= Program.Count)
      {
        return false;
      }
      RunOneInstruction();
    }
  }

  public static List<long> RunOptimizedProgram(long A)
  {
    List<long> resultingOutput = new();
    while (A > 0)
    {
      resultingOutput.Add(((A % 8) ^ 1 ^ 4) ^ ((A % 8) / (long)Math.Pow(2, (A % 8) ^ 1)));
      A = A / 8;
    }
    return resultingOutput;
  }
  public static long ReverseProgram(List<long> target)
  {
    long A = 0;
    target.Reverse();
    List<long> resultingOutput = new();
    List<long> notFoundForIndex = new();
    List<long> badBunch = new();
    long i = 0;
    foreach (long x in target)
    {
      bool found = false;
      for (long s = 0; s < 8; s++)
      {
        // (((A % 8) ^ 1 ^ 4) ^ ((A % 8) / (long)Math.Pow(2, (A % 8) ^ 1)))
        if (x == ((((s) ^ 1) ^ 4) ^ ((s) / (long)Math.Pow(2, ((s) ^ 1)))) % 8)
        {
          resultingOutput.Add(s);
          found = true;
          break;
        }
      }
      if (!found)
      {
        resultingOutput.Add(0);
        badBunch.Add(i);
        Console.WriteLine(i);
      }

      i++;
    }

    for (long bb0 = 0; bb0 < 8; bb0++)
    {
      for (long bb1 = 0; bb1 < 8; bb1++)
      {
        for (long bb2 = 0; bb2 < 8; bb2++)
        {
          for (long bb3 = 0; bb3 < 8; bb3++)
          {
            for (long bb4 = 0; bb4 < 8; bb4++)
            {
              for (long bb5 = 0; bb5 < 8; bb5++)
              {
                for (long bb6 = 0; bb6 < 8; bb6++)
                {
                  for (long bb7 = 0; bb7 < 8; bb7++)
                  {
                    for (long bb8 = 0; bb8 < 8; bb8++)
                    {
                      for (long bb9 = 0; bb9 < 8; bb9++)
                      {
                        for (long bb10 = 0; bb10 < 8; bb10++)
                        {
                          for (long bb11 = 0; bb11 < 8; bb11++)
                          {
                            for (long bb12 = 0; bb12 < 8; bb12++)
                            {
                              for (long bb13 = 0; bb13 < 8; bb13++)
                              {
                                for (long bb14 = 0; bb14 < 8; bb14++)
                                {
                                  for (long bb15 = 0; bb15 < 8; bb15++)
                                  {
                                    A = 0;
                                    resultingOutput[0] = bb0;
                                    resultingOutput[1] = bb1;
                                    resultingOutput[3] = bb3;
                                    resultingOutput[4] = bb4;
                                    resultingOutput[5] = bb5;
                                    resultingOutput[6] = bb6;
                                    resultingOutput[7] = bb7;
                                    resultingOutput[8] = bb8;
                                    resultingOutput[9] = bb9;
                                    resultingOutput[10] = bb10;
                                    resultingOutput[11] = bb11;
                                    resultingOutput[12] = bb12;
                                    resultingOutput[13] = bb13;
                                    resultingOutput[14] = bb14;
                                    resultingOutput[15] = bb15;


                                    foreach (long x in resultingOutput)
                                    {
                                      A *= 8;
                                      if (x >= 0)
                                      {
                                        A += x;
                                      }
                                    }
                                    long reverseA = 0;
                                    if (Program.SequenceEqual(RunOptimizedProgram(A)))
                                    {
                                      return A;
                                    }
                                  }
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
    }

    return -1;
  }

  public static string Solve(List<String> input)
  {
    Parse(input);

    long Iteration = ReverseProgram(Program);

    return Iteration.ToString();
  }
}
