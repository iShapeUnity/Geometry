namespace iShape.Geometry.Container {

    [System.Serializable]
    public struct PathLayout {
        
        public int begin;
        public int length;
        public bool isClockWise;
            
        public int end => begin + length - 1;

        public PathLayout(int begin, int length, bool isClockWise) {
            this.begin = begin;
            this.length = length;
            this.isClockWise = isClockWise;
        }
    }

}