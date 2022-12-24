using BIGFOOT.MatrixViz.DriverInterfacing;
using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;
using BIGFOOT.MatrixViz.Visuals.Models;
using System.Linq;
using BIGFOOT.MatrixViz.Inputs.Drivers.Enums;
using System.Threading;
using BIGFOOT.MatrixViz.Visuals.Enums;
using BIGFOOT.MatrixViz.Visuals.Constants;

namespace BIGFOOT.MatrixViz.Visuals.GridBuilder
{
    // GridBuilder: playable modelled as a grid with a selector 
    // driven with controller inputs
    //      directional to move tile selector
    //      non-directional to execute actions on selected tile
    public class GridBuilder<TMatrix, TCanvas> : Playable<TMatrix, TCanvas>
        where TMatrix : Matrix<TCanvas>
        where TCanvas : Canvas
    {
        public TCanvas _canvas { get; set; }
        public MatrixGridTile[,] Grid { private set; get; }
        private GridBuilderCursor _selector { get; set; }
        private bool _finishedBuilding { get; set; }
        private readonly string _initialGridStateStr;
        public string Serialized => MatrixGridUtility.Serialize(Grid, Matrix.Size);

        public GridBuilder(TMatrix matrix, ControllerInputDriverBase input, string serializedMapStateToLoad = "") : base(matrix, input)
        {
            _initialGridStateStr = serializedMapStateToLoad;
        }

        protected override void Run()
        {
            InitBuilder();

            while (!_finishedBuilding)
            {
                ExecutePlayableTick();
                Thread.Sleep(TickMs);
            }
        }

        private void InitBuilder()
        {
            _canvas = Matrix.InterfacedGetCanvas();
            Grid = MatrixGridUtility.Deserialize(_initialGridStateStr, Matrix.Size);
            _selector = new GridBuilderCursor(Rows);
        }

        private void ExecutePlayableTick()
        {
            Draw();
            ExecuteQueueInputEvents();
        }

        private void ExecuteQueueInputEvents()
        {
            foreach (var input in InputQueue)
                HandleInputEvent(input);

            InputQueue.Clear();
        }

        protected override void Draw()
        {
            _canvas.Clear();
            DrawEntities();
            _canvas = Matrix.InterfacedSwapOnVsync(_canvas);
        }

        private void HandleInputEvent(ControllerInput input) 
        {
            switch (input)
            {
                case ControllerInput.ACTION_A:
                    Grid[_selector.X, _selector.Y].PlaceNewTile(MatrixGridTileType.BLOCK);
                    break;
                case ControllerInput.ACTION_B:
                    var tileUnderSelector = Grid[_selector.X, _selector.Y];
                    if (tileUnderSelector.Type == MatrixGridTileType.START)
                    {
                        tileUnderSelector.ClearTile();
                        break;
                    }

                    var existingStartTiles = from MatrixGridTile tile in Grid
                                             where tile.Type == MatrixGridTileType.START
                                             select tile;
                    
                    existingStartTiles.ToList().ForEach(tile => tile.ClearTile());
                    tileUnderSelector.PlaceNewTile(MatrixGridTileType.START);
                    
                    break;
                case ControllerInput.ACTION_X:
                    Grid[_selector.X, _selector.Y].PlaceNewTile(MatrixGridTileType.TARGET);
                    break;
                case ControllerInput.ACTION_Y:
                    // does nothing atm
                    break;
                case ControllerInput.DELETE:
                    Grid[_selector.X, _selector.Y].ClearTile();
                    break;
                case ControllerInput.UP:
                    _selector.Move(0, 1);
                    break;
                case ControllerInput.DOWN:
                    _selector.Move(0, -1);
                    break;
                case ControllerInput.LEFT:
                    _selector.Move(-1, 0);
                    break;
                case ControllerInput.RIGHT:
                    _selector.Move(1, 0);
                    break;
            }
        }

        private void DrawEntities()
        {
            MatrixGridUtility.DrawGridLayer(_canvas, Grid, Matrix.Size);
            DrawSelector();
        }

        private void DrawSelector()
        {
            if (_selector.AsyncIsCurrentlyTransparent)
                return;

            _canvas.SetPixel(_selector.X, _selector.Y, MatrixGridTileColors.SELECTOR);
        }
    }

}
