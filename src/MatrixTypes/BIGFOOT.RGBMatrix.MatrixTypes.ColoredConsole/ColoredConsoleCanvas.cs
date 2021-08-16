using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using System;
using System.Linq;

namespace BIGFOOT.RGBMatrix.MatrixTypes.ColoredConsole
{
    public class ColoredConsoleCanvas : Canvas
    {
        private ConsoleColor[,] _grid;
        public ColoredConsoleCanvas(int size)
        {
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
            //Console.Write(string.Join("", Enumerable.Repeat("\n", 32).ToList()));
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

        public ConsoleColor[,] GetGrid()
        {
            return _grid;
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
