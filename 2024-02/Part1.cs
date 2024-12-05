using System;
using System.Collections.Generic;


public static class Part1
{
    public static string Solve(IEnumerable<String> input)
    {
        int safeReports = 0;

        foreach (var line in input)
        {
            string[] raw = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (raw.Length == 0) { continue; }
            else if (raw.Length == 1) { safeReports++; continue; }

            int[] parsed = raw.Select(s => Convert.ToInt32(s)).ToArray();
            bool increasing = true;
            if (parsed[0] > parsed[1]) { increasing = false; }

            bool valid = true;
            for (int i = 1; i < parsed.Length; ++i) {
                int last = parsed[i - 1];
                int current = parsed[i];
                if (increasing)
                {
                    if (last >= current)
                    {
                        valid = false;
                        break;
                    }
                    else
                    {
                        if (current - last > 3)
                        {
                            valid = false;
                            break;
                        }
                    }
                }
                else // decreasing
                {
                    if (last <= current)
                    {
                        valid = false;
                        break;
                    }
                    else
                    {
                        if (last - current> 3)
                        {
                            valid = false;
                            break;
                        }
                    }
                }
            }
            if (valid)
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }
}