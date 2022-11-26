namespace BIGFOOT.MatrixViz.Visuals.Models
{
    // based on (0,0) at the bottom left
    public class MatrixCoordinate
    {
        public int X { internal protected set; get; }
        public int Y { internal protected set; get; }

        public MatrixCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
