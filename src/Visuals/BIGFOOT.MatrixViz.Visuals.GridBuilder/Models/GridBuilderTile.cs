using BIGFOOT.MatrixViz.Visuals.GridBuilder.Enums;
using BIGFOOT.MatrixViz.Visuals.Models;

namespace BIGFOOT.MatrixViz.Visuals.GridBuilder.Models
{
    public class GridBuilderTile
    {
        public GridBuilderTile(GridBuilderTileType? type = null)
        {
            Type = type.HasValue ? type.Value : GridBuilderTileType.EMPTY;
        }

        public GridBuilderTileType Type { get; internal protected set; }
        public bool IsEmpty { get => Type == GridBuilderTileType.EMPTY; }
        public MatrixCoordinate GridCoordinate { get; private set; }
    }
}
