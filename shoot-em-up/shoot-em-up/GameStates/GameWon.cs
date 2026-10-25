using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal static class GameWon
    {
        // Stores the position where the phrases will be drawn
        private static Point _positionYouWon = new Point(Game.gameSize.X / 2, Game.gameSize.Y / 2 - 100);
        private static Point _positionYourScore = new Point(Game.gameSize.X / 2, Game.gameSize.Y / 2);
        private static Point _positionRestart = new Point(Game.gameSize.X / 2, Game.gameSize.Y / 2 + 200);

        // Stores the collection of custom fonts
        private static PrivateFontCollection _fonts = new PrivateFontCollection();
        private static Font _font;
        private static Brush _brush = Brushes.Black; // Defines the color of the text
        //Centers the text
        private static StringFormat _center = new StringFormat { Alignment = StringAlignment.Center };

        static GameWon()
        {
            // Loads the font from a file
            _fonts.AddFontFile(@"fonts\PressStart2P-Regular.ttf");

            // Defines the font and its size
            _font = new Font(_fonts.Families[0], 24f, FontStyle.Regular);
        }

        // Draws the phrases
        public static void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawString($"You won", _font, _brush, _positionYouWon.X, _positionYouWon.Y, _center);
            drawingSpace.Graphics.DrawString($"Your score: {ScoreSystem.Score}", _font, _brush, _positionYourScore.X, _positionYourScore.Y, _center);
            drawingSpace.Graphics.DrawString($"Press R to restart", _font, _brush, _positionRestart.X, _positionRestart.Y, _center);
        }
    }
}
