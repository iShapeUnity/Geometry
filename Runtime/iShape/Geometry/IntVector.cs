using UnityEngine;

namespace iShape.Geometry {

    public struct IntVector {

        public static readonly IntVector Zero = new IntVector(0, 0);

        public readonly long x;
        public readonly long y;

        public long BitPack {
            get {
                return (x << IntGeom.maxBits) + y;
            }
        }

        public IntVector(long x, long y) {
            this.x = x;
            this.y = y;
        }

        public static IntVector operator+ (IntVector left, IntVector right) {
            return new IntVector(left.x + right.x, left.y + right.y);
        }
        
        public static IntVector operator- (IntVector left, IntVector right) {
            return new IntVector(left.x - right.x, left.y - right.y);
        }

        public long ScalarMultiply(IntVector vector) {
            return this.x * vector.x + vector.y * this.y;
        }   
    
        public IntVector Normal(IntGeom iGeom) {
            var p = iGeom.Float(this);
            var l = Mathf.Sqrt(p.x * p.x + p.y * p.y);
            float k = 1.0f / l;

            return iGeom.Int(new Vector2(k * p.x, k * p.y));
        }
    
        public long SqrDistance(IntVector vector) {
            var dx = vector.x - this.x;
            var dy = vector.y - this.y;

            return dx * dx + dy * dy;
        }

        public override string ToString() {
            return "x: " + x + " (" + IntGeom.DefGeom.Float(x) + ") , y: " + y + " (" + IntGeom.DefGeom.Float(y) + ")";
        }
    }

}