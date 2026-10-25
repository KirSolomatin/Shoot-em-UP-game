using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal class Obstacle
    {
        public int PosX { get; private set; }              //Horizontal obstacle position
        private int _posY = 600;              //Vertical obstacle position
        private int _sizeX = 100;
        private int _sizeY = 100;

        private static readonly Image _texture = Image.FromFile(@"Resources\Pawn.png");
        public Rectangle ObstacleCollision;

        public Obstacle (int posX)
        {
            this.PosX = posX;

            //Initialization collision for an obstacle
            ObstacleCollision = new Rectangle(posX, _posY, _sizeX / 2, _sizeY / 10);
        }

        //Methode who can generate any obstacle's number 
        public static List<Obstacle> GenerateObstacle(int numbreObstacle)
        {
            List<Obstacle> obstaclesList = new List<Obstacle>();
            List<int> usedIndex = new List<int>();              //List of used position, so that pawns don't land on top of each other
            Random random = new Random();                       //For generate a random position

            for (int i = 0; i < numbreObstacle; i++)
            {
                int positionIndex = random.Next(0, 8);

                if (!usedIndex.Contains(positionIndex))
                {
                    usedIndex.Add(positionIndex);
                    Obstacle obstacle = new Obstacle(100 * positionIndex);
                    obstaclesList.Add(obstacle);
                }
                else i--;
            }
            return obstaclesList;
        }
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(_texture, PosX, _posY, _sizeX, _sizeY);
        }

    }
}
