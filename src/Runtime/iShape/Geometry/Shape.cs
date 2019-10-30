using UnityEngine;

namespace iShape.Geometry {

	public struct Shape {

		public Vector2[] hull;

		public Vector2[][] holes;

        public Shape(IntShape shape) : this(shape, IntGeom.DefGeom) { }

        public Shape(IntShape shape, IntGeom iGeom) {
			this.hull = iGeom.Float(shape.hull);
			this.holes = iGeom.Float(shape.holes);
        }

        public Shape(Vector2[] hull, Vector2[][] holes) {
            this.hull = hull;
            this.holes = holes;
        }

    }

}