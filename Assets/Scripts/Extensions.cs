using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

    public static Dictionary<string, string> DeserializeJson(string json)
    {
        json = json.Trim().TrimStart('{').TrimEnd('}').Trim();

        Dictionary<string, string> result = new();

        string[] pairs = json.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (string pair in pairs)
        {
            string[] keyValue = pair.Split(':');

            if (keyValue.Length == 2)
            {
                string key = keyValue[0].Trim().Trim('\"');
                string value = keyValue[1].Trim().Trim('\"');

                result[key] = value;
            }
        }

        return result;
    }

    public static IList<GameObject> GetChildren(this GameObject parent) 
    {
        List<GameObject> children = new();

        foreach (Transform child in parent.transform) 
        {
            children.Add(child.gameObject);
        }

        return children.AsReadOnly();
    }

    public static GameObject GetParent(this GameObject child) 
    { 
        return child.gameObject.transform.parent.gameObject;
    }
}
