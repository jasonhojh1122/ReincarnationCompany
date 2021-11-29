
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

        public static void MatchBoxColliderToSprite(BoxCollider2D col, Sprite sprite)
        {
            col.offset = Vector2.zero;
            col.size = new Vector2(sprite.bounds.extents.x * 2.0f, sprite.bounds.extents.y * 2.0f);
        }
    }
}