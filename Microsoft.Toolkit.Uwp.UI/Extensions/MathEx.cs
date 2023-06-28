using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Microsoft.Toolkit.Extensions
{
    /// <summary>
    /// Compatibility for System.Math.Clamp for net7.0
    /// </summary>
    public static class MathEx
    {
        /// <summary>
        /// Clamps a value
        /// </summary>
        /// <param name="value">the value to clamp</param>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>Clamped value</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Clamp(double value, double min, double max)
        {
            if (min > max)
            {
                throw new InvalidOperationException();
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Clamps a value
        /// </summary>
        /// <param name="value">the value to clamp</param>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <returns>Clamped value</returns>
        public static int Clamp(int value, int min, int max)
        {
            if (min > max)
            {
                throw new InvalidOperationException();
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }
    }
}
