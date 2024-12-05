using System;
using System.Collections.Generic;


public static class Part1
{
    public static string Solve(IEnumerable<String> input)
    {
        List<int> leftIds = new List<int>();
        List<int> rightIds = new List<int>();

        foreach (var line in input) {
            string[] raw = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(raw[0], "\n");
            Console.WriteLine(raw[1], "\n");
            leftIds.Add(int.Parse(raw[0]));
            rightIds.Add(int.Parse(raw[1]));
        }

        leftIds.Sort();
        rightIds.Sort();

        int totalMinDistance = 0;

        for (int i = 0; i < leftIds.Count; ++i) {
            totalMinDistance += Math.Abs(leftIds[i] - rightIds[i]);
        }

        return totalMinDistance.ToString();
    }
}