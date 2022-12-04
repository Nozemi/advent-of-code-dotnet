namespace AdventOfCode.Library.Extensions;

public static class ArrayExtensions
{
    public static int IndexOf<T>(this T[] array, T value)
        => Array.IndexOf(array, value);

    public static bool IsEmpty<T>(this IEnumerable<T> list)
        => !list.Any();

    public static List<T> GetAndRemove<T>(this List<T> list, int count)
    {
        var items = list.Take(count).ToList();
        items.ForEach(it => list.Remove(it));

        return items;
    }

    public static List<List<T>> ToGroupsOfSize<T>(this List<T> list, int size)
    {
        var groups = new List<List<T>>();
        
        while (!list.IsEmpty())
            groups.Add(list.GetAndRemove(size));

        return groups;
    }
}