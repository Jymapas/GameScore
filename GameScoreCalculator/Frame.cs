using GameScoreCalculator.Interfaces;

namespace GameScoreCalculator;


public class Frame : IScore
{
    public int Id { get; }
    public int FirstThrow { get; set; }

    public int? SecondThrow { get; set; }

    public int Bonus { get; set; }

    public bool IsStrike { get; set; }
    public bool IsSpare => FirstThrow + (SecondThrow ?? 0) == 10;

    public int OwnScore => FirstThrow + (SecondThrow ?? 0) + Bonus;

    public Frame? Previous { get; }

    public int Score => Previous.Score + OwnScore;

    public Frame(int firstThrow, Frame? previous)
    {
        FirstThrow = firstThrow;
        Previous = previous;
        Id = previous?.Id + 1 ?? 1;
        if (FirstThrow == 10) IsStrike = true;
    }
}