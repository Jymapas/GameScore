using GameScoreCalculator.Helpers;
using GameScoreCalculator.Interfaces;

namespace GameScoreCalculator
{
    public class ScoreCalculator : IScoreCalculator
    {
        public List<IFrameScore> ShowScore(IList<string> input)
        {
            var actualResults = ScoreParser.ParseBowlingFrames(input);

            AddBonuses(actualResults);

            return new List<IFrameScore>(actualResults.Count > 10 ? actualResults.GetRange(0, 10) : actualResults);
        }


        private static void AddBonuses(List<Frame> actualResults)
        {
            for (var i = actualResults.Count - 1; i > 0; i--)
            {
                var frame = actualResults[i];

                if (frame.Previous!.IsSpare)
                {
                    frame.Previous.Bonus += frame.FirstThrow;
                }

                if (!frame.Previous.IsStrike)
                {
                    continue;
                }

                if (frame.IsStrike && i < actualResults.Count - 1)
                {
                    frame.Previous.Bonus += actualResults[i + 1].FirstThrow;
                }

                frame.Previous.Bonus += frame.SecondThrow ?? 0;
            }
        }
    }
}
