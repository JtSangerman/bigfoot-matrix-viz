using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing
{
    public interface Canvas
    {
        public Canvas SwapOnVsync(Canvas canvas);
        public void SetPixel(int x, int y, Color color);
        public void Fill(Color color);
        public void Clear();
        public void DrawCircle(int x0, int y0, int radius, Color color);
        public void DrawLine(int x0, int y0, int x1, int y1, Color color);
    }
}
