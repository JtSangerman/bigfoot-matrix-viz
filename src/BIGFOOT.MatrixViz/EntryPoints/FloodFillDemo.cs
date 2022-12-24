using BIGFOOT.MatrixViz.MatrixTypes.Direct2D;
using BIGFOOT.MatrixViz.Visuals.FloodFill;
using System.Threading.Tasks;

namespace BIGFOOT.MatrixViz.EntryPoints
{
    internal class FloodFillDemo
    {
        static async Task Main(string[] args)
        {
            var floodFill = new FloodFill<Direct2DMatrix, Direct2DCanvas>(new Direct2DMatrix(64));
            await Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(floodFill, 50).ConfigureAwait(false);
        }
    }
}
