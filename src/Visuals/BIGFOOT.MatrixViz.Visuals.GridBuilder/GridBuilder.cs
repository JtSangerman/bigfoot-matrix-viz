using BIGFOOT.MatrixViz.DriverInterfacing;
using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;
using BIGFOOT.MatrixViz.Visuals.GridBuilder.Models;
using BIGFOOT.MatrixViz.Visuals.Models;
using System.Threading.Tasks;
using System.Linq;
using BIGFOOT.MatrixViz.Inputs.Drivers.Enums;
using System.Threading;
using BIGFOOT.MatrixViz.Visuals.GridBuilder.Enums;
using System.Collections.Generic;
using BIGFOOT.MatrixViz.Visuals.GridBuilder.Constants;

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
        public List<GridBuilderTile> Tiles { private set; get; }
        private GridTileSelector _selector { get; set; }
        private bool _finishedBuilding { get; set; }

        public GridBuilder(TMatrix matrix, ControllerInputDriverBase input) : base(matrix, input) { }

        public void Init()
        {
            _canvas = Matrix.InterfacedGetCanvas();
            _selector = new GridTileSelector(Rows);

            Grid = CreateEmptyGrid();
        }

        private GridBuilderTile[,] CreateEmptyGrid()
        {
            var grid = new GridBuilderTile[Rows, Rows];
            for (int i = 0; i < Rows * Rows; i++)
            {
                grid[i/Rows, i%Rows] = new GridBuilderTile();
            }

            return grid;
        }

        protected override void Run()
        {
            Init();

            while (!_finishedBuilding)
            {
                ExecutePlayableTick();
                Thread.Sleep(TickMs);
            }
        }

        private void ExecutePlayableTick()
        {
            HandleQueuedInputs();
            Draw();
        }

        private void HandleQueuedInputs()
        {
            foreach (var input in InputQueue)
                HandledQueuedDirectionInput(input);               

            InputQueue.Clear();
        }

        private void HandledQueuedDirectionInput(ControllerInput input) 
        {
            var prevSelectorPosition = (_selector.X, _selector.Y);
            switch (input)
            {
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
                case ControllerInput.ACTION_A:
                    _placeTileBtnActiveFramesLeft = 1;
                    Grid[_selector.X, _selector.Y].Type = GridBuilderTileType.BLOCK;
                    break;
            }

            if (prevSelectorPosition != (_selector.X, _selector.Y))
            {
                _placeTileBtnActiveFramesLeft = 0;
            }
        }

        private int _placeTileBtnActiveFramesLeft = 0;
        protected override void Handle_E_INPUT_ACTION_A(){ }

        protected override void Draw()
        {
            _canvas.Clear();
            DrawEntities();
            _canvas = Matrix.InterfacedSwapOnVsync(_canvas);
        }

        private void DrawEntities()
        {
            DrawTiles();
            DrawSelector();
        }

        private void DrawSelector()
        {
            var color = GridBuilderTileColors.SELECTOR_DEFAULT;
            if (_placeTileBtnActiveFramesLeft > 0)
            {
                color = GridBuilderTileColors.SELECTOR_ACTIVE;
                _placeTileBtnActiveFramesLeft--;
            }

            _canvas.SetPixel(_selector.X, _selector.Y, color);
        }

        private void DrawTiles()
        {
            for (int x = 0; x < Rows; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    if (Grid[x, y].IsEmpty || (x, y) == (_selector.X, _selector.Y))
                    {
                        continue;
                    }

                    _canvas.SetPixel(x, y, GridBuilderTileColors.BLOCK);
                }
            }
        }
    }

}
