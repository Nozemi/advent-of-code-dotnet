namespace AdventOfCode.Puzzles.Year2022.Day01;

public class Elf
{
    public Elf(int id)
    {
        Id = id;
    }

    public int Id { get; }
    public List<int> Calories { get; } = new();
}