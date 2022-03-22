using BIGFOOT.RGBMatrix.Visuals.Inputs;
using rpi_rgb_led_matrix_sharp;
using System;
using System.Threading;

namespace BIGFOOT.RGBMatrix.Visuals.ArraySorts
{
    public class BubbleSort : Visual
    {
        public BubbleSort(RGBLedMatrix matrix, ControllerInputDriverBase controller = null) : base(matrix) 
        {

        }

        private RGBLedCanvas _canvas;

        public override void Run()
        {
            var array = ArrayUtils.CreateShuffledSequential(Matrix.GetCanvas().Width);
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
                        Thread.Sleep(sleepMs);
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
