using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shoot_em_up.Helpers
{
    internal class RandomHelper
    {
        public static Random random = new Random();

        // Random value between 0 (inclusive) and max (exclusive)
        public static int Next(int max) => random.Next(max);

        // Random value between min (inclusive) and max (exclusive)
        public static int Next(int min, int max) => random.Next(min, max);
    }
}
