using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;
using BIGFOOT.MatrixViz.MatrixTypes.Direct2D;
using BIGFOOT.MatrixViz.Visuals;
using BIGFOOT.MatrixViz.Visuals.GridBuilder;
using BIGFOOT.MatrixViz.Visuals.Maze;
using BIGFOOT.MatrixViz.Visuals.Snake;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIGFOOT.MatrixViz.EntryPoints
{
    internal class GridBuilderDemo
    {
        static async Task Main(string[] args)
        {
            try
            {
                var mazeStr = new SimpleMazeHolder(31, 31).MazeStackToString();
                //var map = await AsyncGenerateMazeString().ConfigureAwait(false);
                var editedMap = await AsyncStartMapEditor(mazeStr).ConfigureAwait(false);
                Console.WriteLine(editedMap);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task<string> AsyncStartMapEditor(string serializedMapStartStr = null, CancellationToken cancellationToken = default)
        {
            var matrix = new Direct2DMatrix(64);
            ControllerInputDriverBase input = new KeyboardConsoleDriver();


            var mapBuilder = new GridBuilder<Direct2DMatrix, Direct2DCanvas>(matrix, input, serializedMapStartStr);
            await Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(mapBuilder, 50).ConfigureAwait(false);
            var finishedGrid = mapBuilder.SerializeMapState();
            Console.WriteLine("edited Grid start: ---");
            Console.WriteLine(finishedGrid);

            Console.WriteLine("--- end:");

            return finishedGrid;
        }


        public static async Task<string> AsyncGenerateMazeString(CancellationToken cancellationToken = default)
        {
            var matrix = new Direct2DMatrix(64);
            var maze = new MazeHolder<Direct2DMatrix, Direct2DCanvas>(matrix, 31, 31, 1);
            await Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(maze, 1).ConfigureAwait(false);
            var mazeStr = maze.SerializedMazeStr;
            Console.WriteLine("maze start: ---");
            Console.WriteLine(mazeStr);
            Console.WriteLine("--- end:");

            return mazeStr;
        }
    }
}
