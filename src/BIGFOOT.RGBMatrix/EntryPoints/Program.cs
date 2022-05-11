using BIGFOOT.RGBMatrix.MatrixTypes.ColoredConsole;
using BIGFOOT.RGBMatrix.MatrixTypes.Direct2D;
using BIGFOOT.RGBMatrix.Visuals.ArraySorts;
using BIGFOOT.RGBMatrix.Visuals.Maze;

using System;
using BIGFOOT.RGBMatrix.Visuals;
using System.Collections.Generic;
using BIGFOOT.RGBMatrix.ConsoleHelper;
using System.Threading.Tasks;
using System.Threading;
using BIGFOOT.RGBMatrix.MatrixTypes.InterfacedRGBLed;

namespace BIGFOOT.RGBMatrix
{


    class Program
    {
        static Dictionary<string, string> MENU2_OPTIONS = new Dictionary<string, string>()
        {
            { "<1>", "BubbleSort\t\tO(n^2)  \t\tColoredConsole" },
            { "<2>", "InsertionSort\t\tO(n^2)  \t\tColoredConsole" },
            { "<3>", "BubbleSort\t\tO(n^2)  \t\tFastConsole" },
            { "<4>", "Maze solver\t\tO(?)  \t\t\tColoredConsole" },
            { "<5>", "BubbleSort\t\tO(n^2)  \t\tInterfacing (UNSTABLE)" },
            { "\n",  "\t<ENTER> to go back to rendered graphics options -->\n\n\t\tNOTE: these are expirimental & may not work on all cpu architectures" },
        };

        static Dictionary<string, string> MENU1_OPTIONS = new Dictionary<string, string>()
        {
            { "<1>", "Maze solver\t\tO(?)  \t\t\tDirectX" },
            { "<2>", "InsertionSort\t\tO(n^2) \t\t\tDirectX"},
            { "<3>", "BubbleSort\t\tO(n^2)  \t\tDirectX" },
            { "\n",  "\t<ENTER> to see basic prototyping rendering options -->\n\n\t\tNOTE: these are expirimental & may not work on all cpu architectures" },
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
               // await Task.Run(async () =>
             //   {

              //  });
            }
        }

        public static async Task ExecuteProcessLoop(bool initial, CancellationToken cancellationToken = default)
        {
            if (initial)
                StartupConsole.DisplayGreeting();

            StartupConsole.DisplayOptions(MENU1_OPTIONS);

            var opt = Console.ReadKey().Key;

            var matrixv = new Direct2DMatrix(32);
            Visual<Direct2DMatrix, Direct2DCanvas> v;

            switch (opt)
            {
                case ConsoleKey.D1:
                    v = new MazeHolder<Direct2DMatrix, Direct2DCanvas>(matrixv, 15, 15, 5);
                    break;
                case ConsoleKey.D2:
                    v = new InsertionSort<Direct2DMatrix, Direct2DCanvas>(matrixv);
                    break;
                case ConsoleKey.D3:
                    v = new BubbleSort<Direct2DMatrix, Direct2DCanvas>(matrixv);
                    break;
                default:
                    v = null;
                    break;
            }

            if (v != null)
            {
                StartupConsole.PromptCustomTickRate();

                var tickMs = 100;
                var tickMsInputStr = Console.ReadLine();
                if (int.TryParse(tickMsInputStr, out var parsed))
                {
                    tickMs = parsed;
                }

                StartupConsole.DisplayLoadingEmulation();
                await Task.Run(async () =>
                {
                    await Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(v, tickMs, cancellationToken);
                });
            }
            else
            {
                StartupConsole.DisplayOptions(MENU2_OPTIONS);
                opt = Console.ReadKey().Key;

                if (opt == ConsoleKey.D1)
                {
                    var matrix = new ColoredConsoleMatrix(32);
                    var bubble = new BubbleSort<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix);
                    StartupConsole.DisplayLoadingEmulation();
                    bubble.Visualize();
                }
                else if (opt == ConsoleKey.D2)
                {
                    var matrix = new ColoredConsoleMatrix(32);
                    var insertion = new InsertionSort<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix);
                    StartupConsole.DisplayLoadingEmulation();
                    insertion.Visualize();
                }
                else if (opt == ConsoleKey.D3)
                {
                    var matrix = new FastConsoleMatrix(32);
                    var bubble = new InsertionSort<FastConsoleMatrix, FastConsoleCanvas>(matrix);
                    StartupConsole.DisplayLoadingEmulation();
                    bubble.Visualize();
                }
                else if (opt == ConsoleKey.D4)
                {
                    var matrix = new ColoredConsoleMatrix(32);
                    var maze = new MazeHolder<ColoredConsoleMatrix, ColoredConsoleCanvas>(matrix, 4, 4, 100);
                    StartupConsole.DisplayLoadingEmulation();
                    maze.Visualize();
                }
                else
                {
                    var hardwareMatrix = new InterfacedRGBLedMatrix(32, 1, 1);
                    var bubble = new BubbleSort<InterfacedRGBLedMatrix, InterfacedRGBLedCanvas>(hardwareMatrix);
                    StartupConsole.DisplayLoadingEmulation();
                    bubble.Visualize();
                }
            }

            StartupConsole.DisplayVisualEmulationCompleted();
        }
    }
}
