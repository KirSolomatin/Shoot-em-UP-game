using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal class BulletRock
    {
        public int posX { get; private set; }              //Horizontal bullet position
        public int posY { get; private set; }                //Vertical bullet position
        public int bulletSpeed = 1;         //Bullet speed
        public int bulletSizeX = 50;
        public int bulletSizeY = 50;

        Image rockImage = Image.FromFile(@"Resources\Rock.png");

        public BulletRock(Player player)
        {
            posX = player.posX;
            posY = player.posY;
        }

        //Bullet vertical movement
        private void BulletMovement(int interval)
        {
            posY -= bulletSpeed * interval;
        }

        // This method calculates the bullet's new state after
        // 'interval' milliseconds have elapsed
        public void Update(int interval)
        {
            BulletMovement(interval);
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(rockImage, posX + bulletSizeX/2, posY, bulletSizeX, bulletSizeY);
        }
    }
}
