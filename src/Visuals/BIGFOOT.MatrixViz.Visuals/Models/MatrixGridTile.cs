using BIGFOOT.MatrixViz.Visuals.Enums;
using System;

namespace BIGFOOT.MatrixViz.Visuals.Models
{
    public class MatrixGridTile
    {
        public MatrixGridTileType Type { get; private set; }
        public bool IsEmpty => Type == MatrixGridTileType.EMPTY; 

        public MatrixGridTile(MatrixGridTileType? type = null) =>
            Type = type.HasValue ? type.Value : MatrixGridTileType.EMPTY;
        
        public void ClearTile() => Type = MatrixGridTileType.EMPTY;

        public void PlaceNewTile(MatrixGridTileType type)
        {
            Type = type;
        }

        private static MatrixGridTileType FromChar(char c)
        {
            switch (c)
            {
                case '#':
                    return MatrixGridTileType.BLOCK;
                case ' ':
                    return MatrixGridTileType.EMPTY;
                case '%':
                    return MatrixGridTileType.START;
                case '$':
                    return MatrixGridTileType.TARGET;
                default:
                    throw new Exception($"Could not parse map builder from character: {c}");
            }
        }
    }
}
