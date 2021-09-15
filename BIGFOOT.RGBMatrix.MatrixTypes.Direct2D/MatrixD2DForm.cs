using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using unvell.D2DLib.WinForm;

namespace BIGFOOT.RGBMatrix.MatrixTypes.Direct2D
{

    public class MatrixD2DForm : D2DForm
    {
        public MatrixD2DForm(int rows = 32)
        {
            var screenSize = Screen.FromControl(this).WorkingArea.Size;
            //Size = new System.Drawing.Size(rows * 30 + 16, rows * 30 + 40);
            Size = new System.Drawing.Size(rows * 30 - 30, rows * 30 - 30);
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
    }
}