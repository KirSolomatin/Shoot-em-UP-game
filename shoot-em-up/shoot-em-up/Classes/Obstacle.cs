using shoot_em_up.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    public class Obstacle
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

            for (int i = 0; i < numbreObstacle; i++)
            {
                int positionIndex = RandomHelper.Next(0,8); //Take a random position

                if (!usedIndex.Contains(positionIndex))//If position is free 
                {
                    usedIndex.Add(positionIndex);//Add position to the list of used 
                    Obstacle obstacle = new Obstacle(100 * positionIndex);//Give position to an obstacle
                    obstaclesList.Add(obstacle);//Add obstacle to the list of obstacles
                }
                else i--;//If position wasn't free, cancel the stage
            }
            return obstaclesList;
        }
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(_texture, PosX, _posY, _sizeX, _sizeY);
        }

    }
}
