using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up.Helpers
{
    internal static class CollisionHelper
    {
        // Check collisions for each bullet, starting from the end of the list
        public static void CheckBulletCollision()
        {
            for (int i = Game.bulletList.Count - 1; i >= 0; i--)
            {
                bool bulletRemoved = false;
                for (int j = Game.obstacleList.Count - 1; j >= 0; j--)
                {
                    if (Game.bulletList[i].BulletCollision.IntersectsWith(Game.obstacleList[j].ObstacleCollision) && Game.bulletList[i].BulletType != Bullet.BulletTypeList.Knight)
                    {
                        Game.bulletList.RemoveAt(i);
                        bulletRemoved = true;
                        break;
                    }
                }

                // Skip enemy collision checks if the bullet was removed
                if (bulletRemoved) continue;

                for (int k = Game.enemyList.Count - 1; k >= 0; k--)
                {
                    if (Game.bulletList[i].BulletCollision.IntersectsWith(Game.enemyList[k].EnemyCollision))
                    {
                        Game.enemyList.RemoveAt(k);
                        //plus 10 points for every killed enemy
                        ScoreSystem.Score += 20;
                        if (Game.bulletList[i].BulletType != Bullet.BulletTypeList.Bishop)
                        {
                            Game.bulletList.RemoveAt(i);
                            bulletRemoved = true;
                            break;
                        }
                    }
                }

                // Skip enemy collision checks if the bullet was removed
                if (bulletRemoved) continue;

                if (Game.boss != null && Game.bulletList[i].BulletCollision.IntersectsWith(Game.boss.BossCollision))
                {
                    Game.boss.Health--;
                    Game.bulletList.RemoveAt(i);
                }
            }
        }

        //Check collision for every enemy
        public static void CheckEnemyCollision()
        {
            for (int i = Game.enemyList.Count - 1; i >= 0; i--)
            {
                for (int j = Game.obstacleList.Count - 1; j >= 0; j--)
                {
                    if (Game.enemyList[i].EnemyCollision.IntersectsWith(Game.obstacleList[j].ObstacleCollision))
                    {
                        Game.enemyList.RemoveAt(i);
                        Game.obstacleList.RemoveAt(j);
                        break;
                    }
                }
            }
        }
    }
}
