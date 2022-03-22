using rpi_rgb_led_matrix_sharp;

namespace BIGFOOT.RGBMatrix.Visuals
{
    public abstract class Visual
    {
        protected readonly RGBLedMatrix Matrix;

        public Visual(RGBLedMatrix matrix)
        {
            Matrix = matrix;
        }

        public RGBLedMatrix GetMatrix()
        {
            return Matrix;
        }

        public abstract void Run();
    }
}
