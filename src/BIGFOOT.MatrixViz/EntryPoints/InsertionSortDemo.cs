using BIGFOOT.MatrixViz.MatrixTypes.Direct2D;
using BIGFOOT.MatrixViz.Visuals.ArraySorts;
using System.Threading.Tasks;

namespace BIGFOOT.MatrixViz.EntryPoints
{
    internal class InsertionSortDemo
    {
        static async Task Main(string[] args)
        {
            var insertionSort = new InsertionSort<Direct2DMatrix, Direct2DCanvas>(new Direct2DMatrix(64));
            await Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(insertionSort, 15).ConfigureAwait(false);
        }
    }
}
