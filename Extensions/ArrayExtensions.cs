namespace Nidot;

public static class ArrayExtensions
{
    public static T RandomElement<T>(this T[] list)
    {
        var count = list.Length;
        if (count == 0) return default;

        var rnd = new Random();
        var i = rnd.Next(0, count);
        return list[i];
    }
}