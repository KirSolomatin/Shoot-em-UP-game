using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal class Bullet
    {
        public int posX { get; private set; }              //Horizontal bullet position
        public int posY { get; private set; }              //Vertical bullet position
        private int bulletSpeed = 1;                        //Bullet speed
        private int bulletSizeX = 50;
        private int bulletSizeY = 50;

        private BulletType bulletType;
        private DirectionHelper.Directions direction;       //The direction in which a bullet will travel if knight or bishop

        public enum BulletType {Rock, Bishop, Knight};

        //Bullet texture
        private Image texture;

        public Bullet(Player player, BulletType type)
        {
            posX = player.posX;
            posY = player.posY;

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
        }

        //Bullet vertical movement
        private void BulletMovement(int interval, BulletType type)
        {
            switch (type)
            {
                //Rock movement
                case BulletType.Rock:
                    posY -= bulletSpeed * interval;
                    break;

                //Bishop movement
                case BulletType.Bishop:
                    posY -= bulletSpeed * interval;
                    if (direction == DirectionHelper.Directions.Left) posX -= bulletSpeed * interval;
                    else posX += bulletSpeed * interval;
                    break;

                //Knight movement
                case BulletType.Knight:
                    while(posY < 600)
                    {
                        posY -= bulletSpeed * interval;
                    }
                    if (direction == DirectionHelper.Directions.Left) posX -= bulletSpeed * interval;
                    else posX += bulletSpeed * interval;
                    break;
            }
        }

        // This method calculates the bullet's new state after
        // 'interval' milliseconds have elapsed
        public void Update(int interval)
        {
            BulletMovement(interval, this.bulletType);
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(texture, posX + bulletSizeX/2, posY + bulletSizeX / 2, bulletSizeX, bulletSizeY);
        }
    }
}
