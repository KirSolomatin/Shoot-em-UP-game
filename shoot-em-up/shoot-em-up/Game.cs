using shoot_em_up.Classes;

namespace shoot_em_up
{
    public partial class Game : Form
    {
        private Player player = new Player();

        public Game()
        {
            InitializeComponent();
            this.KeyPreview = true;                             //Permet de détecter les frappes au clavier même lorsque le focus se trouve sur un autre élément

            Render();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) player.RightMove();
            if (e.KeyCode == Keys.A) player.LeftMove();
        }

        private void Render()
        {
            using Graphics graphics = CreateGraphics();

            graphics.DrawImage(player.playerTexture, player.posX, player.posY);
        }
    }
}
