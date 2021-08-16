using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using System;

namespace BIGFOOT.RGBMatrix.MatrixTypes.ColoredConsole
{
    public class ColoredConsoleMatrix : Matrix<ColoredConsoleCanvas>
    {
        public ColoredConsoleMatrix(int rows, int chained_not_yet_supported = 1, int parallel_not_yet_supported = 1) 
            : base(rows, chained_not_yet_supported, parallel_not_yet_supported) { }

        public override ColoredConsoleCanvas InterfacedCreateOffscreenCanvas()
        {
            return new ColoredConsoleCanvas(Size);
        }

        public override ColoredConsoleCanvas InterfacedSwapOnVsync(ColoredConsoleCanvas canvas)
        {
            //Console.Clear();
            var grid = canvas.GetGrid();

            // Print rotated 90 degrees
            for (int col = grid.GetLength(1) - 1; col >= 0; col--)
            {
                for (int row = 0; row < grid.GetLength(0); row++)
                {
                    Console.ForegroundColor= grid[row, col];
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

            return canvas;
        }

        public override ColoredConsoleCanvas InterfacedGetCanvas()
        {
            return new ColoredConsoleCanvas(Size);
        }

        /* The methods below should never be executed, but exist for abstraction compilation.       */
        /* An RGB Led visual can't be ran virtually. This class on the hardware exists differently  */
        /* and these functions will be executed as such there                                       */
        private const string _invalidOpError = "Invalid operation. This Method be explicitly implemented for non-hardware interfaced classes.";
        public override RGBLedCanvas CreateOffscreenCanvas()
        {
            throw new InvalidOperationException(_invalidOpError);
        }
        public override RGBLedCanvas GetCanvas()
        {
            throw new InvalidOperationException(_invalidOpError);
        }
        public override RGBLedCanvas SwapOnVsync(RGBLedCanvas canvas)
        {
            throw new InvalidOperationException(_invalidOpError);
        }
    }
}
