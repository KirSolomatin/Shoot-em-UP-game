using shoot_em_up.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal class Enemy
    {
        //Enemy position
        public Point Position { get; private set; }

        private Point _size;

        //Enemy model
        private Image _texture = Image.FromFile(@"Resources\Enemy.png");

        public float speed = 0.3f;

        private int _spawnCoolDown = 0;

        public Enemy()
        {
            //Set the start position
            Position = Config.enemySpawnPoints[RandomHelper.Next(Config.enemySpawnPoints.Length - 1)];

            _size.X = 100;
            _size.Y = 100;
        }

        //Enemy movement
        private void Move(int interval)
        {
            Position = new Point(Position.X, Position.Y + (int)(speed * interval));
        }

        public void Update(int interval)
        {
            Move(interval);
            _spawnCoolDown -= interval;

            //SpawnEnemy();
            //if(_spawnCoolDown <= 0)
            //{
            //    SpawnEnemy();
            //    _spawnCoolDown = Config.enemySpawnCoolDown;
            //}
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(_texture, Position.X, Position.Y, _size.X, _size.Y);
        }

        public static void SpawnEnemy()
        {
            Game.enemyList.Add(new Enemy());
        }
    }
}
