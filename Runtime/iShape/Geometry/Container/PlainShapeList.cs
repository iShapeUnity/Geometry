using Unity.Collections;

namespace iShape.Geometry.Container {

    public struct PlainShapeList {

        public NativeArray<IntVector> points;
        public NativeArray<PathLayout> layouts;
        private NativeArray<Segment> segments;
        
        public int Count => segments.Length;
        
        public PlainShapeList(Allocator allocator) {
            this.points = new NativeArray<IntVector>(0, allocator);
            this.layouts = new NativeArray<PathLayout>(0, allocator);
            this.segments = new NativeArray<Segment>(0, allocator);
        }

        public PlainShapeList(NativeArray<IntVector> points, NativeArray<PathLayout> layouts, NativeArray<Segment> segments) {
            this.points = points;
            this.layouts = layouts;
            this.segments = segments;
        }
        
        public PlainShapeList(PlainShape plainShape, Allocator allocator) {
            this.points = new NativeArray<IntVector>(plainShape.points, allocator);
            this.layouts = new NativeArray<PathLayout>(plainShape.layouts, allocator);
            this.segments = new NativeArray<Segment>(1, allocator);
            this.segments[0] = new Segment(0, 1);
        }
        

        public PlainShapeList(NativeArray<IntVector> points, bool isClockWise, Allocator allocator) {
            this.points = new NativeArray<IntVector>(points, allocator);
            this.layouts = new NativeArray<PathLayout>(1, allocator); 
            this.layouts[0] = new PathLayout(0, points.Length, isClockWise);
            this.segments = new NativeArray<Segment>(1, allocator);
            this.segments[0] = new Segment(0, 1);
        }
        
        public PlainShapeList(DynamicPlainShape plainShape, Allocator allocator) {
            this.segments = new NativeArray<Segment>(1, allocator) {[0] = new Segment(0, 1)};
            this.points = plainShape.points.ToArray(allocator);
            this.layouts = plainShape.layouts.ToArray(allocator);
        }

        public void Dispose() {
            this.points.Dispose();
            this.layouts.Dispose();
            this.segments.Dispose();
        }
    }

}