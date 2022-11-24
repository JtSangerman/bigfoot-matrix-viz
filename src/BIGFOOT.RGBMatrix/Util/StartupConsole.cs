using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace BIGFOOT.RGBMatrix.ConsoleHelper
{

    public static class StartupConsole
    {
        // TODO add proper config handling
        private static class LazyConfigs
        {
            public const bool SkipFakeLoads = true;
        }

        private static string BIGFOOT_ASCII_ART_BLOCK = @"
_|_|_|    _|_|_|    _|_|_|  _|_|_|_|    _|_|      _|_|    _|_|_|_|_|  
_|    _|    _|    _|        _|        _|    _|  _|    _|      _|      
_|_|_|      _|    _|  _|_|  _|_|_|    _|    _|  _|    _|      _|      
_|    _|    _|    _|    _|  _|        _|    _|  _|    _|      _|      
_|_|_|    _|_|_|    _|_|_|  _|          _|_|      _|_|        _|      

";

        private static string BIGFOOT_ASCII_ART_STANDARD_REPEATED = @"
 ____ ___ ____ _____ ___   ___ _____           ____ ___ ____ _____ ___   ___ _____           ____ ___ ____ _____ ___   ___ _____ 
| __ )_ _/ ___|  ___/ _ \ / _ \_   _|         | __ )_ _/ ___|  ___/ _ \ / _ \_   _|         | __ )_ _/ ___|  ___/ _ \ / _ \_   _|
|  _ \| | |  _| |_ | | | | | | || |           |  _ \| | |  _| |_ | | | | | | || |           |  _ \| | |  _| |_ | | | | | | || | 
| |_) | | |_| |  _|| |_| | |_| || |           | |_) | | |_| |  _|| |_| | |_| || |           | |_) | | |_| |  _|| |_| | |_| || |    
|____/___\____|_|   \___/ \___/ |_|           |____/___\____|_|   \___/ \___/ |_|           |____/___\____|_|   \___/ \___/ |_|  
 ____ ___ ____ _____ ___   ___ _____           ____ ___ ____ _____ ___   ___ _____           ____ ___ ____ _____ ___   ___ _____     
--------------------------------------------------------------------------------------------------------------------------------

";

        private static string BIGFOOT_ASCII_ART_STANDARD = @"
  ____ _____ _____ ______ ____   ____ _______      
 |  _ \_   _/ ____|  ____/ __ \ / __ \__   __|     
 | |_) || || |  __| |__ | |  | | |  | | | |         __   _(_)____
 |  _ < | || | |_ |  __|| |  | | |  | | | |         \ \ / / |_  /
 | |_) || || |__| | |   | |__| | |__| | | |    _     \ V /| |/ / 
 |____/_____\_____|_|    \____/ \____/  |_|   (_)     \_/ |_/___|
 ________________________________________________________________
";

        private static string BIGFOOT_ASCII_ART_GRAFFITI = @"
__________.___  ___________________________   ___________________
\______   \   |/  _____/\_   _____/\_____  \  \_____  \__    ___/
 |    |  _/   /   \  ___ |    __)   /   |   \  /   |   \|    |   
 |    |   \   \    \_\  \|     \   /    |    \/    |    \    |   
 |______  /___|\______  /\___  /   \_______  /\_______  /____|   
        \/            \/     \/            \/         \/        
";

        private static string VERSION = "1.1.2-alpha";
        private static int CONSOLE_WIDTH = 100;
        private static int LOADING_SEGMENTS = (int)(StartupConsole.CONSOLE_WIDTH / 1.5);

        public static void DisplayGreeting()
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 100;

            DisplayAsciiArt();

            RenderLoadingBar("> Welcome to BIGFOOT:\n> A LED Panel Emulator designed for exploring visualizations of anything interesting.\n\n", LOADING_SEGMENTS, 5000 / 80);

            Console.Clear();
        }

        public static void DisplayLoadingEmulation()
        {
            Console.Clear();
            DisplayAsciiArt();

            RenderLoadingBar("> Building emulation - Generating and randomizing:\n\n\n", LOADING_SEGMENTS, 5000 / 200);
        }

        public static void DisplayVisualEmulationCompleted()
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 100;
            Console.Clear();
            DisplayAsciiArt();

            RenderLoadingBar("> Cleaning up and returning to menu... \n\n\n", (int)(LOADING_SEGMENTS / 1.6), 5000 / 450);
        }

        public static void RenderLoadingBar(string preBuffer = "", int segments = 50, int tickRateConst = 1, bool skipRender = LazyConfigs.SkipFakeLoads)
        {
            if (skipRender) return;

            var tickRateMs = 5000 / (segments * tickRateConst);
            const string loadingTxt = "LOADING ";
            Random r = new Random();
            Console.CursorVisible = false;
            Stopwatch w = Stopwatch.StartNew();


            for (int i = 1; i <= segments; i++)
            {
                Console.Write(preBuffer);

                int delayChance = r.Next(20);
                if (delayChance == 1)
                {
                    int delayMs = r.Next(1000);
                    Thread.Sleep(delayMs);
                }

                Console.SetCursorPosition((Console.WindowWidth - (loadingTxt.Length + 2 + segments)) / 2, Console.CursorTop);
                Console.Write(loadingTxt);

                var prevColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("|");

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(new String(' ', i - (i == 1 ? 1 : 0)));

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(new String(' ', segments - i));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("|");
                Console.ForegroundColor = prevColor;

                Thread.Sleep(tickRateMs);

                if (i == 1)
                {
                    Thread.Sleep(2000);
                }

                Console.SetCursorPosition(0, Console.CursorTop - 3);
                Console.Write(new String(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1); // SET TO Console.CursorTop-2 for DEPLOYED BUILD, ELSE -1
                Console.Write(new String(' ', Console.BufferWidth));

                if (i == segments)
                {
                    Console.Write(preBuffer);

                    Console.SetCursorPosition((Console.WindowWidth - (loadingTxt.Length + 2 + segments)) / 2, Console.CursorTop);
                    Console.Write(loadingTxt);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("|");

                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{new String(' ', segments)}");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("|");

                    Console.ForegroundColor = prevColor;

                    Console.SetCursorPosition((Console.WindowWidth - (loadingTxt.Length + 2 + segments)) / 2, Console.CursorTop);
                    Console.Write("DONE");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" in {w.ElapsedMilliseconds}ms");

                    Console.ForegroundColor = prevColor;
                }
            }

            Thread.Sleep(2000);
            Console.CursorVisible = true;
        }

        public static void DisplayAsciiArt()
        {
            Console.Clear();
            DisplayAsciiArt(BIGFOOT_ASCII_ART_STANDARD);
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void PromptCustomTickRate(int defaultTickMs)
        {
            Console.Clear();
            DisplayAsciiArt();

            var prevColor = Console.ForegroundColor;

            Console.Write("\n> Set a custom tick rate (ms)?");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" (Press <ENTER> to use default value of");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($" {defaultTickMs}ms");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(").");

            Console.ForegroundColor = prevColor;
            Console.Write("> ");
        }

        public static void DisplayVersionNumber_Right()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(new String(' ', Console.BufferWidth - VERSION.Length - 10));
            Console.WriteLine(VERSION);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void DisplayVersionNumber_Left()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(VERSION);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void DisplayOptions(Dictionary<string, string> options)
        {
            Console.Clear();
            DisplayAsciiArt(BIGFOOT_ASCII_ART_STANDARD);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.CursorVisible = true;

            Console.WriteLine("> Explore a visualization:\n\n\n");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\t\tConcept \t\tComputed Complexity\tInterfacing");
            Console.WriteLine("\t\t------------------- \t-------------------\t-------------------");

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var key in options.Keys)
            {
                string separator = "";//string.IsNullOrWhiteSpace(key) ? "" : ":";
                var prevColor = Console.ForegroundColor;
                if (string.IsNullOrWhiteSpace(key))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.WriteLine($"\t{key}{separator} \t{options[key]}");
                Console.ForegroundColor = prevColor;
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\n\n\n> ");
        }

        public static void DisplayAsciiArt(string ascii)
        {
            DisplayVersionNumber_Left();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{ascii}");
        }
    }
}
