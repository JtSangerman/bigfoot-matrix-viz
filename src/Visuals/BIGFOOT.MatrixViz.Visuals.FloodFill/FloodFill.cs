
using BIGFOOT.MatrixViz.DriverInterfacing;
using System;
using System.Threading;

namespace BIGFOOT.MatrixViz.Visuals.FloodFill
{
    public class FloodFill<TMatrix, TCanvas> : Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        private bool _floodFillingFinished = false;

        public FloodFill(TMatrix matrix, string? serializedInitialGridStr = null, int? tickMs = null) : base(matrix, tickMs)
        {
        }

        public override void VisualizeOnHardware()
        {
            throw new NotImplementedException();
        }

        public override void VisualizeVirtually()
        {
            Run();
        }

        private void Run()
        {
            while (!_floodFillingFinished)
            {
                Tick();
                Draw();
                Thread.Sleep(TickMs);
            }
        }

        private void Tick()
        {

        }

        private void Draw()
        {

        }
    }
}