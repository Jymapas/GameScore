using NUnit.Framework;

namespace GameScoreCalculator.Tests
{
    public class ScoreCalculatorTests
    {
        private IScoreCalculator? _scoreCalculator;

        [SetUp]
        public void Setup()
        {
            _scoreCalculator = new ScoreCalculator();
        }

        [Test]
        [TestCase(new[] {"X"}, new[] {10})]
        public void ShowScoreSuccess(string[] input, int[] expectedScore)
        {
            // Arrange
            var inputList = new List<string>(input);
            var expectedScoreList = new List<int>(expectedScore);

            // Act
            var result = _scoreCalculator!.ShowScore(inputList);

            // Assert
            Assert.That(expectedScoreList, Is.EqualTo(result));
        }
    }
}