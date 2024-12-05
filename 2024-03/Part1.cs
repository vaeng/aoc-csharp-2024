using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public static class Part1
{
    public static string Solve(IEnumerable<String> input)
    {
        int result = 0;
        string pattern = @"mul\(([0-9]+),([0-9]+)\)";
        Regex rg = new Regex(pattern);

        foreach (var line in input)
        {
            MatchCollection matchedMultiplications = rg.Matches(line);

            for (int count = 0; count < matchedMultiplications.Count; count++) {
                result += Int32.Parse(matchedMultiplications[count].Groups[1].Value) * Int32.Parse(matchedMultiplications[count].Groups[2].Value);
            }
        }

        return result.ToString();
    }
}