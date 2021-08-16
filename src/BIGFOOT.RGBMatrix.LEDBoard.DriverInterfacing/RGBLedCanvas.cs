using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;

namespace BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing
{
    public class RGBLedCanvas
    {
        public RGBLedCanvas() { }
        public void Clear() { }
        public void DrawLine(int x0, int y0, int x1, int y1, Color color) { }
        public void SetPixel(int x, int y, Color color) { }
        public void Fill(Color color) { }
        public void DrawCircle(int x0, int y0, int radius, Color color) { }
    }
}
