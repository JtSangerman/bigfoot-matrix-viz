using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using BIGFOOT.RGBMatrix.Visuals.Inputs;
using BIGFOOT.RGBMatrix.Visuals.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.Visuals
{
    // TODO doesn't support hardware viz at the moment
    public abstract class Game<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private readonly TMatrix _matrix;
        private readonly ControllerInput _input;

        public Game(TMatrix matrix, ControllerInput input) : base(matrix)
        {
            _matrix = matrix;
            _input = input;
        }

        public override void VisualizeVirtually()
        {
            Visualize(true);
        }
        public override void VisualizeOnHardware()
        {
            Visualize(false);
        }

        private void Visualize(bool isEmulating)
        {
            ConnectController();
            Start();
        }

        private void ConnectController()
        {
            _input.Connect();
        }

        public abstract void Start();
    }
}