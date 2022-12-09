using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Library.Extensions;
using AdventOfCode.Library.Puzzle;
using AdventOfCode.Library.Utilities;

namespace AdventOfCode.Puzzles.Year2022.Day05;

[Puzzle(Year = 2022, Day = 5)]
public class Day05SupplyStacks : Puzzle
{
    public Day05SupplyStacks() : base(
        solutions: new[]
        {
            Solution("Part 1", Solution1),
            Solution("Part 2", Solution2)
        })
    {
    }
    
    private static object Solution1(IEnumerable<string> input) => input.ParseInput(Extensions.CraneMode.Single);
    private static object Solution2(IEnumerable<string> input) => input.ParseInput(Extensions.CraneMode.Multi);
}

public class MoveCommand
{
    public int FromColumn { get; set; }
    public int ToColumn { get; set; }
    public int Count { get; set; }

    public override string ToString()
        => $"move {Count} from {FromColumn} to {ToColumn}";
}

public static class Extensions
{
    private static string PrintStacks(this Dictionary<int, List<char>> stackOverview, Dictionary<int, int> stackColumns)
    {
        var width = stackColumns.Values.Max() + 1;
        var height = stackOverview.Values.MaxBy(list => list.Count)?.Count ?? 0;
        
        var lineBuilder = new StringBuilder();
        for (var i = 0; i < width; i++)
            lineBuilder.Append("   ");

        var content = new List<string>();
        for (var i = 0; i < height; i++)
            content.Add(lineBuilder.ToString());

        foreach (var keyValuePair in stackOverview)
        {
            for (var i = 0; i < keyValuePair.Value.Count; i++)
                content[i] = content[i]
                    .Remove(stackColumns[keyValuePair.Key] - 1, 3)
                    .Insert(stackColumns[keyValuePair.Key] - 1, $"[{keyValuePair.Value[i]}]");
        }
        content.Reverse();
        content.Add(string.Join(" ", stackOverview.Keys.Select(col => $" {col} ")));

        return string.Join("\n", content);
    }
    
    public static string ParseInput(this IEnumerable<string> input, CraneMode mode)
    {
        var commandPattern = new Regex("move ([\\d]+) from ([\\d]+) to ([\\d]+)");

        var inputList = input.ToList();
        var commands = inputList.FindAll(
            line => commandPattern.IsMatch(line)
        ).Select(line =>
        {
            var matches = commandPattern.Match(line).Groups.Values.ToList();

            return new MoveCommand
            {
                FromColumn = int.Parse(matches[2].ToString()),
                ToColumn = int.Parse(matches[3].ToString()),
                Count = int.Parse(matches[1].ToString())
            };
        }).ToList();
        
        var stacks = inputList.FindAll(
            line => !commandPattern.IsMatch(line) && !string.IsNullOrEmpty(line)
        ).ToList();

        var stackNumbers = stacks.Last();
        stacks.Remove(stackNumbers);

        var stackColumns = new Dictionary<int, int>();
        stackNumbers.ToList()
            .ForEachIndexed((index, character) =>
            {
                if (character != ' ')
                    stackColumns.Add(int.Parse(character + ""), index);
            });

        var stackOverview = new Dictionary<int, List<char>>();
        foreach (var keyValuePair in stackColumns)
        {
            stackOverview.Add(keyValuePair.Key, new List<char>());
            foreach (var stack in stacks)
                stackOverview[keyValuePair.Key].Add(stack[keyValuePair.Value]);
            
            stackOverview[keyValuePair.Key] = stackOverview[keyValuePair.Key].FindAll(
                item => new Regex("[A-Z]").IsMatch(item.ToString())
            );
            stackOverview[keyValuePair.Key].Reverse();
        }

        foreach (var command in commands)
            stackOverview.MoveCrates(command, mode);

        var result = stackOverview.Values.Select(value => value.Last()).ToList();
        return string.Join("", result);
    }

    public enum CraneMode
    {
        Multi,
        Single
    }
    
    private static void MoveCrates(this Dictionary<int, List<char>> stackOverview, MoveCommand command, CraneMode mode)
    {
        if (mode == CraneMode.Single) {
            for (var i = 1; i <= command.Count; i++)
            {
                if (stackOverview[command.FromColumn].IsEmpty())
                    continue;
                    
                var target = stackOverview[command.FromColumn].Last();
                stackOverview[command.ToColumn].Add(target);
                stackOverview[command.FromColumn].RemoveAt(stackOverview[command.FromColumn].Count - 1);
            }
        }
        else
        {
            var targets = stackOverview[command.FromColumn].GetRange(
                stackOverview[command.FromColumn].Count - command.Count, command.Count
            );
            stackOverview[command.ToColumn].AddRange(targets);
            stackOverview[command.FromColumn].RemoveRange(stackOverview[command.FromColumn].Count - command.Count, command.Count);
        }
    }
}