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
        public static int Score { get; set; } = 0; // Stores the current score
        private static Point _position = new Point(0, 0); // Stores the position where the score will be drawn

        // Stores the collection of custom fonts
        private static PrivateFontCollection _fonts = new PrivateFontCollection();
        private static Font _font; 
        private static Brush _brush = Brushes.Black; // Defines the color of the text

        private static int _scoreCoolDown = Config.TIME_FOR_POINT;

        static ScoreSystem ()
        {
            // Loads the font from a file
            _fonts.AddFontFile(@"fonts\PressStart2P-Regular.ttf");

            // Defines the font and its size
            _font = new Font(_fonts.Families[0], 16f, FontStyle.Regular);
        }

        //Increases points over time
        public static void Update(int interval)
        {
            _scoreCoolDown -= interval;

            if(_scoreCoolDown <= 0)
            {
                Score++;
                _scoreCoolDown = Config.TIME_FOR_POINT;
            }
        }

        // Draws the score string at the specified 
        public static void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawString($"Score:{Score}", _font, _brush, _position.X, _position.Y);
        }
    }
}
