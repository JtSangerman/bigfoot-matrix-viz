using BIGFOOT.RGBMatrix.Visuals.Snake;
using BluetoothXboxOneControllerBIGFOOT.RGBMatrix.Visuals.Inputs;
using rpi_rgb_led_matrix_sharp;

namespace BIGFOOT.RGBMatrix
{
    public class HardwareSnakeStartup
    {
        public static void Main(string[] args)
        {
            var matrix = new RGBLedMatrix(32, 1, 1);
            var controller = new BTXboxOneControllerDriver();
            var vis = new Snake(matrix, controller);
            vis.Run();
        }
    }
}
