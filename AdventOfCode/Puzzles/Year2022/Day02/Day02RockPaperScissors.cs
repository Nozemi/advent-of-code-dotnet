using AdventOfCode.Library.Puzzle;

namespace AdventOfCode.Puzzles.Year2022.Day02;

[Puzzle(Year = 2022, Day = 2)]
public class Day02RockPaperScissors : Puzzle
{
    public Day02RockPaperScissors() : base(
        solutions: new[]
        {
            Solution("Part 1", Solution1),
            Solution("Part 2", Solution2)
        })
    {
    }
    
    private static readonly Dictionary<string, int> ResultTranslations = new()
    {
        { "X", -1 },
        { "Y", 0 },
        { "Z", 1 }
    };

    private static readonly Dictionary<string, Strategy> Strategies = new()
    {
        { "A", Strategy.Rock },
        { "B", Strategy.Paper },
        { "C", Strategy.Scissors }
    };

    public enum Strategy
    {
        Rock,
        Paper,
        Scissors
    }

    private enum RoundResult
    {
        Win,
        Loss,
        Draw
    }

    private static RoundResult GetResultOfRound(IReadOnlyList<string> round)
    {
        var opponentStrategy = Strategies[round[0]];
        var myStrategy = Strategies[round[1]];

        if (opponentStrategy == myStrategy)
            return RoundResult.Draw;

        if ((opponentStrategy == Strategy.Rock && myStrategy == Strategy.Scissors)
            || (opponentStrategy == Strategy.Paper && myStrategy == Strategy.Rock)
            || (opponentStrategy == Strategy.Scissors && myStrategy == Strategy.Paper))
            return RoundResult.Loss;

        return RoundResult.Win;
    }

    private static readonly Dictionary<Enum, int> Scores = new()
    {
        { Strategy.Rock, 1 },
        { Strategy.Paper, 2 },
        { Strategy.Scissors, 3 },
        { RoundResult.Win, 6 },
        { RoundResult.Draw, 3 },
        { RoundResult.Loss, 0 }
    };

    private static object Solution1(IEnumerable<string> rawInput)
        => rawInput.Select(round => round.Split(" "))
            .ToList().Sum(round =>
                Scores[round[1] switch
                {
                    "X" => Strategy.Rock,
                    "Y" => Strategy.Paper,
                    "Z" => Strategy.Scissors,
                    _ => throw new Exception($"Nope... {round[1]} is not valid...")
                }] + GetResultOfRound(round.Select(r =>
                        r.Replace("X", "A")
                            .Replace("Y", "B")
                            .Replace("Z", "C")
                    ).ToList()) switch
                    {
                        RoundResult.Loss => 0,
                        RoundResult.Draw => 3,
                        RoundResult.Win => 6,
                        _ => throw new ArgumentOutOfRangeException()
                    }
            );


    private static object Solution2(IEnumerable<string> rawInput)
    {
        var score = 0;
        foreach (var round in rawInput)
        {
            var parts = round.Split(" ");
            var mine = parts[1];
            var their = parts[0];

            var neededOutcome = ResultTranslations[mine];

            var mineIndex = Strategies.Keys.ToList().IndexOf(their) + neededOutcome;
            if (mineIndex == -1)
                mineIndex = Strategies.Keys.Count - 1;
            if (mineIndex == Strategies.Keys.Count)
                mineIndex = 0;

            score += mineIndex + 1 + neededOutcome switch
            {
                -1 => 0,
                0 => 3,
                1 => 6,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return score;
    }
}