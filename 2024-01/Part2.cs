using System;


public static class Part2
{
    public static string Solve(IEnumerable<String> input)
    {
        List<int> leftIds = new List<int>();
        List<int> rightIds = new List<int>();

        foreach (var line in input)
        {
            string[] raw = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            leftIds.Add(int.Parse(raw[0]));
            rightIds.Add(int.Parse(raw[1]));
        }

        Dictionary<int, int> rightFreq = new();

        foreach (var group in rightIds.GroupBy(id => id))
        {
            rightFreq.Add(group.Key, group.Count());
        }


        int similarityScore = 0;
        foreach (int leftID in leftIds)
        {
            if(!rightFreq.ContainsKey(leftID)) {continue;}
            similarityScore += leftID * rightFreq[leftID];
        }


        return similarityScore.ToString();
    }
}