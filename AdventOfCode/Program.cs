using AdventOfCode.Puzzles.Year2022.Day02;

var example = new Day02RockPaperScissors(true);
Console.WriteLine($"== Solving {example.GetType().Name} - Example ==");
Console.WriteLine("Part 1: " + example.SolvePart1(await example.ParseInputData()));
Console.WriteLine("Part 2: " + example.SolvePart2(await example.ParseInputData()));

var actual = new Day02RockPaperScissors();
Console.WriteLine($"== Solving {actual.GetType().Name} ==");
Console.WriteLine("Part 1: " + actual.SolvePart1(await actual.ParseInputData()));
Console.WriteLine("Part 2: " + actual.SolvePart2(await actual.ParseInputData()));