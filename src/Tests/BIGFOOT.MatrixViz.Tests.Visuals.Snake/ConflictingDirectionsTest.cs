using BIGFOOT.MatrixViz.Visuals.Snake.Enums;
using Xunit;

namespace BIGFOOT.MatrixViz.Tests.Visuals.Snake
{
    public class ConflictingDirectionsTest
    {
        [Theory]
        [InlineData(Direction.LEFT, Direction.LEFT)]
        [InlineData(Direction.LEFT, Direction.RIGHT)]
        [InlineData(Direction.RIGHT, Direction.RIGHT)]
        public void TestHorizontalConflicts(Direction first, Direction second)
        {
            Assert.True(DirectionalUtils.CheckConflicted(first, second));
        }

        [Theory]
        [InlineData(Direction.UP, Direction.UP)]
        [InlineData(Direction.UP, Direction.DOWN)]
        [InlineData(Direction.DOWN, Direction.DOWN)]
        public void TestVerticalConflicts(Direction first, Direction second)
        {
            Assert.True(DirectionalUtils.CheckConflicted(first, second));
        }
    }
}
