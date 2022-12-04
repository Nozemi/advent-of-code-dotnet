namespace AdventOfCode.Library.Utilities;

public static class LoopingUtilities
{
    public static void ForEachIndexed<T>(this IEnumerable<T> list, Action<int, T> callback)
    {
        var index = 0;
        foreach (var item in list)
            callback(index++, item);
    }
    
    public static void ForEach<T>(this IEnumerable<T> list, Action<T> callback)
    {
        foreach (var item in list)
            callback(item);
    }
}