using Unity.Collections;

namespace iShape.Geometry.Container {

    public struct PlainShapeList {

        public PlainShapeList(Allocator allocator) {
            
        }
        
        public PlainShapeList(PlainShape plainShape, Allocator allocator, bool dispose = false) {
            
        }
        
        public PlainShapeList(DynamicPlainShape plainShape, Allocator allocator, bool dispose = false) {
            
        }

        public void Dispose() {

        }
    }

}