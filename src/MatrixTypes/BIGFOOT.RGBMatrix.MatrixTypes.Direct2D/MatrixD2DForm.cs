using unvell.D2DLib.WinForm;

namespace BIGFOOT.RGBMatrix.MatrixTypes.Direct2D
{

    public class MatrixD2DForm : D2DForm
    {
        public const int SCREEN_SIZE_PX = 960;
        public  int PxConst { get => SCREEN_SIZE_PX / 64; }
        public MatrixD2DForm(int rows = 64)
        {
            Size = new System.Drawing.Size(SCREEN_SIZE_PX-PxConst, SCREEN_SIZE_PX-PxConst);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}