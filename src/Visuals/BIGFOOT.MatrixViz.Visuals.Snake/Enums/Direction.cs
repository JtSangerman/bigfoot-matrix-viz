using System.Collections.Generic;

namespace BIGFOOT.MatrixViz.Visuals.Snake.Enums
{
    public enum Direction
    {
        UP              = 0b_0000_0001,
        DOWN            = 0b_0000_0010,
        RIGHT           = 0b_0000_0100,
        LEFT            = 0b_0000_1000,
        HORIZONTALS     = RIGHT | LEFT,
        VERTICALS       = UP | DOWN
    }


    // supports snake
    public static class DirectionalUtils
    {
        public readonly static List<Direction> ConflictingDirectionalSets = new List<Direction>() { Direction.HORIZONTALS, Direction.VERTICALS };

        public static bool CheckConflicted(Direction first, Direction second)
        {
            var bitwiseUnion = first | second;
            return (
                (first ^ second) == 0 ||
                ConflictingDirectionalSets.Contains(bitwiseUnion)
            );
        }
    }
}
