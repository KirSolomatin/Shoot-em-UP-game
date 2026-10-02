using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up
{
    internal class DirectionHelper
    {
        //directions possible
        public enum Directions { Right, Left};

        //The direction in which a bullet will travel, for a knight and a bishop
        public static Directions bulletDirection; 

        public static Directions ChooseRandomDirection()
        {
            Random random = new Random();
            return (DirectionHelper.Directions)random.Next(2);
        }
    }
}
