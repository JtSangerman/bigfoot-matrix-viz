﻿using System;
using SharpDX.XInput;


namespace BluetoothXboxOneController
{
    public class BTXboxOneControllerDriver
    {
        Controller controller;
        Gamepad gamepad;
        bool connected;
        public BTXboxOneControllerDriver()
        {
            controller = new Controller(UserIndex.One);
            connected = controller.IsConnected;
        }


        //class MultiThreadGameEnginePOC
        //{
        //    public static ConsoleKey _dir;
        //    public static bool _running = true;

        //    /*
        //     * Currently a multihthreading POC to support user input while drawing games
        //     */
        //    static void Main(string[] args)
        //    {
        //        Console.WriteLine("Starting Hello World! ...");

        //        DoStuffThreaded();

        //        while (true)
        //        {
        //            _dir = Console.ReadKey().Key;
        //            if (_dir == ConsoleKey.Z)
        //            {
        //                _running = false;
        //                break;
        //            }
        //        }

        //        Console.WriteLine("Goodbye, world! Main thread ENDED!");
        //    }

        //    public static void DoStuffThreaded()
        //    {
        //        new Thread(() =>
        //        {
        //            Console.WriteLine("Starting to do stuff");
        //            while (_running)
        //            {
        //                Console.WriteLine($"doing stuff, but the key is {_dir}");
        //                Thread.Sleep(1000);
        //            }
        //        }).Start();
        //    }
        //}
    }
}
