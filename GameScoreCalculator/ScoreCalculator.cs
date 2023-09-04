﻿using GameScoreCalculator.Interfaces;

namespace GameScoreCalculator
{
    public class ScoreCalculator : IScoreCalculator
    {
        private const string StrikeMark = "X";

        public List<IFrameScore> ShowScore(IList<string> input)
        {
            var actualResults = ParseBowlingFrames(input);

            AddBonuses(actualResults);

            return new List<IFrameScore>(actualResults.Count > 10 ? actualResults.GetRange(0, 10) : actualResults);
        }

        private static List<Frame> ParseBowlingFrames(IList<string> input)
        {
            const int frameCap = 10;
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

                if (actualResults.Count != 10 || actualResults[^1].SecondThrow == null)
                {
                    actualResults[^1].SecondThrow = numScore;
                    continue;
                }

                actualResults[^1].ThirdThrow = numScore;
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

                if (frame.IsStrike && i < actualResults.Count - 1)
                    frame.Previous.Bonus += actualResults[i + 1].FirstThrow;

                frame.Previous.Bonus += frame.SecondThrow ?? 0;
            }
        }

        private static int ScoreToNumeral(string score)
        {
            return score switch
            {
                StrikeMark => 10,
                _ => int.TryParse(score, out var num) && num <= 10 && num >= 0
                    ? num
                    : throw new ArgumentException("Incorrect input. Please check your score-list.")
            };
        }
    }
}
