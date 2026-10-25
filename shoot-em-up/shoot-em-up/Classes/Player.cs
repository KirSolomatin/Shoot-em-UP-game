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

        private static readonly Image playerModel = Image.FromFile(@"Resources\King.png"); //player model

        private int coolDown = 0; // Interwal between shots
        public int Health { get; set; } = Config.PLAYER_MAX_HEALTH;
        private static readonly Image Full_heart = Image.FromFile(@"Resources\Full_heart.png");
        private static readonly Image Empty_heart = Image.FromFile(@"Resources\Empty_heart.png");
        public Image[] HealthUI { get; set; }
        public Player()
        {
            posX = Config.PLAYER_START_X_POSITION; //Sets horizontal the player's position from start
            posY = Config.PLAYER_START_Y_POSITION; //Sets vertical the player's position from start

            HealthUI = new Image[Health];
            for (int i = 0; i < HealthUI.Length; i++)
            {
                HealthUI[i] = i < Health ? Full_heart : Empty_heart;
            }
        }

        //Movement to the right
        public void LeftMove()
        {
            if(posX > 0) posX -= 100;
        }

        //Movement to the left
        public void RightMove()
        {
            if(posX < 700) posX += 100;
        }

        //Spawn bullets
        public void BulletSpawn(List<Bullet> bullets, Bullet.BulletType bulletType)
        {
            if (coolDown < 0)
            {
                bullets.Add(new Bullet(this, bulletType));
                coolDown = Config.COOL_DOWN;
            }
        }

        public void Update(int interval)
        {
            coolDown -= interval;


            for (int i = 0; i < HealthUI.Length; i++)
            {
                HealthUI[i] = i < Health ? Full_heart : Empty_heart;
            }
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            //Draw player
            drawingSpace.Graphics.DrawImage(playerModel, posX, posY, Config.PLAYER_X_SIZE, Config.PLAYER_Y_SIZE);
            for (int i = 0; i < HealthUI.Length; i++)
            {
                drawingSpace.Graphics.DrawImage(HealthUI[i], 600 + 50 * i, 20, 25, 25);
            }
        }
    }
}
