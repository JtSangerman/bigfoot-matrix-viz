using BIGFOOT.MatrixViz.Visuals;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIGFOOT.MatrixViz.MatrixTypes.Direct2D
{
    public static class Direct2DVisualEngine
    {
        public static event FormClosedEventHandler E_FORM_CLOSED = delegate { };

        public static async Task BeginVirtualDirect2DGraphicsVisualEmulation<TVisual>(TVisual visual, int tickMs = 100, CancellationToken cancellationToken = default) 
            where TVisual : Visual<Direct2DMatrix, Direct2DCanvas>
        {
            Application.EnableVisualStyles();

            var canvasForm = visual.GetMatrix().InterfacedCreateOffscreenCanvas();
            visual.SetTickMs(tickMs);
            canvasForm.SetVisual(visual);
            canvasForm.Show();

            Application.Run(canvasForm);
            canvasForm.Close();
            Application.Exit();
        }
    }
}
