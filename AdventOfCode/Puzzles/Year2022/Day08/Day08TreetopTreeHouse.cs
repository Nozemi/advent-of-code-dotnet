using AdventOfCode.Library.Puzzle;

namespace AdventOfCode.Puzzles.Year2022.Day08;

public class Day08TreetopTreeHouse : Puzzle
{
    public override int Year() => 2022;
    public override int Day() => 8;

    public override Dictionary<object, Func<IEnumerable<string>, object>> Solutions() => new()
    {
        { "Part 1", Solve1 },
        { "Part 2", Solve2 }
    };

    private static object Solve1(IEnumerable<string> input) => input.ToGrid().VisibleTrees().Count;
    private static object Solve2(IEnumerable<string> input) => input.ToGrid().FindScenicScores().Max();
}

internal static class Day08Extensions
{
    internal static List<List<int>> ToGrid(this IEnumerable<string> input)
    {
        var data = input.ToList();
        var grid = new List<List<int>>();

        for (var y = 0; y < data.Count; y++)
        {
            grid.Add(new List<int>());
            for (var x = 0; x < data.First().Length; x++)
                grid[y].Add(int.Parse(data[y][x] + ""));
        }

        return grid;
    }

    internal static void Draw(this List<List<int>> grid)
    {
        foreach (var row in grid)
        {
            Console.WriteLine();
            foreach (var column in row)
                Console.Write(column);
        }

        Console.WriteLine();
    }

    internal static List<int> FindScenicScores(this List<List<int>> grid)
    {
        var list = new List<int>();
        for (var y = 0; y < grid.Count; y++)
        {
            for (var x = 0; x < grid[y].Count; x++)
            {
                if (y == 0 || y == grid.Count - 1 || x == 0 || x == grid[y].Count - 1)
                    continue;

                var currentTree = grid[y][x];

                var visibleNorth = 1;
                var visibleSouth = 1;
                var visibleWest = 1;
                var visibleEast = 1;

                var checkVertical = y + 1;
                while (checkVertical < grid.Count - 1 && grid[checkVertical][x] < currentTree)
                {
                    checkVertical++;
                    visibleNorth++;
                }

                checkVertical = y - 1;
                while (checkVertical > 0 && grid[checkVertical][x] < currentTree)
                {
                    checkVertical--;
                    visibleSouth++;
                }

                var checkHorizontal = x + 1;
                while (checkHorizontal < grid[y].Count - 1 && grid[y][checkHorizontal] < currentTree)
                {
                    checkHorizontal++;
                    visibleEast++;
                }

                checkHorizontal = x - 1;
                while (checkHorizontal > 0 && grid[y][checkHorizontal] < currentTree)
                {
                    checkHorizontal--;
                    visibleWest++;
                }

                list.Add(visibleNorth * visibleSouth * visibleWest * visibleEast);
                //list.Add(Math.Max(1, visibleNorth) * Math.Max(1, visibleSouth) * Math.Max(1, visibleWest) * Math.Max(1, visibleEast));
            }
        }

        return list;
    }

    internal static List<Tuple<int, int, int>> VisibleTrees(this List<List<int>> grid)
    {
        var visibleTrees = new List<Tuple<int, int, int>>();
        for (var y = 0; y < grid.Count; y++)
        {
            for (var x = 0; x < grid[y].Count; x++)
            {
                if (y == 0 || y == grid.Count - 1 || x == 0 || x == grid[y].Count - 1)
                {
                    visibleTrees.Add(new Tuple<int, int, int>(x, y, grid[y][x]));
                    continue;
                }

                var currentTree = grid[y][x];

                var visibleNorth = true;
                var visibleSouth = true;
                var visibleWest = true;
                var visibleEast = true;

                var checkVertical = y;
                while (checkVertical < grid.Count - 1 && visibleNorth)
                {
                    checkVertical++;
                    if (grid[checkVertical][x] >= currentTree)
                        visibleNorth = false;
                }

                checkVertical = y;
                while (checkVertical > 0 && visibleSouth)
                {
                    checkVertical--;
                    if (grid[checkVertical][x] >= currentTree)
                        visibleSouth = false;
                }

                var checkHorizontal = x;
                while (checkHorizontal < grid[y].Count - 1 && visibleEast)
                {
                    checkHorizontal++;
                    if (grid[y][checkHorizontal] >= currentTree)
                        visibleEast = false;
                }

                checkHorizontal = x;
                while (checkHorizontal > 0 && visibleWest)
                {
                    checkHorizontal--;
                    if (grid[y][checkHorizontal] >= currentTree)
                        visibleWest = false;
                }

                if (visibleEast || visibleWest || visibleNorth || visibleSouth)
                    visibleTrees.Add(new Tuple<int, int, int>(x, y, grid[y][x]));
            }
        }

        return visibleTrees;
    }
}