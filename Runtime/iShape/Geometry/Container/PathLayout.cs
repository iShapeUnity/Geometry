namespace iShape.Geometry.Container {

    public struct PathLayout {
        public readonly int begin;
        public readonly int length;
        public readonly bool isClockWise;
            
        public int end => begin + length - 1;
        public bool isHole => !isClockWise;
        public bool isEmpty => begin == -1;

            
        public PathLayout(int begin, int length, bool isClockWise) {
            this.begin = begin;
            this.length = length;
            this.isClockWise = isClockWise;
        }
    }

}