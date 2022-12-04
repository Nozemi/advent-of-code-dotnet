namespace AdventOfCode.Library.Utilities;

public class VariousUtilities
{
    public static List<int> IntListFromRange(int start, int end)
    {
        var numbers = new List<int>();
        
        for (var i = start; i <= end; i++)
            numbers.Add(i);
        
        return numbers;
    }
}