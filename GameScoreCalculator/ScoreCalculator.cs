namespace GameScoreCalculator
{
    public class ScoreCalculator : IScoreCalculator
    {
        private const string strikeMark = "X";

        public List<int> ShowScore(IList<string> input)
        {
            var actualResults = GetFramesByScore(input);

            AddBonuses(actualResults);

            var frames = actualResults.Count > 10 ? actualResults.GetRange(0, 10) : actualResults;
            return frames.Select(x => x.OwnScore).ToList();
        }

        private static List<Frame> GetFramesByScore(IList<string> input)
        {
            const int frameCap = 12;
            List<Frame> actualResults = new(frameCap);
            for (var i = 0; i < input.Count; i++)
            {
                var frame = i > 0 ? actualResults[^1] : null;
                var score = input[i].ToUpper();
                var numScore = ScoreToNumeral(score);

                if (i == 0 || actualResults[^1].IsStrike || actualResults[^1].SecondThrow != null)
                {
                    actualResults.Add(new Frame(numScore, frame));
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
                var frame = actualResults[i];

                if (frame.Previous!.IsSpare) frame.Previous.Bonus += frame.FirstThrow;

                if (!frame.Previous.IsStrike)
                    continue;

                if (frame.IsStrike && i < actualResults.Count - 1 )
                    frame.Previous.Bonus += actualResults[i + 1].FirstThrow;

                frame.Previous.Bonus += frame.SecondThrow ?? 0;
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

            public int OwnScore => FirstThrow + (SecondThrow ?? 0) + Bonus;

            public Frame? Previous { get; }

            public int Score => Previous.Score + OwnScore;

            public Frame(int firstThrow, Frame? previous)
            {
                FirstThrow = firstThrow;
                Previous = previous;
                if (FirstThrow == 10) IsStrike = true;
            }
        }
    }
}
