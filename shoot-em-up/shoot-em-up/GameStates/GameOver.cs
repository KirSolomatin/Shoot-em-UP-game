using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal static class GameOver
    {
        // Stores the position where the phrases will be drawn
        private static Point positionCheckmate = new Point(Game.gameSize.X / 2, Game.gameSize.Y / 2 - 100);
        private static Point positionYourScore = new Point(Game.gameSize.X / 2, Game.gameSize.Y / 2);
        private static Point positionRestart = new Point(Game.gameSize.X / 2, Game.gameSize.Y / 2 + 200);

        // Stores the collection of custom fonts
        private static PrivateFontCollection fonts = new PrivateFontCollection();
        private static Font font;
        private static Brush brush = Brushes.Black; // Defines the color of the text
        //Centers the text
        private static StringFormat center = new StringFormat { Alignment = StringAlignment.Center };

        static GameOver()
        {
            // Loads the font from a file
            fonts.AddFontFile(@"fonts\PressStart2P-Regular.ttf");

            // Defines the font and its size
            font = new Font(fonts.Families[0], 24f, FontStyle.Regular);
        }

        // Draws the phrases
        public static void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawString($"Checkmate", font, brush, positionCheckmate.X, positionCheckmate.Y, center);
            drawingSpace.Graphics.DrawString($"Your score: {ScoreSystem.Score}", font, brush, positionYourScore.X, positionYourScore.Y, center);
            drawingSpace.Graphics.DrawString($"Press R to restart", font, brush, positionRestart.X, positionRestart.Y, center);
        }
    }
}
