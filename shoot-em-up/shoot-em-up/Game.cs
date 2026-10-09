namespace shoot_em_up
{
    public partial class Game : Form
    {
        private Player player = new Player(); //Player
        internal static List<Bullet> bulletList = new List<Bullet>(); //List of bullets
        internal static List<Obstacle> obstacleList= new List<Obstacle>(); // List of obstacles
        internal static List<Enemy> enemyList = new List<Enemy>();
        Image backGround = Image.FromFile(@"Resources\background.png"); //

        BufferedGraphicsContext currentContext;
        BufferedGraphics game;

        public Game()
        {
            InitializeComponent();
            this.KeyPreview = true;                             //Allows keyboard input to be detected even when the focus is on another element

            obstacleList = Obstacle.GenerateObstacle(3);

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            game = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            Render();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D) player.RightMove();
            if (e.KeyCode == Keys.A) player.LeftMove();
            if (e.KeyCode == Keys.J) player.BulletSpawn(bulletList, Bullet.BulletType.Rock);
            if (e.KeyCode == Keys.K) player.BulletSpawn(bulletList, Bullet.BulletType.Bishop);
            if (e.KeyCode == Keys.L) player.BulletSpawn(bulletList, Bullet.BulletType.Knight);
        }

        // Displaying the current status
        private void Render()
        {
            game.Graphics.Clear(Color.White);

            //Draw the background
            game.Graphics.DrawImage(backGround, 0, 0, 800, 800);

            //Draw the player
            player.Render(game);

            //Draw bullets
            foreach (Bullet bulletRock in bulletList)
            {
                bulletRock.Render(game);
            }

            foreach (Obstacle obstacle in obstacleList)
            {
                obstacle.Render(game);
            }

            foreach (Enemy enemy in enemyList)
            {
                enemy.Render(game);
            }
            game.Render();
        }

        // Calculate the new state after 'interval' milliseconds have elapsed
        private void Update(int interval)
        {

            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                bulletList[i].Update(interval);

                //If bullet is out of game space
                if (bulletList[i].isDestroyed || 
                    bulletList[i].posY < -10 || 
                    bulletList[i].posX < -50 || 
                    bulletList[i].posX > 810) 
                    bulletList.Remove(bulletList[i]);
            }

            foreach (Enemy enemy in enemyList)
            {
                enemy.Update(interval);
            }

            player.Update(interval);

            Enemy.SpawnEnemy();
            //Check if bullet intersects with obstacle every tick
            CheckWallCollision();
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

        private void CheckWallCollision()
        {

            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < obstacleList.Count; j++)
                {
                    if (bulletList[i].bulletCollision.IntersectsWith(obstacleList[j].obstacleCollision) && bulletList[i].bulletType != Bullet.BulletType.Knight)
                    {
                        bulletList.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}
