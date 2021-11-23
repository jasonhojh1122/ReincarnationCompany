
using UnityEngine;

namespace Utils {
    public class Fuzzy
    {
        static float gap = 0.001f;
        public static bool CloseFloat(float a, float b)
        {
            return Mathf.Abs(a-b) <= gap;
        }
    }
}