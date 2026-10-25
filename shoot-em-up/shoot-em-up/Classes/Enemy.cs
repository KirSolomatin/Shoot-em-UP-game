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
        private static readonly Image _texture = Image.FromFile(@"Resources\Enemy.png");

        public float Speed { get; set; } = Config.DEFAULT_ENEMY_SPEED;

        //Collision for an enemy 
        public Rectangle EnemyCollision;

        public Enemy(Point position)
        {
            //Set the start position
            Position = position;

            _size.X = 100;
            _size.Y = 100;

            EnemyCollision = new Rectangle(Position.X, Position.Y, _size.X, _size.Y);
        }

        //Enemy movement
        private void Move(int interval)
        {
            Position = new Point(Position.X, Position.Y + (int)(Speed * interval));
        }

        public void Update(int interval)
        {
            Move(interval);

            //Update collision every tick 
            EnemyCollision = new Rectangle(Position.X, Position.Y, _size.X, _size.Y);
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(_texture, Position.X, Position.Y, _size.X, _size.Y);
        }

        public static void SpawnEnemy()
        {
            Game.enemyList.Add(new Enemy(Config.EnemySpawnPoints[RandomHelper.Next(Config.EnemySpawnPoints.Length)]));
        }
    }
}
