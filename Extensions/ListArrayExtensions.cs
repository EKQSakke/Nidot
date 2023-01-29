using System;
using System.Collections.Generic;

public static class ListArrayExtensions {
    public static T RandomElement<T>(this List<T> list)
    {
        var count = list.Count;
        if (count == 0) return default;

        var rnd = new Random();
        var i = rnd.Next(0, count);
        return list[i];
    }

    public static T RandomElement<T>(this T[] list)
    {
        var count = list.Length;
        if (count == 0) return default;

        var rnd = new Random();
        var i = rnd.Next(0, count);
        return list[i];
    }
}