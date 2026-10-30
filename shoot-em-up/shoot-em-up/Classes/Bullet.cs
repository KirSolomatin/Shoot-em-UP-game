using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace shoot_em_up
{
    public class Bullet
    {
        public int PosX { get; private set; }              //Horizontal bullet position
        public int PosY { get; private set; }              //Vertical bullet position

        //Note where the bullet came from
        public int StartY { get; private set; }
        public int StartX { get; private set; }
        public bool IsDestroyed { get; set; }// true when the bullet has travelled its path

        private float _bulletSpeed = 1.3f;                        //Bullet speed
        private int _bulletSizeX = 100;
        private int _bulletSizeY = 100;
        //Collision for an bullet 
        public Rectangle BulletCollision { get; set; }

        public BulletTypeList BulletType { get; private set; }
        private DirectionHelper.Directions _direction;       //The direction in which a bullet will travel if knight or bishop

        public enum BulletTypeList { Rock, Bishop, Knight };

        //Bullet's textures
        private Image _texture;
        private static readonly Image _rockTexture = Image.FromFile(@"Resources\Rock.png");
        private static readonly Image _bishopTexture = Image.FromFile(@"Resources\bishop.png");
        private static readonly Image _knightTexture = Image.FromFile(@"Resources\knight.png");

        public Bullet(Player player, BulletTypeList type)
        {
            PosX = player.PosX;
            PosY = player.PosY;

            StartY = PosY;
            StartX = PosX;

            BulletType = type;

            switch (BulletType)
            {
                case BulletTypeList.Rock:
                    _texture = _rockTexture;
                    break;

                case BulletTypeList.Bishop:
                    _texture = _bishopTexture;
                    _direction = DirectionHelper.ChooseRandomDirection();
                    break;

                case BulletTypeList.Knight:
                    _texture = _knightTexture;
                    _direction = DirectionHelper.ChooseRandomDirection();
                    break;

                default:
                    _texture = _rockTexture;
                    break;
            }
        }

        //Bullet vertical movement
        private void Move(int interval, BulletTypeList type)
        {
            switch (type)
            {
                //Rock movement
                case BulletTypeList.Rock:
                    PosY -= (int)(_bulletSpeed * interval);

                    //Update collision every tick 
                    BulletCollision = new Rectangle(PosX, PosY, _bulletSizeX / 2, _bulletSizeY / 2);
                    break;

                //Bishop movement
                case BulletTypeList.Bishop:
                    if (_direction == DirectionHelper.Directions.Left) PosX -= (int)(_bulletSpeed * interval);
                    else PosX += (int)(_bulletSpeed * interval);
                    PosY -= (int)(_bulletSpeed * interval);

                    //Update collision every tick 
                    BulletCollision = new Rectangle(PosX, PosY, _bulletSizeX / 2, _bulletSizeY / 4);
                    break;

                //Knight movement
                case BulletTypeList.Knight:
                    if (StartY - PosY < 200)          //Hasn't scrolled 200 px upwards yet
                    {
                        PosY -= (int)(_bulletSpeed * interval);
                    }
                    else if (Math.Abs(StartX - PosX) < 100)
                    {
                        if (_direction == DirectionHelper.Directions.Left)
                            PosX -= (int)(_bulletSpeed * interval);
                        else if (_direction == DirectionHelper.Directions.Right)
                            PosX += (int)(_bulletSpeed * interval);
                    }
                    else
                    {
                        IsDestroyed = true;
                    }

                    BulletCollision = new Rectangle(PosX, PosY, _bulletSizeX, _bulletSizeY);
                    break;
            }
        }

        // This method calculates the bullet's new state after
        // 'interval' milliseconds have elapsed
        public void Update(int interval)
        {
            Move(interval, this.BulletType);
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(_texture, PosX, PosY, _bulletSizeX, _bulletSizeY);
        }
    }
}
