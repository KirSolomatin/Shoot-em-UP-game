using shoot_em_up.Classes;

namespace shoot_em_up
{
    public partial class Game : Form
    {
        private Player player = new Player();
        Image backGround = Image.FromFile(@"Resources\background.jpg"); //

        BufferedGraphicsContext currentContext;
        BufferedGraphics game;

        public Game()
        {
            InitializeComponent();
            this.KeyPreview = true;                             //Allows keyboard input to be detected even when the focus is on another element

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            game = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            Render();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) player.RightMove();
            if (e.KeyCode == Keys.D) player.LeftMove();
        }

        private void Render()
        {
            game.Graphics.Clear(Color.White);
            game.Graphics.DrawImage(backGround, 0, 0);

            player.Render(game);
            game.Render();
        }

        //Method called every frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Render();
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
