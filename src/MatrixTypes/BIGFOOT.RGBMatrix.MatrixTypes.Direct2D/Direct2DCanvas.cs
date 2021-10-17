using BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing;
using BIGFOOT.RGBMatrix.Visuals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using unvell.D2DLib;
using unvell.D2DLib.WinForm;

namespace BIGFOOT.RGBMatrix.MatrixTypes.Direct2D
{
    public class Direct2DCanvas : MatrixD2DForm, Canvas
    {
        private BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing.Color[,] _grid;
        private Game<Direct2DMatrix, Direct2DCanvas> _visual;

        private readonly int _size;
        public Direct2DCanvas(int size) : base(size)
        {
            _size = size;
            _grid = new BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing.Color[size, size];
            Text = "BIGFOOT Emulator - D2D Graphics Engine";
            BackColor = System.Drawing.Color.Black;

            ShowFPS = true;
            AnimationDraw = true;
        }

        protected override void OnRender(D2DGraphics g)
        {
            Display(g);
            if (_bitMap != null)
                g.DrawBitmap(_bitMap, this.ClientRectangle);
        }

        protected override void OnFrame()
        {
            //Console.WriteLine($"Current key: {_visual._dir}");
            SceneChanged = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //if (_visual._input != null)
            //{
            //    new Thread(() =>
            //    {
            //        _visual._input.EstablishControllerConnection();
            //    }).Start();
            //}

            new Thread(() =>
            {
                _visual.Visualize();
            }).Start();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            _visual._dir = e.KeyCode.ToString();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        public void SetVisual(Game<Direct2DMatrix, Direct2DCanvas> visual)
        {
            _visual = visual;
        }

        public BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing.Color[,] GetGrid()
        {
            return _grid;
        }

        public void Clear()
        {
            var color = new LEDBoard.DriverInterfacing.Color(BackColor.R, BackColor.B, BackColor.G);
            Fill(color);
        }

        public void DrawCircle(int x0, int y0, int radius, BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing.Color color)
        {
        }

        public void DrawLine(int x0, int y0, int x1, int y1, BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing.Color color)
        {
            for (var x = x0; x <= x1; x++)
            {
                for (var y = y0; y <= y1; y++)
                {
                    _grid[x, y] = color;
                }
            }
        }

        public void Fill(BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing.Color color)
        {
            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    _grid[i, j] = color;
                }
            }
        }

        public void SetPixel(int x, int y, BIGFOOT.RGBMatrix.LEDBoard.DriverInterfacing.Color color)
        {
            if (
                 (
                     x >= 0,
                     y >= 0,
                     x < _grid.GetLength(0),
                     y < _grid.GetLength(1)
                 )
                 == (true, true, true, true)
             )
                _grid[x, y] = color;
        }


        private Bitmap _bitMap;
        public void Display(D2DGraphics g)
        {
            _bitMap?.Dispose();
            _bitMap = new Bitmap(_visual.Rows * 30, _visual.Rows * 30);
            using (Graphics g2 = Graphics.FromImage(_bitMap))
            {
                g2.Clear(BackColor);
                for (int i = 0; i < _grid.GetLength(0); i++)
                {
                    for (int j = 0; j < _grid.GetLength(1); j++)
                    {
                        var led = _grid[i, _size - j - 1];
                        if (led != null)
                        {
                            if ((led.R, led.B, led.G) == (123, 123, 123))
                            {
                                led.R = led.G = led.B = 255;
                            }
                            var pen = new Pen(System.Drawing.Color.FromArgb(led.R, led.G, led.B));
                            var rect = new Rectangle(i * 30, j * 30, 30, 30);
                            var brush = pen.Brush;
                            g2.FillRectangle(brush, rect);
                            //g2.DrawString($"{i},{j}", Font, Pens.Pink.Brush, new PointF(i*30, j*30));
                        }
                    }
                }
            }

        }

    }

}
