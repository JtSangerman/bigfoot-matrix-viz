using BIGFOOT.MatrixViz.DriverInterfacing;
using BIGFOOT.MatrixViz.Visuals.Constants;
using BIGFOOT.MatrixViz.Visuals.Enums;
using System;
using System.Linq;

namespace BIGFOOT.MatrixViz.Visuals.Models
{
    public static class MatrixGridUtility
    {
        public static MatrixGridTile[,] EmptyGrid(int size = 64)
        {
            var grid = new MatrixGridTile[size, size];
            
            int bound = size * size;
            for (int i = 0; i < bound; i++)
                grid[i / size, i % size] = new MatrixGridTile(MatrixGridTileType.EMPTY);

            return grid;
        }

        public static MatrixGridTile[,] Deserialize(string serialized, int size = 64)
        {
            var grid = new MatrixGridTile[size, size];
            
            var lines = serialized.Split('\n');
            for (int i = 0; i < size; i++)
            {
                var line = i < lines.Length ? lines[i] : string.Join("", Enumerable.Repeat(" ", size));
                for (int j = 0; j < size; j++)
                {
                    var c = j < line.Length ? line[j] : ' ';
                    MatrixGridTileType t = c == '#' ? MatrixGridTileType.BLOCK : MatrixGridTileType.EMPTY;
                    grid[j, i] = new MatrixGridTile(t);
                }
            }

            return grid;
        }

        public static string Serialize(MatrixGridTile[,] deserialized, int size = 64)
        {
            string serialized = string.Empty;

            (int b0, int b1) = (deserialized.GetLength(0), deserialized.GetLength(1));
            int bound = b0 * b1;
            
            for (int i = 0; i < bound; i++)
            {
                (int x, int y) = (i / b0, i % b1);

                MatrixGridTileType type = deserialized[y, x].Type;
                switch (type)
                {
                    case MatrixGridTileType.BLOCK:
                        serialized += '#';
                        break;
                    case MatrixGridTileType.EMPTY:
                        serialized += ' ';
                        break;
                    case MatrixGridTileType.START:
                        serialized += '%';
                        break;
                    case MatrixGridTileType.TARGET:
                        serialized += '$';
                        break;
                    default:
                        throw new Exception($"Unknown tile type '{type}' at ({x},{y}))");
                }

                if (y == size - 1)
                {
                    serialized += '\n';
                    continue;
                }
            }

            return serialized;
        }

        public static void DrawGridLayer(Canvas canvas, MatrixGridTile[,] grid, int size = 64)
        {
            int bound = size * size;
            for (int i = 0; i < bound; i++)
            {
                (int x, int y) = (i / size, i % size);
                canvas.SetPixel(x, y, TileTypeColor(grid[x, y].Type));
            }
        }

        private static Color TileTypeColor(MatrixGridTileType tileType)
        {
            switch (tileType)
            {
                case MatrixGridTileType.BLOCK:
                    return MatrixGridTileColors.BLOCK;
                case MatrixGridTileType.START:
                    return MatrixGridTileColors.START;
                case MatrixGridTileType.TARGET:
                    return MatrixGridTileColors.TARGET;
                case MatrixGridTileType.EMPTY:
                    return MatrixGridTileColors.EMPTY;
                case MatrixGridTileType.VISITED:
                    return MatrixGridTileColors.VISITED;
                default:
                    throw new Exception("GridBuilder could not draw unrecognized GridBuilderTileType: " + tileType);
            }
        }

        private static Color ShiftedLuminance(Color color, byte amount)
            => new Color(
                    amount + color.R,
                    amount + color.G,
                    amount + color.B);
    }
}
