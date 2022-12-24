using BIGFOOT.MatrixViz.MatrixTypes.Direct2D;
using BIGFOOT.MatrixViz.Visuals.Maze;
using System.Threading.Tasks;

namespace BIGFOOT.MatrixViz.EntryPoints
{
    internal class MazeSolverDemo
    {
        static async Task Main(string[] args)
        {
            int tickMs = 1;
            var mazeSolver = new MazeSolver<Direct2DMatrix, Direct2DCanvas>(new Direct2DMatrix(64), tickMs);
            Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(mazeSolver, tickMs).ConfigureAwait(false);
        }
    }
}
