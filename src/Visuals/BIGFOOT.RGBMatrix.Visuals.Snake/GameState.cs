using BIGFOOT.RGBMatrix.Visuals.Snake.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.Visuals.Snake
{
    public class GameState
    {
        private readonly int _boardSize;
        private readonly TileType[,] _board;
        public GameState(int boardSize)
        {
            _boardSize = boardSize;
            _board = new TileType[boardSize, boardSize];

            Init();
        }


        private void Init()
        {
            // set all board tiles to default
            for (int i = 0; i < _boardSize * _boardSize; i++) 
                _board[i % _boardSize, i / _boardSize] = TileType.Empty;

            // set snake start

            // set start direction

            //set first goal
        }
    }
}
