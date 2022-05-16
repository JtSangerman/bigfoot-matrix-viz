using rpi_rgb_led_matrix_sharp;
using System;
using System.Threading;
using System.Diagnostics;

namespace minimal_example
{
	class Program
	{
                static void bogoSort(int[] arr, int sleepMs)
            {
		 Stopwatch stopWatch = new Stopwatch();
        	stopWatch.Start();
                int n = arr.Length;
                var sorted = new Color(0, 0, 123);
                var unsorted = new Color(123, 123, 123);
                var processing = new Color(123, 0, 0);
                var matrix = new RGBLedMatrix(32, 1, 1);
                var canvas = matrix.CreateOffscreenCanvas();
		int shuffles = 0;

                var isSorted = false;
                while (!isSorted)
                {
                canvas.Clear();    
		isSorted = true;
                    for (int i = 0; i < n; i++)
                    {
                        canvas.DrawLine(i, 0, i, arr[i] - 1, unsorted);
                    }
                    canvas = matrix.SwapOnVsync(canvas);
                    for (int i = 0; i < n; i++)
                    {

//                        canvas.DrawLine(i, 0, i, arr[i] - 1, processing);
//                        canvas = matrix.SwapOnVsync(canvas);

                        if (i+1 == arr[i])
                        {
//                            canvas.DrawLine(i, 0, i, arr[i] - 1, sorted);
//			    canvas = matrix.SwapOnVsync(canvas);

                        }
                        else
                        {
				++shuffles;
				shuffle(arr);
                            isSorted = false;
                            break;
                        }


                    }

		    canvas = matrix.SwapOnVsync(canvas);

                    
                    Thread.Sleep(sleepMs);
canvas = matrix.SwapOnVsync(canvas);

                }

                canvas.Clear();
                for (int k = 0; k < arr.Length; k++)
                {
                    canvas.DrawLine(k, 0, k, arr[k] - 1, new Color(0, 0, 123));
                }
                canvas = matrix.SwapOnVsync(canvas);

stopWatch.Stop();
        // Get the elapsed time as a TimeSpan value.
        TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine("RunTime " + elapsedTime + ". Number of attempts: " + shuffles);
		}

            static void shuffle(int[] array)
            {
                Random rng = new Random();
                int n = array.Length;
                while (n > 1)
                {
                    int k = rng.Next(n--);
                    int temp = array[n];
                    array[n] = array[k];
                    array[k] = temp;
                }

            }

            static int Main(string[] args)
            {

                 var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                shuffle(arr);
                int sleepMs = 100;

                if (args.Length > 0)
                    sleepMs = Int32.Parse(args[0]);

                bogoSort(arr, sleepMs);
                Thread.Sleep(7500);

                return 0;
            }    }
}
