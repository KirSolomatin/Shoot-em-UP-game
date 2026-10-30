using shoot_em_up;
using System.Media;
namespace shoot_em_up.Test
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void PLAYER_DOES_NOT_GO_BEYOND_BOUNDARIES()
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

        [TestMethod]
        public void BOSS_CAN_SPAWN_ENEMIES()
        {
            //Arrange
            var boss = new Boss();

            //Act
            boss.Shoot();

            //Assert
            Assert.AreEqual(1, Game.EnemyList.Count);
        }
    }
}
