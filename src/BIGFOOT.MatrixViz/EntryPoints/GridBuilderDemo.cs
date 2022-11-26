using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;
using BIGFOOT.MatrixViz.MatrixTypes.Direct2D;
using BIGFOOT.MatrixViz.Visuals;
using BIGFOOT.MatrixViz.Visuals.GridBuilder;
using BIGFOOT.MatrixViz.Visuals.Snake;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BIGFOOT.MatrixViz.EntryPoints
{
    internal class GridBuilderDemo
    {
        static async Task Main(string[] args)
        {
            try
            {
                await ExecuteProcessLoop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task ExecuteProcessLoop(CancellationToken cancellationToken = default)
        {
            var matrix = new Direct2DMatrix(64);
            ControllerInputDriverBase input = new KeyboardConsoleDriver();
           
            //var runnable = new Snake<Direct2DMatrix, Direct2DCanvas>(matrix, input);

            var runnable = new GridBuilder<Direct2DMatrix, Direct2DCanvas>(matrix, input);
            //runnable._canvas = matrix.InterfacedGetCanvas();

            await Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(runnable, 500);
        }
    }
}
