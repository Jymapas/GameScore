namespace GameScoreCalculator.Helpers
{
    public class ScoreParser
    {
        private const string StrikeMark = "X";

        public static List<Frame> ParseBowlingFrames(IList<string> input)
        {
            const int frameCap = 10;
            List<Frame> actualResults = new(frameCap);
            for (var i = 0; i < input.Count; i++)
            {
                ParseFrame(input, i, actualResults);
            }

            return actualResults;
        }

        private static void ParseFrame(IList<string> input, int i, List<Frame> actualResults)
        {
            var frame = i > 0 ? actualResults[^1] : null;
            var score = input[i].ToUpper();
            var numScore = ScoreToNumeral(score);

            if (!ValidateNumeralScore(numScore))
            {
                throw new ArgumentException("Incorrect input. Please check your score-list.");
            }

            var validatedScore = (int)numScore;

            if (i == 0 || actualResults[^1].IsStrike || actualResults[^1].SecondThrow != null)
            {
                actualResults.Add(new Frame(validatedScore, frame));
                return;
            }

            if (actualResults.Count != 10 || actualResults[^1].SecondThrow == null)
            {
                actualResults[^1].SecondThrow = validatedScore;
                return;
            }

            actualResults[^1].ThirdThrow = validatedScore;
        }

        private static int? ScoreToNumeral(string score)
        {
            if (score == StrikeMark)
            {
                return 10;
            }

            if (int.TryParse(score, out var num))
            {
                return num;
            }

            return null;
        }

        private static bool ValidateNumeralScore(int? score)
        {
            return score != null && score >= 0 && score <= 10;
        }
    }
}
