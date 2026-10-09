namespace shoot_em_up
{
    public static class Config
    {

        #region ================ Player ================

        public const int PLAYER_START_X_POSITION = 0;
        public const int PLAYER_START_Y_POSITION = 700;
        public const int PLAYER_X_SIZE = 100;
        public const int PLAYER_Y_SIZE = 100;

        #endregion

        #region ================ Bullet ================

        public const int COOL_DOWN = 300;

        #endregion

        #region ================ Enemy ================
        //Possible spawn points for enemy
        //There are 8 spawn points corresponding to each square on a chessboard
        //y position = -100, enemy spawns outside the window
        public static Point[] enemySpawnPoints =
        {
            new Point(0, -100),
            new Point(100, -100),
            new Point(200, -100),
            new Point(300, -100),
            new Point(400, -100),
            new Point(500, -100),
            new Point(600, -100),
            new Point(700, -100)
        };

        //Interval between enemy spawns, not constant because as the score goes up, the interval goes down to make the game more challenging
        public static int enemySpawnCoolDown = 3000;

        #endregion
    }
}
