using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extensions
{
    private static Random rnd;

    public static int GetHighestIndex(this float[] arr)
    {
        float highest = 0;
        int index = 0;
        List<int> indices = new List<int>();

        for (int i = 0; i < arr.Length; i++)
        {
            float item = arr[i];
            if (item > highest)
            {
                highest = item;
                index = i;
                indices.Clear();
                indices.Add(i);
            }
            else if (Math.Abs(item - highest) < 0.1f)
            {
                indices.Add(i);
            }
        }

        return indices.Count < 1 ? index : RandomIndex(indices);
    }

    private static int RandomIndex(IReadOnlyList<int> input)
    {
        return input[Random.Range(0, input.Count)];
    }

    public static bool WithinAngle(this float f, float angle) => Math.Abs(f) <= angle / 2;

    public static float AngleBetweenXZ(this Vector3 p1, Vector3 p2)
    {
        p1 = new Vector3(p1.x, 0, p1.z);
        p2 = new Vector3(p2.x, 0, p2.z);
        return Vector3.Angle(p1, p2);
    }

    public static List<T> ToList<T>(this T[] arr)
    {
        List<T> tmp = new List<T>();

        foreach (var entry in arr)
            tmp.Add(entry);

        return tmp;
    }

    public static List<T> Copy<T>(this List<T> list)
    {
        List<T> output = new List<T>();

        foreach (var item in list)
            output.Add(item);

        return output;
    }

    public static Vector3 Abs(this Vector3 vec) =>
        new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
}
