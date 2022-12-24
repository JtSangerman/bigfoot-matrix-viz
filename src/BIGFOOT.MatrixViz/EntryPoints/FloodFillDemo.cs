using BIGFOOT.MatrixViz.MatrixTypes.Direct2D;
using BIGFOOT.MatrixViz.Visuals.FloodFill;

namespace BIGFOOT.MatrixViz.EntryPoints
{
    internal class FloodFillDemo
    {
        public static void Main(string[] args)
        {
            var floodFill = new FloodFill<Direct2DMatrix, Direct2DCanvas>(new Direct2DMatrix(64));
        }
    }
}
