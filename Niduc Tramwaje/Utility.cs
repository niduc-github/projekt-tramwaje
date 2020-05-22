using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niduc_Tramwaje
{
    static class Utility
    {
        public static float Clamp01(float value) {
            if (value < 0)
                return 0f;
            else if (value > 1f)
                return 1f;
            else
                return value;
        }

        public static int PingPong(int value, int min, int max) {
            if (min >= max)
                throw new Exception("Min musi być mniejsze od max!");
            while(value < min || value > max) {
                if (value > max)
                    value = 2 * max - value;
                else if (value < min)
                    value = 2 * min - value;
            }
            return value;
        }
    }
}
