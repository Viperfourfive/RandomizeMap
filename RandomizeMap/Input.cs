using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RandomizeMap
{
    class Input : Game
    {
        public KeyboardState newInput;
        public KeyboardState oldInput;

        public Input()
        {
            
        }
        public Vector2 PlayerMovement(Vector2 playerPosition, Rectangle boardSize)
        {
            //Used for comparing position on game board below, must be before if()
            Vector2 playerPositionOld = playerPosition;
            
            newInput = Keyboard.GetState(); 
            if (newInput.IsKeyDown(Keys.Right))
            {
                if (!oldInput.IsKeyDown(Keys.Right))
                {
                    playerPosition.X += 50;
                }
            }
            else if (newInput.IsKeyDown(Keys.Left))
            {
                if (!oldInput.IsKeyDown(Keys.Left))
                {
                    playerPosition.X -= 50;
                }
            }
            else if (newInput.IsKeyDown(Keys.Up))
            {
                if (!oldInput.IsKeyDown(Keys.Up))
                {
                    playerPosition.Y -= 50;
                }
            }
            else if (newInput.IsKeyDown(Keys.Down))
            {
                if (!oldInput.IsKeyDown(Keys.Down))
                {
                    playerPosition.Y += 50;
                }
            }
            //Used for allows one movement per keystroke
            oldInput = newInput;

            //Confine movement to gameboard only            
            if (boardSize.Contains(playerPosition))
            {
                //do nothing
            }
            else
            {
                playerPosition = playerPositionOld;
            }
            return playerPosition;
        }
    }
    


}
