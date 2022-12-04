using AdventOfCode.Library.Extensions;

namespace AdventOfCode.Puzzles.Year2022.Day03;

public class Rucksack
{
    public static readonly char[] TypesAndCategories =
    {
        '_',

        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z',

        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
        'W', 'X', 'Y', 'Z'
    };

    public Rucksack(List<int[]> compartments)
    {
        Compartments = compartments;
    }

    public Rucksack(string input)
    {
        Compartments = new List<int[]>
        {
            input.ToList().GetRange(0, input.Length / 2)
                .Select(c => TypesAndCategories.IndexOf(c))
                .ToArray(),

            input.ToList().GetRange(input.Length / 2, input.Length / 2)
                .Select(c => TypesAndCategories.IndexOf(c))
                .ToArray()
        };
    }

    public List<int[]> Compartments { get; }

    public List<int> CompartmentsJoined()
        => Compartments.SelectMany(cIndex => cIndex).ToList();

    public override string ToString()
        => string.Join("", CompartmentsJoined().Select(c => TypesAndCategories[c]));
}