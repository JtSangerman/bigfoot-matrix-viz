using BIGFOOT.MatrixViz.Visuals.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.MatrixViz.Visuals.Models
{
    internal class MatrixGrid
    {
        public readonly GridBuilderTile[,] Grid;
        public MatrixGrid(int size = 64) => Grid = CreateEmptyGrid(size);

        private static GridBuilderTile[,] CreateEmptyGrid(int size)
        {
            var grid = new GridBuilderTile[size, size];
            int bound = size * size;

            for (int i = 0; i < bound; i++)
                grid[i / bound, i % bound] = new GridBuilderTile(GridBuilderTileType.EMPTY);

            return grid;
        }

    }
}
