using AdventOfCode.Library;
using AdventOfCode.Puzzles.Year2022.Day01;
using AdventOfCode.Puzzles.Year2022.Day02;
using AdventOfCode.Puzzles.Year2022.Day03;
using AdventOfCode.Puzzles.Year2022.Day04;

var example1 = new Day01CalorieCounting(true);
var actual1 = new Day01CalorieCounting();
Console.WriteLine($"== Solving {actual1.GetType().Name} ==");
Console.WriteLine($"Part 1: {actual1.SolvePart1(await actual1.ParseInputData())} | (Example: {example1.SolvePart1(await example1.ParseInputData())})");
Console.WriteLine($"Part 2: {actual1.SolvePart2(await actual1.ParseInputData())} | (Example: {example1.SolvePart2(await example1.ParseInputData())})");

var example2 = new Day02RockPaperScissors(true);
var actual2 = new Day02RockPaperScissors();
Console.WriteLine($"== Solving {actual2.GetType().Name} ==");
Console.WriteLine($"Part 1: {actual2.SolvePart1(await actual2.ParseInputData())} | (Example: {example2.SolvePart1(await example2.ParseInputData())})");
Console.WriteLine($"Part 2: {actual2.SolvePart2(await actual2.ParseInputData())} | (Example: {example2.SolvePart2(await example2.ParseInputData())})");

var example3 = new Day03RucksackReorganization(true);
var actual3 = new Day03RucksackReorganization();
Console.WriteLine($"== Solving {actual3.GetType().Name} ==");
Console.WriteLine($"Part 1: {actual3.SolvePart1(await actual3.ParseInputData())} | (Example: {example3.SolvePart1(await example3.ParseInputData())})");
Console.WriteLine($"Part 2: {actual3.SolvePart2(await actual3.ParseInputData())} | (Example: {example3.SolvePart2(await example3.ParseInputData())})");

var example4 = new Day04CampCleanup(true);
var actual4 = new Day04CampCleanup();

Console.WriteLine($"== Solving {actual4.GetType().Name} ==");
Console.WriteLine($"Part 1: {actual4.SolvePart1(await actual4.ParseInputData())} | (Example: {example4.SolvePart1(await example4.ParseInputData())})");
Console.WriteLine($"Part 2: {actual4.SolvePart2(await actual4.ParseInputData())} | (Example: {example4.SolvePart2(await example4.ParseInputData())})");
