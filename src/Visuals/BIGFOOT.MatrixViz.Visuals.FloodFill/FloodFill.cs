
using BIGFOOT.MatrixViz.DriverInterfacing;
using BIGFOOT.MatrixViz.Visuals.Enums;
using BIGFOOT.MatrixViz.Visuals.FloodFill.Models;
using BIGFOOT.MatrixViz.Visuals.Maze;
using BIGFOOT.MatrixViz.Visuals.Models;
using System;
using System.Drawing;
using System.Threading;

namespace BIGFOOT.MatrixViz.Visuals.FloodFill
{
    public class FloodFill<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private TCanvas _canvas { get; set; }

        private readonly FillStateManager _fillStateManager;

        public FloodFill(TMatrix matrix, string serializedInitialGridStr = "", int? tickMs = null) : base(matrix, tickMs)
        {
            _fillStateManager = new FillStateManager(serializedInitialGridStr, Matrix.Size);
        }

        public override void VisualizeOnHardware() => throw new NotImplementedException();

        public override void VisualizeVirtually() => Run();

        private void Run()
        {
            _canvas = Matrix.InterfacedGetCanvas();

            while (!_fillStateManager.FillCompleted)
            {
                Tick();
            }
        }

        private void Tick()
        {
            _fillStateManager.StepStateForward();
            Draw();
            Thread.Sleep(TickMs);
        }

        private void Draw()
        {
            MatrixGridUtility.DrawGridLayer(_canvas, _fillStateManager.CurrentState, Matrix.Size);
            _canvas = Matrix.InterfacedSwapOnVsync(_canvas);
        }
    }
}