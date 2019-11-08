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

        public int Count => layouts.Count;

        private DynamicArray<IntVector> points;
        private DynamicArray<Layout> layouts; 
    
        public Layout GetLayout(int index) {
            return layouts[index];
        }

        public NativeArray<IntVector> GetPath(Layout layout, Allocator allocator) {
            int count = layout.end - layout.begin;
            var slice = new NativeArray<IntVector>(count, allocator);
            slice.Slice(0,count).CopyFrom(points.slice);
            return slice;
        }
        
        public NativeArray<IntVector> GetPath(int index, Allocator allocator) {
            return GetPath(layouts[index], allocator);
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
        
        public void Add(NativeSlice<IntVector> path, bool isClockWise) {
            int begin = points.Count;
            int end = begin + path.Length;
            var layout = new Layout(begin, end, isClockWise);
            points.Add(path);
            layouts.Add(layout);
        }
        
        public void Dispose() {
            this.points.Dispose();
            this.layouts.Dispose(); 
        }
    }
}