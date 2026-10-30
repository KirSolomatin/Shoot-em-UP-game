using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal class MainMenu
    {
        // Stores the position where the phrases will be drawn
        private static Point _positionTitle = new Point(Game.gameSize.X / 2, Game.gameSize.Y / 4);
        private static Point _positionStart = new Point(Game.gameSize.X / 2, Game.gameSize.Y / 3);
        private static Point _positionHowToPlay = new Point(Game.gameSize.X / 2, Game.gameSize.Y / 3 + 200);

        // Stores the collection of custom fonts
        private static PrivateFontCollection _fonts = new PrivateFontCollection();
        private static Font _font;
        private static Brush _brush = Brushes.Black; // Defines the color of the text
        //Centers the text
        private static StringFormat _center = new StringFormat { Alignment = StringAlignment.Center };

        static MainMenu()
        {
            // Loads the font from a file
            _fonts.AddFontFile(@"fonts\PressStart2P-Regular.ttf");

            // Defines the font and its size
            _font = new Font(_fonts.Families[0], 24f, FontStyle.Regular);
        }

        // Draws the phrases
        public static void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawString($"Chessageddon", _font, _brush, _positionTitle.X, _positionTitle.Y, _center);
            drawingSpace.Graphics.DrawString($"Press Space to start", _font, _brush, _positionStart.X, _positionStart.Y, _center);
            drawingSpace.Graphics.DrawString($"Press H", _font, _brush, _positionHowToPlay.X, _positionHowToPlay.Y, _center);
            drawingSpace.Graphics.DrawString($"to know how to play", _font, _brush, _positionHowToPlay.X, _positionHowToPlay.Y + 100, _center);
        }
    }
}
