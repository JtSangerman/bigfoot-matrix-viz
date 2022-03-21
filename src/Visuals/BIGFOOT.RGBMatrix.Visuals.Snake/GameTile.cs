using BIGFOOT.RGBMatrix.Visuals.Snake.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BIGFOOT.RGBMatrix.Visuals.Snake
{
    public class GameTile
    {
        public Tile Type { get; set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int? Id { get; set; }

        private const int BOARD_SIZE = 32; // TODO hardcoded for now
        
        public GameTile(int x, int y, Tile type, int? id = null)
        {
            X = x;
            Y = y;
            Type = type;

            Id = id;
        }

        public GameTile Copy()
        {
            return new GameTile(X, Y, Type, Id);
        }

        public void Up()
        {
            if ((Y + 1) > (BOARD_SIZE - 1))
            {
                Y = 0;
            }
            else
            {
                Y++;
            }
        }

        public void Down()
        {
            if ((Y - 1) < 0)
            {
                Y = BOARD_SIZE - 1;
            }
            else
            {
                Y--;
            }
        }

        public void Right()
        {
            if ((X + 1) > BOARD_SIZE - 1)
            {
                X = 0;
            }
            else
            {
                X++;
            }
        }

        public void Left()
        {
            if ((X - 1) < 0)
            {
                X = BOARD_SIZE - 1;
            }
            else
            {
                X--;
            }
        }

        public static GameTile Copy(GameTile g)
        {
            return new GameTile(g.X, g.Y, g.Type, g.Id);
        }
    }
}
