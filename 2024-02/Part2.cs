using System;
using System.Collections.Generic;


public static class Part2
{

    public static List<int> GetIntsList(string line)
    {
        string[] raw = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return raw.Select(s => Convert.ToInt32(s)).ToList();
    }


    public static bool LineIsValid(List<int> parsed)
    {
        bool increasing = true;
        if (parsed[0] > parsed[1]) { increasing = false; }

        bool valid = true;
        for (int i = 1; i < parsed.Count; ++i)
        {
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
                    if (last - current > 3)
                    {
                        valid = false;
                        break;
                    }
                }
            }
        }
        return valid;
    }
    public static string Solve(IEnumerable<String> input)
    {

        int safeReports = 0;

        foreach (var line in input)
        {
            var  parsed = GetIntsList(line);

            bool valid = LineIsValid(parsed);
            int i = 0;
            while(!valid && i < parsed.Count) {
                var altered_list = parsed.ToList();
                altered_list.RemoveAt(i);
                valid = LineIsValid(altered_list);
                i++;
            }

            if (valid)
            {
                safeReports++;
            }
        }

        return safeReports.ToString();
    }
}