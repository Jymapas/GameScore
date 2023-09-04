namespace GameScoreCalculator.Interfaces
{
    public interface IScoreCalculator
    {
        List<IFrameScore> ShowScore(IList<string> input);
    }
}
