using Unity.Collections;

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

        public NativeArray<IntVector> points;
        public DynamicArray<Layout> layouts;
    
        public var pathes: [[IntPoint]] {
            let n = layouts.count
            var pathes = Array<[IntPoint]>()
            pathes.reserveCapacity(n)
            for i in 0..<n {
                let layout = self.layouts[i]
                let slice = self.points[layout.begin..<layout.end]
                pathes.append(Array(slice))
            }
            return pathes
        }
    
        public NativeArray<IntVector> getPath(Layout layout, Allocator allocator) {
            int count = layout.end - layout.begin;
            var slice = new NativeArray<IntVector>(count, allocator);
            slice.Slice(0, count).CopyFrom(points.Slice(0, count));
            
            
            //let slice = self.points[layout.begin..<layout.end]
            return slice;
        }
    

    
        public init() {
            self.points = [IntPoint]()
            self.layouts = [Layout]()
        }
    
        public init(points: [IntPoint], layouts: [Layout]) {
            self.points = points
            self.layouts = layouts
        }
    
        public mutating func append(path: [IntPoint], isClockWise: Bool) {
            let begin = points.count
            let end = begin + path.count
            let layout = Layout(
                begin: begin,
                end: end,
                isClockWise: isClockWise
            )
            points.append(contentsOf: path)
            layouts.append(layout)
        }
    }
}