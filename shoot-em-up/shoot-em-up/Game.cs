using shoot_em_up.Helpers;

namespace shoot_em_up
{
    public partial class Game : Form
    {
        private Player player = new Player(); //Player
        internal static List<Bullet> bulletList = new List<Bullet>(); //List of bullets
        internal static List<Obstacle> obstacleList= new List<Obstacle>(); // List of obstacles
        internal static List<Enemy> enemyList = new List<Enemy>(); //List of enemys
        private int _enemySpawnCoolDown = Config.EnemySpawnCoolDown;
        Image backGround = Image.FromFile(@"Resources\background.png"); //Background texture
        private GameStates currentState = GameStates.Game;  //Current game state

        //Screen size
        public static Point gameSize { get; private set; } = new Point(800, 800);

        BufferedGraphicsContext currentContext;
        BufferedGraphics game;
        //The difficulty level increases as your score goes up
        private int _difficultyLevel = 0;

        public Game()
        {
            InitializeComponent();
            this.KeyPreview = true;                             //Allows keyboard input to be detected even when the focus is on another element

            obstacleList = Obstacle.GenerateObstacle(6);

            // Gets a reference to the current BufferedGraphicsContext
            currentContext = BufferedGraphicsManager.Current;
            // Creates a BufferedGraphics instance associated with this form, and with
            // dimensions the same size as the drawing surface of the form.
            game = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            Render();
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (currentState == GameStates.Game)
            {
                if (e.KeyCode == Keys.D) player.RightMove();
                if (e.KeyCode == Keys.A) player.LeftMove();
                if (e.KeyCode == Keys.J) player.BulletSpawn(bulletList, Bullet.BulletType.Rock);
                if (e.KeyCode == Keys.K) player.BulletSpawn(bulletList, Bullet.BulletType.Bishop);
                if (e.KeyCode == Keys.L) player.BulletSpawn(bulletList, Bullet.BulletType.Knight);
            }

            if (currentState == GameStates.GameOver && e.KeyCode == Keys.R) RestartGame();
        }

        // Displaying the current status
        private void Render()
        {
            game.Graphics.Clear(Color.White);

            switch(currentState)
            {
                //If the game in progress 
                case GameStates.Game:

                    //Draw the background
                    game.Graphics.DrawImage(backGround, 0, 0, gameSize.X, gameSize.Y);

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

                    ScoreSystem.Render(game);

                    break;

                //If the game is done
                case GameStates.GameOver:
                    //Draw the background
                    game.Graphics.DrawImage(backGround, 0, 0, gameSize.X, gameSize.Y);
                    GameOver.Render(game);
                    break;
            }

            game.Render();

        }

        // Calculate the new state after 'interval' milliseconds have elapsed
        private void Update(int interval)
        {
            //Difficulty level increasing every 30 points
            _difficultyLevel = ScoreSystem.Score / 30;

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

            for (int i = enemyList.Count - 1; i >= 0; i--)
            {
                enemyList[i].Update(interval);

                //Loss condition
                if (enemyList[i].Position.Y >= 700)
                {
                    currentState = GameStates.GameOver;
                }

                enemyList[i].speed = Math.Min(Config.DEFAULT_ENEMY_SPEED + _difficultyLevel * 0.1f, Config.MAXIMUM_ENEMY_SPEED);
            }

            player.Update(interval);

            //Check if bullet intersects with obstacle every tick if the case delete bullet
            CollisionHelper.CheckBulletCollision();

            CollisionHelper.CheckEnemyCollision();
            //Counting down the time until the next enemy
            _enemySpawnCoolDown -= interval;

            //If cooldown under 0 spawn enemy
            if (_enemySpawnCoolDown <= 0)
            {
                Enemy.SpawnEnemy();
                //Calculate new enemy cooldown based on difficulty level
                _enemySpawnCoolDown = Math.Max(Config.MINIMUM_ENEMY_SPAWN_COOLDOWN, Config.DEFAULT_ENEMY_SPAWN_COOLDOWN - _difficultyLevel * 100);
            }

            ScoreSystem.Update(interval);
        }

        //Method called every frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Render();

            if (currentState == GameStates.Game) this.Update(timer.Interval); //Update the gameplay only while the game is in progress, not on the loss screen
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void RestartGame()
        {
            // Clear away everything left over from the previous batch
            bulletList.Clear();
            enemyList.Clear();
            obstacleList = Obstacle.GenerateObstacle(6);

            //
            player = new Player();
            // Reset the difficulty, score and the enemy spawn timer
            Config.EnemySpawnCoolDown = Config.DEFAULT_ENEMY_SPAWN_COOLDOWN;
            _enemySpawnCoolDown = Config.EnemySpawnCoolDown;
            _difficultyLevel = 0;
            ScoreSystem.Score = 0;
            
            currentState = GameStates.Game;
        }
    }
}
