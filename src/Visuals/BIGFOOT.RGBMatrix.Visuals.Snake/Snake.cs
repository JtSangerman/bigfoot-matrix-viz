using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using BIGFOOT.RGBMatrix.Visuals;
using BIGFOOT.RGBMatrix.Visuals.Input;
using System;

namespace SnakeBIGFOOT.RGBMatrix.Visuals.Maze
{
    public class Snake<TMatrix, TCanvas> : Game<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private readonly ControllerInput _input;
        public Snake(TMatrix matrix, ControllerInput input) : base(matrix, input) 
        {
            _input = input;
        }

        //private readonly ControllerInput _input;
        //public Snake(TMatrix matrix, ControllerInput input) : base(matrix, input)
        //{
        //    _input = input;
        //}

        public override void Start()
        {
            _input.Connect();

            _input.Up();
        }
    }
}
