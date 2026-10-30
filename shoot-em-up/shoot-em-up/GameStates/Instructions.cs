using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal class Instructions
    {
        // Stores the position where the phrases will be drawn
        private static Point _positionTitle = new Point(Game.gameSize.X / 2, 100);
        private static Point _positionLeftMove = new Point(Game.gameSize.X / 2, 200);
        private static Point _positionRightMove = new Point(Game.gameSize.X / 2, 240);
        private static Point _positionRockAtack = new Point(Game.gameSize.X / 2, 280);
        private static Point _positionBishopAtack = new Point(Game.gameSize.X / 2,  320);
        private static Point _positionKnightAtack = new Point(Game.gameSize.X / 2, 360);
        private static Point _positionExit = new Point(Game.gameSize.X / 2, 600);

        // Stores the collection of custom fonts
        private static PrivateFontCollection _fonts = new PrivateFontCollection();
        private static Font _fontTitle;
        private static Font _fontText;
        private static Brush _brush = Brushes.Black; // Defines the color of the text
        //Centers the text
        private static StringFormat _center = new StringFormat { Alignment = StringAlignment.Center };

        static Instructions()
        {
            // Loads the font from a file
            _fonts.AddFontFile(@"fonts\PressStart2P-Regular.ttf");

            // Defines the font and its size
            _fontTitle = new Font(_fonts.Families[0], 24f, FontStyle.Regular);
            _fontText = new Font(_fonts.Families[0], 18f, FontStyle.Regular);
        }

        // Draws the phrases
        public static void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawString($"How to play", _fontTitle, _brush, _positionTitle.X, _positionTitle.Y, _center);
            drawingSpace.Graphics.DrawString($"Press A to move left", _fontText, _brush, _positionLeftMove.X, _positionLeftMove.Y, _center);
            drawingSpace.Graphics.DrawString($"Press D to move right", _fontText, _brush, _positionRightMove.X, _positionRightMove.Y, _center);
            drawingSpace.Graphics.DrawString($"Press J to atack with rock", _fontText, _brush, _positionRockAtack.X, _positionRockAtack.Y, _center);
            drawingSpace.Graphics.DrawString($"Press K to atack with bishop", _fontText, _brush, _positionBishopAtack.X, _positionBishopAtack.Y, _center);
            drawingSpace.Graphics.DrawString($"Press L to atack with knight", _fontText, _brush, _positionKnightAtack.X, _positionKnightAtack.Y, _center);
            drawingSpace.Graphics.DrawString($"Press escape", _fontText, _brush, _positionExit.X, _positionExit.Y, _center);
            drawingSpace.Graphics.DrawString($"to go back to main menu now", _fontText, _brush, _positionExit.X, _positionExit.Y +40, _center);
        }
    }
}
