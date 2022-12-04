using AdventOfCode.Library;
using AdventOfCode.Library.Extensions;
using Microsoft.Extensions.Configuration;

namespace AdventOfCode.Puzzles.Year2022.Day03;

public class Day03RucksackReorganization : Puzzle
{
    public Day03RucksackReorganization(IConfiguration config) : base(config)
    {
    }
    
    public override int Year() => 2022;
    public override int Day() => 3;

    private async Task<List<Rucksack>> ParseInputData(bool exampleData = false)
        => (await RawInput(exampleData))
            .Select(line => new Rucksack(line))
            .ToList();

    public override async Task<long> SolvePart1(bool exampleData = false)
        => (await ParseInputData(exampleData)).Select(rucksack =>
            rucksack.Compartments[0].Intersect(rucksack.Compartments[1])
        ).Sum(duplicate => duplicate.Sum());

    public override async Task<long> SolvePart2(bool exampleData = false)
        => (await ParseInputData(exampleData)).ToGroupsOfSize(3).Select(group =>
        {
            var result = new List<int>();
            group.ForEach(elf =>
            {
                result = result.IsEmpty()
                    ? elf.CompartmentsJoined()
                    : result.Intersect(elf.CompartmentsJoined()).ToList();
            });

            return result;
        }).Sum(it => it.Sum());
}