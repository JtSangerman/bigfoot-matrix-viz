using BIGFOOT.RGBMatrix.DriverInterfacing;
using System;
using System.Threading;

namespace BIGFOOT.RGBMatrix.Visuals
{
    public abstract class Visual<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        protected readonly TMatrix Matrix;
        public readonly int Rows = 64;
        protected int TickMs;
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

        public virtual void SetTickMs(int tickMs)
        {
            TickMs = tickMs;
        }

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

            if (Matrix.GetType().Name != "Direct2DMatrix")
            {
                Thread.Sleep(2500);
            }
        }

        public TMatrix GetMatrix()
        {
            return Matrix;
        }

        public abstract void VisualizeOnHardware();
        public abstract void VisualizeVirtually();
    }
}
