namespace iShape.Geometry.Container {

    public struct Segment {
        public int begin;
        public int length;
        public int end => begin + length - 1;
        
        
        public Segment(int begin, int length) {
            this.begin = begin;
            this.length = length;
        }
    }

}