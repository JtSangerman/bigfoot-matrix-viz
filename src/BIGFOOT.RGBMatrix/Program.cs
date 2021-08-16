using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using BIGFOOT.RGBMatrix.Visuals.ArraySorts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace BIGFOOT.RGBMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var opt = Console.ReadKey().Key;
            if (opt == ConsoleKey.D1)
            {
                Maze();
            }
            else if (opt == ConsoleKey.D2)
            {
                Bubble();
            }
            else if (opt == ConsoleKey.D3)
            {
                Insertion();
            } 
            else
            {
                Matrix();
            }
        }

        private static void Matrix()
        {
            var matrix = new RGBLedMatrix(32, 2);
            var canvas = matrix.CreateOffscreenCanvas();
            var rnd = new Random();
            var points = new List<Point>();
            var recycled = new Stack<Point>();
            int frame = 0;
            var stopwatch = new Stopwatch();

            const int MAX_HEIGHT = 16;
            const int COLOR_STEP = 15;
            const int FRAME_STEP = 1;

            while (!Console.KeyAvailable)
            {
                stopwatch.Restart();

                frame++;

                if (frame % FRAME_STEP == 0)
                {
                    if (recycled.Count == 0)
                        points.Add(new Point(rnd.Next(0, 32 - 1), 0));
                    else
                    {
                        var point = recycled.Pop();
                        point.x = rnd.Next(0, 32 - 1);
                        point.y = 0;
                        point.recycled = false;
                    }
                }

                canvas.Clear();

                foreach (var point in points)
                {
                    if (!point.recycled)
                    {
                        point.y++;

                        if (point.y - MAX_HEIGHT > 32)
                        {
                            point.recycled = true;
                            recycled.Push(point);
                        }

                        for (var i = 0; i < MAX_HEIGHT; i++)
                        {
                            canvas.SetPixel(point.x, point.y - i, new Color(0, 255 - i * COLOR_STEP, 0));
                        }
                    }
                }

                canvas = matrix.SwapOnVsync(canvas);

                // force 30 FPS
                var elapsed = stopwatch.ElapsedMilliseconds;
                if (elapsed < 33)
                {
                    Thread.Sleep(33 - (int)elapsed);
                }
            }
        }

        private static void Maze()
        {
          //  MazeHolder curMaze = new MazeHolder(5, 5, 1);
        }

        private static void Bubble()
        {
            var array = ArrayUtils.CreateShuffledSequential(32);
            var sleepMs = 1;

            var matrix = new RGBLedMatrix(32, 1, 1);
            var canvas = matrix.CreateOffscreenCanvas();
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

                        canvas.Clear();
                        for (int k = 0; k < n; k++)
                        {
                            canvas.DrawLine(k, 0, k, array[k] - 1, k == j + 1 ? new Color(123, 0, 0) : k < n - i ? new Color(123, 123, 123) : new Color(0, 0, 123));
                            //Console.WriteLine($"Drawing line ({k}, {array[k] - 1}: x0={k}, y0={0}, x1={k}, y1={array[k] - 1}");
                        }
                        canvas = matrix.SwapOnVsync(canvas);
                        //print(array);
                        Thread.Sleep(sleepMs);
                    }
            }
            canvas.Clear();
            for (int k = 0; k < array.Length; k++)
            {
                canvas.DrawLine(k, 0, k, array[k] - 1, new Color(0, 0, 123));
            }
            canvas = matrix.SwapOnVsync(canvas);
        }

        private static void Insertion()
        {
            var arr = ArrayUtils.CreateShuffledSequential(32);
            int n = arr.Length;
            var sorted = new Color(0, 0, 123);
            var unsorted = new Color(123, 123, 123);
            var processing = new Color(123, 0, 0);
            var matrix = new RGBLedMatrix(32, 1, 1);
            var canvas = matrix.CreateOffscreenCanvas();
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
                    canvas = matrix.SwapOnVsync(canvas);
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
            canvas = matrix.SwapOnVsync(canvas);
        }
    }


    public class RGBLedMatrix
    {
        public RGBLedMatrix(int rows, int chained = 1, int parallel = 1)
        {

        }

        public RGBLedCanvas CreateOffscreenCanvas()
        {
            return new RGBLedCanvas();
        }

        public RGBLedCanvas SwapOnVsync(RGBLedCanvas canvas)
        {
            Console.Clear();
            var grid = canvas.GetGrid();

            // Print rotated 90 degrees
            for (int col = grid.GetLength(1) - 1; col >= 0; col--)
            {
                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    Console.ForegroundColor = grid[row, col];
                    Console.Write("#");
                }
                Console.Write("\n");
            }

            //for (var i = 0; i < grid.GetLength(0); i++)
            //{
            //    for (var j = 0; j < grid.GetLength(1); j++)
            //    {
            //        Console.ForegroundColor = grid[i, j];
            //        Console.Write("#");
            //    }
            //    Console.Write("\n");
            //}

            return canvas;
        }
    }

    public class RGBLedCanvas
    {
        private ConsoleColor[,] _grid;
        public RGBLedCanvas()
        {
            _grid = new ConsoleColor[32, 32];
            Clear();
        }

        public void DrawLine(int x0, int y0, int x1, int y1, Color color)
        {
            for (var x = x0; x <= x1; x++)
            {
                for (var y = y0; y <= y1; y++)
                {
                    _grid[x, y] = GetConsoleColor(color);
                }
            }
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (
                (
                    x >= 0,
                    y >= 0,
                    x < _grid.GetLength(0),
                    y < _grid.GetLength(1)
                ) 
                == (true, true, true, true)
            )
                _grid[x, y] = GetConsoleColor(color);
        }
         
        public void Clear()
        {
            for (var i = 0; i < _grid.GetLength(0); i++)
            {
                for (var j = 0; j < _grid.GetLength(1); j++)
                {
                    _grid[i, j] = ConsoleColor.Black;
                }
            }
            Console.Clear();
        }

        public ConsoleColor GetConsoleColor(Color c)
        {
            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0;
                index |= (c.R > 64) ? 4 : 0;
                index |= (c.G > 64) ? 2 : 0;
                index |= (c.B > 64) ? 1 : 0;

            return (ConsoleColor)index;
        }

        public ConsoleColor[,] GetGrid()
        {
            return _grid;
        }
    }
}
