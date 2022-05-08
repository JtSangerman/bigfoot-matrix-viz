using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace BIGFOOT.RGBMatrix.ConsoleHelper
{
    public static class StartupConsole
    {
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

        private static string VERSION = "1.1.0-alpha";

        public static void DisplayGreeting()
        {
            Console.CursorVisible = false;
            Console.WindowWidth = 100;

            DisplayAsciiArt();

            RenderLoadingBar(10, "> Welcome to BIGFOOT:\n> A LED Panel Emulator designed for exploring visualizations of anything interesting.\n");

            Console.Clear();
        }

        public static void DisplayLoadingEmulation()
        {
            Console.Clear();
            DisplayAsciiArt();
           // RenderLoadingBar(10, "> Building emulation:\n");
        }

        public static void RenderLoadingBar(int segments, string preBuffer = "", int tickRateMs = 500)
        {
            Console.CursorVisible = false;

            Stopwatch w = Stopwatch.StartNew();
            for (int i = 1; i <= segments; i++)
            {
                Console.Write(preBuffer);

                if (i == 1)
                {
                    Thread.Sleep(tickRateMs * 2);
                }

                Console.Write("\n\t  LOADING ");

                var prevColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("[");

                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(new String(' ', i));

                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(new String(' ', segments - i));

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("]");
                Console.ForegroundColor = prevColor;

                Thread.Sleep(tickRateMs);

                Console.SetCursorPosition(0, Console.CursorTop-3);
                Console.Write(new String(' ', Console.BufferWidth));
                Console.SetCursorPosition(0, Console.CursorTop-1);
                Console.Write(new String(' ', Console.BufferWidth));

                if (i == segments)
                {
                    Console.Write(preBuffer);

                    Console.Write($"\n\t  LOADING ");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("[");

                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($"{new String(' ', segments)}");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("]");

                    Console.ForegroundColor = prevColor;
                    Console.Write("\t  DONE");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write($" ({w.ElapsedMilliseconds - (tickRateMs * 3)}ms)");

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

        public static void PromptCustomTickRate()
        {
            Console.Clear();
            DisplayAsciiArt();
            
            var prevColor = Console.ForegroundColor;

            Console.Write("\n> Set a custom tick rate (ms)?");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" (Press <ENTER> to use default value of");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" 100ms");

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
                string separator = string.IsNullOrWhiteSpace(key) ? "" : ":";
                Console.WriteLine($"\t{key}{separator} \t{options[key]}");
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
