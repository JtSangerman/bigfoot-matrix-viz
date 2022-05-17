using BIGFOOT.RGBMatrix.Visuals;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIGFOOT.RGBMatrix.MatrixTypes.Direct2D
{
    public static class Direct2DVisualEngine
    {
        public static event FormClosedEventHandler E_FORM_CLOSED = delegate { };

        public static async Task BeginVirtualDirect2DGraphicsVisualEmulation<TVisual>(TVisual visual, int tickMs = 100, CancellationToken cancellationToken = default) 
            where TVisual : Visual<Direct2DMatrix, Direct2DCanvas>
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var canvasForm = visual.GetMatrix().InterfacedCreateOffscreenCanvas();
            visual.SetTickMs(tickMs);
            canvasForm.SetVisual(visual);

            Application.Run(canvasForm);
        }
    }
}
