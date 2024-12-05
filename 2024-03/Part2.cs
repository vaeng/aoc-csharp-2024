using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public static class Part2
{

    public static string Solve(IEnumerable<String> input)
    {
        int result = 0;
        string pattern = @"(do\(\)|don\'t\(\)|mul\(([0-9]+),([0-9]+)\))";
        Regex rg = new Regex(pattern);

        int mult = 1;
        foreach (var line in input)
        {
            MatchCollection matchedMultiplications = rg.Matches(line);

            for (int count = 0; count < matchedMultiplications.Count; count++) {
                if (matchedMultiplications[count].Groups[0].Value == "do()") {
                    mult = 1;
                } else if (matchedMultiplications[count].Groups[0].Value == "don't()") {
                    mult = 0;
                } else {
                    result += mult * Int32.Parse(matchedMultiplications[count].Groups[2].Value) * Int32.Parse(matchedMultiplications[count].Groups[3].Value);
                }
            }
        }

        return result.ToString();
    }
}