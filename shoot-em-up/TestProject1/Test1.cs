using shoot_em_up;
using System.Media;
namespace shoot_em_up.Test
{
    [TestClass]
    public sealed class Test1
    {
        #region ================ Player ================
        [TestMethod]
        public void Player_Does_Not_Go_Beyond_Boundaries()
        {
            //Arrange
            var player = new Player();

            //Act
            for (int i = 0; i < 20; i++)
            {
                player.MoveRight();
            }
            //Assert
            Assert.AreEqual(700, player.PosX);
        }
        #endregion

        #region ================ Boss ================
        [TestMethod]
        public void Boss_Can_Spawn_Enemies()
        {
            //Arrange
            var boss = new Boss();

            //Act
            Game.EnemyList.Clear();
            boss.Shoot();

            //Assert
            Assert.AreEqual(1, Game.EnemyList.Count);
        }
        #endregion

        #region ================ Obstacle ================
        [TestMethod]
        public void GenerateObstacles_Called_Generates_6_Obstacles()
        {
            //Arrange
            List<Obstacle> obstaclesList = new List<Obstacle>();

            //Act
            obstaclesList = Obstacle.GenerateObstacle(6);

            //Assert
            Assert.AreEqual(6, obstaclesList.Count);
        }
        #endregion

        
       
    }
}
