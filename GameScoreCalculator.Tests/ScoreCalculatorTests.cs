    using NUnit.Framework;

    namespace GameScoreCalculator.Tests
    {
        public class ScoreCalculatorTests
        {
            private IScoreCalculator? _scoreCalculator;

            private static IEnumerable<string[]?> TestCasesForShowScoreIncorrectInput
            {
                get
                {
                    yield return new[] { "11" };
                    yield return new[] { "A" };
                }
            }

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
            
            [Test]
            [TestCaseSource(nameof(TestCasesForShowScoreIncorrectInput))]
            public void ShowScoreIncorrectInput(string[] input)
            {
                // Arrange
                var inputList = new List<string>(input);

                // Act & Assert
                Assert.Throws<Exception>(() => _scoreCalculator!.ShowScore(inputList)); // Todo CustomException
            }
        }
    }