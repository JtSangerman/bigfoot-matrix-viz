using BIGFOOT.RGBMatrix.MatrixTypes.ColoredConsole;
using BIGFOOT.RGBMatrix.MatrixTypes.InterfacedRGBLed;
using BIGFOOT.RGBMatrix.Visuals.ArraySorts;
using BIGFOOT.RGBMatrix.Visuals.Maze;
using System;

namespace BIGFOOT.RGBMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var opt = Console.ReadKey().Key;
            if (opt == ConsoleKey.D1)
            {
                var matrix = new ColoredConsoleMatrix(32);
                var bubble = new BubbleSort<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix);
                bubble.Visualize();
            }
            else if (opt == ConsoleKey.D2)
            {
                var matrix = new ColoredConsoleMatrix(32);
                var insertion = new InsertionSort<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix);
                insertion.Visualize();
            }
            else if (opt == ConsoleKey.D3)
            {
                var matrix = new FastConsoleMatrix(32);
                var bubble = new InsertionSort<FastConsoleMatrix, FastConsoleCanvas>(matrix);
                bubble.Visualize();
            }
            else if (opt == ConsoleKey.D4)
            {
                var matrix = new ColoredConsoleMatrix(32);
                var maze = new MazeHolder<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix, 4, 4, 100);
                maze.Visualize();
            }
            else
            {
                var hardwareMatrix = new InterfacedRGBLedMatrix(32, 1, 1);
                var bubble = new BubbleSort<InterfacedRGBLedMatrix, InterfacedRGBLedCanvas>(hardwareMatrix);
                bubble.Visualize();
            }
        }
    }
}
