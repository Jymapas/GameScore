namespace GameScoreCalculator
{
    public class IncorrectInputException : Exception
    {
        public IncorrectInputException() : base() {}
        public IncorrectInputException(string message) : base(message) { }
    }
}
