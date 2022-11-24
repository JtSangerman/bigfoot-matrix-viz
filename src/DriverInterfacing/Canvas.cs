namespace BIGFOOT.MatrixViz.DriverInterfacing
{
    public interface Canvas
    {
        public void SetPixel(int x, int y, Color color);
        public void Fill(Color color);
        public void Clear();
        public void DrawCircle(int x0, int y0, int radius, Color color);
        public void DrawLine(int x0, int y0, int x1, int y1, Color color);
    }
}
