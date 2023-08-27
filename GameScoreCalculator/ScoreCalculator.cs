namespace GameScoreCalculator
{
    public class ScoreCalculator : IScoreCalculator
    {
        public List<int> ShowScore(IList<string> input)
        {
            List<int> actualResults = new(input.Count);
            foreach (var t in input)
            {
                var score = t.ToUpper();
                actualResults.Add(score switch
                {
                    "X" => 10,
                    _ => int.TryParse(score, out var numScore) && numScore <= 10 && numScore >= 0
                        ? numScore 
                        : throw new IncorrectInputException("Incorrect input. Please check your score-list.")
                });
            }
            
            return actualResults;
        }
    }
}
