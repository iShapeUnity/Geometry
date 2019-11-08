using UnityEngine;

namespace iShape.Geometry {
    
    public static class Vector2Extension {

        public static float Multiply(this Vector2 self, Vector2 vector) {
            return self.x * vector.y - self.y * vector.x;
        }

    }
}