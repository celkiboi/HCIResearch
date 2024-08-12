using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Extensions
{
    public static double StdDev<T>(this IEnumerable<T> values)
    {
        if (values.Count() == 0)
        {
            return 0;
        }

        IEnumerable<double> doubleValues = values.Select(x => Convert.ToDouble(x));

        double average = doubleValues.Average();
        int count = doubleValues.Count();
        double stdSum = doubleValues.Sum(x => (x - average) * (x - average));

        return Math.Sqrt(stdSum / count);
    }

    public static double Median<T>(this IEnumerable<T> values)
    {
        if (values.Count() == 0) 
        {
            return 0;
        }

        IEnumerable<double> doubleValues = values.Select(x => Convert.ToDouble(x)).OrderBy(x => x);
        
        int count = doubleValues.Count();
        int midpoint = count / 2;
        if (count % 2 == 0)
            return (doubleValues.ElementAt(midpoint - 1) + doubleValues.ElementAt(midpoint)) / 2.0;
        return (doubleValues.ElementAt(midpoint));
    }
}
