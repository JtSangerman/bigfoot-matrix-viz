using BIGFOOT.MatrixViz.MatrixTypes.ColoredConsole;
using BIGFOOT.MatrixViz.MatrixTypes.Direct2D;
using BIGFOOT.MatrixViz.Util;
using BIGFOOT.MatrixViz.Visuals;
using BIGFOOT.MatrixViz.Visuals.ArraySorts;
using BIGFOOT.MatrixViz.Visuals.Maze;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BIGFOOT.MatrixViz.EntryPoints
{
    class BIGFOOTDemo
    {
        static Dictionary<string, string> MENU2_OPTIONS = new Dictionary<string, string>()
        {
            { "<1>", "BubbleSort\t\tO(n^2)  \t\tColoredConsole" },
            { "<2>", "InsertionSort\t\tO(n^2)  \t\tColoredConsole" },
            { "<3>", "BubbleSort\t\tO(n^2)  \t\tFastConsole" },
            { "<4>", "Maze solver\t\tO(?)  \t\t\tColoredConsole" },
            { "<5>", "BubbleSort\t\tO(n^2)  \t\tInterfacing (UNSTABLE)" },
            { "\n",  "\t<ENTER> to go back to rendered graphics options -->\n\n\t\tNOTE: these are experimental & may not work on all cpu architectures" },
        };

        static Dictionary<string, string> MENU1_OPTIONS = new Dictionary<string, string>()
        {
            { "<1>", "Dijkstra's maze solver\tO(?)  \t\t\tDirectX" },
            { "<2>", "InsertionSort\t\tO(n^2) \t\t\tDirectX"},
            { "<3>", "BubbleSort\t\tO(n^2)  \t\tDirectX" },
            { "\n",  "\t<ENTER> to see basic prototyping rendering options -->\n\n\t\tNOTE: these are experimental & may not work on all cpu architectures" },
        };



        static async Task Main(string[] args)
        {
            var initial = true;
            while (true)
            {
                try
                {
                    await ExecuteProcessLoop(initial, default);
                    initial = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static async Task ExecuteProcessLoop(bool initial, CancellationToken cancellationToken = default)
        {
            if (initial)
                StartupConsole.DisplayGreeting();

            StartupConsole.DisplayOptions(MENU1_OPTIONS);

            var opt = Console.ReadKey().Key;

            var tickMs = opt == ConsoleKey.D1 ? 5 : 100;

            StartupConsole.PromptCustomTickRate(tickMs);

            var tickMsInputStr = Console.ReadLine();
            if (int.TryParse(tickMsInputStr, out var parsed))
            {
                tickMs = parsed;
            }

            var d2dMatrix = new Direct2DMatrix(64);
            Visual<Direct2DMatrix, Direct2DCanvas> d2dVisual;

            switch (opt)
            {
                case ConsoleKey.D1:
                    d2dVisual = new MazeHolder<Direct2DMatrix, Direct2DCanvas>(d2dMatrix, tickMs);
                    break;
                case ConsoleKey.D2:
                    d2dVisual = new InsertionSort<Direct2DMatrix, Direct2DCanvas>(d2dMatrix);
                    break;
                case ConsoleKey.D3:
                    d2dVisual = new BubbleSort<Direct2DMatrix, Direct2DCanvas>(d2dMatrix);
                    break;
                default:
                    d2dVisual = null;
                    break;
            }

            if (d2dVisual != null)
            {
                d2dVisual.SetTickMs(tickMs);
                StartupConsole.DisplayLoadingEmulation();
                Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(d2dVisual, tickMs, cancellationToken).ConfigureAwait(false); ;
                
            }
            else
            {
                StartupConsole.DisplayOptions(MENU2_OPTIONS);
                opt = Console.ReadKey().Key;

                if (opt == ConsoleKey.D1)
                {
                    var matrix = new ColoredConsoleMatrix(64);
                    var bubble = new BubbleSort<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix);
                    StartupConsole.DisplayLoadingEmulation();
                    bubble.Visualize();
                }
                else if (opt == ConsoleKey.D2)
                {
                    var matrix = new ColoredConsoleMatrix(64);
                    var insertion = new InsertionSort<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix);
                    StartupConsole.DisplayLoadingEmulation();
                    insertion.Visualize();
                }
                else if (opt == ConsoleKey.D3)
                {
                    var matrix = new FastConsoleMatrix(64);
                    var bubble = new InsertionSort<FastConsoleMatrix, FastConsoleCanvas>(matrix);
                    StartupConsole.DisplayLoadingEmulation();
                    bubble.Visualize();
                }
                else if (opt == ConsoleKey.D4)
                {
                    var matrix = new ColoredConsoleMatrix(64);
                    var maze = new MazeHolder<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix, 1);
                    StartupConsole.DisplayLoadingEmulation();
                    maze.Visualize();
                }
                else if (opt == ConsoleKey.D5)
                {
                    // doesn't do anything on emulator

                    //var hardwareMatrix = new InterfacedRGBLedMatrix(64, 1, 1);
                    //var bubble = new BubbleSort<InterfacedRGBLedMatrix, InterfacedRGBLedCanvas>(hardwareMatrix);
                    StartupConsole.DisplayLoadingEmulation();
                    //bubble.Visualize();
                }
                else
                {
                    return;
                }
            }

            StartupConsole.DisplayVisualEmulationCompleted();
        }
    }
}
