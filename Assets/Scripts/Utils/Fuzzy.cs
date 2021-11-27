
using UnityEngine;

namespace Utils {
    public class Fuzzy
    {
        static float gap = 0.001f;
        public static bool CloseFloat(float a, float b)
        {
            return Mathf.Abs(a-b) <= gap;
        }

        public static bool CloseVector2(Vector2 a, Vector2 b)
        {
            return (a-b).magnitude < gap;
        }
    }
}