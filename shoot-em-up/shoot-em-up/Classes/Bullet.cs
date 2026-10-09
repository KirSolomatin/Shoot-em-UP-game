using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace shoot_em_up
{
    internal class Bullet
    {
        public int posX { get; private set; }              //Horizontal bullet position
        public int posY { get; private set; }              //Vertical bullet position

        //Note where the bullet came from
        public int startY { get; private set; }
        public int startX { get; private set; }
        public bool isDestroyed { get; set; }// true when the bullet has travelled its path

        private float bulletSpeed = 1.3f;                        //Bullet speed
        private int bulletSizeX = 50;
        private int bulletSizeY = 50;
        //Collision for an bullet 
        public Rectangle bulletCollision;

        public BulletType bulletType { get; private set; }
        private DirectionHelper.Directions direction;       //The direction in which a bullet will travel if knight or bishop

        public enum BulletType { Rock, Bishop, Knight };

        //Bullet texture
        private Image texture;

        public Bullet(Player player, BulletType type)
        {
            posX = player.posX;
            posY = player.posY;

            startY = posY;
            startX = posX;

            bulletType = type;

            switch (bulletType)
            {
                case BulletType.Rock:
                    texture = Image.FromFile(@"Resources\Rock.png");
                    break;

                case BulletType.Bishop:
                    texture = Image.FromFile(@"Resources\bishop.png");
                    direction = DirectionHelper.ChooseRandomDirection();
                    break;

                case BulletType.Knight:
                    texture = Image.FromFile(@"Resources\knight.png");
                    direction = DirectionHelper.ChooseRandomDirection();
                    break;

                default:
                    texture = Image.FromFile(@"Resources\Rock.png");
                    break;
            }

            bulletCollision = new Rectangle(posX + bulletSizeX / 2, posY - bulletSizeY / 2, bulletSizeX, bulletSizeY);
        }

        //Bullet vertical movement
        private void Move(int interval, BulletType type)
        {
            switch (type)
            {
                //Rock movement
                case BulletType.Rock:
                    posY -= (int)(bulletSpeed * interval);
                    break;

                //Bishop movement
                case BulletType.Bishop:
                    if (direction == DirectionHelper.Directions.Left) posX -= (int)(bulletSpeed * interval);
                    else posX += (int)(bulletSpeed * interval);
                    posY -= (int)(bulletSpeed * interval);
                    break;

                //Knight movement
                case BulletType.Knight:
                    if (startY - posY < 200)          //Hasn't scrolled 200 px upwards yet
                    {
                        posY -= (int)(bulletSpeed * interval);
                    }
                    else if (Math.Abs(startX - posX) < 100)
                    {
                        if (direction == DirectionHelper.Directions.Left)
                            posX -= (int)(bulletSpeed * interval);
                        else if (direction == DirectionHelper.Directions.Right)
                            posX += (int)(bulletSpeed * interval);
                    }
                    else
                    {
                        isDestroyed = true;
                    }
                    break;
            }

            //Update collision every tick 
            bulletCollision = new Rectangle(posX + bulletSizeX / 2, posY - bulletSizeY / 2, bulletSizeX, bulletSizeY);
        }

        // This method calculates the bullet's new state after
        // 'interval' milliseconds have elapsed
        public void Update(int interval)
        {
            Move(interval, this.bulletType);
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(texture, posX + bulletSizeX / 2, posY + bulletSizeY / 2, bulletSizeX, bulletSizeY);
        }
    }
}
