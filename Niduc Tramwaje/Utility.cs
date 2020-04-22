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
    }
}
