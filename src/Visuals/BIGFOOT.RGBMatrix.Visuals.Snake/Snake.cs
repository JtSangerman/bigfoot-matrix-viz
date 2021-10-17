using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using BIGFOOT.RGBMatrix.Visuals;
using BIGFOOT.RGBMatrix.Visuals.Inputs;
using BIGFOOT.RGBMatrix.Visuals.Snake;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SnakeBIGFOOT.RGBMatrix.Visuals.Games
{
    public class Snake<TMatrix, TCanvas> : Game<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private readonly GameState _state;
        private const int _tickRateMs = 1000;

        public Snake(TMatrix matrix, ControllerInputDriver input) : base(matrix, input) 
        {
            _state = new GameState(Rows);
        }

        //public override void Start()
        //{
        //    while (!_state.IsGameOver)
        //    {
        //        Tick();
        //    }
        //}

        private void Tick(int tickRateMs = _tickRateMs)
        {
            Debug_enumerateInputQueue();
            Thread.Sleep(tickRateMs);
        }


        ///// debug
        ///
        private void Debug_enumerateInputQueue()
        {
            if (_inputQueue?.Count == 0) return;

            Console.WriteLine("Input queue: \n\n");

            var num = 0;
            _inputQueue.ForEach(i =>
            {
                Console.WriteLine($"{num++}: {i}");
            });
        }
    }
}
