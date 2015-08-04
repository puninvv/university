import FromUniversity.Matrix;

/**
 * Created by Виктор on 04.08.2015.
 */
public class myBasis {
    private myPoint start;
    private myVector right;
    private myVector left;

    public myBasis(myPoint start, myVector right, myVector left) {
        if (start.equals(null) || right.equals(null) || left.equals(null))
            throw new IllegalArgumentException("Не передавай пустые ссылки!");
        this.start = start;
        this.right = right;
        this.left = left;
    }

    public myPoint getStart() {
        return start;
    }

    public myVector getRight() {
        return right;
    }

    public myVector getLeft() {
        return left;
    }

    @Override
    public String toString() {
        return "myBasis{" + "start=" + start + ", right=" + right + ", left=" + left + '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        myBasis myBasis = (myBasis) o;

        if (!start.equals(myBasis.start)) return false;
        if (!right.equals(myBasis.right)) return false;
        return left.equals(myBasis.left);

    }

    @Override
    public int hashCode() {
        int result = start.hashCode();
        result = 31 * result + right.hashCode();
        result = 31 * result + left.hashCode();
        return result;
    }

    public myPoint GetCoordinatesOfVector(myVector vector){
        /*
        * vector.x = a1*right.x + a2*left.x
        * vector.y = a1*right.y + a2*left.y
        * */
        double[][] A = new double[2][2];
        A[0][0] = right.getEnd().getX() - right.getStart().getX();
        A[0][1] = left.getEnd().getX() - left.getStart().getX();
        A[1][0] = right.getEnd().getY() - right.getStart().getY();
        A[1][1] = left.getEnd().getY() - left.getStart().getY();

        double[] b = new double[2];
        b[0] = vector.getEnd().getX() - vector.getStart().getX();
        b[1] = vector.getEnd().getY() - vector.getStart().getY();

        double[] x = new Matrix(A).CountSLAY_PLU(b);

        return new myPoint(x[0],x[1]);
    }
}
