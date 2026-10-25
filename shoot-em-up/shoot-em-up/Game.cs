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
        private GameStates _currentState = GameStates.Game;  //Current game state

        internal static Boss? boss;

        //Screen size
        public static Point gameSize { get; private set; } = new Point(800, 800);

        BufferedGraphicsContext currentContext;
        BufferedGraphics game;
        //The difficulty level and enemy speed increase as your score goes up
        private int _difficultyLevel = 0;
        private int _enemySpeedlevel = 0;

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
            if (_currentState == GameStates.Game)
            {
                if (e.KeyCode == Keys.D) player.RightMove();
                if (e.KeyCode == Keys.A) player.LeftMove();
                if (e.KeyCode == Keys.J) player.BulletSpawn(bulletList, Bullet.BulletTypeList.Rock);
                if (e.KeyCode == Keys.K) player.BulletSpawn(bulletList, Bullet.BulletTypeList.Bishop);
                if (e.KeyCode == Keys.L) player.BulletSpawn(bulletList, Bullet.BulletTypeList.Knight);
            }

            if ((_currentState == GameStates.GameOver || _currentState == GameStates.GameWon) && e.KeyCode == Keys.R) RestartGame();
        }

        // Displaying the current status
        private void Render()
        {
            game.Graphics.Clear(Color.White);

            switch(_currentState)
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

                    //If the boss spawned 
                    boss?.Render(game);
                    break;

                //If the game is done
                case GameStates.GameOver:
                    //Draw the background
                    game.Graphics.DrawImage(backGround, 0, 0, gameSize.X, gameSize.Y);
                    GameOver.Render(game);
                    break;
                //If the game is won
                case GameStates.GameWon:
                    //Draw the background
                    game.Graphics.DrawImage(backGround, 0, 0, gameSize.X, gameSize.Y);
                    GameWon.Render(game);
                    break;
            }
            game.Render();
        }

        // Calculate the new state after 'interval' milliseconds have elapsed
        private void Update(int interval)
        {
            //Difficulty level increasing every 20 points
            _difficultyLevel = ScoreSystem.Score / 20;

            _enemySpeedlevel = ScoreSystem.Score / 100;

            for (int i = bulletList.Count - 1; i >= 0; i--)
            {
                bulletList[i].Update(interval);

                //If bullet is out of game space
                if (bulletList[i].IsDestroyed || 
                    bulletList[i].PosY < -10 || 
                    bulletList[i].PosX < -50 || 
                    bulletList[i].PosX > 810) 
                    bulletList.Remove(bulletList[i]);
            }

            for (int i = enemyList.Count - 1; i >= 0; i--)
            {
                enemyList[i].Update(interval);
                //Grow up enemy speed
                enemyList[i].Speed = Math.Min(Config.DEFAULT_ENEMY_SPEED + _enemySpeedlevel * 0.1f, Config.MAXIMUM_ENEMY_SPEED);

                //Take away a player's life
                if (enemyList[i].Position.Y >= 700)
                {
                    enemyList.Remove(enemyList[i]);
                    player.Health--;
                }
            }

            player.Update(interval);

            //If player have more then 500 points, spawn the boss
            if(ScoreSystem.Score >= 550 && boss == null)
            {
                boss = new Boss();
            }

            //If the boss spawned 
            boss?.Update(interval);

            //Boss is dead if less than 1 hp
            if(boss?.Health <= 0)
            {
                boss = null;
                _currentState = GameStates.GameWon;
            }

            //The game is over if player have less than 1hp or boss get the final line
            if(player.Health <= 0 || boss?.Position.Y >= 700)
            {
                _currentState = GameStates.GameOver;
            }
            //Check if bullet intersects with obstacle every tick if the case delete bullet
            CollisionHelper.CheckBulletCollision();

            CollisionHelper.CheckEnemyCollision();
            //Counting down the time until the next enemy
            _enemySpawnCoolDown -= interval;

            //If cooldown under 0 spawn enemy. Stop enemy spawn if player has more than 500 points
            if (_enemySpawnCoolDown <= 0 && ScoreSystem.Score < 550)
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

            if (_currentState == GameStates.Game) this.Update(timer.Interval); //Update the gameplay only while the game is in progress, not on the loss screen
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

            //Creat new player
            player = new Player();
            // Reset the difficulty, score and the enemy spawn timer
            Config.EnemySpawnCoolDown = Config.DEFAULT_ENEMY_SPAWN_COOLDOWN;
            _enemySpawnCoolDown = Config.EnemySpawnCoolDown;
            _difficultyLevel = 0;
            ScoreSystem.Score = 0;

            //Reset boss
            boss = null;
            _currentState = GameStates.Game;
        }
    }
}
