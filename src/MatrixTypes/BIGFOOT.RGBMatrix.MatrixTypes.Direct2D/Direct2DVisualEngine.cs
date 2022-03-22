//using BIGFOOT.RGBMatrix.Visuals;
//using System.Windows.Forms;

//namespace BIGFOOT.RGBMatrix.MatrixTypes.Direct2D
//{
//    public static class Direct2DVisualEngine
//    {
//        public static void BeginVirtualDirect2DGraphicsVisualEmulation<TVisual>(TVisual visual) 
//            where TVisual : Game<Direct2DMatrix, Direct2DCanvas>
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            var canvasForm = visual.GetMatrix().InterfacedCreateOffscreenCanvas();
//            canvasForm.SetVisual(visual);
//            Application.Run(canvasForm);
//        }
//    }
//}
