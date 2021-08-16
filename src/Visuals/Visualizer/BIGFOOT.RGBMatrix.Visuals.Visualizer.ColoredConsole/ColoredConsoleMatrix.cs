using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.Visuals.Visualizer.ColoredConsole
{
    public class ColoredConsoleMatrix : Matrix<ColoredConsoleCanvas>
    {
        public ColoredConsoleMatrix(int rows, int chained = 1, int parallel = 1) : base(rows, chained, parallel)
        {
            
        }

        public override ColoredConsoleCanvas CreateOffscreenCanvas()
        {
            return new ColoredConsoleCanvas(Size);
        }

        public override ColoredConsoleCanvas SwapOnVsync(ColoredConsoleCanvas canvas)
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

        public override ColoredConsoleCanvas GetCanvas()
        {
            throw new NotImplementedException();
        }

    }
}
