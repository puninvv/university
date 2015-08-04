/**
 * Created by Виктор on 04.08.2015.
 */
public class myPoint {
    private double x;
    private double y;

    @Override
    public String toString() {
        return "(" + x + "," + y + ")";
    }

    public myPoint(double x, double y) {
        this.x = x;
        this.y = y;
    }

    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        myPoint myPoint = (myPoint) o;

        if (Double.compare(myPoint.x, x) != 0) return false;
        return Double.compare(myPoint.y, y) == 0;
    }

    @Override
    public int hashCode() {
        int result;
        long temp;
        temp = Double.doubleToLongBits(x);
        result = (int) (temp ^ (temp >>> 32));
        temp = Double.doubleToLongBits(y);
        result = 31 * result + (int) (temp ^ (temp >>> 32));
        return result;
    }

    public double CountDistaneTo(myPoint point){
        return Math.sqrt( (point.x-x)*(point.x-x) + (point.y - y)*(point.y - y) );
    }
}
