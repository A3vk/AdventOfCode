﻿namespace AdventOfCode.Core.Utils;

public static class Calculations
{
    public static long LeastCommonMultiple(long a, long b)
    {
        return a * b / GreatestCommonDivisor(a, b);
    }

    public static long GreatestCommonDivisor(long a, long b)
    {
        return b == 0 ? a : GreatestCommonDivisor(b, a % b);
    }
}