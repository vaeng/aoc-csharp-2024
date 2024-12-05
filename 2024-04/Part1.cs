using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public static class Part1
{
    public static int rows;
    public static int cols;
    public static int Check(List<string> input, int i1, int j1, int i2, int j2, int i3, int j3) {
        if(i3 < 0 || j3 < 0 || i3 >= rows || j3 >= cols  ) return 0;
        if(input[i1][j1] != 'M') return 0;
        if(input[i2][j2] != 'A') return 0;
        if(input[i3][j3] != 'S') return 0;
        return 1;
     }
    public static int Count(List<String> input, int i, int j ) {
        int result = 0;

        result += Check( input, i-1, j, i-2, j, i-3, j);
        result += Check( input, i+1, j, i+2, j, i+3, j);
        result += Check( input, i-1, j-1, i-2, j-2, i-3, j-3);
        result += Check( input, i-1, j+1, i-2, j+2, i-3, j+3);
        result += Check( input, i+1, j-1, i+2, j-2, i+3, j-3);
        result += Check( input, i+1, j+1, i+2, j+2, i+3, j+3);
        result += Check( input, i, j-1, i, j-2, i, j-3);
        result += Check( input, i, j+1, i, j+2, i, j+3);
        return result;
    }
    public static string Solve(List<String> input)
    {
        int result = 0;
        cols = input[0].Length;
        rows = input.Count;

        Console.WriteLine(rows + " " + cols);

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (input[i][j] == 'X') {
                    result += Count(input, i, j);
                }
            }
        }

        return result.ToString();
    }
}