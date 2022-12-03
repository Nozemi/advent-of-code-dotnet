using AdventOfCode.Library;

namespace AdventOfCode.Puzzles.Year2022.Day02;

public class Day02RockPaperScissors : Puzzle<List<string[]>>
{
    public Day02RockPaperScissors(bool exampleMode = false) : base(2022, 2, exampleMode)
    {
    }

    private static readonly Dictionary<string, Strategy> Strategies = new()
    {
        { "A", Strategy.Rock },
        { "B", Strategy.Paper },
        { "C", Strategy.Scissors }
    };

    private static readonly Dictionary<Enum, int> Scores = new()
    {
        { Strategy.Rock, 1 },
        { Strategy.Paper, 2 },
        { Strategy.Scissors, 3 },
        { RoundResult.Win, 6 },
        { RoundResult.Draw, 3 },
        { RoundResult.Loss, 0 }
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

    public override async Task<List<string[]>> ParseInputData()
        => (await RawInput()).Select(round => round.Split(" "))
            .ToList();

    private RoundResult GetResultOfRound(IReadOnlyList<string> round)
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

    public override string SolvePart1(List<string[]> parsedInput)
        => parsedInput.Sum(round =>
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
        ) + "";
    
    public override string SolvePart2(List<string[]> parsedInput)
        => parsedInput.Where(round => round[1] == "X")
               .Sum(round => Scores[Strategies[round[0]].GetCounterLoss()])
           + parsedInput.Where(round => round[1] == "Y")
               .Sum(round => Scores[Strategies[round[0]]] + 3)
           + parsedInput.Where(round => round[1] == "Z")
               .Sum(round => Scores[Strategies[round[0]].GetCounterWin()] + 6) + "";
}