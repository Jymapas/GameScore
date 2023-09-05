namespace GameScoreCalculator.Interfaces
{
    public interface IFrameScore
    {
        public int Id { get; }
        public int FirstThrow { get; }

        public int? SecondThrow { get; }
        public int Score { get; }
        public int OwnScore { get; }
    }
}
