using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


public static class Part2
{public static int rows;
    public static int cols;
    public static int Check(List<string> input, int i, int j, int im1, int jm1, int im2, int jm2) {
        if(i < 1 || j < 1 || i + 1 >= rows || j + 1 >= cols  ) return 0;
        if(input[i + im1][j + jm1] != 'M') return 0;
        if(input[i + im2][j + jm2] != 'M') return 0;
        if(input[i - im1][j - jm1] != 'S') return 0;
        if(input[i - im2][j - jm2] != 'S') return 0;
        return 1;
     }
    public static int Count(List<String> input, int i, int j ) {
        int result = 0;

        result += Check( input,i, j, -1, -1, -1, +1);
        result += Check( input,i, j, -1, -1, +1, -1);
        result += Check( input,i, j, +1, +1, -1, +1);
        result += Check( input,i, j, +1, +1, +1, -1);
        
        return result;
    }
    public static string Solve(List<String> input)
    {
        int result = 0;
        cols = input[0].Length;
        rows = input.Count;


        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < cols; j++) {
                if (input[i][j] == 'A') {
                    result += Count(input, i, j);
                }
            }
        }

        return result.ToString();
    }
}