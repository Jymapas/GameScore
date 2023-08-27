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
            [TestCase(new[] { "X", "7" }, new[] { 17, 7 })]
            [TestCase(new[] { "X", "7", "3" }, new[] { 20, 10 })]
            [TestCase(new[] { "X", "X", "X", "X" }, new[] { 30, 30, 20, 10 })]
            [TestCase(new[] { "X", "7", "3", "7" }, new[] { 20, 17, 7 })]
            [TestCase(new[] { "X", "7", "3", "7", "2" }, new[] { 20, 17, 9 })]
            [TestCase(new[] { "X", "7", "3", "1", "2", "9", "1", "X" }, new[] { 20, 11, 3, 20, 10 })]
            [TestCase(new[] { "X", "7", "3", "2", "2", "9", "1", "X", "X", "X" }, new[] { 20, 12, 4, 20, 30, 20, 10 })]
            [TestCase(new[] { "X", "7", "3", "3", "2", "9", "1", "X", "X", "X", "2", "3" }, new[] { 20, 13, 5, 20, 30, 22, 15, 5 })]
            [TestCase(new[] { "X", "7", "3", "4", "2", "9", "1", "X", "X", "X", "2", "3", "6", "4", "7" }, new[] { 20, 14, 6, 20, 30, 22, 15, 5, 17, 7 })]
            [TestCase(new[] { "X", "7", "3", "5", "2", "9", "1", "X", "X", "X", "2", "3", "6", "4", "7", "3" }, new[] { 20, 15, 7, 20, 30, 22, 15, 5, 17, 10 })]
            [TestCase(new[] { "X", "7", "3", "6", "2", "9", "1", "X", "X", "X", "2", "3", "6", "4", "7", "3", "3" }, new[] { 20, 16, 8, 20, 30, 22, 15, 5, 17, 13 })]
        public void ShowScoreSuccess(string[] input, int[] expectedScore)
            {
                // Arrange
                var inputList = new List<string>(input);
                var expectedScoreList = new List<int>(expectedScore);

                // Act
                var result = _scoreCalculator!.ShowScore(inputList);

                // Assert
                Assert.That(result, Is.EqualTo(expectedScoreList));
            }
            
            [Test]
            [TestCaseSource(nameof(TestCasesForShowScoreIncorrectInput))]
            public void ShowScoreIncorrectInput(string[] input)
            {
                // Arrange
                var inputList = new List<string>(input);

                // Act & Assert
                Assert.Throws<IncorrectInputException>(
                    () => _scoreCalculator!.ShowScore(inputList),
                    "Incorrect input. Please check your score-list.");
            }
        }
    }