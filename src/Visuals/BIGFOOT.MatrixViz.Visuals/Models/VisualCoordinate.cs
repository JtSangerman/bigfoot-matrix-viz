namespace BIGFOOT.MatrixViz.Visuals.Models
{
    // based on (0,0) at the bottum left
    public class VisualCoordinate
    {
        public VisualCoordinate(int x, int y) 
        { 
            X = x;
            Y = y;
        }
        public int X { private set; get; }
        public int Y { private set; get; }

        public void AdjustVert(int amount)
        {
            Y += amount;
        }

        public void AdjustLength(int amount)
        {
            X += amount;
        }
    }
}
