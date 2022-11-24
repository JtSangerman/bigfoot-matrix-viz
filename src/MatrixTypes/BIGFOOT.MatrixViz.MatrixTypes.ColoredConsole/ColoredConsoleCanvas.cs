using BIGFOOT.MatrixViz.DriverInterfacing;
using System;

namespace BIGFOOT.MatrixViz.MatrixTypes.ColoredConsole
{
    public class ColoredConsoleCanvas : Canvas
    {
        private ConsoleColor[,] _grid;
        public ColoredConsoleCanvas(int size)
        {
            //int bufWidth = 2 * size;
            //int bufHeight = size + 1;

            //Console.SetWindowSize(bufWidth, bufHeight);
            //Console.BufferHeight = bufHeight;
            //Console.BufferWidth = bufWidth;
            Console.WindowWidth = size * 2 + 8;
            _grid = new ConsoleColor[size, size];
            Fill(new Color(0,0,0));
        }

        public void DrawLine(int x0, int y0, int x1, int y1, Color color)
        {
            var cc = ToConsoleColor(color);
            for (var x = x0; x <= x1; x++)
            {
                for (var y = y0; y <= y1; y++)
                {
                    _grid[x, y] = cc;
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
                _grid[x, y] = ToConsoleColor(color);
        }

        public void Clear()
        {
            Fill(new Color(0,0,0));
            //Console.Write(string.Join("", Enumerable.Repeat("\n", 64).ToList()));
            Console.Clear();
        }

        public void Fill(Color color)
        {
            var cc = ToConsoleColor(color);
            for (var i = 0; i < _grid.GetLength(0); i++)
            {
                for (var j = 0; j < _grid.GetLength(1); j++)
                {
                    _grid[i, j] = cc;
                }
            }
        }

        public void DrawCircle(int x0, int y0, int radius, Color color)
        {

        }

        public void Display()
        {
            Console.Clear();

            // Print rotated 90 degrees
            for (int col = _grid.GetLength(1) - 1; col >= 0; col--)
            {
                for (int row = 0; row < _grid.GetLength(0); row++)
                {
                    Console.ForegroundColor = _grid[row, col];
                    Console.Write("[]");
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
        }

        private ConsoleColor ToConsoleColor(Color c)
        {
            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0;
                index |= (c.R > 64) ? 4 : 0;
                index |= (c.G > 64) ? 2 : 0;
                index |= (c.B > 64) ? 1 : 0;

            return (ConsoleColor)index;
        }
    }
}
