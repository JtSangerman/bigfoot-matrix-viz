using BIGFOOT.RGBMatrix.Visuals.Inputs;
using BIGFOOT.RGBMatrix.Visuals.Snake.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using BIGFOOT.RGBMatrix.Visuals.Snake.Constants;
using rpi_rgb_led_matrix_sharp;

namespace BIGFOOT.RGBMatrix.Visuals.Snake
{
    public class Snake : Game
    {
        private const int DEFAULT_TICK_RATE_MS = 50;
        private const int PAUSE_BLINK_TICK_RATE_MS = 500;

        private readonly SnakeGameState _state;
        private Direction _currentDirection = Direction.UP;
        private RGBLedCanvas _canvas;

        public Snake(RGBLedMatrix matrix, ControllerInputDriverBase input) : base(matrix, input) 
        {
            _state = new SnakeGameState(matrix.GetCanvas().Width);
        }

        public async override void Run()
        {
            _canvas = Matrix.GetCanvas();

            while (!_state.IsGameOver)
            {
                await ExecuteGameTick();
                Thread.Sleep(Paused ? PAUSE_BLINK_TICK_RATE_MS : DEFAULT_TICK_RATE_MS);
            }
        }

        private async Task ExecuteGameTick()
        {
            if (!ControllerConnected)
            {
                DrawNoController();
                return;
            }

            if (!Paused)
            {
                await _state.Tick(_currentDirection);

                if (_state.IsGameOver)
                {
                    return;
                }
            }

            Draw();
        }

        private void ExecuteWaitingForControllerConnectionTick()
        {
            DrawNoController();
        }

        private bool _blinking = false;
        protected override void Draw()
        {
            _canvas.Clear();
            
            if (Paused)
            {
                _blinking = !_blinking;
            }

            for (int i = 0; !_blinking && i < _state.Board.GetLength(0); i++)
            {
                for (int j = 0; j < _state.Board.GetLength(1); j++)
                {
                    var color = GetColor(_state.Board[i, j], 1.0);
                    _canvas.SetPixel(i, j, color);
                }
            }

            _canvas = Matrix.SwapOnVsync(_canvas);
        }

        private void DrawNoController()
        {
            _canvas.Clear();
            for (int i = 0; i < _state.Board.GetLength(0); i++)
            {
                for (int j = 0; j < _state.Board.GetLength(1); j++)
                {
                    _canvas.SetPixel(i, j, new Color(123,123,123));
                }
            }
            _canvas = Matrix.SwapOnVsync(_canvas);
        }

        private Color GetColor(Tile tile, double opacity = 1)
        {
            Color color = TileColor.EMPTY;
            switch (tile)
            {
                case Tile.GOAL:
                    color = TileColor.GOAL;
                    break;
                case Tile.SNAKE_BODY:
                    color = TileColor.SNAKE_BODY;
                    break;
                case Tile.SNAKE_HEAD:
                    color = TileColor.SNAKE_HEAD;
                    break;
                default:
                    color = TileColor.EMPTY;
                    break;
            }

            color.R = (byte)Math.Floor(color.R * opacity);
            color.G = (byte)Math.Floor(color.G * opacity);
            color.B = (byte)Math.Floor(color.B * opacity);

            return color;
        }

        // input even handlers
        ////////////////////////

        protected override void Handle_E_INPUT_UP()
        {
            HandleDirectionInput(Direction.UP);
        }

        protected override void Handle_E_INPUT_DOWN()
        {
            HandleDirectionInput(Direction.DOWN);
        }

        protected override void Handle_E_INPUT_LEFT()
        {
            HandleDirectionInput(Direction.LEFT);
        }

        protected override void Handle_E_INPUT_RIGHT()
        {
            HandleDirectionInput(Direction.RIGHT);
        }

        protected override async void Handle_E_INPUT_EXT1()
        {
            base.Handle_E_INPUT_EXT1();
            _blinking = Paused;
            await ExecuteGameTick();
        }

        private void HandleDirectionInput(Direction direction)
        {
            var Debug_msg = $"Direction.{direction}";

            if (DirectionalUtils.CheckConflicted(_currentDirection, direction))
            {
                Debug_msg = $"{Debug_msg} (CONFLICTED)";
            }
            else
            {
                _currentDirection = direction;
            }

            Debug_UpdateCurrentControllerInputOutput(Debug_msg, typeof(Snake).Name);
        }

        ///// debug
        //////////////////
        private void Debug_enumerateInputQueue()
        {
            if (_inputQueue?.Count == 0) return;

            Console.WriteLine("Input queue: \n\n");

            var num = 0;
            _inputQueue.ForEach(i =>
            {
                Console.WriteLine($"{num++}: {i}");
            });
        }
    }
}
