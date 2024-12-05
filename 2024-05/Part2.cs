using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public static class Part2
{

    static Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();
    static List<int[]> updates = new();

    public static void Parse(IEnumerable<String> input) {
        foreach (var line in input)
        {
            if (line.Contains("|"))
            {
                int[] pages = Array.ConvertAll(line.Split("|"), int.Parse);
                if (!rules.ContainsKey(pages[0]))
                {
                    rules[pages[0]] = new List<int>();
                }
                rules[pages[0]].Add(pages[1]);
            }
            else if (line == "")
            {
                continue;
            }
            else
            {
                updates.Add(Array.ConvertAll(line.Split(","), int.Parse));
            }
        }
    }

    public static bool IsInvalid(int[] update) {
        for (int i = 0; i < update.Length; i++)
        {
            if (rules.ContainsKey(update[i]))
            {
                var beforePages = update.Take(i);
                foreach (int afterPage in rules[update[i]])
                {
                    if (beforePages.Contains(afterPage))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static int GetMiddlePage(int[] update) {
        return update[update.Length / 2];
    }
    public static string Solve(IEnumerable<String> input) {
        Parse(input);
        int result = 0;
        foreach (var update in updates) {
            if (IsInvalid(update)) {
                List<int> updateList = update.ToList();
                updateList.Sort((left, right) => {
                    if (rules.ContainsKey(left) && rules[left].Contains(right)) {
                        return -1;
                    }
                    if (rules.ContainsKey(right) && rules[right].Contains(left)) {
                        return 1;
                    }
                    return 0;});
                result += GetMiddlePage(updateList.ToArray());
            }
        }
        return result.ToString();
    }
}