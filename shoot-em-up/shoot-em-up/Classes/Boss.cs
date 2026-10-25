using shoot_em_up.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal class Boss
    {
        //Boss position
        public Point Position { get; private set; }

        private Point _size;

        //Enemy model
        private static readonly Image _texture = Image.FromFile(@"Resources\Boss.png");

        public float Speed { get; set; } = Config.DEFAULT_BOSS_SPEED;

        //Collision for a boss
        public Rectangle BossCollision { get; set; }

        private int _verticalDescent = 0;
        private int _direction = 1; // 1 = goes right, -1 = goes left

        private int _atackCooldown = Config.DEFAULT_BOSS_ATACK_COOLDOWN;
        //Enemy health, need to hit it 3 times to kill it.
        public int Health { get; set; } = 3;
        public Boss()
        {
            //Set the start position
            Position = new Point(0, -100);

            _size.X = 100;
            _size.Y = 100;

            BossCollision = new Rectangle(Position.X, Position.Y, _size.X, _size.Y);
        }

        //Enemy movement
        private void Move(int interval)
        {
            //If the boss hasn't moved down 100 pixels yet
            if (_verticalDescent < 100)
            {
                Position = new Point(Position.X, Position.Y + (int)(Speed * interval));
                _verticalDescent += (int)(Speed * interval);
            }

            else
            {
                //Sideways movement
                int movementX = Position.X + _direction * (int)(Speed * interval);

                //It happens when the boss reaches one of the sides of the board
                if (movementX <= 0 || movementX >= Game.gameSize.X - this._size.X)
                {
                    movementX = Math.Clamp(movementX, 0, Game.gameSize.X - _size.X);
                    _verticalDescent = 0; //Start moving down again
                    _direction = -_direction; //Change direction
                }

                Position = new Point(movementX, Position.Y);
            }
        }

        public void Atack()
        {
            Game.enemyList.Add(new Enemy(Position));
        }

        public void Update(int interval)
        {
            Move(interval);

            _atackCooldown -= interval;

            //Boss spawn black pawn every second
            if (_atackCooldown <= 0)
            {
                Atack();
                _atackCooldown = Config.DEFAULT_BOSS_ATACK_COOLDOWN; //Reset atack cooldown
            }

            //Update collision every tick 
            BossCollision = new Rectangle(Position.X, Position.Y, _size.X, _size.Y);
        }

        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(_texture, Position.X, Position.Y, _size.X, _size.Y);
        }
    }
}
