namespace GameScoreCalculator
{
    public class ScoreCalculator : IScoreCalculator
    {
        private const string strikeMark = "X";

        public List<int> ShowScore(IList<string> input)
        {
            var actualResults = GetFramesByScore(input);

            AddBonuses(actualResults);

            return actualResults.Select(x => x.Score).ToList();
        }

        private static List<Frame> GetFramesByScore(IList<string> input)
        {
            List<Frame> actualResults = new(input.Count);
            for (var i = 0; i < input.Count; i++)
            {
                var score = input[i].ToUpper();
                var numScore = ScoreToNumeral(score);

                if (i == 0 || actualResults[^1].IsStrike || actualResults[^1].SecondThrow != null)
                {
                    actualResults.Add(new Frame(numScore));
                    continue;
                }

                actualResults[^1].SecondThrow = numScore;
            }

            return actualResults;
        }

        private static void AddBonuses(List<Frame> actualResults)
        {
            for (var i = actualResults.Count - 1; i > 0; i--)
            {
                var previousFrame = actualResults[i - 1];
                var currentFrame = actualResults[i];

                if (previousFrame.IsSpare) previousFrame.Bonus += currentFrame.FirstThrow;

                if (!previousFrame.IsStrike)
                    continue;

                if (currentFrame.IsStrike && i < actualResults.Count - 1 )
                    previousFrame.Bonus += actualResults[i + 1].FirstThrow;

                previousFrame.Bonus += currentFrame.SecondThrow ?? 0;
            }
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

        private class Frame
        {
            public int FirstThrow { get; set; }

            public int? SecondThrow { get; set; }

            public int Bonus { get; set; }

            public bool IsStrike { get; set; }
            public bool IsSpare => FirstThrow + (SecondThrow ?? 0) == 10;

            public int Score => FirstThrow + (SecondThrow ?? 0) + Bonus;

            public Frame(int firstThrow)
            {
                FirstThrow = firstThrow;
                if (FirstThrow == 10) IsStrike = true;
            }
        }
    }
}
