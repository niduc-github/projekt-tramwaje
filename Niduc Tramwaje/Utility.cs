using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Niduc_Tramwaje
{
    [Serializable]
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

        public static Color Blend(Color color1, Color color2, float amount) {
            amount = Clamp01(amount);
            byte A = (byte)(color1.A * (1 - amount) + color2.A * amount);
            byte G = (byte)(color1.G * (1 - amount) + color2.G * amount);
            byte R = (byte)(color1.R * (1 - amount) + color2.R * amount);
            byte B = (byte)(color1.B * (1 - amount) + color2.B * amount);
            return Color.FromArgb(A, R, G, B);
        }
    }
}
