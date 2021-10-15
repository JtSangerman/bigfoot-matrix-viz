using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using System;
using System.Threading;

namespace BIGFOOT.RGBMatrix.Visuals
{
    public abstract class Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        protected readonly TMatrix Matrix;
        public readonly int Rows;
        private bool _isEmulating 
        {
            get 
            { 
                return Matrix.GetType().Name != "InterfacedRGBLedMatrix"; 
            } 
        }

        public Visual(TMatrix matrix)
        {
            Rows = matrix.Size;
            Matrix = matrix;
        }


        public string _dir;
        public bool _running = true;
        public virtual void Visualize()
        {
            if (_isEmulating)
            {
                VisualizeVirtually();
            }
            else
            {
                VisualizeOnHardware();
            }

            Thread.Sleep(10000);
            _running = false;
        }

        public TMatrix GetMatrix()
        {
            return Matrix;
        }

        public abstract void VisualizeOnHardware();
        public abstract void VisualizeVirtually();
    }
}
