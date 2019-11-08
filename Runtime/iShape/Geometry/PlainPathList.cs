using Unity.Collections;
using iShape.Collections;

namespace iShape.Geometry {
    
    public struct PlainPathList {
        public struct Layout {

            public readonly int begin;
            public readonly int end;
            public readonly bool isClockWise;
        
            public Layout(int begin, int end, bool isClockWise) {
                this.begin = begin;
                this.end = end;
                this.isClockWise = isClockWise;
            }
        }

        public DynamicArray<IntVector> points;
        public DynamicArray<Layout> layouts; 
    
        public NativeArray<IntVector> GetPath(Layout layout, Allocator allocator) {
            int count = layout.end - layout.begin;
            var slice = new NativeArray<IntVector>(count, allocator);
            slice.Slice(0, count).CopyFrom(points.array.Slice(0, count));
            return slice;
        }
    

    
        public PlainPathList(int capacity, Allocator allocator) {
            this.points = new DynamicArray<IntVector>(10 * capacity, allocator);
            this.layouts = new DynamicArray<Layout>(capacity, allocator);
        }
    
        public PlainPathList(NativeArray<IntVector> points, NativeArray<Layout> layouts, Allocator allocator) {
            this.points = new DynamicArray<IntVector>(points, allocator);
            this.layouts = new DynamicArray<Layout>(layouts, allocator);
        }
    
        public void Add(NativeArray<IntVector> path, bool isClockWise) {
            int begin = points.Count;
            int end = begin + path.Length;
            var layout = new Layout(begin, end, isClockWise);
            points.Add(path);
            layouts.Add(layout);
        }
    }
}