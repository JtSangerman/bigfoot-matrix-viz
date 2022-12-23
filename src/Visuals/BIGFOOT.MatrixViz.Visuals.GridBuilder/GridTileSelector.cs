using BIGFOOT.MatrixViz.DriverInterfacing;
using BIGFOOT.MatrixViz.Visuals.Models;
using System;

namespace BIGFOOT.MatrixViz.Visuals.GridBuilder
{
    internal class GridTileSelector : MatrixCoordinate
    {
        private sbyte _ticker = 0;
        public bool AsyncIsCurrentlyTransparent => (_ticker = (sbyte) (_ticker + 24)) > 0;

        public GridTileSelector(int matrixSize) : base(matrixSize / 2, matrixSize / 2) { }

        public void Move(int xAmount, int yAmount)
        {
            AdjustHeight(yAmount);
            AdjustLength(xAmount);
        }

        private void AdjustHeight(int amount)
        {
            Y += amount;
        }

        private void AdjustLength(int amount)
        {
            X += amount;
        }
    }
}
