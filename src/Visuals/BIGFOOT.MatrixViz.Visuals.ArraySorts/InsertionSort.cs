using BIGFOOT.MatrixViz.DriverInterfacing;
using BIGFOOT.MatrixViz.Visuals.Constants;
using System.Threading;

namespace BIGFOOT.MatrixViz.Visuals.ArraySorts
{
    public class InsertionSort<TMatrix, TCanvas> : Visual<TMatrix, TCanvas> 
        where TMatrix : Matrix<TCanvas> 
        where TCanvas : Canvas
    {
        private static Color COLOR_UNSORTED = MatrixGridTileColors.BLOCK;
        private static Color COLOR_SORTED = MatrixGridTileColors.EMPTY;
        private static Color COLOR_POINTER = MatrixGridTileColors.START;
        private static Color COLOR_BACKGROUND = MatrixGridTileColors.VISITED;


        public InsertionSort(TMatrix matrix) : base(matrix)
        {
        }

        private void FillBackgroundLayer(TCanvas canvas)
        {
            int bound = Matrix.Size * Matrix.Size;
            for (int i = 0; i < bound; i++)
            {
                canvas.SetPixel(i / Matrix.Size, i % Matrix.Size, COLOR_BACKGROUND);
                //canvas.DrawLine(i, 0, bound, bound, COLOR_BACKGROUND);
            }
        }

        public override void VisualizeOnHardware()
        {
            var arr = ArrayUtils.CreateShuffledSequential(64);
            int n = arr.Length;
            var canvas = Matrix.CreateOffscreenCanvas();

            // One by one move boundary of unsorted subarray
            for (int i = 0; i < n - 1; i++)
            {
                // Find the minimum element in unsorted array
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[min_idx])
                    {
                        min_idx = j;
                    }
                    canvas.Clear();
                    for (int k = 0; k < n; k++)
                    {
                        canvas.DrawLine(k, 0, k, arr[k] - 1, k == min_idx || k == j ? COLOR_POINTER : k < i ? COLOR_SORTED : COLOR_UNSORTED);
                        //Console.WriteLine($"Drawing line ({k}, {arr[k] - 1}: x0={k}, y0={0}, x1={k}, y1={arr[k] - 1}");
                    }
                    canvas = Matrix.SwapOnVsync(canvas);
                    Thread.Sleep(TickMs);
                }
                // Swap the found minimum element with the first
                // element
                int temp = arr[min_idx];
                arr[min_idx] = arr[i];
                arr[i] = temp;
            }

            canvas.Clear();
            for (int k = 0; k < arr.Length; k++)
            {
                canvas.DrawLine(k, 0, k, arr[k] - 1, COLOR_SORTED);
            }
            canvas = Matrix.SwapOnVsync(canvas);
        }

        public override void VisualizeVirtually()
        {
            var arr = ArrayUtils.CreateShuffledSequential(64);
            int n = arr.Length;
            var canvas = Matrix.InterfacedCreateOffscreenCanvas();
            var sleepMs = 1;

            // One by one move boundary of unsorted subarray
            for (int i = 0; i < n - 1; i++)
            {
                // Find the minimum element in unsorted array
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[min_idx])
                    {
                        min_idx = j;
                    }
                    canvas.Clear();
                    for (int k = 0; k < n; k++)
                    {
                        canvas.DrawLine(k, 0, k, n-1, COLOR_BACKGROUND);
                        canvas.DrawLine(k, 0, k, arr[k] - 1, k == min_idx || k == j ? COLOR_POINTER : k < i ? COLOR_SORTED : COLOR_UNSORTED);
                        //Console.WriteLine($"Drawing line ({k}, {arr[k] - 1}: x0={k}, y0={0}, x1={k}, y1={arr[k] - 1}");
                    }
                    canvas = Matrix.InterfacedSwapOnVsync(canvas);
                    Thread.Sleep(TickMs);
                }
                // Swap the found minimum element with the first
                // element
                int temp = arr[min_idx];
                arr[min_idx] = arr[i];
                arr[i] = temp;
            }

            canvas.Clear();
            for (int k = 0; k < arr.Length; k++)
            {
                canvas.DrawLine(k, 0, k, arr[k] - 1, COLOR_SORTED);
            }
            canvas = Matrix.InterfacedSwapOnVsync(canvas);
        }
    }
}
