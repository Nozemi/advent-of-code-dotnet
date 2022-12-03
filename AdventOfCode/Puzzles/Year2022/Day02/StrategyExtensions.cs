using static AdventOfCode.Puzzles.Year2022.Day02.Day02RockPaperScissors;

namespace AdventOfCode.Puzzles.Year2022.Day02;

public static class StrategyExtensions
{
    public static Strategy GetCounterWin(this Strategy strategy)
        => strategy switch
        {
            Strategy.Rock => Strategy.Paper,
            Strategy.Paper => Strategy.Scissors,
            Strategy.Scissors => Strategy.Rock,
            _ => throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null)
        };

    public static Strategy GetCounterLoss(this Strategy strategy)
        => strategy switch
        {
            Strategy.Rock => Strategy.Scissors,
            Strategy.Paper => Strategy.Rock,
            Strategy.Scissors => Strategy.Paper,
            _ => throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null)
        };
}