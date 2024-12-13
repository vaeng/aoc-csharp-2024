using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part1
{

  public static List<long> stones = new();

  public struct Claw
  {

    public Claw(Complex a, Complex b, Complex price)
    {
      A = a;
      B = b;
      Price = price;
      Cost = Int64.MaxValue;
      canWin = false;

      for (int i = 0; i <= 100; i++)
      {
        for (int j = 0; j <= 100; j++) {
        Complex move = i * a + j * b;
        if (move == price)
        {
          canWin = true;
          long coinsNeeded = 3 * i + j;
          if(Cost > coinsNeeded) {
            Cost = coinsNeeded;
            pushA = i;
            pushB = j;
          }
        }
      }
    }
  }

  public Complex pushA { get; set; }
  public Complex pushB { get; set; }
  public Complex A { get; init; }
  public Complex B { get; init; }
  public Complex Price { get; init; }

  public long Cost { get; init; }

  public bool canWin { get; init; }

  public override string ToString() => $"a: {A}, b: {B}, price: {Price} (Cost: {Cost} with Ax{pushA} Bx{pushB})";
  }

  public static List<Claw> claws = new();

  public static void Parse(List<String> input)
{
  for (int lineNumber = 0; lineNumber < input.Count; lineNumber += 4)
  {
    long[] rawA = input[lineNumber][11..].Split(new string[] { ", Y" }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt64(s)).ToArray();
    long[] rawB = input[lineNumber + 1][11..].Split(new string[] { ", Y" }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt64(s)).ToArray();
    long[] rawPrice = input[lineNumber + 2][9..].Split(new string[] { ", Y=" }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt64(s)).ToArray();
    Complex a = new Complex(rawA[0], rawA[1]);
    Complex b = new Complex(rawB[0], rawB[1]);
    Complex price = new Complex(rawPrice[0], rawPrice[1]);
    claws.Add(new Claw(a, b, price));
  }
}


public static string Solve(List<String> input)
{
  Parse(input);
  long result = 0;

  foreach (var claw in claws)
  {
    if (claw.canWin)
    {
      result += claw.Cost;
    }
  }
  return result.ToString();
}
}
