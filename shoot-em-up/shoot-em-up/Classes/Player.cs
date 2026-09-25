using shoot_em_up.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal class Player
    {
        public int posX { get; private set; } //horizontal player position
        public int posY { get; private set; } //vertical player position

        Image playerModel = Image.FromFile(@"Resources\King.png"); //player model

        public Player()
        {
            posX = Config.PLAYER_START_X_POSITION; //Sets horizontal the player's position from start
            posY = Config.PLAYER_START_Y_POSITION; //Sets vertical the player's position from start
        }

        //Movement to the right
        public void RightMove()
        {
            if(posX > 0) posX -= 100;
        }

        //Movement to the left
        public void LeftMove()
        {
            if(posX < 700) posX += 100;
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            //Draw player
            drawingSpace.Graphics.DrawImage(playerModel, posX, posY, Config.PLAYER_X_SIZE, Config.PLAYER_Y_SIZE);
        }
    }
}
