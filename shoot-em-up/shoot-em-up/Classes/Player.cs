using shoot_em_up.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up.Classes
{
    internal class Player
    {
        public int posX { get; private set; } //horizontal player position
        public int posY { get; private set; } //vertical player position

        Image playerModel = Image.FromFile(@"Resources\King.png"); //player model

        public Player()
        {
            posX = 0; //Sets horizontal the player's position to 100
            posY = 0; //Sets vertical the player's position to 100
        }

        public void RightMove()
        {
            posX -= 100;
        }

        public void LeftMove()
        {
            posX += 100;
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(playerModel, posX, posY, 200, 200);
        }
    }
}
