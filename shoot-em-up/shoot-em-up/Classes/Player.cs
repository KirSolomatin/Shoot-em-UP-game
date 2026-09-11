using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up.Classes
{
    internal class Player
    {
        public int posX { get; private set; }
        public int posY { get; private set; }

        public Image playerTexture { get; private set; }

        public Player()
        {
            posX = 100;
            posY = 100;

            string path = Path.Combine(
                AppContext.BaseDirectory,
                "Resources",
                "King.png"
            );

            MessageBox.Show(path);

            playerTexture = Image.FromFile(path);
        }

        public void RightMove()
        {
            posX += 100;
        }

        public void LeftMove()
        {
            posX -= 100;
        }
    }
}
