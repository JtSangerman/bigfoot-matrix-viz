using BIGFOOT.MatrixViz.Visuals.GridBuilder.Enums;
using System;

namespace BIGFOOT.MatrixViz.Visuals.GridBuilder.Models
{
    public class GridBuilderTile
    {
        public GridBuilderTileType Type { get; private set; }
        public bool IsEmpty => Type == GridBuilderTileType.EMPTY; 

        public GridBuilderTile(GridBuilderTileType? type = null) =>
            Type = type.HasValue ? type.Value : GridBuilderTileType.EMPTY;
        
        public void ClearTile() => Type = GridBuilderTileType.EMPTY;

        public void PlaceNewTile(GridBuilderTileType type)
        {
            if (type == Type)
            {
                ClearTile();
                return;
            }

            Type = type;
        }

        private static GridBuilderTileType FromChar(char c)
        {
            switch (c)
            {
                case '#':
                    return GridBuilderTileType.BLOCK;
                case ' ':
                    return GridBuilderTileType.EMPTY;
                case '%':
                    return GridBuilderTileType.START;
                case '$':
                    return GridBuilderTileType.TARGET;
                default:
                    throw new Exception($"Could not parse map builder from character: {c}");
            }
        }
    }
}
