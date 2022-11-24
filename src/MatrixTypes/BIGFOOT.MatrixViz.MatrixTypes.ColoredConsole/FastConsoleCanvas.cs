using BIGFOOT.MatrixViz.DriverInterfacing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BIGFOOT.MatrixViz.MatrixTypes.ColoredConsole
{
    /*
     *  This is console implementation of canvas does not support color for the trade off of being much, much faster
     */
    public class FastConsoleCanvas : Canvas
    {
        private List<Tuple<string, ConsoleColor>> _rows;
        
        public FastConsoleCanvas(int size)
        {
            int bufWidth = 2 * size;
            int bufHeight = size + 1;

            Console.SetWindowSize(bufWidth, bufHeight);
            Console.BufferHeight = bufHeight;
            Console.BufferWidth = bufWidth;

            Console.WindowWidth = size * 2 + 8;

            _rows = Enumerable.Repeat(new Tuple<string, ConsoleColor>("", ConsoleColor.Black), 64).ToList();
        }

        public void DrawLine(int x0, int y0, int x1, int y1, Color color)
        {
            var row = new Tuple<string, ConsoleColor>(string.Join("", Enumerable.Repeat("[]", y1+1)), ToConsoleColor(color));
            _rows.RemoveAt(x0);
            _rows.Insert(x0, row);
        }

        public void SetPixel(int x, int y, Color color)
        {
        }

        public void Clear()
        {
            Fill(new Color(0, 0, 0));
            Console.WriteLine(string.Join("", Enumerable.Repeat("\n", 64)));
        }

        public void Fill(Color color)
        {
            _rows = _rows.Select(r =>
            {
                return new Tuple<string, ConsoleColor>(string.Join("", Enumerable.Repeat("[]", 64)), ToConsoleColor(color));
            }).ToList();
        }

        public void DrawCircle(int x0, int y0, int radius, Color color)
        {

        }

        public void Display()
        {
            _rows.ForEach(r =>
            {
                Console.ForegroundColor = r.Item2;
                Console.WriteLine(r.Item1);
            });
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
