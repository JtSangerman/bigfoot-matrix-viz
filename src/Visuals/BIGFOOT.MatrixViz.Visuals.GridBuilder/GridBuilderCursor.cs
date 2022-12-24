namespace BIGFOOT.MatrixViz.Visuals.GridBuilder
{
    internal class GridBuilderCursor
    {
        private sbyte _ticker = 0;
        public bool AsyncIsCurrentlyTransparent => (_ticker = (sbyte) (_ticker + 24)) > 0;
        public int X { internal protected set; get; }
        public int Y { internal protected set; get; }

        public GridBuilderCursor(int matrixSize) 
            => (X, Y) = (matrixSize/2, matrixSize / 2);

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
