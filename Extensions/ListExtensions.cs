namespace Nidot;

public static class ListExtensions
{
    public static bool TryAdd<T>(this List<T> list, T item)
    {
        if (!list.Contains(item))
        {
            list.Add(item);
            return true;
        }

        return false;
    }

    public static bool TryRemove<T>(this List<T> list, T item)
    {
        if (list.Contains(item))
        {
            list.Remove(item);
            return true;
        }

        return false;
    }
}