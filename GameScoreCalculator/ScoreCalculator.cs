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
                    _ => int.Parse(score)
                });
            }
            
            return actualResults;
        }
    }
}
