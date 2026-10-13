using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;

namespace shoot_em_up
{
    internal static class ScoreSystem
    {
        public static int score = 0; // Stores the current score
        private static Point position = new Point(0, 0); // Stores the position where the score will be drawn

        // Stores the collection of custom fonts
        private static PrivateFontCollection fonts = new PrivateFontCollection();
        private static Font font; 
        private static Brush brush = Brushes.Black; // Defines the color of the text

        private static int scoreCoolDown = Config.TIME_FOR_POINT;

        
        static ScoreSystem ()
        {
            // Loads the font from a file
            fonts.AddFontFile(@"fonts\PressStart2P-Regular.ttf");

            // Defines the font and its size
            font = new Font(fonts.Families[0], 16f, FontStyle.Regular);
        }

        //Increases points over time
        public static void Update(int interval)
        {
            scoreCoolDown -= interval;

            if(scoreCoolDown <= 0)
            {
                score++;
                scoreCoolDown = Config.TIME_FOR_POINT;
            }
        }

        // Draws the score string at the specified 
        public static void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawString($"Score:{score}", font, brush, position.X, position.Y);
        }
    }
}
