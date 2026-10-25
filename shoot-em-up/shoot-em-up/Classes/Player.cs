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
        public int PosX { get; private set; } //horizontal player position
        public int PosY { get; private set; } //vertical player position

        private static readonly Image _playerModel = Image.FromFile(@"Resources\King.png"); //player model

        private int _coolDown = 0; // Interwal between shots
        public int Health { get; set; } = Config.PLAYER_MAX_HEALTH;
        private static readonly Image _fullHeart = Image.FromFile(@"Resources\Full_heart.png");
        private static readonly Image _emptyHeart = Image.FromFile(@"Resources\Empty_heart.png");
        public Image[] HealthUI { get; set; }
        public Player()
        {
            PosX = Config.PLAYER_START_X_POSITION; //Sets horizontal the player's position from start
            PosY = Config.PLAYER_START_Y_POSITION; //Sets vertical the player's position from start

            HealthUI = new Image[Health];
            for (int i = 0; i < HealthUI.Length; i++)
            {
                HealthUI[i] = i < Health ? _fullHeart : _emptyHeart;
            }
        }

        //Movement to the right
        public void LeftMove()
        {
            if(PosX > 0) PosX -= 100;
        }

        //Movement to the left
        public void RightMove()
        {
            if(PosX < 700) PosX += 100;
        }

        //Spawn bullets
        public void BulletSpawn(List<Bullet> bullets, Bullet.BulletTypeList bulletType)
        {
            if (_coolDown < 0)
            {
                bullets.Add(new Bullet(this, bulletType));
                _coolDown = Config.COOL_DOWN;
            }
        }

        public void Update(int interval)
        {
            _coolDown -= interval;


            for (int i = 0; i < HealthUI.Length; i++)
            {
                HealthUI[i] = i < Health ? _fullHeart : _emptyHeart;
            }
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            //Draw player
            drawingSpace.Graphics.DrawImage(_playerModel, PosX, PosY, Config.PLAYER_X_SIZE, Config.PLAYER_Y_SIZE);
            for (int i = 0; i < HealthUI.Length; i++)
            {
                drawingSpace.Graphics.DrawImage(HealthUI[i], 600 + 50 * i, 20, 25, 25);
            }
        }
    }
}
