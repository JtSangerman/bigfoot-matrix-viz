using BIGFOOT.RGBMatrix.MatrixTypes.ColoredConsole;
using BIGFOOT.RGBMatrix.MatrixTypes.Direct2D;
using BIGFOOT.RGBMatrix.MatrixTypes.InterfacedRGBLed;
using BIGFOOT.RGBMatrix.Visuals.ArraySorts;
using BIGFOOT.RGBMatrix.Visuals.Maze;
using System.Windows.Forms;

using System;
using BIGFOOT.RGBMatrix.Visuals;

namespace BIGFOOT.RGBMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            var opt = ConsoleKey.Escape; // Console.ReadKey().Key;
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
            else if (opt == ConsoleKey.D5)
            {
                var hardwareMatrix = new InterfacedRGBLedMatrix(32, 1, 1);
                var bubble = new BubbleSort<InterfacedRGBLedMatrix, InterfacedRGBLedCanvas>(hardwareMatrix);
                bubble.Visualize();
            }
            else
            {
                opt = Console.ReadKey().Key;
                //Application.EnableVisualStyles();
                //Application.SetCompatibleTextRenderingDefault(false);
                ////var matrix = new Direct2DMatrix(32);
                ////var bubble = new BubbleSort<Direct2DMatrix, Direct2DCanvas>(matrix);
                //Application.Run(new Direct2DCanvas(32));

                ////bubble.Visualize();
                ///
                var matrix = new Direct2DMatrix(32);
                Visual<Direct2DMatrix, Direct2DCanvas> v;

                switch (opt)
                {
                    case ConsoleKey.D1: 
                        v = new MazeHolder<Direct2DMatrix, Direct2DCanvas>(matrix, 15, 15, 5);
                        break;
                    case ConsoleKey.D2: 
                        v = new BubbleSort<Direct2DMatrix, Direct2DCanvas>(matrix);
                        break;
                    default:
                        v = new InsertionSort<Direct2DMatrix, Direct2DCanvas>(matrix);
                        break;
                }
                //var bubble = new BubbleSort<Direct2DMatrix, Direct2DCanvas>(matrix);
                Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(v);
                //d2d.Visualise();
            }
        }
    }
}
