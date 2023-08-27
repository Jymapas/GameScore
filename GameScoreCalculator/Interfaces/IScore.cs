namespace GameScoreCalculator.Interfaces
{
    public interface IScore
    {
        public int Id { get; }
        public int FirstThrow { get; }

        public int? SecondThrow { get; }
        public int Score { get; }
        public int OwnScore { get; }
    }
}
