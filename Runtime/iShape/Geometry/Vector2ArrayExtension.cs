using Unity.Collections;
using UnityEngine;

namespace iShape.Geometry {

    public static class Vector2ArrayExtension {
        
        public static Vector2 DoCentralSymmetry(this NativeSlice<Vector2> self) {
            float x = 0;
            float y = 0;
            int n = self.Length;
            for (int i = 0; i < n; ++i) {
                var p = self[i]; 
                x += p.x;
                y += p.y;
            }
        
            for (int i = 0; i < n; ++i) {
                var p = self[i];
                self[i] = new Vector2(p.x - x, p.y - y); 
            }

            return new Vector2(x, y);
        }
    }

}