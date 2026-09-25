
namespace shoot_em_up
{
    public partial class Game : Form
    {
        private Player player = new Player();
        private List<BulletRock> bulletRockList = new List<BulletRock>();
        Image backGround = Image.FromFile(@"Resources\background.png"); //

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
            if (e.KeyCode == Keys.J) player.BulletSpawn(bulletRockList);
        }

        private void Render()
        {
            game.Graphics.Clear(Color.White);
            game.Graphics.DrawImage(backGround, 0, 0, 800, 800);

            player.Render(game);
            foreach (BulletRock bulletRock in bulletRockList)
            {
                bulletRock.Render(game);
            }
            game.Render();
        }

        // Calculate the new state after 'interval' milliseconds have elapsed
        private void Update(int interval)
        {
            foreach (BulletRock bulletRock in bulletRockList)
            {
                bulletRock.Update(interval);
            }

            player.Update(interval);
        }

        //Method called every frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Render();
            this.Update(timer.Interval);
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }
    }
}
