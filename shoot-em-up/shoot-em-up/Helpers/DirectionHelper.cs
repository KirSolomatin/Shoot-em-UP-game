using shoot_em_up.Helpers;
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
            return (DirectionHelper.Directions)RandomHelper.random.Next(2);
        }
    }
}
