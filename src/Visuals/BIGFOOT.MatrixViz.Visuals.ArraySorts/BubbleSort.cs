using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;
using BIGFOOT.MatrixViz.DriverInterfacing;
using System.Threading;

namespace BIGFOOT.MatrixViz.Visuals.ArraySorts
{
    public class BubbleSort<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        public BubbleSort(TMatrix matrix, ControllerInputDriverBase controller = null) : base(matrix) 
        {

        }

        private TCanvas _canvas;
        public override void VisualizeVirtually()
        {
            _canvas = Matrix.InterfacedGetCanvas();
            var array = ArrayUtils.CreateShuffledSequential(Matrix.Size);

            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                    if (array[j] > array[j + 1])
                    {
                        // swap temp and arr[i]
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                        _canvas.Clear();
                        for (int k = 0; k < n; k++)
                        {
                            _canvas.DrawLine(k, 0, k, array[k] - 1, k == j + 1 ? new Color(123, 0, 0) : k < n - i ? new Color(123, 123, 123) : new Color(0, 0, 123));
                        }
                        _canvas = Matrix.InterfacedSwapOnVsync(_canvas);
                        Thread.Sleep(TickMs);
                    }
            }
            _canvas.Clear();
            for (int k = 0; k < array.Length; k++)
            {
                _canvas.DrawLine(k, 0, k, array[k] - 1, new Color(0, 0, 123));
            }
            _canvas = Matrix.InterfacedSwapOnVsync(_canvas);
         }

        public override void VisualizeOnHardware()
        {
            var array = ArrayUtils.CreateShuffledSequential(Matrix.Size);
            var sleepMs = 1;

            var canvas = Matrix.CreateOffscreenCanvas();
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                        canvas.Clear();
                        for (int k = 0; k < n; k++)
                        {
                            canvas.DrawLine(k, 0, k, array[k] - 1, k == j + 1 ? new Color(123, 0, 0) : k < n - i ? new Color(123, 123, 123) : new Color(0, 0, 123));
                        }
                        canvas = Matrix.SwapOnVsync(canvas);
                        Thread.Sleep(TickMs);
                    }
            }
            canvas.Clear();
            for (int k = 0; k < array.Length; k++)
            {
                canvas.DrawLine(k, 0, k, array[k] - 1, new Color(0, 0, 123));
            }
            canvas = Matrix.SwapOnVsync(canvas);
        }
    }
}
