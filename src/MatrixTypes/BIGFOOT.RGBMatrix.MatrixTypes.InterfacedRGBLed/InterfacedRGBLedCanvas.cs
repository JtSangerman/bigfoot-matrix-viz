using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using System;

namespace BIGFOOT.RGBMatrix.MatrixTypes.InterfacedRGBLed
{
    public class InterfacedRGBLedCanvas : Canvas
    {
        internal readonly RGBLedCanvas _canvas;
        public InterfacedRGBLedCanvas(RGBLedCanvas canvas)
        {
            _canvas = canvas;
        }
        public void Clear()
        {
            _canvas.Clear();
        }
        public void DrawCircle(int x0, int y0, int radius, Color color)
        {
            _canvas.DrawCircle(x0, y0, radius, color);
        }
        public void DrawLine(int x0, int y0, int x1, int y1, Color color)
        {
            _canvas.DrawLine(x0, y0, x1, y1, color);
        }
        public void Fill(Color color)
        {
            _canvas.Fill(color);
        }
        public void SetPixel(int x, int y, Color color)
        {
            _canvas.SetPixel(x, y, color);
        }
    }

}
