namespace iShape.Geometry {

    public struct Vertex {

        public static readonly Vertex empty = new Vertex(0, IntVector.Zero);

        public int index;
        public IntVector point;

        public Vertex(int index, IntVector point) {
            this.index = index;
            this.point = point;
        }
    }

}