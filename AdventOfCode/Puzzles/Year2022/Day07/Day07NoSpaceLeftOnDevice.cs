using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Library.Puzzle;

namespace AdventOfCode.Puzzles.Year2022.Day07;

[Puzzle(Year = 2022, Day = 7)]
public class Day07NoSpaceLeftOnDevice : Puzzle
{
    public Day07NoSpaceLeftOnDevice() : base(
        solutions: new[]
        {
            Solution("Part 1", Solve1),
            Solution("Part 2", Solve2)
        })
    {
    }

    private static object Solve1(IEnumerable<string> input)
        => input.ParseInput().AllDirectories.FindAll(dir => dir.TotalSize() <= 100_000 && dir.TotalSize() > 0)
            .Sum(dir => dir.TotalSize());

    private static object Solve2(IEnumerable<string> input)
    {
        var dataStructure = input.ParseInput();
        const int totalDiskSpace = 70_000_000;
        const int neededFreeSpace = 30_000_000;
        var usedSpace = dataStructure.RootDirectory.TotalSize();
        var remainingSpace = totalDiskSpace - usedSpace;
        var needToDelete = neededFreeSpace - remainingSpace;

        return dataStructure.AllDirectories.FindAll(dir => dir.TotalSize() >= needToDelete)
            .MinBy(dir => dir.TotalSize())!.TotalSize();
    }
}

public class ObjectStructure
{
    public List<MyObject> AllDirectories { get; init; }
    public MyObject CurrentDirectory { get; init; }
    public MyObject RootDirectory { get; init; }
}

public static class Day07Extensions
{
    public static ObjectStructure ParseInput(this IEnumerable<string> input)
    {
        var lines = input.ToList();

        var rootDirectory = new MyObject
        {
            Type = MyType.Directory,
            ObjectName = "/"
        };
        var currentDirectory = rootDirectory;
        var directories = new List<MyObject>();

        foreach (var line in lines)
        {
            if (new Regex("[\\d]+ (.*)").IsMatch(line))
            {
                var file = new MyObject
                {
                    Type = MyType.File,
                    ObjectName = line.Split(" ").Last(),
                    Parent = currentDirectory,
                    Size = long.Parse(line.Split(" ").First())
                };
                currentDirectory.Children.Add(file);
                //Log.Debug("Creating File: {File}", file.ObjectName);
            }

            if (line.StartsWith("dir ") || (line.StartsWith("$ cd ") && line != "$ cd /" && line != "$ cd .."))
            {
                var dirObj = new MyObject
                {
                    Parent = currentDirectory,
                    Type = MyType.Directory,
                    ObjectName = line.Split(" ").Last(),
                    Size = 0
                };

                if (currentDirectory.Children.All(d => d.ObjectName != dirObj.ObjectName))
                {
                    directories.Add(dirObj);
                    currentDirectory.Children.Add(dirObj);
                }
            }

            if (line.Equals("$ cd .."))
                currentDirectory = currentDirectory.Parent ?? rootDirectory;
            else if (line.StartsWith("$ cd /"))
                currentDirectory = rootDirectory;
            else if (line.StartsWith("$ cd "))
            {
                var newLocation = currentDirectory.Children.Find(
                    d => d.Type == MyType.Directory && d.ObjectName == line.Split(" ").Last()
                );

                if (newLocation == null)
                {
                    newLocation = new MyObject
                    {
                        Type = MyType.Directory,
                        ObjectName = line.Split(" ").Last(),
                        Parent = currentDirectory,
                        Size = 0
                    };
                    directories.Add(newLocation);
                    currentDirectory.Children.Add(newLocation);
                    currentDirectory = newLocation;
                }
                else currentDirectory = newLocation;
            }
        }

        return new ObjectStructure
        {
            AllDirectories = directories,
            RootDirectory = rootDirectory,
            CurrentDirectory = currentDirectory
        };
    }
}

public class MyObject
{
    public string ObjectName { get; init; }
    public MyType Type { get; init; }

    public MyObject? Parent { get; init; }

    public List<MyObject> Children { get; } = new();
    public long Size { get; set; }

    public override string ToString()
    {
        var hops = 0;
        var indent = "  ";
        var directory = Type == MyType.Directory ? this : Parent;
        while (directory != null && directory.ObjectName != "/")
        {
            hops++;
            directory = directory.Parent;
        }

        //Log.Debug("Hops ({Name}): {Hops}", ObjectName, hops);
        for (var i = 0; i < hops; i++)
            indent += indent;

        var builder = new StringBuilder();
        builder.Append($"- {ObjectName} ({Type}");
        builder.Append($", size={TotalSize().ToString("N1").Replace(".0", "")}");
        builder.Append(")\n");

        foreach (var myObject in Children)
            builder.Append($"{indent}{myObject}");

        return builder.ToString();
    }

    public long TotalSize() => Size + Children.Sum(child => child.TotalSize());
}

public enum MyType
{
    File,
    Directory
}