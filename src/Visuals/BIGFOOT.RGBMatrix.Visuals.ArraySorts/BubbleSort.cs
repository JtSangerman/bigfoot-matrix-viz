using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using BIGFOOT.RGBMatrix.Visuals;

namespace BIGFOOT.RGBMatrix.Visuals.ArraySorts
{
    public class BubbleSort<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
    {
        public BubbleSort(TMatrix matrix, TCanvas canvas) : base(matrix, canvas)
        {
            Sort();
        }

        private void Sort()
        {
            //var array = ArrayUtils.CreateShuffledSequential(32);
            //var sleepMs = 1;

            //var matrix = new RGBLedMatrix(32, 1, 1);
            //var canvas = matrix.CreateOffscreenCanvas();
            //int n = array.Length;
            //for (int i = 0; i < n - 1; i++)
            //{
            //    for (int j = 0; j < n - i - 1; j++)
            //        if (array[j] > array[j + 1])
            //        {
            //            // swap temp and arr[i]
            //            int temp = array[j];
            //            array[j] = array[j + 1];
            //            array[j + 1] = temp;

            //            canvas.Clear();
            //            for (int k = 0; k < n; k++)
            //            {
            //                canvas.DrawLine(k, 0, k, array[k] - 1, k == j + 1 ? new Color(123, 0, 0) : k < n - i ? new Color(123, 123, 123) : new Color(0, 0, 123));
            //                //Console.WriteLine($"Drawing line ({k}, {array[k] - 1}: x0={k}, y0={0}, x1={k}, y1={array[k] - 1}");
            //            }
            //            canvas = matrix.SwapOnVsync(canvas);
            //            //print(array);
            //            Thread.Sleep(sleepMs);
            //        }
            //}
            //canvas.Clear();
            //for (int k = 0; k < array.Length; k++)
            //{
            //    canvas.DrawLine(k, 0, k, array[k] - 1, new Color(0, 0, 123));
            //}
            //canvas = matrix.SwapOnVsync(canvas);
        }
    }
}
