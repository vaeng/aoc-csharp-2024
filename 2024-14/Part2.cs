using System;
using System.Collections.Generic;
using System.Numerics;

public static class Part2 {

  public struct Claw {

    public Claw(Complex a, Complex b, Complex price) {
      A = a;
      B = b;
      Price = price;
      Cost = 0;

      double divisor = (a.Real * b.Imaginary - a.Imaginary * b.Real);

      double i = (price.Real * b.Imaginary - price.Imaginary * b.Real);
      double j = (price.Imaginary * a.Real - price.Real * a.Imaginary);

      pushA = (long) (i / divisor);
      pushB = (long) (j / divisor);

      if (pushA >= 0 && pushB >= 0 && a * pushA + b * pushB == price) {
        Cost = 3 * pushA + pushB;
      }
    }

    public long pushA { get; set; }
    public long pushB { get; set; }
    public Complex A { get; init; }
    public Complex B { get; init; }
    public Complex Price { get; init; }

    public long Cost { get; init; }
    public override string ToString() => $"a: {A}, b: {B}, price: {Price} (Cost: {Cost} with Ax{pushA} Bx{pushB})";
  }

  public static List<Claw> claws = new();

  public static void Parse(List<String> input) {
    for (int lineNumber = 0; lineNumber < input.Count; lineNumber += 4) {
      long[] rawA = input[lineNumber][11..].Split(new string[] { ", Y" }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt64(s)).ToArray();
      long[] rawB = input[lineNumber + 1][11..].Split(new string[] { ", Y" }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt64(s)).ToArray();
      long[] rawPrice = input[lineNumber + 2][9..].Split(new string[] { ", Y=" }, StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt64(s)).ToArray();
      Complex a = new Complex(rawA[0], rawA[1]);
      Complex b = new Complex(rawB[0], rawB[1]);
      Complex price = new Complex(rawPrice[0] + 10000000000000, rawPrice[1] + 10000000000000);
      claws.Add(new Claw(a, b, price));
    }
  }


  public static string Solve(List<String> input) {
    Parse(input);
    long result = 0;

    foreach (var claw in claws) {
      result += claw.Cost;
    }
    return result.ToString();
  }
}
