using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal class Obstacle
    {
        public int posX { get; private set; }              //Horizontal obstacle position
        private int posY = 600;              //Vertical obstacle position
        private int sizeX = 100;
        private int sizeY = 100;

        private Image texture = Image.FromFile(@"Resources\Pawn.png");
        public Rectangle obstacleCollision;

        public Obstacle (int posX)
        {
            this.posX = posX;

            //Initialization collision for an obstacle
            obstacleCollision = new Rectangle(posX, posY, sizeX, sizeY);
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
            drawingSpace.Graphics.DrawImage(texture, posX, posY, sizeX, sizeY);
        }

    }
}
