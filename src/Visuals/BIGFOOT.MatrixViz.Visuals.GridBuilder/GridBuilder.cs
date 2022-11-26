using BIGFOOT.MatrixViz.DriverInterfacing;
using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;
using BIGFOOT.MatrixViz.Visuals.GridBuilder.Models;
using BIGFOOT.MatrixViz.Visuals.Models;
using System.Threading.Tasks;
using System.Linq;
using BIGFOOT.MatrixViz.Inputs.Drivers.Enums;
using System.Threading;

namespace BIGFOOT.MatrixViz.Visuals.GridBuilder
{
    // visual playable modelled as a grid with a selector 
    // driven with controller inputs
    //      directional to move tile selector
    //      non-directional to execute actions on selected tile
    public class GridBuilder<TMatrix, TCanvas> : Playable<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        public TCanvas _canvas { get; set; }
        public GridBuilderTile[,] Grid { private set; get; }
        private VisualCoordinate _selector { get; set; }
        private bool _finishedBuilding { get; set; }

        public GridBuilder(TMatrix matrix, ControllerInputDriverBase input) : base(matrix, input) { }

        public void Init()
        {
            _canvas = Matrix.InterfacedGetCanvas();

            _selector = new VisualCoordinate(Rows / 2, Rows / 2);

            Grid = new GridBuilderTile[Rows, Rows];
            Grid.Initialize();
        }

        protected override async void Run()
        {
            Init();

            while (!_finishedBuilding)
            {
                await ExecutePlayableTick();
                Thread.Sleep(TickMs);
            }
        }

        private async Task ExecutePlayableTick()
        {
            MoveSelector();
            Draw();
        }

        private (int, int) ParseInputQueue()
        {
            int moveSelectorHeight = InputQueue.Where(x => x == ControllerInput.UP).Count() - InputQueue.Where(x => x == ControllerInput.DOWN).Count();
            int moveSelectorLength = InputQueue.Where(x => x == ControllerInput.RIGHT).Count() - InputQueue.Where(x => x == ControllerInput.LEFT).Count();

            InputQueue.Clear();

            return (moveSelectorLength, moveSelectorHeight);
        }

        private void MoveSelector() 
        {
            (int, int) move = ParseInputQueue();
            _selector.AdjustLength(move.Item1);
            _selector.AdjustVert(move.Item2);
        }

        protected override void Draw()
        {
            _canvas.Clear();

            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    var color = IsTileSelected(x, y)
                         ? Color.FromKnownColor(System.Drawing.KnownColor.White)
                         : Color.FromKnownColor(System.Drawing.KnownColor.Black);

                    _canvas.SetPixel(x, y, color);
                }
            }
            
            _canvas = Matrix.InterfacedSwapOnVsync(_canvas);
        }

        private bool IsTileSelected(int x, int y)
        {
            return (_selector.X, _selector.Y) == (x, y);
        }

        protected override void Handle_E_INPUT_DOWN()
        {
            _selector.AdjustVert(-1);
        }

        protected override void Handle_E_INPUT_LEFT()
        {
            _selector.AdjustLength(-1);
        }

        protected override void Handle_E_INPUT_RIGHT()
        {
            _selector.AdjustLength(1);
        }

        protected override void Handle_E_INPUT_UP()
        {
            _selector.AdjustVert(1);
        }
    }

}
