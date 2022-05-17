using unvell.D2DLib.WinForm;

namespace BIGFOOT.RGBMatrix.MatrixTypes.Direct2D
{

    public class MatrixD2DForm : D2DForm
    {
        public MatrixD2DForm(int rows = 64)
        {
            Size = new System.Drawing.Size(rows * 30 - 30, rows * 30 - 30);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}