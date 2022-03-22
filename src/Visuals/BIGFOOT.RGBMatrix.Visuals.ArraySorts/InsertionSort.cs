using System.Threading;
using rpi_rgb_led_matrix_sharp;


namespace BIGFOOT.RGBMatrix.Visuals.ArraySorts
{
    public class InsertionSort : Visual
    {
        public InsertionSort(RGBLedMatrix matrix) : base(matrix)
        {
        }

        public override void Run()
        {
            var arr = ArrayUtils.CreateShuffledSequential(32);
            int n = arr.Length;
            var canvas = Matrix.CreateOffscreenCanvas();
            var sleepMs = 2;

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
                        canvas.DrawLine(k, 0, k, arr[k] - 1, k == min_idx || k == j ? new Color(123, 0, 0) : k < i ? new Color(0, 0, 123) : new Color(123, 123, 123));
                        //Console.WriteLine($"Drawing line ({k}, {arr[k] - 1}: x0={k}, y0={0}, x1={k}, y1={arr[k] - 1}");
                    }
                    canvas = Matrix.SwapOnVsync(canvas);
                    Thread.Sleep(sleepMs);
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
                canvas.DrawLine(k, 0, k, arr[k] - 1, new Color(0, 0, 123));
            }
            canvas = Matrix.SwapOnVsync(canvas);
        }
    }
}
