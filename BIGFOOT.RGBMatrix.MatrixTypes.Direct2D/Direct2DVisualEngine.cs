using BIGFOOT.RGBMatrix.Visuals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using unvell.D2DLib.WinForm;
using unvell.D2DLib;
using System.Threading.Tasks;
using System.Threading;

namespace BIGFOOT.RGBMatrix.MatrixTypes.Direct2D
{
    public static class Direct2DVisualEngine
    {
        public static void BeginVirtualDirect2DGraphicsVisualEmulation<TVisual>(TVisual visual) 
            where TVisual : Visual<Direct2DMatrix, Direct2DCanvas>
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var canvasForm = visual.GetMatrix().InterfacedCreateOffscreenCanvas();
            canvasForm.SetVisual(visual);
            Application.Run(canvasForm);
        }
    }
}
