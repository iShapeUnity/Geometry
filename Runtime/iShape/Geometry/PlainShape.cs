using Unity.Collections;

namespace iShape.Geometry {

    public struct PlainShape {

        public struct Layout {

            public readonly int begin;
            public readonly int end;
            public readonly bool isHole;

            public Layout(int begin, int end, bool isHole) {
                this.begin = begin;
                this.end = end;
                this.isHole = isHole;
            }

            public bool isEmpty => begin == -1;
        }

        public readonly NativeArray<IntVector> points;
        public readonly NativeArray<Layout> layouts;

        public PlainShape(NativeArray<IntVector> points, NativeArray<Layout> layouts) {
            this.points = points;
            this.layouts = layouts;
        }

        public PlainShape(IntShape iShape, Allocator allocator) {
            var count = iShape.hull.Length;

            for(int j = 0; j < iShape.holes.Length; ++j) {
                count += iShape.holes[j].Length;
            }

            this.points = new NativeArray<IntVector>(count, allocator);
            this.layouts = new NativeArray<Layout>(iShape.holes.Length + 1, allocator);

            int layoutCounter = 0;

            int start = 0;
            int end = start + iShape.hull.Length - 1;

            int pointCounter = 0;
            for(int k = 0; k < iShape.hull.Length; ++k) {
                points[pointCounter++] = iShape.hull[k];
            }

            var layout = new Layout(start, end, false);
            layouts[layoutCounter++] = layout;

            start = end + 1;

            for(int j = 0; j < iShape.holes.Length; ++j) {
                var hole = iShape.holes[j];
                end = start + hole.Length - 1;
                for(int k = 0; k < hole.Length; ++k) {
                    points[pointCounter++] = hole[k];
                }

                layouts[layoutCounter++] = new Layout(start, end, true);


                start = end + 1;
            }
        }

        public void Dispose() {
            this.points.Dispose();
            this.layouts.Dispose();
        }

    }

}