using System;
using System.Collections.Generic;
using UnityEngine;

public static class FermatMethod {
    private const string N_MUST_BE_ODD = "N має бути непарним";
    private const string N_MUST_BE_GREATER_THEN_1 = "N має бути більше 1";
    private const int MAX_OPERATION_TIME = 1;
    private const string TOO_MUCH_TIME = "Помилка виконання";

    public static long[] Factorize(long n) {
        if (n % 2 == 0) {
            throw new Exception(N_MUST_BE_ODD);
        }
        
        if (n <= 1) {
            throw new Exception(N_MUST_BE_GREATER_THEN_1);
        }

        var multipliers = new List<long>();
        var sqrts = GetSumOfSquares(n);
        multipliers.Add(Math.Abs(sqrts[0] + sqrts[1]));
        multipliers.Add(Math.Abs(sqrts[0] - sqrts[1]));

        return multipliers.ToArray();
    }

    private static long[] GetSumOfSquares(long n) {
        double x, y;

        x = Math.Ceiling(Math.Sqrt(n));
        y = Math.Pow(x, 2) - n;

        while (Math.Abs(Math.Sqrt(y) - Math.Ceiling(Math.Sqrt(y))) > 0.0001f) {
            x++;
            y = Math.Pow(x, 2) - n;

            if (Time.deltaTime >= MAX_OPERATION_TIME) throw new Exception(TOO_MUCH_TIME);

        }

        return new[] {(long) x, (long) Math.Sqrt(y)};
    }
}