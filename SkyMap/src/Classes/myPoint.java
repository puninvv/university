package Classes;

/**
 * Created by Виктор on 04.08.2015.
 */
public class myPoint {
    private double x;
    private double y;

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
    public String toString(){
        return "("+x+";"+y+")";
    }
    public boolean equals(myPoint point){
        return ((point.x == x) && (point.y == y));
    }
    public double getDistance(myPoint point){
        return Math.sqrt( (x - point.x) * (x - point.x) + (y - point.y)*(y - point.y) );
    }
}
