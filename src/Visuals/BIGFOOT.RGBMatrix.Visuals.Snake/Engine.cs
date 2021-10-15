using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.Visuals.Snake
{
    public class Engine
    {
        private readonly int _tickMs;
        private readonly GameState _gameState;
        public Engine(int boardSize, int tickMS)
        {
            _tickMs = tickMS;
            _gameState = new GameState(boardSize);
        }

        public void Start()
        {

        }
    }
}
