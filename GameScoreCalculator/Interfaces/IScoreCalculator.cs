namespace GameScoreCalculator.Interfaces
{
    public interface IScoreCalculator
    {
        List<IScore> ShowScore(IList<string> input);
    }
}
