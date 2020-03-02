using Unity.Collections;

namespace iShape.Geometry.Container {

    public struct PlainShape {

        public NativeArray<IntVector> points;
        public NativeArray<PathLayout> layouts;
        public int Count => layouts.Length;

        public PlainShape(NativeArray<IntVector> points, NativeArray<PathLayout> layouts) {
            this.points = points;
            this.layouts = layouts;
        }
        
        public PlainShape(Allocator allocator) {
            this.points = new NativeArray<IntVector>(0, allocator);
            this.layouts = new NativeArray<PathLayout>(0, allocator);
        }

        public PlainShape(NativeArray<IntVector> points, bool isClockWise, Allocator allocator) {
            this.points = new NativeArray<IntVector>(points.Length, allocator);
            this.points.CopyFrom(points);
            this.layouts = new NativeArray<PathLayout>(1, allocator);
            this.layouts[0] = new PathLayout(0, points.Length, isClockWise);
        }

        public PlainShape(IntShape iShape, Allocator allocator) {
            var count = iShape.hull.Length;

            for(int j = 0; j < iShape.holes.Length; ++j) {
                count += iShape.holes[j].Length;
            }

            this.points = new NativeArray<IntVector>(count, allocator);
            this.layouts = new NativeArray<PathLayout>(iShape.holes.Length + 1, allocator);

            int layoutCounter = 0;

            int start = 0;
            int end = start + iShape.hull.Length - 1;

            int pointCounter = 0;
            for(int k = 0; k < iShape.hull.Length; ++k) {
                this.points[pointCounter++] = iShape.hull[k];
            }

            var layout = new PathLayout(start, iShape.hull.Length, true);
            this.layouts[layoutCounter++] = layout;

            start = end + 1;

            for(int j = 0; j < iShape.holes.Length; ++j) {
                var hole = iShape.holes[j];
                end = start + hole.Length - 1;
                for(int k = 0; k < hole.Length; ++k) {
                    this.points[pointCounter++] = hole[k];
                }

                this.layouts[layoutCounter++] = new PathLayout(start, hole.Length, false);

                start = end + 1;
            }
        }

        public IntVector DoCentralSymmetry() {
            var slice = this.Get(0);
            long x = 0;
            long y = 0;
            for (int i = 0; i < slice.Length; ++i) {
                var p = slice[i]; 
                x += p.x;
                y += p.y;
            }
            
            for (int i = 0; i < this.points.Length; ++i) {
                var p = this.points[i];
                this.points[i] = new IntVector(p.x - x, p.y - y); 
            }

            return new IntVector(x, y);
        }

        public NativeArray<IntVector> Get(int index, Allocator allocator) {
            var layout = this.layouts[index];
            var array = new NativeArray<IntVector>(layout.length, allocator);
            array.Slice(0, layout.length).CopyFrom(this.points.Slice(layout.begin, layout.length));
            return array;
        }
        
        public NativeSlice<IntVector> Get(int index) {
            var layout = this.layouts[index];
            return this.points.Slice(layout.begin, layout.length);
        }

        public void Dispose() {
            this.points.Dispose();
            this.layouts.Dispose();
        }

    }

}