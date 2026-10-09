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
                    if (Game.bulletList[i].bulletCollision.IntersectsWith(Game.obstacleList[j].obstacleCollision) && Game.bulletList[i].bulletType != Bullet.BulletType.Knight)
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
                    if (Game.bulletList[i].bulletCollision.IntersectsWith(Game.enemyList[k].enemyCollision))
                    {
                        Game.enemyList.RemoveAt(k);
                        if (Game.bulletList[i].bulletType != Bullet.BulletType.Bishop) Game.bulletList.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}
