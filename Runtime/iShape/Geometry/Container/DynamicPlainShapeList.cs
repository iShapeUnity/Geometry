using Unity.Collections;

namespace iShape.Geometry.Container {

    public struct DynamicPlainShapeList {
        
        public DynamicPlainShapeList(Allocator allocator) {
            
        }
        
        public DynamicPlainShapeList(PlainShape plainShape, Allocator allocator) {
            
        }
        
        public DynamicPlainShapeList(int minimumPointsCapacity, int minimumLayoutsCapacity, int minimumSegmentsCapacity, Allocator allocator) {
            
        }
        
        public void Add(PlainShape shape) {

        }
        
        public void Add(DynamicPlainShape shape) {

        }

        public void Dispose() {

        }
        
        public PlainShapeList Convert() {
            return new PlainShapeList();
        }
    }

}