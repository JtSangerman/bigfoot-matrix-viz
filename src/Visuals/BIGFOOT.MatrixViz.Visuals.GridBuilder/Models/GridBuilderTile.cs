using BIGFOOT.MatrixViz.Visuals.GridBuilder.Enums;
using System;

namespace BIGFOOT.MatrixViz.Visuals.GridBuilder.Models
{
    public class GridBuilderTile
    {
        public GridBuilderTileType Type { get; }
        public bool IsEmpty => Type == GridBuilderTileType.EMPTY; 

        public GridBuilderTile(GridBuilderTileType? type = null)
        {
            Type = type.HasValue ? type.Value : GridBuilderTileType.EMPTY;
        }

        public GridBuilderTile(char c) => Type = FromChar(c);

        private static GridBuilderTileType FromChar(char c)
        {
            switch (c)
            {
                case '#':
                    return GridBuilderTileType.BLOCK;
                case ' ':
                    return GridBuilderTileType.EMPTY;
                default:
                    throw new Exception($"Could not parse map builder from character: {c}");
            }
        }
    }
}
