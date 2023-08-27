namespace GameScoreCalculator
{
    public class ScoreCalculator : IScoreCalculator
    {
        private const string strikeMark = "X";

        public List<int> ShowScore(IList<string> input)
        {
            List<int> actualResults = new(input.Count);
            for (var i = 0; i < input.Count; i++)
            {
                var score = input[i].ToUpper();
                var numScore = ScoreToNumeral(score);

                if (i >= 1 && input[i - 1].Equals(strikeMark, StringComparison.OrdinalIgnoreCase))
                {
                    actualResults[^1] += numScore;
                }

                actualResults.Add(numScore);
            }

            return actualResults;
        }

        private static int ScoreToNumeral(string score)
        {
            return score switch
            {
                strikeMark => 10,
                _ => int.TryParse(score, out var num) && num <= 10 && num >= 0
                    ? num 
                    : throw new IncorrectInputException("Incorrect input. Please check your score-list.")
            };
        }
    }
}
