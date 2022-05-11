using BIGFOOT.RGBMatrix.Visuals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BIGFOOT.RGBMatrix.MatrixTypes.Direct2D
{
    public class Direct2DContext<TVisual> : ApplicationContext 
        where TVisual : Visual<Direct2DMatrix, Direct2DCanvas>
    {
        private List<Form> forms;

        private static Direct2DContext<TVisual> context;

        private Direct2DContext(TVisual visual)
        {
            forms = new List<Form>();
            ShowMatrixD2DForm();
        }

        public static void ShowMatrixD2DForm()
        {
            Form form = new MatrixD2DForm();
            context.AddForm(form);
            form.Show();
        }

        private void AddForm(Form f)
        {
            f.Closed += FormClosed;
            forms.Add(f);
        }

        private void FormClosed(object sender, EventArgs e)
        {
            Form f = sender as Form;
            if (forms != null)
                forms.Remove(f);
            if (forms.Count == 0)
                Application.Exit();
        }
    }
}
