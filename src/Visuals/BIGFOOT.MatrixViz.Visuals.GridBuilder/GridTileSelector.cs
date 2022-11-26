using BIGFOOT.MatrixViz.Visuals.Models;

namespace BIGFOOT.MatrixViz.Visuals.GridBuilder
{
    internal class GridTileSelector : MatrixCoordinate
    {
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
