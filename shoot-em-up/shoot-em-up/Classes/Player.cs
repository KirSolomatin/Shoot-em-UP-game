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

        private int rockCoolDown = 0; // Interwal between rock's shots

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

        public void BulletSpawn(List<BulletRock> rock)
        {
            if (rockCoolDown < 0)
            {
                rock.Add(new BulletRock(this));
                rockCoolDown = Config.ROCK_COOL_DOWN;
            }
        }

        public void Update(int interval)
        {
            rockCoolDown -= interval;
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            //Draw player
            drawingSpace.Graphics.DrawImage(playerModel, posX, posY, Config.PLAYER_X_SIZE, Config.PLAYER_Y_SIZE);
        }
    }
}
